using CoffeeBrowser.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeBrowser.DataProviders
{
    public interface ICoffeeDataProvider
    {
        Task<IEnumerable<Coffee>> LoadCoffees();
    }

    public class CoffeeInMemoryDataProvider : ICoffeeDataProvider
    {
        public async Task<IEnumerable<Coffee>> LoadCoffees()
        {
            await Task.Delay(100);
            return new[]
            {
                new Coffee{ Name="Cappuccino", Description="Espresso with streamed milk and with milk form"},
                new Coffee{ Name="Doppio", Description="Double espresso"},
                new Coffee{ Name="Espresso",Description="Pure coffee to keep you awake!"},
                new Coffee{ Name="Latte", Description="Cappuccino with more streamed milk"}
            };
        }
    }

    public class CoffeeWebApiDataProvider : ICoffeeDataProvider
    {
        private static readonly HttpClient _client = new HttpClient();

        public async Task<IEnumerable<Coffee>> LoadCoffees()
        {
            var json = await _client.GetStringAsync(new Uri("https://thomasclaudiushuber.com/pluralsight/coffees.json"));
            return JsonConvert.DeserializeObject<IEnumerable<Coffee>>(json);
        }
    }
}
