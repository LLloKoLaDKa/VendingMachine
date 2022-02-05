using System;

namespace VendingMachine.Domain.Drinks
{
    public class VMDrinkBlank
    {
        public Guid? Id { get; set; }
        public String? Name { get; set; }
        public Byte[] Image { get; set; }
        public Guid? VendingMachineId { get; set; }
        public Guid? DrinkId { get; set; }
        public Int32? Nominal { get; set; } // Price
        public Int32? Count { get; set; }

        public VMDrinkBlank(Guid? id, String name, Byte[] image, Guid? vendingMachineId, Guid? drinkId, Int32? nominal, Int32? count)
        {
            Id = id;
            Name = name;
            Image = image;
            VendingMachineId = vendingMachineId;
            DrinkId = drinkId;
            Nominal = nominal;
            Count = count;
        }
    }
}
