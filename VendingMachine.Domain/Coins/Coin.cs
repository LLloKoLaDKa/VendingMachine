using System;

namespace VendingMachine.Domain.Coins
{
    public class Coin
    {
        public Guid Id { get; }
        public Int32 Nominal { get; }

        public Coin(Guid id, Int32 nominal)
        {
            Id = id;
            Nominal = nominal;
        }
    }
}
