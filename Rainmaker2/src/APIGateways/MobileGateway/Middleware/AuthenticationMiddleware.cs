using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TokenCacheHelper.TokenManager;

namespace MobileGateway.Middleware
{
    public class AuthenticationMiddleware : RainsoftGateway.Core.Middleware.AuthenticationMiddleware
    {
        public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger, ITokenManager tokenManager) : base(next, logger, tokenManager)
        {
        }
    }
}
