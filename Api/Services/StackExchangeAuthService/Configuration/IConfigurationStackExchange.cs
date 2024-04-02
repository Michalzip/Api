using System;

namespace Api.Services.StackExchangeAuthService.Configuration
{
    public interface IConfigurationStackExchange
    {
        string ClientId { get; }
        string ClientSecret { get; }
    }
}
