using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CacheManager.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using TokenCacheHelper.TokenManager;

namespace MainGateway.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthenticationMiddleware> _logger;
        private readonly ITokenManager _tokenManager;


        public AuthenticationMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor, ILogger<AuthenticationMiddleware> logger, ITokenManager tokenManager)
        {
            this._next = next;
            this._httpContextAccessor = httpContextAccessor;
            this._logger = logger;
            this._tokenManager = tokenManager;
        }


        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").LastOrDefault();
            if (!string.IsNullOrEmpty(token))
            {
                var tokenInfo = await this._tokenManager.FindUserTokenAsync(token);
                if (tokenInfo == null) // If token not found
                {
                    //context.Request.Headers["Authorization"] = ""; // Remove authorization token
                    this._logger.LogWarning("Access token does not exist in token store.");
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await context.Response.WriteAsync(text: "Unauthorized",
                                                      encoding: Encoding.UTF8);
                    return;
                }
            }

            await _next(context);
        }
    }
}
