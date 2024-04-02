using System;
using Api.Exceptions.Base;

namespace Api.Exceptions
{
    public class FetchDataException : ExceptionBase
    {
        public FetchDataException(string message)
            : base(message) { }

        public override int StatusCode => StatusCodes.Status403Forbidden;
    }
}
