using DocumentManagement.API.Controllers;
using DocumentManagement.Entity;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static DocumentManagement.Model.Template;

namespace DocumentManagement.Tests
{
    public class TemplateTests
    {
        [Fact]
        public async Task TestGetTemplatesController()
        {
            //Arrange
            Mock<ITemplateService> mock = new Mock<ITemplateService>();
            List<TemplateModel> list = new List<TemplateModel>() { { new TemplateModel() { type = "MCU Template" } } };

            mock.Setup(x => x.GetTemplates(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(list);

            var templateController = new TemplateController(mock.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            templateController.ControllerContext = context;

            //Act
            IActionResult result = await templateController.GetTemplates(1);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as List<TemplateModel>;
            Assert.Single(content);
            Assert.Equal("MCU Template", content[0].type);
        }

        [Fact]
        public async Task TestDeleteControllerTrue()
        {
            Mock<ITemplateService> mock = new Mock<ITemplateService>();

            mock.Setup(x => x.DeleteTemplate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);
            var controller = new TemplateController(mock.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            IActionResult result = await controller.DeleteTemplate("5eba77905561502c495f6777", 1);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);

        }

        [Fact]
        public async Task TestDeleteControllerFalse()
        {
            Mock<ITemplateService> mock = new Mock<ITemplateService>();

            mock.Setup(x => x.DeleteTemplate(It.IsAny<string>(), It.IsAny<int>() , It.IsAny<int>())).ReturnsAsync(false);
            var controller = new TemplateController(mock.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            IActionResult result = await controller.DeleteTemplate("5eba77905561502c495f6777", 1);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task TestGetTemplatesService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Template>> mockCollectionMCU = new Mock<IMongoCollection<Template>>();
            Mock<IMongoCollection<Template>> mockCollectionTenant = new Mock<IMongoCollection<Template>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorMCU = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorTenant = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> listMCU = new List<BsonDocument>()
            { new BsonDocument
                    {
                        //Cover all empty fields
                        { "_id" , BsonString.Empty },
                        { "name" ,"MCU Template1"},
                        { "type" , BsonString.Empty }
                    }
            ,  new BsonDocument
             {
                        { "_id" , BsonString.Empty },
                        { "name" , BsonString.Empty },
                        { "type" ,  "MCU Template" }
                    }
            };

            List<BsonDocument> listTenant = new List<BsonDocument>()
            { new BsonDocument
                    {
                        //Cover all empty fields
                        { "_id" , BsonString.Empty },
                        { "name" ,"MCU Template1"},
                        { "type" , BsonString.Empty }
                    }
            ,  new BsonDocument
             {
                        { "_id" , BsonString.Empty },
                        { "name" , BsonString.Empty },
                        { "type" ,  "MCU Template" }
                    }
            };

            mockCursorMCU.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorMCU.SetupGet(x => x.Current).Returns(listMCU);

            mockCursorMCU.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorMCU.SetupGet(x => x.Current).Returns(listTenant);

            mockCollectionMCU.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Template, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorMCU.Object);
            mockCollectionTenant.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Template, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorTenant.Object);

            mockdb.Setup(x => x.GetCollection<Template>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionMCU.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new TemplateService(mock.Object);
            //Act
            List<TemplateModel> dto = await service.GetTemplates(1, 1);
            //Assert
            Assert.NotNull(dto);
            Assert.Equal(2, dto.Count);
            Assert.Equal("MCU Template1", dto[1].name);
            Assert.Equal("MCU Template", dto[2].type);
        }

        [Fact]
        public async Task TestDeleteService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Template>> mockCollection = new Mock<IMongoCollection<Template>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            mockdb.Setup(x => x.GetCollection<Template>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.DeleteOneAsync(It.IsAny<FilterDefinition<Template>>(), It.IsAny<CancellationToken>())).ReturnsAsync(new DeleteResult.Acknowledged(1));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act
            ITemplateService templateService = new TemplateService(mock.Object);
            bool result = await templateService.DeleteTemplate("5eba77905561502c495f6777", 1,1);
            //Assert
            Assert.True(result);
        }

    }
}
