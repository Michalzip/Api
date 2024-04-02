using System;
using Api.Services.StackExchangeAuthService;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class AuthController : Controller
    {
        private readonly IStackExchangeAuthService _stackExchangeAuthService;

        public AuthController(IStackExchangeAuthService stackExchangeAuthService)
        {
            _stackExchangeAuthService = stackExchangeAuthService;
        }

        [HttpPost("SignIn")]
        public string SignIn()
        {
            return _stackExchangeAuthService.GetLoginUrl();
        }

        [HttpGet("SignInCallback")]
        public async Task<ActionResult> SignInCallback([FromQuery] string code)
        {
            string jwtToken = await _stackExchangeAuthService.GetJwtToken(code);

            return Ok($"Authorization Bearer {jwtToken}");
        }
    }
}
