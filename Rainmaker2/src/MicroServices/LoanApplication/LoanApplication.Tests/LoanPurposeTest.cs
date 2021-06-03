using LoanApplication.API.Controllers;
using LoanApplication.Model;
using LoanApplication.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Service;
using Xunit;

namespace LoanApplication.Tests
{
    public class LoanPurposeTest
    {
        [Fact]
        public async Task GetAllLoanPurposeControllerTest()
        {
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            var controller = new LoanPurposeController(  mockInfoService.Object);
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
                LoanApplicationId=1

            };
             var result = await controller.GetAllLoanPurpose();
            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);
        }

 
    }
}
