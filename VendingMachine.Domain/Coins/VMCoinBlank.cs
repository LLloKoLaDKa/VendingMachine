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
    }
}
