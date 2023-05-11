using Catalog.API.Entities;
using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/catalog")]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogService _service;

        public CatalogController(CatalogService service)
        {
            _service = service;
        }

        [HttpGet()]
        public async Task<List<Product>> GetListProducts([FromQuery] string category)
        {
            return await _service.GetListProductsAsync(category);
        }

        [HttpGet("{id}")]
        public async Task<Product> GetProduct([FromRoute] string id)
        {
            return await _service.GetProductAsync(id);
        }

        [HttpPost]
        public async Task CreateProduct([FromBody] Product product)
        {
            await _service.CreateProductAsync(product);
        }
    }
}
