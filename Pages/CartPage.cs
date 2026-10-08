using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace SauceLabs.NunitFramework.Pages
{
    public class CartPage
    {
        protected readonly IPage _page = null!;

        public CartPage(IPage page) => _page = page;

        private ILocator AddedItem => _page.Locator(".inventory_item_name");

        public ILocator GetAddedItem() => AddedItem;
    }
}
