using System;
using System.Linq;
using VendingMachine.Domain.Coins;
using VendingMachine.EntitiesCore.Extensions;
using VendingMachine.EntitiesCore.Models;
using VendingMachine.EntitiesCore.Models.Converters;
using VendingMachine.EntitiesCore.Repositories.Interfaces;

namespace VendingMachine.EntitiesCore.Repositories
{
    public class CoinsRepository : BaseRepository, ICoinsRepository
    {
        public void SaveCoins(VMCoinBlank[] coinBlanks)
        {
            UseContext(context =>
            {
                foreach (VMCoinBlank blank in coinBlanks)
                {
                    VMCoinDb coinDb = blank.ToVMCoinDb();

                    context.Attach(coinDb);
                    context.VMCoins.AddOrUpdate(coinDb, context);
                    context.SaveChanges();
                }
            });
        }

        public VMCoin[] GetAllCoins(Guid vendingMachineId)
        {
            return UseContext(context =>
            {
                VMCoinDb[] vmCoinsDbs = context.VMCoins.Where(c => c.VendingMachineId == vendingMachineId).ToArray();
                CoinDb[] coinDbs = context.Coins.Where(c => vmCoinsDbs.Select(vcd => vcd.CoinId).Contains(c.Id)).ToArray();

                return vmCoinsDbs.ToVmCoins(coinDbs).OrderBy(c => c.Coin.Nominal).ToArray();
            });
        }
    }
}
