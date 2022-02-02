using System;

namespace VendingMachine.Domain.Drinks
{
    public class Drink
    {
        public Guid Id { get; }
        public String Name { get; }
        public Byte[] Image { get; }
        public Int32 Price { get; }

        public Drink(Guid id, String name, Byte[] image, Int32 price)
        {
            Id = id;
            Name = name;
            Image = image;
            Price = price;
        }
    }
}
