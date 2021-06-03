using Microsoft.AspNetCore.Http;

namespace MobileGateway.Middleware
{
    public class InputFilter : RainsoftGateway.Core.Middleware.InputFilter
    {
        public InputFilter(RequestDelegate next) : base(next)
        {
        }
    }
}
