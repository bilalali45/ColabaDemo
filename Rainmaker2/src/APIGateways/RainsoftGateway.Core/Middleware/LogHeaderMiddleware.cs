using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace RainsoftGateway.Core.Middleware
{
    public class LogHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public LogHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public virtual async Task InvokeAsync(HttpContext context)
        {
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
                    header = context.Response.Headers["CorrelationId"];
                    if (header.Count > 0)
                    {
                        sessionId = header[0];
                    }
                    else
                    {
                        sessionId = Guid.NewGuid().ToString();
                    }
                    context.Request.Headers.Add("CorrelationId", new StringValues(sessionId));
                }

                context.Response.Headers.Remove("CorrelationId");
                context.Response.Headers.Add("CorrelationId", new StringValues(sessionId));
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
