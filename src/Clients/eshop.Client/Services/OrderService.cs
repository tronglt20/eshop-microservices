using eshop.Client.Extensions;
using eshop.Client.Models;
using RestSharp;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace eshop.Client.Services
{
    public class OrderService
    {
        private readonly RestClientService _client;

        public OrderService(RestClientService client)
        {
            _client = client;
        }

        public async Task<IEnumerable<OrderResponse>?> GetOrdersAsync(string userName)
        {
            var response = await _client.SendRequestAsync($"/api/ordering"
                , Method.Get
                , parameters: new Dictionary<string, string>
                {
                    {"userName", userName }
                });

            if (response?.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonSerializer.Deserialize<IEnumerable<OrderResponse>>(response.Content, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

            return new List<OrderResponse>();
        }
    }
}
