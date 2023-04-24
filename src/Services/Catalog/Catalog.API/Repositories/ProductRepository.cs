using Catalog.API.Data;
using Catalog.API.Entities;
using Catalog.API.Interfaces;
using Shared.Infrastructure;

namespace Catalog.API.Repositories
{
    public class ProductRepository : MongoRepository<Product>, IProductRepository
    {
        public ProductRepository(CatalogMongoDatabaseOptions dbContext, IServiceProvider serviceProvider) 
            : base(dbContext.Database, serviceProvider)
        {
        }
    }
}
