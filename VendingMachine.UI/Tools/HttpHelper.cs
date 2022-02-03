using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using VendingMachine.Domain;
using VendingMachine.Domain.Coins;

namespace VendingMachine.UI.Tools
{
    internal static class HttpHelper
    {
        private static readonly HttpClient _client = new();
        public static readonly String _host = "https://localhost:44364/";

        public async static Task<VMCoin[]> GetVmCoins(Guid vendingMachineId)
        {
            String fullUrl = _host + $"Coins/GetAll?vendingMachineId={vendingMachineId}";
            String response = await _client.GetStringAsync(fullUrl);

            return JsonSerializer.Deserialize<VMCoin[]>(response);
        }

        public async static Task<VendingMachineDomain> GetVendingMachine(Guid vendingMachineId)
        {
            String fullUrl = _host + $"VendingMachines/GetById?vendingMachineId={vendingMachineId}";
            String response = await _client.GetStringAsync(fullUrl);

            return JsonSerializer.Deserialize<VendingMachineDomain>(response);
        }
    }
}
