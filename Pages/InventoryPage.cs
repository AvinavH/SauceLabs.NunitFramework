using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace SauceLabs.NunitFramework.Pages
{
    public class InventoryPage
    {
        protected readonly IPage _page;
        public InventoryPage(IPage page) => _page = page;

        private ILocator SauceLabsBackbag => _page.GetByRole(AriaRole.Link, new() { NameString = "Sauce Labs Backpack" });
        private ILocator ShoppingCartBadge => _page.Locator(".shopping_cart_badge");
        private ILocator InventoryItemName => _page.Locator(".inventory_item_name");
        private ILocator SortSelect => _page.Locator(".product_sort_container");
        private ILocator AddToCartButtonSauceLabsBacklog => _page.Locator("#add-to-cart-sauce-labs-backpack");
        private ILocator AddToCartButtonSauceLabsBikeLight => _page.Locator("#add-to-cart-sauce-labs-bike-light");
        private ILocator GoToCart => _page.Locator(".shopping_cart_link");
       
        public async Task ClickAddToCartForSauceLabsBackbagAsync()
        {
            await AddToCartButtonSauceLabsBacklog.ClickAsync();
        }
        public async Task ClickAddToCartForSauceLabsBikeLightAsync()
        {
            await AddToCartButtonSauceLabsBikeLight.ClickAsync();
        }

        public async Task SortByAsync(string value) => await SortSelect.SelectOptionAsync(value);
        public ILocator GetShoppingCartBadge() => ShoppingCartBadge;
        public async Task ClickGoToCartAsync() => await GoToCart.ClickAsync();
        public ILocator GetInventoryItemName() => InventoryItemName;
    }
}
