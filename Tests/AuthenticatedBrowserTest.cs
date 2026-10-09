using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace SauceLabs.NunitFramework.Tests
{
    public class AuthenticatedBrowserTest : BrowserTest
    {
        // Override the base URL with the authenticated URL & storage state file
        protected override BrowserNewContextOptions GetContextOptions()
        {
            var options = new BrowserNewContextOptions();
            if (File.Exists(GlobalSetup.StorageStatePath))
            {
                options.StorageStatePath = GlobalSetup.StorageStatePath;
            }
            options.BaseURL = config.TestSettings.baseURL; // Set the base URL for the context
            options.ViewportSize = new ViewportSize { Width = 1280, Height = 720 }; // Set the viewport size
            return options;

        }
    }
}
