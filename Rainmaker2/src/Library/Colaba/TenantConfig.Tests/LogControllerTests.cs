using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.API.Controllers;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Service;
using Xunit;

namespace TenantConfig.Tests
{
    public class LogControllerTests
    {
        [Fact]
        public async Task InsertContactEmailLog()
        {
            Mock<ILogService> mockLogService = new Mock<ILogService>();
            var httpContext = new DefaultHttpContext();

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
            httpContext.Items.Add(Constants.COLABA_TENANT, model);
            mockLogService.Setup(x => x.InsertLogContactEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Verifiable();
            var context = new ControllerContext(new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new LogController(mockLogService.Object);
            controller.ControllerContext = context;

            //Act
            IActionResult result = await controller.InsertContactEmailLog(new Model.ContactEmailLogModel() { });

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task InsertContactEmailPhoneLog()
        {
            Mock<ILogService> mockLogService = new Mock<ILogService>();
            var httpContext = new DefaultHttpContext();

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
            httpContext.Items.Add(Constants.COLABA_TENANT, model);
            mockLogService.Setup(x => x.InsertLogContactEmailPhone(It.IsAny<string>(),It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Verifiable();
            var context = new ControllerContext(new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new LogController(mockLogService.Object);
            controller.ControllerContext = context;

            //Act
            IActionResult result = await controller.InsertContactEmailPhoneLog(new Model.ContactEmailPhoneLogModel() { });

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetRedirectUrl()
        {
            Mock<ITermConditionService> mockLogService = new Mock<ITermConditionService>();
            var httpContext = new DefaultHttpContext();

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
            httpContext.Items.Add(Constants.COLABA_TENANT, model);

            var context = new ControllerContext(new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new TenantController(mockLogService.Object);
            controller.ControllerContext = context;

            //Act
            IActionResult result = await controller.GetRedirectUrl();

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<VerifyModel>(res.Value);
        }
        [Fact]
        public async Task GetTermCondition()
        {
            Mock<ITermConditionService> mockLogService = new Mock<ITermConditionService>();
            var httpContext = new DefaultHttpContext();

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
            httpContext.Items.Add(Constants.COLABA_TENANT, model);
            mockLogService.Setup(x => x.GetTermsConditions(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync("test");
            var context = new ControllerContext(new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new TenantController(mockLogService.Object);
            controller.ControllerContext = context;

            //Act
            IActionResult result = await controller.GetTermCondition(1);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            var strRes = Assert.IsType<string>(res.Value);
            Assert.Equal("test",strRes);
        }
    }
}
