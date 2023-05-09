using Microsoft.AspNetCore.Mvc;
using Ordering.API.Services;
using Ordering.API.ViewModels;

namespace Ordering.API.Controllers
{
    [Route("api/ordering")]
    public class OrderingController : ControllerBase
    {
        private readonly OrderingService _orderingService;

        public OrderingController(OrderingService orderingService)
        {
            _orderingService = orderingService;
        }


        [HttpGet]
        public async Task<ActionResult<OrderResponse>> GetOrder(string userName)
        {
            return await _orderingService.GetOrderAsync(userName);
        }
    }
}
