using TechTalk.SpecFlow;
using ProjectRoot.Builders;
using ProjectRoot.Managers;
using OpenQA.Selenium;
using FluentAssertions;

namespace ProjectRoot.Tests
{
    [Binding]
    public class NavigationSteps : IDisposable
    {
        private readonly IWebDriver driver;
        private readonly PageBuilder builder;

        public NavigationSteps()
        {
            driver = WebDriverManager.GetDriver();
            builder = new PageBuilder(driver);
        }

        [Given(@"the user is on the homepage")]
        public void GivenTheUserIsOnTheHomepage()
        {
            var homepage = builder.BuildHomePage();
            homepage.Open();
        }

        [When(@"the user clicks on the About link")]
        public void WhenTheUserClicksOnTheAboutLink()
        {
            var homepage = builder.BuildHomePage();
            homepage.ClickAboutLink(); // Calls the existing method to click the About link
        }

        [Then(@"the URL should be ""(.*)""")]
        public void ThenTheURLShouldBe(string expectedUrl)
        {
            driver.Url.Should().Be(expectedUrl, $"because the user clicked the 'About' link");
        }

        [Then(@"the page header should be ""(.*)""")]
        public void ThenThePageHeaderShouldBe(string expectedHeader)
        {
            var headerText = driver.FindElement(By.TagName("h1")).Text;
            headerText.Should().Be(expectedHeader, $"because the header text on the 'About' page should be '{expectedHeader}'");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Quit(); 
        }

        public void Dispose()
        {
            WebDriverManager.QuitDriver();
        }
    }
}
