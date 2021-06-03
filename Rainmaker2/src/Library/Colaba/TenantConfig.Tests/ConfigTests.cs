using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using TenantConfig.Service;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace TenantConfig.Tests
{
    public class ConfigTests
    {
        //[Fact]
        //public async Task UpdateTenantBranchCache()
        //{
        //    var serviceProvider = new Mock<IServiceProvider>();

        //    DbContextOptions<TenantConfigContext> options;
        //    var builder = new DbContextOptionsBuilder<TenantConfigContext>();
        //    builder.UseInMemoryDatabase("TenantConfig");
        //    options = builder.Options;
        //    using TenantConfigContext dataContext = new TenantConfigContext(options);

        //    dataContext.Database.EnsureCreated();
        //    TenantUrl tenantUrl = new TenantUrl()
        //    {
        //        Id = 11,
        //        Url = "apply.lendova.com:5003",
        //        TenantId = 11,
        //        BranchId = 11,
        //        TypeId = 1


        //    };
        //    dataContext.Set<TenantUrl>().Add(tenantUrl);

        //    Tenant tenant = new Tenant
        //    {
        //        Id = 11,
        //        Code = "Lendova",
        //        Name = "Lendova",
        //        Branches = new List<Branch>
        //        {
        //            new Branch
        //        {
        //        Id = 11,
        //        Code = "lendova",
        //        TenantId = 11,
        //        Name = "Lendova",
        //        IsCorporate=true,
        //        IsActive=true
        //            }
        //        }
        //    };
        //    dataContext.Set<Tenant>().Add(tenant);

        //    BranchLoanOfficerBinder branchLoanOfficerBinder = new BranchLoanOfficerBinder
        //    {
        //        Id = 11,
        //        BranchId = 11,
        //        EmployeeId = 11
        //    };
        //    dataContext.Set<BranchLoanOfficerBinder>().Add(branchLoanOfficerBinder);

        //    Employee employee = new Employee
        //    {
        //        Id = 11,
        //        Code = "44",
        //        IsActive = true,
        //        IsLoanOfficer = true
        //    };
        //    dataContext.Set<Employee>().Add(employee);

        //    dataContext.SaveChanges();

        //    serviceProvider
        //        .Setup(x => x.GetService(typeof(IUnitOfWork<TenantConfigContext>)))
        //        .Returns(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())));

        //    var redisService = new Mock<IConnectionMultiplexer>();
        //    serviceProvider
        //        .Setup(x => x.GetService(typeof(IConnectionMultiplexer)))
        //        .Returns(redisService.Object);

        //    var dbProvider = new Mock<IDatabase>();
        //    redisService.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(dbProvider.Object);

        //    var serviceScope = new Mock<IServiceScope>();
        //    serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

        //    var serviceScopeFactory = new Mock<IServiceScopeFactory>();
        //    serviceScopeFactory
        //        .Setup(x => x.CreateScope())
        //        .Returns(serviceScope.Object);

        //    serviceProvider
        //        .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
        //        .Returns(serviceScopeFactory.Object);

        //    dbProvider.Setup(x => x.KeyDeleteAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>())).Verifiable();
        //    dbProvider.Setup(x => x.HashSetAsync(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), It.IsAny<RedisValue>(), It.IsAny<When>(), It.IsAny<CommandFlags>())).Verifiable();

        //    var service = new TenantConfigService(serviceProvider.Object);
        //    await service.UpdateTenantBranchCache();
        //}

        //[Fact]
        //public async Task UpdateSettingCache()
        //{
        //    var serviceProvider = new Mock<IServiceProvider>();

        //    DbContextOptions<TenantConfigContext> options;
        //    var builder = new DbContextOptionsBuilder<TenantConfigContext>();
        //    builder.UseInMemoryDatabase("TenantConfig");
        //    options = builder.Options;
        //    using TenantConfigContext dataContext = new TenantConfigContext(options);

        //    dataContext.Database.EnsureCreated();
        //    var branch = new Branch
        //    {
        //        Id = 100,
        //        Code = "lendova",
        //        TenantId = 1,
        //        Name = "Lendova",
        //        IsCorporate = true,
        //        IsActive = true
        //    };
            
        //    dataContext.Set<Branch>().Add(branch);

        //    Setting b1 = new Setting
        //    {
        //        Id = 100,
        //        Name = "test100",
        //        Value = "test100",
        //        BranchId = 100,
        //        IsActive = true
        //    };
        //    dataContext.Set<Setting>().Add(b1);

        //    Setting b2 = new Setting
        //    {
        //        Id = 101,
        //        Name = "test101",
        //        Value = "test101",
        //        TenantId = 101,
        //        IsActive = true
        //    };
        //    dataContext.Set<Setting>().Add(b2);

        //    Setting b3 = new Setting
        //    {
        //        Id = 102,
        //        Name = "test102",
        //        Value = "test102",
        //        IsActive = true
        //    };
        //    dataContext.Set<Setting>().Add(b3);

        //    dataContext.SaveChanges();

        //    serviceProvider
        //        .Setup(x => x.GetService(typeof(IUnitOfWork<TenantConfigContext>)))
        //        .Returns(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())));

        //    var redisService = new Mock<IConnectionMultiplexer>();
        //    serviceProvider
        //        .Setup(x => x.GetService(typeof(IConnectionMultiplexer)))
        //        .Returns(redisService.Object);

        //    var dbProvider = new Mock<IDatabase>();
        //    redisService.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(dbProvider.Object);

        //    var serviceScope = new Mock<IServiceScope>();
        //    serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

        //    var serviceScopeFactory = new Mock<IServiceScopeFactory>();
        //    serviceScopeFactory
        //        .Setup(x => x.CreateScope())
        //        .Returns(serviceScope.Object);

        //    serviceProvider
        //        .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
        //        .Returns(serviceScopeFactory.Object);

        //    dbProvider.Setup(x => x.KeyDeleteAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>())).Verifiable();
        //    dbProvider.Setup(x => x.HashSetAsync(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), It.IsAny<RedisValue>(), It.IsAny<When>(), It.IsAny<CommandFlags>())).Verifiable();

        //    var service = new TenantConfigService(serviceProvider.Object);
        //    await service.UpdateSettingCache();
        //}

        //[Fact]
        //public async Task UpdateStringResourceCache()
        //{
        //    var serviceProvider = new Mock<IServiceProvider>();

        //    DbContextOptions<TenantConfigContext> options;
        //    var builder = new DbContextOptionsBuilder<TenantConfigContext>();
        //    builder.UseInMemoryDatabase("TenantConfig");
        //    options = builder.Options;
        //    using TenantConfigContext dataContext = new TenantConfigContext(options);

        //    dataContext.Database.EnsureCreated();
        //    var branch = new Branch
        //    {
        //        Id = 102,
        //        Code = "lendova",
        //        TenantId = 1,
        //        Name = "Lendova",
        //        IsCorporate = true,
        //        IsActive = true
        //    };

        //    dataContext.Set<Branch>().Add(branch);

        //    StringResource b1 = new StringResource
        //    {
        //        Id = 100,
        //        Name = "test100",
        //        Value = "test100",
        //        BranchId = 100,
        //        IsActive = true
        //    };
        //    dataContext.Set<StringResource>().Add(b1);

        //    StringResource b2 = new StringResource
        //    {
        //        Id = 101,
        //        Name = "test101",
        //        Value = "test101",
        //        TenantId = 101,
        //        IsActive = true
        //    };
        //    dataContext.Set<StringResource>().Add(b2);

        //    StringResource b3 = new StringResource
        //    {
        //        Id = 102,
        //        Name = "test102",
        //        Value = "test102",
        //        IsActive = true
        //    };
        //    dataContext.Set<StringResource>().Add(b3);

        //    dataContext.SaveChanges();

        //    serviceProvider
        //        .Setup(x => x.GetService(typeof(IUnitOfWork<TenantConfigContext>)))
        //        .Returns(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())));

        //    var redisService = new Mock<IConnectionMultiplexer>();
        //    serviceProvider
        //        .Setup(x => x.GetService(typeof(IConnectionMultiplexer)))
        //        .Returns(redisService.Object);

        //    var dbProvider = new Mock<IDatabase>();
        //    redisService.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(dbProvider.Object);

        //    var serviceScope = new Mock<IServiceScope>();
        //    serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

        //    var serviceScopeFactory = new Mock<IServiceScopeFactory>();
        //    serviceScopeFactory
        //        .Setup(x => x.CreateScope())
        //        .Returns(serviceScope.Object);

        //    serviceProvider
        //        .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
        //        .Returns(serviceScopeFactory.Object);

        //    dbProvider.Setup(x => x.KeyDeleteAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>())).Verifiable();
        //    dbProvider.Setup(x => x.HashSetAsync(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), It.IsAny<RedisValue>(), It.IsAny<When>(), It.IsAny<CommandFlags>())).Verifiable();

        //    var service = new TenantConfigService(serviceProvider.Object);
        //    await service.UpdateStringResourceCache();
        //}
    }
}
