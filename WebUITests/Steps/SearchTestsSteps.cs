using FluentAssertions;
using TechTalk.SpecFlow;
using WebUITests.Builders;
using WebUITests.Managers;
using WebUITests.Pages;

namespace WebUITests.StepDefinitions
{
    [Binding]
    public class SearchFunctionalityStepDefinitions
    {
        private readonly HomePage _homePage;
        private readonly PageBuilder _pageBuilder;

        public SearchFunctionalityStepDefinitions()
        {
            WebDriverManager.InitializeDriver();
            _homePage = new HomePage(WebDriverManager.Driver);
            _pageBuilder = new PageBuilder();
        }

        [Given("the user is on the homepage for navigation testing")]
        public void GivenTheUserIsOnTheHomepageForNavigationTesting()
        {
            string baseUrl = _pageBuilder.GetConfigValue("TestSettings:EHUBaseUrl");
            _homePage.NavigateToUrl(baseUrl);
        }


        [When("the user clicks on the search button")]
        public void WhenTheUserClicksOnTheSearchButton()
        {
            _homePage.ClickSearchButton();
        }

        [When("enters the search term \"(.*)\"")]
        public void WhenEntersTheSearchTerm(string searchTerm)
        {
            _homePage.EnterSearchTerm(searchTerm);
        }

        [Then("the URL should contain \"(.*)\"")]
        public void ThenTheURLShouldContain(string expectedFragment)
        {
            WebDriverManager.Driver.Url.Should().Contain(expectedFragment, $"because the URL should contain the search query '{expectedFragment}'");
        }

        [Then("search results should be displayed")]
        public void ThenSearchResultsShouldBeDisplayed()
        {
            _homePage.HasSearchResults().Should().BeTrue("because search results are expected for a valid query.");
        }

        [AfterScenario]
        public void Dispose()
        {
            WebDriverManager.QuitDriver();
        }
    }
}
