
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
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
            List<MilestoneModel> list = new List<MilestoneModel>() { new MilestoneModel() { Id = 1, Name = "Test" } };
            mock.Setup(x => x.GetAllMilestones(It.IsAny<int>())).ReturnsAsync(list);
            var controller = new MilestoneController(mock.Object, null, null, null);

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
            Assert.Equal(1, content[0].Id);
        }
        [Fact]
        public async Task TestGetMilestoneId()
        {
            //Arrange
            Mock<IRainmakerService> mock = new Mock<IRainmakerService>();
            mock.Setup(x => x.GetMilestoneId(It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(1);
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var controller = new MilestoneController(null, mock.Object, null, null);
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
            Mock<ISettingService> mockSettingService = new Mock<ISettingService>();
            mock.Setup(x => x.SetMilestoneId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).Verifiable();
            mockMilestone.Setup(x => x.UpdateMilestoneLog(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var controller = new MilestoneController(mockMilestone.Object, mock.Object, null, mockSettingService.Object);
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(m => m.Request).Returns(request.Object);

            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            //Act
            IActionResult result = await controller.SetMilestoneId(new MilestoneIdModel() { loanApplicationId = 1, milestoneId = 1 });

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
            mock.Verify(x => x.SetMilestoneId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IEnumerable<string>>()), Times.Once());
            mockMilestone.Verify(x => x.UpdateMilestoneLog(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once());
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
            var controller = new MilestoneController(mockMilestone.Object, mock.Object, null, null);
            var httpContext = new Mock<HttpContext>();
            MilestoneloanIdsModel milestoneloanIdsModel = new MilestoneloanIdsModel();
            milestoneloanIdsModel.loanApplicationId = new int[2] { 2, 3 };
            httpContext.SetupGet(m => m.Request).Returns(request.Object);
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            //Act
            IActionResult result = await controller.GetMilestoneForBorrowerDashboard(milestoneloanIdsModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (List<MilestoneForBorrowerDashboard>)(result as OkObjectResult).Value;
            Assert.Equal("Test", content[0].Name);
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
            var controller = new MilestoneController(mockMilestone.Object, mock.Object, null, null);
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(m => m.Request).Returns(request.Object);
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            MilestoneloanIdsModel milestoneloanIdsModel = new MilestoneloanIdsModel();
            milestoneloanIdsModel.loanApplicationId = new int[2] { 2, 3 };
            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            //Act
            IActionResult result = await controller.GetMilestoneForBorrowerDashboard(milestoneloanIdsModel);

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
            mockMilestone.Setup(x => x.GetMilestoneForLoanCenter(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<MilestoneForLoanCenter>() { new MilestoneForLoanCenter() { Name = "Test" } });
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var controller = new MilestoneController(mockMilestone.Object, mock.Object, null, null);
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
            var controller = new MilestoneController(mockMilestone.Object, mock.Object, null, null);
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
            mockMilestone.Setup(x => x.GetMilestoneForMcuDashboard(It.IsAny<int>(), It.IsAny<BothLosMilestoneModel>(), It.IsAny<int>())).ReturnsAsync(new MilestoneForMcuDashboard() { milestone = "Test" });
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var controller = new MilestoneController(mockMilestone.Object, mock.Object, null, null);
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
            var content = (MilestoneForMcuDashboard)(result as OkObjectResult).Value;
            Assert.Equal("Test", content.milestone);
        }
        [Fact]
        public async Task TestGetMilestoneForMcuDashboardNull()
        {
            //Arrange
            Mock<IRainmakerService> mock = new Mock<IRainmakerService>();
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mock.Setup(x => x.GetMilestoneId(It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(-1);
            mockMilestone.Setup(x => x.GetMilestoneForMcuDashboard(It.IsAny<int>(), It.IsAny<BothLosMilestoneModel>(), It.IsAny<int>())).ReturnsAsync(new MilestoneForMcuDashboard() { milestone = "Test" });
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var controller = new MilestoneController(mockMilestone.Object, mock.Object, null, null);
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
            var content = (MilestoneForMcuDashboard)(result as OkObjectResult).Value;
            Assert.Equal("Test", content.milestone);
        }
        [Fact]
        public async Task TestSetLosMilestone()
        {
            //Arrange
            Mock<IRainmakerService> mock = new Mock<IRainmakerService>();
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mock.Setup(x => x.SetMilestoneId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).Verifiable();
            mockMilestone.Setup(x => x.GetLosMilestone(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<short>())).ReturnsAsync(1);
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var controller = new MilestoneController(mockMilestone.Object, mock.Object, null, null);
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(m => m.Request).Returns(request.Object);
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            IActionResult result = await controller.SetLosMilestone(new LosMilestoneModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task TestSetLosMilestoneValid()
        {
            //Arrange
            Mock<IRainmakerService> mock = new Mock<IRainmakerService>();
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            Mock<ISettingService> mockSettingService = new Mock<ISettingService>();
            mock.Setup(x => x.SetMilestoneId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).Verifiable();
            mock.Setup(x => x.GetLoanApplicationId(It.IsAny<string>(), It.IsAny<short>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(1);
            mockMilestone.Setup(x => x.GetLosMilestone(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<short>())).ReturnsAsync(1);
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var controller = new MilestoneController(mockMilestone.Object, mock.Object, null, mockSettingService.Object);
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(m => m.Request).Returns(request.Object);

            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
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
            mock.Setup(x => x.SetMilestoneId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).Verifiable();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com/");
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var controller = new MilestoneController(mockMilestone.Object, mock.Object, mockConfiguration.Object, null);
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(m => m.Request).Returns(request.Object);

            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
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
            var controller = new MilestoneController(mockMilestone.Object, null, null, null);
            //act
            var result = await controller.GetGlobalMilestoneSetting(1);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (GlobalMilestoneSettingModel)(result as OkObjectResult).Value;
            Assert.True(content.ShowMilestone);
        }
        [Fact]
        public async Task TestSetGlobalMilestoneSetting()
        {
            //arrange
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mockMilestone.Setup(x => x.SetGlobalMilestoneSetting(It.IsAny<GlobalMilestoneSettingModel>())).Verifiable();
            var controller = new MilestoneController(mockMilestone.Object, null, null, null);
            //act
            var result = await controller.SetGlobalMilestoneSetting(new GlobalMilestoneSettingModel());
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestGetMilestoneSettingList()
        {
            //arrange
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mockMilestone.Setup(x => x.GetMilestoneSettingList(It.IsAny<int>())).ReturnsAsync(new List<MilestoneSettingModel>() { new MilestoneSettingModel() { Id = 1 } });
            var controller = new MilestoneController(mockMilestone.Object, null, null, null);
            //act
            var result = await controller.GetMilestoneSettingList(1);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (List<MilestoneSettingModel>)(result as OkObjectResult).Value;
            Assert.Equal(1, content[0].Id);
        }
        [Fact]
        public async Task TestGetMilestoneSetting()
        {
            //arrange
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mockMilestone.Setup(x => x.GetMilestoneSetting(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new MilestoneSettingModel() { Id = 1 });
            var controller = new MilestoneController(mockMilestone.Object, null, null, null);
            //act
            var result = await controller.GetMilestoneSetting(1, 1);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (MilestoneSettingModel)(result as OkObjectResult).Value;
            Assert.Equal(1, content.Id);
        }
        [Fact]
        public async Task TestSetMilestoneSetting()
        {
            //arrange
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mockMilestone.Setup(x => x.SetMilestoneSetting(It.IsAny<MilestoneSettingModel>())).Verifiable();
            var controller = new MilestoneController(mockMilestone.Object, null, null, null);
            //act
            var result = await controller.SetMilestoneSetting(new MilestoneSettingModel());
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestGetLosAll()
        {
            //arrange
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mockMilestone.Setup(x => x.GetLosAll()).ReturnsAsync(new List<LosModel>() { new LosModel() { Id = 1 } });
            var controller = new MilestoneController(mockMilestone.Object, null, null, null);
            //act
            var result = await controller.GetLosAll();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (List<LosModel>)(result as OkObjectResult).Value;
            Assert.Equal(1, content[0].Id);
        }
        [Fact]
        public async Task TestGetMappingAll()
        {
            //arrange
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mockMilestone.Setup(x => x.GetMappingAll(It.IsAny<int>(), It.IsAny<short>())).ReturnsAsync(new List<MappingModel>() { new MappingModel() { Id = 1 } });
            var controller = new MilestoneController(mockMilestone.Object, null, null, null);
            //act
            var result = await controller.GetMappingAll(1, 1);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (List<MappingModel>)(result as OkObjectResult).Value;
            Assert.Equal(1, content[0].Id);
        }
        [Fact]
        public async Task TestGetMapping()
        {
            //arrange
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mockMilestone.Setup(x => x.GetMapping(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new MilestoneMappingModel() { Id = 1 });
            var controller = new MilestoneController(mockMilestone.Object, null, null, null);
            //act
            var result = await controller.GetMapping(1, 1);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (MilestoneMappingModel)(result as OkObjectResult).Value;
            Assert.Equal(1, content.Id);
        }
        [Fact]
        public async Task TestSetMapping()
        {
            //arrange
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mockMilestone.Setup(x => x.SetMapping(It.IsAny<MilestoneMappingModel>())).Verifiable();
            var controller = new MilestoneController(mockMilestone.Object, null, null, null);
            //act
            var result = await controller.SetMapping(new MilestoneMappingModel());
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestAddMapping()
        {
            //arrange
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mockMilestone.Setup(x => x.AddMapping(It.IsAny<MilestoneAddMappingModel>())).Verifiable();
            var controller = new MilestoneController(mockMilestone.Object, null, null, null);
            //act
            var result = await controller.AddMapping(new MilestoneAddMappingModel());
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestEditMapping()
        {
            //arrange
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mockMilestone.Setup(x => x.EditMapping(It.IsAny<MilestoneAddMappingModel>())).Verifiable();
            var controller = new MilestoneController(mockMilestone.Object, null, null, null);
            //act
            var result = await controller.EditMapping(new MilestoneAddMappingModel());
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestDeleteMapping()
        {
            //arrange
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mockMilestone.Setup(x => x.DeleteMapping(It.IsAny<MilestoneAddMappingModel>())).Verifiable();
            var controller = new MilestoneController(mockMilestone.Object, null, null, null);
            //act
            var result = await controller.DeleteMapping(new MilestoneAddMappingModel());
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestInsertMilestoneLog()
        {
            //arrange
            Mock<IMilestoneService> mockMilestone = new Mock<IMilestoneService>();
            mockMilestone.Setup(x => x.InsertMilestoneLog(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();
            var controller = new MilestoneController(mockMilestone.Object, null, null, null);
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(m => m.Request).Returns(request.Object);
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            //act
            var result = await controller.InsertMilestoneLog(new MilestoneIdModel() { loanApplicationId = 1, milestoneId = 1 });
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
    }
}
