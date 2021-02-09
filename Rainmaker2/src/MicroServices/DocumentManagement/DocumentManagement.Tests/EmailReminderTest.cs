using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DocumentManagement.API.Controllers;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using Moq.Protected;
using Xunit;

namespace DocumentManagement.Tests
{
    public class EmailReminderTest
    {
        [Fact]
        public async Task TestGetEmailRemindersController()
        {
            Mock<IEmailReminderService> mock = new Mock<IEmailReminderService>();
            EmailReminderModel emailReminderModel = new EmailReminderModel();
            mock.Setup(x => x.GetEmailReminders(It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(emailReminderModel);
            var emailReminderController = new EmailReminderController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;

            IActionResult result = await emailReminderController.GetEmailReminders();
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestGetDocumentStatusByLoanIdsController()
        {
            Mock<IEmailReminderService> mock = new Mock<IEmailReminderService>();
            List<RemainingDocuments> remainingDocuments = new List<RemainingDocuments>();
            mock.Setup(x => x.GetDocumentStatusByLoanIds(It.IsAny<List<RemainingDocuments>>())).ReturnsAsync(remainingDocuments);
            var emailReminderController = new EmailReminderController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;
            RemainingDocumentsModel remainingDocumentsModel = new RemainingDocumentsModel();
            IActionResult result = await emailReminderController.GetDocumentStatusByLoanIds(remainingDocumentsModel);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestGetEmailReminderByIdController()
        {
            Mock<IEmailReminderService> mock = new Mock<IEmailReminderService>();
            EmailReminder emailReminder = new EmailReminder();
            mock.Setup(x => x.GetEmailReminderById(It.IsAny<string>())).ReturnsAsync(emailReminder);
            var emailReminderController = new EmailReminderController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;
            EmailReminderByIdModel emailReminderModel = new EmailReminderByIdModel();
            IActionResult result = await emailReminderController.GetEmailReminderById(emailReminderModel);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestAddEmailReminderController()
        {
            Mock<IEmailReminderService> mock = new Mock<IEmailReminderService>();
            mock.Setup(x => x.AddEmailReminder(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync("");
            var emailReminderController = new EmailReminderController(mock.Object);

            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;
            AddEmailReminder addEmailReminderModel = new AddEmailReminder()
            {
                email = new List<Email>()
                {
                    new Email()
                    {
                        subject ="subject",
                        emailBody ="body",
                        CCAddress= "sasas",
                    }
                }
            };
            IActionResult result = await emailReminderController.AddEmailReminder(addEmailReminderModel);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestUpdateEmailReminderController()
        {
            Mock<IEmailReminderService> mock = new Mock<IEmailReminderService>();
            mock.Setup(x => x.UpdateEmailReminder(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(true);
            var emailReminderController = new EmailReminderController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(new StringValues("Test"));
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;
            UpdateEmailReminder updateEmailReminderModel = new UpdateEmailReminder()
            {
                email = new List<Email>()
                {
                    new Email()
                    {
                        subject ="subject",
                        emailBody ="body",
                        CCAddress= "sasas",
                    }
                }
            };
            IActionResult result = await emailReminderController.UpdateEmailReminder(updateEmailReminderModel);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestEnableDisableEmailReminderController()
        {
            Mock<IEmailReminderService> mock = new Mock<IEmailReminderService>();
            mock.Setup(x => x.EnableDisableEmailReminder(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(true);
            var emailReminderController = new EmailReminderController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(new StringValues("Test"));
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;
            EnableDisableEmailReminderModel enableDisableEmailReminderModel = new EnableDisableEmailReminderModel();
            IActionResult result = await emailReminderController.EnableDisableEmailReminder(enableDisableEmailReminderModel);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestEnableDisableEmailReminderNotFoundController()
        {
            Mock<IEmailReminderService> mock = new Mock<IEmailReminderService>();
            mock.Setup(x => x.EnableDisableEmailReminder(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(false);
            var emailReminderController = new EmailReminderController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(new StringValues("Test"));
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;
            EnableDisableEmailReminderModel enableDisableEmailReminderModel = new EnableDisableEmailReminderModel();
            IActionResult result = await emailReminderController.EnableDisableEmailReminder(enableDisableEmailReminderModel);
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task TestEnableDisableAllEmailRemindersController()
        {
            Mock<IEmailReminderService> mock = new Mock<IEmailReminderService>();
            mock.Setup(x => x.EnableDisableAllEmailReminders(It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IEnumerable<string>>()));
            var emailReminderController = new EmailReminderController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(new StringValues("Test"));
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;
            EnableDisableAllEmailReminderModel enableDisableAllEmailReminderModel = new EnableDisableAllEmailReminderModel();
            IActionResult result = await emailReminderController.EnableDisableAllEmailReminders(enableDisableAllEmailReminderModel);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestGetEmailReminderByIdsController()
        {
            Mock<IEmailReminderService> mock = new Mock<IEmailReminderService>();
            List<Model.EmailReminder> emailReminders = new List<EmailReminder>();
            mock.Setup(x => x.GetEmailReminderByIds(It.IsAny<List<string>>(), It.IsAny<int>())).ReturnsAsync(emailReminders);
            var emailReminderController = new EmailReminderController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(new StringValues("Test"));
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;
            GetEmailRemidersByIds getEmailRemidersByIdsModel = new GetEmailRemidersByIds()
            {
                id = new string[] { "1", "2" }

            };
            IActionResult result = await emailReminderController.GetEmailReminderByIds(getEmailRemidersByIdsModel);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestDeleteEmailReminderController()
        {
            Mock<IEmailReminderService> mock = new Mock<IEmailReminderService>();
            mock.Setup(x => x.DeleteEmailReminder(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(true);
            var emailReminderController = new EmailReminderController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(new StringValues("Test"));
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;
            DeleteEmailReminderModel deleteEmailReminderModelModel = new DeleteEmailReminderModel();
            IActionResult result = await emailReminderController.DeleteEmailReminder(deleteEmailReminderModelModel);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestDeleteEmailReminderNotFoundController()
        {
            Mock<IEmailReminderService> mock = new Mock<IEmailReminderService>();
            mock.Setup(x => x.DeleteEmailReminder(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(false);
            var emailReminderController = new EmailReminderController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(new StringValues("Test"));
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;
            DeleteEmailReminderModel deleteEmailReminderModelModel = new DeleteEmailReminderModel();
            IActionResult result = await emailReminderController.DeleteEmailReminder(deleteEmailReminderModelModel);
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }
        #region Services 
        [Fact]
        public async Task TestGetEmailRemindersService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.EmailReminder>> mockCollection = new Mock<IMongoCollection<Entity.EmailReminder>>();

            Mock<IAsyncCursor<BsonDocument>> mockCursorEmailReminder = new Mock<IAsyncCursor<BsonDocument>>();

            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            BsonDocument email = new BsonDocument() {
                new BsonDocument
                {
                     { "_id" , "5fa0202d4c2ff92af0a1c861"},
                     { "fromAddress" , "from@gmail.com"},
                     { "ccAddress" , "cc@gmail.com"},
                     { "subject" , "Texas"},
                     { "emailBody" , "<p>Its number 2</p>\n" }
                 }
            };

            List<BsonDocument> emailReminder = new List<BsonDocument>()
            {
                new BsonDocument
                {
                     { "_id" , "5fa0202d4c2ff92af0a1c860"},
                     { "noOfDays" , 10},
                     { "recurringTime" , "2021-01-19T10:00:46.000Z"},
                     { "isActive" , true},
                     { "email" , email}
                 }
            };

            mockCursorEmailReminder.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorEmailReminder.SetupGet(x => x.Current).Returns(emailReminder);

            mockCollection.SetupSequence(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.EmailReminder, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorEmailReminder.Object);

            mockdb.SetupSequence(x => x.GetCollection<Entity.EmailReminder>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            mockconfiguration.Setup(x => x["Setting:Url"]).Returns("http://test.com");

            var userProfile = new
            {
                Id = 1,
                UserName = "rainsoft"
            };

            var jobType = new
            {
                id =1,
                name ="Email Reminder",
                isActive =true
            };

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(jobType), Encoding.UTF8, "application/json")
               })
               .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var service = new EmailReminderService(mock.Object, httpClient, mockconfiguration.Object);

            //Act
            List<string> emailReminderIds = new List<string>();
            emailReminderIds.Add("5eb25d1fe519051af2eeb72d");
            emailReminderIds.Add("5fa0202d4c2ff92af0a1c860");
            IEnumerable<string> authHeader = new string[] { "Authorization" };
            Model.EmailReminderModel dto = await service.GetEmailReminders(1, authHeader);

            //Assert
            Assert.NotNull(dto);
            Assert.Equal("5fa0202d4c2ff92af0a1c860", dto.emailReminders[0].id);
            Assert.Equal(10, dto.emailReminders[0].noOfDays);
            Assert.Equal(true, dto.isActive);
        }

        [Fact]
        public async Task TestAddEmailReminderService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.EmailReminder>> mockCollection = new Mock<IMongoCollection<Entity.EmailReminder>>();

            mockdb.Setup(x => x.GetCollection<Entity.EmailReminder>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(s => s.InsertOneAsync(It.IsAny<Entity.EmailReminder>(), It.IsAny<InsertOneOptions>(), It.IsAny<System.Threading.CancellationToken>()));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act
            IEmailReminderService emailReminderService = new EmailReminderService(mock.Object, null, null);

            await emailReminderService.AddEmailReminder(1, 2, DateTime.Now, "###RequestorUserEmail###", "###Co-BorrowerEmailAddress###", "###BusinessUnitName###", "<p>ddddd</p>\n", 1);
        }

        [Fact]
        public async Task TestDeleteEmailReminderService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.EmailReminder>> mockCollection = new Mock<IMongoCollection<Entity.EmailReminder>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();

            mockdb.Setup(x => x.GetCollection<Entity.EmailReminder>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Entity.EmailReminder>>(), It.IsAny<UpdateDefinition<Entity.EmailReminder>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            mockconfiguration.Setup(x => x["Setting:Url"]).Returns("http://test.com");

            var userProfile = new
            {
                Id = 1,
                UserName = "rainsoft"
            };

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(userProfile), Encoding.UTF8, "application/json")
               })
               .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            //Act
            IEnumerable<string> authHeader = new string[] { "Authorization" };
            IEmailReminderService emailReminderService = new EmailReminderService(mock.Object, httpClient, mockconfiguration.Object);
            bool result = await emailReminderService.DeleteEmailReminder("5fa020214c2ff92af0a1c85f", 1, authHeader);

            //Assert
            Assert.True(result);
        }
        [Fact]
        public async Task TestUpdateEmailReminderService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.EmailReminder>> mockCollection = new Mock<IMongoCollection<Entity.EmailReminder>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();

            mockdb.Setup(x => x.GetCollection<Entity.EmailReminder>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Entity.EmailReminder>>(), It.IsAny<UpdateDefinition<Entity.EmailReminder>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            mockconfiguration.Setup(x => x["Setting:Url"]).Returns("http://test.com");

            var userProfile = new
            {
                Id = 1,
                UserName = "rainsoft"
            };

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(userProfile), Encoding.UTF8, "application/json")
               })
               .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            //Act
            IEnumerable<string> authHeader = new string[] { "Authorization" };
            IEmailReminderService emailReminderService = new EmailReminderService(mock.Object, httpClient, mockconfiguration.Object);
            bool result = await emailReminderService.UpdateEmailReminder("5fa020214c2ff92af0a1c85f", 2, DateTime.Now, "###RequestorUserEmail###", "###Co-BorrowerEmailAddress###", "###BusinessUnitName###", "<p>ddddd</p>\n", 1, authHeader);

            //Assert
            Assert.True(result);
        }
        [Fact]
        public async Task TestEnableDisableEmailReminderService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.EmailReminder>> mockCollection = new Mock<IMongoCollection<Entity.EmailReminder>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();

            mockdb.Setup(x => x.GetCollection<Entity.EmailReminder>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Entity.EmailReminder>>(), It.IsAny<UpdateDefinition<Entity.EmailReminder>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            mockconfiguration.Setup(x => x["Setting:Url"]).Returns("http://test.com");

            var userProfile = new
            {
                Id = 1,
                UserName = "rainsoft"
            };

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(userProfile), Encoding.UTF8, "application/json")
               })
               .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            //Act
            IEnumerable<string> authHeader = new string[] { "Authorization" };
            IEmailReminderService emailReminderService = new EmailReminderService(mock.Object, httpClient, mockconfiguration.Object);
            bool result = await emailReminderService.EnableDisableEmailReminder("5fa020214c2ff92af0a1c85f", false, 1, authHeader);

            //Assert
            Assert.True(result);
        }
        [Fact]
        public async Task TestEnableDisableAllEmailRemindersService()
        {
            //Arrange
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();

            mockconfiguration.Setup(x => x["Setting:Url"]).Returns("http://test.com");

            var userProfile = new
            {
                Id = 1,
                UserName = "rainsoft"
            };

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(userProfile), Encoding.UTF8, "application/json")
               })
               .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            //Act
            IEnumerable<string> authHeader = new string[] { "Authorization" };
            IEmailReminderService emailReminderService = new EmailReminderService(null, httpClient, mockconfiguration.Object);
            await emailReminderService.EnableDisableAllEmailReminders(false, 1, authHeader);
        }
        [Fact]
        public async Task TestGetEmailReminderByIdService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.EmailReminder>> mockCollection = new Mock<IMongoCollection<Entity.EmailReminder>>();

            Mock<IAsyncCursor<BsonDocument>> mockCursorEmailReminder = new Mock<IAsyncCursor<BsonDocument>>();

            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            BsonDocument email = new BsonDocument() {
                new BsonDocument
                {
                     { "_id" , "5fa0202d4c2ff92af0a1c861"},
                     { "fromAddress" , "from@gmail.com"},
                     { "ccAddress" , "cc@gmail.com"},
                     { "subject" , "Texas"},
                     { "emailBody" , "<p>Its number 2</p>\n" }
                 }
            };

            List<BsonDocument> emailReminder = new List<BsonDocument>()
            {
                new BsonDocument
                {
                     { "_id" , "5fa0202d4c2ff92af0a1c860"},
                     { "noOfDays" , 10},
                     { "recurringTime" , "2021-01-19T10:00:46.000Z"},
                     { "isActive" , true},
                     { "email" , email}
                 }
            };

            mockCursorEmailReminder.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorEmailReminder.SetupGet(x => x.Current).Returns(emailReminder);

            mockCollection.SetupSequence(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.EmailReminder, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorEmailReminder.Object);

            mockdb.SetupSequence(x => x.GetCollection<Entity.EmailReminder>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new EmailReminderService(mock.Object, null, null);

            //Act
            Model.EmailReminder dto = await service.GetEmailReminderById("5fa0202d4c2ff92af0a1c860");

            //Assert
            Assert.NotNull(dto);
            Assert.Equal("5fa0202d4c2ff92af0a1c860", dto.id);
            Assert.Equal(10, dto.noOfDays);
            Assert.Equal(true, dto.isActive);
        }
        [Fact]
        public async Task TestGetDocumentStatusByLoanIdsService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Request>> mockCollection = new Mock<IMongoCollection<Entity.Request>>();

            Mock<IAsyncCursor<BsonDocument>> mockCursorEmailRequest = new Mock<IAsyncCursor<BsonDocument>>();

            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();

            List<BsonDocument> emailReminder = new List<BsonDocument>()
            {
                new BsonDocument
                {
                     { "_id" , "5fa0202d4c2ff92af0a1c860"}
                   
                },
                  new BsonDocument
                {
                     { "_id" , "5fa0202d4c2ff92af0a1c861"}
                    
                },
                  new BsonDocument
                {
                     { "_id" , "5fa0202d4c2ff92af0a1c862"}
                   
                }
            };

            mockCursorEmailRequest.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorEmailRequest.SetupGet(x => x.Current).Returns(emailReminder);

            mockCollection.SetupSequence(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorEmailRequest.Object);

            mockdb.SetupSequence(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new EmailReminderService(mock.Object, null, null);

            //Act
            List<RemainingDocuments> remainingDocuments = new List<RemainingDocuments>();

            RemainingDocuments remainingDocuments1 = new RemainingDocuments();
            remainingDocuments1.loanApplicationId = 10;
            remainingDocuments.Add(remainingDocuments1);

            //RemainingDocuments remainingDocuments2 = new RemainingDocuments();
            //remainingDocuments1.loanApplicationId = 11;
            //remainingDocuments.Add(remainingDocuments2);

            //RemainingDocuments remainingDocuments3 = new RemainingDocuments();
            //remainingDocuments1.loanApplicationId = 12;
            //remainingDocuments.Add(remainingDocuments3);
            
            List<RemainingDocuments> dto = await service.GetDocumentStatusByLoanIds(remainingDocuments);

            //Assert
            Assert.NotNull(dto);
            Assert.Equal(10, dto[0].loanApplicationId);
            Assert.Equal(true, dto[0].isDocumentRemaining);
        }
        [Fact]
        public async Task TestGetEmailReminderByIdsService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.EmailReminder>> mockCollection = new Mock<IMongoCollection<Entity.EmailReminder>>();

            Mock<IAsyncCursor<BsonDocument>> mockCursorEmailReminder = new Mock<IAsyncCursor<BsonDocument>>();

            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            BsonDocument email = new BsonDocument() {
                new BsonDocument
                {
                     { "_id" , "5fa0202d4c2ff92af0a1c861"},
                     { "fromAddress" , "from@gmail.com"},
                     { "ccAddress" , "cc@gmail.com"},
                     { "subject" , "Texas"},
                     { "emailBody" , "<p>Its number 2</p>\n" }
                 }
            };

            List<BsonDocument> emailReminder = new List<BsonDocument>()
            {
                new BsonDocument
                {
                     { "_id" , "5fa0202d4c2ff92af0a1c860"},
                     { "noOfDays" , 10},
                     { "recurringTime" , "2021-01-19T10:00:46.000Z"},
                     { "isActive" , true},
                     { "email" , email}
                 }
            };

            mockCursorEmailReminder.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorEmailReminder.SetupGet(x => x.Current).Returns(emailReminder);

            mockCollection.SetupSequence(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.EmailReminder, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorEmailReminder.Object);

            mockdb.SetupSequence(x => x.GetCollection<Entity.EmailReminder>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new EmailReminderService(mock.Object, null, null);

            //Act
            List<string> emailReminderIds = new List<string>();
            emailReminderIds.Add("5eb25d1fe519051af2eeb72d");
            emailReminderIds.Add("5fa0202d4c2ff92af0a1c860");

            List<Model.EmailReminder> dto = await service.GetEmailReminderByIds(emailReminderIds,1);

            //Assert
            Assert.NotNull(dto);
            Assert.Equal("5fa0202d4c2ff92af0a1c860", dto[0].id);
            Assert.Equal(10, dto[0].noOfDays);
            Assert.Equal(true, dto[0].isActive);
        }
        #endregion
    }
}
