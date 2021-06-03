using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MobileGateway.Middleware
{
    public class RequestResponseLoggingMiddleware : RainsoftGateway.Core.Middleware.RequestResponseLoggingMiddleware
    {
        public RequestResponseLoggingMiddleware(RequestDelegate next) : base(next, new string[]{ }, new string[]{ })
        {
        }
    }
}
