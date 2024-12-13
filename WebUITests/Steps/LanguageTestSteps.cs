using FluentAssertions;
using TechTalk.SpecFlow;
using WebUITests.Pages;
using WebUITests.Managers;
using WebUITests.Builders;
using NUnit.Framework;
[assembly: Parallelizable(ParallelScope.Fixtures)]

[Binding]
public class LanguageSwitchSteps
{
    private readonly HomePage _homePage;
    private readonly string _baseUrl;
    private readonly string _lithuanianUrl;

    public LanguageSwitchSteps()
    {
        WebDriverManager.InitializeDriver();
        WebDriverManager.Driver.Manage().Window.Maximize();
        _homePage = new HomePage(WebDriverManager.Driver);
        _baseUrl = new PageBuilder().GetConfigValue("TestSettings:EHUBaseUrl");
        _lithuanianUrl = new PageBuilder().GetConfigValue("TestSettings:LithuanianVersionUrl");
    }

    [Given(@"the user navigates to the base URL")]
    public void GivenTheUserNavigatesToTheBaseUrl()
    {
        _homePage.NavigateToUrl(_baseUrl);
    }

    [When(@"the user switches to the Lithuanian version")]
    public void WhenTheUserSwitchesToTheLithuanianVersion()
    {
        _homePage.SwitchToLithuanianVersion(_lithuanianUrl);
    }

    [Then(@"the page URL should be updated to the Lithuanian version")]
    public void ThenThePageUrlShouldBeUpdatedToTheLithuanianVersion()
    {
        WebDriverManager.Driver.Url.Should().Be(_lithuanianUrl);
    }

    [Then(@"the page content should reflect the Lithuanian language")]
    public void ThenThePageContentShouldReflectTheLithuanianLanguage()
    {
        _homePage.GetHeaderText().Should().Contain("Kodėl EHU?");
    }

    [AfterScenario]
    public void Dispose()
    {
        WebDriverManager.QuitDriver();
    }
}
