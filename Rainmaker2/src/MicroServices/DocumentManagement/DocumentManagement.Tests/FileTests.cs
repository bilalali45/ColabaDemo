using System;
using System.Collections.Generic;
using DocumentManagement.API.Controllers;
using DocumentManagement.Entity;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using Moq.Protected;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System.IO;

namespace DocumentManagement.Tests
{
    public class FileTests
    {
        [Fact]
        public async Task TestRenameControllerTrue()
        {
            //Arrange
            Mock<IFileService> mock = new Mock<IFileService>();
            FileRenameModel model = new FileRenameModel() { docId = "1", requestId = "1", fileId = "1", fileName = "clientName.txt" };

            mock.Setup(x => x.Rename(It.IsAny<FileRenameModel>())).ReturnsAsync(true);

            FileController controller = new FileController(mock.Object);
            //Act
            IActionResult result = await controller.Rename(new FileRenameModel() { docId = "1", requestId = "1", fileId = "1", fileName = "clientName.txt" });
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestRenameControllerFalse()
        {
            //Arrange
            Mock<IFileService> mock = new Mock<IFileService>();
            FileRenameModel model = new FileRenameModel() { docId = "1", requestId = "1", fileId = "1", fileName = "clientName.txt" };

            mock.Setup(x => x.Rename(It.IsAny<FileRenameModel>())).ReturnsAsync(false);

            FileController controller = new FileController(mock.Object);
            //Act
            IActionResult result = await controller.Rename(new FileRenameModel() { docId = "1", requestId = "1", fileId = "1", fileName = "clientName.txt" });
            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestRenameServiceTrue()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();

            var fileRenameModel = new FileRenameModel() { id= "5eb25d1fe519051af2eeb72d", docId = "5eb25d1fe519051af2eeb72d", requestId = "5eb25d1fe519051af2eeb72d", fileId = "5eb25d1fe519051af2eeb72d", fileName = "clientName.txt" };
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(),It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1,1, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act

            IFileService fileService = new FileService(mock.Object);
            bool result = await fileService.Rename(fileRenameModel);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task TestRenameServiceFalse()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();

            var fileRenameModel = new FileRenameModel() { id = "5eb25d1fe519051af2eeb72d", docId = "5eb25d1fe519051af2eeb72d", requestId = "5eb25d1fe519051af2eeb72d", fileId = "5eb25d1fe519051af2eeb72d", fileName = "clientName.txt" };
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(0, 0, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act

            IFileService fileService = new FileService(mock.Object);
            bool result = await fileService.Rename(fileRenameModel);

            //Assert
            Assert.False(result);
        }
    }
}
