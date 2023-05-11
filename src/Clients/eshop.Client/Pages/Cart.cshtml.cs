using eshop.Client.Models;
using eshop.Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eshop.Client
{
    public class CartModel : PageModel
    {
        private readonly BasketService _basketService;

        public CartModel(BasketService basketService)
        {
            _basketService = basketService;
        }

        public Basket Cart { get; set; } = new Basket();

        public async Task<IActionResult> OnGetAsync()
        {
            var userName = "tronglt";
            Cart = await _basketService.GetBasketAsync(userName);

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
        {
            var userName = "tronglt";
            var basket = await _basketService.GetBasketAsync(userName);

            if (basket != null)
            {
                var item = basket.Items.Single(x => x.ProductId == productId);
                basket.Items.Remove(item);
                var basketUpdated = await _basketService.UpdateBasketAsync(basket);
            }

            return RedirectToPage();
        }
    }
}