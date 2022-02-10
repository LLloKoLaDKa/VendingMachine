using System;
using System.Threading.Tasks;

namespace VendingMachine.UI.Tools
{
    internal static class UserInterface
    {
        public static async Task Freeze(Func<Task> function)
        {
            App.Base.LoadingRun();
            await function();
            App.Base.LoadingStop();
        }

        public static async Task<T> Freeze<T>(Func<Task<T>> function)
        {
            App.Base.LoadingRun();
            T result = await function();
            App.Base.LoadingStop();

            return result;
        }
    }
}
