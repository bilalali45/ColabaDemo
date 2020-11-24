using Moq;
using Setting.Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Setting.Model;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Primitives;
using Setting.API.Controllers;

namespace Setting.Tests
{
    public class NotificationTest
    {
        [Fact]
        public async Task TestGetSettingsController()
        {
            //Arrange
            Mock<INotificationService> mockNotificationService = new Mock<INotificationService>();

            SettingModel settingModel = new SettingModel();
            settingModel.Id = 1;
            settingModel.UserId = 1;
            settingModel.TenantId = 1;
            settingModel.DeliveryModeId = 2;
            settingModel.NotificationMediumId = 1;
            settingModel.NotificationTypeId = 1;
            settingModel.DelayedInterval = 25;

            List<SettingModel> lstSettingModels = new List<SettingModel>();
            lstSettingModels.Add(settingModel);

            mockNotificationService.Setup(x => x.GetSettings(It.IsAny<IEnumerable<string>>())).ReturnsAsync(lstSettingModels);

            var httpContext = new Mock<HttpContext>();

            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                                                                      new StringValues("Test")
                                                                     );

            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new NotificationController(mockNotificationService.Object);

            controller.ControllerContext = context;

            //Act
            IActionResult result = await controller.GetSettings();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestUpdateSettingsControllerTrue()
        {
            //Arrange
            Mock<INotificationService> mockNotificationService = new Mock<INotificationService>();

            mockNotificationService.Setup(x => x.UpdateSettings(It.IsAny<int>(), It.IsAny<short>(), It.IsAny<short?>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(true);
           
            var httpContext = new Mock<HttpContext>();
          
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );

            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var notificationController = new NotificationController(mockNotificationService.Object);

            notificationController.ControllerContext = context;
            //Act
            UpdateSettingModel updateSettingModel = new UpdateSettingModel();
            updateSettingModel.notificationTypeId = 1;
            updateSettingModel.deliveryModeId = 2;
            updateSettingModel.delayedInterval = 5;
            
            IActionResult result = await notificationController.UpdateSettings(updateSettingModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestUpdateSettingsControllerFalse()
        {
            //Arrange
            Mock<INotificationService> mockNotificationService = new Mock<INotificationService>();

            mockNotificationService.Setup(x => x.UpdateSettings(It.IsAny<int>(), It.IsAny<short>(), It.IsAny<short?>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(false);

            var httpContext = new Mock<HttpContext>();

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                                                                      new StringValues("Test")
                                                                     );

            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var notificationController = new NotificationController(mockNotificationService.Object);

            notificationController.ControllerContext = context;
            //Act
            UpdateSettingModel updateSettingModel = new UpdateSettingModel();
            updateSettingModel.notificationTypeId = 1;
            updateSettingModel.deliveryModeId = 2;
            updateSettingModel.delayedInterval = 5;

            IActionResult result = await notificationController.UpdateSettings(updateSettingModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
