using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace VendingMachine.EntitiesCore.Extensions
{
    public static class DbSetExtensions
    {
        public static void AddOrUpdate<T>(this DbSet<T> dbSet, T obj) where T : class
        {
            T findedObject = dbSet.FirstOrDefault(v => v == obj);
            if (findedObject is null) dbSet.Add(obj);
            else dbSet.Update(obj);
        }
    }
}
