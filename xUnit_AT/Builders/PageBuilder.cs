using OpenQA.Selenium;
using ProjectRoot.Pages;

namespace ProjectRoot.Builders
{
    public class PageBuilder
    {
        private readonly IWebDriver driver;

        public PageBuilder(IWebDriver driver)
        {
            this.driver = driver;
        }

        public HomePage BuildHomePage() => new HomePage(driver);
    }
}
