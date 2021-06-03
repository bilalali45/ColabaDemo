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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using TokenCacheHelper.TokenManager;

namespace MainGateway.Middleware
{
    public class AuthenticationMiddleware : RainsoftGateway.Core.Middleware.AuthenticationMiddleware
    {
        public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger, ITokenManager tokenManager):base(next,logger,tokenManager)
        {
        }
    }
}
