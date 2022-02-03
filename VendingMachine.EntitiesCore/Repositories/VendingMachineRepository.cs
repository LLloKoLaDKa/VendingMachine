using System;
using System.Linq;
using VendingMachine.Domain;
using VendingMachine.EntitiesCore.Models.Converters;
using VendingMachine.EntitiesCore.Repositories.Interfaces;

namespace VendingMachine.EntitiesCore.Repositories
{
    public class VendingMachineRepository : BaseRepository, IVendingMachineRepository
    {
        public VendingMachineDomain GetVendingMachine(Guid vendingMachineId)
        {
            return UseContext(context =>
            {
                return context.VendingMachines.FirstOrDefault(vm => vm.Id == vendingMachineId).ToVendingMachine();
            });
        }
    }
}
