using Discount.API.Services;
using Discount.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [Route("api/discount")]
    public class DiscountController : ControllerBase
    {
        private readonly DiscountService _discountService;

        public DiscountController(DiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<ActionResult<Coupon>> GetDiscount(string productId)
        {
            return await _discountService.GetDiscountAsync(productId);
        }
    }
}
