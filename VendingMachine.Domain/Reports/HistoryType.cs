namespace VendingMachine.Domain.Reports
{
    public enum HistoryType
    {
        /// <summary>
        /// Заправка
        /// </summary>
        Refill = 1,

        /// <summary>
        /// Покупка
        /// </summary>
        Buy = 2,

        /// <summary>
        /// Изменение цены
        /// </summary>
        PriceChange = 3
    }
}
