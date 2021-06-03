using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using MainGateway.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace MainGateway.Tests
{
    public class ServerTests
    {
        [Fact]
        public async Task TestTenantResolverService()
        {
            var httpContext = new DefaultHttpContext();
            var connectionMultiplexer = new Mock<IConnectionMultiplexer>();
            Mock<IDatabase> database = new Mock<IDatabase>();
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfig");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);

            dataContext.Database.EnsureCreated();
            TenantUrl tenantUrl = new TenantUrl()
            {
                Id = 1,
                Url = "apply.lendova.com:5003",
                TenantId = 1,
                BranchId = 1,
                TypeId = 1


            };
            dataContext.Set<TenantUrl>().Add(tenantUrl);

            Tenant tenant = new Tenant
            {
                Id = 1,
                Code = "Lendova",
                Name = "Lendova",
                Branches = new List<Branch>
                {
                    new Branch
                {
                Id = 1,
                Code = "lendova",
                TenantId = 1,
                Name = "Lendova",
                IsCorporate=true,
                IsActive=true
                    }
                }
            };
            dataContext.Set<Tenant>().Add(tenant);

            BranchLoanOfficerBinder branchLoanOfficerBinder = new BranchLoanOfficerBinder
            {
                Id = 1,
                BranchId = 1,
                EmployeeId = 1
            };
            dataContext.Set<BranchLoanOfficerBinder>().Add(branchLoanOfficerBinder);

            Employee employee = new Employee
            {
                Id = 1,
                Code = "44",
                IsActive = true,
                IsLoanOfficer = true
            };
            dataContext.Set<Employee>().Add(employee);

            dataContext.SaveChanges();

            httpContext.Request.Headers.Add(Constants.COLABA_WEB_URL_HEADER, "https://apply.lendova.com:5003");

            connectionMultiplexer.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(database.Object);
            RainsoftGateway.Core.Services.ITenantResolver resolver = new TenantResolver(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), connectionMultiplexer.Object);
            await resolver.ResolveTenant(httpContext);

        }
        [Fact]
        public async Task TestTenantResolverServiceTenantIsNull()
        {
            var httpContext = new DefaultHttpContext();
            var connectionMultiplexer = new Mock<IConnectionMultiplexer>();
            Mock<IDatabase> database = new Mock<IDatabase>();
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfig");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);

            dataContext.Database.EnsureCreated();
  
            dataContext.SaveChanges();

            httpContext.Request.Headers.Add(Constants.COLABA_WEB_URL_HEADER, "https://apply.lendova.com:5003");

            connectionMultiplexer.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(database.Object);
            RainsoftGateway.Core.Services.ITenantResolver resolver = new TenantResolver(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), connectionMultiplexer.Object);

            await Assert.ThrowsAsync<Exception>(() => resolver.ResolveTenant(httpContext));
        }
        
        [Fact]
        public async Task TestTenantResolverServiceTenantUrlIsZero()
        {
            var httpContext = new DefaultHttpContext();
            var connectionMultiplexer = new Mock<IConnectionMultiplexer>();
            Mock<IDatabase> database = new Mock<IDatabase>();
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfig");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);

            dataContext.Database.EnsureCreated();
            TenantUrl tenantUrl = new TenantUrl()
            {
                Id = 2,
                Url = "apply.lendova1.com:5003",
                TenantId = 0,
                BranchId = 2,
                TypeId = 2,
                
                
                

            };
            dataContext.Set<TenantUrl>().Add(tenantUrl);

            Tenant tenant = new Tenant
            {
                Id = 10,
                Code = "Lendova",
                Name = "Lendova",
                Branches = new List<Branch>
                {
                    new Branch
                {
                Id = 2,
                Code = "lendova",
                TenantId = 2,
                Name = "Lendova",
                IsCorporate=true,
                IsActive=true
                    }
                }
            };
            dataContext.Set<Tenant>().Add(tenant);

            BranchLoanOfficerBinder branchLoanOfficerBinder = new BranchLoanOfficerBinder
            {
                Id = 2,
                BranchId = 2,
                EmployeeId = 2
            };
            dataContext.Set<BranchLoanOfficerBinder>().Add(branchLoanOfficerBinder);

            Employee employee = new Employee
            {
                Id = 2,
                Code = "44",
                IsActive = true,
                IsLoanOfficer = true
            };
            dataContext.Set<Employee>().Add(employee);

            dataContext.SaveChanges();

            httpContext.Request.Headers.Add(Constants.COLABA_WEB_URL_HEADER, "https://apply.lendova1.com:5003");

            connectionMultiplexer.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(database.Object);
            RainsoftGateway.Core.Services.ITenantResolver resolver = new TenantResolver(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), connectionMultiplexer.Object);

            await Assert.ThrowsAsync<Exception>(() => resolver.ResolveTenant(httpContext));
        }
        [Fact]
        public async Task TestTenantResolverServiceTenantBranchIsZero()
        {
            var httpContext = new DefaultHttpContext();
            var connectionMultiplexer = new Mock<IConnectionMultiplexer>();
            Mock<IDatabase> database = new Mock<IDatabase>();
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfig");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);

            dataContext.Database.EnsureCreated();
            TenantUrl tenantUrl = new TenantUrl()
            {
                Id = 4,
                Url = "apply.lendova2.com:5003",
                TenantId = 4,
                BranchId = 4,
                TypeId =1


            };
            dataContext.Set<TenantUrl>().Add(tenantUrl);

            Tenant tenant = new Tenant
            {
                Id = 4,
                Code = "Lendova",
                Name = "Lendova",
                Branches = new List<Branch>
                {
                    new Branch
                {
                Id = 4,
                Code = "lendova",
                TenantId =4,
                Name = "Lendova",
                IsCorporate=false,
                IsActive=true
                    }
                }
            };
            dataContext.Set<Tenant>().Add(tenant);

            BranchLoanOfficerBinder branchLoanOfficerBinder = new BranchLoanOfficerBinder
            {
                Id = 5,
                BranchId = 4,
                EmployeeId = 4
            };
            dataContext.Set<BranchLoanOfficerBinder>().Add(branchLoanOfficerBinder);

            Employee employee = new Employee
            {
                Id = 4,
                Code = "44",
                IsActive = true,
                IsLoanOfficer = true
            };
            dataContext.Set<Employee>().Add(employee);

            dataContext.SaveChanges();

            httpContext.Request.Headers.Add(Constants.COLABA_WEB_URL_HEADER, "https://apply.lendova2.com:5003");

            connectionMultiplexer.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(database.Object);
            RainsoftGateway.Core.Services.ITenantResolver resolver = new TenantResolver(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), connectionMultiplexer.Object);

            await Assert.ThrowsAsync<Exception>(() => resolver.ResolveTenant(httpContext));
        }
       
        [Fact]
        public async Task TestTenantResolverServiceUrlSegmentIsTwoAndBranchExists()
        {
            var httpContext = new DefaultHttpContext();
            var connectionMultiplexer = new Mock<IConnectionMultiplexer>();
            Mock<IDatabase> database = new Mock<IDatabase>();
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfig");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);

            dataContext.Database.EnsureCreated();

            TenantModel model = new TenantModel();
            model.Id = 7;
            model.Code = "33";
            model.Urls = new List<UrlModel>
            {  new UrlModel
            {
                Type=TenantUrlType.Customer,
                Url="apply.lendova3.com:5003"
            }
            };
            model.Branches = new List<BranchModel>

            {
                new BranchModel
                {
                    Code="texas",
                    Id=7,
                    IsCorporate=true,
                    LoanOfficers= new List<LoanOfficerModel>
                    {
                         new LoanOfficerModel
                         {
                             Id=7,
                             Code="33"
                         }
                    }

                }
            };

            database.Setup(x => x.HashGetAsync(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), It.IsAny<CommandFlags>())).ReturnsAsync(RedisValue.Unbox(Newtonsoft.Json.JsonConvert.SerializeObject(model)));


            httpContext.Request.Headers.Add(Constants.COLABA_WEB_URL_HEADER, "https://apply.lendova3.com:5003/texas");

            connectionMultiplexer.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(database.Object);
            RainsoftGateway.Core.Services.ITenantResolver resolver = new TenantResolver(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), connectionMultiplexer.Object);
            await resolver.ResolveTenant(httpContext);
        }
        [Fact]
        public async Task TestTenantResolverServiceUrlSegmentIsTwoAndBranchDoesnotExists()
        {
            var httpContext = new DefaultHttpContext();
            var connectionMultiplexer = new Mock<IConnectionMultiplexer>();
            Mock<IDatabase> database = new Mock<IDatabase>();
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfig");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);

            dataContext.Database.EnsureCreated();
            TenantUrl tenantUrl = new TenantUrl()
            {
                Id = 6,
                Url = "apply.lendova6.com:6003",
                TenantId = 6,
                BranchId = 6,
                TypeId = 1


            };
            dataContext.Set<TenantUrl>().Add(tenantUrl);

            Tenant tenant = new Tenant
            {
                Id = 6,
                Code = "texasa",
                Name = "texasa",
                Branches = new List<Branch>
                {
                    new Branch
                {
                Id =6,
                Code = "texasa",
                TenantId = 6,
                Name = "texasa",
                IsCorporate=false,
                IsActive=true
                    }
                }
            };
            dataContext.Set<Tenant>().Add(tenant);

            BranchLoanOfficerBinder branchLoanOfficerBinder = new BranchLoanOfficerBinder
            {
                Id = 6,
                BranchId = 6,
                EmployeeId = 6
            };
            dataContext.Set<BranchLoanOfficerBinder>().Add(branchLoanOfficerBinder);

            Employee employee = new Employee
            {
                Id = 6,
                Code = "44",
                IsActive = true,
                IsLoanOfficer = true
            };
            dataContext.Set<Employee>().Add(employee);

            dataContext.SaveChanges();

            httpContext.Request.Headers.Add(Constants.COLABA_WEB_URL_HEADER, "https://apply.lendova6.com:6003/texas");

            connectionMultiplexer.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(database.Object);
            RainsoftGateway.Core.Services.ITenantResolver resolver = new TenantResolver(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), connectionMultiplexer.Object);
            await Assert.ThrowsAsync<Exception>(() => resolver.ResolveTenant(httpContext));

        }
        [Fact]
        public async Task TestTenantResolverServiceRedisValueIsNull()
        {
            var httpContext = new DefaultHttpContext();
            var connectionMultiplexer = new Mock<IConnectionMultiplexer>();
            Mock<IDatabase> database = new Mock<IDatabase>();
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfig");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);

            dataContext.Database.EnsureCreated();

            dataContext.SaveChanges();
            httpContext.Request.Headers.Add(Constants.COLABA_WEB_URL_HEADER, "https://apply.lendova6.com:6003/texas");
            TenantModel model = new TenantModel();
            model.Id = 7;
            model.Code = "33";
            model.Urls = new List<UrlModel>
            {  new UrlModel
            {
                Type=TenantUrlType.Customer,
                Url="https://apply.lendova6.com:6003/texas"
            }
            };
            model.Branches = new List<BranchModel>

            {
                new BranchModel
                {
                    Code="33",
                    Id=7,
                    IsCorporate=true,
                    LoanOfficers= new List<LoanOfficerModel>
                    {
                         new LoanOfficerModel
                         {
                             Id=7,
                             Code="33"
                         }
                    }

                }
            };
           
            database.Setup(x => x.HashGetAsync(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), It.IsAny<CommandFlags>())).ReturnsAsync(RedisValue.Unbox(Newtonsoft.Json.JsonConvert.SerializeObject(model)));
            connectionMultiplexer.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(database.Object);
            RainsoftGateway.Core.Services.ITenantResolver resolver = new TenantResolver(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), connectionMultiplexer.Object);

            await Assert.ThrowsAsync<Exception>(() => resolver.ResolveTenant(httpContext));
        }
        [Fact]
        public async Task TestTenantResolverServiceUrlSegmentIsThreeAndBranchDoesnotExists()
        {
            var httpContext = new DefaultHttpContext();
            var connectionMultiplexer = new Mock<IConnectionMultiplexer>();
            Mock<IDatabase> database = new Mock<IDatabase>();
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfig");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);

            dataContext.Database.EnsureCreated();
            TenantUrl tenantUrl = new TenantUrl()
            {
                Id = 8,
                Url = "apply.lendova8.com:8003",
                TenantId = 8,
                BranchId = 8,
                TypeId = 1


            };
            dataContext.Set<TenantUrl>().Add(tenantUrl);

            Tenant tenant = new Tenant
            {
                Id = 8,
                Code = "texasa",
                Name = "texasa",
                Branches = new List<Branch>
                {
                    new Branch
                {
                Id =8,
                Code = "texasa",
                TenantId = 8,
                Name = "texasa",
                IsCorporate=false,
                IsActive=true
                    }
                }
            };
            dataContext.Set<Tenant>().Add(tenant);

            BranchLoanOfficerBinder branchLoanOfficerBinder = new BranchLoanOfficerBinder
            {
                Id = 8,
                BranchId = 8,
                EmployeeId = 8
            };
            dataContext.Set<BranchLoanOfficerBinder>().Add(branchLoanOfficerBinder);

            Employee employee = new Employee
            {
                Id = 8,
                Code = "44",
                IsActive = true,
                IsLoanOfficer = true
            };
            dataContext.Set<Employee>().Add(employee);

            dataContext.SaveChanges();

            httpContext.Request.Headers.Add(Constants.COLABA_WEB_URL_HEADER, "https://apply.lendova8.com:8003/texas/Alia");

            connectionMultiplexer.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(database.Object);
            RainsoftGateway.Core.Services.ITenantResolver resolver = new TenantResolver(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), connectionMultiplexer.Object);
            await Assert.ThrowsAsync<Exception>(() => resolver.ResolveTenant(httpContext));

        }
        [Fact]
        public async Task TestTenantResolverServiceUrlSegmentIsThreeAndBranchExists()
        {
            var httpContext = new DefaultHttpContext();
            var connectionMultiplexer = new Mock<IConnectionMultiplexer>();
            Mock<IDatabase> database = new Mock<IDatabase>();
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfig");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);

            dataContext.Database.EnsureCreated();

            TenantModel model = new TenantModel();
            model.Id = 9;
            model.Code = "33";
            model.Urls = new List<UrlModel>
            {  new UrlModel
            {
                Type=TenantUrlType.Customer,
                Url="apply.lendova9.com:9003"
            }
            };
            model.Branches = new List<BranchModel>

            {
                new BranchModel
                {
                    Code="texas",
                    Id=9,
                    IsCorporate=true,
                    LoanOfficers= new List<LoanOfficerModel>
                    {
                         new LoanOfficerModel
                         {
                             Id=9,
                             Code="33"
                         }
                    }

                }
            };

            database.Setup(x => x.HashGetAsync(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), It.IsAny<CommandFlags>())).ReturnsAsync(RedisValue.Unbox(Newtonsoft.Json.JsonConvert.SerializeObject(model)));

            httpContext.Request.Headers.Add(Constants.COLABA_WEB_URL_HEADER, "https://apply.lendova9.com:9003/texas/Alia");

            connectionMultiplexer.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(database.Object);
            RainsoftGateway.Core.Services.ITenantResolver resolver = new TenantResolver(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), connectionMultiplexer.Object);
            await Assert.ThrowsAsync<Exception>(() => resolver.ResolveTenant(httpContext));

        }
        [Fact]
        public async Task TestTenantResolverServiceUrlSegmentIsZero()
        {
            var httpContext = new DefaultHttpContext();
            var connectionMultiplexer = new Mock<IConnectionMultiplexer>();
            Mock<IDatabase> database = new Mock<IDatabase>();
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfig");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);

            dataContext.Database.EnsureCreated();
            TenantUrl tenantUrl = new TenantUrl()
            {
                Id = 9,
                Url = "apply.lendova9.com:9003",
                TenantId = 9,
                BranchId = 9,
                TypeId = 1


            };
            dataContext.Set<TenantUrl>().Add(tenantUrl);

            Tenant tenant = new Tenant
            {
                Id = 9,
                Code = "texas",
                Name = "texas",
                Branches = new List<Branch>
                {
                    new Branch
                {
                Id =9,
                Code = "texas",
                TenantId = 9,
                Name = "texas",
                IsCorporate=false,
                IsActive=true
                    }
                }
            };
            dataContext.Set<Tenant>().Add(tenant);

            BranchLoanOfficerBinder branchLoanOfficerBinder = new BranchLoanOfficerBinder
            {
                Id = 9,
                BranchId = 9,
                EmployeeId = 9
            };
            dataContext.Set<BranchLoanOfficerBinder>().Add(branchLoanOfficerBinder);

            Employee employee = new Employee
            {
                Id = 9,
                Code = "44",
                IsActive = true,
                IsLoanOfficer = true
            };
            dataContext.Set<Employee>().Add(employee);

            dataContext.SaveChanges();

            httpContext.Request.Headers.Add(Constants.COLABA_WEB_URL_HEADER, "https://apply.lendova9.com:9003/texas/Alia/test/app");

            connectionMultiplexer.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(database.Object);
            RainsoftGateway.Core.Services.ITenantResolver resolver = new TenantResolver(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), connectionMultiplexer.Object);
            await Assert.ThrowsAsync<Exception>(() => resolver.ResolveTenant(httpContext));

        }
    }
}
