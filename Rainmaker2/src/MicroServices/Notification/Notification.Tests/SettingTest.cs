using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Moq;
using Notification.API.Controllers;
using Notification.Data;
using Notification.Entity.Models;
using Notification.Model;
using Notification.Service;
using TrackableEntities.Common.Core;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace Notification.Tests
{
    public class SettingTest
    {
        [Fact]
        public async Task TestGetSettingsController()
        {
            //Arrange
            Mock<ISettingService> mockSettingService = new Mock<ISettingService>();

            var settingController = new SettingController(mockSettingService.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new Microsoft.AspNetCore.Mvc.ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            settingController.ControllerContext = context;

            //Act
            IActionResult result = await settingController.GetSettings();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestUpdateSettingsController()
        {
            //Arrange
            Mock<ISettingService> mockSettingService = new Mock<ISettingService>();

            var settingController = new SettingController(mockSettingService.Object);

            UpdateSettingModel updateSettingModel = new UpdateSettingModel();
            updateSettingModel.notificationTypeId = 1;
            updateSettingModel.deliveryModeId = 2;
            updateSettingModel.delayedInterval = 5;

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new Microsoft.AspNetCore.Mvc.ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            settingController.ControllerContext = context;

            //Act
            IActionResult result = await settingController.UpdateSettings(updateSettingModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestGetSettingsService()
        {
            //Arrange
            DbContextOptions<NotificationContext> options;
            var builder = new DbContextOptionsBuilder<NotificationContext>();
            builder.UseInMemoryDatabase("Notification");
            options = builder.Options;
            using NotificationContext dataContext = new NotificationContext(options);

            dataContext.Database.EnsureCreated();

            TenantSetting tenantSetting = new TenantSetting()
            {
                Id = 1,
                UserId = 1,
                TenantId = 1,
                DeliveryModeId = 2,
                NotificationMediumId = 1,
                NotificationTypeId = 1,
                DelayedInterval = 25
            };
            dataContext.Set<TenantSetting>().Add(tenantSetting);

            NotificationType notificationType = new NotificationType()
            {
                Id = 1,
                Name = "Document Submission"
            };
            dataContext.Set<NotificationType>().Add(notificationType);

            dataContext.SaveChanges();

            ISettingService settingService = new SettingService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            List<SettingModel> res = await settingService.GetSettings(1, 1);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(1, res[0].Id);
            Assert.Equal(1, res[0].UserId);
            Assert.Equal(1, res[0].TenantId);
            Assert.Equal(2, res[0].DeliveryModeId);
            Assert.Equal(1, res[0].NotificationMediumId);
            Assert.Equal(1, res[0].NotificationTypeId);
            Assert.Equal("Document Submission", res[0].NotificationType);
        }
        [Fact]
        public async Task TestGetSettingsServiceUserIdNull()
        {
            //Arrange
            DbContextOptions<NotificationContext> options;
            var builder = new DbContextOptionsBuilder<NotificationContext>();
            builder.UseInMemoryDatabase("Notification");
            options = builder.Options;
            using NotificationContext dataContext = new NotificationContext(options);

            dataContext.Database.EnsureCreated();

            TenantSetting tenantSetting = new TenantSetting()
                                          {
                                              Id = 2,
                                              TenantId = 2,
                                              DeliveryModeId = 1,
                                              NotificationMediumId = 2,
                                              NotificationTypeId = 2,
                                              DelayedInterval = 25
                                          };
            dataContext.Set<TenantSetting>().Add(tenantSetting);

            NotificationType notificationType = new NotificationType()
                                                {
                                                    Id = 2,
                                                    Name = "Loan Application Submitted"
                                                };
            dataContext.Set<NotificationType>().Add(notificationType);

            dataContext.SaveChanges();

            ISettingService settingService = new SettingService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            List<SettingModel> res = await settingService.GetSettings(2, 2);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(2, res[0].Id);
            Assert.Equal(2, res[0].TenantId);
            Assert.Equal(1, res[0].DeliveryModeId);
            Assert.Equal(2, res[0].NotificationMediumId);
            Assert.Equal(2, res[0].NotificationTypeId);
        }

        [Fact]
        public async Task TestUpdateSettingsService()
        {
            //Arrange
            DbContextOptions<NotificationContext> options;
            var builder = new DbContextOptionsBuilder<NotificationContext>();
            builder.UseInMemoryDatabase("Notification");
            options = builder.Options;
            using NotificationContext dataContext = new NotificationContext(options);

            dataContext.Database.EnsureCreated();

            TenantSetting app = new TenantSetting()
                                {
                                    Id = 20,
                                    TrackingState = TrackingState.Modified,
                                    DeliveryModeId = 3,
                                    TenantId = 20,
                                    NotificationTypeId = 10,
                                    UserId = 2
            };
            dataContext.Set<TenantSetting>().Add(app);

            dataContext.SaveChanges();

            ISettingService settingService = new SettingService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            await settingService.UpdateSettings(1,1,9,3,null );
        }

        [Fact]
        public async Task TestUpdateSettingsServiceExist()
        {
            //Arrange
            DbContextOptions<NotificationContext> options;
            var builder = new DbContextOptionsBuilder<NotificationContext>();
            builder.UseInMemoryDatabase("Notification");
            options = builder.Options;
            using NotificationContext dataContext = new NotificationContext(options);

            dataContext.Database.EnsureCreated();

            TenantSetting app = new TenantSetting()
                                {
                                    Id = 50,
                                    TrackingState = TrackingState.Modified,
                                    DeliveryModeId = 1,
                                    TenantId = 39,
                                    UserId = 1,
                                    NotificationTypeId = 9
                                };
            dataContext.Set<TenantSetting>().Add(app);

            dataContext.SaveChanges();

            ISettingService settingService = new SettingService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            await settingService.UpdateSettings(39, 1, 9, 2, 25);
        }
    }
}
