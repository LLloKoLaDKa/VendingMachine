using System.Collections.Generic;
using System.Linq;
using VendingMachine.Domain.Reports;

namespace VendingMachine.EntitiesCore.Models.Converters
{
    internal static class ReportConverters
    {
        public static DrinkHistory ToHistory(this VMDrinkHistoryDb db)
        {
            return new DrinkHistory(db.Id, db.DrinkId, db.Count, db.Nominal, db.Type, db.Date);
        }

        public static DrinkHistory[] ToHistories(this IEnumerable<VMDrinkHistoryDb> dbs)
        {
            return dbs.Select(ToHistory).ToArray();
        }
    }
}
