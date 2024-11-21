using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;


namespace WebUITests
{
    public class WebTestsEhu : IDisposable
    {
        private IWebDriver driver;

        /// <summary>
        /// Constructor for setting up WebDriver and browser configuration.
        /// This acts as a replacement for NUnit's [SetUp] method.
        /// </summary>
        public WebTestsEhu()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30); // Set an implicit wait for locating elements
            driver.Manage().Window.Maximize(); // Maximize the browser window
        }

        /// <summary>
        /// Test Case 1: Verify Navigation to "About EHU" Page
        /// Steps:
        /// - Navigate to the homepage.
        /// - Click the "About EHU" link.
        /// - Verify the URL of the redirected page.
        /// </summary>
        [Fact]
        public void VerifyNavigationToAboutPage()
        {
            // Step 1: Navigate to the EHU homepage
            driver.Navigate().GoToUrl("https://en.ehu.lt/");

            // Step 2: Locate and click the "About EHU" link
            var aboutLink = driver.FindElement(By.XPath("//*[@id=\"menu-item-16178\"]/a"));
            aboutLink.Click();

            // Step 3: Verify the URL of the redirected page
            Assert.Equal("https://en.ehu.lt/about/", driver.Url);
        }

        /// <summary>
        /// Test Case 2: Verify Search Functionality
        /// Steps:
        /// - Navigate to the homepage.
        /// - Open the search bar, enter a query, and submit.
        /// - Verify the URL contains the search term.
        /// - Verify search results are displayed.
        /// </summary>
        [Fact]
        public void VerifySearchFunctionality()
        {
            // Step 1: Navigate to the homepage
            driver.Navigate().GoToUrl("https://en.ehu.lt/");

            // Step 2: Locate and click the search button to open the search bar
            var searchButton = driver.FindElement(By.XPath("//*[@id=\"masthead\"]/div[1]/div/div[4]/div"));
            searchButton.Click();

            // Step 3: Locate the search bar and input the search term
            var searchBar = driver.FindElement(By.XPath("//*[@id=\"masthead\"]/div[1]/div/div[4]/div/form/div/input"));
            searchBar.SendKeys("study programs");

            // Step 4: Submit the search query
            searchBar.SendKeys(Keys.Enter);

            // Step 5: Verify the URL contains the search term
            Assert.Contains("/?s=study+programs", driver.Url);

            // Step 6: Verify search results are displayed
            var searchResults = driver.FindElements(By.XPath("//*[@id=\"page\"]/div[3]")); // Replace with actual selector for search results
            Assert.True(searchResults.Count > 0, "No search results were found.");

            // Step 7 (Optional): Check if search results contain relevant content
            bool resultsContainSearchTerm = searchResults.Any(result => result.Text.Contains("study program", StringComparison.OrdinalIgnoreCase));
            Assert.True(resultsContainSearchTerm, "Search results do not contain expected term 'study programs'.");
        }

        /// <summary>
        /// Test Case 3: Verify Language Switch Functionality
        /// Steps:
        /// - Navigate to the English homepage.
        /// - Open the language switcher and select Lithuanian (Lietuvių).
        /// - Verify the URL changes to the Lithuanian site.
        /// </summary>
        [Fact]
        public void VerifyLanguageSwitchFunctionality()
        {
            // Step 1: Navigate to the English version of the site
            driver.Navigate().GoToUrl("https://en.ehu.lt/");

            // Step 2: Locate and click the language switcher to open the menu
            var languageSwitchButton = driver.FindElement(By.XPath("//*[@id=\"masthead\"]/div[1]/div/div[4]/ul"));
            languageSwitchButton.Click();

            // Step 3: Locate and click the Lithuanian language option (Lietuvių)
            var ltButton = driver.FindElement(By.XPath("//*[@id=\"masthead\"]/div[1]/div/div[4]/ul/li/ul/li[3]/a"));
            ltButton.Click();

            // Step 4: Wait for the Lithuanian version to load and verify the URL
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Equals("https://lt.ehu.lt/"));

            // Step 5: Assert the URL has changed to the Lithuanian version
            Assert.Equal("https://lt.ehu.lt/", driver.Url);
        }

        /// <summary>
        /// Clean up the WebDriver after each test.
        /// This acts as a replacement for NUnit's [TearDown] method.
        /// </summary>
        public void Dispose()
        {
            if (driver != null)
            {
                driver.Quit(); // Close the browser and dispose of the WebDriver
            }
        }
    }
}
