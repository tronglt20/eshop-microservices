using Catalog.API.Entities;
using Shared.Domain.Interfaces;

namespace Catalog.API.Interfaces
{
    public interface IProductRepository : IMongoRepository<Product>
    {
    }
}
