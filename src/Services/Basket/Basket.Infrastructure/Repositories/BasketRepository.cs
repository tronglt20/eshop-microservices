using Basket.Domain.Entities;
using Basket.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Infrastructure;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository : DistributedCacheRepository, IBasketRepository
    {
        public BasketRepository(IDistributedCache distributedCache) : base(distributedCache)
        {
        }

        public async Task DeleteBasketAsync(string userName)
        {
            await DeleteAsync(userName);
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName)
        {
            var basket = await GetAsync<ShoppingCart>(userName);
            if(basket == null)
                return null;

            return basket;
        }

        public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart basket)
        {
            await UpdateAsync<ShoppingCart>(basket.UserName, basket);
            return await GetBasketAsync(basket.UserName);
        }
    }
}
