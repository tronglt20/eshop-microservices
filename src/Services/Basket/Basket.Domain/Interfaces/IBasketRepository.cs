using Basket.Domain.Entities;
using Shared.Domain.Interfaces;

namespace Basket.Domain.Interfaces
{
    public interface IBasketRepository : IDistributedCacheRepository
    {
        Task<ShoppingCart> GetBasketAsync(string userName);
        Task<ShoppingCart> UpdateBasketAsync(ShoppingCart basket);
        Task DeleteBasketAsync(string userName);
    }
}
