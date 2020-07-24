using DocumentManagement.API.Controllers;
using DocumentManagement.Entity;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Xunit;
using Microsoft.Extensions.Logging;

namespace DocumentManagement.Tests
{
    public class AdminDashboardTests
    {
        [Fact]
        public async Task TestGetDocumentsController()
        {
            //Arrange
            Mock<IAdminDashboardService> mock = new Mock<IAdminDashboardService>();
            List<AdminDashboardDTO> list = new List<AdminDashboardDTO>() { { new AdminDashboardDTO() { docId = "1" } } };

            mock.Setup(x => x.GetDocument(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(list);

            var admindashboardController = new AdminDashboardController(mock.Object,Mock.Of<ILogger<AdminDashboardController>>());

            GetDocuments moGetDocuments = new GetDocuments();
            moGetDocuments.tenantId = 1;
            moGetDocuments.loanApplicationId = 1;
            moGetDocuments.pending = true;
            //Act
            IActionResult result = await admindashboardController.GetDocuments(moGetDocuments);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as List<AdminDashboardDTO>;
            Assert.Single(content);
            Assert.Equal("1", content[0].docId);
        }

        [Fact]
        public async Task TestDeleteControllerTrue()
        {
            //Arrange
            Mock<IAdminDashboardService> mock = new Mock<IAdminDashboardService>();
            AdminDeleteModel model = new AdminDeleteModel() { id = "1", docId = "1", requestId = "1", tenantId = 1 };

            mock.Setup(x => x.Delete(It.IsAny<AdminDeleteModel>())).ReturnsAsync(true);

            var controller = new AdminDashboardController(mock.Object, Mock.Of<ILogger<AdminDashboardController>>()
            );



            //Act
            IActionResult result = await controller.Delete(new AdminDeleteModel() { id = "1", docId = "1", requestId = "1", tenantId = 1 });
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestDeleteControllerFalse()
        {
            //Arrange
            Mock<IAdminDashboardService> mock = new Mock<IAdminDashboardService>();
            AdminDeleteModel model = new AdminDeleteModel() { id = "1", docId = "1", requestId = "1", tenantId = 1 };

            mock.Setup(x => x.Delete(It.IsAny<AdminDeleteModel>())).ReturnsAsync(false);

            var controller = new AdminDashboardController(mock.Object, Mock.Of<ILogger<AdminDashboardController>>()
            );

            //Act
            IActionResult result = await controller.Delete(new AdminDeleteModel() { id = "1", docId = "1", requestId = "1", tenantId = 1 });
            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestGetDocumentsServicePending()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Request>> mockCollection = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            {
                new BsonDocument
                    {
                        //Cover all empty fields
                        { "_id" , BsonString.Empty },
                        { "requestId" , BsonString.Empty  },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "typeName" , BsonString.Empty },
                        { "status" , BsonString.Empty },
                        { "createdOn" , Convert.ToDateTime("2020-06-25T07:39:57.233Z") },
                        { "files" , BsonArray.Create(new Entity.RequestFile[]{ })}
                    }
            ,
                    new BsonDocument
                    {
                        //Cover all empty fields except docName
                          { "_id" , BsonString.Empty },
                        { "requestId" , BsonString.Empty  },
                        { "docId" , BsonString.Empty },
                        { "docName" , "House Document" },
                        { "typeName" , BsonString.Empty },
                        { "status" , BsonString.Empty },
                        { "createdOn" , Convert.ToDateTime("2020-06-25T07:39:57.233Z") },
                        { "files" , BsonArray.Create(new Entity.RequestFile[]{ })}
                    }
                    ,
                    new BsonDocument
                    {
                        //Cover all empty fields except typeName
                         { "_id" , BsonString.Empty },
                        { "requestId" , BsonString.Empty  },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "typeName" ,  "Property" },
                        { "status" , BsonString.Empty },
                        { "createdOn" , Convert.ToDateTime("2020-06-25T07:39:57.233Z") },
                        { "files" , BsonArray.Create(new Entity.RequestFile[]{ })}
                    }
                    ,
                  new BsonDocument
                    {
                        //Cover all empty fields except status
                         { "_id" , BsonString.Empty },
                        { "requestId" , BsonString.Empty  },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "typeName" , BsonString.Empty},
                        { "status" ,"Started" },
                        { "createdOn" , Convert.ToDateTime("2020-06-25T07:39:57.233Z") },
                        { "files" , BsonArray.Create(new Entity.RequestFile[]{ })}
                    }
                    ,
                 new BsonDocument
                    {
                        //Cover all empty fields except status and set files null
                          { "_id" , BsonString.Empty },
                        { "requestId" , BsonString.Empty  },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "typeName" , BsonString.Empty },
                        { "status" , "Started"  },
                        { "createdOn" , Convert.ToDateTime("2020-06-25T07:39:57.233Z") },
                        { "files" ,BsonNull.Value}
                    }
                  ,
                 new BsonDocument
                    {
                        //Cover all empty fields except status and files
                        { "_id" , BsonString.Empty },
                        { "requestId" , BsonString.Empty  },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "typeName" , BsonString.Empty },
                        { "status" , "Started" },
                        { "createdOn" , BsonNull.Value },
                        { "files" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() {{"id", "5ef454cd86c96583744140d9" }, { "clientName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "mcuName", "abc" },{ "byteProStatus","Active" },{ "status", "Submitted To Mcu" } } })}
                    }
            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new AdminDashboardService(mock.Object, mockActivityLogService.Object);
            //Act
            List<AdminDashboardDTO> dto = await service.GetDocument(1, 1 ,true);
            //Assert
            Assert.NotNull(dto);
            Assert.Equal(6, dto.Count);
            Assert.Equal("House Document", dto[1].docName);
            Assert.Equal("Property", dto[2].docName);
            Assert.Equal("Started", dto[3].status);
            Assert.Equal("Started", dto[4].status);
            Assert.Equal("asd", dto[5].files[0].clientName);
        }

        [Fact]
        public async Task TestGetDocumentsService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Request>> mockCollection = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            {
                new BsonDocument
                    {
                        //Cover all empty fields
                        { "_id" , BsonString.Empty },
                        { "requestId" , BsonString.Empty  },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "typeName" , BsonString.Empty },
                        { "status" , BsonString.Empty },
                        { "createdOn" , Convert.ToDateTime("2020-06-25T07:39:57.233Z") },
                        { "files" , BsonArray.Create(new Entity.RequestFile[]{ })}
                    }
            ,
                    new BsonDocument
                    {
                        //Cover all empty fields except docName
                          { "_id" , BsonString.Empty },
                        { "requestId" , BsonString.Empty  },
                        { "docId" , BsonString.Empty },
                        { "docName" , "House Document" },
                        { "typeName" , BsonString.Empty },
                        { "status" , BsonString.Empty },
                        { "createdOn" , Convert.ToDateTime("2020-06-25T07:39:57.233Z") },
                        { "files" , BsonArray.Create(new Entity.RequestFile[]{ })}
                    }
                    ,
                    new BsonDocument
                    {
                        //Cover all empty fields except typeName
                         { "_id" , BsonString.Empty },
                        { "requestId" , BsonString.Empty  },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "typeName" ,  "Property" },
                        { "status" , BsonString.Empty },
                        { "createdOn" , Convert.ToDateTime("2020-06-25T07:39:57.233Z") },
                        { "files" , BsonArray.Create(new Entity.RequestFile[]{ })}
                    }
                    ,
                  new BsonDocument
                    {
                        //Cover all empty fields except status
                         { "_id" , BsonString.Empty },
                        { "requestId" , BsonString.Empty  },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "typeName" , BsonString.Empty},
                        { "status" ,"Started" },
                        { "createdOn" , Convert.ToDateTime("2020-06-25T07:39:57.233Z") },
                        { "files" , BsonArray.Create(new Entity.RequestFile[]{ })}
                    }
                    ,
                 new BsonDocument
                    {
                        //Cover all empty fields except status and set files null
                          { "_id" , BsonString.Empty },
                        { "requestId" , BsonString.Empty  },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "typeName" , BsonString.Empty },
                        { "status" , "Started"  },
                        { "createdOn" , Convert.ToDateTime("2020-06-25T07:39:57.233Z") },
                        { "files" ,BsonNull.Value}
                    }
                  ,
                 new BsonDocument
                    {
                        //Cover all empty fields except status and files
                        { "_id" , BsonString.Empty },
                        { "requestId" , BsonString.Empty  },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "typeName" , BsonString.Empty },
                        { "status" , "Started" },
                        { "createdOn" , BsonNull.Value },
                        { "files" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() {{"id", "5ef454cd86c96583744140d9" }, { "clientName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "mcuName", "abc" },{ "byteProStatus","Active" },{ "status", "Submitted To Mcu" } } })}
                    }
            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new AdminDashboardService(mock.Object, mockActivityLogService.Object);
            //Act
            List<AdminDashboardDTO> dto = await service.GetDocument(1, 1, false);
            //Assert
            Assert.NotNull(dto);
            Assert.Equal(6, dto.Count);
            Assert.Equal("House Document", dto[1].docName);
            Assert.Equal("Property", dto[2].docName);
            Assert.Equal("Started", dto[3].status);
            Assert.Equal("Started", dto[4].status);
            Assert.Equal("asd", dto[5].files[0].clientName);
        }

        [Fact]
        public async Task TestDeleteServiceTrue()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Request>> mockCollection = new Mock<IMongoCollection<Entity.Request>>();

            var adminDeleteModel = new AdminDeleteModel() { id = "5eb25d1fe519051af2eeb72d", docId = "5eb25d1fe519051af2eeb72d", requestId = "5eb25d1fe519051af2eeb72d", tenantId = 1 };
            mockdb.Setup(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Entity.Request>>(), It.IsAny<UpdateDefinition<Entity.Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act

            IAdminDashboardService adminDashboardService = new AdminDashboardService(mock.Object,mockActivityLogService.Object);
            bool result = await adminDashboardService.Delete(adminDeleteModel);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task TestDeleteServiceFalse()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Request>> mockCollection = new Mock<IMongoCollection<Entity.Request>>();

            var adminDeleteModel = new AdminDeleteModel() { id = "5eb25d1fe519051af2eeb72d", docId = "5eb25d1fe519051af2eeb72d", requestId = "5eb25d1fe519051af2eeb72d", tenantId = 1 };
            mockdb.Setup(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Entity.Request>>(), It.IsAny<UpdateDefinition<Entity.Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(0, 0, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act

            IAdminDashboardService adminDashboardService = new AdminDashboardService(mock.Object,mockActivityLogService.Object);
            bool result = await adminDashboardService.Delete(adminDeleteModel);
            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task TestIsDocumentDraftController()
        {
            //Arrange
            Mock<IAdminDashboardService> mock = new Mock<IAdminDashboardService>();
            
            mock.Setup(x => x.IsDocumentDraft(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync("abc15d1fe456051af2eeb768");

            var adminDashboardController = new AdminDashboardController(mock.Object, Mock.Of<ILogger<AdminDashboardController>>()
            );

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            adminDashboardController.ControllerContext = context;
            IsDocumentDraft moIsDocumentDraft= new IsDocumentDraft();
            moIsDocumentDraft.id = "5eb25d1fe519051af2eeb72d";
            //Act
            IActionResult result = await adminDashboardController.IsDocumentDraft(moIsDocumentDraft);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestIsDocumentDraftService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Request>> mockCollection = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            {
                new BsonDocument
                    {
                        { "requestId" , BsonString.Empty }
                    }
            ,
                    new BsonDocument
                    {
                        { "requestId" , "abc15d1fe456051af2eeb768" }
                    }
            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new AdminDashboardService(mock.Object,mockActivityLogService.Object);
            //Act
            var dto = await service.IsDocumentDraft("5eb25d1fe519051af2eeb72d", 3842);
            //Assert
            Assert.NotNull(dto);
        }
    }
}
