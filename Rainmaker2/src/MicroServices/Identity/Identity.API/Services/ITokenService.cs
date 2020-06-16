using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Services
{
    public interface ITokenService
    {
         Task<JwtSecurityToken> GenerateAccessToken(IEnumerable<Claim> claims);         
         string GenerateRefreshToken();    
         Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);            
    }
}