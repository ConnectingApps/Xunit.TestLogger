using System.Net;
using ConnectingApps.Xunit.TestLogger;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConnectingApps.LoggingWebApi.IntegrationTest11
{
    public  class NoLoggingTests : IDisposable
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public NoLoggingTests()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task ReadInTestOutputIfNothingIsLogged()
        {
            var response = await _client.GetAsync("/weatherforecast");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var providers = _factory.Services.GetServices<ILoggerProvider>().ToList();
            Assert.NotEmpty(providers);
            Assert.DoesNotContain(providers, provider => provider is TestLoggerProvider);
        }

        public void Dispose()
        {
            _factory.Dispose();
            _client.Dispose();
        }
    }
}
