using System;
using Api.Exceptions.Base;

namespace Api.Exceptions
{
    public class AuthorizationException : ExceptionBase
    {
        public AuthorizationException(string message)
            : base(message) { }

        public override int StatusCode => StatusCodes.Status401Unauthorized;
    }
}
