using eshop.Client.Models;
using eshop.Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eshop.Client
{
    public class ProductModel : PageModel
    {
        private readonly CatalogService _catalogService;
        private readonly BasketService _basketService;

        public ProductModel(CatalogService catalogService, BasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public IEnumerable<string> CategoryList { get; set; } = new List<string>();
        public IEnumerable<Catalog> ProductList { get; set; } = new List<Catalog>();


        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(string categoryName)
        {
            var productList = await _catalogService.GetListCatalogsAsync();
            CategoryList = productList.Select(p => p.Category).Distinct();

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                ProductList = productList.Where(p => p.Category == categoryName);
                SelectedCategory = categoryName;
            }
            else
            {
                ProductList = productList;
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
                Quantity = 1,
                Color = "Black"
            });

            var basketUpdated = await _basketService.UpdateBasketAsync(basket);

            return RedirectToPage("Cart");
        }
    }
}