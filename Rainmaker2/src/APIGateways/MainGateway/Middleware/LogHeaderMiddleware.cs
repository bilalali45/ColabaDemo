using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace MainGateway.Middleware
{
    public class LogHeaderMiddleware : RainsoftGateway.Core.Middleware.LogHeaderMiddleware
    {
        public LogHeaderMiddleware(RequestDelegate next) : base(next)
        {
        }
    }
}
