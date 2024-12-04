using Xunit;
using ProjectRoot.Builders;
using ProjectRoot.Managers;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

[Trait("Category", "Language Tests")]
public class LanguageTests : IDisposable
{
    private readonly IWebDriver driver;
    private readonly PageBuilder builder;

    public LanguageTests()
    {
        driver = WebDriverManager.GetDriver();
        builder = new PageBuilder(driver);
    }

    [Fact]
    public void VerifyLanguageSwitchFunctionality()
    {
        var homepage = builder.BuildHomePage();
        homepage.Open();
        homepage.SwitchLanguageToLithuanian();

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(d => d.Url.Equals("https://lt.ehu.lt/"));

        Assert.Equal("https://lt.ehu.lt/", driver.Url);
    }

    public void Dispose()
    {
        WebDriverManager.QuitDriver(); // Обязательно закрываем драйвер
    }
}
