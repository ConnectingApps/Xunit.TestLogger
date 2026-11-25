using Microsoft.Extensions.Logging;
using System.Globalization;
using Xunit.Abstractions;

namespace ConnectingApps.Xunit.TestLogger
{
    public class XunitTestLogger : ILogger
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public XunitTestLogger(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        IDisposable ILogger.BeginScope<TState>(TState state)
        {
            return default!;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            string message = $"{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)} [{logLevel}] {formatter(state, exception)}";
            _testOutputHelper.WriteLine(message);
        }
    }
}
