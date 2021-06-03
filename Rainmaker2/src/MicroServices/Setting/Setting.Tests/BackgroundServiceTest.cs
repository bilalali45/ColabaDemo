using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Setting.Model;
using Setting.Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Extensions.ExtensionClasses;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TenantConfig.Common;
using URF.Core.Abstractions;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using URF.Core.EF.Factories;
using URF.Core.EF;
using System.Net.Mail;
using StackExchange.Redis;
using Notification.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Setting.Tests
{
    public class BackgroundServiceTest
    {
        [Fact]
        public async Task TestEmailReminderJobService()
        {
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            Mock<IEmailReminderService> mockEmailReminderService = new Mock<IEmailReminderService>();
            Mock<IEmailTemplateService> mockEmailTemplateService = new Mock<IEmailTemplateService>();
            Mock<IRainmakerService> mockRainmkerService = new Mock<IRainmakerService>();

            mockconfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");

            JobTypeModel jobTypeModel = new JobTypeModel();
            jobTypeModel.id = 1;
            jobTypeModel.isActive = true;
            jobTypeModel.name = "Email Reminder";

            mockEmailReminderService.Setup(x => x.GetJobType(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(jobTypeModel);

            EmailTemplate emailTemplate = new EmailTemplate();
            emailTemplate.id = 1;
            emailTemplate.tenantId = 1;
            emailTemplate.templateName = "Template #1";
            emailTemplate.templateDescription = "Sed ut perspiciatis unde omnis iste natus";
            emailTemplate.fromAddress = "aliya@texastrustloans.com";
            emailTemplate.CCAddress = "Ali@gmail.com,hasan@gmail.com";
            emailTemplate.toAddress = "prasla@gmail.com";
            emailTemplate.subject = "You have new tasks to complete for your Texas Trust Home Loans loan application";
            emailTemplate.emailBody = "Hi Javed,To continue your application, we need some more information.";
            emailTemplate.sortOrder = 1;

            mockEmailTemplateService.Setup(x => x.GetEmailReplacedToken(It.IsAny<EmailReminder>(), It.IsAny<string>())).ReturnsAsync(emailTemplate);

            List<EmailReminderLogModel> emailReminderLogModels = new List<EmailReminderLogModel>();

            EmailReminderLogModel emailReminderLogModel = new EmailReminderLogModel();
            emailReminderLogModel.tenantId = 1;
            emailReminderLogModel.loanApplicationId = 1;
            emailReminderLogModel.jobTypeId = 1;
            emailReminderLogModel.ReminderId = "6006cae813394a3e188324dc";

            emailReminderLogModels.Add(emailReminderLogModel);

            mockEmailReminderService.Setup(x => x.GetEmailreminderLogByDate(It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(emailReminderLogModels);

            mockRainmkerService.Setup(x => x.SendBorrowerEmail(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()));

            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/keystore/keystore?key=backgroundjobuser", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("rainsoft")
            });
            messages.Add("http://test.com/api/keystore/keystore?key=backgroundjobpwd", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("rainsoft")
            });
            messages.Add("http://test.com/api/identity/token/authorize", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"status\": null,\"data\": {\"token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNQ1UiLCJVc2VyUHJvZmlsZUlkIjoiMSIsIlVzZXJOYW1lIjoicmFpbnNvZnQiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicmFpbnNvZnQiLCJGaXJzdE5hbWUiOiJTeXN0ZW0iLCJMYXN0TmFtZSI6IkFkbWluaXN0cmF0b3IiLCJUZW5hbnRJZCI6IjEiLCJFbXBsb3llZUlkIjoiMSIsImV4cCI6MTYxMjEzMDIxOCwiaXNzIjoicmFpbnNvZnRmbiIsImF1ZCI6InJlYWRlcnMifQ.sjjF6HdHKN9r7LAo5se8mlBYCl2k0Pqo22UDFaI9Bwk\",\"refreshToken\": null,\"userProfileId\": 0,\"userName\": null,\"validFrom\": \"0001-01-01T00:00:00\",\"validTo\": \"0001-01-01T00:00:00\"},\"message\": null,\"code\": null}")
            });
            messages.Add("http://test.com/api/documentmanagement/emailreminder/getdocumentstatusbyloanids", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{\"loanApplicationId\": 1,\"isDocumentRemaining\": true}]")
            });
            messages.Add("http://test.com/api/documentmanagement/emailreminder/getemailreminderbyids", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{\"id\": \"6006cae813394a3e188324dc\",\"noOfDays\": \"2\",\"recurringTime\": \"0001-01-01T00:00:00\",\"isActive\":true,\"email\":{\"id\":\"6006cae813394a3e188324dd\",\"fromAddress\":\"###RequestorUserEmail###\",\"ccAddress\":\"###Co-BorrowerEmailAddress###\",\"subject\":\"subject\",\"emailBody\":\"<p>body</p>\"},\"LoanApplicationId\":1,\"EmailReminderLogId\":1}]")
            });
            messages.Add("http://test.com/api/documentmanagement/dashboardmcu/getpendingdocuments?loanapplicationid=1", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{\"id\": \"6006cae813394a3e188324dc\",\"requestId\": \"6006cae813394a3e188324de\",\"docId\": \"6006cae813394a3e188324df\",\"docName\":\"Pay slip\",\"docMessage\":\"Please upload your pay slip\",\"isRejected\":true,\"files\":[{\"id\":\"6006cae813394a3e188323dd\",\"clientName\":\"unnamed.jpg\",\"fileUploadedOn\":\"0001-01-01T00:00:00\",\"size\":\"325\",\"order\":\"1\"}]}]")
            });

            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            var handlerMock = new Mock<TestMessageHandler>(MockBehavior.Strict);
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
                    Content = new StringContent("rainsoft", Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));

            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(x => x.GetService(typeof(HttpClient)))
                .Returns(httpClient);

            serviceProvider
                .Setup(x => x.GetService(typeof(IEmailReminderService)))
                .Returns(mockEmailReminderService.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IConfiguration)))
                .Returns(mockconfiguration.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IEmailTemplateService)))
                .Returns(mockEmailTemplateService.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IRainmakerService)))
                .Returns(mockRainmkerService.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(ILogger<HangfireBackgroundService>)))
                .Returns(Mock.Of<ILogger<HangfireBackgroundService>>());

            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactory.Object);

            IBackgroundService backgroundService = new HangfireBackgroundService(serviceProvider.Object);

            //Act

            await backgroundService.EmailReminderJob();
        }
        [Fact]
        public async Task TestEmailReminderJobDocumentFalseService()
        {
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            Mock<IEmailReminderService> mockEmailReminderService = new Mock<IEmailReminderService>();
            Mock<IEmailTemplateService> mockEmailTemplateService = new Mock<IEmailTemplateService>();
            Mock<IRainmakerService> mockRainmkerService = new Mock<IRainmakerService>();

            mockconfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");

            JobTypeModel jobTypeModel = new JobTypeModel();
            jobTypeModel.id = 1;
            jobTypeModel.isActive = true;
            jobTypeModel.name = "Email Reminder";

            mockEmailReminderService.Setup(x => x.GetJobType(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(jobTypeModel);

            EmailTemplate emailTemplate = new EmailTemplate();
            emailTemplate.id = 1;
            emailTemplate.tenantId = 1;
            emailTemplate.templateName = "Template #1";
            emailTemplate.templateDescription = "Sed ut perspiciatis unde omnis iste natus";
            emailTemplate.fromAddress = "aliya@texastrustloans.com";
            emailTemplate.CCAddress = "Ali@gmail.com,hasan@gmail.com";
            emailTemplate.toAddress = "prasla@gmail.com";
            emailTemplate.subject = "You have new tasks to complete for your Texas Trust Home Loans loan application";
            emailTemplate.emailBody = "Hi Javed,To continue your application, we need some more information.";
            emailTemplate.sortOrder = 1;

            mockEmailTemplateService.Setup(x => x.GetEmailReplacedToken(It.IsAny<EmailReminder>(), It.IsAny<string>())).ReturnsAsync(emailTemplate);

            List<EmailReminderLogModel> emailReminderLogModels = new List<EmailReminderLogModel>();

            EmailReminderLogModel emailReminderLogModel = new EmailReminderLogModel();
            emailReminderLogModel.tenantId = 1;
            emailReminderLogModel.loanApplicationId = 1;
            emailReminderLogModel.jobTypeId = 1;
            emailReminderLogModel.ReminderId = "6006cae813394a3e188324dc";

            emailReminderLogModels.Add(emailReminderLogModel);

            mockEmailReminderService.Setup(x => x.GetEmailreminderLogByDate(It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(emailReminderLogModels);

            mockRainmkerService.Setup(x => x.SendBorrowerEmail(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()));

            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/keystore/keystore?key=backgroundjobuser", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("rainsoft")
            });
            messages.Add("http://test.com/api/keystore/keystore?key=backgroundjobpwd", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("rainsoft")
            });
            messages.Add("http://test.com/api/identity/token/authorize", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"status\": null,\"data\": {\"token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNQ1UiLCJVc2VyUHJvZmlsZUlkIjoiMSIsIlVzZXJOYW1lIjoicmFpbnNvZnQiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicmFpbnNvZnQiLCJGaXJzdE5hbWUiOiJTeXN0ZW0iLCJMYXN0TmFtZSI6IkFkbWluaXN0cmF0b3IiLCJUZW5hbnRJZCI6IjEiLCJFbXBsb3llZUlkIjoiMSIsImV4cCI6MTYxMjEzMDIxOCwiaXNzIjoicmFpbnNvZnRmbiIsImF1ZCI6InJlYWRlcnMifQ.sjjF6HdHKN9r7LAo5se8mlBYCl2k0Pqo22UDFaI9Bwk\",\"refreshToken\": null,\"userProfileId\": 0,\"userName\": null,\"validFrom\": \"0001-01-01T00:00:00\",\"validTo\": \"0001-01-01T00:00:00\"},\"message\": null,\"code\": null}")
            });
            messages.Add("http://test.com/api/documentmanagement/emailreminder/getdocumentstatusbyloanids", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{\"loanApplicationId\": 1,\"isDocumentRemaining\": false}]")
            });
            messages.Add("http://test.com/api/documentmanagement/emailreminder/getemailreminderbyids", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{\"id\": \"6006cae813394a3e188324dc\",\"noOfDays\": \"2\",\"recurringTime\": \"0001-01-01T00:00:00\",\"isActive\":true,\"email\":{\"id\":\"6006cae813394a3e188324dd\",\"fromAddress\":\"###RequestorUserEmail###\",\"ccAddress\":\"###Co-BorrowerEmailAddress###\",\"subject\":\"subject\",\"emailBody\":\"<p>body</p>\"},\"LoanApplicationId\":1,\"EmailReminderLogId\":1}]")
            });
            messages.Add("http://test.com/api/documentmanagement/dashboardmcu/getpendingdocuments?loanapplicationid=1", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{\"id\": \"6006cae813394a3e188324dc\",\"requestId\": \"6006cae813394a3e188324de\",\"docId\": \"6006cae813394a3e188324df\",\"docName\":\"Pay slip\",\"docMessage\":\"Please upload your pay slip\",\"isRejected\":true,\"files\":[{\"id\":\"6006cae813394a3e188323dd\",\"clientName\":\"unnamed.jpg\",\"fileUploadedOn\":\"0001-01-01T00:00:00\",\"size\":\"325\",\"order\":\"1\"}]}]")
            });

            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            var handlerMock = new Mock<TestMessageHandler>(MockBehavior.Strict);
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
                    Content = new StringContent("rainsoft", Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));

            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(x => x.GetService(typeof(HttpClient)))
                .Returns(httpClient);

            serviceProvider
                .Setup(x => x.GetService(typeof(IEmailReminderService)))
                .Returns(mockEmailReminderService.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IConfiguration)))
                .Returns(mockconfiguration.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IEmailTemplateService)))
                .Returns(mockEmailTemplateService.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IRainmakerService)))
                .Returns(mockRainmkerService.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(ILogger<HangfireBackgroundService>)))
                .Returns(Mock.Of<ILogger<HangfireBackgroundService>>());

            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactory.Object);

            IBackgroundService backgroundService = new HangfireBackgroundService(serviceProvider.Object);

            //Act

            await backgroundService.EmailReminderJob();
        }
        [Fact]
        public async Task TestDispatchEmailJobService()
        {
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            Mock<IEmailReminderService> mockEmailReminderService = new Mock<IEmailReminderService>();
            Mock<IEmailTemplateService> mockEmailTemplateService = new Mock<IEmailTemplateService>();
            Mock<IRainmakerService> mockRainmkerService = new Mock<IRainmakerService>();
            Mock<IKeyStoreService> mockKeyStoreService = new Mock<IKeyStoreService>();
            Mock<ISettingService> mockSettingService = new Mock<ISettingService>();
            var mockSmtpService = new Mock<ISmtpService>();
            mockSmtpService.Setup(x => x.Send(It.IsAny<SmtpClient>(), It.IsAny<MailMessage>())).Verifiable();
            Mock<IUnitOfWork<TenantConfigContext>> mockTenantConfigContext = new Mock<IUnitOfWork<TenantConfigContext>>();

            mockKeyStoreService.Setup(x => x.GetFtpKey(It.IsAny<string>())).ReturnsAsync("this is the long and strong key.");
            //Arrange
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfig");
            options = builder.Options;
            using TenantConfigContext ConfigContext = new TenantConfigContext(options);
            ConfigContext.Database.EnsureCreated();
            WorkQueue workQueue = new WorkQueue
            {
                ScheduleDate = DateTime.Now.AddDays(-1),
                EndDate = null,
                RetryCount = null,
                IsActive = true,
                Activity = new TenantConfig.Entity.Models.Activity
                {
                    ActivityTypeId = 1,
                    Template = new Template
                    {
                        Id = 1,
                        Body = "abcbcb"


                    },
                    ActivityTenantBinders = new List<ActivityTenantBinder>
                    {
                         new ActivityTenantBinder
                         {
                             Id=1,
                             ActivityId=1,
                             TenantId=1

                         }
                    },

                },
                Priority = 1,
                TenantId = 1,
                To = "rainmaker.rainsoft@gmail.com"

            };
            ConfigContext.Set<WorkQueue>().Add(workQueue);
            WorkQueueToken workQueueToken = new WorkQueueToken
            {
                WorkQueueId = 1,
                Key = "###FromEmail###",
                Value = "rainmaker.rainsoft@gmail.com"

            };
            ConfigContext.Set<WorkQueueToken>().Add(workQueueToken);
            ConfigContext.SaveChanges();
            mockSettingService.SetupSequence(x => x.GetSetting<string>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<bool>())).
                ReturnsAsync("127.0.0.1").ReturnsAsync("rainmaker.rainsoft@gmail.com").ReturnsAsync("CwZpE3Jf8gMtRFIYfqdv1j5DVpYYIEpOg4Vb1CbuJ4Q=");
            mockSettingService.Setup(x => x.GetSetting<int>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int>(), It.IsAny<bool>())).
             ReturnsAsync(8080);
            mockSettingService.Setup(x => x.GetSetting<bool>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<bool>(), It.IsAny<bool>())).
            ReturnsAsync(true);
            JobTypeModel jobTypeModel = new JobTypeModel();
            jobTypeModel.id = 1;
            jobTypeModel.isActive = true;
            jobTypeModel.name = "Email Reminder";

            mockEmailReminderService.Setup(x => x.GetJobType(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(jobTypeModel);

            EmailTemplate emailTemplate = new EmailTemplate();
            emailTemplate.id = 1;
            emailTemplate.tenantId = 1;
            emailTemplate.templateName = "Template #1";
            emailTemplate.templateDescription = "Sed ut perspiciatis unde omnis iste natus";
            emailTemplate.fromAddress = "aliya@texastrustloans.com";
            emailTemplate.CCAddress = "Ali@gmail.com,hasan@gmail.com";
            emailTemplate.toAddress = "prasla@gmail.com";
            emailTemplate.subject = "You have new tasks to complete for your Texas Trust Home Loans loan application";
            emailTemplate.emailBody = "Hi Javed,To continue your application, we need some more information.";
            emailTemplate.sortOrder = 1;

            mockEmailTemplateService.Setup(x => x.GetEmailReplacedToken(It.IsAny<EmailReminder>(), It.IsAny<string>())).ReturnsAsync(emailTemplate);

            List<EmailReminderLogModel> emailReminderLogModels = new List<EmailReminderLogModel>();

            EmailReminderLogModel emailReminderLogModel = new EmailReminderLogModel();
            emailReminderLogModel.tenantId = 1;
            emailReminderLogModel.loanApplicationId = 1;
            emailReminderLogModel.jobTypeId = 1;
            emailReminderLogModel.ReminderId = "6006cae813394a3e188324dc";

            emailReminderLogModels.Add(emailReminderLogModel);

            mockEmailReminderService.Setup(x => x.GetEmailreminderLogByDate(It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(emailReminderLogModels);

            mockRainmkerService.Setup(x => x.SendBorrowerEmail(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()));

            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/keystore/keystore?key=backgroundjobuser", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("rainsoft")
            });
            messages.Add("http://test.com/api/keystore/keystore?key=backgroundjobpwd", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("rainsoft")
            });
            messages.Add("http://test.com/api/identity/token/authorize", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"status\": null,\"data\": {\"token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNQ1UiLCJVc2VyUHJvZmlsZUlkIjoiMSIsIlVzZXJOYW1lIjoicmFpbnNvZnQiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicmFpbnNvZnQiLCJGaXJzdE5hbWUiOiJTeXN0ZW0iLCJMYXN0TmFtZSI6IkFkbWluaXN0cmF0b3IiLCJUZW5hbnRJZCI6IjEiLCJFbXBsb3llZUlkIjoiMSIsImV4cCI6MTYxMjEzMDIxOCwiaXNzIjoicmFpbnNvZnRmbiIsImF1ZCI6InJlYWRlcnMifQ.sjjF6HdHKN9r7LAo5se8mlBYCl2k0Pqo22UDFaI9Bwk\",\"refreshToken\": null,\"userProfileId\": 0,\"userName\": null,\"validFrom\": \"0001-01-01T00:00:00\",\"validTo\": \"0001-01-01T00:00:00\"},\"message\": null,\"code\": null}")
            });
            messages.Add("http://test.com/api/documentmanagement/emailreminder/getdocumentstatusbyloanids", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{\"loanApplicationId\": 1,\"isDocumentRemaining\": true}]")
            });
            messages.Add("http://test.com/api/documentmanagement/emailreminder/getemailreminderbyids", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{\"id\": \"6006cae813394a3e188324dc\",\"noOfDays\": \"2\",\"recurringTime\": \"0001-01-01T00:00:00\",\"isActive\":true,\"email\":{\"id\":\"6006cae813394a3e188324dd\",\"fromAddress\":\"###RequestorUserEmail###\",\"ccAddress\":\"###Co-BorrowerEmailAddress###\",\"subject\":\"subject\",\"emailBody\":\"<p>body</p>\"},\"LoanApplicationId\":1,\"EmailReminderLogId\":1}]")
            });
            messages.Add("http://test.com/api/documentmanagement/dashboardmcu/getpendingdocuments?loanapplicationid=1", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{\"id\": \"6006cae813394a3e188324dc\",\"requestId\": \"6006cae813394a3e188324de\",\"docId\": \"6006cae813394a3e188324df\",\"docName\":\"Pay slip\",\"docMessage\":\"Please upload your pay slip\",\"isRejected\":true,\"files\":[{\"id\":\"6006cae813394a3e188323dd\",\"clientName\":\"unnamed.jpg\",\"fileUploadedOn\":\"0001-01-01T00:00:00\",\"size\":\"325\",\"order\":\"1\"}]}]")
            });

            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            var handlerMock = new Mock<TestMessageHandler>(MockBehavior.Strict);
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
                    Content = new StringContent("rainsoft", Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(x => x.GetService(typeof(IKeyStoreService)))
                .Returns(mockKeyStoreService.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IUnitOfWork<TenantConfigContext>)))
                .Returns(new UnitOfWork<TenantConfigContext>(ConfigContext, new RepositoryProvider(new RepositoryFactories())));

            serviceProvider
                .Setup(x => x.GetService(typeof(IConfiguration)))
                .Returns(mockconfiguration.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(ISettingService)))
                .Returns(mockSettingService.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(ISmtpService)))
                .Returns(mockSmtpService.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(ILogger<HangfireBackgroundService>)))
                .Returns(Mock.Of<ILogger<HangfireBackgroundService>>());

            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactory.Object);
            IBackgroundService backgroundService = new HangfireBackgroundService(serviceProvider.Object);

            //Act

            await backgroundService.DispatchEmailJob();
        }

        [Fact]
        public async Task UpdateTenantBranchCache()
        {
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigUpdateTenantBranchCache");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);

            dataContext.Database.EnsureCreated();
            TenantUrl tenantUrl = new TenantUrl()
            {
                Id = 11,
                Url = "apply.lendova.com:5003",
                TenantId = 11,
                BranchId = 11,
                TypeId = 1


            };
            dataContext.Set<TenantUrl>().Add(tenantUrl);

            Tenant tenant = new Tenant
            {
                Id = 11,
                Code = "Lendova",
                Name = "Lendova",
                Branches = new List<Branch>
                {
                    new Branch
                {
                Id = 11,
                Code = "lendova",
                TenantId = 11,
                Name = "Lendova",
                IsCorporate=true,
                IsActive=true
                    }
                }
            };
            dataContext.Set<Tenant>().Add(tenant);

            BranchLoanOfficerBinder branchLoanOfficerBinder = new BranchLoanOfficerBinder
            {
                Id = 11,
                BranchId = 11,
                EmployeeId = 11
            };
            dataContext.Set<BranchLoanOfficerBinder>().Add(branchLoanOfficerBinder);

            Employee employee = new Employee
            {
                Id = 11,
                Code = "44",
                IsActive = true,
                IsLoanOfficer = true
            };
            dataContext.Set<Employee>().Add(employee);

            dataContext.SaveChanges();

            var redisService = new Mock<IConnectionMultiplexer>();

            var dbProvider = new Mock<IDatabase>();
            redisService.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(dbProvider.Object);

            dbProvider.Setup(x => x.KeyDeleteAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>())).Verifiable();
            dbProvider.Setup(x => x.HashSetAsync(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), It.IsAny<RedisValue>(), It.IsAny<When>(), It.IsAny<CommandFlags>())).Verifiable();

            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(x => x.GetService(typeof(IUnitOfWork<TenantConfigContext>)))
                .Returns(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())));

            serviceProvider
                .Setup(x => x.GetService(typeof(IConnectionMultiplexer)))
                .Returns(redisService.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(ILogger<HangfireBackgroundService>)))
                .Returns(Mock.Of<ILogger<HangfireBackgroundService>>());

            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactory.Object);

            var service = new HangfireBackgroundService(serviceProvider.Object);
            await service.UpdateTenantBranchCache();
        }
        [Fact]
        public async Task UpdateSettingCache()
        {
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigUpdateSettingCache");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);

            dataContext.Database.EnsureCreated();
            var branch = new Branch
            {
                Id = 100,
                Code = "lendova",
                TenantId = 1,
                Name = "Lendova",
                IsCorporate = true,
                IsActive = true
            };

            dataContext.Set<Branch>().Add(branch);

            TenantConfig.Entity.Models.Setting b1 = new TenantConfig.Entity.Models.Setting
            {
                Id = 100,
                Name = "test100",
                Value = "test100",
                BranchId = 100,
                IsActive = true
            };
            dataContext.Set<TenantConfig.Entity.Models.Setting>().Add(b1);

            TenantConfig.Entity.Models.Setting b2 = new TenantConfig.Entity.Models.Setting
            {
                Id = 101,
                Name = "test101",
                Value = "test101",
                TenantId = 101,
                IsActive = true
            };
            dataContext.Set<TenantConfig.Entity.Models.Setting>().Add(b2);

            TenantConfig.Entity.Models.Setting b3 = new TenantConfig.Entity.Models.Setting
            {
                Id = 102,
                Name = "test102",
                Value = "test102",
                IsActive = true
            };
            dataContext.Set<TenantConfig.Entity.Models.Setting>().Add(b3);

            dataContext.SaveChanges();

            

            var redisService = new Mock<IConnectionMultiplexer>();
            

            var dbProvider = new Mock<IDatabase>();
            redisService.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(dbProvider.Object);

            dbProvider.Setup(x => x.KeyDeleteAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>())).Verifiable();
            dbProvider.Setup(x => x.HashSetAsync(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), It.IsAny<RedisValue>(), It.IsAny<When>(), It.IsAny<CommandFlags>())).Verifiable();
            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(x => x.GetService(typeof(IUnitOfWork<TenantConfigContext>)))
                .Returns(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())));

            serviceProvider
                .Setup(x => x.GetService(typeof(IConnectionMultiplexer)))
                .Returns(redisService.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(ILogger<HangfireBackgroundService>)))
                .Returns(Mock.Of<ILogger<HangfireBackgroundService>>());

            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactory.Object);
            var service = new HangfireBackgroundService(serviceProvider.Object);
            await service.UpdateSettingCache();
        }
        [Fact]
        public async Task UpdateStringResourceCache()
        {
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigUpdateStringResourceCache");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);

            dataContext.Database.EnsureCreated();
            var branch = new Branch
            {
                Id = 102,
                Code = "lendova",
                TenantId = 1,
                Name = "Lendova",
                IsCorporate = true,
                IsActive = true
            };

            dataContext.Set<Branch>().Add(branch);

            StringResource b1 = new StringResource
            {
                Id = 100,
                Name = "test100",
                Value = "test100",
                BranchId = 100,
                IsActive = true
            };
            dataContext.Set<StringResource>().Add(b1);

            StringResource b2 = new StringResource
            {
                Id = 101,
                Name = "test101",
                Value = "test101",
                TenantId = 101,
                IsActive = true
            };
            dataContext.Set<StringResource>().Add(b2);

            StringResource b3 = new StringResource
            {
                Id = 102,
                Name = "test102",
                Value = "test102",
                IsActive = true
            };
            dataContext.Set<StringResource>().Add(b3);

            dataContext.SaveChanges();

            var redisService = new Mock<IConnectionMultiplexer>();

            var dbProvider = new Mock<IDatabase>();
            redisService.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(dbProvider.Object);

            dbProvider.Setup(x => x.KeyDeleteAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>())).Verifiable();
            dbProvider.Setup(x => x.HashSetAsync(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), It.IsAny<RedisValue>(), It.IsAny<When>(), It.IsAny<CommandFlags>())).Verifiable();
            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(x => x.GetService(typeof(IUnitOfWork<TenantConfigContext>)))
                .Returns(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())));

            serviceProvider
                .Setup(x => x.GetService(typeof(IConnectionMultiplexer)))
                .Returns(redisService.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(ILogger<HangfireBackgroundService>)))
                .Returns(Mock.Of<ILogger<HangfireBackgroundService>>());

            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactory.Object);
            var service = new HangfireBackgroundService(serviceProvider.Object);
            await service.UpdateStringResourceCache();
        }
        [Fact]
        public async Task TestPollAndSendNotification()
        {
            var notificationService = new Mock<INotificationService>();
            notificationService.Setup(x => x.SendNotification(It.IsAny<Notification.Model.NotificationModel>())).ReturnsAsync(true);
            var redisService = new Mock<IConnectionMultiplexer>();
            var dbProvider = new Mock<IDatabase>();
            redisService.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(dbProvider.Object);

            NotificationModel model = new NotificationModel();
            model.UsersToSendList = new List<int>() { 1, 2, 3, 4 };
            model.DateTime = DateTime.UtcNow.AddHours(-1);
            model.NotificationType = 1;
            model.tenantId = 1;

            dbProvider.Setup(x => x.ListLengthAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>())).ReturnsAsync(1);
            dbProvider.Setup(x => x.ListGetByIndexAsync(It.IsAny<RedisKey>(), It.IsAny<long>(), It.IsAny<CommandFlags>())).ReturnsAsync(Newtonsoft.Json.JsonConvert.SerializeObject(model));
            dbProvider.Setup(x => x.ListRemoveAsync(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), It.IsAny<long>(), It.IsAny<CommandFlags>())).Verifiable();

            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(x => x.GetService(typeof(INotificationService)))
                .Returns(notificationService.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IConnectionMultiplexer)))
                .Returns(redisService.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(ILogger<HangfireBackgroundService>)))
                .Returns(Mock.Of<ILogger<HangfireBackgroundService>>());

            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactory.Object);

            var service = new HangfireBackgroundService(serviceProvider.Object);
            await service.PollAndSendNotification();
        }
    }
}
