using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace ProjectRoot.Managers
{
    public static class WebDriverManager
    {
        private static readonly ThreadLocal<IWebDriver> threadLocalDriver = new(() =>
        {
            var options = new ChromeOptions();
            options.AddArgument("--incognito");
            options.AddArgument("--disable-extensions");
            var driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            return driver;
        });

        public static IWebDriver GetDriver()
        {
            return threadLocalDriver.Value;
        }

        public static void QuitDriver()
        {
            var driver = threadLocalDriver.Value;
            if (driver != null)
            {
                driver.Quit();
                threadLocalDriver.Value = null;
            }
        }
    }
}
