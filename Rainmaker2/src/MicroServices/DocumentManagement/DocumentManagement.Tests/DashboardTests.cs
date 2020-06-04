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
using System.Threading.Tasks;
using Xunit;

namespace DocumentManagement.Tests
{
    public class DashboardTests
    {
        [Fact]
        public async Task TestGetAllPendingDocumentsController()
        {
            //Arrange
            Mock<IDashboardService> mock = new Mock<IDashboardService>();
            List<DashboardDTO> list = new List<DashboardDTO>() { { new DashboardDTO() { docId="1" } } };
            
            mock.Setup(x => x.GetPendingDocuments(It.IsAny<int>(), It.IsAny<int>(),It.IsAny<IMongoAggregateService<Request>>(),It.IsAny<IMongoAggregateService<BsonDocument>>())).ReturnsAsync(list);
            
            DashboardController controller = new DashboardController(mock.Object,null,null);
            //Act
            IActionResult result = await controller.GetPendingDocuments(1, 1);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as List<DashboardDTO>;
            Assert.Single(content);
            Assert.Equal("1",content[0].docId);
        }

        [Fact]
        public async Task TestGetAllPendingDocumentsService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAggregateFluent<Request>> mockAggregate = new Mock<IAggregateFluent<Request>>();
            Mock<IAggregateFluent<BsonDocument>> mockBson = new Mock<IAggregateFluent<BsonDocument>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IMongoAggregateService<Request>> requestMock = new Mock<IMongoAggregateService<Request>>();
            Mock<IMongoAggregateService<BsonDocument>> bsonMock = new Mock<IMongoAggregateService<BsonDocument>>();
            List<BsonDocument> list = new List<BsonDocument>()
            { new BsonDocument
                    {
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
            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);
            
            mockBson.Setup(x => x.Match(It.IsAny<FilterDefinition<BsonDocument>>())).Returns(mockBson.Object);
            //mockBson.Setup(x => x.Unwind(It.IsAny<FieldDefinition<BsonDocument>>())).Returns(mockBson.Object);
            //mockBson.Setup(x => x.Lookup<BsonDocument>(It.IsAny<string>(), It.IsAny<FieldDefinition<BsonDocument>>(), It.IsAny<FieldDefinition<BsonDocument>>(), It.IsAny<FieldDefinition<BsonDocument>>())).Returns(mockBson.Object);
            //mockBson.Setup(x => x.Project(It.IsAny<ProjectionDefinition<BsonDocument,BsonDocument>>())).Returns(mockBson.Object);
            mockBson.Setup(x => x.ToCursorAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(mockCursor.Object);

            mockAggregate.Setup(x => x.Match(It.IsAny<FilterDefinition<Request>>())).Returns(mockAggregate.Object);
            mockAggregate.Setup(x => x.Unwind(It.IsAny<FieldDefinition<Request>>())).Returns(mockBson.Object);
            
            mockCollection.Setup(x => x.Aggregate(It.IsAny<AggregateOptions>())).Returns(mockAggregate.Object);
            
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            
            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            
            var service = new DashboardService(mock.Object);
            //Act
            List<DashboardDTO> dto = await service.GetPendingDocuments(1, 1,requestMock.Object,bsonMock.Object);
            //Assert
            Assert.NotNull(dto);
            Assert.Single(dto);
        }
    }
}
