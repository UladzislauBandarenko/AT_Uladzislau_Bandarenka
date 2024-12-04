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

        public void Open()
        {
            driver.Navigate().GoToUrl("https://en.ehu.lt/");
        }

        public void ClickAboutLink()
        {
            var aboutLink = driver.FindElement(By.XPath("//*[@id=\"menu-item-16178\"]/a"));
            aboutLink.Click();
        }

        public void Search(string query)
        {
            var searchButton = driver.FindElement(By.XPath("//*[@id=\"masthead\"]/div[1]/div/div[4]/div"));
            searchButton.Click();

            var searchBar = driver.FindElement(By.XPath("//*[@id=\"masthead\"]/div[1]/div/div[4]/div/form/div/input"));
            searchBar.SendKeys(query + Keys.Enter);
        }

        public IReadOnlyCollection<IWebElement> GetSearchResults()
        {
            return driver.FindElements(By.XPath("//*[@id=\"page\"]/div[3]"));
        }

        public void SwitchLanguageToLithuanian()
        {
            var languageSwitchButton = driver.FindElement(By.XPath("//*[@id=\"masthead\"]/div[1]/div/div[4]/ul"));
            languageSwitchButton.Click();

            var ltButton = driver.FindElement(By.XPath("//*[@id=\"masthead\"]/div[1]/div/div[4]/ul/li/ul/li[3]/a"));
            ltButton.Click();
        }
    }
}
