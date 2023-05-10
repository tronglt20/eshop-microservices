using eshop.Client.Models;
using eshop.Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eshop.Client
{
    public class OrderModel : PageModel
    {
        private readonly OrderService _orderService;

        public OrderModel(OrderService orderService)
        {
            _orderService = orderService;
        }

        public IEnumerable<OrderResponse> Orders { get; set; } = new List<OrderResponse>();

        public async Task<IActionResult> OnGetAsync()
        {
            Orders = await _orderService.GetOrdersAsync("tronglt");

            return Page();
        }
    }
}