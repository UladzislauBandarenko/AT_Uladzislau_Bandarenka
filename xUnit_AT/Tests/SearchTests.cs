using Xunit;
using ProjectRoot.Builders;
using ProjectRoot.Managers;
using OpenQA.Selenium;

[Trait("Category", "Search Tests")]
public class SearchTests : IDisposable
{
    private readonly IWebDriver driver;
    private readonly PageBuilder builder;

    public SearchTests()
    {
        driver = WebDriverManager.GetDriver();
        builder = new PageBuilder(driver);
    }

    [Fact]
    public void VerifySearchFunctionality()
    {
        var homepage = builder.BuildHomePage();
        homepage.Open();
        homepage.Search("study programs");

        Assert.Contains("/?s=study+programs", driver.Url);

        var searchResults = homepage.GetSearchResults();
        Assert.True(searchResults.Count > 0, "No search results were found.");
    }

    public void Dispose()
    {
        WebDriverManager.QuitDriver(); 
    }
}
