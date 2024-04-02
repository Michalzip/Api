namespace Api.Services.StackExchangeAuthService
{
    public interface IStackExchangeAuthService
    {
        public Task<string> GetJwtToken(string code);
        public string GetLoginUrl();
    }
}
