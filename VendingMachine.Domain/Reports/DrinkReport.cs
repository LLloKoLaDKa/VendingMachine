using System;
using System.Linq;
using VendingMachine.Domain.Drinks;

namespace VendingMachine.Domain.Reports
{
    public class DrinkReport
    {
        public Guid RefillId { get; }
        public Int32 RefillCount { get; }
        public Int32 AvailableCount { get; }
        public Int32 RefillPrice { get; }
        public DateTime RefillDate { get; }
        public Drink Drink { get; }
        public DrinkHistory[] Purchases { get; }

        public String RefillDateString => RefillDate.ToString("dd/MM/yyyy");
        public Int32 Profit => Purchases.Select(p => p.Price * p.Count).Sum();

        public DrinkReport(Guid refillId, Int32 refillCount, Int32 availableCount, Int32 refillPrice, DateTime refillDate, Drink drink, DrinkHistory[] purchases)
        {
            RefillId = refillId;
            RefillCount = refillCount;
            AvailableCount = availableCount;
            RefillPrice = refillPrice;
            RefillDate = refillDate;
            Drink = drink;
            Purchases = purchases;
        }
    }
}
