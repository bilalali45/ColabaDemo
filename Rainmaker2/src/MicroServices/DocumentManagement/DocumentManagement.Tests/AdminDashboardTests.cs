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
using Microsoft.Extensions.Primitives;
using System.Linq;
using Request = DocumentManagement.Model.Request;

namespace DocumentManagement.Tests
{
    public class AdminDashboardTests
    {
        [Fact]
        public async Task TestGetDocumentsController()
        {
            //Arrange
            Mock<IAdminDashboardService> mock = new Mock<IAdminDashboardService>();
            List<AdminDashboardDto> list = new List<AdminDashboardDto>() { { new AdminDashboardDto() { docId = "1" } } };

            mock.Setup(x => x.GetDocument(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<int>())).ReturnsAsync(list);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var admindashboardController = new AdminDashboardController(mock.Object, Mock.Of<ILogger<AdminDashboardController>>());

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            admindashboardController.ControllerContext = context;

            GetDocuments moGetDocuments = new GetDocuments();
            moGetDocuments.loanApplicationId = 1;
            moGetDocuments.pending = true;
            //Act
            IActionResult result = await admindashboardController.GetDocuments(moGetDocuments);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as List<AdminDashboardDto>;
            Assert.Single(content);
            Assert.Equal("1", content[0].docId);
        }

        [Fact]
        public async Task TestDeleteControllerTrue()
        {
            //Arrange
            Mock<IAdminDashboardService> mock = new Mock<IAdminDashboardService>();
           

            mock.Setup(x => x.Delete(It.IsAny<AdminDeleteModel>(), It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(true);
            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var controller = new AdminDashboardController(mock.Object, Mock.Of<ILogger<AdminDashboardController>>()
            );

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            IActionResult result = await controller.Delete(new AdminDeleteModel() { id = "1", docId = "1", requestId = "1" });
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestDeleteControllerFalse()
        {
            //Arrange
            Mock<IAdminDashboardService> mock = new Mock<IAdminDashboardService>();
           

            mock.Setup(x => x.Delete(It.IsAny<AdminDeleteModel>(), It.IsAny<int>(), It.IsAny<List<string>>())).ReturnsAsync(false);
            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var controller = new AdminDashboardController(mock.Object, Mock.Of<ILogger<AdminDashboardController>>()
            );

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            IActionResult result = await controller.Delete(new AdminDeleteModel() { id = "1", docId = "1", requestId = "1" });
            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result);
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
                         { "files" , BsonArray.Create(new Entity.RequestFile[]{ })}
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

            var service = new AdminDashboardService(mock.Object, mockActivityLogService.Object, null);
            //Act
            List<AdminDashboardDto> dto = await service.GetDocument(1, 1, true, 1);
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
                        //Cover all empty fields except status and files
                        { "_id" , BsonString.Empty },
                        { "requestId" , BsonString.Empty  },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "typeName" , BsonString.Empty },
                        { "status" , "Started" },
                        { "createdOn" , BsonNull.Value },
                        { "files" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() {{"id", "5ef454cd86c96583744140d9" }, { "clientName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "mcuName", "abc" },{ "byteProStatus","Active" },{ "status", "Submitted To Mcu" } } })},
                        { "mcuFiles" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() {{"id", "5ef454cd86c96583744140d9" }, { "clientName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "mcuName", "abc" },{ "byteProStatus","Active" },{ "status", "Submitted To Mcu" } } })}
                    }
            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new AdminDashboardService(mock.Object, mockActivityLogService.Object, null);
            //Act
            List<AdminDashboardDto> dto = await service.GetDocument(1, 1, false, 1);
            //Assert
            Assert.NotNull(dto);
            Assert.Equal(5, dto.Count);
            Assert.Equal("House Document", dto[1].docName);
            Assert.Equal("Property", dto[2].docName);
            Assert.Equal("Started", dto[3].status);
            Assert.Equal("Started", dto[4].status);
            Assert.Equal("asd", dto[4].files[0].clientName);
        }

        [Fact]
        public async Task TestDeleteServiceTrue()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            List<BsonDocument> list = new List<BsonDocument>()
                                      {
                                          new BsonDocument
                                          {
                                              //Cover all empty fields except status
                                             
                                              {"status", DocumentStatus.BorrowerTodo}

                                          },
                                          new BsonDocument
                                          {
                                              //Cover all empty fields except status
                                             
                                              {"status", DocumentStatus.Completed}

                                          }
                                          ,
                                          new BsonDocument
                                          {
                                              //Cover all empty fields except status
                                             
                                              {"status", DocumentStatus.Deleted}

                                          },
                                          new BsonDocument
                                          {
                                              //Cover all empty fields except status
                                             
                                              {"status", DocumentStatus.Draft}

                                          },
                                          new BsonDocument
                                          {
                                              //Cover all empty fields except status
                                             
                                              {"status", DocumentStatus.ManuallyAdded}

                                          }
                                          ,
                                          new BsonDocument
                                          {
                                              //Cover all empty fields except status
                                             
                                              {"status", DocumentStatus.PendingReview}

                                          },
                                          new BsonDocument
                                          {
                                              //Cover all empty fields except status
                                             
                                              {"status", DocumentStatus.Started}

                                          }
                                      };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);

            mockCursor.SetupGet(x => x.Current).Returns(list);
          
            mockCollection.SetupSequence(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(0)));

            var adminDeleteModel = new AdminDeleteModel() { id = "5eb25d1fe519051af2eeb72d", docId = "5eb25d1fe519051af2eeb72d", requestId = "5eb25d1fe519051af2eeb72d" };

            //Act

            IAdminDashboardService adminDashboardService = new AdminDashboardService(mock.Object, mockActivityLogService.Object, Mock.Of<IRainmakerService>());
            bool result = await adminDashboardService.Delete(adminDeleteModel, 1, new List<string>());
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
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            List<BsonDocument> list = new List<BsonDocument>()
                                      {
                                          new BsonDocument
                                          {
                                              //Cover all empty fields except status
                                             
                                              {"status", "Started"}

                                          }
                                      };
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);

            mockCursor.SetupGet(x => x.Current).Returns(list);
           
            mockCollection.SetupSequence(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(0, 0, BsonInt32.Create(1)));

            var adminDeleteModel = new AdminDeleteModel() { id = "5eb25d1fe519051af2eeb72d", docId = "5eb25d1fe519051af2eeb72d", requestId = "5eb25d1fe519051af2eeb72d" };

            //Act

            IAdminDashboardService adminDashboardService = new AdminDashboardService(mock.Object, mockActivityLogService.Object, Mock.Of<IRainmakerService>());
            bool result = await adminDashboardService.Delete(adminDeleteModel, 1, new List<string>());
            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task TestIsDocumentDraftController()
        {
            //Arrange
            Mock<IAdminDashboardService> mock = new Mock<IAdminDashboardService>();
            RequestIdQuery query = new RequestIdQuery() { requestId = "abc15d1fe456051af2eeb768" };
            mock.Setup(x => x.IsDocumentDraft(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(query);

            var adminDashboardController = new AdminDashboardController(mock.Object, Mock.Of<ILogger<AdminDashboardController>>()
            );

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            adminDashboardController.ControllerContext = context;
            IsDocumentDraft moIsDocumentDraft = new IsDocumentDraft();
            moIsDocumentDraft.loanApplicationId = 14;
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
            Mock<IAsyncCursor<BsonDocument>> mockRequestCursor = new Mock<IAsyncCursor<BsonDocument>>();

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
            ,
                    new BsonDocument
                    {
                        { "requestId" , "" }
                    }
            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockRequestCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockRequestCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.SetupSequence(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object).Returns(mockRequestCursor.Object);

            mockdb.Setup(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new AdminDashboardService(mock.Object, mockActivityLogService.Object, null);
            //Act
            var dto = await service.IsDocumentDraft(14, 3842);
            //Assert
            Assert.NotNull(dto);
        }
        [Fact]
        public async Task TestGetDashboardSettingService()
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
                        { "userId" , 1 },
                        { "pending" , true }
                    }
            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.SetupSequence(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new AdminDashboardService(mock.Object, mockActivityLogService.Object, null);
            //Act
            var dto = await service.GetDashboardSetting(1);
            //Assert
            Assert.NotNull(dto);
            Assert.Equal(1, dto.userId);
        }
        [Fact]
        public async Task TestGetDashboardSettingServiceFalse()
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
                        { "userId" , 1 },
                        { "pending" , true }
                    }
            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.SetupSequence(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new AdminDashboardService(mock.Object, mockActivityLogService.Object, null);
            //Act
            var dto = await service.GetDashboardSetting(1);
            //Assert
            Assert.NotNull(dto);
            Assert.Equal(1,dto.userId);
        }
        [Fact] 
        public async Task TestGetDashboardSetting()
        {
            //Arrange
            Mock<IAdminDashboardService> mock = new Mock<IAdminDashboardService>();
            mock.Setup(x => x.GetDashboardSetting(It.IsAny<int>())).ReturnsAsync(new DashboardSettingModel() { userId=1,pending=true});

            var adminDashboardController = new AdminDashboardController(mock.Object, Mock.Of<ILogger<AdminDashboardController>>()
            );

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            adminDashboardController.ControllerContext = context;
            
            //Act
            IActionResult result = await adminDashboardController.GetDashboardSetting();
            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsType<DashboardSettingModel>(res.Value);
            Assert.Equal(1,data.userId);
        }
    }
}
