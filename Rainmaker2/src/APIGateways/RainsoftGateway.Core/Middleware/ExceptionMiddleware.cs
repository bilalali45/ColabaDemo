using Microsoft.AspNetCore.Http;
using RainsoftGateway.Core.Helpers;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace RainsoftGateway.Core.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public virtual async Task InvokeAsync(HttpContext httpContext, ILogger<ExceptionMiddleware> _logger)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong");
                await HandleExceptionAsync(httpContext);
            }
        }

        protected virtual async Task HandleExceptionAsync(HttpContext context)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorModel
                {
                    Code = context.Response.StatusCode,
                    Message = "Internal Server Error"
                }));
            }
        }
    }
}
