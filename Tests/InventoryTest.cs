using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SauceLabs.NunitFramework.Pages;

namespace SauceLabs.NunitFramework.Tests
{
    [Parallelizable(ParallelScope.Self)]
    //[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [TestFixture]
    [Category("Cart")]
    public class InventoryTest : AuthenticatedBrowserTest
    {
        private InventoryPage _inventoryPage = null!;

         [SetUp]
        public async Task LogIn()
        {
            /* var loginPage = new LoginPage(Page);
            await Page.GotoAsync(config.TestSettings.baseURL);
            await loginPage.LoginAsync(data.Users.Standard, config.TestSettings.saucePassword); */
            await Page.GotoAsync("/inventory.html");
            _inventoryPage = new InventoryPage(Page);
            await Expect(Page).ToHaveURLAsync(new Regex("inventory.html"));
        } 

        [Test, Category("smoke")]
        public async Task AddItemToCart()
        {
            await _inventoryPage.ClickAddToCartForSauceLabsBackbagAsync();
            await Expect(_inventoryPage.GetShoppingCartBadge()).ToHaveTextAsync("1");
        }
        [Test, Category("smoke")]
        public async Task NavigateToCart()
        {
            await _inventoryPage.ClickAddToCartForSauceLabsBackbagAsync();
            await _inventoryPage.ClickAddToCartForSauceLabsBikeLightAsync();
            await _inventoryPage.ClickGoToCartAsync();
            await Expect(Page).ToHaveURLAsync(new Regex("cart.html"));

            var cartPage = new CartPage(Page);
            //await Expect(cartPage.GetCartItemNames()).ToHaveCountAsync(2);
            await Expect(cartPage.GetAddedItem()).ToHaveTextAsync(new string[] { "Sauce Labs Backpack", "Sauce Labs Bike Light" });
        }
        [Test]
        public async Task SortByAlphabeticalOrderDescAsync()
        {
            await _inventoryPage.SortByAsync("za");
            await Expect(_inventoryPage.GetInventoryItemName().First).ToContainTextAsync("Test.allTheThings() T-Shirt (Red)");
            IReadOnlyList<string> allItemNames = await _inventoryPage.GetInventoryItemName().AllTextContentsAsync();
            IList<string> sortedList = [.. allItemNames.OrderByDescending(name => name)];
            Assert.That(allItemNames, Is.EqualTo(sortedList), "The items are not sorted in descending order.");
        }
    }
}
