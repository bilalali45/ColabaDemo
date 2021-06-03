using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.API.Controllers;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using TenantConfig.Service;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;
using BranchModel = TenantConfig.Model.BranchModel;
namespace TenantConfig.Tests
{
    public class TermConditionServiceTest
    {
        [Fact]
        public async Task GetSettingControllerTest()
        {
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            Mock<IUnitOfWork<TenantConfigContext>> mockTenantConfigContext = new Mock<IUnitOfWork<TenantConfigContext>>();
            Mock<ITermConditionService> mockTermConditionService = new Mock<ITermConditionService>();
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
            var controller = new TenantController(mockTermConditionService.Object);
            var httpContext = new DefaultHttpContext();
            httpContext.Items.Add(Constants.COLABA_TENANT, model);
            controller.ControllerContext = new ControllerContext(new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            var result = await controller.GetSetting();
            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);
        }
        [Fact]
        public async Task GetTermsConditionsControllerTest()
        {
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            Mock<IUnitOfWork<TenantConfigContext>> mockTenantConfigContext = new Mock<IUnitOfWork<TenantConfigContext>>();
            Mock<ITermConditionService> mockTermConditionService = new Mock<ITermConditionService>();
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
            var controller = new TenantController(mockTermConditionService.Object);
            var httpContext = new DefaultHttpContext();
            httpContext.Items.Add(Constants.COLABA_TENANT, model);
            controller.ControllerContext = new ControllerContext(new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            var result = await controller.GetTermCondition(1);
            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);
        }
        [Fact]
        public async Task GetTermsConditionsServiceTest()
        {
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            Mock<IUnitOfWork<TenantConfigContext>> mockTenantConfigContext = new Mock<IUnitOfWork<TenantConfigContext>>();
            Mock<ITermConditionService> mockTermConditionService = new Mock<ITermConditionService>();

            //Arrange
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfig");
            options = builder.Options;
            using TenantConfigContext tenantConfigContext = new TenantConfigContext(options);
            tenantConfigContext.Database.EnsureCreated();
            TermsCondition termsCondition = new TermsCondition
            {
                TenantId = 1,
                BranchId = 1,
                IsActive = true,
                TermTypeId = 1,
                TermsContent = "vc"
            };
            tenantConfigContext.Set<TermsCondition>().Add(termsCondition);
            tenantConfigContext.SaveChanges();

            mockTermConditionService.Setup(x => x.GetTermsConditions(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync("");
            var service = new TermConditionService(new UnitOfWork<TenantConfigContext>(tenantConfigContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object);
            //Act
            var result = await service.GetTermsConditions(1, 1, 1);
            Assert.NotNull(result);
            Assert.Equal("vc", result);

        }
        [Fact]
        public async Task GetSettingServiceTest()
        {
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            Mock<IUnitOfWork<TenantConfigContext>> mockTenantConfigContext = new Mock<IUnitOfWork<TenantConfigContext>>();
            Mock<ITermConditionService> mockTermConditionService = new Mock<ITermConditionService>();

            //Arrange
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfig");
            options = builder.Options;
            using TenantConfigContext tenantConfigContext = new TenantConfigContext(options);
            tenantConfigContext.Database.EnsureCreated();
            Branch branch = new Branch
            {
                Id = 1,
                IsActive = true,
                PrimaryColor = "abcd",
                Footer = "bc"

            };

            tenantConfigContext.Set<Branch>().Add(branch);
            tenantConfigContext.SaveChanges();
            TenantModel tenantModel = new TenantModel
            {
                Id = 1,
                Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>
                {
                    new TenantConfig.Common.DistributedCache.BranchModel
                    {
                        Id=1,
                        Code="200",

                    }
                },
                Code = "200",
                Urls = new List<UrlModel>
                {
                     new UrlModel
                     {

                         Url= "https://"
                     }
                }

            };
            BranchModel branchModel = new BranchModel
            {
                Color = "abcd",
                Favicon = "abc",
                Footer = "bc",
                Logo = "abcd"
            };
            mockTermConditionService.Setup(x => x.GetSetting(It.IsAny<TenantModel>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(branchModel);
            var service = new TermConditionService(new UnitOfWork<TenantConfigContext>(tenantConfigContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object);
            //Act
            var result = await service.GetSetting(tenantModel, 1,"1");
            Assert.NotNull(result);
            Assert.Equal("abcd", result.Color);
            Assert.Equal("https://https:///colabacdn/200/200/", result.Favicon);
            Assert.Equal("https://https:///colabacdn/200/200/", result.Logo);
            Assert.Equal("bc", result.Footer);

        }
    }
}
