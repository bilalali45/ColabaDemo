using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace MainGateway.Middleware
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
                    context.Request.Headers.Add("CorrelationId",new StringValues(sessionId));
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
