using System;
using System.Linq;
using VendingMachine.Domain;
using VendingMachine.EntitiesCore.Extensions;
using VendingMachine.EntitiesCore.Models;
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

        public Boolean Login(Guid vendingMachineId, String password)
        {
            return UseContext(context =>
            {
                VendingMachineDb? machineDb = context.VendingMachines.FirstOrDefault(vm => vm.Id == vendingMachineId);
                if (machineDb is null) return false;

                return machineDb.SecretCode == password.ToMD5Hash();
            });
        }
    }
}
