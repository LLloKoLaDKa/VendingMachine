using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using VendingMachine.Domain;
using VendingMachine.Domain.Coins;
using VendingMachine.Domain.Drinks;
using VendingMachine.Domain.Results;

namespace VendingMachine.UI.Tools
{
    internal static class HttpHelper
    {
        private static readonly HttpClient _client = new();
        public static readonly String _host = "https://localhost:44364/";

        #region Coins

        public static async Task<Result> SaveCoins(VMCoinBlank[] blanks)
        {
            String fullUrl = _host + "Coins/Save";

            HttpRequestMessage request = new(HttpMethod.Post, fullUrl);
            request.Content = JsonContent.Create(blanks);
            HttpResponseMessage responseMessage = await _client.SendAsync(request);
            String response = await responseMessage.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Result>(response);
        }

        public async static Task<VMCoin[]> GetVmCoins(Guid vendingMachineId)
        {
            String fullUrl = _host + $"Coins/GetAll?vendingMachineId={vendingMachineId}";

            HttpRequestMessage request = new(HttpMethod.Get, fullUrl);
            HttpResponseMessage responseMessage = await _client.SendAsync(request);
            String response = await responseMessage.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<VMCoin[]>(response);
        }

        #endregion Coins

        #region Drinks

        public static async Task<Result> SaveDrinks(VMDrink[] blanks)
        {
            String fullUrl = _host + "Drinks/Save";

            HttpRequestMessage request = new(HttpMethod.Post, fullUrl);
            request.Content = JsonContent.Create(blanks);
            HttpResponseMessage responseMessage = await _client.SendAsync(request);
            String response = await responseMessage.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Result>(response);
        }

        public async static Task<VMDrink[]> GetVmDrinks(Guid vendingMachineId)
        {
            String fullUrl = _host + $"Drinks/GetAll?vendingMachineId={vendingMachineId}";

            HttpRequestMessage request = new(HttpMethod.Get, fullUrl);
            HttpResponseMessage responseMessage = await _client.SendAsync(request);
            String response = await responseMessage.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<VMDrink[]>(response);
        }

        #endregion Drinks

        #region VendingMachines

        public async static Task<VendingMachineDomain> GetVendingMachine(Guid vendingMachineId)
        {
            String fullUrl = _host + $"VendingMachines/GetById?vendingMachineId={vendingMachineId}";
            String response = await _client.GetStringAsync(fullUrl);

            return JsonSerializer.Deserialize<VendingMachineDomain>(response);
        }

        #endregion VendingMachines
    }
}
