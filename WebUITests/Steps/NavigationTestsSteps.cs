using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using TechTalk.SpecFlow;
using WebUITests.Builders;
using WebUITests.Managers;
using WebUITests.Pages;

namespace WebUITests.StepDefinitions
{
    [Binding]
    public class NavigationStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly HomePage _homePage;
        private readonly PageBuilder _pageBuilder;

        public NavigationStepDefinitions()
        {
            WebDriverManager.InitializeDriver();
            _driver = WebDriverManager.Driver;
            _homePage = new HomePage(_driver);
            _pageBuilder = new PageBuilder();
        }

        [Given("the user is on the homepage")]
        public void GivenTheUserIsOnTheHomepage()
        {
            string baseUrl = _pageBuilder.GetConfigValue("TestSettings:EHUBaseUrl");
            _homePage.NavigateToUrl(baseUrl);
        }

        [When("the user clicks on the About link")]
        public void WhenTheUserClicksOnTheAboutLink()
        {
            _homePage.ClickAboutLink();
        }

        [Then("the URL should be \"(.*)\"")]
        public void ThenTheURLShouldBe(string expectedUrl)
        {
            _driver.Url.Should().Be(expectedUrl, $"because the user should be redirected to {expectedUrl}");
        }

        [Then("the page header should be \"(.*)\"")]
        public void ThenThePageHeaderShouldBe(string expectedHeader)
        {
            _homePage.GetHeaderText().Should().Be(expectedHeader, $"because the header text should match {expectedHeader}");
        }

        [AfterScenario]
        public void Dispose()
        {
            WebDriverManager.QuitDriver();
        }
    }
}