using eshop.Aggregator.Dtos;
using eshop.Aggregator.Models;
using RestSharp;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace eshop.Aggregator.Services
{
    public class ShoppingService
    {
        public ShoppingService()
        {

        }

        public async Task<Shopping> GetShoppingAsync(string userName)
        {
            var basket = await GetBasketAsync(userName);

            foreach (var item in basket.Items)
            {
                var product = await GetCatalogAsync(item.ProductId);

                // set additional product fields onto basket item
                item.ProductName = product.Name;
                item.Category = product.Category;
                item.Summary = product.Summary;
                item.Description = product.Description;
                item.ImageFile = product.ImageFile;
            }

            var orders = await GetOrdersAsync(userName);
            return new Shopping
            {
                UserName = userName,
                Basket = basket,
                Orders = orders
            };
        }

        private async Task<Basket?> GetBasketAsync(string userName)
        {
            var uriBuilder = new UriBuilder(ApiSettings.BasketUrl);
            uriBuilder.Path = Path.Combine(uriBuilder.Path, $"/api/basket");
            var request = new RestRequest(uriBuilder.Uri, Method.Get)
                                .AddParameter("userName", userName);

            var client = new RestClient();
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonSerializer.Deserialize<Basket>(response.Content, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
                return result;
            }

            throw new Exception("Không tìm thấy Basket");
        }

        public async Task<Catalog?> GetCatalogAsync(string id)
        {
            var uriBuilder = new UriBuilder(ApiSettings.CatalogUrl);
            uriBuilder.Path = Path.Combine(uriBuilder.Path, $"/api/catalog/{id}");
            var request = new RestRequest(uriBuilder.Uri, Method.Get);

            var client = new RestClient();
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonSerializer.Deserialize<Catalog>(response.Content, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

            throw new Exception("Không tìm thấy Catalog");
        }

        public async Task<IEnumerable<OrderResponse>?> GetOrdersAsync(string userName)
        {
            var uriBuilder = new UriBuilder(ApiSettings.OrderingUrl);
            uriBuilder.Path = Path.Combine(uriBuilder.Path, $"/api/ordering");
            var request = new RestRequest(uriBuilder.Uri, Method.Get)
                                .AddParameter("userName", userName); ;

            var client = new RestClient();
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonSerializer.Deserialize<IEnumerable<OrderResponse>>(response.Content, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

            throw new Exception("Không tìm thấy Order");
        }

    }
}
