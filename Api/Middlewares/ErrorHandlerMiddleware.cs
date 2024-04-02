using System;
using Api.Exceptions;
using Api.Exceptions.Base;
using Newtonsoft.Json;

namespace Api.Middlewares
{
    internal class ErrorHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ExceptionBase ex)
            {
                context.Response.ContentType = "application/json";

                var exceptionResponse = new ExceptionBaseFormat(ex);

                var errorMessage = JsonConvert.SerializeObject(exceptionResponse);

                await context.Response.WriteAsync(errorMessage);
            }
            catch (Exception ex)
            {
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
