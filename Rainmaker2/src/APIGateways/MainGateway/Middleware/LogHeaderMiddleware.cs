using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

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
            }
            await _next(context);
        }
    }
}
