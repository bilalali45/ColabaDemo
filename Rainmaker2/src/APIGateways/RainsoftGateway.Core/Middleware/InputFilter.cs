using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RainsoftGateway.Core.Middleware
{
    public class InputFilter
    {
        private readonly RequestDelegate _next;


        public InputFilter(RequestDelegate next)
        {
            _next = next;
        }


        public virtual async Task Invoke(HttpContext context)
        {
            var invalidInputDetected = false;

            #region Headers Filter

            var allowedHeaderPatterns = new List<Regex>();
            foreach (var requestHeader in context.Request.Headers)
            {
                //todo Dani get below from permanent store
                allowedHeaderPatterns.Add(item: new Regex(pattern: @".*"));

                var passed =
                    allowedHeaderPatterns.Any(predicate: regex => regex.Match(input: requestHeader.Key).Success);

                if (!passed)
                {
                    invalidInputDetected = true;
                    break;
                }
            }

            #endregion

            #region Cookies Filter

            var allowedCookiePatterns = new List<Regex>();
            foreach (var cookie in context.Request.Cookies)
            {
                //todo Dani get below from permanent store
                allowedCookiePatterns.Add(item: new Regex(pattern: @".*"));

                var passed =
                    allowedCookiePatterns.Any(predicate: regex => regex.Match(input: cookie.Key).Success);

                if (!passed)
                {
                    invalidInputDetected = true;
                    break;
                }
            }

            #endregion

            if (invalidInputDetected)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await context.Response.WriteAsync(text: "Potentially unsafe request detected",
                                                  encoding: Encoding.UTF8);
                return;
            }

            await _next.Invoke(context: context);
        }
    }
}
