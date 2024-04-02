using Api.Exceptions;
using Api.IntegrationTest.Factory;
using Api.Services.StackExchangeAuthService;
using Api.Services.StackExchangeAuthService.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.IntegrationTest.Test
{
    public class StackExchangeAuthTest : IClassFixture<ApiFactory<Program>>
    {
        private readonly ApiFactory<Program> _factory;

        public StackExchangeAuthTest(ApiFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetToken_Should_Return_Exception()
        {
            // Arrange
            string validCode = "12341241";
            string endpoint = $"SignInCallback?code={validCode}";
            var client = _factory.CreateClient();

            var scope = _factory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
            var coniguration = scope.ServiceProvider.GetService<IConfigurationStackExchange>();
            var stackExchangeAuthService = new StackExchangeAuthService(coniguration, client);

            // Act & Assert
            await Assert.ThrowsAsync<AuthorizationException>(
                () => stackExchangeAuthService.GetJwtToken(validCode)
            );
        }
    }
}
