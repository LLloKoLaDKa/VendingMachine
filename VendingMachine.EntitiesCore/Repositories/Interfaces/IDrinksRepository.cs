using System;
using VendingMachine.Domain.Drinks;
using VendingMachine.Domain.Reports;

namespace VendingMachine.EntitiesCore.Repositories.Interfaces
{
    public interface IDrinksRepository
    {
        public void SaveDrink(VMDrinkBlank vmDrinkBlank);
        public void PurchaseFixation(VMDrinkBlank[] vmDrinkBlanks);
        public VMDrink[] GetAllDrinks(Guid vendingMachineId);
        public void DeleteDrink(Guid drinkId);

        #region DrinkReports

        public DrinkReport[] GetDrinkReports(Guid[] drinksIds, Guid vendingMachineId);

        #endregion DrinkReports
    }
}
