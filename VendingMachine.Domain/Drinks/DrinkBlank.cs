using System;

namespace VendingMachine.Domain.Drinks
{
    public class DrinkBlank
    {
        public Guid? Id { get; set; }
        public String? Name { get; set; }
        public Byte[] Image { get; set; }
        public Int32? Price { get; set; }
        public Int32? Count { get; set; }
    }
}
