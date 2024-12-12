using OpenQA.Selenium;
using System.Collections.Generic;

namespace ProjectRoot.Pages
{
    public class HomePage
    {
        private readonly IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }


        private readonly By SearchResults = By.CssSelector("#page > div.content");
        private readonly By LanguageSwitchButton = By.CssSelector(".language-switcher");
        private readonly By LithuanianLanguageButton = By.LinkText("LT");


        public void Open()
        {
            driver.Navigate().GoToUrl("https://en.ehu.lt/");
        }

        public void ClickAboutLink()
        {
            var aboutLink = driver.FindElement(By.LinkText("About"));
            aboutLink.Click();
        }

        public void Search(string query)
        {
            var searchButton = driver.FindElement(By.CssSelector("#masthead > div.header > div > div.col-1 > div"));
            searchButton.Click();

            var searchBar = driver.FindElement(By.CssSelector("input.form-control[name='s']"));
            searchBar.SendKeys(query + Keys.Enter);
        }
        public void ClickSearchButton()
        {
            driver.FindElement(By.CssSelector("#masthead > div.header > div > div.col-1 > div")).Click();
        }

        public IReadOnlyCollection<IWebElement> GetSearchResults()
        {
            return driver.FindElements(By.CssSelector("#page > div.content"));
        }

        public void SwitchLanguageToLithuanian()
        {
            var languageSwitchButton = driver.FindElement(By.CssSelector(".language-switcher"));
            languageSwitchButton.Click();

            var ltButton = driver.FindElement(By.LinkText("LT"));
            ltButton.Click();
        }
    }
}
