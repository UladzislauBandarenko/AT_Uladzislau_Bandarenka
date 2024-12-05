using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace WebUITests.Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;


        // Locators
        private readonly By _aboutLink = By.LinkText("About");
        private readonly By _searchButton = By.CssSelector("#masthead > div.header > div > div.col-1 > div");
        private readonly By _languageSwitchButton = By.CssSelector(".language-switcher");
        private readonly By _searchBar = By.CssSelector("input.form-control[name='s']");
        private readonly By _header = By.TagName("h1");
        private readonly By _searchResults = By.CssSelector("#page > div.content");


        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void NavigateToUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void ClickAboutLink()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(_aboutLink)).Click();
        }

        public void ClickSearchButton()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(_searchButton)).Click();
        }

        public void EnterSearchTerm(string searchTerm)
        {
            var searchInput = _wait.Until(ExpectedConditions.ElementIsVisible(_searchBar));
            searchInput.Clear();
            searchInput.SendKeys(searchTerm);
            searchInput.SendKeys(Keys.Enter);
        }

        public void SwitchToLithuanianVersion(string lithuanianUrl)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(_languageSwitchButton)).Click();
            var ltButton = _wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("LT")));
            ltButton.Click();
            _wait.Until(ExpectedConditions.UrlToBe(lithuanianUrl));
        }

        public string GetPageTitle() => _driver.Title;

        public string GetHeaderText() => _driver.FindElement(_header).Text;

        public bool HasSearchResults() => _driver.FindElements(_searchResults).Count > 0;
    }
}
