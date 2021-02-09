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

            mockEmailReminderService.Setup(x=>x.GetJobType(It.IsAny<int>(),It.IsAny<int>())).ReturnsAsync(jobTypeModel);

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

            mockEmailReminderService.Setup(x=>x.GetEmailreminderLogByDate(It.IsAny<DateTime>(),It.IsAny<int>(),It.IsAny<int>())).ReturnsAsync(emailReminderLogModels);

            mockRainmkerService.Setup(x=>x.SendBorrowerEmail(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()));

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
            }) ;
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

            IBackgroundService backgroundService = new HangfireBackgroundService(httpClient, mockEmailReminderService.Object, mockconfiguration.Object, mockEmailTemplateService.Object, mockRainmkerService.Object, Mock.Of<ILogger<HangfireBackgroundService>>());

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

            IBackgroundService backgroundService = new HangfireBackgroundService(httpClient, mockEmailReminderService.Object, mockconfiguration.Object, mockEmailTemplateService.Object, mockRainmkerService.Object, Mock.Of<ILogger<HangfireBackgroundService>>());

            //Act

            await backgroundService.EmailReminderJob();
        }
    }
}
