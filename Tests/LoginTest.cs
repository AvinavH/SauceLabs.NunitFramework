using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SauceLabs.NunitFramework.config;
using SauceLabs.NunitFramework.data;
using SauceLabs.NunitFramework.Pages;

namespace SauceLabs.NunitFramework.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    [Category("LoginTests")]
    public class LoginTest : BaseTest
    {
        private LoginPage _loginPage = null!;

        [SetUp]
        public async Task SetUp()
        {
            _loginPage = new LoginPage(Page);
            await Page.GotoAsync(config.TestSettings.baseURL);
        }

        [Test, Category("smoke")]
        public async Task StandardUser_CanLogIn()
        {
            await _loginPage.LoginAsync(Users.Standard, TestSettings.saucePassword);
            await Expect(Page).ToHaveURLAsync(new Regex("inventory.html"));
        }

        [Test, Category("negative")]
        [TestCase(Users.LockedOut, "secret_sauce", "Epic sadface: Sorry, this user has been locked out.")]
        [TestCase(Users.Standard, "wrong_password", "Epic sadface: Username and password do not match any user in this service")]
        [TestCase("", "secret_sauce", "Epic sadface: Username is required")]
        public async Task InvalidLogin_ShowsError(string user, string password, string expectedError)
        {
            await _loginPage.LoginAsync(user, password);
            await Expect(_loginPage.GetErrorMessage()).ToHaveTextAsync(expectedError);
            await Expect(Page).Not.ToHaveURLAsync(new Regex("inventory.html"));
        }


    }
}
