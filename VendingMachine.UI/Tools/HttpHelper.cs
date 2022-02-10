using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using VendingMachine.Domain;
using VendingMachine.Domain.Coins;
using VendingMachine.Domain.Drinks;
using VendingMachine.Domain.Reports;
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

        public static async Task<Result> SaveDrink(VMDrinkBlank blank)
        {
            String fullUrl = _host + "Drinks/Save";

            HttpRequestMessage request = new(HttpMethod.Post, fullUrl);
            request.Content = JsonContent.Create(blank);
            HttpResponseMessage responseMessage = await _client.SendAsync(request);
            String response = await responseMessage.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Result>(response);
        }

        public static async Task<Result> SaveDrinks(VMDrinkBlank[] blanks)
        {
            String fullUrl = _host + "Drinks/SaveDrinks";

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

        public async static Task<Result> DeleteDrink(Guid drinkId)
        {
            String fullUrl = _host + "Drinks/Delete";

            HttpRequestMessage request = new(HttpMethod.Post, fullUrl);
            request.Content = JsonContent.Create(drinkId);
            HttpResponseMessage responseMessage = await _client.SendAsync(request);
            String response = await responseMessage.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Result>(response);
        }

        public async static Task<DrinkReport[]> GetDrinkReports(Guid[] drinkIds)
        {
            String fullUrl = _host + $"Drinks/GetReports?vendingMachineId={App.VendingMachine.Id}";

            HttpRequestMessage request = new(HttpMethod.Post, fullUrl);
            request.Content = JsonContent.Create(drinkIds);
            HttpResponseMessage responseMessage = await _client.SendAsync(request);
            String response = await responseMessage.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<DrinkReport[]>(response);
        }

        #endregion Drinks

        #region VendingMachines

        public async static Task<VendingMachineDomain> GetVendingMachine(Guid vendingMachineId)
        {
            String fullUrl = _host + $"VendingMachines/GetById?vendingMachineId={vendingMachineId}";

            HttpRequestMessage request = new(HttpMethod.Get, fullUrl);
            HttpResponseMessage responseMessage = await _client.SendAsync(request);
            String response = await responseMessage.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<VendingMachineDomain>(response);
        }

        public async static Task<Result> LoginInVendingMachine(Guid vendingMachineId, String password)
        {
            String fullUrl = _host + $"VendingMachines/Login?vendingMachineId={vendingMachineId}";

            HttpRequestMessage request = new(HttpMethod.Post, fullUrl);
            request.Content = JsonContent.Create(password);
            HttpResponseMessage responseMessage = await _client.SendAsync(request);
            String response = await responseMessage.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Result>(response);
        }

        #endregion VendingMachines
    }
}
