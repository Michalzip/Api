using System;
namespace Api.Exceptions.Base
{
 

    public class ExceptionBaseFormat
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        public ExceptionBaseFormat(ExceptionBase exception)
        {
            Message = exception.Message;
            StatusCode = exception.StatusCode;
        }
    }
}

