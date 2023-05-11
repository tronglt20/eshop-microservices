using eshop.Client.Models;
using eshop.Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eshop.Client
{
    public class CheckOutModel : PageModel
    {
        private readonly BasketService _basketService;

        public CheckOutModel(BasketService basketService)
        {
            _basketService = basketService;
        }

        [BindProperty]
        public BasketCheckout Order { get; set; }

        public Basket Cart { get; set; } = new Basket();

        public async Task<IActionResult> OnGetAsync()
        {
            var userName = "tronglt";
            Cart = await _basketService.GetBasketAsync(userName);

            return Page();
        }

        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            var userName = "tronglt";
            Cart = await _basketService.GetBasketAsync(userName);

            /*if (!ModelState.IsValid)
            {
                return Page();
            }*/

            Order.UserName = userName;
            Order.TotalPrice = Cart.TotalPrice;

            await _basketService.CheckoutBasketAsync(Order);

            return RedirectToPage("Confirmation", "OrderSubmitted");
        }
    }
}