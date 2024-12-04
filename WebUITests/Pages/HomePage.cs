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
        private readonly By _searchButton = By.XPath("//*[@id=\"masthead\"]/div[1]/div/div[4]/div");
        private readonly By _languageSwitchButton = By.XPath("//*[@id=\"masthead\"]/div[1]/div/div[4]/ul");
        private readonly By _searchBar = By.XPath("//*[@id=\"masthead\"]/div[1]/div/div[4]/div/form/div/input");
        private readonly By _header = By.TagName("h1");
        private readonly By _searchResults = By.XPath("//*[@id=\"page\"]/div[3]");

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
            var ltButton = _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"masthead\"]/div[1]/div/div[4]/ul/li/ul/li[3]/a")));
            ltButton.Click();
            _wait.Until(ExpectedConditions.UrlToBe(lithuanianUrl));
        }

        public string GetPageTitle() => _driver.Title;

        public string GetHeaderText() => _driver.FindElement(_header).Text;

        public bool HasSearchResults() => _driver.FindElements(_searchResults).Count > 0;
    }
}
