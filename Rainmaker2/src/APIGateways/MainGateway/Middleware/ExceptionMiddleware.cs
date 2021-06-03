using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MainGateway.Middleware
{
    public class ExceptionMiddleware : RainsoftGateway.Core.Middleware.ExceptionMiddleware
    {
        public ExceptionMiddleware(RequestDelegate next) : base(next)
        {
        }
    }
}