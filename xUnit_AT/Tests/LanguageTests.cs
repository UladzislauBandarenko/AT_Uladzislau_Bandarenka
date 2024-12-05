using Xunit;
using ProjectRoot.Builders;
using ProjectRoot.Managers;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using Serilog;
using FluentAssertions;

[Trait("Category", "Language Tests")]
public class LanguageTests : IDisposable
{
    private readonly IWebDriver driver;
    private readonly PageBuilder builder;
    private readonly ILogger log;

    public LanguageTests()
    {
        driver = WebDriverManager.GetDriver();
        builder = new PageBuilder(driver);
        log = LoggerConfig.CreateLogger();

        log.Information("Test setup complete.");
    }

    [Fact]
    public void VerifyLanguageSwitchFunctionality()
    {
        log.Information("Starting test: VerifyLanguageSwitchFunctionality");

        try
        {
            var homepage = builder.BuildHomePage();
            log.Debug("Opening homepage...");
            homepage.Open();

            log.Debug("Switching language to Lithuanian...");
            homepage.SwitchLanguageToLithuanian();

            log.Debug("Verifying that the URL matches the Lithuanian version...");
            driver.Url.Should().Be("https://lt.ehu.lt/", "because switching the language should navigate to the Lithuanian homepage");

            log.Information("Test passed: VerifyLanguageSwitchFunctionality");
        }
        catch (Exception ex)
        {
            log.Error(ex, "Test failed: VerifyLanguageSwitchFunctionality");
            throw;
        }
    }

    public void Dispose()
    {
        log.Information("Tearing down test.");
        WebDriverManager.QuitDriver();
        log.Information("Driver quit successfully.");
    }
}
