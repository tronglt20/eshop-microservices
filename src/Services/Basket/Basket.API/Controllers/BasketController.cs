using Basket.API.Services;
using Basket.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/basket")]
    public class BasketController : ControllerBase
    {
        private readonly BasketService _service;

        public BasketController(BasketService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetBasket([FromQuery] string userName)
        {
            return await _service.GetBasketAsync(userName);
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            return await _service.UpdateBasketAsync(basket);
        }

        [HttpDelete]
        public async Task DeleteBasket(string userName)
        {
            await _service.DeleteBasketAsync(userName);
        }

        [HttpPost("checkout")]
        public async Task Checkout([FromBody] BasketCheckout basketCheckout)
        {
            await _service.CheckoutAsync(basketCheckout);
        }
    }
}
