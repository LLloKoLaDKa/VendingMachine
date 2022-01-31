﻿using Microsoft.EntityFrameworkCore;
using VendingMachine.EntitiesCore.Models;

namespace VendingMachine.EntitiesCore
{
    public class VendingMachineContext : DbContext
    {
        private static DbContextOptions<VendingMachineContext> _dbContextOptions =
            new DbContextOptionsBuilder<VendingMachineContext>()
            .UseSqlServer(@"Server=DESKTOP-9SV6HT1\SQLEXPRESS; Database=VendingMachine;Trusted_Connection=True;")
            .Options;

        internal DbSet<VendingMachineDb> VendingMachines { get; set; }
        internal DbSet<CoinDb> Coins { get; set; }
        internal DbSet<DrinkDb> Drinks { get; set; }
        internal DbSet<VMCoinDb> VMCoins { get; set; }
        internal DbSet<VMCoinDb> VMDrinks { get; set; }

        public VendingMachineContext() : base(_dbContextOptions) { }
    }
}
