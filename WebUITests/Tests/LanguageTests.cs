using NUnit.Framework;
using WebUITests.Managers;
using WebUITests.Builders;
using WebUITests.Pages;

namespace WebUITests.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)] 
    public class LanguageTests
    {
        private PageBuilder _config;
        private HomePage _homePage;

        [SetUp]
        public void Setup()
        {
            _config = new PageBuilder();
            WebDriverManager.InitializeDriver(); 
            WebDriverManager.Driver.Manage().Window.Maximize();
            _homePage = new HomePage(WebDriverManager.Driver);
        }

        [TearDown]
        public void Teardown()
        {
            WebDriverManager.QuitDriver(); 
        }

        [Test]
        public void VerifyLanguageChangeFunctionality()
        {
            string baseUrl = _config.GetConfigValue("TestSettings:EHUBaseUrl");
            string lithuanianUrl = _config.GetConfigValue("TestSettings:LithuanianVersionUrl");

            _homePage.NavigateToUrl(baseUrl);
            _homePage.SwitchToLithuanianVersion(lithuanianUrl);

            Assert.That(WebDriverManager.Driver.Url, Is.EqualTo(lithuanianUrl));
        }
    }
}
