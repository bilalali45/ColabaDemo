using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Moq;
using Rainmaker.API.Controllers;
using Rainmaker.Service;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Routing;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Rainmaker.Test
{
    public class AdminDashboardTest
    {
        [Fact]
        public async Task TestGetMenuController()
        {
            //Arrange
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();
            Mock<ISitemapService> mockSitemapService = new Mock<ISitemapService>();

            var adminDashboardController = new AdminDashboardController(mockSitemapService.Object, mockUserProfileService.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            adminDashboardController.ControllerContext = context;

            //LoanOfficer obj = new LoanOfficer() { FirstName = "Smith" };
            //mockSitemapService.Setup(x => x.Get(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(obj);

            ////Act
            //IActionResult result = await loanApplicationController.GetLOInfo(1, 1);
            ////Assert
            //Assert.NotNull(result);
            //Assert.IsType<OkObjectResult>(result);
            //var content = (result as OkObjectResult).Value as LoanOfficer;
            //Assert.NotNull(content);
            //Assert.Equal("Smith", content.FirstName);
        }
    }
}
