using System;
using VendingMachine.Domain;

namespace VendingMachine.EntitiesCore.Repositories.Interfaces
{
    public interface IVendingMachineRepository
    {
        public VendingMachineDomain GetVendingMachine(Guid vendingMachineId);
    }
}
