using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace NUnit_AT.Tests
{
    public static class ExtentManager
    {
        private static ExtentReports? _extent;
        private static ExtentSparkReporter? _sparkReporter;

        public static ExtentReports GetExtent()
        {
            if (_extent == null)
            {
                _sparkReporter = new ExtentSparkReporter("TestReport.html");
                _extent = new ExtentReports();
                _extent.AttachReporter(_sparkReporter);
            }
            return _extent;
        }
    }
}