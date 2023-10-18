using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace ConnectingApps.Xunit.TestLogger
{
    public class TestLoggerProvider : ILoggerProvider
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public TestLoggerProvider(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new XunitTestLogger(_testOutputHelper);
        }
    }
}
