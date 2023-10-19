using System.Net;
using ConnectingApps.Xunit.TestLogger;
using Xunit.Abstractions;

namespace ConnectingApps.LoggingWebApi.IntegrationTest8
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
            var response = await _client.GetAsync("/weatherforecast");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public void Dispose()
        {
            _factory.Dispose();
            _client.Dispose();
        }
    }
}