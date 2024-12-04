using Xunit;
using ProjectRoot.Builders;
using OpenQA.Selenium;
using ProjectRoot.Managers;

[Trait("Category", "Navigation Tests")]
public class NavigationTests : IDisposable
{
    private readonly IWebDriver driver;
    private readonly PageBuilder builder;

    public NavigationTests()
    {
        driver = WebDriverManager.GetDriver();
        builder = new PageBuilder(driver);
    }

    [Fact]
    public void VerifyNavigationToAboutPage()
    {
        var homepage = builder.BuildHomePage();
        homepage.Open();
        homepage.ClickAboutLink();

        Assert.Equal("https://en.ehu.lt/about/", driver.Url);
    }

    public void Dispose()
    {
        WebDriverManager.QuitDriver();
    }
}
