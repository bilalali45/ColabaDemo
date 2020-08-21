using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Moq;
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
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace Notification.Tests
{
    public class NotificationTest
    {
        [Fact]
        public async Task TestAddController()
        {
            //Arrange
            Mock<Service.INotificationService> mockUserProfileService = new Mock<Service.INotificationService>();

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
            IActionResult result = await notificationController.GetPaged(1, -1, 1);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestReadController()
        {
            //Arrange
            Mock<Service.INotificationService> mockUserProfileService = new Mock<Service.INotificationService>();

            var notificationController = new NotificationController(mockUserProfileService.Object, null, Mock.Of<IRedisService>());

            NotificationRead notificationRead = new NotificationRead();
            notificationRead.ids = new List<long> {10};

            //Act
            IActionResult result = await notificationController.Read(notificationRead);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestDeleteController()
        {
            //Arrange
            Mock<Service.INotificationService> mockUserProfileService = new Mock<Service.INotificationService>();

            var notificationController = new NotificationController(mockUserProfileService.Object, null, Mock.Of<IRedisService>());

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

            var notificationController = new NotificationController(mockUserProfileService.Object, null, Mock.Of<IRedisService>());

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
                StatusId = 4
            };
            dataContext.Set<NotificationRecepient>().Add(notificationRecepient);

            dataContext.SaveChanges();

            INotificationService notificationService = new NotificationService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            await notificationService.Read(new List<long>{1});
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
                StatusId = 4,
                RecipientId = 1
            };
            dataContext.Set<NotificationRecepient>().Add(notificationRecepient);

            NotificationRecepientStatusLog notificationRecepientStatusLogs = new NotificationRecepientStatusLog()
            {
                Id = 1,
                StatusId = 4
            };
            dataContext.Set<NotificationRecepientStatusLog>().Add(notificationRecepientStatusLogs);

            StatusListEnum statusListEnum = new StatusListEnum()
            {
                Id = 4,
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
        //[Fact]
        //public async Task TestAddService()
        //{
        //    //Arrange
        //    DbContextOptions<NotificationContext> options;
        //    var builder = new DbContextOptionsBuilder<NotificationContext>();
        //    builder.UseInMemoryDatabase("Notification");
        //    options = builder.Options;
        //    using NotificationContext dataContext = new NotificationContext(options);

        //    dataContext.Database.EnsureCreated();

        //    TenantSetting tenantSetting = new TenantSetting()
        //    {
        //        TenantId = 1,
        //        NotificationTypeId = 1

        //    };
        //    dataContext.Set<TenantSetting>().Add(tenantSetting);

        //    //NotificationRecepientMedium app = new NotificationRecepientMedium()
        //    //{
        //    //    Id = 6,
        //    //    SentTextJson = @"{   
        //    //                    'name':'Talha',  
        //    //                    'addess':'Street 99'  
        //    //                    }",
        //    //    DeliveryModeId = 1,
        //    //    NotificationRecepientId = 6,
        //    //    NotificationMediumid = 1
        //    //};
        //    //dataContext.Set<NotificationRecepientMedium>().Add(app);

        //    //NotificationRecepient notificationRecepient = new NotificationRecepient()
        //    //{
        //    //    Id = 6,
        //    //    StatusId = 4,
        //    //    RecipientId = 1
        //    //};
        //    //dataContext.Set<NotificationRecepient>().Add(notificationRecepient);

        //    //StatusListEnum statusListEnum = new StatusListEnum()//    //{
        //    //    Id = 4,
        //    //    Name = "Read"
        //    //};

        //    dataContext.SaveChanges();

        //    INotificationService notificationService = new NotificationService(new UnitOfWork<NotificationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

        //    //Act
        //    NotificationModel notificationModel = new NotificationModel();
        //    notificationModel.NotificationType = 1;
        //    notificationModel.EntityId = 1;
        //    notificationModel.CustomTextJson = "";

        //    List<string> authorization = new List<string>();
        //    authorization.Add("Authorization");
        //    long res = await notificationService.Add(notificationModel, 1, 1, authorization);

        //    // Assert
        //    Assert.NotNull(res);
        //    //Assert.Equal(4, res);
        //}
    }
}
