using TechTalk.SpecFlow;
using ProjectRoot.Builders;
using ProjectRoot.Managers;
using OpenQA.Selenium;
using FluentAssertions;

namespace ProjectRoot.Tests
{
    [Binding]
    public class SearchSteps : IDisposable
    {
        private readonly IWebDriver driver;
        private readonly PageBuilder builder;

        public SearchSteps()
        {
            driver = WebDriverManager.GetDriver();
            builder = new PageBuilder(driver);
        }

        [Given(@"the user is on the homepage for navigation testing")]
        public void GivenTheUserIsOnTheHomepageForNavigationTesting()
        {
            var homepage = builder.BuildHomePage();
            homepage.Open();
        }

        [When(@"the user clicks on the search button")]
        public void WhenTheUserClicksOnTheSearchButton()
        {
            var homepage = builder.BuildHomePage();
            homepage.ClickSearchButton();
        }

        [When(@"enters the search term ""(.*)""")]
        public void WhenEntersTheSearchTerm(string searchTerm)
        {
            var homepage = builder.BuildHomePage();
            homepage.Search(searchTerm); 
        }

        [Then(@"the URL should contain ""(.*)""")]
        public void ThenTheURLShouldContain(string expectedUrlPart)
        {
            driver.Url.Should().Contain(expectedUrlPart, $"because the search query should be in the URL");
        }

        [Then(@"search results should be displayed")]
        public void ThenSearchResultsShouldBeDisplayed()
        {
            var homepage = builder.BuildHomePage();
            var searchResults = homepage.GetSearchResults();
            searchResults.Should().NotBeEmpty("because the search results should return at least one result for a valid search term");
        }

        // Dispose the WebDriver to ensure the browser is closed after the tests
        [AfterScenario]
        public void AfterScenario()
        {
            driver.Quit();  // Quit the driver to close the browser
        }

        public void Dispose()
        {
            driver?.Dispose();  // Dispose of the WebDriver when no longer needed
        }
    }
}
