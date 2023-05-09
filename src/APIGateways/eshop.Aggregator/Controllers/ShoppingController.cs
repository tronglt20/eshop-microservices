using eshop.Aggregator.Models;
using eshop.Aggregator.Services;
using Microsoft.AspNetCore.Mvc;

namespace eshop.Aggregator.Controllers
{
    [Route("api/shopping")]
    public class ShoppingController : ControllerBase
    {
        private readonly ShoppingService _shoppingService;

        public ShoppingController(ShoppingService shoppingService)
        {
            _shoppingService = shoppingService;
        }

        [HttpGet]
        public async Task<ActionResult<Shopping>> GetShopping(string userName)
        {
            return await _shoppingService.GetShoppingAsync(userName);
        }
    }
}
