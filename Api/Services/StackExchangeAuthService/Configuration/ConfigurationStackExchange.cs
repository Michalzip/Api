using System;
using Ardalis.GuardClauses;

namespace Api.Services.StackExchangeAuthService.Configuration
{
    public class ConfigurationStackExchange : IConfigurationStackExchange
    {
        private const string SectionName = "StackExchange";

        private readonly IConfigurationSection _configuration;

        public ConfigurationStackExchange(IConfiguration configuration)
        {
            _configuration = configuration.GetSection(SectionName);
        }

        public string ClientId =>
            Guard.Against.NullOrEmpty(
                _configuration.GetValue<string>(nameof(ClientId)),
                nameof(ClientId)
            );
        public string ClientSecret =>
            Guard.Against.NullOrEmpty(
                _configuration.GetValue<string>(nameof(ClientSecret)),
                nameof(ClientSecret)
            );
    }
}
