using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Domain.Drinks;
using VendingMachine.Domain.Reports;
using VendingMachine.EntitiesCore.Extensions;
using VendingMachine.EntitiesCore.Models;
using VendingMachine.EntitiesCore.Models.Converters;
using VendingMachine.EntitiesCore.Repositories.Interfaces;

namespace VendingMachine.EntitiesCore.Repositories
{
    public class DrinksRepository : BaseRepository, IDrinksRepository
    {
        public void SaveDrink(VMDrinkBlank vmDrinkBlank)
        {
            UseContext(context =>
            {
                // если изменяется только цена => не делать пополнение
                DrinkDb drinkDb = vmDrinkBlank.ToDrinkDb();
                context.Attach(drinkDb);
                context.Drinks.AddOrUpdate(drinkDb, context);

                VMDrinkDb vmDrinkDb = vmDrinkBlank.ToVMDrinkDb();
                context.Attach(vmDrinkDb);
                context.VMDrinks.AddOrUpdate(vmDrinkDb, context);

                VMDrinkHistoryDb historyDb = new(Guid.NewGuid(), vmDrinkDb.DrinkId, vmDrinkDb.Count, drinkDb.Price, HistoryType.Refill);
                context.VMDrinkHistories.Add(historyDb);

                context.SaveChanges();
            });
        }

        public void PurchaseFixation(VMDrinkBlank[] vmDrinkBlanks)
        {
            UseContext(context =>
            {
                List<VMDrinkDb> vmDrinkDb = context.VMDrinks.ToArray().Where(d => vmDrinkBlanks.Any(b => b.Id == d.Id)).ToList();
                List<VMDrinkHistoryDb> histories = new();
                vmDrinkDb.ForEach(d =>
                {
                    VMDrinkBlank blank = vmDrinkBlanks.First(b => b.Id == d.Id);
                    if (blank.Count == d.Count) return;

                    Int32 soldCount = d.Count - blank.Count.Value;
                    d.Count = blank.Count.Value;
                    context.Entry(d).State = EntityState.Modified;

                    VMDrinkHistoryDb historyDb = new(Guid.NewGuid(), d.DrinkId, soldCount, blank.Nominal.Value, HistoryType.Buy);
                    context.Entry(historyDb).State = EntityState.Added;
                });

                
                context.VMDrinkHistories.AddRange(histories);
                context.SaveChanges();
            });
        }

        public VMDrink[] GetAllDrinks(Guid vendingMachineId)
        {
            return UseContext(context =>
            {
                VMDrinkDb[] vmDrinkDbs = context.VMDrinks.Where(d => d.VendingMachineId == vendingMachineId).ToArray();
                DrinkDb[] drinkDbs = context.Drinks.Where(d => vmDrinkDbs.Select(d => d.DrinkId).Contains(d.Id)).ToArray();
                return vmDrinkDbs.ToVMDrinks(drinkDbs);
            });
        }

        public void DeleteDrink(Guid drinkId)
        {
            UseContext(context =>
            {
                VMDrinkDb vmDrinkDb = context.VMDrinks.FirstOrDefault(d => d.Id == drinkId);
                if (vmDrinkDb is null) return;

                context.Entry(vmDrinkDb).State = EntityState.Deleted;

                DrinkDb drinkDb = context.Drinks.FirstOrDefault(d => d.Id == vmDrinkDb.DrinkId);
                if (drinkDb is not null) context.Entry(drinkDb).State = EntityState.Deleted;

                context.SaveChanges();
            });
        }

        #region DrinkReports

        public DrinkReport[] GetDrinkReports(Guid[] drinksIds)
        {
            return UseContext(context =>
            {
                VMDrinkHistoryDb[] historyDbs = context.VMDrinkHistories.Where(h => drinksIds.Contains(h.DrinkId)).ToArray();
                VMDrinkHistoryDb[] reportDbs = GetReportDbs(historyDbs);

                Guid[] drinkIds = reportDbs.Select(r => r.DrinkId).ToArray();
                DrinkDb[] drinkDbs = context.Drinks.Where(d => drinkIds.Contains(d.Id)).ToArray();
                VMDrinkDb[] vmDrinkDbs = context.VMDrinks.Where(d => drinkIds.Contains(d.DrinkId)).ToArray();

                DrinkHistory[] histories = historyDbs.Where(h => h.Type == HistoryType.Buy).ToHistories();

                return reportDbs
                    .Select(r => {
                        Drink drink = drinkDbs.First(d => d.Id == r.DrinkId).ToDrink();
                        VMDrinkDb vmDrinkDb = vmDrinkDbs.First(d => d.DrinkId == drink.Id);

                        return new DrinkReport(r.Id, r.Count, vmDrinkDb.Count, r.Nominal, r.Date, drink,
                            histories.Where(h => h.DrinkId == r.DrinkId && h.Date >= r.Date).OrderBy(h => h.Date).ToArray());
                    })
                    .OrderBy(r => r.Drink.Name)
                    .ToArray();
            });
        }

        private VMDrinkHistoryDb[] GetReportDbs(VMDrinkHistoryDb[] histories)
        {
            List<VMDrinkHistoryDb> result = new();
            VMDrinkHistoryDb[] reportDbs = histories
                .Where(h => h.Type == HistoryType.Refill)
                .OrderByDescending(h => h.Date)
                .ToArray();

            foreach (VMDrinkHistoryDb report in reportDbs)
            {
                VMDrinkHistoryDb history = result.FirstOrDefault(i => i.DrinkId == report.DrinkId);
                if (history is not null) continue;

                result.Add(report);
            }

            return result.ToArray();
        }

        #endregion DrinkReports
    }
}
