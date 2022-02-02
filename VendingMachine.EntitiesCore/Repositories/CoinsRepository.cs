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
        public void SaveCoins(CoinBlank[] coinBlanks)
        {
            UseContext(context =>
            {
                foreach (CoinBlank blank in coinBlanks)
                {
                    CoinDb coinDb = blank.ToCoinDb();

                    context.Attach(coinDb);
                    context.Coins.AddOrUpdate(coinDb);
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

                return vmCoinsDbs.ToVmCoins(coinDbs);
            });
        }
    }
}
