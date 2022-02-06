using System;

namespace VendingMachine.Domain.Coins
{
    public class VMCoin
    {
        public Guid Id { get; }
        public Guid VendingMachineId { get; }
        public Coin Coin { get; }
        public Int32 Count { get; private set; }
        public Boolean IsActive { get; private set; }

        public VMCoin(Guid id, Guid vendingMachineId, Coin coin, Int32 count, Boolean isActive)
        {
            Id = id;
            VendingMachineId = vendingMachineId;
            Coin = coin;
            Count = count;
            IsActive = isActive;
        }

        public void AddCount() => Count++;
        public void DecreaseCount() => Count--;
        public void DecreaseCount(Int32 count) => Count -= count;
    }
}
