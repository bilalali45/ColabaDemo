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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using StackExchange.Redis;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
//using TenantConfig.Entity.Models;
using URF.Core.Abstractions;


namespace RainsoftGateway.Core.Services
{
    public interface IMobileTenantResolver
    {
        Task ResolveMobileTenant(HttpContext context);
        Task ResolveIntermediateMobileTenant(HttpContext context);
    }
    public class MobileTenantResolver : IMobileTenantResolver
    {
        private const string AUTH_HEADER = "Authorization";
        private const string INTERMEDIATE_TOKEN = "IntermediateToken";
        private const string IntermediateUserId = "IntermediateUserId";

        private readonly IUnitOfWork<TenantConfigContext> _uow;
        private readonly IConnectionMultiplexer _connection;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<MobileTenantResolver> _logger;

        public MobileTenantResolver(IUnitOfWork<TenantConfigContext> uow, IConnectionMultiplexer connection, IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<MobileTenantResolver> logger)
        {
            _uow = uow;
            _connection = connection;
            _clientFactory = clientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        

        public async Task ResolveMobileTenant(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey(AUTH_HEADER))
            {
                var token = Convert.ToString(context.Request.Headers[AUTH_HEADER]).Split(' ')[1];
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
                    var tenantInfo = await _uow.Repository<TenantConfig.Entity.Models.Tenant>()
                        .Query(x => x.IsActive && x.Code == tenantCode)
                        .Include(t => t.TenantUrls)
                        .Include(t => t.Branches)
                        .Include(t => t.Branches).ThenInclude(b => b.BranchLoanOfficerBinders)
                        .FirstOrDefaultAsync();
                    if (tenantInfo == null)
                    {
                        throw new Exception("Tenant detail not found.");
                    }

                    tenantInfo.Branches = tenantInfo.Branches.Where(b => b.IsActive).ToList();

                    var employeeInfo = await _uow.Repository<TenantConfig.Entity.Models.Employee>()
                        .Query(x => x.UserId == int.Parse(userId) && x.TenantId == tenantInfo.Id && x.IsActive)
                        .FirstOrDefaultAsync();

                    if (employeeInfo == null)
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
                            Type = (TenantUrlType)url.TypeId,
                            Url = url.Url
                        }).ToList()
                    };
                }

                if (tenant != null)
                {
                    context.Request.Headers.Remove(Constants.COLABA_TENANT);
                    context.Request.Headers.Add(Constants.COLABA_TENANT, new Microsoft.Extensions.Primitives.StringValues(JsonConvert.SerializeObject(tenant)));
                }
            }
        }

        public async Task ResolveIntermediateMobileTenant(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey(INTERMEDIATE_TOKEN))
            {
                var token = Convert.ToString(context.Request.Headers[INTERMEDIATE_TOKEN]);
                if (string.IsNullOrEmpty(token))
                {
                    throw new UnauthorizedAccessException("Authorization token not found.");
                }

                var claims = await this.GetIntermediateTokenPrincipal(token);

                IDatabaseAsync db = _connection.GetDatabase();
                TenantModel tenant = null;
                var intermediateUserId = claims.FindFirst(IntermediateUserId).Value;
                var tenantCode = claims.FindFirst("TenantCode").Value;

                var redisValue = await db.HashGetAsync(Constants.TENANTS, $"{Constants.URL_PREFIX}_UserId_{intermediateUserId}");
                if (redisValue.IsNullOrEmpty)
                {
                    var tenantInfo = await _uow.Repository<TenantConfig.Entity.Models.Tenant>()
                        .Query(x => x.IsActive && x.Code == tenantCode)
                        .Include(t => t.TenantUrls)
                        .Include(t => t.Branches)
                        .Include(t => t.Branches).ThenInclude(b => b.BranchLoanOfficerBinders)
                        .FirstOrDefaultAsync();
                    if (tenantInfo == null)
                    {
                        throw new Exception("Tenant detail not found.");
                    }

                    tenantInfo.Branches = tenantInfo.Branches.Where(b => b.IsActive).ToList();

                    var employeeInfo = await _uow.Repository<TenantConfig.Entity.Models.Employee>()
                        .Query(x => x.UserId == int.Parse(intermediateUserId) && x.TenantId == tenantInfo.Id && x.IsActive)
                        .FirstOrDefaultAsync();

                    if (employeeInfo == null)
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
                            Type = (TenantUrlType)url.TypeId,
                            Url = url.Url
                        }).ToList()
                    };
                }

                if (tenant != null)
                {
                    context.Request.Headers.Remove(Constants.COLABA_TENANT);
                    context.Request.Headers.Add(Constants.COLABA_TENANT, new Microsoft.Extensions.Primitives.StringValues(JsonConvert.SerializeObject(tenant)));

                    context.Request.Headers.Remove(IntermediateUserId);
                    context.Request.Headers.Add(IntermediateUserId, new Microsoft.Extensions.Primitives.StringValues(intermediateUserId));
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

        private async Task<ClaimsPrincipal> GetIntermediateTokenPrincipal(string token)
        {
            //security key
            var securityKey = await this.GetJwtSecurityKeyAsync();
            //symmetric security key
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
