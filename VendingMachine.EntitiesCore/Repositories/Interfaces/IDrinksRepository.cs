using System;
using VendingMachine.Domain.Drinks;

namespace VendingMachine.EntitiesCore.Repositories.Interfaces
{
    public interface IDrinksRepository
    {
        public void SaveDrink(VMDrinkBlank vmDrinkBlank);
        public VMDrink[] GetAllDrinks(Guid VendingMachineId);
    }
}
