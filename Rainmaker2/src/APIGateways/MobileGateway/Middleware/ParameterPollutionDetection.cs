using Microsoft.AspNetCore.Http;

namespace MobileGateway.Middleware
{
    public class ParameterPollutionDetection : RainsoftGateway.Core.Middleware.ParameterPollutionDetection
    {
        public ParameterPollutionDetection(RequestDelegate next) : base(next)
        {
        }
    }
}
