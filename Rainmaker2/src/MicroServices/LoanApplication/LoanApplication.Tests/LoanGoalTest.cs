using LoanApplication.API.Controllers;
using LoanApplication.Model;
using LoanApplication.Service;
using LoanApplicationDb.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using TenantConfig.Service;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace LoanApplication.Tests
{
    public class LoanGoalTest
    {
        [Fact]
        public async Task AddOrUpdateControllerTest()
        {
            Mock<ILoanGoalService> mockLoanGoalService = new Mock<ILoanGoalService>();
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            var controller = new LoanGoalController(mockLoanGoalService.Object, mockInfoService.Object);
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
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

            {
                new TenantConfig.Common.DistributedCache.BranchModel
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
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserId")).Returns(new Claim("UserId", "1"));
            Dictionary<object, object> keyValuePairs = new Dictionary<object, object>();
            keyValuePairs.Add(Constants.COLABA_TENANT, model);
            httpContext.Setup(x => x.Items).Returns(keyValuePairs);

            controller.ControllerContext = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            AddOrUpdateLoanGoalModel addOrUpdate = new AddOrUpdateLoanGoalModel
            {
                LoanApplicationId = 1

            };
            var result = await controller.AddOrUpdate(addOrUpdate);
            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);
        }
        [Fact]
        public async Task GetAllLoanGoalControllerTest()
        {
            Mock<ILoanGoalService> mockLoanGoalService = new Mock<ILoanGoalService>();
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            var controller = new LoanGoalController(mockLoanGoalService.Object, mockInfoService.Object);
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
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

            {
                new TenantConfig.Common.DistributedCache.BranchModel
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
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserId")).Returns(new Claim("UserId", "1"));
            Dictionary<object, object> keyValuePairs = new Dictionary<object, object>();
            keyValuePairs.Add(Constants.COLABA_TENANT, model);
            httpContext.Setup(x => x.Items).Returns(keyValuePairs);
            controller.ControllerContext = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            var result = controller.GetAllLoanGoal(1);
            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);
        }
        [Fact]
        public async Task GetLoanGoalControllerTest()
        {
            Mock<ILoanGoalService> mockLoanGoalService = new Mock<ILoanGoalService>();
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            var controller = new LoanGoalController(mockLoanGoalService.Object, mockInfoService.Object);
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
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

            {
                new TenantConfig.Common.DistributedCache.BranchModel
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
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserId")).Returns(new Claim("UserId", "1"));
            Dictionary<object, object> keyValuePairs = new Dictionary<object, object>();
            keyValuePairs.Add(Constants.COLABA_TENANT, model);
            httpContext.Setup(x => x.Items).Returns(keyValuePairs);
            controller.ControllerContext = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            var result = await controller.GetLoanGoal(1);
            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);
        }


        [Fact]
        public async Task AddOrUpdateServiceTest()
        {
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
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

            {
                new TenantConfig.Common.DistributedCache.BranchModel
                {
                    Code="texas",
                    Id=900,
                    IsCorporate=true,
                    LoanOfficers= new List<LoanOfficerModel>
                    {
                         new LoanOfficerModel
                         {
                             Id=900,
                             Code="33"
                         }
                    }

                }
            };

            //Arrange
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigLoanGoalAddUpdate");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();

            //dataContext.Set<CompanyPhoneInfo>().Add(companyPhoneInfo);
            dataContext.SaveChanges();

            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationLoanGoalAddUpdate");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 1,
                IsActive = true,
                UserId = 1,
                TenantId = 9
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();
         
            var service = new LoanGoalService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())));
            AddOrUpdateLoanGoalModel addOrUpdate = new AddOrUpdateLoanGoalModel
            {
            LoanApplicationId=1
            };
            var result = await service.AddOrUpdate(model, 1, addOrUpdate);
            //Assert
            Assert.NotNull(result);

        }
        [Fact]
        public async Task AddOrUpdateServiceTestNew()
        {
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
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

            {
                new TenantConfig.Common.DistributedCache.BranchModel
                {
                    Code="texas",
                    Id=900,
                    IsCorporate=true,
                    LoanOfficers= new List<LoanOfficerModel>
                    {
                         new LoanOfficerModel
                         {
                             Id=900,
                             Code="33"
                         }
                    }

                }
            };

            //Arrange
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigLoanGoalAddUpdateNew");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();

            TenantConfig.Entity.Models.Customer customer = new TenantConfig.Entity.Models.Customer
            {
                Id=1,
                TenantId=9,
                UserId=1
            };
            dataContext.Set<TenantConfig.Entity.Models.Customer>().Add(customer);
            dataContext.SaveChanges();

            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationLoanGoalAddUpdateNew");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 1,
                IsActive = true,
                UserId = 1,
                TenantId = 9
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            LoanApplicationDb.Entity.Models.Config config = new LoanApplicationDb.Entity.Models.Config
            {
                Id = 90000,
                Name = "Test"
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.Config>().Add(config);
            dataContext1.SaveChanges();

            var service = new LoanGoalService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())));
            AddOrUpdateLoanGoalModel addOrUpdate = new AddOrUpdateLoanGoalModel
            {
                LoanApplicationId = null
            };
            var result = await service.AddOrUpdate(model, 1, addOrUpdate);
            //Assert
            Assert.NotNull(result);

        }
        [Fact]
        public async Task GetLoanGoalServiceTest()
        {
            Mock<ILoanGoalService> mockLoanGoalService = new Mock<ILoanGoalService>();

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
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

            {
                new TenantConfig.Common.DistributedCache.BranchModel
                {
                    Code="texas",
                    Id=900,
                    IsCorporate=true,
                    LoanOfficers= new List<LoanOfficerModel>
                    {
                         new LoanOfficerModel
                         {
                             Id=900,
                             Code="33"
                         }
                    }

                }
            };

            //Arrange
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigGetLoanGoalServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();

            //dataContext.Set<CompanyPhoneInfo>().Add(companyPhoneInfo);
            dataContext.SaveChanges();
            GetLoanGoalModel getLoanGoalModel = new GetLoanGoalModel
            {  LoanGoal=1,
            LoanPurpose=1,
            
            };

            mockLoanGoalService.Setup(x => x.GetLoanGoal(It.IsAny<TenantModel>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(getLoanGoalModel);
            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationGetLoanGoalServiceTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 1,
                IsActive = true,
                UserId = 1,
                TenantId = 9,
                LoanGoalId=1
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            var service = new LoanGoalService(new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())));
            AddOrUpdateLoanGoalModel addOrUpdate = new AddOrUpdateLoanGoalModel
            {
                LoanApplicationId = 1
            };
            var result = await service.GetLoanGoal(model, 1, 1);
            //Assert
            Assert.NotNull(result);

        }
    }
}
