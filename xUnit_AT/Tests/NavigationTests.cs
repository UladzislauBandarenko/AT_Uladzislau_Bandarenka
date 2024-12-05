using Xunit;
using ProjectRoot.Builders;
using ProjectRoot.Managers;
using OpenQA.Selenium;
using Serilog;
using FluentAssertions;

[Trait("Category", "Navigation Tests")]
public class NavigationTests : IDisposable
{
    private readonly IWebDriver driver;
    private readonly PageBuilder builder;
    private readonly ILogger log;

    public NavigationTests()
    {
        driver = WebDriverManager.GetDriver();
        builder = new PageBuilder(driver);
        log = LoggerConfig.CreateLogger();

        log.Information("Test Setup: Initializing test environment.");
    }

    [Fact]
    public void VerifyNavigationToAboutPage()
    {
        log.Information("Starting test: VerifyNavigationToAboutPage");

        try
        {
            var homepage = builder.BuildHomePage();
            log.Debug("Navigating to base URL: https://en.ehu.lt");
            homepage.Open();

            log.Debug("Clicking on the 'About' link.");
            homepage.ClickAboutLink();

            log.Debug("Validating navigation to the 'About' page.");
            driver.Url.Should().Be("https://en.ehu.lt/about/", "because the user clicked the 'About' link");

            log.Debug("URL validation passed: https://en.ehu.lt/about/");

            var pageTitle = driver.Title;
            pageTitle.Should().Be("About", "because the 'About' page title should be 'About'");

            log.Debug("Page title validation passed: About");

            var headerText = driver.FindElement(By.TagName("h1")).Text;
            headerText.Should().Be("About", "because the header text on the 'About' page should be 'About'");

            log.Debug("Header text validation passed: About");

            log.Information("Test passed: VerifyNavigationToAboutPage");
        }
        catch (Exception ex)
        {
            log.Error(ex, "Test failed: VerifyNavigationToAboutPage");
            throw;
        }
    }

    public void Dispose()
    {
        log.Information("Test Teardown: Cleaning up test environment.");
        WebDriverManager.QuitDriver();
        log.Information("Driver quit successfully.");
    }
}
