using NUnit.Framework;
using WebUITests.Managers;
using WebUITests.Builders;
using WebUITests.Pages;

namespace WebUITests.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)] 
    public class SearchTests
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
        public void VerifySearchFunctionality()
        {
            string baseUrl = _config.GetConfigValue("TestSettings:EHUBaseUrl");
            string searchTerm = _config.GetConfigValue("TestSettings:SearchTerm");

            _homePage.NavigateToUrl(baseUrl);
            _homePage.ClickSearchButton();
            _homePage.EnterSearchTerm(searchTerm);

            Assert.That(WebDriverManager.Driver.Url, Does.Contain("/?s=" + searchTerm.Replace(" ", "+")));
            Assert.That(_homePage.HasSearchResults(), Is.True, "No search results were found.");
        }
    }
}
