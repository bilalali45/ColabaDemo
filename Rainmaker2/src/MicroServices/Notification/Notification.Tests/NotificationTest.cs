using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Moq;
using Notification.API;
using Notification.API.Controllers;
using Notification.Common;
using Notification.Data;
using Notification.Entity.Models;
using Notification.Model;
using Notification.Service;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace Notification.Tests
{
    public class NotificationTest
    {
        [Fact]
        public async Task TestAddDeliveryModeExpressController()
        {
            //Arrange
            Mock<Service.INotificationService> mockUserProfileService = new Mock<Service.INotificationService>();
            TenantSetting tenantSetting = new TenantSetting();
            tenantSetting.DeliveryModeId = (short)Notification.Common.DeliveryMode.Express;
            mockUserProfileService.Setup(x => x.GetTenantSetting(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(tenantSetting);
            var notificationController = new NotificationController(mockUserProfileService.Object, null, Mock.Of<IRedisService>());

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new Microsoft.AspNetCore.Mvc.ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            notificationController.ControllerContext = context;

            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );

            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            NotificationModel notificationModel = new NotificationModel();
            notificationModel.NotificationType = 1;
            notificationModel.EntityId = 1;
            notificationModel.CustomTextJson = "test";
            //Act
            IActionResult result = await notificationController.Add(notificationModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestAddDeliveryModeQueuedController()
        {
            //Arrange
            Mock<Service.INotificationService> mockUserProfileService = new Mock<Service.INotificationService>();
            TenantSetting tenantSetting = new TenantSetting();
            tenantSetting.DeliveryModeId = (short)Notification.Common.DeliveryMode.Queued;
            mockUserProfileService.Setup(x => x.GetTenantSetting(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(tenantSetting);
            var notificationController = new NotificationController(mockUserProfileService.Object, null, Mock.Of<IRedisService>());

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new Microsoft.AspNetCore.Mvc.ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            notificationController.ControllerContext = context;

            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );

            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            NotificationModel notificationModel = new NotificationModel();
            notificationModel.NotificationType = 1;
            notificationModel.EntityId = 1;
            notificationModel.CustomTextJson = "test";
            //Act
            IActionResult result = await notificationController.Add(notificationModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestGetPagedController()
        {
            //Arrange
            Mock<Service.INotificationService> mockUserProfileService = new Mock<Service.INotificationService>();

            var notificationController = new NotificationController(mockUserProfileService.Object, null, Mock.Of<IRedisService>());

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new Microsoft.AspNetCore.Mvc.ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            notificationController.ControllerContext = context;

            //Act
            IActionResult result = await notificationController.GetPaged(-1, -1, 1);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestReadController()
        {
            //Arrange
            Mock<Service.INotificationService> mockUserProfileService = new Mock<Service.INotificationService>();
            Mock<IHubContext<ServerHub, IClientHub>> mockHubContext = new Mock<IHubContext<ServerHub, IClientHub>>();
            Mock<IClientHub> clientHub = new Mock<IClientHub>();

            var notificationController = new NotificationController(mockUserProfileService.Object, mockHubContext.Object, Mock.Of<IRedisService>());

            NotificationRead notificationRead = new NotificationRead();
            notificationRead.ids = new List<long> { 10 };

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            List<long> lstRead = new List<long>();
            lstRead.Add(1);

            mockUserProfileService.Setup(x => x.Read(It.IsAny<List<long>>(), It.IsAny<int>())).ReturnsAsync(lstRead);

            mockHubContext.Setup(x => x.Clients.Clients(It.IsAny<IReadOnlyList<string>>())).Returns(clientHub.Object);

            var context = new Microsoft.AspNetCore.Mvc.ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            notificationController.ControllerContext = context;

            //Act
            IActionResult result = await notificationController.Read(notificationRead, Mock.Of<IConnectionMultiplexer>());
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestDeleteController()
        {
            //Arrange
            Mock<Service.INotificationService> mockUserProfileService = new Mock<Service.INotificationService>();
            Mock<IHubContext<ServerHub, IClientHub>> mockHubContext = new Mock<IHubContext<ServerHub, IClientHub>>();
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            Mock<IClientHub> clientHub = new Mock<IClientHub>();

            mockHubContext.Setup(x => x.Clients.Clients(It.IsAny<IReadOnlyList<string>>())).Returns(clientHub.Object);
            var context = new Microsoft.AspNetCore.Mvc.ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var notificationController = new NotificationController(mockUserProfileService.Object, mockHubContext.Object, Mock.Of<IRedisService>());

            notificationController.ControllerContext = context;

            NotificationDelete notificationDelete = new NotificationDelete();
            notificationDelete.id = 10;

            //Act
            IActionResult result = await notificationController.Delete(notificationDelete, Mock.Of<IConnectionMultiplexer>());
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestDeleteAllController()
        {
            //Arrange
            Mock<Service.INotificationService> mockUserProfileService = new Mock<Service.INotificationService>();
            Mock<IHubContext<ServerHub, IClientHub>> mockHubContext = new Mock<IHubContext<ServerHub, IClientHub>>();
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            Mock<IClientHub> clientHub = new Mock<IClientHub>();

            mockHubContext.Setup(x => x.Clients.Clients(It.IsAny<IReadOnlyList<string>>())).Returns(clientHub.Object);

            var context = new Microsoft.AspNetCore.Mvc.ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var notificationController = new NotificationController(mockUserProfileService.Object, mockHubContext.Object, Mock.Of<IRedisService>());

            notificationController.ControllerContext = context;
            //Act
            IActionResult result = await notificationController.DeleteAll(Mock.Of<IConnectionMultiplexer>());
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestUndeleteController()
        {
            //Arrange
            Mock<Service.INotificationService> mockUserProfileService = new Mock<Service.INotificationService>();

            var notificationController = new NotificationController(mockUserProfileService.Object, null, Mock.Of<IRedisService>());

            NotificationUndelete notificationUndelete = new NotificationUndelete();
            notificationUndelete.id = 10;

            //Act
            IActionResult result = await notificationController.Undelete(notificationUndelete);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestReadService()
        {
            //Arrange
            DbContextOptions<NotificationContext> options;
            var builder = new DbContextOptionsBuilder<NotificationContext>();
            builder.UseInMemoryDatabase("Notification");
            options = builder.Options;
            using NotificationContext dataContext = new NotificationContext(options);

            dataContext.Database.EnsureCreated();

            NotificationRecepientMedium app = new NotificationRecepientMedium()
            {
                Id = 9,
                SentTextJson = "test",
                DeliveryModeId = 1,
                NotificationRecepientId = 1
            };
            dataContext.Set<NotificationRecepientMedium>().Add(app);

            NotificationRecepient notificationRecepient = new NotificationRecepient()
            {
                Id = 1,
                StatusId = 6,
                RecipientId = 1,
                NotificationObjectId = 1
            };
            dataContext.Set<NotificationRecepient>().Add(notificationRecepient);

            NotificationObject notificationObject = new NotificationObject()
            {
                Id = 1,
                EntityId = 1
            };
            dataContext.Set<NotificationObject>().Add(notificationObject);

            dataContext.SaveChanges();

            INotificationService notificationService = new NotificationService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            await notificationService.Read(new List<long> { 9 }, 1);
        }
        [Fact]
        public async Task TestDeleteService()
        {
            //Arrange
            DbContextOptions<NotificationContext> options;
            var builder = new DbContextOptionsBuilder<NotificationContext>();
            builder.UseInMemoryDatabase("Notification");
            options = builder.Options;
            using NotificationContext dataContext = new NotificationContext(options);

            dataContext.Database.EnsureCreated();

            NotificationRecepientMedium app = new NotificationRecepientMedium()
            {
                Id = 2,
                SentTextJson = "test",
                DeliveryModeId = 1,
                NotificationRecepientId = 2
            };
            dataContext.Set<NotificationRecepientMedium>().Add(app);

            NotificationRecepient notificationRecepient = new NotificationRecepient()
            {
                Id = 2,
                StatusId = 5
            };
            dataContext.Set<NotificationRecepient>().Add(notificationRecepient);

            dataContext.SaveChanges();

            INotificationService notificationService = new NotificationService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            await notificationService.Delete(2);

        }
        [Fact]
        public async Task TestDeleteAllService()
        {
            //Arrange
            DbContextOptions<NotificationContext> options;
            var builder = new DbContextOptionsBuilder<NotificationContext>();
            builder.UseInMemoryDatabase("Notification");
            options = builder.Options;
            using NotificationContext dataContext = new NotificationContext(options);

            dataContext.Database.EnsureCreated();

            NotificationRecepientMedium app = new NotificationRecepientMedium()
            {
                Id = 3,
                SentTextJson = "test",
                DeliveryModeId = 1,
                NotificationRecepientId = 3
            };
            dataContext.Set<NotificationRecepientMedium>().Add(app);

            NotificationRecepient notificationRecepient = new NotificationRecepient()
            {
                Id = 3,
                StatusId = 4
            };
            dataContext.Set<NotificationRecepient>().Add(notificationRecepient);

            dataContext.SaveChanges();

            INotificationService notificationService = new NotificationService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            await notificationService.DeleteAll();
        }
        [Fact]
        public async Task TestGetPagedService()
        {
            //Arrange
            DbContextOptions<NotificationContext> options;
            var builder = new DbContextOptionsBuilder<NotificationContext>();
            builder.UseInMemoryDatabase("Notification");
            options = builder.Options;
            using NotificationContext dataContext = new NotificationContext(options);

            dataContext.Database.EnsureCreated();

            NotificationRecepientMedium app = new NotificationRecepientMedium()
            {
                Id = 4,
                SentTextJson = @"{   
                                'name':'Talha',  
                                'addess':'Street 99'  
                                }",
                DeliveryModeId = 1,
                NotificationRecepientId = 4,
                NotificationMediumid = 1
            };
            dataContext.Set<NotificationRecepientMedium>().Add(app);

            NotificationRecepient notificationRecepient = new NotificationRecepient()
            {
                Id = 4,
                StatusId = 4,
                RecipientId = 1
            };
            dataContext.Set<NotificationRecepient>().Add(notificationRecepient);

            StatusListEnum statusListEnum = new StatusListEnum()
            {
                Id = 4,
                Name = "Read"
            };
            dataContext.Set<StatusListEnum>().Add(statusListEnum);

            dataContext.SaveChanges();

            INotificationService notificationService = new NotificationService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            List<NotificationMediumModel> res = await notificationService.GetPaged(1, 6, 1, 1);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(4, res[0].id);
        }
        [Fact]
        public async Task TestUndeleteService()
        {
            //Arrange
            DbContextOptions<NotificationContext> options;
            var builder = new DbContextOptionsBuilder<NotificationContext>();
            builder.UseInMemoryDatabase("Notification");
            options = builder.Options;
            using NotificationContext dataContext = new NotificationContext(options);

            dataContext.Database.EnsureCreated();

            NotificationRecepientMedium app = new NotificationRecepientMedium()
            {
                Id = 5,
                SentTextJson = @"{   
                                'name':'Talha',  
                                'addess':'Street 99'  
                                }",
                DeliveryModeId = 1,
                NotificationRecepientId = 5,
                NotificationMediumid = 1
            };
            dataContext.Set<NotificationRecepientMedium>().Add(app);

            NotificationRecepient notificationRecepient = new NotificationRecepient()
            {
                Id = 5,
                StatusId = 3,
                RecipientId = 1
            };
            dataContext.Set<NotificationRecepient>().Add(notificationRecepient);

            NotificationRecepientStatusLog notificationRecepientStatusLogs = new NotificationRecepientStatusLog()
            {
                Id = 1,
                StatusId = 3,
                NotificationRecepientId = 5
            };
            dataContext.Set<NotificationRecepientStatusLog>().Add(notificationRecepientStatusLogs);

            StatusListEnum statusListEnum = new StatusListEnum()
            {
                Id = 3,
                Name = "Read"
            };
            dataContext.Set<StatusListEnum>().Add(statusListEnum);

            dataContext.SaveChanges();

            INotificationService notificationService = new NotificationService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            NotificationMediumModel res = await notificationService.Undelete(5);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(5, res.id);
        }
        [Fact]
        public async Task TestAddService()
        {
            //Arrange
            DbContextOptions<NotificationContext> options;
            var builder = new DbContextOptionsBuilder<NotificationContext>();
            builder.UseInMemoryDatabase("Notification");
            options = builder.Options;
            using NotificationContext dataContext = new NotificationContext(options);

            dataContext.Database.EnsureCreated();

            dataContext.SaveChanges();

            Mock<IRainmakerService> mockRainmakerService = new Mock<IRainmakerService>();
            List<int> lstAssignedUsers = new List<int>();
            lstAssignedUsers.Add(500);
            mockRainmakerService.Setup(x => x.GetAssignedUsers(It.IsAny<int>())).ReturnsAsync(lstAssignedUsers);

            INotificationService notificationService = new NotificationService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, mockRainmakerService.Object, Mock.Of<ITemplateService>());

            //Act
            NotificationModel notificationModel = new NotificationModel();
            notificationModel.NotificationType = 2;
            notificationModel.EntityId = 500;
            notificationModel.CustomTextJson = "";
            notificationModel.tenantId = 55;
            notificationModel.userId = 6308;
            long res = await notificationService.Add(notificationModel);

            // Assert
            Assert.NotNull(res);
        }
        [Fact]
        public async Task TestSeenController()
        {
            //Arrange
            Mock<Service.INotificationService> mockUserProfileService = new Mock<Service.INotificationService>();

            Mock<IHubContext<ServerHub, IClientHub>> mockHubContext = new Mock<IHubContext<ServerHub, IClientHub>>();
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            Mock<IClientHub> clientHub = new Mock<IClientHub>();

            mockHubContext.Setup(x => x.Clients.Clients(It.IsAny<IReadOnlyList<string>>())).Returns(clientHub.Object);

            var context = new Microsoft.AspNetCore.Mvc.ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var notificationController = new NotificationController(mockUserProfileService.Object, mockHubContext.Object, Mock.Of<IRedisService>());

            notificationController.ControllerContext = context;

            NotificationSeen notificationSeen = new NotificationSeen();
            List<long> ids = new List<long>();
            ids.Add(11);
            notificationSeen.ids = ids;

            //Act
            IActionResult result = await notificationController.Seen(notificationSeen, Mock.Of<IConnectionMultiplexer>());
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestSeenService()
        {
            //Arrange
            DbContextOptions<NotificationContext> options;
            var builder = new DbContextOptionsBuilder<NotificationContext>();
            builder.UseInMemoryDatabase("Notification");
            options = builder.Options;
            using NotificationContext dataContext = new NotificationContext(options);

            dataContext.Database.EnsureCreated();

            NotificationRecepientMedium app = new NotificationRecepientMedium()
            {
                Id = 6,
                SentTextJson = @"{   
                                'name':'Talha',  
                                'addess':'Street 99'  
                                }",
                DeliveryModeId = 1,
                NotificationRecepientId = 6,
                NotificationMediumid = 1,
            };
            dataContext.Set<NotificationRecepientMedium>().Add(app);

            NotificationRecepient notificationRecepient = new NotificationRecepient()
            {
                Id = 6,
                StatusId = 1,
                RecipientId = 1
            };
            dataContext.Set<NotificationRecepient>().Add(notificationRecepient);

            NotificationRecepientStatusLog notificationRecepientStatusLogs = new NotificationRecepientStatusLog()
            {
                Id = 3,
                StatusId = 1,
                UpdatedOn = new DateTime(),
                NotificationRecepientId = 6
            };
            dataContext.Set<NotificationRecepientStatusLog>().Add(notificationRecepientStatusLogs);

            NotificationRecepientStatusLog notificationRecepientStatusLogs2 = new NotificationRecepientStatusLog()
            {
                Id = 4,
                StatusId = 1,
                UpdatedOn = new DateTime(),
                NotificationRecepientId = 6
            };
            dataContext.Set<NotificationRecepientStatusLog>().Add(notificationRecepientStatusLogs2);

            StatusListEnum statusListEnum = new StatusListEnum()
            {
                Id = 1,
                Name = "Created",
            };
            dataContext.Set<StatusListEnum>().Add(statusListEnum);

            dataContext.SaveChanges();

            INotificationService notificationService = new NotificationService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            List<long> ids = new List<long>();
            ids.Add(6);
            await notificationService.Seen(ids);
        }
        [Fact]
        public async Task TestSeenCountService()
        {
            //Arrange
            DbContextOptions<NotificationContext> options;
            var builder = new DbContextOptionsBuilder<NotificationContext>();
            builder.UseInMemoryDatabase("Notification");
            options = builder.Options;
            using NotificationContext dataContext = new NotificationContext(options);

            dataContext.Database.EnsureCreated();

            NotificationRecepientMedium app = new NotificationRecepientMedium()
            {
                Id = 11,
                SentTextJson = @"{   
                                'name':'Talha',  
                                'addess':'Street 99'  
                                }",
                DeliveryModeId = 1,
                NotificationRecepientId = 11,
                NotificationMediumid = 1,
            };
            dataContext.Set<NotificationRecepientMedium>().Add(app);

            NotificationRecepient notificationRecepient = new NotificationRecepient()
            {
                Id = 11,
                StatusId = 2,
                RecipientId = 1
            };
            dataContext.Set<NotificationRecepient>().Add(notificationRecepient);

            NotificationRecepientStatusLog notificationRecepientStatusLogs = new NotificationRecepientStatusLog()
            {
                Id = 5,
                StatusId = 2,
                UpdatedOn = new DateTime(),
                NotificationRecepientId = 11
            };
            dataContext.Set<NotificationRecepientStatusLog>().Add(notificationRecepientStatusLogs);

            StatusListEnum statusListEnum = new StatusListEnum()
            {
                Id = 2,
                Name = "Delivered",
            };
            dataContext.Set<StatusListEnum>().Add(statusListEnum);

            dataContext.SaveChanges();

            INotificationService notificationService = new NotificationService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            List<long> ids = new List<long>();
            ids.Add(11);
            await notificationService.Seen(ids);
        }
        [Fact]
        public async Task TestGetCountController()
        {
            //Arrange
            Mock<Service.INotificationService> mockUserProfileService = new Mock<Service.INotificationService>();

            var notificationController = new NotificationController(mockUserProfileService.Object, null, Mock.Of<IRedisService>());

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new Microsoft.AspNetCore.Mvc.ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            notificationController.ControllerContext = context;

            //Act
            IActionResult result = await notificationController.GetCount();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestGetCountService()
        {
            //Arrange
            DbContextOptions<NotificationContext> options;
            var builder = new DbContextOptionsBuilder<NotificationContext>();
            builder.UseInMemoryDatabase("Notification");
            options = builder.Options;
            using NotificationContext dataContext = new NotificationContext(options);

            dataContext.Database.EnsureCreated();

            NotificationRecepient notificationRecepient = new NotificationRecepient()
            {
                Id = 7,
                StatusId = 6
            };
            dataContext.Set<NotificationRecepient>().Add(notificationRecepient);

            dataContext.SaveChanges();

            INotificationService notificationService = new NotificationService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            await notificationService.GetCount(1);
        }
        [Fact]
        public async Task TestGetTenantSettingController()
        {
            //Arrange
            Mock<Service.INotificationService> mockUserProfileService = new Mock<Service.INotificationService>();

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new Microsoft.AspNetCore.Mvc.ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            TenantSettingModel tenantSettingModel = new TenantSettingModel();
            tenantSettingModel.deliveryModeId = 1;

            mockUserProfileService.Setup(x => x.GetTenantSetting(It.IsAny<int>())).ReturnsAsync(tenantSettingModel);

            var notificationController = new NotificationController(mockUserProfileService.Object, null, Mock.Of<IRedisService>());

            notificationController.ControllerContext = context;

            //Act
            IActionResult result = await notificationController.GetTenantSetting();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestGetTenantSettingService()
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
                Id = 49,
                NotificationTypeId = 3
            };
            dataContext.Set<TenantSetting>().Add(app);

            dataContext.SaveChanges();

            INotificationService notificationService = new NotificationService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            await notificationService.GetTenantSetting(49, 3);
        }
        [Fact]
        public async Task TestSetTenantSettingController()
        {
            //Arrange
            Mock<Service.INotificationService> mockUserProfileService = new Mock<Service.INotificationService>();

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new Microsoft.AspNetCore.Mvc.ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            mockUserProfileService.Setup(x => x.SetTenantSetting(It.IsAny<int>(), It.IsAny<TenantSettingModel>()));

            var notificationController = new NotificationController(mockUserProfileService.Object, null, Mock.Of<IRedisService>());

            notificationController.ControllerContext = context;

            //Act
            TenantSettingModel tenantSettingModel = new TenantSettingModel();
            tenantSettingModel.deliveryModeId = 1;
            IActionResult result = await notificationController.SetTenantSetting(tenantSettingModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestSetTenantSettingService()
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
                Id = 4,
                TrackingState = TrackingState.Modified,
                DeliveryModeId = 1,
                TenantId = 7
            };
            dataContext.Set<TenantSetting>().Add(app);

            TenantSetting setting = new TenantSetting()
            {
                TenantId = 7
            };
            dataContext.Set<TenantSetting>().Add(setting);

            dataContext.SaveChanges();

            INotificationService notificationService = new NotificationService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            TenantSettingModel tenantSettingModel = new TenantSettingModel();
            tenantSettingModel.deliveryModeId = 1;
            await notificationService.SetTenantSetting(7, tenantSettingModel);
        }
        [Fact]
        public async Task TestGetRecepientService()
        {
            //Arrange
            DbContextOptions<NotificationContext> options;
            var builder = new DbContextOptionsBuilder<NotificationContext>();
            builder.UseInMemoryDatabase("Notification");
            options = builder.Options;
            using NotificationContext dataContext = new NotificationContext(options);

            dataContext.Database.EnsureCreated();

            NotificationRecepient notificationRecepient = new NotificationRecepient()
            {
                Id = 10021,
                NotificationObjectId = 40247,
                RecipientId = 21,
                StatusId = 6
            };
            dataContext.Set<NotificationRecepient>().Add(notificationRecepient);

            dataContext.SaveChanges();

            INotificationService notificationService = new NotificationService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            await notificationService.GetRecepient(10021);
        }
        [Fact]
        public async Task TestGetTenantSettingOverloadService()
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
                Id = 111,
                UserId = 111,
                TenantId = 59,
                DeliveryModeId = 2,
                NotificationMediumId = 1,
                NotificationTypeId = 1,
                DelayedInterval = 25
            };
            dataContext.Set<TenantSetting>().Add(app);

            dataContext.SaveChanges();

            INotificationService notificationService = new NotificationService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            TenantSettingModel tenantSetting = await notificationService.GetTenantSetting(59);

            //Assert
            Assert.NotNull(tenantSetting);
        }
        [Fact]
        public async Task TestGetSettingService()
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
                Id = 61,
                DeliveryModeId = 2,
                DelayedInterval = 5
            };
            dataContext.Set<TenantSetting>().Add(app);

            dataContext.SaveChanges();

            INotificationService notificationService = new NotificationService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            await notificationService.GetSetting(61);
        }
        [Fact]
        public async Task TestGetByIdForTemplateService()
        {
            //Arrange
            DbContextOptions<NotificationContext> options;
            var builder = new DbContextOptionsBuilder<NotificationContext>();
            builder.UseInMemoryDatabase("Notification");
            options = builder.Options;
            using NotificationContext dataContext = new NotificationContext(options);

            dataContext.Database.EnsureCreated();

            NotificationObject notificationObject = new NotificationObject()
            {
                Id = 40246
            };
            dataContext.Set<NotificationObject>().Add(notificationObject);

            Entity.Models.NotificationType notificationType = new Entity.Models.NotificationType()
            {
                Id = 3
            };
            dataContext.Set<Entity.Models.NotificationType>().Add(notificationType);

            dataContext.SaveChanges();

            INotificationService notificationService = new NotificationService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            await notificationService.GetByIdForTemplate(40246);
        }
        [Fact]
        public async Task TestAddUserNotificationMediumService()
        {
            //Arrange
            DbContextOptions<NotificationContext> options;
            var builder = new DbContextOptionsBuilder<NotificationContext>();
            builder.UseInMemoryDatabase("Notification");
            options = builder.Options;
            using NotificationContext dataContext = new NotificationContext(options);

            dataContext.Database.EnsureCreated();

            dataContext.SaveChanges();

            INotificationService notificationService = new NotificationService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, Mock.Of<ITemplateService>());

            //Act
            long res = await notificationService.AddUserNotificationMedium(6326, 40244, 1, 1, 1);

            // Assert
            Assert.NotNull(res);
        }
        //[Fact]
        //public async Task TestPollAndSendNotification()
        //{
        //    var hubContext = new Mock<IHubContext<ServerHub, IClientHub>>();
        //    var clients = new Mock<IHubClients<IClientHub>>();
        //    var client = new Mock<IClientHub>();
        //    client.Setup(x => x.SendNotification(It.IsAny<string>())).Verifiable();
        //    clients.Setup(x => x.Clients(It.IsAny<IReadOnlyList<string>>())).Returns(client.Object);
        //    hubContext.SetupGet(x => x.Clients).Returns(clients.Object);
        //    var serviceProvider = new Mock<IServiceProvider>();
        //    var notificationService = new Mock<INotificationService>();
        //    serviceProvider
        //        .Setup(x => x.GetService(typeof(INotificationService)))
        //        .Returns(notificationService.Object);
        //    serviceProvider
        //        .Setup(x => x.GetService(typeof(IHubContext<ServerHub, IClientHub>)))
        //        .Returns(hubContext.Object);
        //    var configurationService = new Mock<IConfiguration>();
        //    serviceProvider
        //        .Setup(x => x.GetService(typeof(IConfiguration)))
        //        .Returns(configurationService.Object);
        //    var settingService = new Mock<ISettingService>();
        //    serviceProvider
        //        .Setup(x => x.GetService(typeof(ISettingService)))
        //        .Returns(settingService.Object);
        //    var redisService = new Mock<IConnectionMultiplexer>();
        //    serviceProvider
        //        .Setup(x => x.GetService(typeof(IConnectionMultiplexer)))
        //        .Returns(redisService.Object);
        //    var dbProvider = new Mock<IDatabase>();
        //    redisService.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(dbProvider.Object);
        //    var serviceScope = new Mock<IServiceScope>();
        //    serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

        //    var serviceScopeFactory = new Mock<IServiceScopeFactory>();
        //    serviceScopeFactory
        //        .Setup(x => x.CreateScope())
        //        .Returns(serviceScope.Object);

        //    serviceProvider
        //        .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
        //        .Returns(serviceScopeFactory.Object);
        //    var service = new RedisService(serviceProvider.Object);

        //    NotificationModel model = new NotificationModel();
        //    model.UsersToSendList = new List<int>() { 1, 2, 3, 4 };
        //    model.DateTime = DateTime.UtcNow.AddHours(-1);
        //    model.NotificationType = (int)Common.NotificationType.DocumentSubmission;
        //    model.tenantId = 1;
        //    notificationService.Setup(x => x.AddUserNotificationMedium(It.IsAny<int>(), It.IsAny<long>(), It.IsAny<short>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(1);
        //    notificationService.Setup(x => x.GetRecepient(It.IsAny<long>())).ReturnsAsync(new NotificationRecepient() 
        //    {
        //        NotificationRecepientMediums = new List<NotificationRecepientMedium>() { new NotificationRecepientMedium() { } },
        //        StatusListEnum = new StatusListEnum { },
        //        RecipientId=1
        //    });
        //    dbProvider.Setup(x => x.ListLengthAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>())).ReturnsAsync(1);
        //    dbProvider.Setup(x => x.ListGetByIndexAsync(It.IsAny<RedisKey>(), It.IsAny<long>(), It.IsAny<CommandFlags>())).ReturnsAsync(Newtonsoft.Json.JsonConvert.SerializeObject(model));
        //    dbProvider.Setup(x => x.ListRemoveAsync(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), It.IsAny<long>(), It.IsAny<CommandFlags>())).Verifiable();
        //    settingService.SetupSequence(x => x.GetSettings(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<SettingModel>() { })
        //        .ReturnsAsync(new List<SettingModel>() { new SettingModel() { DeliveryModeId = (short)DeliveryMode.Off, NotificationTypeId = (int)Common.NotificationType.DocumentSubmission } })
        //        .ReturnsAsync(new List<SettingModel>() { new SettingModel() { DeliveryModeId = (short)DeliveryMode.Express, NotificationTypeId = (int)Common.NotificationType.DocumentSubmission } })
        //        .ReturnsAsync(new List<SettingModel>() { { new SettingModel() { DeliveryModeId = (short)DeliveryMode.Queued, DelayedInterval=0, NotificationTypeId = (int)Common.NotificationType.DocumentSubmission } } });
        //    await service.PollAndSendNotification();
        //}

        [Fact]
        public async Task TestInsertInCache()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            var configurationService = new Mock<IConfiguration>();
            serviceProvider
                .Setup(x => x.GetService(typeof(IConfiguration)))
                .Returns(configurationService.Object);
            var redisService = new Mock<IConnectionMultiplexer>();
            serviceProvider
                .Setup(x => x.GetService(typeof(IConnectionMultiplexer)))
                .Returns(redisService.Object);
            var dbProvider = new Mock<IDatabase>();
            redisService.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(dbProvider.Object);
            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactory.Object);

            NotificationModel model = new NotificationModel();
            model.UsersToSendList = new List<int>() { 1, 2, 3, 4 };
            model.DateTime = DateTime.UtcNow.AddHours(-1);
            model.NotificationType = (int)Common.NotificationType.DocumentSubmission;
            model.tenantId = 1;

            dbProvider.Setup(x => x.ListLengthAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>())).ReturnsAsync(1);
            dbProvider.Setup(x => x.ListGetByIndexAsync(It.IsAny<RedisKey>(), It.IsAny<long>(), It.IsAny<CommandFlags>())).ReturnsAsync(Newtonsoft.Json.JsonConvert.SerializeObject(model));
            dbProvider.Setup(x => x.ListRemoveAsync(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), It.IsAny<long>(), It.IsAny<CommandFlags>())).Verifiable();
            dbProvider.Setup(x => x.ListRightPushAsync(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), It.IsAny<When>(), It.IsAny<CommandFlags>())).Verifiable();

            var service = new RedisService(serviceProvider.Object);
            await service.InsertInCache(model);
        }
    }
}
