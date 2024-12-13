using TechTalk.SpecFlow;
using ProjectRoot.Builders;
using ProjectRoot.Managers;
using OpenQA.Selenium;
using FluentAssertions;
using Serilog;

namespace ProjectRoot.Tests
{
    [Binding]
    public class LanguageSteps : IDisposable
    {
        private readonly IWebDriver driver;
        private readonly PageBuilder builder;
        private readonly ILogger log;

        public LanguageSteps()
        {
            driver = WebDriverManager.GetDriver();
            builder = new PageBuilder(driver);
            log = LoggerConfig.CreateLogger();
        }

        [Given(@"the user navigates to the base URL")]
        public void GivenTheUserNavigatesToTheBaseUrl()
        {
            log.Information("Opening homepage...");
            var homepage = builder.BuildHomePage();
            homepage.Open(); 
        }

        [When(@"the user switches to the Lithuanian version")]
        public void WhenTheUserSwitchesToTheLithuanianVersion()
        {
            log.Debug("Switching language to Lithuanian...");
            var homepage = builder.BuildHomePage();
            homepage.SwitchLanguageToLithuanian(); 
        }

        [Then(@"the page URL should be updated to the Lithuanian version")]
        public void ThenThePageUrlShouldBeUpdatedToTheLithuanianVersion()
        {
            log.Debug("Verifying the URL contains 'lt' for Lithuanian version...");
            driver.Url.Should().Be("https://lt.ehu.lt/", "because switching the language should navigate to the Lithuanian homepage");
        }

        [Then(@"the page content should reflect the Lithuanian language")]
        public void ThenThePageContentShouldReflectTheLithuanianLanguage()
        {
            log.Debug("Verifying that the page content reflects the Lithuanian language...");
            var pageContent = driver.PageSource;
            pageContent.Should().Contain("LT", "because the page content should include Lithuanian text after switching the language");
        }

        [AfterScenario]
        public void Dispose()
        {
            WebDriverManager.QuitDriver();
        }
    }
}
