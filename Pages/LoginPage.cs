using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace SauceLabs.NunitFramework.Pages
{
    public class LoginPage
    {
        protected readonly IPage _page;

        public LoginPage(IPage page) => _page = page;

        private ILocator UsernameInput => _page.GetByPlaceholder("Username");
        private ILocator PasswordInput => _page.GetByPlaceholder("Password");
        private ILocator LoginButton => _page.GetByRole(AriaRole.Button, new() { NameString = "Login" });
        private ILocator ErrorMessage => _page.Locator("[data-test='error']");
        public async Task LoginAsync(string userName, string password)
        {
            await UsernameInput.FillAsync(userName);
            await PasswordInput.FillAsync(password);
            await LoginButton.ClickAsync();
        }

        public ILocator GetErrorMessage() => ErrorMessage;
    }
}
