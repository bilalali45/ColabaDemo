using Identity.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Identity.Data;
using Identity.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TenantConfig.Common;
using URF.Core.Abstractions;

namespace Identity.Service
{
    public class TokenService : ServiceBase<IdentityContext, User>, ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration,
                            IKeyStoreService keyStoreService,
                            IUnitOfWork<IdentityContext> previousUow, IServiceProvider services, ILogger<TokenService> logger) : base(previousUow, services)
        {
            _configuration = configuration;
            _keyStoreService = keyStoreService;
            _logger = logger;
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

        public async Task<ClaimsPrincipal> GetPrincipalFrom2FaToken(string token)
        {

            //security key
            var securityKey = await _keyStoreService.GetJwtSecurityKeyAsync();
            //symmetric security key
            var symmetricSecurityKey = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: string.Concat("_", securityKey)));

            //signing credentials

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

        public async Task<JwtSecurityToken> Generate2FaTokenAsync(IEnumerable<Claim> claims)
        {


            //security key
            var securityKey = await _keyStoreService.GetJwtSecurityKeyAsync();
            //symmetric security key
            var symmetricSecurityKey = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: string.Concat("_", securityKey)));
            //var symmetricSecurityKey = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: securityKey));

            //signing credentials
            var signingCredentials =
                new SigningCredentials(key: symmetricSecurityKey,
                    algorithm: SecurityAlgorithms.HmacSha256);

            //create token
            var token = new JwtSecurityToken(
                issuer: "rainsoftfn",
                audience: "readers",
                //expires: DateTime.Now.AddMinutes(value: Convert.ToDouble(_configuration["Token:TimeoutInMinutes"])),
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: signingCredentials,
                claims: claims
            );

            return token;
        }

        //public async Task<string> GenerateTwoFaTokenAsync(IEnumerable<Claim> claims)
        //{
        //    var securityKey = await _keyStoreService.GetJwtSecurityKeyAsync();
        //    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey));

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Expires = DateTime.UtcNow.AddMinutes(30),
        //        //Issuer = myIssuer,
        //        Issuer = "rainsoftfn",
        //        //Audience = myAudience,
        //        Audience = "readers",
        //        SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature),
        //        Claims = claims.ToDictionary(c => c.Type, c => c.Value as object)
        //    };


        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}

        public async Task<ClaimsPrincipal> Validate2FaTokenAsync(string token)
        {
            var principal = await this.GetPrincipalFrom2FaToken(token);

            if (principal == null)
            {
                this._logger.LogWarning("Cannot read 2FA token.");
            }
            else
            {
                bool profileClaimExists = principal.Claims.Any(c => c.Type == "UserProfileId");
                bool contactClaimExists = principal.Claims.Any(c => c.Type == "ContactId");
                bool emailClaimExists = principal.Claims.Any(c => c.Type == "ContactId");
                if (profileClaimExists && contactClaimExists && emailClaimExists)
                {
                    return principal;
                }
            }
            return null;
        }
        private readonly IKeyStoreService _keyStoreService;
        private readonly ILogger<TokenService> _logger;
    }
}