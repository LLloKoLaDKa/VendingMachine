using System;

namespace VendingMachine.Domain.Coins
{
    public class VMCoinBlank
    {
        public Guid Id { get; set; }
        public Guid VendingMachineId { get; set; }
        public Guid CoinId { get; set; }
        public Int32 Nominal { get; set; }
        public Int32 Count { get; set; }
        public Boolean IsActive { get; set; }

        public VMCoinBlank(Guid id, Guid vendingMachineId, Guid coinId, Int32 nominal, Int32 count, Boolean isActive)
        {
            Id = id;
            VendingMachineId = vendingMachineId;
            CoinId = coinId;
            Nominal = nominal;
            Count = count;
            IsActive = isActive;
        }
    }
}
