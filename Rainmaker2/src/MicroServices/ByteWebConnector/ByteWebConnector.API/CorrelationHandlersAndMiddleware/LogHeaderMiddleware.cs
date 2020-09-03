﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ByteWebConnector.API.CorrelationHandlersAndMiddleware
{
    public class LogHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public LogHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //context.Request.Headers.TryGetValue("CorrelationId",
            //                                    out StringValues value);

            //var dd = context.Request.Headers.ToJson();
            try
            {
                var header = context.Request.Headers["CorrelationId"];
                string sessionId;

                if (header.Count > 0)
                {
                    sessionId = header[0];
                }
                else
                {
                    sessionId = Guid.NewGuid().ToString();
                }

                context.Items["CorrelationId"] = sessionId;
            }
            catch
            {
                // this exception can be ignored as correlation id is only for logging
            }
            await _next(context);
        }
    }
}
