using ConnectingApps.Xunit.TestLogger;
using System.Net;
using Xunit.Abstractions;

namespace ConnectingApps.LoggingWebApi.IntegrationTest
{
    public class ImprovedLoggingTest : IDisposable
    {
        private readonly TestLoggerWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;


        public ImprovedLoggingTest(ITestOutputHelper output)
        {
            _factory = new TestLoggerWebApplicationFactory<Program>(output);
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task ReadInTestOutputIfSomethingIsLogged()
        {
            var response = await _client.GetAsync("/WeatherForecast");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public void Dispose()
        {
            _factory.Dispose();
            _client.Dispose();
        }
    }
}