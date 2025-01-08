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
    public class LanguageTests
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
        public void VerifyLanguageChangeFunctionality()
        {
            _logger.Information("Starting test: VerifyLanguageChangeFunctionality");

            try
            {
                string baseUrl = _config.GetConfigValue("TestSettings:EHUBaseUrl");
                string lithuanianUrl = _config.GetConfigValue("TestSettings:LithuanianVersionUrl");

                _logger.Debug($"Navigating to base URL: {baseUrl}");
                _test.Log(Status.Info, $"Navigating to base URL: {baseUrl}");
                _homePage.NavigateToUrl(baseUrl);

                _logger.Debug($"Switching to Lithuanian version: {lithuanianUrl}");
                _test.Log(Status.Info, $"Switching to Lithuanian version: {lithuanianUrl}");
                _homePage.SwitchToLithuanianVersion(lithuanianUrl);

                _logger.Debug("Validating URL...");
                _test.Log(Status.Info, "Validating URL...");

                // Intentionally fail the test by setting an incorrect URL here
                string incorrectLithuanianUrl = lithuanianUrl.Replace("lt", "en"); // Modify the URL slightly to make the test fail
                WebDriverManager.Driver.Url.Should().Be(incorrectLithuanianUrl);  // This will fail
                _test.Log(Status.Pass, "URL validation passed.");  // This line will not be reached

                _logger.Information("Test passed: VerifyLanguageChangeFunctionality");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Test failed: VerifyLanguageChangeFunctionality");
                _test.Log(Status.Fail, $"Test failed due to exception: {ex.Message}");
                throw;
            }
        }
    }
}
