using System;
using VendingMachine.Domain.Coins;

namespace VendingMachine.EntitiesCore.Repositories.Interfaces
{
    public interface ICoinsRepository
    {
        public void SaveCoins(VMCoinBlank[] coinBlanks); 
        public VMCoin[] GetAllCoins(Guid vendingMachineId);
    }
}
