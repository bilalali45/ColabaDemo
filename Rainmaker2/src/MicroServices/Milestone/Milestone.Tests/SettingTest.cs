using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Milestone.API.Controllers;
using Milestone.Data;
using Milestone.Model;
using Milestone.Service;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace Milestone.Tests
{
    public class SettingTest
    {
        #region Controller

        [Fact]
        public async Task TestGetLoanStatusesController()
        {
            Mock<ISettingService> mock = new Mock<ISettingService>();

            LoanStatus loanStatus = new LoanStatus();
            loanStatus.id = 2;
            loanStatus.mcuName = "Application Submitted";
            loanStatus.statusId = 65;
            loanStatus.tenantId = 1;
            loanStatus.fromStatusId = 2;
            loanStatus.fromStatus = "Application Submitted";
            loanStatus.toStatusId = 7;
            loanStatus.toStatus = "Completed";
            loanStatus.noofDays = 2;
            loanStatus.recurringTime = DateTime.Now;
            loanStatus.isActive = true;
            loanStatus.emailId = 63;
            loanStatus.fromAddress = "###RequestorUserEmail###";
            loanStatus.ccAddress = "###PrimaryBorrowerEmailAddress###";
            loanStatus.subject = "Subject";
            loanStatus.body = "<p>Application submitted Completed</p>\n";

            StatusConfigurationModel statusConfigurationModel = new StatusConfigurationModel();
            statusConfigurationModel.isActive = true;
            statusConfigurationModel.loanStatuses = new List<LoanStatus>();
            statusConfigurationModel.loanStatuses.Add(loanStatus);
            mock.Setup(x => x.GetLoanStatuses(It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(statusConfigurationModel);
            var settingController = new SettingController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            settingController.ControllerContext = context;

            IActionResult result = await settingController.GetLoanStatuses();
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestGetEmailConfigurationsByIdsController()
        {
            Mock<ISettingService> mock = new Mock<ISettingService>();
            int[] ids = new int[] { 1, 3, 5, 7, 9 };

            GetEmailConfigurationByIds getEmailConfigurationByIds = new GetEmailConfigurationByIds();
            getEmailConfigurationByIds.id = ids;

            List<EmailConfigurationModel> emailConfigurationModel = new List<EmailConfigurationModel>();

            EmailConfigurationModel emailConfigurationModel1 = new EmailConfigurationModel();
            emailConfigurationModel1.id = 1;
            emailConfigurationModel1.statusUpdateId = 1;
            emailConfigurationModel1.fromAddress = "###RequestorUserEmail###";
            emailConfigurationModel1.ccAddress = "###PrimaryBorrowerEmailAddress###";
            emailConfigurationModel1.subject = "Subject";
            emailConfigurationModel1.body = "<p>Application submitted Completed</p>\n";

            emailConfigurationModel.Add(emailConfigurationModel1);

            mock.Setup(x => x.GetEmailConfigurations(It.IsAny<List<int>>())).ReturnsAsync(emailConfigurationModel);
            var settingController = new SettingController(mock.Object);

            IActionResult result = await settingController.GetEmailConfigurationsByIds(getEmailConfigurationByIds);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestUpdateLoanStatusesController()
        {
            Mock<ISettingService> mock = new Mock<ISettingService>();
            mock.Setup(x => x.UpdateLoanStatuses(It.IsAny<StatusConfigurationModel>(), It.IsAny<int>())).ReturnsAsync(true);
            var settingController = new SettingController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(new StringValues("Test"));
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            settingController.ControllerContext = context;

            LoanStatus loanStatus = new LoanStatus();
            loanStatus.id = 2;
            loanStatus.mcuName = "Application Submitted";
            loanStatus.statusId = 65;
            loanStatus.tenantId = 1;
            loanStatus.fromStatusId = 2;
            loanStatus.fromStatus = "Application Submitted";
            loanStatus.toStatusId = 7;
            loanStatus.toStatus = "Completed";
            loanStatus.noofDays = 2;
            loanStatus.recurringTime = DateTime.Now;
            loanStatus.isActive = true;
            loanStatus.emailId = 63;
            loanStatus.fromAddress = "###RequestorUserEmail###";
            loanStatus.ccAddress = "###PrimaryBorrowerEmailAddress###";
            loanStatus.subject = "Subject";
            loanStatus.body = "<p>Application submitted Completed</p>\n";

            StatusConfigurationModel statusConfigurationModel = new StatusConfigurationModel();
            statusConfigurationModel.isActive = true;
            statusConfigurationModel.loanStatuses = new List<LoanStatus>();
            statusConfigurationModel.loanStatuses.Add(loanStatus);

            IActionResult result = await settingController.UpdateLoanStatuses(statusConfigurationModel);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestEnableDisableEmailReminderController()
        {
            Mock<ISettingService> mock = new Mock<ISettingService>();
            mock.Setup(x => x.EnableDisableEmailReminder(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(true);
            var settingController = new SettingController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(new StringValues("Test"));
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            settingController.ControllerContext = context;

            EnableDisableEmailReminderModel enableDisableEmailReminderModel = new EnableDisableEmailReminderModel();
            enableDisableEmailReminderModel.id = 1;
            enableDisableEmailReminderModel.isActive = false;

            IActionResult result = await settingController.EnableDisableEmailReminder(enableDisableEmailReminderModel);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestEnableDisableEmailReminderNotFoundController()
        {
            Mock<ISettingService> mock = new Mock<ISettingService>();
            mock.Setup(x => x.EnableDisableEmailReminder(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(false);
            var settingController = new SettingController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(new StringValues("Test"));
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            settingController.ControllerContext = context;

            EnableDisableEmailReminderModel enableDisableEmailReminderModel = new EnableDisableEmailReminderModel();
            enableDisableEmailReminderModel.id = 1;
            enableDisableEmailReminderModel.isActive = false;

            IActionResult result = await settingController.EnableDisableEmailReminder(enableDisableEmailReminderModel);
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestEnableDisableAllEmailRemindersController()
        {
            Mock<ISettingService> mock = new Mock<ISettingService>();
            mock.Setup(x => x.EnableDisableAllEmailReminders(It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<IEnumerable<string>>()));
            var settingController = new SettingController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(new StringValues("Test"));
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            settingController.ControllerContext = context;

            EnableDisableAllEmailReminderModel enableDisableEmailReminderModel = new EnableDisableAllEmailReminderModel();
            enableDisableEmailReminderModel.isActive = false;

            IActionResult result = await settingController.EnableDisableAllEmailReminders(enableDisableEmailReminderModel);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        #endregion

        #region Service

        [Fact]
        public async Task TestGetEmailConfigurationsService()
        {
            //Arrange
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("MilestoneContext");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();

            Milestone.Entity.Models.MilestoneEmailConfiguration emailConfiguration = new Milestone.Entity.Models.MilestoneEmailConfiguration()
            {
                Id = 1,
                StatusUpdateId = 1,
                FromAddress = "###RequestorUserEmail###",
                CcAddress = "###Co-BorrowerEmailAddress###",
                Body = "<p>Processing</p>"
            };
            dataContext.Set<Milestone.Entity.Models.MilestoneEmailConfiguration>().Add(emailConfiguration);

            Milestone.Entity.Models.MilestoneEmailConfiguration emailConfiguration1 = new Milestone.Entity.Models.MilestoneEmailConfiguration()
            {
                Id = 2,
                StatusUpdateId = 2,
                FromAddress = "###RequestorUserEmail###",
                CcAddress = "###Co-BorrowerEmailAddress###",
                Body = "<p>Processing</p> "
            };
            dataContext.Set<Milestone.Entity.Models.MilestoneEmailConfiguration>().Add(emailConfiguration1);

            dataContext.SaveChanges();

            ISettingService userProfileService = new SettingService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null,null,null,null);

            //Act
            List<int> emailIds = new List<int> { 1, 2, 3 };
            List<EmailConfigurationModel> res = await userProfileService.GetEmailConfigurations(emailIds);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(1, res[0].id);
            Assert.Equal(1, res[0].statusUpdateId);
            Assert.Equal("###RequestorUserEmail###", res[0].fromAddress);
            Assert.Equal("###Co-BorrowerEmailAddress###", res[0].ccAddress);
            Assert.Equal("<p>Processing</p>", res[0].body);
        }
        [Fact]
        public async Task TestGetEmailStatusConfigurationService()
        {
            //Arrange
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("MilestoneContext");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();

            Milestone.Entity.Models.MilestoneStatusConfiguration statusConfiguration = new Milestone.Entity.Models.MilestoneStatusConfiguration()
            {
                Id = 1,
                TenantId = 1,
                FromStatus = 1,
                ToStatus = 2,
                NoofDays = 2,
                RecurringTime = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            };
            dataContext.Set<Milestone.Entity.Models.MilestoneStatusConfiguration>().Add(statusConfiguration);

            dataContext.SaveChanges();

            ISettingService settingService = new SettingService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null, null);

            //Act
            EmailStatusConfiguration res = await settingService.GetEmailStatusConfiguration(1,2, 1);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(1, res.id);
            Assert.Equal(1, res.tenantId);
            Assert.Equal(1, res.fromStatus);
            Assert.Equal(2, res.toStatus);
        }

        [Fact]
        public async Task TestEnableDisableAllEmailRemindersService()
        {
            //Arrange
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("MilestoneContext");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();

            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            
            mockconfiguration.Setup(x => x["Setting:Url"]).Returns("http://test.com");

            var jobType = new
            {
                jobTypeId = 2,
                isActive = true
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

            ISettingService settingService = new SettingService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, httpClient, mockconfiguration.Object, null);

            //Act
            IEnumerable<string> authHeader = new string[] { "Authorization" };
            await settingService.EnableDisableAllEmailReminders(true,1, authHeader);
        }

        [Fact]
        public async Task TestEnableDisableEmailReminderService()
        {
            //Arrange
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("MilestoneContext");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();

            Milestone.Entity.Models.MilestoneStatusConfiguration statusConfiguration = new Milestone.Entity.Models.MilestoneStatusConfiguration()
            {
                Id = 2,
                TenantId = 1,
                FromStatus = 3,
                ToStatus = 4,
                NoofDays = 4,
                RecurringTime = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            };
            dataContext.Set<Milestone.Entity.Models.MilestoneStatusConfiguration>().Add(statusConfiguration);

            dataContext.SaveChanges();

            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();

            mockconfiguration.Setup(x => x["Setting:Url"]).Returns("http://test.com");

            var jobType = new
            {
                jobTypeId = 2,
                isActive = true
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

            ISettingService settingService = new SettingService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, httpClient, mockconfiguration.Object, null);

            //Act
            IEnumerable<string> authHeader = new string[] { "Authorization" };
            await settingService.EnableDisableEmailReminder(2,false,1,authHeader);
        }
        [Fact]
        public async Task TestUpdateLoanStatusesService()
        {
            //Arrange
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("MilestoneContext");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();

            Milestone.Entity.Models.Milestone milestone = new Milestone.Entity.Models.Milestone()
            {
                Id = 5,
                McuName = "Approved with Conditions",
                Description = "Your loan has been conditionally approved! We just need a few final documents to wrap up.",
                MilestoneTypeId = 1
            };
            dataContext.Set<Milestone.Entity.Models.Milestone>().Add(milestone);

            Milestone.Entity.Models.MilestoneStatusConfiguration statusConfiguration = new Milestone.Entity.Models.MilestoneStatusConfiguration()
            {
                Id = 5,
                TenantId = 1,
                FromStatus = 5,
                ToStatus = 6,
                NoofDays = 9,
                RecurringTime = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            };
            dataContext.Set<Milestone.Entity.Models.MilestoneStatusConfiguration>().Add(statusConfiguration);

            Milestone.Entity.Models.MilestoneEmailConfiguration emailConfiguration = new Milestone.Entity.Models.MilestoneEmailConfiguration()
            {
                Id = 5,
                StatusUpdateId = 5,
                FromAddress = "###RequestorUserEmail###",
                CcAddress = "###Co-BorrowerEmailAddress###",
                Body = "<p>Processing</p>"
            };
            dataContext.Set<Milestone.Entity.Models.MilestoneEmailConfiguration>().Add(emailConfiguration);

            dataContext.SaveChanges();

            ISettingService settingService = new SettingService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null, null);

            //Act
            IEnumerable<string> authHeader = new string[] { "Authorization" };

            LoanStatus loanStatus = new LoanStatus();
            loanStatus.id = 5;
            loanStatus.mcuName = "Application Submitted";
            loanStatus.statusId = 66;
            loanStatus.tenantId = 1;
            loanStatus.fromStatusId = 2;
            loanStatus.fromStatus = "Application Submitted";
            loanStatus.toStatusId = 7;
            loanStatus.toStatus = "Completed";
            loanStatus.noofDays = 2;
            loanStatus.recurringTime = DateTime.Now;
            loanStatus.isActive = true;
            loanStatus.emailId = 66;
            loanStatus.fromAddress = "###RequestorUserEmail###";
            loanStatus.ccAddress = "###PrimaryBorrowerEmailAddress###";
            loanStatus.subject = "Subject";
            loanStatus.body = "<p>Application submitted Completed</p>\n";

            StatusConfigurationModel statusConfigurationModel = new StatusConfigurationModel();
            statusConfigurationModel.isActive = true;
            statusConfigurationModel.loanStatuses = new List<LoanStatus>();
            statusConfigurationModel.loanStatuses.Add(loanStatus);
            var res = await settingService.UpdateLoanStatuses(statusConfigurationModel, 1);

            // Assert
            Assert.NotNull(res);
        }
        [Fact]
        public async Task TestUpdateLoanStatusesElseService()
        {
            //Arrange
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("MilestoneContext");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();

            Milestone.Entity.Models.Milestone milestone = new Milestone.Entity.Models.Milestone()
            {
                Id = 6,
                McuName = "Closing",
                Description = "You’re near the final stages of your mortgage journey. You’ll be reviewing and signing all the required documents to finish up.",
                MilestoneTypeId = 1
            };
            dataContext.Set<Milestone.Entity.Models.Milestone>().Add(milestone);

            dataContext.SaveChanges();

            ISettingService settingService = new SettingService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null, null, null);

            //Act
            IEnumerable<string> authHeader = new string[] { "Authorization" };

            LoanStatus loanStatus = new LoanStatus();
            loanStatus.id = 6;
            loanStatus.mcuName = "Application Submitted";
            loanStatus.statusId = 67;
            loanStatus.tenantId = 1;
            loanStatus.fromStatusId = 2;
            loanStatus.fromStatus = "Application Submitted";
            loanStatus.toStatusId = 7;
            loanStatus.toStatus = "Completed";
            loanStatus.noofDays = 2;
            loanStatus.recurringTime = DateTime.Now;
            loanStatus.isActive = true;
            loanStatus.emailId = 67;
            loanStatus.fromAddress = "###RequestorUserEmail###";
            loanStatus.ccAddress = "###PrimaryBorrowerEmailAddress###";
            loanStatus.subject = "Subject";
            loanStatus.body = "<p>Application submitted Completed</p>\n";

            StatusConfigurationModel statusConfigurationModel = new StatusConfigurationModel();
            statusConfigurationModel.isActive = true;
            statusConfigurationModel.loanStatuses = new List<LoanStatus>();
            statusConfigurationModel.loanStatuses.Add(loanStatus);
            var res = await settingService.UpdateLoanStatuses(statusConfigurationModel, 1);

            // Assert
            Assert.NotNull(res);
        }
        [Fact]
        public async Task TestGetLoanStatusesService()
        {
            //Arrange
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("MilestoneContext");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();

            Milestone.Entity.Models.Milestone milestone = new Milestone.Entity.Models.Milestone()
            {
                Id = 9,
                McuName = "Application Withdrawn",
                Description = "Your loan application has been withdrawn from consideration. Please contact us if you require additional information.",
                MilestoneTypeId = 2
            };
            dataContext.Set<Milestone.Entity.Models.Milestone>().Add(milestone);

            Milestone.Entity.Models.MilestoneStatusConfiguration statusConfiguration = new Milestone.Entity.Models.MilestoneStatusConfiguration()
            {
                Id = 9,
                TenantId = 1,
                FromStatus = 9,
                ToStatus = 10,
                NoofDays = 10,
                RecurringTime = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            };
            dataContext.Set<Milestone.Entity.Models.MilestoneStatusConfiguration>().Add(statusConfiguration);

            Milestone.Entity.Models.MilestoneEmailConfiguration emailConfiguration = new Milestone.Entity.Models.MilestoneEmailConfiguration()
            {
                Id = 9,
                StatusUpdateId = 9,
                FromAddress = "###RequestorUserEmail###",
                CcAddress = "###Co-BorrowerEmailAddress###",
                Body = "<p>Processing</p>"
            };
            dataContext.Set<Milestone.Entity.Models.MilestoneEmailConfiguration>().Add(emailConfiguration);

            dataContext.SaveChanges();

            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();

            //Act 

            LoanStatus loanStatus = new LoanStatus();
            loanStatus.id = 9;
            loanStatus.mcuName = "Application Submitted";
            loanStatus.statusId = 69;
            loanStatus.tenantId = 1;
            loanStatus.fromStatusId = 9;
            loanStatus.fromStatus = "Application Submitted";
            loanStatus.toStatusId = 7;
            loanStatus.toStatus = "Completed";
            loanStatus.noofDays = 2;
            loanStatus.recurringTime = DateTime.Now;
            loanStatus.isActive = true;
            loanStatus.emailId = 69;
            loanStatus.fromAddress = "###RequestorUserEmail###";
            loanStatus.ccAddress = "###PrimaryBorrowerEmailAddress###";
            loanStatus.subject = "Subject";
            loanStatus.body = "<p>Application submitted Completed</p>\n";

          

            mockconfiguration.Setup(x => x["Setting:Url"]).Returns("http://test.com");

            var jobType = new
            {
                jobTypeId = 2,
                isActive = true
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

            ISettingService settingService = new SettingService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, httpClient, mockconfiguration.Object, null);
            IEnumerable<string> authHeader = new string[] { "Authorization" };
            var res = await settingService.GetLoanStatuses(1, authHeader);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(true,res.isActive);
        }
        [Fact]
        public async Task TestSendEmailReminderLogService()
        {
            //Arrange
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("MilestoneContext");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();

            Milestone.Entity.Models.MilestoneLog milestoneLog = new Milestone.Entity.Models.MilestoneLog()
            {
                Id = 3,
                LoanApplicationId = 10,
                MilestoneId = 10,
                UserId = 1
            };
            dataContext.Set<Milestone.Entity.Models.MilestoneLog>().Add(milestoneLog);

            Milestone.Entity.Models.MilestoneStatusConfiguration statusConfiguration = new Milestone.Entity.Models.MilestoneStatusConfiguration()
            {
                Id = 11,
                TenantId = 1,
                FromStatus = 10,
                ToStatus = 12,
                NoofDays = 10,
                RecurringTime = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            };
            dataContext.Set<Milestone.Entity.Models.MilestoneStatusConfiguration>().Add(statusConfiguration);

            dataContext.SaveChanges();

            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();

            //Act 

            mockconfiguration.Setup(x => x["Setting:Url"]).Returns("http://test.com");

            var jobType = new
            {
                jobTypeId = 2,
                isActive = true
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

            ISettingService settingService = new SettingService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, httpClient, mockconfiguration.Object, null);
            IEnumerable<string> authHeader = new string[] { "Authorization" };
            var res = await settingService.SendEmailReminderLog(10,12,1, authHeader);

            // Assert
            Assert.NotNull(res);
        }

        #endregion
    }
}
