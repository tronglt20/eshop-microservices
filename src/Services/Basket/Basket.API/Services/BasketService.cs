using Basket.API.Services.Grpc;
using Basket.Domain.Entities;
using Basket.Domain.Interfaces;

namespace Basket.API.Services
{
    public class BasketService
    {
        private readonly DiscountGrpcService _discountGrpcService;
        private readonly IBasketRepository _basketRepo;

        public BasketService(IBasketRepository basketRepo
            , DiscountGrpcService discountGrpcService)
        {
            _basketRepo = basketRepo;
            _discountGrpcService = discountGrpcService;
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName)
        {
            return await _basketRepo.GetBasketAsync(userName) ?? new ShoppingCart(userName);
        }

        public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart basket)
        {
            foreach (var item in basket.Items)
            {
                var coupon = await _discountGrpcService.GetDiscountAsync(item.ProductId);
                item.Price -= coupon.Amout;
            }

            return await _basketRepo.UpdateBasketAsync(basket);
        }

        public async Task DeleteBasketAsync(string userName)
        {
            await _basketRepo.DeleteBasketAsync(userName);
        }
    }
}
