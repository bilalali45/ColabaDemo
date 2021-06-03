using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TokenCacheHelper.TokenManager;

namespace RainsoftGateway.Core.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthenticationMiddleware> _logger;
        private readonly ITokenManager _tokenManager;


        public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger, ITokenManager tokenManager)
        {
            this._next = next;
            this._logger = logger;
            this._tokenManager = tokenManager;
        }


        public virtual async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").LastOrDefault();
            if (!string.IsNullOrEmpty(token))
            {
                var tokenInfo = await this._tokenManager.FindUserTokenAsync(token);
                if (tokenInfo == null) // If token not found
                {
                    this._logger.LogWarning("Access token does not exist in token store.");
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }
            }

            await _next(context);
        }
    }
}
