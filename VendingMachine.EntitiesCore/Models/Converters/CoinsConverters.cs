using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Domain.Coins;

namespace VendingMachine.EntitiesCore.Models.Converters
{
    internal static class CoinsConverters
    {
        #region Coins

        public static Coin ToCoin(this CoinDb db)
        {
            return new(db.Id, db.Nominal);
        }

        public static Coin[] ToCoins(this IEnumerable<CoinDb> dbs)
        {
            return dbs.Select(ToCoin).ToArray();
        }

        public static CoinDb ToCoinDb(this CoinBlank vmCoinBlank)
        {
            return new(Guid.NewGuid(), vmCoinBlank.Nominal.Value);
        }

        #endregion Coins

        #region VMCoins

        public static VMCoin ToVMCoin(this VMCoinDb vmCoinDb, CoinDb coinDb)
        {
            return new(vmCoinDb.Id, vmCoinDb.VendingMachineId, coinDb.ToCoin(), vmCoinDb.Count, vmCoinDb.IsActive);
        }

        public static VMCoin[] ToVmCoins(this IEnumerable<VMCoinDb> dbs, IEnumerable<CoinDb> coinDbs)
        {
            return dbs.Select(db => db.ToVMCoin(coinDbs.First(c => c.Id == db.CoinId))).ToArray();
        }

        public static VMCoinDb ToVMCoinDb(this VMCoinBlank vMCoinBlank)
        {
            return new(vMCoinBlank.Id, vMCoinBlank.VendingMachineId, vMCoinBlank.CoinId, vMCoinBlank.Count, vMCoinBlank.IsActive);
        }

        #endregion VMCoins
    }
}
