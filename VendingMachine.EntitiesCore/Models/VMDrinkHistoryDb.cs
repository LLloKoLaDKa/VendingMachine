using System;
using System.ComponentModel.DataAnnotations.Schema;
using VendingMachine.Domain.Reports;

namespace VendingMachine.EntitiesCore.Models
{
    [Table("VMDrinkHistories")]
    public class VMDrinkHistoryDb
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("drinkid")]
        public Guid DrinkId { get; set; }

        /// <summary>
        /// При Type = Refill => количество после заправки
        /// При Type = Buy => Количество покупки
        /// </summary>
        [Column("count")]
        public Int32 Count { get; set; }

        [Column("nominal")]
        public Int32 Nominal { get; set; }

        [Column("type")]
        public HistoryType Type { get; set; }

        [Column("datetime")]
        public DateTime Date {get;set;}

        public VMDrinkHistoryDb(Guid id, Guid drinkId, Int32 count, Int32 nominal, HistoryType type)
        {
            Id = id;
            DrinkId = drinkId;
            Count = count;
            Nominal = nominal;
            Type = type;
            Date = DateTime.Now;
        }
    }
}
