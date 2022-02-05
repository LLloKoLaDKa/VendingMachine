using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Domain.Drinks;

namespace VendingMachine.EntitiesCore.Models.Converters
{
    public static class DrinksConverters
    {
        #region Drinks

        public static Drink ToDrink(this DrinkDb db)
        {
            return new(db.Id, db.Name, db.Image, db.Price);
        }

        public static Drink[] ToDrinks(this IEnumerable<DrinkDb> dbs)
        {
            return dbs.Select(ToDrink).ToArray();
        }

        public static DrinkDb ToDrinkDb(this VMDrinkBlank blank)
        {
            return new(blank.DrinkId.Value, blank.Name!, blank.Image, blank.Nominal.Value); ;
        }

        #endregion Drinks

        #region VMDrinks

        public static VMDrink ToVMDrink(this VMDrinkDb db, DrinkDb drinkDb)
        {
            Drink drink = drinkDb.ToDrink();
            return new(db.Id, db.VendingMachineId, drink, db.Count);
        }

        public static VMDrink[] ToVMDrinks(this IEnumerable<VMDrinkDb> dbs, DrinkDb[] drinkDbs)
        {
            return dbs
                .Where(d => drinkDbs.Any(drink => drink.Id == d.DrinkId))
                .Select(d => d.ToVMDrink(drinkDbs.First(drink => drink.Id == d.DrinkId))).ToArray();
        }

        public static VMDrinkDb ToVMDrinkDb(this VMDrinkBlank blank)
        {
            return new(blank.Id.Value, blank.VendingMachineId.Value, blank.DrinkId.Value, blank.Count.Value);
        }

        #endregion VMDrinks
    }
}
