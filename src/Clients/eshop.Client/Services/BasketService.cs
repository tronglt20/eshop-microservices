using eshop.Client.Extensions;
using eshop.Client.Models;
using RestSharp;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace eshop.Client.Services
{
    public class BasketService
    {
        private readonly RestClientService _client;

        public BasketService(RestClientService client)
        {
            _client = client;
        }

        public async Task<Basket?> GetBasketAsync(string userName)
        {
            var response = await _client.SendRequestAsync($"/api/basket"
                , Method.Get
                , parameters: new Dictionary<string, string>
                {
                    {"userName", userName }
                });

            if (response?.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonSerializer.Deserialize<Basket>(response.Content, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

            return new Basket();
        }

        public async Task<Basket> UpdateBasketAsync(Basket model)
        {
            var response = await _client.SendRequestAsync($"/api/basket"
                , Method.Post
                , body: model);

            if (response?.StatusCode == System.Net.HttpStatusCode.OK)
                return model;
            else
                throw new Exception("Something went wrong when calling api.");
        }

        public async Task CheckoutBasketAsync(BasketCheckout model)
        {
            var response = await _client.SendRequestAsync($"/api/basket/checkout"
               , Method.Post
               , body: model);

            if (response?.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Something went wrong when calling api.");
        }
    }
}
