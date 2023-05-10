using Catalog.API.Entities;
using Catalog.API.Interfaces;
using MongoDB.Driver.Linq;

namespace Catalog.API.Services
{
    public class CatalogService
    {
        private readonly IProductRepository _productRepo;

        public CatalogService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<Product> GetProductAsync(string id)
        {
            return await _productRepo.GetQuery(_ => _.Id == id).SingleOrDefaultAsync();
        }

        public async Task<List<Product>> GetListProductsAsync()
        {
            return await _productRepo.GetAllAsync();
        }

        public async Task CreateProductAsync(Product product)
        {
            await _productRepo.InsertAsync(product);
        }
    }
}
