using Serilog;

namespace WebUITests.Managers
{
    public static class LoggerConfig
    {
        public static ILogger CreateLogger()
        {
            return new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File( "logs/test-log-.txt",rollingInterval: RollingInterval.Day,outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .MinimumLevel.Debug()
                .CreateLogger();
        }
    }
}

