using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using URF.Core.Abstractions;

namespace RainsoftGateway.Core.Services
{
    public class TenantResolver : ITenantResolver
    {
        private readonly IUnitOfWork<TenantConfigContext> _uow;
        private readonly IConnectionMultiplexer _connection;
        public TenantResolver(IUnitOfWork<TenantConfigContext> uow, IConnectionMultiplexer connection)
        {
            _connection = connection;
            _uow = uow;
        }
        public virtual async Task ResolveTenant(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey(Constants.COLABA_WEB_URL_HEADER))
            {
                string url = context.Request.Headers[Constants.COLABA_WEB_URL_HEADER][0].ToLower();
                Uri uri = new Uri(url);

                IDatabaseAsync database = _connection.GetDatabase();
                TenantModel tenant = null;
                RedisValue redisValue = await database.HashGetAsync(Constants.TENANTS, Constants.URL_PREFIX + uri.Authority);
                if (redisValue.IsNullOrEmpty)
                {
                    tenant = await _uow.Repository<TenantUrl>().Query(x => x.Url.ToLower() == uri.Authority)
                        .Include(x => x.Tenant).ThenInclude(x => x.Branches).ThenInclude(x => x.BranchLoanOfficerBinders).ThenInclude(x => x.Employee)
                        .Select(x => new TenantModel
                        {
                            Id = x.Tenant.Id,
                            Code = x.Tenant.Code.ToLower(),
                            Urls = new List<UrlModel> { new UrlModel { Url = x.Url.ToLower(), Type = (TenantUrlType)x.TypeId } },
                            Branches = x.Tenant.Branches.Where(y => y.IsActive == true).Select(y => new BranchModel
                            {
                                Id = y.Id,
                                Code = y.Code.ToLower(),
                                IsCorporate = y.IsCorporate,
                                LoanOfficers = y.BranchLoanOfficerBinders.Where(z => z.Employee.IsActive == true && z.Employee.IsLoanOfficer == true)
                                .Select(z => new LoanOfficerModel
                                {
                                    Id = z.Employee.Id,
                                    Code = z.Employee.Code.ToLower()
                                }).ToList()
                            }).ToList()
                        })
                        .FirstOrDefaultAsync();
                }
                else
                {
                    tenant = JsonConvert.DeserializeObject<TenantModel>(redisValue.ToString());
                }
                if (tenant == null)
                {
                    throw new Exception($"Unable to resolve tenant for {url}");
                }
                tenant.Urls = tenant.Urls.Where(x => x.Url == uri.Authority && x.Type == TenantUrlType.Customer).ToList();
                if (tenant.Urls.Count != 1)
                    throw new Exception($"Unable to find valid customer url for {url}");
                string[] urlSegments = uri.Segments;
                int index = Array.FindIndex(urlSegments, a => a.Replace("/", "") == Constants.SPA_PATH);
                if (index >= 1)
                {
                    Array.Resize(ref urlSegments, index);
                }
                if (urlSegments.Length == 1)
                {
                    tenant.Branches = tenant.Branches.Where(x => x.IsCorporate == true).ToList();
                    if (tenant.Branches.Count != 1)
                        throw new Exception($"Unable to find corporate branch for {url}");
                    tenant.Branches[0].LoanOfficers = new List<LoanOfficerModel>();
                }
                else if (urlSegments.Length == 2)
                {
                    tenant.Branches = tenant.Branches.Where(x => x.Code == urlSegments[1].Replace("/", "")).ToList();
                    if (tenant.Branches.Count != 1)
                        throw new Exception($"Unable to resolve branch for {url}");
                    tenant.Branches[0].LoanOfficers = new List<LoanOfficerModel>();
                }
                else if (urlSegments.Length == 3)
                {
                    tenant.Branches = tenant.Branches.Where(x => x.Code == urlSegments[1].Replace("/", "")).ToList();
                    if (tenant.Branches.Count != 1)
                        throw new Exception($"Unable to resolve branch for {url}");
                    tenant.Branches[0].LoanOfficers = tenant.Branches[0].LoanOfficers.Where(x => x.Code == urlSegments[2].Replace("/", "")).ToList();
                    if (tenant.Branches[0].LoanOfficers.Count != 1)
                        throw new Exception($"Unable to resolve loan officer for {url}");
                }
                else
                {
                    throw new Exception($"Invalid url {url}");
                }
                context.Request.Headers.Remove(Constants.COLABA_TENANT);
                context.Request.Headers.Add(Constants.COLABA_TENANT, new Microsoft.Extensions.Primitives.StringValues(JsonConvert.SerializeObject(tenant)));
            }
        }
    }
}
