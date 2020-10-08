using DocumentManagement.Entity;
using DocumentManagement.Service;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DocumentManagement.Tests
{
    public class ActivityLogTest
    {
        [Fact]
        public async Task TestGetActivityLogIdService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            Mock<IMongoCollection<ActivityLog>> mockCollection = new Mock<IMongoCollection<ActivityLog>>();

            List<BsonDocument> listActivityLog = new List<BsonDocument>()
            {
                new BsonDocument
                {
                    { "_id" , "5f0d668fcc9ce539845d7f9c"}
                }
            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(listActivityLog);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.ActivityLog, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.Setup(x => x.GetCollection<ActivityLog>("ActivityLog", It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mock.Setup(x => x.db).Returns(mockdb.Object);

            //Act

            IActivityLogService service = new ActivityLogService(mock.Object);

            //Assert

            string result = await service.GetActivityLogId("5f0d668fcc9ce539845d7f99", "5f0d668fcc9ce539845d7f9a", "5f0d668fcc9ce539845d7f9b");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestInsertLogService()
        {
            //Arrange

            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IMongoCollection<ActivityLog>> mockCollection = new Mock<IMongoCollection<ActivityLog>>();

            mockdb.Setup(x => x.GetCollection<Entity.ActivityLog>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);

            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<ActivityLog>>(), It.IsAny<UpdateDefinition<ActivityLog>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>()));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            IActivityLogService service = new ActivityLogService(mock.Object);

            //Act

            await service.InsertLog("5f0d668fcc9ce539845d7f9c", "Requested By Danish Faiz");

            //Assert

            mockCollection.VerifyAll();
        }

    }
}
