using System;
using System.Windows;

namespace VendingMachine.Domain.Drinks
{
    public class VMDrink
    {
        public Guid Id { get; }
        public Guid VendingMachineId { get; }
        public Drink Drink { get; }
        public Int32 Count { get; private set; }

        public VMDrink(Guid id, Guid vendingMachineId, Drink drink, Int32 count)
        {
            Id = id;
            VendingMachineId = vendingMachineId;
            Drink = drink;
            Count = count;
        }

        public void DecreaseCount() => Count--;
        public void AddCount() => Count++;
        public void AddCount(Int32 count) => Count += count;
    }
}
