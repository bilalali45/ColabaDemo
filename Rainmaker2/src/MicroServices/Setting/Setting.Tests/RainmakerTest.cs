using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Primitives;
using Moq;
using Setting.API.Controllers;
using Setting.Model;
using Setting.Service;
using Xunit;

namespace Setting.Tests
{
    public class RainmakerTest
    {
        [Fact]
        public async Task TestGetSettingsController()
        {
            //Arrange
            Mock<IRainmakerService> mockRainmakerService = new Mock<IRainmakerService>();

            List<UserRole> lstUserRoles = new List<UserRole>();
            UserRole userRole = new UserRole();
            userRole.RoleName = "Executives";
            userRole.RoleId = 2;
            userRole.IsRoleAssigned = true;
            lstUserRoles.Add(userRole);

            mockRainmakerService.Setup(x => x.GetUserRoles(It.IsAny<IEnumerable<string>>())).ReturnsAsync(lstUserRoles);

            var httpContext = new Mock<HttpContext>();

            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                                                                      new StringValues("Test")
                                                                     );

            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new RainmakerController(mockRainmakerService.Object);

            controller.ControllerContext = context;

            //Act
            IActionResult result = await controller.GetUserRoles();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestUpdateUserRolesControllerTrue()
        {
            //Arrange
            Mock<IRainmakerService> mockRainmakerService = new Mock<IRainmakerService>();

            mockRainmakerService.Setup(x => x.UpdateUserRoles(It.IsAny<List<UserRole>>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(true);

            var httpContext = new Mock<HttpContext>();

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                                                                      new StringValues("Test")
                                                                     );

            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var rainmakerController = new RainmakerController(mockRainmakerService.Object);

            rainmakerController.ControllerContext = context;
            //Act
            List<UserRole> lstUserRoles = new List<UserRole>();
            UserRole userRole = new UserRole();
            userRole.RoleName = "Executives";
            userRole.RoleId = 2;
            userRole.IsRoleAssigned = true;
            lstUserRoles.Add(userRole);

            IActionResult result = await rainmakerController.UpdateUserRoles(lstUserRoles);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestUpdateUserRolesControllerFalse()
        {
            //Arrange
            Mock<IRainmakerService> mockRainmakerService = new Mock<IRainmakerService>();

            mockRainmakerService.Setup(x => x.UpdateUserRoles(It.IsAny<List<UserRole>>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(false);

            var httpContext = new Mock<HttpContext>();

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                                                                      new StringValues("Test")
                                                                     );

            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var rainmakerController = new RainmakerController(mockRainmakerService.Object);

            rainmakerController.ControllerContext = context;
            //Act
            List<UserRole> lstUserRoles = new List<UserRole>();
            UserRole userRole = new UserRole();
            userRole.RoleName = "Executives";
            userRole.RoleId = 2;
            userRole.IsRoleAssigned = true;
            lstUserRoles.Add(userRole);

            IActionResult result = await rainmakerController.UpdateUserRoles(lstUserRoles);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
