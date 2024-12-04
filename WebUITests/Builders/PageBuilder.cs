using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebUITests.Builders
{
    public class PageBuilder
    {
        private readonly IConfiguration _config;

        public PageBuilder()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public string GetConfigValue(string key)
        {
            return _config[key];
        }
    }
}
