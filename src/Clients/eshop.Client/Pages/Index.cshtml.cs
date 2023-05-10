using eshop.Client.Models;
using eshop.Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eshop.Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CatalogService _catalogService;
        private readonly BasketService _basketService;

        public IndexModel(CatalogService catalogService, BasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public IEnumerable<Catalog> ProductList { get; set; } = new List<Catalog>();

        public async Task<IActionResult> OnGetAsync()
        {
            ProductList = await _catalogService.GetListCatalogsAsync();
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
                Quantity = 1,
                Color = "Black"
            });

            var basketUpdated = await _basketService.UpdateBasketAsync(basket);
            return RedirectToPage("Cart");
        }
    }
}
