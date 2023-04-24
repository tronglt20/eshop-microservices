using Catalog.API.Entities;
using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("catalog")]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogService _service;

        public CatalogController(CatalogService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<Product>> GetProducts()
        {
            return await _service.GetProductsAsync();
        }

        [HttpPost]
        public async Task CreateProduct([FromBody] Product product)
        {
            await _service.CreateProductAsync(product);
        }
    }
}
