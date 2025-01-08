using FluentAssertions;
using NUnit.Framework;
using Serilog;
using AventStack.ExtentReports;
using WebUITests.Managers;
using WebUITests.Builders;
using WebUITests.Pages;
using NUnit_AT.Tests;

namespace WebUITests.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class NavigationTests
    {
        private ILogger _logger;
        private PageBuilder _config;
        private HomePage _homePage;
        private ExtentReports _extent;
        private ExtentTest _test;

        [SetUp]
        public void Setup()
        {
            _logger = LoggerConfig.CreateLogger();
            _logger.Information("Test Setup: Initializing test environment.");

            _extent = ExtentManager.GetExtent();
            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);

            _config = new PageBuilder();
            WebDriverManager.InitializeDriver();
            WebDriverManager.Driver.Manage().Window.Maximize();
            _homePage = new HomePage(WebDriverManager.Driver);

            _logger.Debug("WebDriver initialized and browser maximized.");
            _test.Log(Status.Info, "Test Setup completed successfully.");
        }

        [TearDown]
        public void Teardown()
        {
            _extent.Flush();

            _logger.Information("Test Teardown: Cleaning up test environment.");
            WebDriverManager.QuitDriver();
        }

        [Test]
        public void VerifyNavigationToAboutEHUPage()
        {
            _logger.Information("Starting test: VerifyNavigationToAboutEHUPage");

            try
            {
                string baseUrl = _config.GetConfigValue("TestSettings:EHUBaseUrl");
                string aboutUrl = _config.GetConfigValue("TestSettings:AboutPageUrl");
                string expectedTitle = "About";
                string expectedHeader = "About";

                _logger.Debug($"Navigating to base URL: {baseUrl}");
                _test.Log(Status.Info, $"Navigating to base URL: {baseUrl}");
                _homePage.NavigateToUrl(baseUrl);

                _logger.Debug("Clicking on the 'About' link.");
                _test.Log(Status.Info, "Clicking on the 'About' link.");
                _homePage.ClickAboutLink();

                _logger.Debug("Validating navigation to the 'About' page.");
                _test.Log(Status.Info, "Validating navigation to the 'About' page.");
                WebDriverManager.Driver.Url.Should().Be(aboutUrl);
                _test.Log(Status.Pass, $"URL validation passed: {aboutUrl}");

                _homePage.GetPageTitle().Should().Be(expectedTitle, "because the page title should match the expected value.");
                _test.Log(Status.Pass, $"Page title validation passed: {expectedTitle}");

                _homePage.GetHeaderText().Should().Be(expectedHeader, "because the header text should match the expected value.");
                _test.Log(Status.Pass, $"Header text validation passed: {expectedHeader}");

                _logger.Information("Test passed: VerifyNavigationToAboutEHUPage");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Test failed: VerifyNavigationToAboutEHUPage");
                _test.Log(Status.Fail, $"Test failed due to exception: {ex.Message}");
                throw;
            }
        }
    }
}
