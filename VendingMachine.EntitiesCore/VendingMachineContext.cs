using Microsoft.EntityFrameworkCore;
using VendingMachine.EntitiesCore.Models;

namespace VendingMachine.EntitiesCore
{
    public class VendingMachineContext : DbContext
    {
        private static DbContextOptions<VendingMachineContext> _dbContextOptions =
            new DbContextOptionsBuilder<VendingMachineContext>()
            .UseSqlServer(@"Server=Willy\SQLEXPRESS; Database=VendingMachine;Trusted_Connection=True;")
            .Options;

        internal DbSet<VendingMachineDb> VendingMachines { get; set; }
        internal DbSet<CoinDb> Coins { get; set; }
        internal DbSet<DrinkDb> Drinks { get; set; }
        internal DbSet<VMCoinDb> VMCoins { get; set; }
        internal DbSet<VMDrinkDb> VMDrinks { get; set; }
        internal DbSet<VMDrinkHistoryDb> VMDrinkHistories { get; set; }

        public VendingMachineContext() : base(_dbContextOptions) { }
    }
}
