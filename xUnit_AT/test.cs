using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;


public class LanguageTestsCollection { }

// Navigation Test Class
[Trait("Category", "Navigation Tests")]
public class NavigationTests : IDisposable
{
    private readonly IWebDriver driver;

    public NavigationTests()
    {
        driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        driver.Manage().Window.Maximize();
    }

    [Fact]
    public void VerifyNavigationToAboutPage()
    {
        driver.Navigate().GoToUrl("https://en.ehu.lt/");
        var aboutLink = driver.FindElement(By.XPath("//*[@id=\"menu-item-16178\"]/a"));
        aboutLink.Click();
        Assert.Equal("https://en.ehu.lt/about/", driver.Url);
    }

    public void Dispose()
    {
        driver.Quit();
    }
}

// Search Test Class
[Trait("Category", "Search Tests")]
public class SearchTests : IDisposable
{
    private readonly IWebDriver driver;

    public SearchTests()
    {
        driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        driver.Manage().Window.Maximize();
    }

    [Fact]
    public void VerifySearchFunctionality()
    {
        driver.Navigate().GoToUrl("https://en.ehu.lt/");
        var searchButton = driver.FindElement(By.XPath("//*[@id=\"masthead\"]/div[1]/div/div[4]/div"));
        searchButton.Click();

        var searchBar = driver.FindElement(By.XPath("//*[@id=\"masthead\"]/div[1]/div/div[4]/div/form/div/input"));
        searchBar.SendKeys("study programs" + Keys.Enter);

        Assert.Contains("/?s=study+programs", driver.Url);

        var searchResults = driver.FindElements(By.XPath("//*[@id=\"page\"]/div[3]"));
        Assert.True(searchResults.Count > 0, "No search results were found.");
    }

    public void Dispose()
    {
        driver.Quit();
    }
}

// Language Test Class
[Trait("Category", "Language Tests")]
public class LanguageTests : IDisposable
{
    private readonly IWebDriver driver;

    public LanguageTests()
    {
        driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        driver.Manage().Window.Maximize();
    }

    [Fact]
    public void VerifyLanguageSwitchFunctionality()
    {
        driver.Navigate().GoToUrl("https://en.ehu.lt/");
        var languageSwitchButton = driver.FindElement(By.XPath("//*[@id=\"masthead\"]/div[1]/div/div[4]/ul"));
        languageSwitchButton.Click();

        var ltButton = driver.FindElement(By.XPath("//*[@id=\"masthead\"]/div[1]/div/div[4]/ul/li/ul/li[3]/a"));
        ltButton.Click();

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(d => d.Url.Equals("https://lt.ehu.lt/"));

        Assert.Equal("https://lt.ehu.lt/", driver.Url);
    }

    public void Dispose()
    {
        driver.Quit();
    }
}
