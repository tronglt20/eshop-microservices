using Catalog.API.Entities;
using Catalog.API.Interfaces;

namespace Catalog.API.Services
{
    public class CatalogService
    {
        private readonly IProductRepository _productRepo;

        public CatalogService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _productRepo.GetAllAsync();
        }

        public async Task CreateProductAsync(Product product)
        {
            await _productRepo.InsertAsync(product);
        }
    }
}
