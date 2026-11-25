using System.Globalization;
using ConnectingApps.Xunit.TestLogger;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace ConnectingApps.LoggingWebApi.IntegrationTest8
{
    public class TestLoggerFormatTests
    {
        private const string ExpectedWarningLinePattern =
            @"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2} \[Warning\] hello world$";

        private readonly CapturingTestOutputHelper _capture;

        public TestLoggerFormatTests(ITestOutputHelper output)
        {
            _capture = new CapturingTestOutputHelper(output);
        }

        [Fact]
        public void LoggerWritesOneFormattedLineAndIsEnabledForEveryLevel()
        {
            using var provider = new TestLoggerProvider(_capture);
            var logger = provider.CreateLogger("Cat");

            logger.LogWarning("hello {Name}", "world");

            var line = Assert.Single(_capture.Lines);
            Assert.Matches(ExpectedWarningLinePattern, line);
            Assert.All(Enum.GetValues<LogLevel>(), level => Assert.True(logger.IsEnabled(level), $"{level} should be enabled"));
        }

        [Fact]
        public void TimestampDoesNotDependOnTheCurrentCulture()
        {
            var originalCulture = CultureInfo.CurrentCulture;
            try
            {
                // fi-FI (among others) uses '.' as time separator. Cloning the invariant culture reproduces that
                // without relying on the culture data that happens to be installed on the machine.
                var culture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
                culture.DateTimeFormat.TimeSeparator = ".";
                CultureInfo.CurrentCulture = culture;

                using var provider = new TestLoggerProvider(_capture);
                provider.CreateLogger("Cat").LogWarning("hello {Name}", "world");
            }
            finally
            {
                CultureInfo.CurrentCulture = originalCulture;
            }

            var line = Assert.Single(_capture.Lines);
            Assert.Matches(ExpectedWarningLinePattern, line);
        }
    }
}
