using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachine.EntitiesCore.Models
{
    [Table("VMDrinks")]
    internal class VMDrinkDb
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("vmid")]
        public Guid VendingMachineId { get; set; }

        [Column("drinkid")]
        public Guid DrinkId { get; set; }

        [Column("count")]
        public Int32 Count { get; set; }

        public VMDrinkDb(Guid id, Guid vendingMachineId, Guid drinkId, Int32 count)
        {
            Id = id;
            VendingMachineId = vendingMachineId;
            DrinkId = drinkId;
            Count = count;
        }
    }
}
