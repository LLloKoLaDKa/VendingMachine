using System;

namespace VendingMachine.Domain.Reports
{
    public class DrinkHistory
    {
        public Guid Id { get; }
        public Guid DrinkId { get; }

        /// <summary>
        /// При Type = Refill => количество после заправки
        /// При Type = Buy => Количество покупки
        /// </summary>
        public Int32 Count { get; }
        public Int32 Price { get; }
        public HistoryType Type { get; }
        public DateTime Date { get; }

        public Int32 Profit => Count * Price;

        public DrinkHistory(Guid id, Guid drinkId, Int32 count, Int32 price, HistoryType type, DateTime date)
        {
            Id = id;
            DrinkId = drinkId;
            Count = count;
            Price = price;
            Type = type;
            Date = date;
        }
    }
}
