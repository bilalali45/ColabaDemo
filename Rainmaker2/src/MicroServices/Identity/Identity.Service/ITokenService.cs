using Identity.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Identity.Model;

namespace Identity.Service
{
    public interface ITokenService
    {
         Task<JwtSecurityToken> GenerateAccessToken(IEnumerable<Claim> claims);
         string GenerateRefreshToken();    
         Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
        Task<JwtSecurityToken> Generate2FaTokenAsync(IEnumerable<Claim> claims);
        //Task<string> GenerateTwoFaTokenAsync(IEnumerable<Claim> claims);
        Task<ClaimsPrincipal> GetPrincipalFrom2FaToken(string token);
        Task<ClaimsPrincipal> Validate2FaTokenAsync(string token);
    }
}