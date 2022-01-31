using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachine.EntitiesCore.Models
{
    [Table("Coins")]
    internal class CoinDb
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("nominal")]
        public Int32 Nominal { get; set; }

        public CoinDb(Guid id, Int32 nominal)
        {
            Id = id;
            Nominal = nominal;
        }
    }
}
