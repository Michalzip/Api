using Api.Exceptions;
using Api.Services.StackExchangeAuthService.Configuration;

namespace Api.Services.StackExchangeAuthService
{
    public class StackExchangeAuthService : IStackExchangeAuthService
    {
        private readonly IConfigurationStackExchange _configurationStackExchange;
        private readonly HttpClient _httpClient;

        private readonly string _clientId;
        private readonly string _clientSecret;

        public StackExchangeAuthService(
            IConfigurationStackExchange configurationStackExchange,
            HttpClient httpClient
        )
        {
            _configurationStackExchange = configurationStackExchange;
            _httpClient = httpClient;

            _clientId = _configurationStackExchange.ClientId;
            _clientSecret = _configurationStackExchange.ClientSecret;
        }

        public string GetLoginUrl()
        {
            return $"https://stackoverflow.com/oauth?client_id={_clientId}&redirect_uri=http://localhost:7107/SignInCallback&Scope=no_expiry";
        }

        public async Task<string> GetJwtToken(string code)
        {
            var tokenEndpoint = "https://stackoverflow.com/oauth/access_token";

            var parameters = new FormUrlEncodedContent(
                new[]
                {
                    new KeyValuePair<string, string>("client_id", _clientId),
                    new KeyValuePair<string, string>("client_secret", _clientSecret),
                    new KeyValuePair<string, string>("code", code),
                    new KeyValuePair<string, string>(
                        "redirect_uri",
                        "https://localhost:7107/SignInCallback"
                    )
                }
            );

            var response = await _httpClient.PostAsync(tokenEndpoint, parameters);

            if (response == null || !response.IsSuccessStatusCode)
            {
                throw new AuthorizationException("Unable to download access token");
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }
    }
}
