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
            DeleteTemplateModel deleteTemaplateModel = new DeleteTemplateModel();
            deleteTemaplateModel.templateId = "5eba77905561502c495f6777";
            deleteTemaplateModel.tenantId = 1;
            //Act
            IActionResult result = await controller.DeleteTemplate(deleteTemaplateModel);
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
            DeleteTemplateModel deleteTemplateModel = new DeleteTemplateModel();
            deleteTemplateModel.templateId = "5eba77905561502c495f6777";
            deleteTemplateModel.tenantId = 1;
        
            //Act
            IActionResult result = await controller.DeleteTemplate(deleteTemplateModel);
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
            Mock<IMongoCollection<Entity.Template>> mockCollection = new Mock<IMongoCollection<Entity.Template>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorMCU = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorTenant = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorSystem = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            { new BsonDocument
                    {
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
            mockCursorMCU.SetupGet(x => x.Current).Returns(list);

            mockCursorTenant.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorTenant.SetupGet(x => x.Current).Returns(list);

            mockCursorSystem.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorSystem.SetupGet(x => x.Current).Returns(list);

            mockCollection.SetupSequence(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Template, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorMCU.Object).Returns(mockCursorTenant.Object).Returns(mockCursorSystem.Object);

            mockdb.Setup(x => x.GetCollection<Entity.Template>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new TemplateService(mock.Object);

            //Act
            List<TemplateModel> dto = await service.GetTemplates(1, 1);
            
            //Assert
            Assert.NotNull(dto);
            Assert.Equal(6, dto.Count);
            Assert.Equal("MCU Template1", dto[0].name);
            Assert.Equal("MCU Template", dto[1].type);
            Assert.Equal("MCU Template1", dto[2].name);
            Assert.Equal("MCU Template", dto[3].type);
            Assert.Equal("MCU Template1", dto[4].name);
            Assert.Equal("MCU Template", dto[5].type);
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
            DeleteDocumentModel deleteDocumentModel = new DeleteDocumentModel();
            deleteDocumentModel.id = "5eba77905561502c495f6333";
            deleteDocumentModel.tenantId = 1;
            deleteDocumentModel.documentId = "5eb257a3e519051af2eeb477";
            //Act
            IActionResult result = await controller.DeleteDocument(deleteDocumentModel);
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
            DeleteDocumentModel deleteDocumentModel = new DeleteDocumentModel();
            deleteDocumentModel.id = "5eb25acde519051af2eeb111";
            deleteDocumentModel.tenantId = 1;
            deleteDocumentModel.documentId = "5eb257a3e519051af2eeb477";
            //Act
            IActionResult result = await controller.DeleteDocument(deleteDocumentModel);
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
            RenameTemplateModel renameTemplateModel = new RenameTemplateModel();
            renameTemplateModel.id = "5eb25acde519051af2eeb111";
            renameTemplateModel.tenantId = 1;
            renameTemplateModel.name = "salary";
            //Act
            IActionResult result = await controller.RenameTemplate(renameTemplateModel);
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
            RenameTemplateModel renameTemplateModel = new RenameTemplateModel();
            renameTemplateModel.id = "5eb25acde519051af2eeb111";
            renameTemplateModel.tenantId = 1;
            renameTemplateModel.name = "salary";
            //Act
            IActionResult result = await controller.RenameTemplate(renameTemplateModel);
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

            mock.Setup(x => x.GetCategoryDocument(It.IsAny<int>())).ReturnsAsync(list);

            var templateController = new TemplateController(mock.Object);

            //Act
            IActionResult result = await templateController.GetCategoryDocument(It.IsAny<int>());
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


        [Fact]
        public async Task TestAddDocumentServiceTypeIdIsNotNullTrue()
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
            AddDocumentModel addDocumentModel = new AddDocumentModel();
            addDocumentModel.templateId = "5efdbf22a74aa7454c4becef";
            addDocumentModel.tenantId = 1;
            addDocumentModel.docName = "Credit Report";
            addDocumentModel.typeId = "5eb257a3e519051af2eeb624";
            bool result = await templateService.AddDocument(addDocumentModel.templateId, addDocumentModel.tenantId,1, addDocumentModel.typeId, addDocumentModel.docName);
            //Assert
            Assert.True(result);
        }


        [Fact]
        public async Task TestAddDocumentServiceTypeIdIsNotNullFalse()
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
            AddDocumentModel addDocumentModel = new AddDocumentModel();
            addDocumentModel.templateId = "5efdbf22a74aa7454c4becef";
            addDocumentModel.tenantId = 1;
            addDocumentModel.docName = "Credit Report";
            addDocumentModel.typeId = "5eb257a3e519051af2eeb624";
            bool result = await templateService.AddDocument(addDocumentModel.templateId, addDocumentModel.tenantId, 1, addDocumentModel.typeId, addDocumentModel.docName);
            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task TestAddDocumentServiceTypeIdIsNullTrue()
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
            AddDocumentModel addDocumentModel = new AddDocumentModel();
            addDocumentModel.templateId = "5efdbf22a74aa7454c4becef";
            addDocumentModel.tenantId = 1;
            addDocumentModel.docName = "Credit Report";
            
            bool result = await templateService.AddDocument(addDocumentModel.templateId, addDocumentModel.tenantId, 1, addDocumentModel.typeId, addDocumentModel.docName);
            //Assert
            Assert.True(result);
        }


        [Fact]
        public async Task TestAddDocumentServiceTypeIdIsNullFalse()
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
            AddDocumentModel addDocumentModel = new AddDocumentModel();
            addDocumentModel.templateId = "5efdbf22a74aa7454c4becef";
            addDocumentModel.tenantId = 1;
            addDocumentModel.docName = "Credit Report";
         
            bool result = await templateService.AddDocument(addDocumentModel.templateId, addDocumentModel.tenantId, 1, addDocumentModel.typeId, addDocumentModel.docName);
            //Assert
            Assert.False(result);
        }



        [Fact]
        public async Task TestAddDocumentServiceTypeIdAndDocNameNullException()
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
            AddDocumentModel addDocumentModel = new AddDocumentModel();
            addDocumentModel.templateId = "5efdbf22a74aa7454c4becef";
            addDocumentModel.tenantId = 1;
           

            Assert.ThrowsAsync<Exception>(async () => await templateService.AddDocument(addDocumentModel.templateId, addDocumentModel.tenantId, 1, addDocumentModel.typeId, addDocumentModel.docName));

          }
        [Fact]
        public async Task TestGetCategoryDocumentService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Category>> mockCollection = new Mock<IMongoCollection<Entity.Category>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            {
                new BsonDocument
                    {
                        { "_id" , "5ebabbfb3845be1cf1edce50" },
                        { "name" ,BsonString.Empty },
                        { "docTypeId" , BsonString.Empty},
                        { "docType" , BsonString.Empty},
                        { "docMessage" , BsonString.Empty},
                        { "messages" , BsonArray.Create(new Message[]{ })}
                    }
                 ,
                 new BsonDocument
                 {
                     { "_id" , "5ebabbfb3845be1cf1edce50"},
                     { "name" ,"Assets"},
                     { "docTypeId" , BsonString.Empty},
                     { "docType" , BsonString.Empty},
                     { "docMessage" , BsonString.Empty},
                     { "messages" , BsonArray.Create(new Message[]{ })}
                 }
                 ,
                 new BsonDocument
                 {
                     { "_id" , BsonString.Empty},
                     { "name" ,BsonString.Empty},
                     { "docTypeId" , "5ebc18cba5d847268075ad4f"},
                     { "docType" , BsonString.Empty},
                     { "docMessage" , BsonString.Empty},
                     { "messages" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "tenantId", 1 },{ "message", "Credit report has been uploaded" } } })}
                 }
                 ,
                 new BsonDocument
                 {
                     { "_id" , BsonString.Empty},
                     { "name" ,BsonString.Empty},
                     { "docTypeId" , "5ebc18cba5d847268075ad4f"},
                     { "docType" , "Credit Report"},
                     { "docMessage" , "Credit report has been uploaded"},
                     { "messages" , BsonArray.Create(new Message[]{ })}
                 }
                 ,
                 new BsonDocument
                 {
                     { "_id" , BsonString.Empty},
                     { "name" ,BsonString.Empty},
                     { "docTypeId" , "5ebc18cba5d847268075ad4f"},
                     { "docType" , BsonString.Empty},
                     { "docMessage" , BsonString.Empty},
                     { "messages" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "tenantId", 2 } , { "message", BsonString.Empty } } })}
                 }
                 ,
                 new BsonDocument
                 {
                     { "_id" , BsonString.Empty},
                     { "name" ,BsonString.Empty},
                     { "docTypeId" , "5ebc18cba5d847268075ad4f"},
                     { "docType" , BsonString.Empty},
                     { "docMessage" , BsonString.Empty},
                     { "messages" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "tenantId", 1 },{ "message", BsonString.Empty } } })}
                 }
                 ,
                 new BsonDocument
                 {
                     { "_id" , BsonString.Empty},
                     { "name" ,BsonString.Empty},
                     { "docTypeId" , "5ebc18cba5d847268075ad4f"},
                     { "docType" , BsonString.Empty},
                     { "docMessage" , BsonString.Empty},
                     { "messages" , BsonNull.Value}
                 }
            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Category, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Entity.Category>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new TemplateService(mock.Object);
            //Act
            List<CategoryDocumentTypeModel> dto = await service.GetCategoryDocument(1);
            //Assert
            Assert.NotNull(dto);
            Assert.Equal(2, dto.Count);
            Assert.Equal("5ebabbfb3845be1cf1edce50", dto[0].catId);
            Assert.Equal("Credit report has been uploaded", dto[1].documents[0].docMessage);
        }

        [Fact]
        public async Task TestSaveTemplateController()
        {
            Mock<ITemplateService> mock = new Mock<ITemplateService>();
            AddTemplateModel addTemplateModel= new AddTemplateModel();

            mock.Setup(x => x.SaveTemplate(It.IsAny<AddTemplateModel>(),It.IsAny<int>())).ReturnsAsync("5eb25acde519051af2eeb111");

            var controller = new TemplateController(mock.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            //Act

            IActionResult result = await controller.SaveTemplate(addTemplateModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestSaveTemplateServiceIsTypeIdNull()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Template>> mockCollection = new Mock<IMongoCollection<Entity.Template>>();
            mockdb.Setup(x => x.GetCollection<Entity.Template>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mockCollection.Setup(s => s.InsertOneAsync(It.IsAny<Entity.Template>(), It.IsAny<InsertOneOptions>(), It.IsAny<System.Threading.CancellationToken>()));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act
            ITemplateService templateService = new TemplateService(mock.Object);
            AddTemplateModel addTemplateModel = new AddTemplateModel();
            TemplateDocument templateDocument= new TemplateDocument();
            addTemplateModel.tenantId =1;
            addTemplateModel.name = "Insert Tenant Template";
            addTemplateModel.documentTypes = new List<TemplateDocument>();

            templateDocument.id = "5f0701b8c4577f7180cd9dc3";
            templateDocument.docName = "abcbbc";
            addTemplateModel.documentTypes.Add(templateDocument);
            string result = await templateService.SaveTemplate(addTemplateModel, 3872);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestSaveTemplateServiceIsTypeIdNotNull()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Template>> mockCollection = new Mock<IMongoCollection<Entity.Template>>();

            mockdb.Setup(x => x.GetCollection<Entity.Template>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mockCollection.Setup(s => s.InsertOneAsync(It.IsAny<Entity.Template>(), It.IsAny<InsertOneOptions>(), It.IsAny<System.Threading.CancellationToken>()));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act
            ITemplateService templateService = new TemplateService(mock.Object);
            AddTemplateModel addTemplateModel = new AddTemplateModel();
            addTemplateModel.tenantId = 1;
            addTemplateModel.name = "Insert Tenant Template";
            addTemplateModel.documentTypes = new List<TemplateDocument>();
            TemplateDocument templateDocument = new TemplateDocument();
            templateDocument.id = "5f0701b8c4577f7180cd9dc3";
            templateDocument.typeId = "5ebc18cba5d847268075ad22";
            addTemplateModel.documentTypes.Add(templateDocument);
            string result = await templateService.SaveTemplate(addTemplateModel, 3872);
            //Assert
            Assert.NotNull(result);
        }


        [Fact]
        public async Task TestInsertTemplateServiceIsTypeIdNull()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Template>> mockCollection = new Mock<IMongoCollection<Entity.Template>>();
            mockdb.Setup(x => x.GetCollection<Entity.Template>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mockCollection.Setup(s => s.InsertOneAsync(It.IsAny<Entity.Template>(), It.IsAny<InsertOneOptions>(), It.IsAny<System.Threading.CancellationToken>()));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act
            ITemplateService templateService = new TemplateService(mock.Object);
             
            string result = await templateService.InsertTemplate(1,3872,"abc");
            //Assert
            Assert.NotNull(result);
            Assert.Equal("abc", result);
        }

    }
}
