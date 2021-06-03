using Microsoft.AspNetCore.Http;

namespace MobileGateway.Middleware
{
    public class LogHeaderMiddleware : RainsoftGateway.Core.Middleware.LogHeaderMiddleware
    {
        public LogHeaderMiddleware(RequestDelegate next) : base(next)
        {
        }
    }
}
