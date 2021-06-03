using Microsoft.AspNetCore.Http;

namespace MobileGateway.Middleware
{
    public class ExceptionMiddleware : RainsoftGateway.Core.Middleware.ExceptionMiddleware
    {
        public ExceptionMiddleware(RequestDelegate next) : base(next)
        {
        }
    }
}
