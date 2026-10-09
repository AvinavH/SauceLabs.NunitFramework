using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;
using SauceLabs.NunitFramework.Pages;

namespace SauceLabs.NunitFramework
{
    [SetUpFixture]
    public class GlobalSetup
    {
        public static string StorageStatePath { get; private set; } = null!;

        [OneTimeSetUp]
        public async Task RunBeforeAllTests()
        {
            StorageStatePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "playwright/.auth/user.json");
            var directory = Path.GetDirectoryName(StorageStatePath);
            if (!string.IsNullOrEmpty(directory)) Directory.CreateDirectory(directory);

            if (File.Exists(StorageStatePath)) return;

            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            var _loginPage = new LoginPage(page);
            TestContext.Progress.WriteLine($"Navigating to: {config.TestSettings.baseURL}");
            await page.GotoAsync(config.TestSettings.baseURL);

            await _loginPage.LoginAsync(data.Users.Standard, config.TestSettings.saucePassword);
            await page.WaitForSelectorAsync(".inventory_list");

            await context.StorageStateAsync(new() { Path = StorageStatePath });
            await context.CloseAsync();
        }
    }
}
