using System;
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
                context.Drinks.AddOrUpdate(drinkDb);

                VMDrinkDb vmDrinkDb = vmDrinkBlank.ToVMDrinkDb();
                context.Attach(vmDrinkDb);
                context.VMDrinks.AddOrUpdate(vmDrinkDb);

                context.SaveChanges();
            });
        }

        public void GetAllDrinks(Guid VendingMachineId)
        {
            throw new NotImplementedException();
        }
    }
}
