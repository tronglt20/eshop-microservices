using Basket.Domain.Entities;
using Basket.Domain.Interfaces;

namespace Basket.API.Services
{
    public class BasketService
    {
        private readonly IBasketRepository _basketRepo;

        public BasketService(IBasketRepository basketRepo)
        {
            _basketRepo = basketRepo;
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName)
        {
            return await _basketRepo.GetBasketAsync(userName) ?? new ShoppingCart(userName);
        }

        public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart basket)
        {
            return await _basketRepo.UpdateBasketAsync(basket);
        }

        public async Task DeleteBasketAsync(string userName)
        {
            await _basketRepo.DeleteBasketAsync(userName);
        }
    }
}
