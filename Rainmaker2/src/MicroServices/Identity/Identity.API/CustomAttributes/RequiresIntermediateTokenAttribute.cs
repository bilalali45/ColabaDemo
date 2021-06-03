using Identity.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.CustomAttributes
{
    public class RequiresIntermediateTokenAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var authHeader = Convert.ToString(filterContext.HttpContext.Request.Headers["IntermediateToken"]);
            if (string.IsNullOrEmpty(authHeader))
            {
                filterContext.Result = new UnauthorizedResult();
                return;
            }

            var token = Convert.ToString(authHeader);
            var serviceProvider = filterContext.HttpContext.RequestServices;
            var keyStoreService = serviceProvider.GetService<IKeyStoreService>();
            var principal = GetPrincipalFromTwoFaToken(token, keyStoreService).Result;
            if (principal == null)
            {
                filterContext.Result = new UnauthorizedResult();
            }
        }

        private async Task<ClaimsPrincipal> GetPrincipalFromTwoFaToken(string token, IKeyStoreService keyStoreService)
        {
           
            var securityKey = await keyStoreService.GetJwtSecurityKeyAsync();
            var symmetricSecurityKey = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: string.Concat("_", securityKey)));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricSecurityKey,
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        
    }
}
