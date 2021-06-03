using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Identity.Data;
using Identity.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using TenantConfig.Data.Mapping;
using TenantConfig.Entity.Models;
using URF.Core.Abstractions;

namespace RainsoftGateway.Core.Services.Mobile
{
    public interface IMobileTenantResolver
    {
        Task ResolveMobileTenant(HttpContext context);
    }
    public class MobileTenantResolver : IMobileTenantResolver
    {
        private const string AUTH_HEADER = "Authorization";

        private readonly IUnitOfWork<TenantConfigContext> _uow;
        private readonly IUnitOfWork<IdentityContext> _identityUow;
        private readonly IConnectionMultiplexer _connection;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<MobileTenantResolver> _logger;

        public MobileTenantResolver(IUnitOfWork<TenantConfigContext> uow, IUnitOfWork<IdentityContext> identityUow, IConnectionMultiplexer connection, IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<MobileTenantResolver> logger)
        {
            _uow = uow;
            _identityUow = identityUow;
            _connection = connection;
            _clientFactory = clientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task ResolveMobileTenant(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey(AUTH_HEADER))
            {
                var token = Convert.ToString(context.Request.Headers[AUTH_HEADER]).Split(' ')[0];
                if (string.IsNullOrEmpty(token))
                {
                    throw new UnauthorizedAccessException("Authorization token not found.");
                }

                var claims = await this.GetTokenPrincipal(token);

                IDatabaseAsync db = _connection.GetDatabase();
                TenantModel tenant = null;
                var userId = claims.FindFirst("UserId").Value;
                var tenantCode = claims.FindFirst("TenantCode").Value;

                var redisValue = await db.HashGetAsync(Constants.TENANTS, $"{Constants.URL_PREFIX}_UserId_{userId}");
                if (redisValue.IsNullOrEmpty)
                {
                    var tenantInfo = await _uow.Repository<Tenant>()
                        .Query(x => x.IsActive && x.Code == tenantCode)
                        .Include(tenant => tenant.TenantUrls)
                        .Include(tenant => tenant.Branches.Select(b => b.IsActive == true))
                        .Include(tenant => tenant.Branches).ThenInclude(b => b.BranchLoanOfficerBinders)
                        .FirstOrDefaultAsync();
                    if(tenantInfo == null)
                    {
                        throw new Exception("Tenant detail not found.");
                    }

                    var userInfo = await _identityUow.Repository<User>()
                        .Query(x => x.Id == int.Parse(userId) && x.IsActive && x.TenantId == tenant.Id)
                        .FirstOrDefaultAsync();
                    if (userInfo == null)
                    {
                        throw new Exception("User detail not found.");
                    }

                    var employeeInfo = await _uow.Repository<Employee>()
                        .Query(x => x.UserId == int.Parse(userId) && x.TenantId == tenant.Id && x.IsActive)
                        .FirstOrDefaultAsync();

                    if(employeeInfo == null)
                    {
                        throw new Exception("MCU detail not found.");
                    }

                    tenant = new TenantModel()
                    {
                        Branches = tenantInfo.Branches.Select(b => new BranchModel()
                        {
                            Code = b.Code,
                            Id = b.Id,
                            IsCorporate = b.IsCorporate,
                            LoanOfficers = new List<LoanOfficerModel>()
                        }).ToList(),
                        Code = tenantInfo.Code,
                        Id = tenantInfo.Id,
                        Urls = tenantInfo.TenantUrls.Select(url => new UrlModel()
                        {
                            Type = (TenantUrlType) url.TypeId,
                            Url = url.Url
                        }).ToList()
                    };
                }
            }
        }

        private async Task<ClaimsPrincipal> GetTokenPrincipal(string token)
        {
            //security key
            var securityKey = await this.GetJwtSecurityKeyAsync();
            //symmetric security key
            var symmetricSecurityKey = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: securityKey));

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

        private async Task<string> GetJwtSecurityKeyAsync()
        {
            var httpClient = _clientFactory.CreateClient(name: "clientWithCorrelationId");
            var jwtKeyResponse = await httpClient.GetAsync($"{_configuration["KeyStore:Url"]}/api/keystore/keystore?key=JWT");
            jwtKeyResponse.EnsureSuccessStatusCode();
            return await jwtKeyResponse.Content.ReadAsStringAsync();
        }
    }
}
