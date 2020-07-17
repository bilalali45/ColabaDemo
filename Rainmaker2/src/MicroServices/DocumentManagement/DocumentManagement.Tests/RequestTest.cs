﻿using Microsoft.AspNetCore.Http;
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
            loanApplication.requests = new List<Request>();

            Request requestModel = new Request();
            requestModel.id = "5f0ede3cce9c4b62509d0dc0";
            requestModel.userId = 3842;
            requestModel.userName = "Danish Faiz";
            requestModel.message = "Hi Mark";
            requestModel.createdOn = DateTime.UtcNow;
            requestModel.status = DocumentStatus.BorrowerTodo;
            requestModel.documents = new List<RequestDocument>();

            RequestDocument document = new RequestDocument();
            document.id = "5f0ede3cce9c4b62509d0dc1";
            document.activityId = "5f0ede3cce9c4b62509d0dc2";
            document.status = DocumentStatus.BorrowerTodo;
            document.typeId = "5eb257a3e519051af2eeb624";
            document.displayName = "W2 2020";
            document.message = "please upload salary slip";
            document.files = new List<RequestFile>();

            RequestFile requestFile = new RequestFile();
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
            loanApplication.requests = new List<Request>();

            Request requestModel = new Request();
            requestModel.id = "5f0ede3cce9c4b62509d0dc0";
            requestModel.userId = 3842;
            requestModel.userName = "Danish Faiz";
            requestModel.message = "Hi Mark";
            requestModel.createdOn = DateTime.UtcNow;
            requestModel.status = DocumentStatus.BorrowerTodo;
            requestModel.documents = new List<RequestDocument>();

            RequestDocument document = new RequestDocument();
            document.id = "5f0ede3cce9c4b62509d0dc1";
            document.activityId = "5f0ede3cce9c4b62509d0dc2";
            document.status = DocumentStatus.BorrowerTodo;
            document.typeId = "5eb257a3e519051af2eeb624";
            document.displayName = "W2 2020";
            document.message = "please upload salary slip";
            document.files = new List<RequestFile>();

            RequestFile requestFile = new RequestFile();
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
            Mock<IMongoCollection<Request>> mockCollectionRequest = new Mock<IMongoCollection<Request>>();
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
            mockdb.Setup(x => x.GetCollection<Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionRequest.Object);
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
            loanApplication.requests = new List<Request>() { };

            Request request = new Request();
            request.userId = 3842;
            request.userName = "Danish Faiz";
            request.message = "Hi Mark";
            request.documents = new List<RequestDocument>() { };

            RequestDocument requestDocument = new RequestDocument();
            requestDocument.status = "Started";
            requestDocument.displayName = "W2 2020";
            requestDocument.message = "document rejected";
            requestDocument.typeId = "5eb257a3e519051af2eeb624";
            requestDocument.files = new List<RequestFile>() { };

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
            Mock<IMongoCollection<Request>> mockCollectionRequest = new Mock<IMongoCollection<Request>>();
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
            mockdb.Setup(x => x.GetCollection<Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionRequest.Object);
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
            loanApplication.requests = new List<Request>() { };

            Request request = new Request();
            request.userId = 3842;
            request.userName = "Danish Faiz";
            request.message = "Hi Mark";
            request.documents = new List<RequestDocument>() { };

            RequestDocument requestDocument = new RequestDocument();
            requestDocument.status = "Started";
            requestDocument.displayName = "W2 2020";
            requestDocument.message = "document rejected";
            requestDocument.files = new List<RequestFile>() { };

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
            Mock<IMongoCollection<Request>> mockCollectionRequest = new Mock<IMongoCollection<Request>>();
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
            mockdb.Setup(x => x.GetCollection<Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionRequest.Object);
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
            loanApplication.requests = new List<Request>() { };

            Request request = new Request();
            request.userId = 3842;
            request.userName = "Danish Faiz";
            request.message = "Hi Mark";
            request.documents = new List<RequestDocument>() { };

            RequestDocument requestDocument = new RequestDocument();
            requestDocument.status = "Started";
            requestDocument.displayName = "";
            requestDocument.message = "document rejected";
            requestDocument.typeId = "5eb257a3e519051af2eeb624";
            requestDocument.files = new List<RequestFile>() { };

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
            Mock<IMongoCollection<Request>> mockCollectionRequest = new Mock<IMongoCollection<Request>>();
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
            mockdb.Setup(x => x.GetCollection<Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionRequest.Object);
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
            loanApplication.requests = new List<Request>() { };

            Request request = new Request();
            request.userId = 3842;
            request.userName = "Danish Faiz";
            request.message = "Hi Mark";
            request.documents = new List<RequestDocument>() { };

            RequestDocument requestDocument = new RequestDocument();
            requestDocument.status = "Started";
            requestDocument.displayName = "";
            requestDocument.message = "document rejected";
            requestDocument.typeId = "5eb257a3e519051af2eeb624";
            requestDocument.files = new List<RequestFile>() { };

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
            Mock<IMongoCollection<Request>> mockCollectionRequest = new Mock<IMongoCollection<Request>>();
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
            mockdb.Setup(x => x.GetCollection<Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollectionRequest.Object);
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
            loanApplication.requests = new List<Request>() { };

            Request request = new Request();
            request.userId = 3842;
            request.userName = "Danish Faiz";
            request.message = "Hi Mark";
            request.documents = new List<RequestDocument>() { };

            RequestDocument requestDocument = new RequestDocument();
            requestDocument.status = "Started";
            requestDocument.displayName = "";
            requestDocument.message = "document rejected";
            requestDocument.typeId = "5eb257a3e519051af2eeb624";
            requestDocument.files = new List<RequestFile>() { };

            request.documents.Add(requestDocument);

            loanApplication.requests.Add(request);

            bool result = await service.Save(loanApplication, false);

            //Assert
            Assert.True(result);

        }
    }
}
