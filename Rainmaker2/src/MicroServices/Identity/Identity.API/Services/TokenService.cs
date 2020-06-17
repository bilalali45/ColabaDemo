using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;

        public TokenService(IConfiguration configuration,
                            IHttpClientFactory clientFactory,
                            IKeyStoreService keyStoreService)
        {
            _configuration = configuration;
            this._clientFactory = clientFactory;
            _keyStoreService = keyStoreService;
        }
        public async Task<JwtSecurityToken> GenerateAccessToken(IEnumerable<Claim> claims)
        {
           

            //security key
            var securityKey = await _keyStoreService.GetJwtSecurityKeyAsync();
            //symmetric security key
            var symmetricSecurityKey = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: securityKey));

            //signing credentials
            var signingCredentials =
                new SigningCredentials(key: symmetricSecurityKey,
                                       algorithm: SecurityAlgorithms.HmacSha256);

            //create token
            var token = new JwtSecurityToken(
                                             issuer: "rainsoftfn",
                                             audience: "readers",
                                             expires: DateTime.Now.AddHours(value: 1),
                                             signingCredentials: signingCredentials,
                                             claims: claims
                                            );

            //return new JwtSecurityTokenHandler().WriteToken(token);
            return token;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public async Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
        {

            //security key
            var securityKey = await _keyStoreService.GetJwtSecurityKeyAsync();
            //symmetric security key
            var symmetricSecurityKey = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: securityKey));

            //signing credentials
            var signingCredentials =
                new SigningCredentials(key: symmetricSecurityKey,
                                       algorithm: SecurityAlgorithms.HmacSha256Signature);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricSecurityKey,
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }


        public static Dictionary<string, string> RefreshTokens= RefreshTokens = new Dictionary<string, string>();
        private readonly IKeyStoreService _keyStoreService;
    }
}