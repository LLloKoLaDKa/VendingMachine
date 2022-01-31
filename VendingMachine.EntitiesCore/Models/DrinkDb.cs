using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachine.EntitiesCore.Models
{
    [Table("Drinks")]
    internal class DrinkDb
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public String Name { get; set; }

        [Column("image")]
        public Byte[] Image { get; set; }

        [Column("price")]
        public Int32 Price { get; set; }

        public DrinkDb(Guid id, String name, Byte[] image, Int32 price)
        {
            Id = id;
            Name = name;
            Image = image;
            Price = price;
        }
    }
}
