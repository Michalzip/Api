using System;
namespace Api.Exceptions.Base
{
    public abstract class ExceptionBase : Exception
    {
        protected ExceptionBase(string message) : base(message)
        {
        }

        protected ExceptionBase(string message, Exception innerException) : base(message,
            innerException)
        {
        }

        /*
         * centrally assigned error code for tracing and localization
         */

        /*
         * status code to be return in controller
         */
        public abstract int StatusCode { get; }
    }

}
