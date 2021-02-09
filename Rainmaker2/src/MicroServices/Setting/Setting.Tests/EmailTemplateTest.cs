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
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace Setting.Tests
{
    public class EmailTemplateTest
    {
        #region Controllers
        [Fact]
        public async Task TestEnableDisableEmailRemindersByStatusUpdateIdController()
        {
            Mock<IEmailTemplateService> mock = new Mock<IEmailTemplateService>();
            List<EmailTemplate> lstEmailTemplate = new List<EmailTemplate>();
           
            lstEmailTemplate.Add(new EmailTemplate() { id = 1, tenantId = 1, templateName = "Template #1", templateDescription = "Key for enabling user email address Key for enabling user email address", fromAddress = "###LoginUserEmail###", CCAddress = "Ali@gmail.com,hasan@gmail.com", subject = "You have new tasks to complete for your ###BusinessUnitName### loan application", emailBody = "<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n", sortOrder = 1 });

            mock.Setup(x => x.GetEmailTemplates(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(lstEmailTemplate);
            var emailReminderController = new EmailTemplateController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;

            IActionResult result = await emailReminderController.GetEmailTemplates();
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestInsertEmailTemplateController()
        {
            Mock<IEmailTemplateService> mock = new Mock<IEmailTemplateService>();

            mock.Setup(x => x.InsertEmailTemplate(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(1);
            var emailReminderController = new EmailTemplateController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;
            InsertEmailTemplateModel insertEmailTemplateModel = new InsertEmailTemplateModel();
            insertEmailTemplateModel.templateTypeId = 1;
            insertEmailTemplateModel.templateName = "Template";
            insertEmailTemplateModel.templateDescription = "Template Description";
            insertEmailTemplateModel.fromAddress = "ahc@ahc.com";
            insertEmailTemplateModel.toAddress = "ahc@ahc.com";
            insertEmailTemplateModel.CCAddress = "Ali@gmail.com,hasan@gmail.com";
            IActionResult result = await emailReminderController.InsertEmailTemplate(insertEmailTemplateModel);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestUpdateEmailTemplateController()
        {
            Mock<IEmailTemplateService> mock = new Mock<IEmailTemplateService>();
           
            mock.Setup(x => x.UpdateEmailTemplate(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()));
            var emailReminderController = new EmailTemplateController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;
            UpdateEmailTemplateModel updateEmailTemplate = new UpdateEmailTemplateModel();
            updateEmailTemplate.templateName = "Template";
            updateEmailTemplate.templateDescription = "Template Description";
            updateEmailTemplate.fromAddress = "ahc@ahc.com";
            updateEmailTemplate.toAddress = "ahc@ahc.com";
            updateEmailTemplate.CCAddress = "Ali@gmail.com,hasan@gmail.com";
            IActionResult result = await emailReminderController.UpdateEmailTemplate(updateEmailTemplate);

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestDeleteEmailTemplateController()
        {
            Mock<IEmailTemplateService> mock = new Mock<IEmailTemplateService>();

            mock.Setup(x => x.DeleteEmailTemplate(It.IsAny<int>(), It.IsAny<int>()));
            var emailReminderController = new EmailTemplateController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;
            DeleteTemplateModel deleteTemplateModel = new DeleteTemplateModel();
            deleteTemplateModel.id = 1;
            IActionResult result = await emailReminderController.DeleteEmailTemplate(deleteTemplateModel);

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestUpdateSortOrderController()
        {
            Mock<IEmailTemplateService> mock = new Mock<IEmailTemplateService>();

            mock.Setup(x => x.UpdateSortOrder(It.IsAny<List<EmailTemplate>>(), It.IsAny<int>()));
            var emailReminderController = new EmailTemplateController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;
            List<EmailTemplate> emailTemplates = new List<EmailTemplate>();
            EmailTemplate emailTemplate = new EmailTemplate();
            emailTemplate.id = 1;
            emailTemplates.Add(emailTemplate);
            IActionResult result = await emailReminderController.UpdateSortOrder(emailTemplates);

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestGetEmailTemplateByIdController()
        {
            Mock<IEmailTemplateService> mock = new Mock<IEmailTemplateService>();
            EmailTemplate emailTemplate1=new EmailTemplate();
            emailTemplate1.id = 1;
            emailTemplate1.tenantId = 1;
            emailTemplate1.templateName = "Template #1";
            emailTemplate1.templateDescription = "Key for enabling user email address Key for enabling user email address";
            emailTemplate1.fromAddress = "###LoginUserEmail###";
            emailTemplate1.CCAddress = "Ali@gmail.com,hasan@gmail.com";
            emailTemplate1.subject = "You have new tasks to complete for your ###BusinessUnitName### loan application";
            emailTemplate1.emailBody = "<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n";
            emailTemplate1.sortOrder = 1;

            mock.Setup(x => x.GetEmailTemplateById(It.IsAny<int>())).ReturnsAsync(emailTemplate1);
            var emailReminderController = new EmailTemplateController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;

            TemplateIdModel templateIdModel = new TemplateIdModel();
            templateIdModel.loanApplicationId = 1;
            templateIdModel.id = 1;
            IActionResult result = await emailReminderController.GetEmailTemplateById(templateIdModel);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestGetRenderEmailTemplateByIdController()
        {
            Mock<IEmailTemplateService> mock = new Mock<IEmailTemplateService>();
            EmailTemplate emailTemplate1 = new EmailTemplate();
            emailTemplate1.id = 1;
            emailTemplate1.tenantId = 1;
            emailTemplate1.templateName = "Template #1";
            emailTemplate1.templateDescription = "Key for enabling user email address Key for enabling user email address";
            emailTemplate1.fromAddress = "###LoginUserEmail###";
            emailTemplate1.CCAddress = "Ali@gmail.com,hasan@gmail.com";
            emailTemplate1.subject = "You have new tasks to complete for your ###BusinessUnitName### loan application";
            emailTemplate1.emailBody = "<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n";
            emailTemplate1.sortOrder = 1;

            mock.Setup(x => x.GetRenderEmailTemplateById(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(emailTemplate1);
            var emailReminderController = new EmailTemplateController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;

            TemplateIdModel templateIdModel = new TemplateIdModel();
            templateIdModel.loanApplicationId = 1;
            templateIdModel.id = 1;
            IActionResult result = await emailReminderController.GetRenderEmailTemplateById(templateIdModel);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestGetTokensController()
        {
            Mock<IEmailTemplateService> mock = new Mock<IEmailTemplateService>();

            mock.Setup(x => x.GetTokens());
            var emailReminderController = new EmailTemplateController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            emailReminderController.ControllerContext = context;

            IActionResult result = await emailReminderController.GetTokens();

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion
     
    }
}
