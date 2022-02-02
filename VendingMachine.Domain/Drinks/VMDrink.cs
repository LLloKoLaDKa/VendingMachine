using System;

namespace VendingMachine.Domain.Drinks
{
    public class VMDrink
    {
        public Guid Id { get; }
        public Guid VendingMachineId { get; }
        public Guid DrinkId { get; }
        public Int32 Count { get; }

        public VMDrink(Guid id, Guid vendingMachineId, Guid drinkId, Int32 count)
        {
            Id = id;
            VendingMachineId = vendingMachineId;
            DrinkId = drinkId;
            Count = count;
        }
    }
}
