using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachine.EntitiesCore.Models
{
    [Table("VMCoins")]
    internal class VMCoinDb
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("vmid")]
        public Guid VendingMachineId { get; set; }

        [Column("coinid")]
        public Guid CoinId { get; set; }

        [Column("count")]
        public Int32 Count { get; set; }

        [Column("isactive")]
        public Boolean IsActive { get; set; }

        public VMCoinDb(Guid id, Guid vendingMachineId, Guid coinId, Int32 count, Boolean isActive)
        {
            Id = id;
            VendingMachineId = vendingMachineId;
            CoinId = coinId;
            Count = count;
            IsActive = isActive;
        }
    }
}
