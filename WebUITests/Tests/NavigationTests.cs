using NUnit.Framework;
using WebUITests.Managers;
using WebUITests.Builders;
using WebUITests.Pages;

namespace WebUITests.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)] 
    public class NavigationTests
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
        public void VerifyNavigationToAboutEHUPage()
        {
            string baseUrl = _config.GetConfigValue("TestSettings:EHUBaseUrl");
            string aboutUrl = _config.GetConfigValue("TestSettings:AboutPageUrl");
            string expectedTitle = "About";
            string expectedHeader = "About";

            _homePage.NavigateToUrl(baseUrl);
            _homePage.ClickAboutLink();

            Assert.That(WebDriverManager.Driver.Url, Is.EqualTo(aboutUrl));
            Assert.That(_homePage.GetPageTitle(), Is.EqualTo(expectedTitle));
            Assert.That(_homePage.GetHeaderText(), Is.EqualTo(expectedHeader));
        }
    }
}
