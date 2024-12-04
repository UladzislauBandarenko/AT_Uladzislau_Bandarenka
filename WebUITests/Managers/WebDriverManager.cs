using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace WebUITests.Managers
{
    public static class WebDriverManager
    {
        private static ThreadLocal<IWebDriver> _driver = new ThreadLocal<IWebDriver>(() => null);


        public static IWebDriver Driver
        {
            get
            {
                if (_driver.Value == null)
                {
                    throw new InvalidOperationException("WebDriver is not initialized. Call 'InitializeDriver' before accessing the driver.");
                }
                return _driver.Value;
            }
        }

        public static void InitializeDriver()
        {
            if (_driver.Value == null)
            {
                var options = new ChromeOptions();
                options.AddArgument("--incognito");
                options.AddArgument("--disable-extensions");
                _driver.Value = new ChromeDriver(options);
            }
        }


        public static void QuitDriver()
        {
            if (_driver.Value != null)
            {
                _driver.Value.Quit();
                _driver.Value.Dispose();
                _driver.Value = null;
            }
        }
    }
}
