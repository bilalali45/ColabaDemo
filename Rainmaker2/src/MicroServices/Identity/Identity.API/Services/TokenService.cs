using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration,
                            IKeyStoreService keyStoreService)
        {
            _configuration = configuration;
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
                                             expires: DateTime.Now.AddMinutes(value: Convert.ToDouble(_configuration["Token:TimeoutInMinutes"]) ),
                                             signingCredentials: signingCredentials,
                                             claims: claims
                                            );

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


        public static readonly Dictionary<string, List<TokenPair>> RefreshTokens= RefreshTokens = new Dictionary<string, List<TokenPair>>();
        private readonly IKeyStoreService _keyStoreService;
    }

    public class TokenPair
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }

    }

}