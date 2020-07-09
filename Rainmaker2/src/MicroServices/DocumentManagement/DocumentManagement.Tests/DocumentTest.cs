﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DocumentManagement.API.Controllers;
using DocumentManagement.Entity;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using MongoDB.Bson;
using MongoDB.Driver;
using Xunit;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading;

namespace DocumentManagement.Tests
{
    public class DocumentTest
    {
        [Fact]
        public async Task TestGetDocumemntsByTemplateIdsController()
        {
            //Arrange
            Mock<IDocumentService> mock = new Mock<IDocumentService>();
            List<DocumentModel> list = new List<DocumentModel>() { { new DocumentModel() { docId = "5ebc18cba5d847268075ad4f" } } };

            mock.Setup(x => x.GetDocumentsByTemplateIds(It.IsAny<TemplateIdModel>())).ReturnsAsync(list);

            var documentController = new DocumentController(mock.Object);

            //Act
            IActionResult result = await documentController.GetDocumentsByTemplateIds(It.IsAny<TemplateIdModel>());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as List<DocumentModel>;
            Assert.Single(content);
            Assert.Equal("5ebc18cba5d847268075ad4f", content[0].docId);
        }
        [Fact]
        public async Task TestGetFilesController()
        {
            //Arrange
            Mock<IDocumentService> mock = new Mock<IDocumentService>();
            List<DocumendDTO> list = new List<DocumendDTO>() { { new DocumendDTO()
            {
                id="5eb25d1fe519051af2eeb72d",
                requestId = "abc15d1fe456051af2eeb768",
                docId = "aaa25d1fe456051af2eeb72d",
                docName = "W2 2016"
            } } };

            mock.Setup(x => x.GetFiles(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(list);

            var documentController = new DocumentController(mock.Object);
            //Act
            IActionResult result = await documentController.GetFiles("1", "1", "1");
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as List<DocumendDTO>;
            Assert.Single(content);
            Assert.Equal("5eb25d1fe519051af2eeb72d", content[0].id);
            Assert.Equal("abc15d1fe456051af2eeb768", content[0].requestId);
            Assert.Equal("aaa25d1fe456051af2eeb72d", content[0].docId);
            Assert.Equal("W2 2016", content[0].docName);

        }

        [Fact]
        public async Task TestGetActivityLogController()
        {
            //Arrange
            Mock<IDocumentService> mock = new Mock<IDocumentService>();
            List<ActivityLogDTO> list = new List<ActivityLogDTO>() { { new ActivityLogDTO()
            {
                id="5f046210f50dc78d7b0c059c",
                userId=3842,
                userName = "abc",
                typeId = "abc15d1fe456051af2eeb768",
                docId = "aaa25d1fe456051af2eeb72d",
                activity = "abc",
                dateTime = Convert.ToDateTime("2020-06-25T07:39:57.233Z"),
                loanId = "5eb25d1fe519051af2eeb72d"
            } } };

            mock.Setup(x => x.GetActivityLog(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(list);

            var documentController = new DocumentController(mock.Object);
            //Act
            IActionResult result = await documentController.GetActivityLog("1", "1", "1");
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as List<ActivityLogDTO>;
            Assert.Single(content);
            Assert.Equal("5f046210f50dc78d7b0c059c", content[0].id);
            Assert.Equal("abc15d1fe456051af2eeb768", content[0].typeId);
            Assert.Equal("aaa25d1fe456051af2eeb72d", content[0].docId);
            Assert.Equal("abc", content[0].userName);
            Assert.Equal("abc", content[0].activity);
            Assert.Equal(Convert.ToDateTime("2020-06-25T07:39:57.233Z"), content[0].dateTime);
            Assert.Equal("5eb25d1fe519051af2eeb72d", content[0].loanId);

        }

        [Fact]
        public async Task TestGetFilesService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            {
                new BsonDocument
                    {
                        //Cover all empty  fields  
                        { "_id" ,  BsonString.Empty},
                        { "docId" ,BsonString.Empty},
                        { "docName" , BsonString.Empty},
                        { "typeName" , BsonString.Empty},
                        { "requestId" , BsonString.Empty}
                    }
                ,new BsonDocument
                {
                    //Cover all empty  fields except _id
                    { "_id" ,  "5eb25d1fe519051af2eeb72d"},
                    { "docId" ,BsonString.Empty},
                    { "docName" , BsonString.Empty},
                    { "typeName" , BsonString.Empty},
                    { "requestId" , BsonString.Empty}
                }
                ,new BsonDocument
                {
                    //Cover all empty  fields  except docId
                    { "_id" ,  BsonString.Empty},
                    { "docId" ,"aaa25d1fe456051af2eeb72d"},
                    { "docName" , BsonString.Empty},
                    { "typeName" , BsonString.Empty},
                    { "requestId" , BsonString.Empty}
                }
                ,new BsonDocument
                {
                    //Cover all empty  fields except docName
                    { "_id" ,  BsonString.Empty},
                    { "docId" ,BsonString.Empty},
                    { "docName" , "W2 2016"},
                    { "typeName" , BsonString.Empty},
                    { "requestId" , BsonString.Empty}
                }
                ,new BsonDocument
                {
                    //Cover all empty  fields except typeName
                    { "_id" ,  BsonString.Empty},
                    { "docId" ,BsonString.Empty},
                    { "docName" , BsonString.Empty},
                    { "typeName" ,"W2 2016"},
                    { "requestId" , BsonString.Empty}
                }
                ,new BsonDocument
                {
                    //Cover all empty  fields except requestId
                    { "_id" ,  BsonString.Empty},
                    { "docId" ,BsonString.Empty},
                    { "docName" , BsonString.Empty},
                    { "typeName" ,BsonString.Empty},
                    { "requestId" ,"abc15d1fe456051af2eeb768"}
                }
            };


            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);


            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new DocumentService(mock.Object);
            //Act
            List<DocumendDTO> dto = await service.GetFiles("5eb25d1fe519051af2eeb72d", "abc15d1fe456051af2eeb768", "aaa25d1fe456051af2eeb72d");
            //Assert
            Assert.NotNull(dto);
            Assert.Equal(6, dto.Count);
            Assert.Equal("5eb25d1fe519051af2eeb72d", dto[1].id);
            Assert.Equal("aaa25d1fe456051af2eeb72d", dto[2].docId);
            Assert.Equal("W2 2016", dto[3].docName);
            Assert.Equal("W2 2016", dto[4].docName);
            Assert.Equal("abc15d1fe456051af2eeb768", dto[5].requestId);
        }

        [Fact]
        public async Task TestGetActivityLogServiceTypeIdNotNull()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.ActivityLog>> mockCollection = new Mock<IMongoCollection<Entity.ActivityLog>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            {
                new BsonDocument
                    {
                        //Cover all empty  fields  
                        { "userId" ,  3842},
                        { "userName" ,BsonString.Empty},
                        { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                        { "_id" , BsonString.Empty},
                        { "typeId" , BsonString.Empty},
                        { "docId" , BsonString.Empty},
                        { "activity" , BsonString.Empty},
                        { "loanId" , BsonString.Empty}
                    }
                ,new BsonDocument
                {
                    //Cover all empty  fields except userId
                    { "userId" , 3842 },
                    { "userName" ,BsonString.Empty},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" , BsonString.Empty},
                    { "typeId" , BsonString.Empty},
                    { "docId" , BsonString.Empty},
                    { "activity" , BsonString.Empty},
                    { "loanId" , BsonString.Empty}
                }
                ,new BsonDocument
                {
                    //Cover all empty  fields  except userName
                    { "userId" , 3842},
                    { "userName" , "abc"},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" , BsonString.Empty},
                    { "typeId" , BsonString.Empty},
                    { "docId" , BsonString.Empty},
                    { "activity" , BsonString.Empty},
                    { "loanId" , BsonString.Empty}
                }

                ,new BsonDocument
                {
                    //Cover all empty  fields except id
                    { "userId" ,  3842},
                    { "userName" ,BsonString.Empty},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" ,  "5f046210f50dc78d7b0c059c"},
                    { "typeId" , BsonString.Empty},
                    { "docId" , BsonString.Empty},
                    { "activity" , BsonString.Empty},
                    { "loanId" , BsonString.Empty}
                }
                ,new BsonDocument
                {
                    //Cover all empty  fields except typeId
                    { "userId" ,  3842},
                    { "userName" ,BsonString.Empty},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" , BsonString.Empty},
                    { "typeId" ,  "abc15d1fe456051af2eeb768"},
                    { "docId" , BsonString.Empty},
                    { "activity" , BsonString.Empty},
                    { "loanId" , BsonString.Empty}
                }
                ,new BsonDocument
                {
                    //Cover all empty  fields except docId
                    { "userId" ,  3842},
                    { "userName" ,BsonString.Empty},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" , BsonString.Empty},
                    { "typeId" , BsonString.Empty},
                    { "docId" ,"aaa25d1fe456051af2eeb72d" },
                    { "activity" , BsonString.Empty},
                    { "loanId" , BsonString.Empty}
                }
                ,new BsonDocument
                {
                    //Cover all empty  fields except activity
                    { "userId" ,  3842},
                    { "userName" ,BsonString.Empty},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" , BsonString.Empty},
                    { "typeId" , BsonString.Empty},
                    { "docId" , BsonString.Empty},
                    { "activity" , "abc" },
                    { "loanId" , BsonString.Empty}
                }
                ,new BsonDocument
                {
                    //Cover all empty  fields except loanId
                    { "userId" ,  3842},
                    { "userName" ,BsonString.Empty},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" , BsonString.Empty},
                    { "typeId" , BsonString.Empty},
                    { "docId" ,BsonString.Empty },
                    { "activity" , BsonString.Empty},
                    { "loanId" , "5eb25d1fe519051af2eeb72d" }
                }
            };


            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.ActivityLog, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);


            mockdb.Setup(x => x.GetCollection<Entity.ActivityLog>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new DocumentService(mock.Object);
            //Act
            List<ActivityLogDTO> dto = await service.GetActivityLog("5eb25d1fe519051af2eeb72d", "abc15d1fe456051af2eeb768", "");
            //Assert
            Assert.NotNull(dto);
            Assert.Equal(8, dto.Count);
            Assert.Equal(3842, dto[1].userId);
            Assert.Equal("abc", dto[2].userName);
            Assert.Equal("5f046210f50dc78d7b0c059c", dto[3].id);
            Assert.Equal("abc15d1fe456051af2eeb768", dto[4].typeId);
            Assert.Equal("aaa25d1fe456051af2eeb72d", dto[5].docId);
            Assert.Equal("abc", dto[6].activity);
            Assert.Equal("5eb25d1fe519051af2eeb72d", dto[7].loanId);
        }

        [Fact]
        public async Task TestGetActivityLogServiceTypeIdNull()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.ActivityLog>> mockCollection = new Mock<IMongoCollection<Entity.ActivityLog>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            {
                new BsonDocument
                    {
                        //Cover all empty  fields  
                        { "userId" ,  3842},
                        { "userName" ,BsonString.Empty},
                        { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                        { "_id" , BsonString.Empty},
                        { "typeId" , BsonString.Empty},
                        { "docId" , BsonString.Empty},
                        { "activity" , BsonString.Empty},
                        { "loanId" , BsonString.Empty}
                    }
                ,new BsonDocument
                {
                    //Cover all empty  fields except userId
                    { "userId" , 3842 },
                    { "userName" ,BsonString.Empty},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" , BsonString.Empty},
                    { "typeId" , BsonString.Empty},
                    { "docId" , BsonString.Empty},
                    { "activity" , BsonString.Empty},
                    { "loanId" , BsonString.Empty}
                }
                ,new BsonDocument
                {
                    //Cover all empty  fields  except userName
                    { "userId" , 3842},
                    { "userName" , "abc"},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" , BsonString.Empty},
                    { "typeId" , BsonString.Empty},
                    { "docId" , BsonString.Empty},
                    { "activity" , BsonString.Empty},
                    { "loanId" , BsonString.Empty}
                }

                ,new BsonDocument
                {
                    //Cover all empty  fields except id
                    { "userId" ,  3842},
                    { "userName" ,BsonString.Empty},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" ,  "5f046210f50dc78d7b0c059c"},
                    { "typeId" , BsonString.Empty},
                    { "docId" , BsonString.Empty},
                    { "activity" , BsonString.Empty},
                    { "loanId" , BsonString.Empty}
                }
                ,new BsonDocument
                {
                    //Cover all empty  fields except typeId
                    { "userId" ,  3842},
                    { "userName" ,BsonString.Empty},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" , BsonString.Empty},
                    { "typeId" ,  "abc15d1fe456051af2eeb768"},
                    { "docId" , BsonString.Empty},
                    { "activity" , BsonString.Empty},
                    { "loanId" , BsonString.Empty}
                }
                ,new BsonDocument
                {
                    //Cover all empty  fields except docId
                    { "userId" ,  3842},
                    { "userName" ,BsonString.Empty},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" , BsonString.Empty},
                    { "typeId" , BsonString.Empty},
                    { "docId" ,"aaa25d1fe456051af2eeb72d" },
                    { "activity" , BsonString.Empty},
                    { "loanId" , BsonString.Empty}
                }
                ,new BsonDocument
                {
                    //Cover all empty  fields except activity
                    { "userId" ,  3842},
                    { "userName" ,BsonString.Empty},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" , BsonString.Empty},
                    { "typeId" , BsonString.Empty},
                    { "docId" , BsonString.Empty},
                    { "activity" , "abc" },
                    { "loanId" , BsonString.Empty}
                }
                ,new BsonDocument
                {
                    //Cover all empty  fields except loanId
                    { "userId" ,  3842},
                    { "userName" ,BsonString.Empty},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" , BsonString.Empty},
                    { "typeId" , BsonString.Empty},
                    { "docId" ,BsonString.Empty },
                    { "activity" , BsonString.Empty},
                    { "loanId" , "5eb25d1fe519051af2eeb72d" }
                }
            };


            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.ActivityLog, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);


            mockdb.Setup(x => x.GetCollection<Entity.ActivityLog>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new DocumentService(mock.Object);
            //Act
            List<ActivityLogDTO> dto = await service.GetActivityLog("5eb25d1fe519051af2eeb72d", "", "aaa25d1fe456051af2eeb72d");
            //Assert
            Assert.NotNull(dto);
            Assert.Equal(8, dto.Count);
            Assert.Equal(3842, dto[1].userId);
            Assert.Equal("abc", dto[2].userName);
            Assert.Equal("5f046210f50dc78d7b0c059c", dto[3].id);
            Assert.Equal("abc15d1fe456051af2eeb768", dto[4].typeId);
            Assert.Equal("aaa25d1fe456051af2eeb72d", dto[5].docId);
            Assert.Equal("abc", dto[6].activity);
            Assert.Equal("5eb25d1fe519051af2eeb72d", dto[7].loanId);
        }

        [Fact]
        public async Task TestGetDocumemntsByTemplateIdsService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Template>> mockCollection = new Mock<IMongoCollection<Entity.Template>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            {
                new BsonDocument
                 {
                     { "docId" ,"5ebc18cba5d847268075ad4f" },
                     { "typeName" , BsonString.Empty},
                     { "docMessage" , "Credit report has been uploaded"},
                     { "messages" , BsonArray.Create(new Message[]{ })},
                     { "docName" , "Credit Report"}
                 }
                  ,
                 new BsonDocument
                 {
                     { "docId" , "5ebc18cba5d847268075ad4f"},
                     { "typeName" ,BsonString.Empty},
                     { "docMessage" , BsonString.Empty},
                     { "messages" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "tenantId", 1 },{ "message", "Credit report has been uploaded" } } })},
                     { "docName" , "Credit Report"},
                 }
                 ,
                 new BsonDocument
                 {
                     { "docId" , BsonString.Empty},
                     { "typeName" , BsonString.Empty},
                     { "docMessage" , "Credit report has been uploaded"},
                     { "messages" , BsonArray.Create(new Message[]{ })},
                     { "docName" , BsonString.Empty}
                 }
                 ,
                 new BsonDocument
                 {
                     { "docId" , BsonString.Empty},
                     { "typeName" ,BsonString.Empty},
                     { "docMessage" , "Credit report has been uploaded"},
                     { "messages" , BsonArray.Create(new BsonDocument[]{ new BsonDocument() { { "tenantId", 1 },{ "message", BsonString.Empty } } })},
                     { "docName" , BsonString.Empty}
                 }
                 ,
                 new BsonDocument
                 {
                     { "docId" , BsonString.Empty},
                     { "typeName" ,BsonString.Empty},
                     { "docMessage" ,BsonString.Empty},
                     { "messages" , BsonNull.Value},
                     { "docName" , BsonString.Empty}
                    
                 }
            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Template, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<Entity.Template>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new DocumentService(mock.Object);

            TemplateIdModel templateIdModel = new TemplateIdModel();
            templateIdModel.tenantId = 1;
            List<string> listIds = new List<string>();
            listIds.Add("5eb25acde519051af2eeb111");
            listIds.Add("5eb25acde519051af2eeb211");

            templateIdModel.id = listIds;
            //Act
            List<DocumentModel> dto = await service.GetDocumentsByTemplateIds(templateIdModel);

            //Assert
            Assert.NotNull(dto);
            Assert.Equal(2, dto.Count);
            Assert.Equal("5ebc18cba5d847268075ad4f", dto[0].docId);
            Assert.Equal("Credit report has been uploaded", dto[1].docMessage);
        }

        [Fact]
        public async Task TestGetEmailLogController()
        {
            //Arrange
            Mock<IDocumentService> mock = new Mock<IDocumentService>();
            List<EmailLogDTO> list = new List<EmailLogDTO>() { { new EmailLogDTO()
            {
                id="5f046210f50dc78d7b0c059c",
                userId=3842,
                userName = "abc",
                emailText = "abc",
                dateTime = Convert.ToDateTime("2020-06-25T07:39:57.233Z"),
                loanId = "5eb25d1fe519051af2eeb72d"
            } } };

            mock.Setup(x => x.GetEmailLog(It.IsAny<string>())).ReturnsAsync(list);

            var documentController = new DocumentController(mock.Object);
            //Act
            IActionResult result = await documentController.GetEmailLog("1");
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as List<EmailLogDTO>;
            Assert.Single(content);
            Assert.Equal("5f046210f50dc78d7b0c059c", content[0].id);
            Assert.Equal("abc", content[0].userName);
            Assert.Equal("abc", content[0].emailText);
            Assert.Equal(Convert.ToDateTime("2020-06-25T07:39:57.233Z"), content[0].dateTime);
            Assert.Equal("5eb25d1fe519051af2eeb72d", content[0].loanId);

        }

        [Fact]
        public async Task TestGetEmailLogService() 
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.EmailLog>> mockCollection = new Mock<IMongoCollection<Entity.EmailLog>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            {
                new BsonDocument
                    {
                        //Cover all empty  fields  
                        { "userId" ,  3842},
                        { "userName" ,BsonString.Empty},
                        { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                        { "_id" , BsonString.Empty},
                        { "requestId" , BsonString.Empty},
                        { "docId" , BsonString.Empty},
                        { "emailText" , BsonString.Empty},
                        { "loanId" , BsonString.Empty}
                    }
                ,new BsonDocument
                {
                    //Cover all empty  fields except userId
                    { "userId" , 3842 },
                    { "userName" ,BsonString.Empty},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" , BsonString.Empty},
                    { "requestId" , BsonString.Empty},
                    { "docId" , BsonString.Empty},
                    { "emailText" , BsonString.Empty},
                    { "loanId" , BsonString.Empty}
                }
                ,new BsonDocument
                {
                    //Cover all empty  fields  except userName
                    { "userId" , 3842},
                    { "userName" , "abc"},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" , BsonString.Empty},
                    { "requestId" , BsonString.Empty},
                    { "docId" , BsonString.Empty},
                    { "emailText" , BsonString.Empty},
                    { "loanId" , BsonString.Empty}
                }


                ,new BsonDocument
                {
                    //Cover all empty  fields except dateTime
                    { "userId" ,  3842},
                    { "userName" ,BsonString.Empty},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" , BsonString.Empty},
                    { "requestId" , BsonString.Empty},
                    { "docId" , BsonString.Empty},
                    { "emailText" , BsonString.Empty},
                    { "loanId" , BsonString.Empty}
                }

                ,new BsonDocument
                {
                    //Cover all empty  fields except id
                    { "userId" ,  3842},
                    { "userName" ,BsonString.Empty},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" ,  "5f046210f50dc78d7b0c059c"},
                    { "requestId" , BsonString.Empty},
                    { "docId" , BsonString.Empty},
                    { "emailText" , BsonString.Empty},
                    { "loanId" , BsonString.Empty}
                }
                ,new BsonDocument
                {
                    //Cover all empty  fields except requestId
                    { "userId" ,  3842},
                    { "userName" ,BsonString.Empty},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" , BsonString.Empty},
                    { "requestId" ,  "abc15d1fe456051af2eeb768"},
                    { "docId" , BsonString.Empty},
                    { "emailText" , BsonString.Empty},
                    { "loanId" , BsonString.Empty}
                }
                ,new BsonDocument
                {
                    //Cover all empty  fields except docId
                    { "userId" ,  3842},
                    { "userName" ,BsonString.Empty},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" , BsonString.Empty},
                    { "requestId" , BsonString.Empty},
                    { "docId" ,"aaa25d1fe456051af2eeb72d" },
                    { "emailText" , BsonString.Empty},
                    { "loanId" , BsonString.Empty}
                }
                ,new BsonDocument
                {
                    //Cover all empty  fields except activity
                    { "userId" ,  3842},
                    { "userName" ,BsonString.Empty},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" , BsonString.Empty},
                    { "requestId" , BsonString.Empty},
                    { "docId" , BsonString.Empty},
                    { "emailText" , "abc" },
                    { "loanId" , BsonString.Empty}
                }
                ,new BsonDocument
                {
                    //Cover all empty  fields except loanId
                    { "userId" ,  3842},
                    { "userName" ,BsonString.Empty},
                    { "dateTime" , Convert.ToDateTime("2020-06-25T07:39:57.233Z")},
                    { "_id" , BsonString.Empty},
                    { "requestId" , BsonString.Empty},
                    { "docId" ,BsonString.Empty },
                    { "emailText" , BsonString.Empty},
                    { "loanId" , "5eb25d1fe519051af2eeb72d" }
                }
            };


            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.EmailLog, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);


            mockdb.Setup(x => x.GetCollection<Entity.EmailLog>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new DocumentService(mock.Object);
            //Act
            List<EmailLogDTO> dto = await service.GetEmailLog("5eb25d1fe519051af2eeb72d");
            //Assert
            Assert.NotNull(dto);
            Assert.Equal(9, dto.Count);
            Assert.Equal(3842, dto[1].userId);
            Assert.Equal("abc", dto[2].userName);
            Assert.Equal("5f046210f50dc78d7b0c059c", dto[4].id);
            Assert.Equal("abc", dto[7].emailText);
            Assert.Equal("5eb25d1fe519051af2eeb72d", dto[8].loanId);

        }
        [Fact]
        public async Task TestmcuRenameControllerTrue()
        {
            //Arrange
            Mock<IDocumentService> mock = new Mock<IDocumentService>();
            mock.Setup(x => x.mcuRename(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            var controller = new DocumentController(mock.Object);
            //Act
            IActionResult result = await controller.mcuRename( "1","1","1","1","abc");
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestmcuRenameControllerFalse()
        {
            //Arrange
            Mock<IDocumentService> mock = new Mock<IDocumentService>();
            mock.Setup(x => x.mcuRename(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            var controller = new DocumentController(mock.Object);
            //Act
            IActionResult result = await controller.mcuRename("1", "1", "1", "1", "abc");
            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestmcuRenameServiceTrue()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();

             mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act
            IDocumentService service = new DocumentService(mock.Object);
            bool result = await service.mcuRename("5eb25d1fe519051af2eeb72d", "abc15d1fe456051af2eeb768", "aaa25d1fe456051af2eeb72d", "5ef454cd86c96583744140d9", "abc");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task TestmcuRenameServiceFalse()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();

            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(0, 0, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act

            IDocumentService service = new DocumentService(mock.Object);
            bool result = await service.mcuRename("5eb25d1fe519051af2eeb72d", "abc15d1fe456051af2eeb768", "aaa25d1fe456051af2eeb72d", "5ef454cd86c96583744140d9", "abc");

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task TestAcceptDocumentControllerTrue()
        {
            //Arrange
            Mock<IDocumentService> mock = new Mock<IDocumentService>();
            mock.Setup(x => x.AcceptDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            var controller = new DocumentController(mock.Object);
            //Act
            AcceptDocumentModel acceptDocumentModel = new AcceptDocumentModel();
            acceptDocumentModel.id = "5eb25d1fe519051af2eeb72d";
            acceptDocumentModel.requestId = "abc15d1fe456051af2eeb768";
            acceptDocumentModel.docId = "aaa25d1fe456051af2eeb72d";
            IActionResult result = await controller.AcceptDocument(acceptDocumentModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestAcceptDocumentControllerFalse()
        {
            //Arrange
            Mock<IDocumentService> mock = new Mock<IDocumentService>();
            mock.Setup(x => x.AcceptDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            var controller = new DocumentController(mock.Object);
            //Act
            AcceptDocumentModel acceptDocumentModel = new AcceptDocumentModel();
            acceptDocumentModel.id = "5eb25d1fe519051af2eeb72d";
            acceptDocumentModel.requestId = "abc15d1fe456051af2eeb768";
            acceptDocumentModel.docId = "aaa25d1fe456051af2eeb72d";
            IActionResult result = await controller.AcceptDocument(acceptDocumentModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestRejectDocumentControllerTrue()
        {
            //Arrange
            Mock<IDocumentService> mock = new Mock<IDocumentService>();
            mock.Setup(x => x.RejectDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            var controller = new DocumentController(mock.Object);
            //Act
            RejectDocumentModel rejectDocumentModel = new RejectDocumentModel();
            rejectDocumentModel.id = "5eb25d1fe519051af2eeb72d";
            rejectDocumentModel.requestId = "abc15d1fe456051af2eeb768";
            rejectDocumentModel.docId = "aaa25d1fe456051af2eeb72d";
            rejectDocumentModel.message = "document rejected";
            IActionResult result = await controller.RejectDocument(rejectDocumentModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestRejectDocumentControllerFalse()
        {
            //Arrange
            Mock<IDocumentService> mock = new Mock<IDocumentService>();
            mock.Setup(x => x.RejectDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            var controller = new DocumentController(mock.Object);
            //Act
            RejectDocumentModel rejectDocumentModel = new RejectDocumentModel();
            rejectDocumentModel.id = "5eb25d1fe519051af2eeb72d";
            rejectDocumentModel.requestId = "abc15d1fe456051af2eeb768";
            rejectDocumentModel.docId = "aaa25d1fe456051af2eeb72d";
            rejectDocumentModel.message = "document rejected";
            IActionResult result = await controller.RejectDocument(rejectDocumentModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestAcceptDocumentService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();

            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act
            IDocumentService service = new DocumentService(mock.Object);
            bool result = await service.AcceptDocument("5eb25d1fe519051af2eeb72d", "abc15d1fe456051af2eeb768", "aaa25d1fe456051af2eeb72d");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task TestRejectDocumentService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();

            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act
            IDocumentService service = new DocumentService(mock.Object);
            bool result = await service.RejectDocument("5eb25d1fe519051af2eeb72d", "abc15d1fe456051af2eeb768", "aaa25d1fe456051af2eeb72d", "document rejected");

            //Assert
            Assert.True(result);
        }
    }
}
