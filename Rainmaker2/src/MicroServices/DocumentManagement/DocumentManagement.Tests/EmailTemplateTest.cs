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
    public class EmailTemplateTest
    {
        [Fact]
        public async Task TestGetTokensController()
        {
            //Arrange
            Mock<IEmailTemplateService> mock = new Mock<IEmailTemplateService>();
            List<TokenModel> lstTokenModels = new List<TokenModel>();

            lstTokenModels.Add(new TokenModel() { id = "5fa02360c873e00f73864f36", description = "Key for enabling user email address Key for enabling user email address", key = "LoginUserEmail", name = "Login User Email", symbol = "###LoginUserEmail###" });

            mock.Setup(x => x.GetTokens()).ReturnsAsync(lstTokenModels);

            var emailTemplateController = new EmailTemplateController(mock.Object);

            var httpContext = new Mock<HttpContext>();

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            emailTemplateController.ControllerContext = context;

            //Act
            IActionResult result = await emailTemplateController.GetTokens();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as List<TokenModel>;
            Assert.Single(content);
            Assert.Equal("5fa02360c873e00f73864f36", content[0].id);
            Assert.Equal("Key for enabling user email address Key for enabling user email address", content[0].description);
            Assert.Equal("LoginUserEmail", content[0].key);
            Assert.Equal("Login User Email", content[0].name);
            Assert.Equal("###LoginUserEmail###", content[0].symbol);
        }
        [Fact]
        public async Task TestGetEmailTemplatesController()
        {
            //Arrange
            Mock<IEmailTemplateService> mock = new Mock<IEmailTemplateService>();
            List<Model.EmailTemplate> lstEmailTemplate = new List<Model.EmailTemplate>();

            lstEmailTemplate.Add(new Model.EmailTemplate()
            {
                id = "5fa020214c2ff92af0a1c85f",
                tenantId = 1,
                templateName = "Template #1",
                templateDescription = "Sed ut perspiciatis unde omnis iste natus",
                fromAddress = "###LoginUserEmail###",
                CCAddress = "Ali@gmail.com,hasan@gmail.com",
                subject = "You have new tasks to complete for your ###BusinessUnitName### loan application",
                emailBody = "<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n",
                sortOrder = 1
            });

            mock.Setup(x => x.GetEmailTemplates(It.IsAny<int>())).ReturnsAsync(lstEmailTemplate);

            var emailTemplateController = new EmailTemplateController(mock.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            emailTemplateController.ControllerContext = context;

            //Act
            IActionResult result = await emailTemplateController.GetEmailTemplates();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as List<Model.EmailTemplate>;
            Assert.Single(content);
            Assert.Equal("5fa020214c2ff92af0a1c85f", content[0].id);
            Assert.Equal(1, content[0].tenantId);
            Assert.Equal("Template #1", content[0].templateName);
            Assert.Equal("Sed ut perspiciatis unde omnis iste natus", content[0].templateDescription);
            Assert.Equal("###LoginUserEmail###", content[0].fromAddress);
            Assert.Equal("Ali@gmail.com,hasan@gmail.com", content[0].CCAddress);
            Assert.Equal("You have new tasks to complete for your ###BusinessUnitName### loan application", content[0].subject);
            Assert.Equal("<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n", content[0].emailBody);
            Assert.Equal(1, content[0].sortOrder);

        }
        [Fact]
        public async Task TestGetEmailTemplateByIdController()
        {
            //Arrange
            Mock<IEmailTemplateService> mock = new Mock<IEmailTemplateService>();
            Model.EmailTemplate emailTemplate = new Model.EmailTemplate();

            emailTemplate.id = "5fa020214c2ff92af0a1c85f";
            emailTemplate.tenantId = 1;
            emailTemplate.templateName = "Template #1";
            emailTemplate.templateDescription = "Sed ut perspiciatis unde omnis iste natus";
            emailTemplate.fromAddress = "###LoginUserEmail###";
            emailTemplate.CCAddress = "Ali@gmail.com,hasan@gmail.com";
            emailTemplate.subject = "You have new tasks to complete for your ###BusinessUnitName### loan application";
            emailTemplate.emailBody = "<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n";
            emailTemplate.sortOrder = 1;

            mock.Setup(x => x.GetEmailTemplateById(It.IsAny<string>())).ReturnsAsync(emailTemplate);

            var emailTemplateController = new EmailTemplateController(mock.Object);

            var httpContext = new Mock<HttpContext>();

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            emailTemplateController.ControllerContext = context;

            //Act
            EmailTemplateIdModel emailTemplateIdModel = new EmailTemplateIdModel();
            emailTemplateIdModel.id = "5fa020214c2ff92af0a1c85f";
            IActionResult result = await emailTemplateController.GetEmailTemplateById(emailTemplateIdModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as Model.EmailTemplate;
            Assert.Equal("5fa020214c2ff92af0a1c85f", content.id);
            Assert.Equal(1, content.tenantId);
            Assert.Equal("Template #1", content.templateName);
            Assert.Equal("Sed ut perspiciatis unde omnis iste natus", content.templateDescription);
            Assert.Equal("###LoginUserEmail###", content.fromAddress);
            Assert.Equal("Ali@gmail.com,hasan@gmail.com", content.CCAddress);
            Assert.Equal("You have new tasks to complete for your ###BusinessUnitName### loan application", content.subject);
            Assert.Equal("<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n", content.emailBody);
            Assert.Equal(1, content.sortOrder);
        }
        [Fact]
        public async Task TestGetDraftEmailTemplateByIdController()
        {
            //Arrange
            Mock<IEmailTemplateService> mock = new Mock<IEmailTemplateService>();
            Model.EmailTemplate emailTemplate = new Model.EmailTemplate();

            emailTemplate.id = "5fa020214c2ff92af0a1c85f";
            emailTemplate.tenantId = 1;
            emailTemplate.templateName = "Template #1";
            emailTemplate.templateDescription = "Sed ut perspiciatis unde omnis iste natus";
            emailTemplate.fromAddress = "aliya@texastrustloans.com";
            emailTemplate.CCAddress = "Ali@gmail.com,hasan@gmail.com";
            emailTemplate.toAddress = "prasla@gmail.com";
            emailTemplate.subject = "You have new tasks to complete for your Texas Trust Home Loans loan application";
            emailTemplate.emailBody = "Hi Javed,To continue your application, we need some more information.";
            emailTemplate.sortOrder = 1;

            mock.Setup(x => x.GetDraftEmailTemplateById(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(emailTemplate);

            var emailTemplateController = new EmailTemplateController(mock.Object);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            emailTemplateController.ControllerContext = context;

            //Act
            RenderTemplateIdModel renderTemplateIdModel = new RenderTemplateIdModel();
            renderTemplateIdModel.id = "5fa020214c2ff92af0a1c85f";
            IActionResult result = await emailTemplateController.GetDraftEmailTemplateById(renderTemplateIdModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as Model.EmailTemplate;
            Assert.Equal("5fa020214c2ff92af0a1c85f", content.id);
            Assert.Equal(1, content.tenantId);
            Assert.Equal("Template #1", content.templateName);
            Assert.Equal("Sed ut perspiciatis unde omnis iste natus", content.templateDescription);
            Assert.Equal("aliya@texastrustloans.com", content.fromAddress);
            Assert.Equal("Ali@gmail.com,hasan@gmail.com", content.CCAddress);
            Assert.Equal("prasla@gmail.com", content.toAddress);
            Assert.Equal("You have new tasks to complete for your Texas Trust Home Loans loan application", content.subject);
            Assert.Equal("Hi Javed,To continue your application, we need some more information.", content.emailBody);
            Assert.Equal(1, content.sortOrder);
        }
        [Fact]
        public async Task TestInsertEmailTemplateController()
        {
            //Arrange
            Mock<IEmailTemplateService> mock = new Mock<IEmailTemplateService>();

            mock.Setup(x => x.InsertEmailTemplate(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync("5fa020214c2ff92af0a1c85f");
            var controller = new EmailTemplateController(mock.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            InsertEmailTemplateModel insertEmailTemplateModel = new InsertEmailTemplateModel();

            insertEmailTemplateModel.templateDescription = "Sed ut perspiciatis unde omnis iste natus";
            insertEmailTemplateModel.fromAddress = "###LoginUserEmail###";
            insertEmailTemplateModel.CCAddress = "Ali@gmail.com,hasan@gmail.com";
            insertEmailTemplateModel.subject = "You have new tasks to complete for your ###BusinessUnitName### loan application";
            insertEmailTemplateModel.emailBody = "<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n";

            IActionResult result = await controller.InsertEmailTemplate(insertEmailTemplateModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestUpdateEmailTemplateTrueController()
        {
            //Arrange
            Mock<IEmailTemplateService> mock = new Mock<IEmailTemplateService>();

            mock.Setup(x => x.UpdateEmailTemplate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(true);
            var controller = new EmailTemplateController(mock.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            UpdateEmailTemplateModel updateEmailTemplateModel = new UpdateEmailTemplateModel();

            updateEmailTemplateModel.templateDescription = "Sed ut perspiciatis unde omnis iste natus";
            updateEmailTemplateModel.fromAddress = "###LoginUserEmail###";
            updateEmailTemplateModel.CCAddress = "Ali@gmail.com,hasan@gmail.com";
            updateEmailTemplateModel.subject = "You have new tasks to complete for your ###BusinessUnitName### loan application";
            updateEmailTemplateModel.emailBody = "<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n";

            IActionResult result = await controller.UpdateEmailTemplate(updateEmailTemplateModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestUpdateEmailTemplateFalseController()
        {
            //Arrange
            Mock<IEmailTemplateService> mock = new Mock<IEmailTemplateService>();

            mock.Setup(x => x.UpdateEmailTemplate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(false);
            var controller = new EmailTemplateController(mock.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            UpdateEmailTemplateModel updateEmailTemplateModel = new UpdateEmailTemplateModel();

            updateEmailTemplateModel.templateDescription = "Sed ut perspiciatis unde omnis iste natus";
            updateEmailTemplateModel.fromAddress = "###LoginUserEmail###";
            updateEmailTemplateModel.CCAddress = "Ali@gmail.com,hasan@gmail.com";
            updateEmailTemplateModel.subject = "You have new tasks to complete for your ###BusinessUnitName### loan application";
            updateEmailTemplateModel.emailBody = "<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n";

            IActionResult result = await controller.UpdateEmailTemplate(updateEmailTemplateModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task TestUpdateSortOrderController()
        {
            //Arrange
            Mock<IEmailTemplateService> mock = new Mock<IEmailTemplateService>();

            mock.Setup(x => x.UpdateSortOrder(It.IsAny<List<TemplateSortModel>>()));
            var controller = new EmailTemplateController(mock.Object);

            var httpContext = new Mock<HttpContext>();

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            List<TemplateSortModel> lstTemplates = new List<TemplateSortModel>();
            lstTemplates.Add(new TemplateSortModel()
            {
                id = "5fa02360c873e00f73864f36",
                tenantId = 1,
                templateName = "Template #1",
                templateDescription = "Sed ut perspiciatis unde omnis iste natus",
                fromAddress = "###LoginUserEmail###",
                CCAddress = "Ali@gmail.com,hasan@gmail.com",
                subject = "You have new tasks to complete for your ###BusinessUnitName### loan application",
                emailBody = "<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n",
                sortOrder = 1
            });

            IActionResult result = await controller.UpdateSortOrder(lstTemplates);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestDeleteEmailTemplateTrueController()
        {
            //Arrange
            Mock<IEmailTemplateService> mock = new Mock<IEmailTemplateService>();

            mock.Setup(x => x.DeleteEmailTemplate(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(true);
            var controller = new EmailTemplateController(mock.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            DeleteEmailTemplateModel deleteEmailTemplateModel = new DeleteEmailTemplateModel();

            deleteEmailTemplateModel.id = "5fa020214c2ff92af0a1c85f";

            IActionResult result = await controller.DeleteEmailTemplate(deleteEmailTemplateModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestDeleteEmailTemplateFalseController()
        {
            //Arrange
            Mock<IEmailTemplateService> mock = new Mock<IEmailTemplateService>();

            mock.Setup(x => x.DeleteEmailTemplate(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(false);
            var controller = new EmailTemplateController(mock.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            DeleteEmailTemplateModel deleteEmailTemplateModel = new DeleteEmailTemplateModel();

            deleteEmailTemplateModel.id = "5fa020214c2ff92af0a1c85f";

            IActionResult result = await controller.DeleteEmailTemplate(deleteEmailTemplateModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task TestGetEmailTemplatesService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.EmailTemplate>> mockCollection = new Mock<IMongoCollection<Entity.EmailTemplate>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorEmailTemplates = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            { new BsonDocument
                {
                     { "_id" , "5fa020214c2ff92af0a1c85f" },
                     { "tenantId" ,1},
                     { "templateName" , "Template #1"},
                     { "templateDescription","Sed ut perspiciatis unde omnis iste natus" },
                     { "fromAddress" , "###LoginUserEmail###"},
                     { "toAddress" , BsonString.Empty},
                     { "CCAddress" , "Ali@gmail.com,hasan@gmail.com"},
                     { "subject" , "You have new tasks to complete for your ###BusinessUnitName### loan application"},
                     { "emailBody" , "<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n"},
                     { "sortOrder" , 1},
                 }
            ,  new BsonDocument
             {
                     { "_id" , "5fa0202d4c2ff92af0a1c860" },
                     { "tenantId" ,1},
                     { "templateName" , "Template #2"},
                     { "templateDescription","Decided by System Administrator" },
                     { "fromAddress" , "###LoginUserEmail###"},
                     { "toAddress" , BsonString.Empty},
                     { "CCAddress" , "Salman@gmail.com,hasan@gmail.com"},
                     { "subject" , "Email Template"},
                     { "emailBody" , "<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n"},
                     { "sortOrder" , 2},
                }
            };

            mockCursorEmailTemplates.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorEmailTemplates.SetupGet(x => x.Current).Returns(list);

            mockCollection.SetupSequence(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.EmailTemplate, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorEmailTemplates.Object);

            mockdb.Setup(x => x.GetCollection<Entity.EmailTemplate>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new EmailTemplateService(mock.Object, null, null);

            //Act
            List<Model.EmailTemplate> dto = await service.GetEmailTemplates(1);

            //Assert
            Assert.NotNull(dto);
            Assert.Equal("5fa020214c2ff92af0a1c85f", dto[0].id);
            Assert.Equal("Template #1", dto[0].templateName);
            Assert.Equal("Sed ut perspiciatis unde omnis iste natus", dto[0].templateDescription);
            Assert.Equal("Salman@gmail.com,hasan@gmail.com", dto[1].CCAddress);
            Assert.Equal("Email Template", dto[1].subject);
            Assert.Equal("<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n", dto[1].emailBody);
            Assert.Equal(2, dto[1].sortOrder);
        }
        [Fact]
        public async Task TestGetTokensService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.TokenParam>> mockCollection = new Mock<IMongoCollection<Entity.TokenParam>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorTokens = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            { new BsonDocument
                {
                     { "_id" , "5fa02360c873e00f73864f36" },
                     { "name" ,"Login User Email"},
                     { "symbol" , "###LoginUserEmail###"},
                     { "description","Key for enabling user email address Key for enabling user email address"},
                     { "key" , "LoginUserEmail"}
                 }
            ,  new BsonDocument
             {
                     { "_id" , "5fa0236fc873e00f73864f6e" },
                     { "name" ,"Customer First Name"},
                     { "symbol" , "###CustomerFirstname###"},
                     { "description","Key for enabling customer first name" },
                     { "key" , "CustomerFirstName"}
                }
            };

            mockCursorTokens.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorTokens.SetupGet(x => x.Current).Returns(list);

            mockCollection.SetupSequence(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.TokenParam, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorTokens.Object);

            mockdb.Setup(x => x.GetCollection<Entity.TokenParam>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new EmailTemplateService(mock.Object, null, null);

            //Act
            List<Model.TokenModel> dto = await service.GetTokens();

            //Assert
            Assert.NotNull(dto);
            Assert.Equal("5fa02360c873e00f73864f36", dto[0].id);
            Assert.Equal("Login User Email", dto[0].name);
            Assert.Equal("###LoginUserEmail###", dto[0].symbol);
            Assert.Equal("5fa0236fc873e00f73864f6e", dto[1].id);
            Assert.Equal("###CustomerFirstname###", dto[1].symbol);
            Assert.Equal("Key for enabling customer first name", dto[1].description);
            Assert.Equal("CustomerFirstName", dto[1].key);
        }
        [Fact]
        public async Task TestGetEmailTemplateByIdService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.EmailTemplate>> mockCollection = new Mock<IMongoCollection<Entity.EmailTemplate>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorEmailTemplates = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            { new BsonDocument
                {
                     { "_id" , "5fa020214c2ff92af0a1c85f" },
                     { "tenantId" ,1},
                     { "templateName" , "Template #1"},
                     { "templateDescription","Sed ut perspiciatis unde omnis iste natus" },
                     { "fromAddress" , "###LoginUserEmail###"},
                     { "toAddress" , BsonString.Empty},
                     { "CCAddress" , "Ali@gmail.com,hasan@gmail.com"},
                     { "subject" , "You have new tasks to complete for your ###BusinessUnitName### loan application"},
                     { "emailBody" , "<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n"},
                     { "sortOrder" , 1},
                 }
            };

            mockCursorEmailTemplates.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorEmailTemplates.SetupGet(x => x.Current).Returns(list);

            mockCollection.SetupSequence(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.EmailTemplate, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorEmailTemplates.Object);

            mockdb.Setup(x => x.GetCollection<Entity.EmailTemplate>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new EmailTemplateService(mock.Object, null, null);

            //Act
            Model.EmailTemplate dto = await service.GetEmailTemplateById("5fa020214c2ff92af0a1c85f");

            //Assert
            Assert.NotNull(dto);
            Assert.Equal("5fa020214c2ff92af0a1c85f", dto.id);
            Assert.Equal("Template #1", dto.templateName);
            Assert.Equal("Sed ut perspiciatis unde omnis iste natus", dto.templateDescription);
            Assert.Equal("Ali@gmail.com,hasan@gmail.com", dto.CCAddress);
            Assert.Equal("You have new tasks to complete for your ###BusinessUnitName### loan application", dto.subject);
            Assert.Equal("<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n", dto.emailBody);
            Assert.Equal(1, dto.sortOrder);
        }

        [Fact]
        public async Task TestInsertEmailTemplateService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.EmailTemplate>> mockCollection = new Mock<IMongoCollection<Entity.EmailTemplate>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorMaxSortOrder = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            { new BsonDocument
                {
                     { "maxSortOrder" , 1}
                 }
            };
            mockCursorMaxSortOrder.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorMaxSortOrder.SetupGet(x => x.Current).Returns(list);

            mockCollection.SetupSequence(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.EmailTemplate, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorMaxSortOrder.Object);

            mockdb.Setup(x => x.GetCollection<Entity.EmailTemplate>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mockCollection.Setup(s => s.InsertOneAsync(It.IsAny<Entity.EmailTemplate>(), It.IsAny<InsertOneOptions>(), It.IsAny<System.Threading.CancellationToken>()));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act
            IEmailTemplateService emailTemplateService = new EmailTemplateService(mock.Object,null,null);

            await emailTemplateService.InsertEmailTemplate(1, "Template #1","Sed ut perspiciatis unde omnis iste natus", "###LoginUserEmail###",null, "Ali@gmail.com,hasan@gmail.com", "You have new tasks to complete for your ###BusinessUnitName### loan application", "<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n",1);
        }

        [Fact]
        public async Task TestDeleteEmailTemplateService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.EmailTemplate>> mockCollection = new Mock<IMongoCollection<Entity.EmailTemplate>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            mockdb.Setup(x => x.GetCollection<Entity.EmailTemplate>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Entity.EmailTemplate>>(), It.IsAny<UpdateDefinition<Entity.EmailTemplate>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act
            IEmailTemplateService emailTemplateService = new EmailTemplateService(mock.Object, null, null);
            bool result = await emailTemplateService.DeleteEmailTemplate("5fa020214c2ff92af0a1c85f", 1);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task TestUpdateEmailTemplateService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.EmailTemplate>> mockCollection = new Mock<IMongoCollection<Entity.EmailTemplate>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            mockdb.Setup(x => x.GetCollection<Entity.EmailTemplate>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Entity.EmailTemplate>>(), It.IsAny<UpdateDefinition<Entity.EmailTemplate>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act
            IEmailTemplateService emailTemplateService = new EmailTemplateService(mock.Object, null, null);
            bool result = await emailTemplateService.UpdateEmailTemplate("5fa020214c2ff92af0a1c85f", "Template #1", "Sed ut perspiciatis unde omnis iste natus", "###LoginUserEmail###", "Ali@gmail.com,hasan@gmail.com", "You have new tasks to complete for your ###BusinessUnitName### loan application", "<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n", 1);

            //Assert
            Assert.True(result);
        }
        [Fact]
        public async Task TestUpdateSortOrderService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.EmailTemplate>> mockCollection = new Mock<IMongoCollection<Entity.EmailTemplate>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            mockdb.Setup(x => x.GetCollection<Entity.EmailTemplate>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Entity.EmailTemplate>>(), It.IsAny<UpdateDefinition<Entity.EmailTemplate>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>()));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act
            IEmailTemplateService emailTemplateService = new EmailTemplateService(mock.Object, null, null);
            List<TemplateSortModel> lstTempalteSortModel = new List<TemplateSortModel>();
            lstTempalteSortModel.Add(new TemplateSortModel() { id = "5fa02360c873e00f73864f36", tenantId = 1 ,templateName = "Template #1", templateDescription = "Key for enabling user email address Key for enabling user email address", fromAddress = "###LoginUserEmail###", CCAddress = "Ali@gmail.com,hasan@gmail.com", subject = "You have new tasks to complete for your ###BusinessUnitName### loan application",emailBody= "<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n",sortOrder = 1});
            lstTempalteSortModel.Add(new TemplateSortModel() { id = "5fa0202d4c2ff92af0a1c860", tenantId = 1, templateName = "Template #2", templateDescription = "Key for enabling user email address Key for enabling user email address", fromAddress = "###LoginUserEmail###", CCAddress = "Ali@gmail.com,hasan@gmail.com", subject = "You have new tasks to complete for your ###BusinessUnitName### loan application", emailBody = "<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n", sortOrder = 2 });
            await emailTemplateService.UpdateSortOrder(lstTempalteSortModel);
        }
        [Fact]
        public async Task TestGetDraftEmailTemplateByIdService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Request>> mockCollection = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorDraftEmail = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            { new BsonDocument
                {
                     { "emailTemplateId" ,"5fa020214c2ff92af0a1c85f"},
                     { "fromAddress" , "aliya@texastrustloans.com"},
                     { "toAddress" , "prasla@gmail.com"},
                     { "ccAddress" , "Ali@gmail.com,hasan@gmail.com"},
                     { "subject" , "You have new tasks to complete for your Texas Trust Home Loans loan application"},
                     { "emailBody" , "Hi Javed, To continue your application, we need some more information."},
                 }
            };

            mockCursorDraftEmail.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorDraftEmail.SetupGet(x => x.Current).Returns(list);

            mockCollection.SetupSequence(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorDraftEmail.Object);

            mockdb.Setup(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new EmailTemplateService(mock.Object, null, null);

            //Act
            IEnumerable<string> authHeader = new string[] { "Authorization"};
           
            Model.EmailTemplate dto = await service.GetDraftEmailTemplateById("5fa020214c2ff92af0a1c85f", 1,1, authHeader);

            //Assert
            Assert.NotNull(dto);
            Assert.Equal("5fa020214c2ff92af0a1c85f", dto.id);
            Assert.Equal("aliya@texastrustloans.com", dto.fromAddress);
            Assert.Equal("prasla@gmail.com", dto.toAddress);
            Assert.Equal("Ali@gmail.com,hasan@gmail.com", dto.CCAddress);
            Assert.Equal("You have new tasks to complete for your Texas Trust Home Loans loan application", dto.subject);
            Assert.Equal("Hi Javed, To continue your application, we need some more information.", dto.emailBody);
        }

        [Fact]
        public async Task TestGetRenderEmailTemplateByIdService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Request>> mockCollection = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IMongoCollection<Entity.EmailTemplate>> mockCollectionEmailTemplate = new Mock<IMongoCollection<Entity.EmailTemplate>>();
            Mock<IMongoCollection<Entity.TokenParam>> mockCollectionTokens = new Mock<IMongoCollection<Entity.TokenParam>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorDraftEmail = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorEmailTemplate = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorTokens = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();

            List<BsonDocument> list = new List<BsonDocument>();

            List<BsonDocument> listEmailTemplate = new List<BsonDocument>()
            {
                new BsonDocument
                {
                     { "tenantId" , 1},
                     { "templateName" , "System decided Template"},
                     { "templateDescription" , "Decided by System Administrator"},
                     { "fromAddress" , "###LoginUserEmail###"},
                     { "toAddress" , "prasla@gmail.com" },
                     { "CCAddress" , "Ali@gmail.com,hasan@gmail.com"},
                     { "subject" , "Email Template"},
                     { "emailBody" , "<p> Asalam O alaikum ###CustomerFirstName###, </p> <p>To continue your application, we need some more informations.</p><p>Kindly submit following documents</p><p>###DoucmentList###</p>"},
                     { "sortOrder" , "2"}
                 }
            };

            List<BsonDocument> listTokens = new List<BsonDocument>()
            { new BsonDocument
                {
                     { "_id" , "5fa02360c873e00f73864f36" },
                     { "name" ,"Login User Email"},
                     { "symbol" , "###LoginUserEmail###"},
                     { "description","Key for enabling user email address Key for enabling user email address"},
                     { "key" , "LoginUserEmail"}
                 }
            ,  new BsonDocument
             {
                     { "_id" , "5fa0236fc873e00f73864f6e" },
                     { "name" ,"Customer First Name"},
                     { "symbol" , "###CustomerFirstname###"},
                     { "description","Key for enabling customer first name" },
                     { "key" , "CustomerFirstName"}
                }
            };

            mockCursorDraftEmail.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorDraftEmail.SetupGet(x => x.Current).Returns(list);

            mockCursorEmailTemplate.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorEmailTemplate.SetupGet(x => x.Current).Returns(listEmailTemplate);

            mockCursorTokens.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorTokens.SetupGet(x => x.Current).Returns(listTokens);

            mockCollection.SetupSequence(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorDraftEmail.Object);

            mockCollectionEmailTemplate.SetupSequence(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.EmailTemplate, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorEmailTemplate.Object);

            mockCollectionTokens.SetupSequence(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.TokenParam, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorTokens.Object);

            mockdb.SetupSequence(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockdb.SetupSequence(x => x.GetCollection<Entity.EmailTemplate>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionEmailTemplate.Object);
            mockdb.SetupSequence(x => x.GetCollection<Entity.TokenParam>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionTokens.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            mockconfiguration.Setup(x => x["RainMaker:Url"]).Returns("http://test.com");


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

            var service = new EmailTemplateService(mock.Object, httpClient, mockconfiguration.Object);

            //Act
            Model.EmailTemplate dto = await service.GetDraftEmailTemplateById("5fa0202d4c2ff92af0a1c860", 1033, 1, new List<string>());

            //Assert
            Assert.NotNull(dto);
            Assert.Equal("5fa0202d4c2ff92af0a1c860", dto.id);
            Assert.Equal(1, dto.tenantId);
            Assert.Equal("System decided Template", dto.templateName);
            Assert.Equal("Decided by System Administrator", dto.templateDescription);
        }

    }
}
