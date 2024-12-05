using FluentAssertions;
using NUnit.Framework;
using Serilog;
using WebUITests.Managers;
using WebUITests.Builders;
using WebUITests.Pages;

namespace WebUITests.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class LanguageTests
    {
        private ILogger _logger;
        private PageBuilder _config;
        private HomePage _homePage;

        [SetUp]
        public void Setup()
        {
            _logger = LoggerConfig.CreateLogger();
            _logger.Information("Test Setup: Initializing test environment.");

            _config = new PageBuilder();
            WebDriverManager.InitializeDriver();
            WebDriverManager.Driver.Manage().Window.Maximize();
            _homePage = new HomePage(WebDriverManager.Driver);
        }

        [TearDown]
        public void Teardown()
        {
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
                _homePage.NavigateToUrl(baseUrl);

                _logger.Debug($"Switching to Lithuanian version: {lithuanianUrl}");
                _homePage.SwitchToLithuanianVersion(lithuanianUrl);

                _logger.Debug("Validating URL...");
                WebDriverManager.Driver.Url.Should().Be(lithuanianUrl);

                _logger.Information("Test passed: VerifyLanguageChangeFunctionality");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Test failed: VerifyLanguageChangeFunctionality");
                throw;
            }
        }
    }
}
