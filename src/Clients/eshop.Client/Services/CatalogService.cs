using eshop.Client.Extensions;
using eshop.Client.Models;
using RestSharp;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace eshop.Client.Services
{
    public class CatalogService
    {
        private readonly RestClientService _client;

        public CatalogService(RestClientService client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Catalog>?> GetListCatalogsAsync()
        {
            var response = await _client.SendRequestAsync($"/api/catalog", Method.Get);
            if (response?.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonSerializer.Deserialize< IEnumerable<Catalog>>(response.Content, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
            return new List<Catalog>();
        }

        public async Task<Catalog?> GetCatalogAsync(string id)
        {
            var response = await _client.SendRequestAsync($"/api/catalog/{id}", Method.Get);
            if (response?.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonSerializer.Deserialize<Catalog>(response.Content, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
            return new Catalog();
        }

        public async Task<IEnumerable<Catalog>?> GetListCatalogsByCategory(string category)
        {
            var response = await _client.SendRequestAsync($"/api/catalog"
                    , Method.Get
                    , parameters: new Dictionary<string, string> (){ { "category", category } }
                );

            if (response?.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonSerializer.Deserialize<IEnumerable<Catalog>>(response.Content, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
            return new List<Catalog>();
        }

        public async Task<Catalog> CreateCatalogAsync(Catalog catalog)
        {
            var response = await _client.SendRequestAsync($"/api/catalog", Method.Post, body: catalog);
            if (response?.StatusCode == System.Net.HttpStatusCode.OK)
                return catalog;
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }

    }
}
