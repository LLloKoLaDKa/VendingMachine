using System;

namespace VendingMachine.Domain.Coins
{
    public class VMCoin
    {
        public Guid Id { get; }
        public Guid VendingMachineId { get; }
        public Int32 Nominal { get; }
        public Int32 Count { get; }
        public Boolean IsActive { get; }

        public VMCoin(Guid id, Guid vendingMachineId, Int32 nominal, Int32 count, Boolean isActive)
        {
            Id = id;
            VendingMachineId = vendingMachineId;
            Nominal = nominal;
            Count = count;
            IsActive = isActive;
        }
    }
}
