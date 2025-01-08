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
    public class SearchTests
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
        }

        [TearDown]
        public void Teardown()
        {
            _extent.Flush();

            _logger.Information("Test Teardown: Cleaning up test environment.");
            WebDriverManager.QuitDriver();
        }

        [Test]
        [Ignore("Skipping this test for now due to issues with search functionality.")]
        public void VerifySearchFunctionality()
        {
            _logger.Information("Starting test: VerifySearchFunctionality");

            try
            {
                string baseUrl = _config.GetConfigValue("TestSettings:EHUBaseUrl");
                string searchTerm = _config.GetConfigValue("TestSettings:SearchTerm");

                _logger.Debug($"Navigating to base URL: {baseUrl}");
                _test.Log(Status.Info, $"Navigating to base URL: {baseUrl}");
                _homePage.NavigateToUrl(baseUrl);

                _logger.Debug($"Performing search for: {searchTerm}");
                _test.Log(Status.Info, $"Performing search for: {searchTerm}");
                _homePage.ClickSearchButton();
                _homePage.EnterSearchTerm(searchTerm);

                WebDriverManager.Driver.Url.Should().Contain($"/?s={searchTerm.Replace(" ", "+")}");
                _homePage.HasSearchResults().Should().BeTrue("because search results are expected for a valid query.");

                _test.Log(Status.Pass, "Search functionality validated successfully.");
                _logger.Information("Test passed: VerifySearchFunctionality");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Test failed: VerifySearchFunctionality");
                _test.Log(Status.Fail, $"Test failed due to exception: {ex.Message}");
                throw;
            }
        }
    }
}
