using DocumentManagement.API.Controllers;
using DocumentManagement.Entity;
using DocumentManagement.Model;
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

            mock.Setup(x => x.DeleteTemplate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(false);
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
            Mock<IMongoCollection<Entity.Template>> mockCollectionMCU = new Mock<IMongoCollection<Entity.Template>>();
            Mock<IMongoCollection<Entity.Template>> mockCollectionTenant = new Mock<IMongoCollection<Entity.Template>>();
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

            mockCollectionMCU.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Template, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorMCU.Object);
            mockCollectionTenant.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Template, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorTenant.Object);

            mockdb.Setup(x => x.GetCollection<Entity.Template>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionMCU.Object);

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
            Mock<IMongoCollection<Entity.Template>> mockCollection = new Mock<IMongoCollection<Entity.Template>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            mockdb.Setup(x => x.GetCollection<Entity.Template>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Entity.Template>>(), It.IsAny<UpdateDefinition<Entity.Template>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act
            ITemplateService templateService = new TemplateService(mock.Object);
            bool result = await templateService.DeleteTemplate("5eba77905561502c495f6777", 1, 1);
            //Assert
            Assert.True(result);
        }

        /// <summary>
        /// shehroz
        /// </summary>

        [Fact]
        public async Task TestGetDocumentController()
        {
            //Arrange
            Mock<ITemplateService> mock = new Mock<ITemplateService>();
            List<TemplateDTO> list = new List<TemplateDTO>() { { new TemplateDTO() { docId = "5eb257a3e519051af2eeb477", docName = "Salary Slip" } } };

            mock.Setup(x => x.GetDocument(It.IsAny<string>())).ReturnsAsync(list);

            var templateController = new TemplateController(mock.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            templateController.ControllerContext = context;

            //Act
            IActionResult result = await templateController.GetDocument("5eba77905561502c495f6444");
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as List<TemplateDTO>;
            Assert.Single(content);
            Assert.Equal("5eb257a3e519051af2eeb477", content[0].docId);
            Assert.Equal("Salary Slip", content[0].docName);
        }
        [Fact]
        public async Task TestGetDocumentService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Template>> mockCollection = new Mock<IMongoCollection<Entity.Template>>();
            Mock<IMongoCollection<Entity.Template>> mockCollectionTenant = new Mock<IMongoCollection<Entity.Template>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            {
                new BsonDocument
                    {
                        //Cover all empty  fields
                        { "_id" ,  BsonString.Empty},
                        { "docId" ,BsonString.Empty},
                        { "docName" , BsonString.Empty},
                        { "typeName" , BsonString.Empty}
                    }
                ,
                new BsonDocument
                    {
                        //Cover all empty  fields except docId
                        { "_id" , BsonString.Empty},
                        { "docId" ,"5eba77905561502c495f6333"},
                        { "docName" , BsonString.Empty},
                        { "typeName" , BsonString.Empty}
                    }
                 ,
                new BsonDocument
                    {
                        //Cover all empty  fields except docName
                        { "_id" , BsonString.Empty},
                        { "docId" ,BsonString.Empty},
                        { "docName" , "Salary Slip"},
                        { "typeName" , BsonString.Empty}
                    }
                 ,
                new BsonDocument
                    {
                        //Cover all empty  fields except typeName
                        { "_id" , BsonString.Empty},
                        { "docId" ,BsonString.Empty},
                        { "docName" , BsonString.Empty},
                        { "typeName" , "Salary Slip"}
                    }
            };


            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Template, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);


            mockdb.Setup(x => x.GetCollection<Entity.Template>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new TemplateService(mock.Object);
            //Act
            List<TemplateDTO> dto = await service.GetDocument("5eba77905561502c495f6333");
            //Assert
            Assert.NotNull(dto);
            Assert.Equal(4, dto.Count);
            Assert.Equal("5eba77905561502c495f6333", dto[1].docId);
            Assert.Equal("Salary Slip", dto[2].docName);
            Assert.Equal("Salary Slip", dto[3].docName);
        }
        [Fact]
        public async Task TestDeleteDocumentControllerTrue()
        {
            Mock<ITemplateService> mock = new Mock<ITemplateService>();

            mock.Setup(x => x.DeleteDocument(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(true);
            var controller = new TemplateController(mock.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            IActionResult result = await controller.DeleteDocument("5eba77905561502c495f6333", 1, "5eb257a3e519051af2eeb477");
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);

        }

        [Fact]
        public async Task TestDeleteDocumentControllerFalse()
        {
            Mock<ITemplateService> mock = new Mock<ITemplateService>();

            mock.Setup(x => x.DeleteTemplate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(false);
            var controller = new TemplateController(mock.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            IActionResult result = await controller.DeleteDocument("5eb25acde519051af2eeb111", 1, "5eb257a3e519051af2eeb477");
            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);

        }


        [Fact]
        public async Task TestRenameControllerTrue()
        {
            //Arrange
            Mock<ITemplateService> mock = new Mock<ITemplateService>();

            mock.Setup(x => x.RenameTemplate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(true);

            var controller = new TemplateController(mock.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            IActionResult result = await controller.RenameTemplate("5eb25acde519051af2eeb111", 1, "salary");
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestRenameControllerFalse()
        {
            //Arrange
            Mock<ITemplateService> mock = new Mock<ITemplateService>();

            mock.Setup(x => x.RenameTemplate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(false);

            var controller = new TemplateController(mock.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            IActionResult result = await controller.RenameTemplate("5eb25acde519051af2eeb6ac", 1, "abc");
            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestRenameTemplateServiceTrue()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Template>> mockCollection = new Mock<IMongoCollection<Entity.Template>>();

            mockdb.Setup(x => x.GetCollection<Entity.Template>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Entity.Template>>(), It.IsAny<UpdateDefinition<Entity.Template>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act

            ITemplateService templateService = new TemplateService(mock.Object);
            bool result = await templateService.RenameTemplate("5eb25acde519051af2eeb111", 1, "ABC", 1);

            //Assert
            Assert.True(result);
        }


        [Fact]
        public async Task TestDeleteDocumentServiceTrue()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Template>> mockCollection = new Mock<IMongoCollection<Entity.Template>>();

            mockdb.Setup(x => x.GetCollection<Entity.Template>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Entity.Template>>(), It.IsAny<UpdateDefinition<Entity.Template>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act

            ITemplateService templateService = new TemplateService(mock.Object);
            bool result = await templateService.DeleteDocument("5eb25acde519051af2eeb111", 1, "5eb257a3e519051af2eeb477", 1);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task TestDeleteDocumentServiceFalse()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Template>> mockCollection = new Mock<IMongoCollection<Entity.Template>>();

            mockdb.Setup(x => x.GetCollection<Entity.Template>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Entity.Template>>(), It.IsAny<UpdateDefinition<Entity.Template>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(0, 0, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act
            ITemplateService templateService = new TemplateService(mock.Object);
            bool result = await templateService.DeleteDocument("6eb25acde519051af2eeb111", 1, "6eb257a3e519051af2eeb477", 1);
            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task TestRenameTemplateServiceFalse()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Template>> mockCollection = new Mock<IMongoCollection<Entity.Template>>();

            mockdb.Setup(x => x.GetCollection<Entity.Template>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Entity.Template>>(), It.IsAny<UpdateDefinition<Entity.Template>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(0, 0, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act
            ITemplateService templateService = new TemplateService(mock.Object);
            bool result = await templateService.RenameTemplate("6eb25acde519051af2eeb111", 1, "salary", 1);
            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task TestAddDocumentControllerTrue()
        {
            Mock<ITemplateService> mock = new Mock<ITemplateService>();

            mock.Setup(x => x.AddDocument(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            var controller = new TemplateController(mock.Object);
            controller.ControllerContext = context;

            //Act

            AddDocumentModel addDocumentModel = new AddDocumentModel();
            addDocumentModel.templateId = "5efdbf22a74aa7454c4becef";
            addDocumentModel.tenantId = 1;
            addDocumentModel.docName = "Credit Report";
            addDocumentModel.typeId = "5eb257a3e519051af2eeb624";
            IActionResult result = await controller.AddDocument(addDocumentModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestAddDocumentControllerFalse()
        {
            Mock<ITemplateService> mock = new Mock<ITemplateService>();

            mock.Setup(x => x.AddDocument(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            var controller = new TemplateController(mock.Object);
            controller.ControllerContext = context;

            //Act

            AddDocumentModel addDocumentModel = new AddDocumentModel();
            addDocumentModel.templateId = "5efdbf22a74aa7454c4becef";
            addDocumentModel.tenantId = 1;
            addDocumentModel.docName = "Credit Report";
            addDocumentModel.typeId = "5eb257a3e519051af2eeb624";
            IActionResult result = await controller.AddDocument(addDocumentModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestGetCategoryDocumentController()
        {
            //Arrange
            Mock<ITemplateService> mock = new Mock<ITemplateService>();
            List<CategoryDocumentTypeModel> list = new List<CategoryDocumentTypeModel>() { { new CategoryDocumentTypeModel() { catId = "5ebabbfb3845be1cf1edce50" } } };

            mock.Setup(x => x.GetCategoryDocument()).ReturnsAsync(list);

            var templateController = new TemplateController(mock.Object);

            //Act
            IActionResult result = await templateController.GetCategoryDocument();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as List<CategoryDocumentTypeModel>;
            Assert.Single(content);
            Assert.Equal("5ebabbfb3845be1cf1edce50", content[0].catId);
        }
        [Fact]
        public async Task TestInsertTemplateController()
        {
            Mock<ITemplateService> mock = new Mock<ITemplateService>();

            mock.Setup(x => x.InsertTemplate(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync("5eb25acde519051af2eeb111");
            var controller = new TemplateController(mock.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            InsertTemplateModel insertTemplateModel = new InsertTemplateModel();
            insertTemplateModel.name = "Salary Slip";
            insertTemplateModel.tenantId = 1;
            IActionResult result = await controller.InsertTemplate(insertTemplateModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
