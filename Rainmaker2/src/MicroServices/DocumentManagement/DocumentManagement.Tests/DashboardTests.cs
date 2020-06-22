using DocumentManagement.API.Controllers;
using DocumentManagement.Entity;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Routing;

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

            var dashboardController = new DashboardController(mock.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            dashboardController.ControllerContext = context;

            //DashboardController controller = new DashboardController(mock.Object);
            //Act
            IActionResult result = await dashboardController.GetPendingDocuments(1, 1);
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
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
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
                        { "files" , BsonArray.Create(new RequestFile[]{ })}
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
                        { "files" , BsonArray.Create(new RequestFile[]{ })}
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
                        { "files" , BsonArray.Create(new RequestFile[]{ })}
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
                        { "files" , BsonArray.Create(new RequestFile[]{ })}
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
                        { "files" , BsonArray.Create(new RequestFile[]{ })}
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
                        { "files" , BsonArray.Create(new RequestFile[]{ })}
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
                        { "files" , BsonArray.Create(new RequestFile[]{ })}
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
                        { "files" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "clientName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } })}
                    }
            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

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

            var dashboardController = new DashboardController(mock.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            dashboardController.ControllerContext = context;

            //DashboardController controller = new DashboardController(mock.Object);
            //Act
            IActionResult result = await dashboardController.GetSubmittedDocuments(1, 1);
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
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
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
                        { "files" , BsonArray.Create(new RequestFile[]{ })}
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
                        { "files" , BsonArray.Create(new RequestFile[]{ })}
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
                        { "files" , BsonArray.Create(new RequestFile[]{ })}
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
                        { "files" , BsonArray.Create(new RequestFile[]{ })}
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
                        { "files" , BsonArray.Create(new RequestFile[]{ })}
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
                        { "files" , BsonArray.Create(new RequestFile[]{ })}
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
                        { "files" , BsonArray.Create(new RequestFile[]{ })}
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
                        { "files" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "clientName", "asd" },{ "fileUploadedOn", BsonDateTime.Create(DateTime.Now) }, { "size", 1 },{ "order",1 } } })}
                    }
            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new DashboardService(mock.Object);
            //Act
            List<DashboardDTO> dto = await service.GetSubmittedDocuments(1, 1,1);
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
        public async Task TestGetDashboardStatusController()
        {
            //Arrange
            Mock<IDashboardService> mock = new Mock<IDashboardService>();
            List<DashboardStatus> list = new List<DashboardStatus>() { { new DashboardStatus() { order = 1 } } };

            mock.Setup(x => x.GetDashboardStatus(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(list);

            var dashboardController = new DashboardController(mock.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            dashboardController.ControllerContext = context;

            //DashboardController controller = new DashboardController(mock.Object);
            //Act
            IActionResult result = await dashboardController.GetDashboardStatus(1, 1);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as List<DashboardStatus>;
            Assert.Single(content);
            Assert.Equal(1, content[0].order);
        }

        [Fact]
        public async Task TestGetDashboardStatusService()
        { 
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<StatusList>> mockCollectionStatusList = new Mock<IMongoCollection<StatusList>>();
            Mock<IAsyncCursor<StatusList>> mockCursor = new Mock<IAsyncCursor<StatusList>>();
            Mock<IMongoCollection<LoanApplication>> mockCollectionLoanApplication = new Mock<IMongoCollection<LoanApplication>>();
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
                        { "requests" , BsonArray.Create(new Request[]{ }) },
                        { "status" ,  BsonString.Empty}

                 }
                };
         
            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCursor1.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor1.SetupGet(x => x.Current).Returns(listLoanApplication);

            mockCursor2.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor2.SetupGet(x => x.Current).Returns(new List<BsonDocument>());

            //    mockCursor2.SetupGet(x => x.Current).Returns(new List<BsonDocument>());

            mockCollectionStatusList.Setup(x => x.FindAsync<StatusList>(FilterDefinition<StatusList>.Empty, It.IsAny<FindOptions<StatusList, StatusList>>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockCursor.Object);
            mockCollectionLoanApplication.Setup(x => x.FindAsync<BsonDocument>(It.IsAny<FilterDefinition<LoanApplication>>(), It.IsAny<FindOptions<LoanApplication, BsonDocument>>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockCursor1.Object);
            mockCollectionLoanApplication.Setup(x => x.FindAsync<BsonDocument>(It.IsAny<FilterDefinition<LoanApplication>>(), It.IsAny<FindOptions<LoanApplication, BsonDocument>>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockCursor2.Object);

            //    mockCollectionLoanApplication.Setup(x => x.FindAsync<BsonDocument>(It.IsAny<FilterDefinition<LoanApplication>>(), It.IsAny<FindOptions<LoanApplication, BsonDocument>>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockCursor2.Object);
            mockCursor1.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor2.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false).ReturnsAsync(false);

            mockdb.Setup(x => x.GetCollection<StatusList>("StatusList", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionStatusList.Object);
            mockdb.Setup(x => x.GetCollection<LoanApplication>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionLoanApplication.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);
           
            var service = new DashboardService(mock.Object);
            //Act
            List<DashboardStatus> dto = await service.GetDashboardStatus(1, 1,1);
            //Assert
            Assert.NotNull(dto);
            Assert.Single(dto);
            Assert.Equal(3,dto[0].order);

        }
    }
}
