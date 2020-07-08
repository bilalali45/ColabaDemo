using Microsoft.AspNetCore.Mvc;
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
using ActivityLog = DocumentManagement.Entity.ActivityLog;

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

            mock.Setup(x => x.GetDocumemntsByTemplateIds(It.IsAny<TemplateIdModel>())).ReturnsAsync(list);

            var documentController = new DocumentController(mock.Object);

            //Act
            IActionResult result = await documentController.GetDocumemntsByTemplateIds(It.IsAny<TemplateIdModel>());

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
                requestId = "abc15d1fe456051af2eeb768",
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
            Assert.Equal("abc15d1fe456051af2eeb768", content[0].requestId);
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
        public async Task TestGetActivityLogService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<ActivityLog>> mockCollection = new Mock<IMongoCollection<ActivityLog>>();
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
                    { "requestId" , BsonString.Empty},
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
                    { "requestId" , BsonString.Empty},
                    { "docId" , BsonString.Empty},
                    { "activity" , BsonString.Empty},
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
                    { "requestId" , BsonString.Empty},
                    { "docId" , BsonString.Empty},
                    { "activity" , BsonString.Empty},
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
                    { "requestId" , BsonString.Empty},
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
                    { "requestId" , BsonString.Empty},
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
                    { "requestId" , BsonString.Empty},
                    { "docId" ,BsonString.Empty },
                    { "activity" , BsonString.Empty},
                    { "loanId" , "5eb25d1fe519051af2eeb72d" }
                }
            };


            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<ActivityLog, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);


            mockdb.Setup(x => x.GetCollection<ActivityLog>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new DocumentService(mock.Object);
            //Act
            List<ActivityLogDTO> dto = await service.GetActivityLog("5eb25d1fe519051af2eeb72d", "abc15d1fe456051af2eeb768", "aaa25d1fe456051af2eeb72d");
            //Assert
            Assert.NotNull(dto);
            Assert.Equal(9, dto.Count);
            Assert.Equal(3842, dto[1].userId);
            Assert.Equal("abc", dto[2].userName);
            Assert.Equal("5f046210f50dc78d7b0c059c", dto[4].id);
            Assert.Equal("abc15d1fe456051af2eeb768", dto[5].requestId);
            Assert.Equal("aaa25d1fe456051af2eeb72d", dto[6].docId);
            Assert.Equal("abc", dto[7].activity);
            Assert.Equal("5eb25d1fe519051af2eeb72d", dto[8].loanId);

        }
    }
}
