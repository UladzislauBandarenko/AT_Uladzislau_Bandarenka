using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebUITests.Builders
{
    public class TestDataBuilder
    {
        private readonly IConfiguration _configuration;

        public TestDataBuilder()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public string GetBaseUrl() => _configuration["TestSettings:EHUBaseUrl"];
        public string GetAboutPageUrl() => _configuration["TestSettings:AboutPageUrl"];
        public string GetSearchTerm() => _configuration["TestSettings:SearchTerm"];
        public string GetLithuanianUrl() => _configuration["TestSettings:LithuanianVersionUrl"];
    }
}
