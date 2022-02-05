using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using VendingMachine.Domain.Drinks;
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
                DrinkDb drinkDb = vmDrinkBlank.ToDrinkDb();
                context.Attach(drinkDb);
                context.Drinks.AddOrUpdate(drinkDb, context);

                VMDrinkDb vmDrinkDb = vmDrinkBlank.ToVMDrinkDb();
                context.Attach(vmDrinkDb);
                context.VMDrinks.AddOrUpdate(vmDrinkDb, context);

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
    }
}
