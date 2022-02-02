using VendingMachine.Domain;

namespace VendingMachine.EntitiesCore.Models.Converters
{
    public static class VendingMachineConverters
    {
        public static VendingMachineDomain ToVendingMachine(this VendingMachineDb db)
        {
            return new(db.Id, db.SecretCode);
        }
    }
}
