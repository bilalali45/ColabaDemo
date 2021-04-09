﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Notification.Model;
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

        private async Task HandleExceptionAsync(HttpContext context)
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
