using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

public class WebDriverManager
{
    private static ThreadLocal<IWebDriver> threadLocalDriver = new ThreadLocal<IWebDriver>(() =>
    {
        var options = new ChromeOptions();
        options.AddArgument("--incognito");
        options.AddArgument("--disable-extensions");
        return new ChromeDriver(options);
    });

    public static IWebDriver GetDriver()
    {
        return threadLocalDriver.Value;
    }

    public static void QuitDriver()
    {
        if (threadLocalDriver.IsValueCreated)
        {
            threadLocalDriver.Value.Quit();
            threadLocalDriver.Dispose();
        }
    }
}
