using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Notification.API.CorrelationHandlersAndMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext,ILogger<ExceptionMiddleware> _logger)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"Something went wrong");
                await HandleExceptionAsync(httpContext);
            }
        }

        private Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new
            {
                Code = context.Response.StatusCode.ToString(),
                Message = "Internal Server Error"
            }.ToString());
        }
    }
}
