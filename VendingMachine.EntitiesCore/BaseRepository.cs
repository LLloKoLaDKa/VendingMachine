using System;

namespace VendingMachine.EntitiesCore
{
    public abstract class BaseRepository
    {
        internal void UseContext(Action<VendingMachineContext> action)
        {
            using (VendingMachineContext _context = new())
            {
                action(_context);
            }
        }

        internal T UseContext<T>(Func<VendingMachineContext, T> action)
        {
            using (VendingMachineContext _context = new())
            {
                T result = action(_context);
                return result;
            }
        }
    }
}
