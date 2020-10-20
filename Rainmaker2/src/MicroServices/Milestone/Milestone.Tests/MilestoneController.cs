using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
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
            Assert.Single(content);
            Assert.Equal(1,content[0].Id);
        }
        [Fact]
        public async Task TestGetMilestoneId()
        {
            //Arrange
            Mock<IRainmakerService> mock = new Mock<IRainmakerService>();
            mock.Setup(x => x.GetMilestoneId(It.IsAny<int>(),It.IsAny<IEnumerable<string>>())).ReturnsAsync(1);
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var controller = new MilestoneController(null,mock.Object);
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(m => m.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            //Act
            IActionResult result = await controller.GetMilestoneId(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (int)(result as OkObjectResult).Value;
            Assert.Equal(1, content);
        }
        [Fact]
        public async Task TestSetMilestoneId()
        {
            //Arrange
            Mock<IRainmakerService> mock = new Mock<IRainmakerService>();
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mock.Setup(x => x.SetMilestoneId(It.IsAny<int>(), It.IsAny<int>())).Verifiable();
            mockMilestone.Setup(x => x.UpdateMilestoneLog(It.IsAny<int>(), It.IsAny<int>())).Verifiable();
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var controller = new MilestoneController(mockMilestone.Object, mock.Object);
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(m => m.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            //Act
            IActionResult result = await controller.SetMilestoneId(new MilestoneIdModel() {loanApplicationId=1,milestoneId=1 });

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
            mock.Verify(x => x.SetMilestoneId(It.IsAny<int>(), It.IsAny<int>()),Times.Once());
            mockMilestone.Verify(x => x.UpdateMilestoneLog(It.IsAny<int>(), It.IsAny<int>()),Times.Once());
        }
        [Fact]
        public async Task TestGetMilestoneForBorrowerDashboard()
        {
            //Arrange
            Mock<IRainmakerService> mock = new Mock<IRainmakerService>();
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mock.Setup(x => x.GetMilestoneId(It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(1);
            mockMilestone.Setup(x => x.GetMilestoneForBorrowerDashboard(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new MilestoneForBorrowerDashboard() { Icon = "", Name = "Test" });
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var controller = new MilestoneController(mockMilestone.Object, mock.Object);
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(m => m.Request).Returns(request.Object);
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            //Act
            IActionResult result = await controller.GetMilestoneForBorrowerDashboard(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (MilestoneForBorrowerDashboard)(result as OkObjectResult).Value;
            Assert.Equal("Test", content.Name);
        }
        [Fact]
        public async Task TestGetMilestoneForBorrowerDashboardNull()
        {
            //Arrange
            Mock<IRainmakerService> mock = new Mock<IRainmakerService>();
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mock.Setup(x => x.GetMilestoneId(It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(-1);
            mockMilestone.Setup(x => x.GetMilestoneForBorrowerDashboard(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new MilestoneForBorrowerDashboard() { Icon = "", Name = "Test" });
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var controller = new MilestoneController(mockMilestone.Object, mock.Object);
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(m => m.Request).Returns(request.Object);
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            //Act
            IActionResult result = await controller.GetMilestoneForBorrowerDashboard(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (MilestoneForBorrowerDashboard)(result as OkObjectResult).Value;
            Assert.Null(content);
        }

        [Fact]
        public async Task TestGetMilestoneForLoanCenter()
        {
            //Arrange
            Mock<IRainmakerService> mock = new Mock<IRainmakerService>();
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mock.Setup(x => x.GetMilestoneId(It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(1);
            mockMilestone.Setup(x => x.GetMilestoneForLoanCenter(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<MilestoneForLoanCenter>() { new MilestoneForLoanCenter() { Name="Test"} });
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var controller = new MilestoneController(mockMilestone.Object, mock.Object);
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(m => m.Request).Returns(request.Object);
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            //Act
            IActionResult result = await controller.GetMilestoneForLoanCenter(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (List<MilestoneForLoanCenter>)(result as OkObjectResult).Value;
            Assert.Equal("Test", content[0].Name);
        }
        [Fact]
        public async Task TestGetMilestoneForLoanCenterNull()
        {
            //Arrange
            Mock<IRainmakerService> mock = new Mock<IRainmakerService>();
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mock.Setup(x => x.GetMilestoneId(It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(-1);
            mockMilestone.Setup(x => x.GetMilestoneForLoanCenter(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<MilestoneForLoanCenter>() { new MilestoneForLoanCenter() { Name = "Test" } });
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var controller = new MilestoneController(mockMilestone.Object, mock.Object);
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(m => m.Request).Returns(request.Object);
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            //Act
            IActionResult result = await controller.GetMilestoneForLoanCenter(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (List<MilestoneForLoanCenter>)(result as OkObjectResult).Value;
            Assert.Null(content);
        }
        [Fact]
        public async Task TestGetMilestoneForMcuDashboard()
        {
            //Arrange
            Mock<IRainmakerService> mock = new Mock<IRainmakerService>();
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mock.Setup(x => x.GetMilestoneId(It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(1);
            mockMilestone.Setup(x => x.GetMilestoneForMcuDashboard(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync("Test");
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var controller = new MilestoneController(mockMilestone.Object, mock.Object);
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(m => m.Request).Returns(request.Object);
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            //Act
            IActionResult result = await controller.GetMilestoneForMcuDashboard(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (string)(result as OkObjectResult).Value;
            Assert.Equal("Test", content);
        }
        [Fact]
        public async Task TestGetMilestoneForMcuDashboardNull()
        {
            //Arrange
            Mock<IRainmakerService> mock = new Mock<IRainmakerService>();
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mock.Setup(x => x.GetMilestoneId(It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(-1);
            mockMilestone.Setup(x => x.GetMilestoneForMcuDashboard(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync("Test");
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var controller = new MilestoneController(mockMilestone.Object, mock.Object);
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(m => m.Request).Returns(request.Object);
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            //Act
            IActionResult result = await controller.GetMilestoneForMcuDashboard(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (string)(result as OkObjectResult).Value;
            Assert.Equal("",content);
        }
        [Fact]
        public async Task TestSetLosMilestone()
        {
            //Arrange
            Mock<IRainmakerService> mock = new Mock<IRainmakerService>();
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mock.Setup(x => x.SetMilestoneId(It.IsAny<int>(), It.IsAny<int>())).Verifiable();
            mockMilestone.Setup(x => x.GetLosMilestone(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<short>())).ReturnsAsync(1);
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var controller = new MilestoneController(mockMilestone.Object, mock.Object);
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(m => m.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            IActionResult result = await controller.SetLosMilestone(new LosMilestoneModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestSetLosMilestoneNull()
        {
            //Arrange
            Mock<IRainmakerService> mock = new Mock<IRainmakerService>();
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mock.Setup(x => x.SetMilestoneId(It.IsAny<int>(), It.IsAny<int>())).Verifiable();
            mockMilestone.Setup(x => x.GetLosMilestone(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<short>())).ReturnsAsync(-1);
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var controller = new MilestoneController(mockMilestone.Object, mock.Object);
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(m => m.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            IActionResult result = await controller.SetLosMilestone(new LosMilestoneModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestGetGlobalMilestoneSetting()
        {
            //arrange
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mockMilestone.Setup(x => x.GetGlobalMilestoneSetting(It.IsAny<int>())).ReturnsAsync(new GlobalMilestoneSettingModel() { ShowMilestone = true, TenantId = 1 });
            var controller = new MilestoneController(mockMilestone.Object, null);
            //act
            var result = await controller.GetGlobalMilestoneSetting(1);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (GlobalMilestoneSettingModel)(result as OkObjectResult).Value;
            Assert.True(content.ShowMilestone);
        }
    }
}
