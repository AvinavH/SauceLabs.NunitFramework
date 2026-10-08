using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceLabs.NunitFramework.config
{
    public static class TestSettings
    {
        public static string baseURL => Environment.GetEnvironmentVariable("BASE_URL") ?? "https://www.saucedemo.com";
        public static string saucePassword => Environment.GetEnvironmentVariable("SAUCE_PASSWORD") ?? "secret_sauce";
    }
}
