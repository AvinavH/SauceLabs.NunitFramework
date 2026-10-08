using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace SauceLabs.NunitFramework.Tests
{
    public abstract class BaseTest : PageTest
    {
        public override BrowserNewContextOptions ContextOptions() => new()
        {
            BaseURL = config.TestSettings.baseURL,
            ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
        };

        [SetUp]
        public async Task StartTracing()
        {
            await Context.Tracing.StartAsync(new() { Screenshots = true, Snapshots = true, Sources = true});
        }

        [TearDown]
        public async Task StopTracing()
        {
            bool testFailed = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed;
            await Context.Tracing.StopAsync(new()
            {
                Path = testFailed ? $"trace-{TestContext.CurrentContext.Test.Name}.zip" : null
            });
        }
        
    }
}
