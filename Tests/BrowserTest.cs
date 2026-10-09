using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;


// Incomplete Method for Storage State Implemenation
namespace SauceLabs.NunitFramework.Tests
{
    public class BrowserTest : PlaywrightTest
    {
        protected IBrowserContext _context { get; set; } = null!;
        protected IPage Page { get; set; } = null!;
        private IBrowser _browser = null!; // Added field to hold the browser instance

        // Corrected method signature to match the base class
        protected virtual BrowserNewContextOptions GetContextOptions()
        {
            return new BrowserNewContextOptions();
        }

        [SetUp]
        public async Task BaseSetupAsync()
        {
            if (_browser == null) // Ensure the browser instance is initialized
            {
                _browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = true, // Adjust options as needed
                    Channel = "msedge"
                });
            }

            var options = GetContextOptions();
            _context = await _browser.NewContextAsync(options); // Use the initialized browser instance
            Page = await _context.NewPageAsync();
        }
        [TearDown]
        public async Task BaseTearDownAsync()
        {
            if (_context != null)
            {
                await _context.CloseAsync();
            }
        }
    }
}
