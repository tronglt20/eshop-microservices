using eshop.Client.Models;
using eshop.Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eshop.Client
{
    public class ProductDetailModel : PageModel
    {
        private readonly CatalogService _catalogService;
        private readonly BasketService _basketService;

        public ProductDetailModel(CatalogService catalogService, BasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public Catalog Product { get; set; }

        [BindProperty]
        public string Color { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGetAsync(string productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            Product = await _catalogService.GetCatalogAsync(productId);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string productId)
        {
            var product = await _catalogService.GetCatalogAsync(productId);

            var userName = "tronglt";
            var basket = await _basketService.GetBasketAsync(userName);

            basket.Items.Add(new BasketItem
            {
                ProductId = productId,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = Quantity,
                Color = Color
            });

            var basketUpdated = await _basketService.UpdateBasketAsync(basket);

            return RedirectToPage("Cart");
        }
    }
}