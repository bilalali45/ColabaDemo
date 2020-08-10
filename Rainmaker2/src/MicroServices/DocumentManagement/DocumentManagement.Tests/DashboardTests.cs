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
using Microsoft.Extensions.Logging;
using Xunit;
using Microsoft.Extensions.Primitives;
using System.IO;

namespace DocumentManagement.Tests
{
    public class DashboardTests
    {
        [Fact]
        public async Task TestGetAllPendingDocumentsController()
        {
            //Arrange
            Mock<IDashboardService> mock = new Mock<IDashboardService>();
            List<DashboardDTO> list = new List<DashboardDTO>() { { new DashboardDTO() { docId = "1" } } };

            mock.Setup(x => x.GetPendingDocuments(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(list);

            var dashboardController = new DashboardController(mock.Object,Mock.Of<ILogger<DashboardController>>(), null);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            dashboardController.ControllerContext = context;

            GetPendingDocuments moGetPendingDocuments= new GetPendingDocuments();
            moGetPendingDocuments.loanApplicationId = 1;
       
            IActionResult result = await dashboardController.GetPendingDocuments(moGetPendingDocuments);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as List<DashboardDTO>;
            Assert.Single(content);
            Assert.Equal("1", content[0].docId);
        }

        [Fact]
        public async Task TestGetAllPendingDocumentsService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Request>> mockCollection = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()    
            { new BsonDocument
                    {
                        //Cover all empty fields
                        { "_id" , BsonString.Empty },
                        { "createdOn" , BsonDateTime.Create(DateTime.Now) },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "docMessage" , BsonString.Empty },
                        { "typeName" , BsonString.Empty },
                        { "typeMessage" , BsonString.Empty },
                        { "messages" , BsonArray.Create(new Message[]{ }) },
                        { "isRejected",BsonNull.Value},
                        { "files" , BsonArray.Create(new Entity.RequestFile[]{ })}
                    }
            ,
                    new BsonDocument
                    {
                        //Cover all empty fields except docName
                        { "_id" , BsonString.Empty },
                        { "createdOn" , BsonDateTime.Create(DateTime.Now) },
                        { "docId" , BsonString.Empty },
                        { "docName" , "House Document" },
                        { "docMessage" , BsonString.Empty },
                        { "typeName" ,  BsonString.Empty },
                        { "typeMessage" , BsonString.Empty },
                        { "messages" , BsonArray.Create(new Message[]{ }) },
                        { "isRejected", BsonBoolean.True},
                        { "files" , BsonArray.Create(new Entity.RequestFile[]{ })}
                    }
                    ,
                    new BsonDocument
                    {
                        //Cover all empty fields except typeName
                        { "_id" , BsonString.Empty },
                        { "createdOn" , BsonDateTime.Create(DateTime.Now) },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "docMessage" , BsonString.Empty },
                        { "typeName" ,  "Property" },
                        { "typeMessage" , BsonString.Empty },
                        { "messages" , BsonArray.Create(new Message[]{ }) },
                        { "isRejected",BsonNull.Value},
                        { "files" , BsonArray.Create(new Entity.RequestFile[]{ })}
                    }
                    ,
                new BsonDocument
                    {
                        //Cover all empty fields except docMessage
                        { "_id" , BsonString.Empty },
                        { "createdOn" , BsonDateTime.Create(DateTime.Now) },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "docMessage" , "please upload house document" },
                        { "typeName" ,  BsonString.Empty },
                        { "typeMessage" , BsonString.Empty },
                        { "messages" , BsonArray.Create(new Message[]{ }) },
                        { "isRejected", BsonBoolean.False},
                        { "files" , BsonArray.Create(new Entity.RequestFile[]{ })}
                    }
                ,
                 new BsonDocument
                    {
                        //Cover all empty fields except typeMessage
                        { "_id" , BsonString.Empty },
                        { "createdOn" , BsonDateTime.Create(DateTime.Now) },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "docMessage" , BsonString.Empty },
                        { "typeName" , BsonString.Empty  },
                        { "typeMessage" , "please upload house document" },
                        { "messages" , BsonArray.Create(new Message[]{ })},
                        { "isRejected", BsonBoolean.False},
                        { "files" , BsonArray.Create(new Entity.RequestFile[]{ })}
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except messages
                        { "_id" , BsonString.Empty },
                        { "createdOn" , BsonDateTime.Create(DateTime.Now) },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "docMessage" , BsonString.Empty },
                        { "typeName" ,  BsonString.Empty },
                        { "typeMessage" , BsonString.Empty },
                        { "messages" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "message", "please upload house document" },{ "tenantId" , 1 } } })},
                        { "isRejected", BsonBoolean.False},
                        { "files" , BsonArray.Create(new Entity.RequestFile[]{ })}
                    }
                  ,
                 new BsonDocument
                    {
                        //Cover all empty fields except typeMessage and messages
                        { "_id" , BsonString.Empty },
                        { "createdOn" , BsonDateTime.Create(DateTime.Now) },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "docMessage" , BsonString.Empty },
                        { "typeName" ,  BsonString.Empty },
                        { "typeMessage" , "please upload house document" },
                        { "messages" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "message", BsonString.Empty },{ "tenantId" , 2 } } })},
                        { "isRejected", BsonBoolean.False},
                        { "files" , BsonArray.Create(new Entity.RequestFile[]{ })}
                    }
                  ,
                 new BsonDocument
                    {
                        //Cover all empty fields except typeMessage
                        { "_id" , BsonString.Empty },
                        { "createdOn" , BsonDateTime.Create(DateTime.Now) },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "docMessage" , BsonString.Empty },
                        { "typeName" ,  BsonString.Empty },
                        { "typeMessage" , "please upload house document" },
                        { "messages" , BsonNull.Value },
                        { "isRejected", BsonBoolean.False},
                        { "files" , BsonNull.Value}
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "createdOn" , BsonDateTime.Create(DateTime.Now) },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "docMessage" , BsonString.Empty },
                        { "typeName" ,  BsonString.Empty },
                        { "typeMessage" , BsonString.Empty },
                        { "messages" , BsonNull.Value },
                        { "isRejected", BsonBoolean.False},
                        { "files" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "clientName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } })}
                    }
            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new DashboardService(mock.Object);
            //Act
            List<DashboardDTO> dto = await service.GetPendingDocuments(1, 1,1);
            //Assert
            Assert.NotNull(dto);
            Assert.Equal(9, dto.Count);
            Assert.Equal("House Document", dto[1].docName);
            Assert.Equal("Property", dto[2].docName);
            Assert.Equal("please upload house document", dto[3].docMessage);
            Assert.Equal("please upload house document", dto[4].docMessage);
            Assert.Equal("please upload house document", dto[5].docMessage);
            Assert.Equal("please upload house document", dto[6].docMessage);
            Assert.Equal("please upload house document", dto[7].docMessage);
            Assert.Equal("asd", dto[8].files[0].clientName);
        }

        [Fact]
        public async Task TestGetAllSubmittedDocumentsController()
        {
            //Arrange
            Mock<IDashboardService> mock = new Mock<IDashboardService>();
            List<DashboardDTO> list = new List<DashboardDTO>() { { new DashboardDTO() { docId = "1" } } };

            mock.Setup(x => x.GetSubmittedDocuments(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(list);

            var dashboardController = new DashboardController(mock.Object, Mock.Of<ILogger<DashboardController>>(),null);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            dashboardController.ControllerContext = context;
            GetSubmittedDocuments moGetSubmittedDocuments= new GetSubmittedDocuments();
            moGetSubmittedDocuments.loanApplicationId = 1;
            //Act
            IActionResult result = await dashboardController.GetSubmittedDocuments(moGetSubmittedDocuments);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as List<DashboardDTO>;
            Assert.Single(content);
            Assert.Equal("1", content[0].docId);
        }

        [Fact]
        public async Task TestGetAllSubmittedDocumentsService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Request>> mockCollection = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            { new BsonDocument
                    {
                        //Cover all empty fields
                        { "_id" , BsonString.Empty },
                        { "createdOn" , BsonDateTime.Create(DateTime.Now) },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "docMessage" , BsonString.Empty },
                        { "typeName" , BsonString.Empty },
                        { "typeMessage" , BsonString.Empty },
                        { "messages" , BsonArray.Create(new Message[]{ }) },
                        { "isRejected",BsonNull.Value},
                        { "files" , BsonArray.Create(new Entity.RequestFile[]{ })}
                    }
            ,
                    new BsonDocument
                    {
                        //Cover all empty fields except docName
                        { "_id" , BsonString.Empty },
                        { "createdOn" , BsonDateTime.Create(DateTime.Now) },
                        { "docId" , BsonString.Empty },
                        { "docName" , "House Document" },
                        { "docMessage" , BsonString.Empty },
                        { "typeName" ,  BsonString.Empty },
                        { "typeMessage" , BsonString.Empty },
                        { "messages" , BsonArray.Create(new Message[]{ }) },
                        { "isRejected", BsonBoolean.True},
                        { "files" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "clientName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 },{ "status",FileStatus.SubmittedToMcu }, { "id", "5f30d944ccbf4475dcdfed33" } } })}
                    }
                    ,
                    new BsonDocument
                    {
                        //Cover all empty fields except typeName
                        { "_id" , BsonString.Empty },
                        { "createdOn" , BsonDateTime.Create(DateTime.Now) },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "docMessage" , BsonString.Empty },
                        { "typeName" ,  "Property" },
                        { "typeMessage" , BsonString.Empty },
                        { "messages" , BsonArray.Create(new Message[]{ }) },
                        { "isRejected", BsonNull.Value},
                        { "files" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "clientName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 },{ "status",FileStatus.SubmittedToMcu }, { "id", "5f30d944ccbf4475dcdfed33" } } })}
                    }
                    ,
                new BsonDocument
                    {
                        //Cover all empty fields except docMessage,typeName and files
                        { "_id" , BsonString.Empty },
                        { "createdOn" , BsonDateTime.Create(DateTime.Now) },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonNull.Value },
                        { "docMessage" , "please upload house document" },
                        { "typeName" ,  "Property" },
                        { "typeMessage" , BsonString.Empty },
                        { "messages" , BsonArray.Create(new Message[]{ }) },
                        { "isRejected", BsonBoolean.False },
                        { "files" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "clientName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 },{ "status",FileStatus.SubmittedToMcu }, { "id", "5f30d944ccbf4475dcdfed33" } } })}
                    }
                ,
                 new BsonDocument
                    {
                        //Cover all empty fields except typeMessage and files
                        { "_id" , BsonString.Empty },
                        { "createdOn" , BsonDateTime.Create(DateTime.Now) },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "docMessage" , BsonString.Empty },
                        { "typeName" , BsonString.Empty  },
                        { "typeMessage" , "please upload house document" },
                        { "messages" , BsonArray.Create(new Message[]{ })},
                        { "isRejected",BsonBoolean.False },
                       { "files" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "clientName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 },{ "status",FileStatus.SubmittedToMcu }, { "id", "5f30d944ccbf4475dcdfed33" } } })}
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except messages and files
                        { "_id" , BsonString.Empty },
                        { "createdOn" , BsonDateTime.Create(DateTime.Now) },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "docMessage" , BsonString.Empty },
                        { "typeName" ,  BsonString.Empty },
                        { "typeMessage" , BsonString.Empty },
                        { "messages" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "message", "please upload house document" },{ "tenantId" , 1 } } })},
                        { "isRejected", BsonBoolean.False },
                        { "files" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "clientName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 },{ "status",FileStatus.SubmittedToMcu }, { "id", "5f30d944ccbf4475dcdfed33" } } })}
                    }
                  ,
                 new BsonDocument
                    {
                        //Cover all empty fields except typeMessage, messages and files
                        { "_id" , BsonString.Empty },
                        { "createdOn" , BsonDateTime.Create(DateTime.Now) },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "docMessage" , BsonString.Empty },
                        { "typeName" ,  BsonString.Empty },
                        { "typeMessage" , "please upload house document" },
                        { "messages" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "message", BsonString.Empty },{ "tenantId" , 2 } } })},
                        { "isRejected", BsonBoolean.False },
                        { "files" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "clientName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 },{ "status",FileStatus.SubmittedToMcu }, { "id", "5f30d944ccbf4475dcdfed33" } } })}
                    }
                  ,
                 new BsonDocument
                    {
                        //Cover all empty fields except typeMessage
                        { "_id" , BsonString.Empty },
                        { "createdOn" , BsonDateTime.Create(DateTime.Now) },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "docMessage" , BsonString.Empty },
                        { "typeName" ,  BsonString.Empty },
                        { "typeMessage" , "please upload house document" },
                        { "messages" , BsonNull.Value },
                        { "isRejected", BsonNull.Value },
                        { "files" , BsonNull.Value }
                    }
                 ,
                 new BsonDocument
                    {
                        //Cover all empty fields except files
                        { "_id" , BsonString.Empty },
                        { "createdOn" , BsonDateTime.Create(DateTime.Now) },
                        { "docId" , BsonString.Empty },
                        { "docName" , BsonString.Empty },
                        { "docMessage" , BsonString.Empty },
                        { "typeName" ,  BsonString.Empty },
                        { "typeMessage" , BsonString.Empty },
                        { "messages" , BsonNull.Value },
                        { "isRejected", BsonBoolean.False},
                        { "files" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "clientName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 },{ "status", BsonString.Empty }, { "id", "5f30d944ccbf4475dcdfed33" } } })}
                    }
            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new DashboardService(mock.Object);
            //Act
            List<DashboardDTO> dto = await service.GetSubmittedDocuments(1, 1,1);
            //Assert
            Assert.NotNull(dto);
            Assert.Equal(7, dto.Count);
            Assert.Equal("House Document", dto[0].docName);
            Assert.Equal("Property", dto[1].docName);
            Assert.Equal("please upload house document", dto[2].docMessage);
            Assert.Equal("please upload house document", dto[3].docMessage);
            Assert.Equal("please upload house document", dto[4].docMessage);
            Assert.Equal("please upload house document", dto[5].docMessage);
            Assert.Equal("", dto[6].docMessage);
        }

        [Fact]
        public async Task TestGetDashboardStatusController()
        {
            //Arrange
            Mock<IDashboardService> mock = new Mock<IDashboardService>();
            List<DashboardStatus> list = new List<DashboardStatus>() { { new DashboardStatus() { order = 1 } } };

            mock.Setup(x => x.GetDashboardStatus(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(list);

            var dashboardController = new DashboardController(mock.Object, Mock.Of<ILogger<DashboardController>>(),null);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            dashboardController.ControllerContext = context;
            GetDashboardStatus moGetDashboardStatus= new GetDashboardStatus();
            moGetDashboardStatus.loanApplicationId = 1;
           //Act
           IActionResult result = await dashboardController.GetDashboardStatus(moGetDashboardStatus);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as List<DashboardStatus>;
            Assert.Single(content);
            Assert.Equal(1, content[0].order);
        }

        [Fact]
        public async Task TestGetDashboardStatusServiceTrue()
        { 
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<StatusList>> mockCollectionStatusList = new Mock<IMongoCollection<StatusList>>();
            Mock<IAsyncCursor<StatusList>> mockCursor = new Mock<IAsyncCursor<StatusList>>();
            Mock<IMongoCollection<Entity.LoanApplication>> mockCollectionLoanApplication = new Mock<IMongoCollection<Entity.LoanApplication>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor1 = new Mock<IAsyncCursor<BsonDocument>>();
            
            List<StatusList> list = new List<StatusList>()
            {
                new StatusList()
                {
                    id="5ee86503305e33a11c51ebbc",
                    name="Fill out application",
                    description="Tell us about yourself and your financial situation so we can find loan options for you",
                    order=3
                }
            };

            List<BsonDocument> listLoanApplication = new List<BsonDocument>()
            {
                new BsonDocument
                {       //cover all field
                        { "id" , "5eb25d1fe519051af2eeb72d" },
                        { "customerId" , 1275},
                        { "tenantId" , 1 },
                        { "loanApplicationId" ,1},
                         { "status" ,  "5ee86503305e33a11c51ebbc" },
                       { "requests",new BsonArray {

                               new BsonDocument {
                                { "id","abc15d1fe456051af2eeb768" },
                                { "employeeId", 1234},
                                { "createdOn", "2020-06-17T08:00:00.000"},
                                { "status", "requested" },
                                { "message", "Dear John, please send following documents." } ,

                                { "documents",   new BsonArray {
                                  new BsonDocument  {
                                {"id" , "ddd25d1fe456057652eeb72d"},
                                { "typeId" , 1},
                                { "displayName" , "W2 2016"},
                                { "message" ,  "please upload salary slip for year 2016"},
                                { "status",  "requested"},
                                { "files" ,new BsonArray {
                                  new BsonDocument {
                                  {"id" ,"5ee76ed6c766b54188e807a0"},
                                  { "clientName" , "SSL enable in mongodb"},
                                  { "serverName", "ccb09149-a9fb-4af0-a0dd-e7daa753cb0b.enc"},
                                  { "fileUploadedOn" ,  "2020-06-17T08:06:23.338Z"},
                                  { "size" , 854},
                                  {"encryptionKey" , "FileKey"},
                                  { "encryptionAlgorithm" , "AES"},
                                  { "order" , 3},
                                  {"mcuName" , ""},
                                  {"contentType" , "application/octet-stream"},
                                  {"status" , "submitted"}
                                                 }
                                                         }
                                 }
                                                      }
                                }

                }
                       }}}
                    }
                ,
                new BsonDocument
                 { 
                        //cover all field except loanApplicationId
                         { "id" ,BsonString.Empty},
                        { "customerId" , BsonString.Empty},
                        { "tenantId" ,  BsonString.Empty },
                        { "loanApplicationId" , BsonString.Empty },
                        { "requests" , BsonArray.Create(new Entity.Request[]{ }) },
                        { "status" ,  BsonString.Empty}

                 }
                };
         
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCursor1.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(true);
            mockCursor1.SetupGet(x => x.Current).Returns(listLoanApplication);

            mockCollectionStatusList.Setup(x => x.FindAsync<StatusList>(FilterDefinition<StatusList>.Empty, It.IsAny<FindOptions<StatusList, StatusList>>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockCursor.Object);
            mockCollectionLoanApplication.Setup(x => x.FindAsync<BsonDocument>(It.IsAny<FilterDefinition<Entity.LoanApplication>>(), It.IsAny<FindOptions<Entity.LoanApplication, BsonDocument>>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockCursor1.Object);

            mockCursor1.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(true);

            mockdb.Setup(x => x.GetCollection<StatusList>("StatusList", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionStatusList.Object);
            mockdb.Setup(x => x.GetCollection<Entity.LoanApplication>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionLoanApplication.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);
           
            var service = new DashboardService(mock.Object);
            //Act
            List<DashboardStatus> dto = await service.GetDashboardStatus(1, 1,1);
            //Assert
            Assert.NotNull(dto);
            Assert.Single(dto);
            Assert.Equal(3,dto[0].order);

        }

        [Fact]
        public async Task TestGetDashboardStatusServiceFalse()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<StatusList>> mockCollectionStatusList = new Mock<IMongoCollection<StatusList>>();
            Mock<IAsyncCursor<StatusList>> mockCursor = new Mock<IAsyncCursor<StatusList>>();
            Mock<IMongoCollection<Entity.LoanApplication>> mockCollectionLoanApplication = new Mock<IMongoCollection<Entity.LoanApplication>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor1 = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor2 = new Mock<IAsyncCursor<BsonDocument>>();

            List<StatusList> list = new List<StatusList>()
            {
                new StatusList()
                {
                    id="5ee86503305e33a11c51ebbc",
                    name="Fill out application",
                    description="Tell us about yourself and your financial situation so we can find loan options for you",
                    order=3
                }
            };

            List<BsonDocument> listLoanApplication = new List<BsonDocument>()
            {
                new BsonDocument
                {       //cover all field
                        { "id" , "5eb25d1fe519051af2eeb72d" },
                        { "customerId" , 1275},
                        { "tenantId" , 1 },
                        { "loanApplicationId" ,1},
                         { "status" ,  "5ee86503305e33a11c51ebbc" },
                       { "requests",new BsonArray {

                               new BsonDocument {
                                { "id","abc15d1fe456051af2eeb768" },
                                { "employeeId", 1234},
                                { "createdOn", "2020-06-17T08:00:00.000"},
                                { "status", "requested" },
                                { "message", "Dear John, please send following documents." } ,

                                { "documents",   new BsonArray {
                                  new BsonDocument  {
                                {"id" , "ddd25d1fe456057652eeb72d"},
                                { "typeId" , 1},
                                { "displayName" , "W2 2016"},
                                { "message" ,  "please upload salary slip for year 2016"},
                                { "status",  "requested"},
                                { "files" ,new BsonArray {
                                  new BsonDocument {
                                  {"id" ,"5ee76ed6c766b54188e807a0"},
                                  { "clientName" , "SSL enable in mongodb"},
                                  { "serverName", "ccb09149-a9fb-4af0-a0dd-e7daa753cb0b.enc"},
                                  { "fileUploadedOn" ,  "2020-06-17T08:06:23.338Z"},
                                  { "size" , 854},
                                  {"encryptionKey" , "FileKey"},
                                  { "encryptionAlgorithm" , "AES"},
                                  { "order" , 3},
                                  {"mcuName" , ""},
                                  {"contentType" , "application/octet-stream"},
                                  {"status" , "submitted"}
                                                 }
                                                         }
                                 }
                                                      }
                                }

                }
                       }}}
                    }
                ,
                new BsonDocument
                 { 
                        //cover all field except loanApplicationId
                         { "id" ,BsonString.Empty},
                        { "customerId" , BsonString.Empty},
                        { "tenantId" ,  BsonString.Empty },
                        { "loanApplicationId" , BsonString.Empty },
                        { "requests" , BsonArray.Create(new Entity.Request[]{ }) },
                        { "status" ,  BsonString.Empty}

                 }
                };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCursor1.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false).ReturnsAsync(false);
            mockCursor1.SetupGet(x => x.Current).Returns(listLoanApplication);

            mockCollectionStatusList.Setup(x => x.FindAsync<StatusList>(FilterDefinition<StatusList>.Empty, It.IsAny<FindOptions<StatusList, StatusList>>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockCursor.Object);
            mockCollectionLoanApplication.Setup(x => x.FindAsync<BsonDocument>(It.IsAny<FilterDefinition<Entity.LoanApplication>>(), It.IsAny<FindOptions<Entity.LoanApplication, BsonDocument>>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockCursor1.Object);
            mockCollectionLoanApplication.Setup(x => x.FindAsync<BsonDocument>(It.IsAny<FilterDefinition<Entity.LoanApplication>>(), It.IsAny<FindOptions<Entity.LoanApplication, BsonDocument>>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockCursor2.Object);

            mockCursor1.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false).ReturnsAsync(false);

            mockdb.Setup(x => x.GetCollection<StatusList>("StatusList", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionStatusList.Object);
            mockdb.Setup(x => x.GetCollection<Entity.LoanApplication>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionLoanApplication.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new DashboardService(mock.Object);
            //Act
            List<DashboardStatus> dto = await service.GetDashboardStatus(1, 1, 1);
            //Assert
            Assert.NotNull(dto);
            Assert.Single(dto);
            Assert.Equal(3, dto[0].order);

        }

        [Fact]
        public async Task TestGetFooterController()
        {
            //Arrange
            Mock<IDashboardService> mock = new Mock<IDashboardService>();
            Mock<IRainmakerService> mockRainMock = new Mock<IRainmakerService>();

            mock.Setup(x => x.GetFooterText(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync("document footer text");
            LoanApplicationModel loanApplicationModel = new LoanApplicationModel();
            loanApplicationModel.BusinessUnitId = 1;
            mockRainMock.Setup(x => x.GetByLoanApplicationId(It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(loanApplicationModel);

            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );

            var dashboardController = new DashboardController(mock.Object, Mock.Of<ILogger<DashboardController>>(), mockRainMock.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            dashboardController.ControllerContext = context;

            //DashboardController controller = new DashboardController(mock.Object, Mock.Of<ILogger<DashboardController>>(),null);
           
            GetFooterText moGetFooterText= new GetFooterText();
            moGetFooterText.loanApplicationId = 1;

            //Act
            IActionResult result = await dashboardController.GetFooterText(moGetFooterText);

            //Assert
            Assert.NotNull(result);
            
            Assert.IsType<OkObjectResult>(result);
         }

        [Fact]
        public async Task TestGetFooterServiceTrue()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IMongoCollection<BusinessUnit>> mockCollectionFooterText = new Mock<IMongoCollection<BusinessUnit>>();

            List<BsonDocument> listFooterText = new List<BsonDocument>()
            {
                new BsonDocument
                 { 
                        { "footerText" , "document footer text"}
                 }
                };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true);
            mockCursor.SetupGet(x => x.Current).Returns(listFooterText);

            mockCollectionFooterText.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<BusinessUnit, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<BusinessUnit>("BusinessUnit", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionFooterText.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new DashboardService(mock.Object);
            //Act
            string dto = await service.GetFooterText(1, 1);
            //Assert
            Assert.NotNull(dto);
            Assert.Equal("document footer text", dto);
        }

        [Fact]
        public async Task TestGetFooterServiceFalse()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IMongoCollection<BusinessUnit>> mockCollectionFooterText = new Mock<IMongoCollection<BusinessUnit>>();

            List<BsonDocument> listFooterText = new List<BsonDocument>()
            { 
                new BsonDocument
                 {
                        { "footerText" , BsonString.Empty}
                 }
                };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(listFooterText);

            mockCollectionFooterText.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<BusinessUnit, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<BusinessUnit>("BusinessUnit", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionFooterText.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new DashboardService(mock.Object);
            //Act
            string dto = await service.GetFooterText(1, 1);
            //Assert
            Assert.NotNull(dto);
            Assert.Equal(string.Empty, dto);
        }
    }
}
