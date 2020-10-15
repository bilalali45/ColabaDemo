using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Milestone.API.Controllers;
using Milestone.Model;
using Milestone.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Milestone.Tests
{
    public partial class UnitTest
    {
        [Fact]
        public async Task TestGetAllMilestones()
        {
            //Arrange
            Mock<IMilestoneService> mock = new Mock<IMilestoneService>();
            List<MilestoneModel> list = new List<MilestoneModel>() { new MilestoneModel() { Id=1,Name="Test"} };
            mock.Setup(x => x.GetAllMilestones(It.IsAny<int>())).ReturnsAsync(list);
            var controller = new MilestoneController(mock.Object, null);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            IActionResult result = await controller.GetAllMilestones();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as List<MilestoneModel>;
            Assert.NotNull(content);
            Assert.Equal(1,content.Count);
            Assert.Equal(1,content[0].Id);
        }
    }
}
