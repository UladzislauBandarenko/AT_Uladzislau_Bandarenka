using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;

namespace WebUITests.Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private IWebElement AboutLink => _driver.FindElement(By.LinkText("About"));
        private IWebElement SearchButton => _driver.FindElement(By.XPath("//*[@id=\"masthead\"]/div[1]/div/div[4]/div"));
        private IWebElement SearchBar => _driver.FindElement(By.XPath("//*[@id=\"masthead\"]/div[1]/div/div[4]/div/form/div/input"));
        private IWebElement LanguageSwitchButton => _driver.FindElement(By.XPath("//*[@id=\"masthead\"]/div[1]/div/div[4]/ul"));
        private IWebElement LithuanianButton => _driver.FindElement(By.XPath("//*[@id=\"masthead\"]/div[1]/div/div[4]/ul/li/ul/li[3]/a"));

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void NavigateTo(string url) => _driver.Navigate().GoToUrl(url);

        public void ClickAbout() => AboutLink.Click();

        public void PerformSearch(string searchTerm)
        {
            SearchButton.Click();
            SearchBar.SendKeys(searchTerm);
            SearchBar.SendKeys(Keys.Enter);
        }

        public void ChangeLanguageToLithuanian()
        {
            LanguageSwitchButton.Click();
            LithuanianButton.Click();
        }

        public bool WaitForUrl(string url)
        {
            return _wait.Until(d => d.Url.Equals(url));
        }
    }
}
