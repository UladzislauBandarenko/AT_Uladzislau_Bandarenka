using Xunit;
using ProjectRoot.Builders;
using ProjectRoot.Managers;
using OpenQA.Selenium;
using Serilog;
using FluentAssertions;

[Trait("Category", "Search Tests")]
public class SearchTests : IDisposable
{
    private readonly IWebDriver driver;
    private readonly PageBuilder builder;
    private readonly ILogger log;

    public SearchTests()
    {
        driver = WebDriverManager.GetDriver();
        builder = new PageBuilder(driver);
        log = LoggerConfig.CreateLogger();

        log.Information("Test setup complete.");
    }

    [Fact]
    public void VerifySearchFunctionality()
    {
        log.Information("Starting test: VerifySearchFunctionality");

        try
        {
            var homepage = builder.BuildHomePage();
            log.Debug("Opening homepage...");
            homepage.Open();

            string searchTerm = "study programs";
            log.Debug($"Searching for term: {searchTerm}");
            homepage.Search(searchTerm);

            log.Debug("Verifying the URL contains the search term...");
            driver.Url.Should().Contain("/?s=study+programs", "because the search should append the query to the URL");

            var searchResults = homepage.GetSearchResults();
            log.Information($"Found {searchResults.Count} search results.");
            searchResults.Should().NotBeEmpty("because the search results page should return at least one result for a valid search term");

            log.Information("Test passed: VerifySearchFunctionality");
        }
        catch (Exception ex)
        {
            log.Error(ex, "Test failed: VerifySearchFunctionality");
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
