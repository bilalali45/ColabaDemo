using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TenantConfig.Common.DistributedCache;
using URF.Core.EF;
using URF.Core.EF.Factories;

namespace LoanApplication.Tests.Helpers
{
    public static class ObjectHelper
    {
        public static TenantModel GetTenantModel(int tenantId)
        {
            return new TenantModel
            {
                Id = tenantId,
                Code = "33",
                Urls = new List<UrlModel>
                {
                    new UrlModel
                    {
                        Type = TenantUrlType.Customer,
                        Url = "apply.lendova9.com:9003"
                    }
                },
                Branches = new List<BranchModel>
                {
                    new BranchModel
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };
        }

        public static T GetDbContext<T>(string dbName) where T : DbContext
        {
            var builder = new DbContextOptionsBuilder<T>();
            builder.UseInMemoryDatabase(dbName);
            var options = builder.Options;
            T dbContext = (T)Activator.CreateInstance(typeof(T), options);
            dbContext.Database.EnsureCreated();

            return dbContext;
        }

        public static UnitOfWork<T> GetInMemoryUnitOfWork<T>(string dbName) where T : DbContext
        {
            var builder = new DbContextOptionsBuilder<T>();
            builder.UseInMemoryDatabase(dbName);
            var options = builder.Options;
            T dbContext = (T)Activator.CreateInstance(typeof(T), options);
            dbContext.Database.EnsureCreated();

            return new UnitOfWork<T>(dbContext, new RepositoryProvider(new RepositoryFactories()));
        }
    }
}