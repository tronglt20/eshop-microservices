using Microsoft.AspNetCore.Mvc;
using Ordering.API.Services;
using Ordering.Domain.Entities;

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
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder([FromQuery] string userName)
        {
            return await _orderingService.GetOrderAsync(userName);
        }
    }
}
