using System;
using Api.Exceptions.Base;

namespace Api.Exceptions
{
    public class NotFoundException : ExceptionBase
    {
        public NotFoundException(string message)
            : base(message) { }

        public override int StatusCode => StatusCodes.Status404NotFound;
    }
}
