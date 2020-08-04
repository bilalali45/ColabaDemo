using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DocumentManagement.API.Controllers;
using DocumentManagement.Entity;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq.Protected;
using Xunit;
using LoanApplication = DocumentManagement.Model.LoanApplication;

namespace DocumentManagement.Tests
{
    public class RequestTest
    {
        [Fact]
        public async Task TestSaveControllerTrue()
        {
            //Arrange
            Mock<IRequestService> mock = new Mock<IRequestService>();
            Mock<IRainmakerService> mockRainMock = new Mock<IRainmakerService>();
            mock.Setup(x => x.Save(It.IsAny<Model.LoanApplication>(), It.IsAny<bool>())).ReturnsAsync(true);
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("FirstName")).Returns(new Claim("FirstName", "Danish"));
            httpContext.Setup(m => m.User.FindFirst("LastName")).Returns(new Claim("LastName", "Faiz"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Connection.RemoteIpAddress).Returns(IPAddress.Parse("127.0.0.1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            mockRainMock.Setup(x => x.PostLoanApplication(It.IsAny<int>(),It.IsAny<bool>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync("{userId:59,userName:'Melissa Merritt'}");

            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );

            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var controller = new RequestController(mock.Object, mockRainMock.Object);

            controller.ControllerContext = context;

            //Act
            LoanApplication loanApplication = new LoanApplication();
            loanApplication.id = "5f0e8d014e72f52edcff3885";
            loanApplication.loanApplicationId = 14;
            loanApplication.tenantId = 1;
            loanApplication.userId = 59;
            loanApplication.userName = "Melissa Merritt";
            loanApplication.requests = new List<Model.Request>();

            Model.Request requestModel = new Model.Request();
            requestModel.id = "5f0ede3cce9c4b62509d0dc0";
            requestModel.userId = 3842;
            requestModel.userName = "Danish Faiz";
            requestModel.message = "Hi Mark";
            requestModel.createdOn = DateTime.UtcNow;
            requestModel.status = DocumentStatus.BorrowerTodo;
            requestModel.documents = new List<Model.RequestDocument>();

            Model.RequestDocument document = new Model.RequestDocument();
            document.id = "5f0ede3cce9c4b62509d0dc1";
            document.status = DocumentStatus.BorrowerTodo;
            document.typeId = "5eb257a3e519051af2eeb624";
            document.displayName = "W2 2020";
            document.message = "please upload salary slip";
            document.files = new List<Model.RequestFile>();

            Model.RequestFile requestFile = new Model.RequestFile();
            requestFile.id = "abc15d1fe456051af2eeb768";
            requestFile.clientName = "3e06ed7f-0620-42f2-b6f5-e7b8ee1f2778.pdf";
            requestFile.serverName = "0c550bb7-7e4b-4384-98eb-5264d9411737.enc";
            requestFile.fileUploadedOn = DateTime.UtcNow;
            requestFile.size = 1904942;
            requestFile.encryptionKey = "FileKey";
            requestFile.encryptionAlgorithm = "AES";
            requestFile.order = 0;
            requestFile.mcuName = "abc";
            requestFile.contentType = "application/pdf";
            requestFile.status = FileStatus.SubmittedToMcu;
            requestFile.byteProStatus = "Active";
            
            document.files.Add(requestFile);

            requestModel.documents.Add(document);

            loanApplication.requests.Add(requestModel);

            IActionResult result = await controller.Save(loanApplication,false);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestSaveControllerFalse()
        {
            //Arrange
            Mock<IRequestService> mock = new Mock<IRequestService>();
            Mock<IRainmakerService> mockRainMock = new Mock<IRainmakerService>();
            mock.Setup(x => x.Save(It.IsAny<Model.LoanApplication>(), It.IsAny<bool>())).ReturnsAsync(false);
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("FirstName")).Returns(new Claim("FirstName", "Danish"));
            httpContext.Setup(m => m.User.FindFirst("LastName")).Returns(new Claim("LastName", "Faiz"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Connection.RemoteIpAddress).Returns(IPAddress.Parse("127.0.0.1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            mockRainMock.Setup(x => x.PostLoanApplication(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync("");

            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );

            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var controller = new RequestController(mock.Object, mockRainMock.Object);

            controller.ControllerContext = context;

            //Act
            LoanApplication loanApplication = new LoanApplication();
            loanApplication.id = "5f0e8d014e72f52edcff3885";
            loanApplication.loanApplicationId = 14;
            loanApplication.tenantId = 1;
            loanApplication.userId = 59;
            loanApplication.userName = "Melissa Merritt";
            loanApplication.requests = new List<Model.Request>();

            Model.Request requestModel = new Model.Request();
            requestModel.id = "5f0ede3cce9c4b62509d0dc0";
            requestModel.userId = 3842;
            requestModel.userName = "Danish Faiz";
            requestModel.message = "Hi Mark";
            requestModel.createdOn = DateTime.UtcNow;
            requestModel.status = DocumentStatus.BorrowerTodo;
            requestModel.documents = new List<Model.RequestDocument>();

            Model.RequestDocument document = new Model.RequestDocument();
            document.id = "5f0ede3cce9c4b62509d0dc1";
            document.status = DocumentStatus.BorrowerTodo;
            document.typeId = "5eb257a3e519051af2eeb624";
            document.displayName = "W2 2020";
            document.message = "please upload salary slip";
            document.files = new List<Model.RequestFile>();

            Model.RequestFile requestFile = new Model.RequestFile();
            requestFile.id = "abc15d1fe456051af2eeb768";
            requestFile.clientName = "3e06ed7f-0620-42f2-b6f5-e7b8ee1f2778.pdf";
            requestFile.serverName = "0c550bb7-7e4b-4384-98eb-5264d9411737.enc";
            requestFile.fileUploadedOn = DateTime.UtcNow;
            requestFile.size = 1904942;
            requestFile.encryptionKey = "FileKey";
            requestFile.encryptionAlgorithm = "AES";
            requestFile.order = 0;
            requestFile.mcuName = "abc";
            requestFile.contentType = "application/pdf";
            requestFile.status = FileStatus.SubmittedToMcu;
            requestFile.byteProStatus = "Active";

            document.files.Add(requestFile);

            requestModel.documents.Add(document);

            loanApplication.requests.Add(requestModel);

            IActionResult result = await controller.Save(loanApplication, false);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestSaveServiceTypeIdNotNull()
        {
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IActivityLogService> mockIActivityLogService = new Mock<IActivityLogService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<StatusList>> mockCollectionStatusList = new Mock<IMongoCollection<StatusList>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorStatusList = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IMongoCollection<Entity.Request>> mockCollectionRequest = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorRequest = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IMongoCollection<Entity.LoanApplication>> mockLoanApplicationCollection = new Mock<IMongoCollection<Entity.LoanApplication>>();
            Mock<IMongoCollection<ActivityLog>> mockCollectionActivityLog = new Mock<IMongoCollection<ActivityLog>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorActivityLog = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> statusList = new List<BsonDocument>()
            {
                new BsonDocument
                {
                    { "_id" , "5ee86503305e33a11c51ebbc"}
                }
            };

            List<BsonDocument> listRequest = new List<BsonDocument>()
            {
                new BsonDocument
                {
                    { "_id" , "5f0ede3cce9c4b62509d0dbf"},
                    { "loanApplicationId" , 14}
                }
            };

            List<BsonDocument> listActivityLog = new List<BsonDocument>()
            {
                new BsonDocument
                {
                    { "_id" , "5f0ffa5cba6f754a10129586"}
                }
            };

            mockCursorStatusList.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorStatusList.SetupGet(x => x.Current).Returns(statusList);

            mockCollectionStatusList.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.StatusList, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorStatusList.Object);

            mockCursorRequest.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorRequest.SetupGet(x => x.Current).Returns(listRequest);

            mockCollectionRequest.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorRequest.Object);

            mockLoanApplicationCollection.Setup(s => s.InsertOneAsync(It.IsAny<Entity.LoanApplication>(), It.IsAny<InsertOneOptions>(), It.IsAny<System.Threading.CancellationToken>()));

            mockCursorActivityLog.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorActivityLog.SetupGet(x => x.Current).Returns(listActivityLog);

            mockCollectionActivityLog.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.ActivityLog, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorActivityLog.Object);


            mockdb.Setup(x => x.GetCollection<StatusList>("StatusList", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionStatusList.Object);
            mockdb.Setup(x => x.GetCollection<Entity.Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionRequest.Object);
            mockdb.Setup(x => x.GetCollection<Entity.LoanApplication>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockLoanApplicationCollection.Object);
            mockdb.Setup(x => x.GetCollection<ActivityLog>("ActivityLog", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionActivityLog.Object);

            mock.Setup(x => x.db).Returns(mockdb.Object);

            //Act
            IRequestService service = new RequestService(mock.Object, mockActivityLogService.Object);

            Model.LoanApplication loanApplication = new Model.LoanApplication();
            loanApplication.userId = 59;
            loanApplication.userName = "Melissa Merritt";
            loanApplication.loanApplicationId = 14;
            loanApplication.tenantId = 1;
            loanApplication.status = "5ee86503305e33a11c51ebbc";
            loanApplication.requests = new List<Model.Request>() { };

            Model.Request request = new Model.Request();
            request.userId = 3842;
            request.userName = "Danish Faiz";
            request.message = "Hi Mark";
            request.documents = new List<Model.RequestDocument>() { };

            Model.RequestDocument requestDocument = new Model.RequestDocument();
            requestDocument.status = "Started";
            requestDocument.displayName = "W2 2020";
            requestDocument.message = "document rejected";
            requestDocument.typeId = "5eb257a3e519051af2eeb624";
            requestDocument.files = new List<Model.RequestFile>() { };

            request.documents.Add(requestDocument);
            
            loanApplication.requests.Add(request);
            
            bool result = await service.Save(loanApplication, false);

            //Assert
            Assert.True(result);

        }

        [Fact]
        public async Task TestSaveServiceTypeIdNull()
        {
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IActivityLogService> mockIActivityLogService = new Mock<IActivityLogService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<StatusList>> mockCollectionStatusList = new Mock<IMongoCollection<StatusList>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorStatusList = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IMongoCollection<Entity.Request>> mockCollectionRequest = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorRequest = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IMongoCollection<Entity.LoanApplication>> mockLoanApplicationCollection = new Mock<IMongoCollection<Entity.LoanApplication>>();
            Mock<IMongoCollection<ActivityLog>> mockCollectionActivityLog = new Mock<IMongoCollection<ActivityLog>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorActivityLog = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> statusList = new List<BsonDocument>()
            {
                new BsonDocument
                {
                    { "_id" , "5ee86503305e33a11c51ebbc"}
                }
            };

            List<BsonDocument> listRequest = new List<BsonDocument>()
            {
                new BsonDocument
                {
                    { "_id" , "5f0ede3cce9c4b62509d0dbf"},
                    { "loanApplicationId" , 14}
                }
            };

            List<BsonDocument> listActivityLog = new List<BsonDocument>()
            {
                new BsonDocument
                {
                    { "_id" , "5f0ffa5cba6f754a10129586"}
                }
            };

            mockCursorStatusList.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorStatusList.SetupGet(x => x.Current).Returns(statusList);

            mockCollectionStatusList.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.StatusList, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorStatusList.Object);

            mockCursorRequest.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorRequest.SetupGet(x => x.Current).Returns(listRequest);

            mockCollectionRequest.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorRequest.Object);

            mockLoanApplicationCollection.Setup(s => s.InsertOneAsync(It.IsAny<Entity.LoanApplication>(), It.IsAny<InsertOneOptions>(), It.IsAny<System.Threading.CancellationToken>()));

            mockCursorActivityLog.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorActivityLog.SetupGet(x => x.Current).Returns(listActivityLog);

            mockCollectionActivityLog.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.ActivityLog, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorActivityLog.Object);


            mockdb.Setup(x => x.GetCollection<StatusList>("StatusList", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionStatusList.Object);
            mockdb.Setup(x => x.GetCollection<Entity.Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionRequest.Object);
            mockdb.Setup(x => x.GetCollection<Entity.LoanApplication>("LoanApplication", It.IsAny<MongoCollectionSettings>())).Returns(mockLoanApplicationCollection.Object);
            mockdb.Setup(x => x.GetCollection<ActivityLog>("ActivityLog", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionActivityLog.Object);

            mock.Setup(x => x.db).Returns(mockdb.Object);

            //Act
            IRequestService service = new RequestService(mock.Object, mockActivityLogService.Object);

            Model.LoanApplication loanApplication = new Model.LoanApplication();
            loanApplication.userId = 59;
            loanApplication.userName = "Melissa Merritt";
            loanApplication.loanApplicationId = 14;
            loanApplication.tenantId = 1;
            loanApplication.status = "5ee86503305e33a11c51ebbc";
            loanApplication.requests = new List<Model.Request>() { };

            Model.Request request = new Model.Request();
            request.userId = 3842;
            request.userName = "Danish Faiz";
            request.message = "Hi Mark";
            request.documents = new List<Model.RequestDocument>() { };

            Model.RequestDocument requestDocument = new Model.RequestDocument();
            requestDocument.status = "Started";
            requestDocument.displayName = "W2 2020";
            requestDocument.message = "document rejected";
            requestDocument.files = new List<Model.RequestFile>() { };

            request.documents.Add(requestDocument);

            loanApplication.requests.Add(request);

            bool result = await service.Save(loanApplication, false);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task TestSaveService()
        {
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IActivityLogService> mockIActivityLogService = new Mock<IActivityLogService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<StatusList>> mockCollectionStatusList = new Mock<IMongoCollection<StatusList>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorStatusList = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IMongoCollection<Entity.Request>> mockCollectionRequest = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorRequest = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IMongoCollection<Entity.LoanApplication>> mockLoanApplicationCollection = new Mock<IMongoCollection<Entity.LoanApplication>>();
            Mock<IMongoCollection<ActivityLog>> mockCollectionActivityLog = new Mock<IMongoCollection<ActivityLog>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorActivityLog = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorDraftDocument = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> statusList = new List<BsonDocument>()
            {
                new BsonDocument
                {
                    { "_id" , "5ee86503305e33a11c51ebbc"}
                }
            };

            List<BsonDocument> listRequest = new List<BsonDocument>()
            {
                new BsonDocument
                {
                    { "_id" , "5f0ede3cce9c4b62509d0dbf"},
                    { "loanApplicationId" , 14}
                }
            };

            List<BsonDocument> listDocumentDraft = new List<BsonDocument>()
            {
                new BsonDocument
                {
                    { "_id" , "5f0ede3cce9c4b62509d0dbf"},
                    { "docId" , "5f2147136621531660dc42c23"},
                    { "requestId" , "5f2147116621531660dc42bf"}
                }
            };

            List<BsonDocument> listActivityLog = new List<BsonDocument>()
            {
                new BsonDocument
                {
                    { "_id" , "5f0ffa5cba6f754a10129586"}
                }
            };

            mockCursorStatusList.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorStatusList.SetupGet(x => x.Current).Returns(statusList);

            mockCollectionStatusList.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.StatusList, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorStatusList.Object);

            mockCursorRequest.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorRequest.Setup(x => x.Current).Returns(listRequest);

            mockCursorDraftDocument.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorDraftDocument.Setup(x => x.Current).Returns(listDocumentDraft);

            mockCollectionRequest.SetupSequence(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorRequest.Object).Returns(mockCursorDraftDocument.Object);

            mockLoanApplicationCollection.Setup(s => s.InsertOneAsync(It.IsAny<Entity.LoanApplication>(), It.IsAny<InsertOneOptions>(), It.IsAny<System.Threading.CancellationToken>()));

            mockCursorActivityLog.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorActivityLog.SetupGet(x => x.Current).Returns(listActivityLog);

            mockCollectionActivityLog.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.ActivityLog, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorActivityLog.Object);

            mockdb.Setup(x => x.GetCollection<StatusList>("StatusList", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionStatusList.Object);
            mockdb.Setup(x => x.GetCollection<Entity.Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionRequest.Object);
            mockdb.Setup(x => x.GetCollection<Entity.LoanApplication>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockLoanApplicationCollection.Object);
            mockdb.Setup(x => x.GetCollection<ActivityLog>("ActivityLog", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionActivityLog.Object);

            mock.Setup(x => x.db).Returns(mockdb.Object);

            //Act
            IRequestService service = new RequestService(mock.Object, mockActivityLogService.Object);

            Model.LoanApplication loanApplication = new Model.LoanApplication();
            loanApplication.userId = 59;
            loanApplication.userName = "Melissa Merritt";
            loanApplication.loanApplicationId = 1;
            loanApplication.tenantId = 1;
            loanApplication.status = "5ee86503305e33a11c51ebbc";
            loanApplication.requests = new List<Model.Request>() { };

            Model.Request request = new Model.Request();
            request.userId = 3842;
            request.userName = "Danish Faiz";
            request.message = "Hi Mark";
            request.documents = new List<Model.RequestDocument>() { };

            Model.RequestDocument requestDocument = new Model.RequestDocument();
            requestDocument.status = "Started";
            requestDocument.displayName = "";
            requestDocument.message = "document rejected";
            requestDocument.typeId = "5eb257a3e519051af2eeb624";
            requestDocument.docId = "5f2147136621531660dc42c2";
            requestDocument.requestId = "5f2147116621531660dc42bf";
            requestDocument.files = new List<Model.RequestFile>() { };

            request.documents.Add(requestDocument);

            loanApplication.requests.Add(request);

            bool result = await service.Save(loanApplication, false);

            //Assert
            Assert.True(result);

        }

        [Fact]
        public async Task TestSaveServiceIsDraftTrue()
        {
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IActivityLogService> mockIActivityLogService = new Mock<IActivityLogService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<StatusList>> mockCollectionStatusList = new Mock<IMongoCollection<StatusList>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorStatusList = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IMongoCollection<Entity.Request>> mockCollectionRequest = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorRequest = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IMongoCollection<Entity.LoanApplication>> mockLoanApplicationCollection = new Mock<IMongoCollection<Entity.LoanApplication>>();
            Mock<IMongoCollection<ActivityLog>> mockCollectionActivityLog = new Mock<IMongoCollection<ActivityLog>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorActivityLog = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> statusList = new List<BsonDocument>()
            {
                new BsonDocument
                {
                    { "_id" , "5ee86503305e33a11c51ebbc"}
                }
            };

            List<BsonDocument> listRequest = new List<BsonDocument>()
            {
                new BsonDocument
                {
                    { "_id" , "5f0ede3cce9c4b62509d0dbf"},
                    { "loanApplicationId" , 14}
                }
            };

            List<BsonDocument> listActivityLog = new List<BsonDocument>()
            {
                new BsonDocument
                {
                    { "_id" , "5f0ffa5cba6f754a10129586"}
                }
            };

            mockCursorStatusList.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorStatusList.SetupGet(x => x.Current).Returns(statusList);

            mockCollectionStatusList.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.StatusList, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorStatusList.Object);

            mockCursorRequest.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorRequest.SetupGet(x => x.Current).Returns(listRequest);

            mockCollectionRequest.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorRequest.Object);

            mockLoanApplicationCollection.Setup(s => s.InsertOneAsync(It.IsAny<Entity.LoanApplication>(), It.IsAny<InsertOneOptions>(), It.IsAny<System.Threading.CancellationToken>()));

            mockCursorActivityLog.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorActivityLog.SetupGet(x => x.Current).Returns(listActivityLog);

            mockCollectionActivityLog.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.ActivityLog, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorActivityLog.Object);


            mockdb.Setup(x => x.GetCollection<StatusList>("StatusList", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionStatusList.Object);
            mockdb.Setup(x => x.GetCollection<Entity.Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionRequest.Object);
            mockdb.Setup(x => x.GetCollection<Entity.LoanApplication>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockLoanApplicationCollection.Object);
            mockdb.Setup(x => x.GetCollection<ActivityLog>("ActivityLog", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionActivityLog.Object);

            mock.Setup(x => x.db).Returns(mockdb.Object);

            //Act
            IRequestService service = new RequestService(mock.Object, mockActivityLogService.Object);

            Model.LoanApplication loanApplication = new Model.LoanApplication();
            loanApplication.userId = 59;
            loanApplication.userName = "Melissa Merritt";
            loanApplication.loanApplicationId = 1;
            loanApplication.tenantId = 1;
            loanApplication.status = "5ee86503305e33a11c51ebbc";
            loanApplication.requests = new List<Model.Request>() { };

            Model.Request request = new Model.Request();
            request.userId = 3842;
            request.userName = "Danish Faiz";
            request.message = "Hi Mark";
            request.documents = new List<Model.RequestDocument>() { };

            Model.RequestDocument requestDocument = new Model.RequestDocument();
            requestDocument.status = "Started";
            requestDocument.displayName = "";
            requestDocument.message = "document rejected";
            requestDocument.typeId = "5eb257a3e519051af2eeb624";
            requestDocument.files = new List<Model.RequestFile>() { };

            request.documents.Add(requestDocument);

            loanApplication.requests.Add(request);

            bool result = await service.Save(loanApplication, true);

            //Assert
            Assert.True(result);

        }

        [Fact]
        public async Task TestSaveServiceActivityIdNull()
        {
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IActivityLogService> mockIActivityLogService = new Mock<IActivityLogService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<StatusList>> mockCollectionStatusList = new Mock<IMongoCollection<StatusList>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorStatusList = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IMongoCollection<Entity.Request>> mockCollectionRequest = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorRequest = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IMongoCollection<Entity.LoanApplication>> mockLoanApplicationCollection = new Mock<IMongoCollection<Entity.LoanApplication>>();
            Mock<IMongoCollection<ActivityLog>> mockCollectionActivityLog = new Mock<IMongoCollection<ActivityLog>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorActivityLog = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> statusList = new List<BsonDocument>()
            {
                new BsonDocument
                {
                    { "_id" , "5ee86503305e33a11c51ebbc"}
                }
            };

            List<BsonDocument> listRequest = new List<BsonDocument>()
            {
                new BsonDocument
                {
                    { "_id" , "5f0ede3cce9c4b62509d0dbf"},
                    { "loanApplicationId" , 14}
                }
            };

            List<BsonDocument> listActivityLog = new List<BsonDocument>()
            {
                new BsonDocument
                {
                    { "_id" , BsonString.Empty}
                }
            };

            mockCursorStatusList.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorStatusList.SetupGet(x => x.Current).Returns(statusList);

            mockCollectionStatusList.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.StatusList, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorStatusList.Object);

            mockCursorRequest.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorRequest.SetupGet(x => x.Current).Returns(listRequest);

            mockCollectionRequest.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorRequest.Object);

            mockLoanApplicationCollection.Setup(s => s.InsertOneAsync(It.IsAny<Entity.LoanApplication>(), It.IsAny<InsertOneOptions>(), It.IsAny<System.Threading.CancellationToken>()));

            mockCursorActivityLog.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorActivityLog.SetupGet(x => x.Current).Returns(listActivityLog);

            mockCollectionActivityLog.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.ActivityLog, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorActivityLog.Object);


            mockdb.Setup(x => x.GetCollection<StatusList>("StatusList", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionStatusList.Object);
            mockdb.Setup(x => x.GetCollection<Entity.Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionRequest.Object);
            mockdb.Setup(x => x.GetCollection<Entity.LoanApplication>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockLoanApplicationCollection.Object);
            mockdb.Setup(x => x.GetCollection<ActivityLog>("ActivityLog", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionActivityLog.Object);

            mock.Setup(x => x.db).Returns(mockdb.Object);

            //Act
            IRequestService service = new RequestService(mock.Object, mockActivityLogService.Object);

            Model.LoanApplication loanApplication = new Model.LoanApplication();
            loanApplication.userId = 59;
            loanApplication.userName = "Melissa Merritt";
            loanApplication.loanApplicationId = 1;
            loanApplication.tenantId = 1;
            loanApplication.status = "5ee86503305e33a11c51ebbc";
            loanApplication.requests = new List<Model.Request>() { };

            Model.Request request = new Model.Request();
            request.userId = 3842;
            request.userName = "Danish Faiz";
            request.message = "Hi Mark";
            request.documents = new List<Model.RequestDocument>() { };

            Model.RequestDocument requestDocument = new Model.RequestDocument();
            requestDocument.status = "Started";
            requestDocument.displayName = "";
            requestDocument.message = "document rejected";
            requestDocument.typeId = "5eb257a3e519051af2eeb624";
            requestDocument.files = new List<Model.RequestFile>() { };

            request.documents.Add(requestDocument);

            loanApplication.requests.Add(request);

            bool result = await service.Save(loanApplication, false);

            //Assert
            Assert.True(result);

        }

        [Fact]
        public async Task TestGetDraftController()
        {
            //Arrange
            Mock<IRequestService> mock = new Mock<IRequestService>();
            List<DraftDocumentDTO> list = new List<DraftDocumentDTO>() { { new DraftDocumentDTO()
            {
                message = "Hi Mark",
                typeId = "5ebc18cba5d847268075ad4f",
                docId = "5f2155194ce1db1a7cdb17e9",
                requestId = "5f2155194ce1db1a7cdb17e8",
                docName = "W3 2020",
                docMessage = "please upload salary slip"
            } } };

            GetDraft getDraft = new GetDraft();
            getDraft.loanApplicationId = 14;

            mock.Setup(x => x.GetDraft(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(list);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var controller = new RequestController(mock.Object, null);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            IActionResult result = await controller.GetDraft(getDraft);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as List<DraftDocumentDTO>;
            Assert.Single(content);
            Assert.Equal("Hi Mark", content[0].message);
            Assert.Equal("5ebc18cba5d847268075ad4f", content[0].typeId);
            Assert.Equal("5f2155194ce1db1a7cdb17e8", content[0].requestId);
            Assert.Equal("5f2155194ce1db1a7cdb17e9", content[0].docId);
            Assert.Equal("W3 2020", content[0].docName);
            Assert.Equal("please upload salary slip", content[0].docMessage);
        }

        [Fact]
        public async Task TestGetDraftService()
        {
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Request>> mockCollectionRequest = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorRequest = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursorRequestDraft = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> listDocumentDraft = new List<BsonDocument>()
            {
                new BsonDocument
                {
                    { "message" , BsonString.Empty},
                    { "typeId" , BsonString.Empty},
                    { "docId" , BsonString.Empty},
                    { "requestId" , BsonString.Empty},
                    { "docName" , BsonString.Empty},
                    { "docMessage" , BsonString.Empty},
                    { "typeName" , BsonString.Empty},
                    { "typeMessage" , BsonString.Empty},
                    { "messages" , BsonArray.Create(new Message[]{ })}
                }
                ,new BsonDocument
                {
                    { "message" , "Hi Mark"},
                    { "typeId" , "5ebc18cba5d847268075ad4f"},
                    { "docId" , "5f2155194ce1db1a7cdb17e9"},
                    { "requestId" , "5f2155194ce1db1a7cdb17e8"},
                    { "docName" , "W3 2020"},
                    { "docMessage" , "please upload salary slip"},
                    { "typeName" , "Salary Slip"},
                    { "typeMessage" , "Credit report has been uploaded"},
                    { "messages" , BsonArray.Create(new Message[]{ })}
                }
                 ,new BsonDocument
                {
                    { "message" , "Hi Mark"},
                    { "typeId" , "5ebc18cba5d847268075ad4f"},
                    { "docId" , "5f2155194ce1db1a7cdb17e9"},
                    { "requestId" , "5f2155194ce1db1a7cdb17e8"},
                    { "docName" , "W3 2020"},
                    { "docMessage" , BsonString.Empty},
                    { "typeName" , "Salary Slip"},
                    { "typeMessage" , "Credit report has been uploaded"},
                    { "messages" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "tenantId", 1 },{ "message", "Credit report has been uploaded" } } })}
                }
                 ,new BsonDocument
                {
                    { "message" , "Hi Mark"},
                    { "typeId" , "5ebc18cba5d847268075ad4f"},
                    { "docId" , "5f2155194ce1db1a7cdb17e9"},
                    { "requestId" , "5f2155194ce1db1a7cdb17e8"},
                    { "docName" , "W3 2020"},
                    { "docMessage" , BsonString.Empty},
                    { "typeName" , "Salary Slip"},
                    { "typeMessage" , "Credit report has been uploaded"},
                    { "messages" , BsonNull.Value }
                }
            };

            List<BsonDocument> listRequestDraft = new List<BsonDocument>()
            {
                new BsonDocument
                {
                    { "message" , BsonString.Empty},
                    { "typeId" , BsonString.Empty},
                    { "docId" , BsonString.Empty},
                    { "requestId" , BsonString.Empty},
                    { "docName" , BsonString.Empty},
                    { "docMessage" , BsonString.Empty},
                    { "typeName" , BsonString.Empty},
                    { "typeMessage" , BsonString.Empty},
                    { "messages" , BsonArray.Create(new Message[]{ })}
                }
                ,new BsonDocument
                {
                   { "message" , "Hi Mark"},
                    { "typeId" , "5ebc18cba5d847268075ad4f"},
                    { "requestId" , "5f2155194ce1db1a7cdb17e8"},
                    { "docName" , "W3 2020"},
                    { "docMessage" , "please upload salary slip"},
                    { "typeName" , "Salary Slip"},
                    { "typeMessage" , "Credit report has been uploaded"},
                     { "messages" , BsonArray.Create(new Message[]{ })}
                }
                 ,new BsonDocument
                {
                    { "message" , "Hi Mark"},
                    { "typeId" , "5ebc18cba5d847268075ad4f"},
                    { "docId" , "5f2155194ce1db1a7cdb17e9"},
                    { "requestId" , "5f2155194ce1db1a7cdb17e8"},
                    { "docName" , "W3 2020"},
                    { "docMessage" , BsonString.Empty},
                    { "typeName" , "Salary Slip"},
                    { "typeMessage" , "Credit report has been uploaded"},
                    { "messages" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "tenantId", 1 },{ "message", "Credit report has been uploaded" } } })}
                }
                 ,new BsonDocument
                {
                    { "message" , "Hi Mark"},
                    { "typeId" , "5ebc18cba5d847268075ad4f"},
                    { "docId" , "5f2155194ce1db1a7cdb17e9"},
                    { "requestId" , "5f2155194ce1db1a7cdb17e8"},
                    { "docName" , "W3 2020"},
                    { "docMessage" , BsonString.Empty},
                    { "typeName" , "Salary Slip"},
                    { "typeMessage" , "Credit report has been uploaded"},
                    { "messages" , BsonNull.Value }
                }
            };

            mockCursorRequest.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorRequest.Setup(x => x.Current).Returns(listDocumentDraft);

            mockCursorRequest.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorRequest.Setup(x => x.Current).Returns(listDocumentDraft);

            mockCursorRequestDraft.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursorRequestDraft.Setup(x => x.Current).Returns(listRequestDraft);

            mockCollectionRequest.SetupSequence(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursorRequest.Object).Returns(mockCursorRequestDraft.Object);

            mockdb.Setup(x => x.GetCollection<Entity.Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionRequest.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new RequestService(mock.Object, null);

            //Act
            List<DraftDocumentDTO> dto = await service.GetDraft(14, 1);

            //Assert
            Assert.NotNull(dto);
            Assert.Equal(8, dto.Count);
            Assert.Equal("", dto[0].docId);
            Assert.Equal("please upload salary slip", dto[1].docMessage);
            Assert.Equal("Credit report has been uploaded", dto[2].docMessage);
            Assert.Equal("Credit report has been uploaded", dto[3].docMessage);
            Assert.Equal("", dto[4].docMessage);
            Assert.Equal("please upload salary slip", dto[5].docMessage);
            Assert.Equal("Credit report has been uploaded", dto[6].docMessage);
            Assert.Equal("Credit report has been uploaded", dto[7].docMessage);
        }

        [Fact]
        public async Task TestGetEmailTemplateController()
        {
            //Arrange
            Mock<IRequestService> mock = new Mock<IRequestService>();

            mock.Setup(x => x.GetEmailTemplate(It.IsAny<int>())).ReturnsAsync("Email Template");

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var controller = new RequestController(mock.Object, null);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            IActionResult result = await controller.GetEmailTemplate();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as string;
        }

        [Fact]
        public async Task TestGetEmailTemplateServiceTrue()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IMongoCollection<Tenant>> mockCollectionEmailTemplate = new Mock<IMongoCollection<Tenant>>();

            List<BsonDocument> emailTemplate = new List<BsonDocument>()
            {
                new BsonDocument
                 {
                        { "emailTemplate" , "Email Template"}
                 }
                };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true);
            mockCursor.SetupGet(x => x.Current).Returns(emailTemplate);

            mockCollectionEmailTemplate.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Tenant, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Tenant>("Tenant", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionEmailTemplate.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new RequestService(mock.Object, null);
            //Act
            string dto = await service.GetEmailTemplate(1);
            //Assert
            Assert.NotNull(dto);
            Assert.Equal("Email Template", dto);
        }

            [Fact]
            public async Task TestGetEmailTemplateServiceFalse()
            {
                //Arrange
                Mock<IMongoService> mock = new Mock<IMongoService>();
                Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
                Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
                Mock<IMongoCollection<Tenant>> mockCollectionEmailTemplate = new Mock<IMongoCollection<Tenant>>();

                List<BsonDocument> emailTemplate = new List<BsonDocument>()
            {
                new BsonDocument
                 {
                        { "emailTemplate" , "Email Template"}
                 }
                };

                mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false);
                mockCursor.SetupGet(x => x.Current).Returns(emailTemplate);

                mockCollectionEmailTemplate.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Tenant, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

                mockdb.Setup(x => x.GetCollection<Tenant>("Tenant", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionEmailTemplate.Object);

                mock.SetupGet(x => x.db).Returns(mockdb.Object);

                var service = new RequestService(mock.Object, null);
                //Act
                string dto = await service.GetEmailTemplate(1);
                //Assert
                Assert.NotNull(dto);
                Assert.Equal(string.Empty, dto);
            }
        }
}
