using System;
using Api.Exceptions.Base;

namespace Api.Exceptions
{
    public class InitializerException : ExceptionBase
    {
        public InitializerException(string message)
            : base(message) { }

        public override int StatusCode => StatusCodes.Status410Gone;
    }
}
