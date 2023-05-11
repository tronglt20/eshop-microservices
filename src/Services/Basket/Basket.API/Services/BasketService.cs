using Basket.API.Services.Grpc;
using Basket.Domain.Entities;
using Basket.Domain.Interfaces;
using EventBus.Messages.Events;
using MassTransit;

namespace Basket.API.Services
{
    public class BasketService
    {
        private readonly DiscountGrpcService _discountGrpcService;
        private readonly IBasketRepository _basketRepo;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketService(IBasketRepository basketRepo
            , DiscountGrpcService discountGrpcService
            , IPublishEndpoint publishEndpoint)
        {
            _basketRepo = basketRepo;
            _discountGrpcService = discountGrpcService;
            _publishEndpoint = publishEndpoint;
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
                if(coupon != null)
                    item.Price -= coupon.Amout;
            }

            return await _basketRepo.UpdateBasketAsync(basket);
        }

        public async Task DeleteBasketAsync(string userName)
        {
            await _basketRepo.DeleteBasketAsync(userName);
        }

        public async Task CheckoutAsync(BasketCheckout basketCheckout)
        {
            var basket = await _basketRepo.GetBasketAsync(basketCheckout.UserName);
            if (basket == null)
            {
                throw new Exception("Không tìm thấy Basket");
            }

            var @event = new BasketCheckoutIntergrationEvent
            {
                UserName = basket.UserName,
                TotalPrice = basket.TotalPrice,
                FirstName = basketCheckout.FirstName,
                LastName = basketCheckout.LastName,
                EmailAddress = basketCheckout.EmailAddress,
                AddressLine = basketCheckout.AddressLine,
                Country = basketCheckout.Country,
                State = basketCheckout.State,
                ZipCode = basketCheckout.ZipCode,
                CardName = basketCheckout.CardName,
                CardNumber = basketCheckout.CardNumber,
                Expiration = basketCheckout.Expiration,
                CVV = basketCheckout.CVV,
                PaymentMethod = basketCheckout.PaymentMethod,
            };

            await _publishEndpoint.Publish(@event);
            await _basketRepo.DeleteBasketAsync(basket.UserName);
        }
    }
}
