using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Moq;
using Moq.Protected;
using Setting.API.Controllers;
using Setting.Data;
using Setting.Model;
using Setting.Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace Setting.Tests
{
    public class EmailReminderTest
    {
        #region Controller
        [Fact]
        public async Task TestGetLoanStatusesController()
        {
            Mock<IEmailReminderService> mock = new Mock<IEmailReminderService>();

            JobTypeModel jobTypeModel = new JobTypeModel();
            jobTypeModel.id = 1;
            jobTypeModel.isActive = true;
            jobTypeModel.name = "Email Reminder";

            mock.Setup(x => x.GetJobType(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(jobTypeModel);
            var emailReminderController = new EmailReminderController(mock.Object,null);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;

            GetJobTypeModel getJobTypeModel = new GetJobTypeModel();
            IActionResult result = await emailReminderController.GetJobType(getJobTypeModel);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestInsertEmailReminderLogController()
        {
            Mock<IEmailReminderService> mock = new Mock<IEmailReminderService>();

            mock.Setup(x => x.InsertEmailReminderLog(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(true);
            var emailReminderController = new EmailReminderController(mock.Object, null);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;

            EmailReminderLogModel emailReminderLogModel = new EmailReminderLogModel();
            emailReminderLogModel.tenantId = 1;
            emailReminderLogModel.loanApplicationId = 1;
            emailReminderLogModel.jobTypeId = 1;
            IActionResult result = await emailReminderController.InsertEmailReminderLog(emailReminderLogModel);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestInsertEmailReminderLogNotFoundController()
        {
            Mock<IEmailReminderService> mock = new Mock<IEmailReminderService>();

            mock.Setup(x => x.InsertEmailReminderLog(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(false);
            var emailReminderController = new EmailReminderController(mock.Object, null);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;

            EmailReminderLogModel emailReminderLogModel = new EmailReminderLogModel();
            emailReminderLogModel.tenantId = 1;
            emailReminderLogModel.loanApplicationId = 1;
            emailReminderLogModel.jobTypeId = 1;
            IActionResult result = await emailReminderController.InsertEmailReminderLog(emailReminderLogModel);
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestInsertLoanStatusLogController()
        {
            Mock<IEmailReminderService> mock = new Mock<IEmailReminderService>();

            mock.Setup(x => x.InsertLoanStatusLog(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(true);
            var emailReminderController = new EmailReminderController(mock.Object, null);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;

            EmailReminderLogModel emailReminderLogModel = new EmailReminderLogModel();
            emailReminderLogModel.tenantId = 1;
            emailReminderLogModel.loanApplicationId = 1;
            emailReminderLogModel.jobTypeId = 1;
            IActionResult result = await emailReminderController.InsertLoanStatusLog(emailReminderLogModel);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestInsertLoanStatusLogNotFoundController()
        {
            Mock<IEmailReminderService> mock = new Mock<IEmailReminderService>();

            mock.Setup(x => x.InsertLoanStatusLog(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(false);
            var emailReminderController = new EmailReminderController(mock.Object, null);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;

            EmailReminderLogModel emailReminderLogModel = new EmailReminderLogModel();
            emailReminderLogModel.tenantId = 1;
            emailReminderLogModel.loanApplicationId = 1;
            emailReminderLogModel.jobTypeId = 1;
            IActionResult result = await emailReminderController.InsertLoanStatusLog(emailReminderLogModel);
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task TestUpdateEmailReminderController()
        {
            Mock<IEmailReminderService> mock = new Mock<IEmailReminderService>();

            mock.Setup(x => x.UpdateEmailReminder(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>()));
            var emailReminderController = new EmailReminderController(mock.Object, null);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;

            UpdateEmailReminderLogModel updateEmailReminderLogModel = new UpdateEmailReminderLogModel();
            updateEmailReminderLogModel.id = "5ffc614c77ac9218e8a3e430";
            updateEmailReminderLogModel.noOfDays = 1;
            updateEmailReminderLogModel.recurringTime = DateTime.UtcNow;
            IActionResult result = await emailReminderController.UpdateEmailReminder(updateEmailReminderLogModel);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestDeleteEmailReminderController()
        {
            Mock<IEmailReminderService> mock = new Mock<IEmailReminderService>();

            mock.Setup(x => x.DeleteEmailReminder(It.IsAny<string>()));
            var emailReminderController = new EmailReminderController(mock.Object, null);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;

            DeleteEmailReminderLogModel deleteEmailReminderLogModel = new DeleteEmailReminderLogModel();
            deleteEmailReminderLogModel.id = "5ffc614c77ac9218e8a3e430";
           
            IActionResult result = await emailReminderController.DeleteEmailReminder(deleteEmailReminderLogModel);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestEnableDisableAllEmailRemindersController()
        {
            Mock<IEmailReminderService> mock = new Mock<IEmailReminderService>();

            mock.Setup(x => x.EnableDisableAllEmailReminders(It.IsAny<bool>(), It.IsAny<int>()));
            var emailReminderController = new EmailReminderController(mock.Object, null);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;

            EnableDisableAllRemindersModel enableDisableAllRemindersModel = new EnableDisableAllRemindersModel();
            enableDisableAllRemindersModel.isActive = false;
            enableDisableAllRemindersModel.jobTypeId = 1;

            IActionResult result = await emailReminderController.EnableDisableAllEmailReminders(enableDisableAllRemindersModel);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestEnableDisableEmailRemindersController()
        {
            Mock<IEmailReminderService> mock = new Mock<IEmailReminderService>();

            mock.Setup(x => x.EnableDisableEmailReminders(It.IsAny<List<string>>(), It.IsAny<bool>()));
            var emailReminderController = new EmailReminderController(mock.Object, null);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;

            string[] str = new string[2] { "1","2"};
            EnableDisableReminderModel enableDisableReminderModel = new EnableDisableReminderModel();
            enableDisableReminderModel.id = str;
            enableDisableReminderModel.isActive = true;

            IActionResult result = await emailReminderController.EnableDisableEmailReminders(enableDisableReminderModel);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestEnableDisableEmailRemindersByStatusUpdateIdController()
        {
            Mock<IEmailReminderService> mock = new Mock<IEmailReminderService>();

            mock.Setup(x => x.EnableDisableEmailReminders(It.IsAny<List<string>>(), It.IsAny<bool>()));
            var emailReminderController = new EmailReminderController(mock.Object, null);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;

            int[] str = new int[2] { 1, 2 };

            EnableDisableLoanStatusReminderModel enableDisableLoanStatusReminderModel = new EnableDisableLoanStatusReminderModel();
            enableDisableLoanStatusReminderModel.id = str;
            enableDisableLoanStatusReminderModel.isActive = true;

            IActionResult result = await emailReminderController.EnableDisableEmailRemindersByStatusUpdateId(enableDisableLoanStatusReminderModel);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestEmailReminderJobController()
        {
            Mock<IBackgroundService> mock = new Mock<IBackgroundService>();

            mock.Setup(x => x.EmailReminderJob());
            var emailReminderController = new EmailReminderController(null, mock.Object);

            IActionResult result = await emailReminderController.EmailReminderJob();
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        #endregion
        #region Services
        [Fact]
        public async Task TestInsertEmailReminderLogService()
        {
            //Arrange
            DbContextOptions<SettingContext> options;
            var builder = new DbContextOptionsBuilder<SettingContext>();
            builder.UseInMemoryDatabase("Setting");
            options = builder.Options;
            using SettingContext dataContext = new SettingContext(options);
            Mock<IEmailReminderService> mockEmailReminderService = new Mock<IEmailReminderService>();
           
            dataContext.Database.EnsureCreated();

            Entity.Models.JobType jobType1 = new Entity.Models.JobType()
            {
                Id = 1,
                TenantId = 1,
                Name = "Email Reminder"
            };
            dataContext.Set<Entity.Models.JobType>().Add(jobType1);

            dataContext.SaveChanges();

            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();

            mockconfiguration.Setup(x => x["DocumentManagement:Url"]).Returns("http://test.com");
            List<EmailReminder> emailReminders = new List<EmailReminder>();

            EmailReminder emailReminder = new EmailReminder();
            emailReminder.id = "5ffc614c77ac9218e8a3e430";
            emailReminders.Add(emailReminder);

            EmailReminderModel emailReminderModel = new EmailReminderModel();
            emailReminderModel.isActive = true;
            emailReminderModel.emailReminders = new List<EmailReminder>();
            emailReminderModel.emailReminders = emailReminders;

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
                   Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(emailReminderModel), Encoding.UTF8, "application/json")
               })
               .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            IEmailReminderService emailReminderService = new EmailReminderService(new UnitOfWork<SettingContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, httpClient, mockconfiguration.Object);

            //Act
            IEnumerable<string> authHeader = new string[] { "Authorization" };
            var res = await emailReminderService.InsertEmailReminderLog(1, 1, 1, authHeader);
        }
        [Fact]
        public async Task TestInsertEmailReminderLogResponseService()
        {
            DbContextOptions<SettingContext> options;
            var builder = new DbContextOptionsBuilder<SettingContext>();
            builder.UseInMemoryDatabase("Setting");
            options = builder.Options;
            using SettingContext dataContext = new SettingContext(options);

            dataContext.Database.EnsureCreated();

            Entity.Models.EmailReminderLog emailReminderLog = new Entity.Models.EmailReminderLog()
            {
                Id = 33,
                TenantId = 1
            };

            dataContext.Set<Entity.Models.EmailReminderLog>().Add(emailReminderLog);

            dataContext.SaveChanges();

            IEmailReminderService emailReminderService = new EmailReminderService(new UnitOfWork<SettingContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            
             await emailReminderService.InsertEmailReminderLogResponse(33, DateTime.UtcNow,false,"");
        }
        [Fact]
        public async Task TestDeleteEmailReminderService()
        {
            //Arrange
            DbContextOptions<SettingContext> options;
            var builder = new DbContextOptionsBuilder<SettingContext>();
            builder.UseInMemoryDatabase("Setting");
            options = builder.Options;
            using SettingContext dataContext = new SettingContext(options);

            dataContext.Database.EnsureCreated();

            Entity.Models.EmailReminderLog emailReminderLog = new Entity.Models.EmailReminderLog()
            {
                Id = 37,
                TenantId = 1,
                ReminderId = "5ffd5635c6b26145b4bf85e7",
                IsDeleted = false,
                RecurringDate = new DateTime(2022, 04, 30)
        };

            dataContext.Set<Entity.Models.EmailReminderLog>().Add(emailReminderLog);

            dataContext.SaveChanges();

            IEmailReminderService emailReminderService = new EmailReminderService(new UnitOfWork<SettingContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            await emailReminderService.DeleteEmailReminder("5ffd5635c6b26145b4bf85e7");
        }
        [Fact]
        public async Task TestEnableDisableAllEmailRemindersService()
        {
            //Arrange
            DbContextOptions<SettingContext> options;
            var builder = new DbContextOptionsBuilder<SettingContext>();
            builder.UseInMemoryDatabase("Setting");
            options = builder.Options;
            using SettingContext dataContext = new SettingContext(options);

            dataContext.Database.EnsureCreated();

            Entity.Models.JobType jobType = new Entity.Models.JobType()
            {
                Id = 3,
                TenantId = 1,
                Name = "Email Reminder"
            };
            dataContext.Set<Entity.Models.JobType>().Add(jobType);

            dataContext.SaveChanges();

            IEmailReminderService emailReminderService = new EmailReminderService(new UnitOfWork<SettingContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            await emailReminderService.EnableDisableAllEmailReminders(false,3);
        }
        [Fact]
        public async Task TestGetJobTypeService()
        {
            //Arrange
            DbContextOptions<SettingContext> options;
            var builder = new DbContextOptionsBuilder<SettingContext>();
            builder.UseInMemoryDatabase("Setting");
            options = builder.Options;
            using SettingContext dataContext = new SettingContext(options);

            dataContext.Database.EnsureCreated();

            Entity.Models.JobType jobType = new Entity.Models.JobType()
            {
                Id = 4,
                TenantId = 1,
                Name = "Email Reminder"
            };
            dataContext.Set<Entity.Models.JobType>().Add(jobType);

            dataContext.SaveChanges();

            IEmailReminderService emailReminderService = new EmailReminderService(new UnitOfWork<SettingContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            var res  = await emailReminderService.GetJobType(4,1);

            //Assert
            Assert.NotNull(res);
            Assert.Equal(4, res.id);
            Assert.Equal("Email Reminder", res.name);
        }
        [Fact]
        public async Task TestUpdateEmailReminderService()
        {
            //Arrange
            DbContextOptions<SettingContext> options;
            var builder = new DbContextOptionsBuilder<SettingContext>();
            builder.UseInMemoryDatabase("Setting");
            options = builder.Options;
            using SettingContext dataContext = new SettingContext(options);

            dataContext.Database.EnsureCreated();

            Entity.Models.EmailReminderLog emailReminderLog = new Entity.Models.EmailReminderLog()
            {
                Id = 39,
                TenantId = 1,
                ReminderId = "5ffd5635c6b26145b4bf85e7",
                IsActive =true,
                IsDeleted = false,
                RecurringDate = new DateTime(2022, 04, 30),
                RequestDate = new DateTime(2022, 04, 29),
                IsEmailSent =false
            };

            dataContext.Set<Entity.Models.EmailReminderLog>().Add(emailReminderLog);

            dataContext.SaveChanges();

            IEmailReminderService emailReminderService = new EmailReminderService(new UnitOfWork<SettingContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            await emailReminderService.UpdateEmailReminder("5ffd5635c6b26145b4bf85e7",3,DateTime.UtcNow);
        }
        [Fact]
        public async Task TestInsertLoanStatusLogService()
        {
            //Arrange
            DbContextOptions<SettingContext> options;
            var builder = new DbContextOptionsBuilder<SettingContext>();
            builder.UseInMemoryDatabase("Setting");
            options = builder.Options;
            using SettingContext dataContext = new SettingContext(options);

            dataContext.Database.EnsureCreated();

            Entity.Models.JobType jobType = new Entity.Models.JobType()
            {
                Id = 5,
                TenantId = 1,
                Name = "Email Reminder",
                IsActive = true
            };
            dataContext.Set<Entity.Models.JobType>().Add(jobType);

            dataContext.SaveChanges();

            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();

            mockconfiguration.Setup(x => x["Milestone:Url"]).Returns("http://test.com");
          
            LoanStatus loanStatus = new LoanStatus();
            loanStatus.id = 89;
            loanStatus.mcuName = "Application Submitted";
            loanStatus.statusId = 99;
            loanStatus.tenantId = 1;
            loanStatus.fromStatusId = 2;
            loanStatus.fromStatus = "Application Submitted";
            loanStatus.toStatusId = 7;
            loanStatus.toStatus = "Completed";
            loanStatus.noofDays = 2;
            loanStatus.recurringTime = DateTime.Now;
            loanStatus.isActive = true;
            loanStatus.emailId = 98;
            loanStatus.fromAddress = "###RequestorUserEmail###";
            loanStatus.ccAddress = "###PrimaryBorrowerEmailAddress###";
            loanStatus.subject = "Subject";
            loanStatus.body = "<p>Application submitted Completed</p>\n";

            StatusConfigurationModel statusConfigurationModel = new StatusConfigurationModel();
            statusConfigurationModel.isActive = true;
            statusConfigurationModel.loanStatuses = new List<LoanStatus>();
            statusConfigurationModel.loanStatuses.Add(loanStatus);

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
                   Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(statusConfigurationModel), Encoding.UTF8, "application/json")
               })
               .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            IEmailReminderService emailReminderService = new EmailReminderService(new UnitOfWork<SettingContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, httpClient, mockconfiguration.Object);

            //Act
            IEnumerable<string> authHeader = new string[] { "Authorization" };
            var res = await emailReminderService.InsertLoanStatusLog(1, 5,11,1, authHeader);

            //Assert
            Assert.NotNull(res);
        }
        [Fact]
        public async Task TestEnableDisableEmailRemindersService()
        {
            DbContextOptions<SettingContext> options;
            var builder = new DbContextOptionsBuilder<SettingContext>();
            builder.UseInMemoryDatabase("Setting");
            options = builder.Options;
            using SettingContext dataContext = new SettingContext(options);

            dataContext.Database.EnsureCreated();

            Entity.Models.EmailReminderLog emailReminderLog = new Entity.Models.EmailReminderLog()
            {
                Id = 43,
                TenantId = 1,
                ReminderId = "5ffd5635c6b26145b4bf85e7",
                IsDeleted = false,
                RecurringDate = new DateTime(2022, 04, 30)
            };

            dataContext.Set<Entity.Models.EmailReminderLog>().Add(emailReminderLog);

            dataContext.SaveChanges();

            IEmailReminderService emailReminderService = new EmailReminderService(new UnitOfWork<SettingContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            List<string> reminderIds = new List<string> { "5ffd5635c6b26145b4bf85e7" };
            await emailReminderService.EnableDisableEmailReminders(reminderIds,false);
        }
        [Fact]
        public async Task TestEnableDisableEmailRemindersLoanStatusService()
        {
            DbContextOptions<SettingContext> options;
            var builder = new DbContextOptionsBuilder<SettingContext>();
            builder.UseInMemoryDatabase("Setting");
            options = builder.Options;
            using SettingContext dataContext = new SettingContext(options);

            dataContext.Database.EnsureCreated();

            Entity.Models.EmailReminderLog emailReminderLog = new Entity.Models.EmailReminderLog()
            {
                Id = 44,
                TenantId = 1,
                ReminderId = "5ffd5635c6b26145b4bf85e7",
                LoanStatusId = 5,
                IsDeleted = false,
                RecurringDate = new DateTime(2022, 04, 30)
            };

            dataContext.Set<Entity.Models.EmailReminderLog>().Add(emailReminderLog);

            dataContext.SaveChanges();

            IEmailReminderService emailReminderService = new EmailReminderService(new UnitOfWork<SettingContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            List<int> reminderIds = new List<int> { 5 };
            await emailReminderService.EnableDisableEmailReminders(reminderIds, false);
        }
        [Fact]
        public async Task TestGetEmailreminderLogByDateService()
        {
            DbContextOptions<SettingContext> options;
            var builder = new DbContextOptionsBuilder<SettingContext>();
            builder.UseInMemoryDatabase("Setting");
            options = builder.Options;
            using SettingContext dataContext = new SettingContext(options);

            dataContext.Database.EnsureCreated();

            Entity.Models.EmailReminderLog emailReminderLog = new Entity.Models.EmailReminderLog()
            {
                Id = 46,
                TenantId = 1,
                ReminderId = "5ffd5635c6b26145b4bf85e7",
                LoanStatusId = 5,
                IsActive =true,
                IsDeleted = false,
                RecurringDate = new DateTime(2022, 04, 30),
                RequestDate = new DateTime(2022, 04, 29),
                IsEmailSent = false,
                JobTypeId = 1
            };

            dataContext.Set<Entity.Models.EmailReminderLog>().Add(emailReminderLog);

            dataContext.SaveChanges();

            IEmailReminderService emailReminderService = new EmailReminderService(new UnitOfWork<SettingContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
           var result = await emailReminderService.GetEmailreminderLogByDate(new DateTime(2022, 04, 30), 1,1);
           Assert.NotNull(result);
           Assert.Equal(46, result[0].id);
        }
        [Fact]
        public async Task TestEnableDisableEmailRemindersbyLAIdService()
        {
            DbContextOptions<SettingContext> options;
            var builder = new DbContextOptionsBuilder<SettingContext>();
            builder.UseInMemoryDatabase("Setting");
            options = builder.Options;
            using SettingContext dataContext = new SettingContext(options);

            dataContext.Database.EnsureCreated();

            Entity.Models.EmailReminderLog emailReminderLog = new Entity.Models.EmailReminderLog()
            {
                Id = 54,
                TenantId = 1,
                ReminderId = "5ffd5635c6b26145b4bf85e7",
                LoanStatusId = 6,
                IsDeleted = false,
                RecurringDate = new DateTime(2022, 04, 30),
                LoanApplicationId = 12
            };

            dataContext.Set<Entity.Models.EmailReminderLog>().Add(emailReminderLog);

            dataContext.SaveChanges();

            IEmailReminderService emailReminderService = new EmailReminderService(new UnitOfWork<SettingContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null);

            //Act
            List<int> reminderIds = new List<int> { 12 };
            await emailReminderService.EnableDisableEmailRemindersbyLAId(reminderIds, false);
        }
        #endregion

    }
}
