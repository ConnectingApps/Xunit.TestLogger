using System.Net;
using System.Text.RegularExpressions;
using ConnectingApps.Xunit.TestLogger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace ConnectingApps.LoggingWebApi.IntegrationTest8
{
    public class ImprovedLoggingTest : IDisposable
    {
        private const string ExpectedLogLinePattern =
            @"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2} \[Information\] This should be logged during testing$";

        private readonly CapturingTestOutputHelper _capture;
        private readonly TestLoggerWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;


        public ImprovedLoggingTest(ITestOutputHelper output)
        {
            _capture = new CapturingTestOutputHelper(output);
            _factory = new TestLoggerWebApplicationFactory<Program>(_capture);
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task ReadInTestOutputIfSomethingIsLogged()
        {
            var response = await _client.GetAsync("/weatherforecast");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(_capture.Lines, line => Regex.IsMatch(line, ExpectedLogLinePattern));
            Assert.Contains(_factory.Services.GetServices<ILoggerProvider>(), provider => provider is TestLoggerProvider);
        }

        public void Dispose()
        {
            _factory.Dispose();
            _client.Dispose();
        }
    }
}