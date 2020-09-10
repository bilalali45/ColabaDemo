using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Moq;
using Notification.API;
using Notification.API.Controllers;
using Notification.Data;
using Notification.Entity.Models;
using Notification.Model;
using Notification.Service;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
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
            var notificationController = new NotificationController(mockUserProfileService.Object,null,Mock.Of<IRedisService>());

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
            notificationRead.ids = new List<long> {10};

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            List<long> lstRead = new List<long>();
            lstRead.Add(1);

            mockUserProfileService.Setup(x => x.Read(It.IsAny<List<long>>(), It.IsAny<int>())).ReturnsAsync(lstRead);

            mockHubContext.Setup(x => x.Clients.Clients(It.IsAny<IReadOnlyList<string>>())).Returns(clientHub.Object);

            var context = new Microsoft.AspNetCore.Mvc.ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            notificationController.ControllerContext = context;

            //Act
            IActionResult result = await notificationController.Read(notificationRead);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestDeleteController()
        {
            //Arrange
            Mock<Service.INotificationService> mockUserProfileService = new Mock<Service.INotificationService>();
            Mock<IHubContext<ServerHub,IClientHub>> mockHubContext = new Mock<IHubContext<ServerHub,IClientHub>>();
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
            IActionResult result = await notificationController.Delete(notificationDelete);
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
            IActionResult result = await notificationController.DeleteAll();
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
                Id = 1,
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
            await notificationService.Read(new List<long>{1},1);
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
            List<NotificationMediumModel> res = await notificationService.GetPaged(1,6,1,1);

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

            TenantSetting tenantSetting = new TenantSetting()
            {
                TenantId = 1,
                NotificationTypeId = 1

            };
            dataContext.Set<TenantSetting>().Add(tenantSetting);

            dataContext.SaveChanges();

            Mock<IRainmakerService> mockRainmakerService = new Mock<IRainmakerService>();
            List<int> lstAssignedUsers = new List<int>();
            lstAssignedUsers.Add(1);
            mockRainmakerService.Setup(x => x.GetAssignedUsers(It.IsAny<int>())).ReturnsAsync(lstAssignedUsers);

            INotificationService notificationService = new NotificationService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, mockRainmakerService.Object, Mock.Of<ITemplateService>());

            //Act
            NotificationModel notificationModel = new NotificationModel();
            notificationModel.NotificationType = 1;
            notificationModel.EntityId = 1;
            notificationModel.CustomTextJson = "";

            TenantSetting model = new TenantSetting();
            model.DeliveryModeId = (short)Notification.Common.DeliveryMode.Express;
            model.NotificationMediumId = 1;
            long res = await notificationService.Add(notificationModel, 1, 1, model);

            // Assert
            Assert.Equal(2,res);
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
            IActionResult result = await notificationController.Seen(notificationSeen);
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

            mockUserProfileService.Setup(x=>x.GetTenantSetting(It.IsAny<int>())).ReturnsAsync(tenantSettingModel);

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
                Id = 1,
                NotificationTypeId = 1
            };
            dataContext.Set<TenantSetting>().Add(app);

            dataContext.SaveChanges();

            INotificationService notificationService = new NotificationService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            await notificationService.GetTenantSetting(1 , 1);
        }
        [Fact]
        public async Task TestSetTenantSettingController()
        {
            //Arrange
            Mock<Service.INotificationService> mockUserProfileService = new Mock<Service.INotificationService>();

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new Microsoft.AspNetCore.Mvc.ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            mockUserProfileService.Setup(x => x.SetTenantSetting(It.IsAny<int>(),It.IsAny<TenantSettingModel>()));

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
                Id = 2,
                TrackingState = TrackingState.Modified,
                DeliveryModeId = 1,
                TenantId = 6
            };
            dataContext.Set<TenantSetting>().Add(app);

            Setting setting = new Setting()
            {
                TenantId = 6
            };
            dataContext.Set<Setting>().Add(setting);

            dataContext.SaveChanges();

            INotificationService notificationService = new NotificationService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            TenantSettingModel tenantSettingModel = new TenantSettingModel();
            tenantSettingModel.deliveryModeId = 1;
            await notificationService.SetTenantSetting(6, tenantSettingModel);
        }
       
    }
}
