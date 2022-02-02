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
        public Int32? Nominal { get; set; }
        public Int32? Count { get; set; }
    }
}
