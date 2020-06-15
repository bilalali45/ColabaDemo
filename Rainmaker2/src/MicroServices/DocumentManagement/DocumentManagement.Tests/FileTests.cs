using DocumentManagement.API.Controllers;
using DocumentManagement.Entity;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DocumentManagement.Tests
{
    public class FileTests
    {
        [Fact]
        public async Task TestDoneControllerTrue()
        {
            Mock<IFileService> mock = new Mock<IFileService>();
            DoneModel obj = new DoneModel();
            obj.id = "1";
            obj.docId = "1";
            obj.requestId = "1";
            mock.Setup(x => x.Done(It.IsAny<DoneModel>())).ReturnsAsync(true);
            FileController controller = new FileController(mock.Object, null, null, null,null,null);
            //Act
            IActionResult result = await controller.Done(obj);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);

        }
        [Fact]
        public async Task TestDoneControllerFalse()
        {
            Mock<IFileService> mock = new Mock<IFileService>();
            DoneModel obj = new DoneModel();
            obj.id = "1";
            obj.docId = "1";
            obj.requestId = "1";
            mock.Setup(x => x.Done(It.IsAny<DoneModel>())).ReturnsAsync(false);
            FileController controller = new FileController(mock.Object, null, null, null, null, null);
            //Act
            IActionResult result = await controller.Done(obj);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task TestDoneFileServiceTrue()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            var doneModel = new DoneModel() { id = "5eb25d1fe519051af2eeb72d", docId = "aaa25d1fe456051af2eeb72d", requestId = "abc15d1fe456051af2eeb768" };
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act
            IFileService fileService = new FileService(mock.Object);
            bool result = await fileService.Done(doneModel);
            //Assert
            Assert.True(result);
        }




        [Fact]
        public async Task TestDoneFileServiceFalse()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            var doneModel = new DoneModel() { id = "5eb25d1fe519051af2eeb72d", docId = "aaa25d1fe456051af2eeb72d", requestId = "abc15d1fe456051af2eeb768" };
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(0, 0, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act
            IFileService fileService = new FileService(mock.Object);
            bool result = await fileService.Done(doneModel);
            //Assert
            Assert.False(result);
        }
        [Fact]
        public async Task TestRenameControllerTrue()
        {
            //Arrange
            Mock<IFileService> mock = new Mock<IFileService>();
            FileRenameModel model = new FileRenameModel() { docId = "1", requestId = "1", fileId = "1", fileName = "clientName.txt" };

            mock.Setup(x => x.Rename(It.IsAny<FileRenameModel>())).ReturnsAsync(true);

            FileController controller = new FileController(mock.Object, null, null, null, null, null);
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

            FileController controller = new FileController(mock.Object, null, null, null, null, null);
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

            var fileRenameModel = new FileRenameModel() { id = "5eb25d1fe519051af2eeb72d", docId = "5eb25d1fe519051af2eeb72d", requestId = "5eb25d1fe519051af2eeb72d", fileId = "5eb25d1fe519051af2eeb72d", fileName = "clientName.txt" };
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));
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



        [Fact]
        public async Task TestOrderController()
        {
            Mock<IFileService> mock = new Mock<IFileService>();
            FileOrderModel fileOrder = new FileOrderModel();

            mock.Setup(x => x.Order(It.IsAny<FileOrderModel>()));

            FileController controller = new FileController(mock.Object, null, null, null, null, null);
            //Act
            IActionResult result = await controller.Order(fileOrder);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);

        }

        [Fact]
        public async Task TestOrderService()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Request>> mockCollection = new Mock<IMongoCollection<Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            FileOrderModel fileOrderModel = new FileOrderModel { docId = "5eb25d1fe519051af2eeb72d", id = "5eb25d1fe519051af2eeb72d", requestId = "5eb25d1fe519051af2eeb72d" };
            FileNameModel fileNameModel = new FileNameModel() { fileName = "abc", order = 1 };
            fileOrderModel.files = new List<FileNameModel>();
            fileOrderModel.files.Add(fileNameModel);
            mockdb.Setup(x => x.GetCollection<Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Request>>(), It.IsAny<UpdateDefinition<Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).Verifiable();
            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            IFileService fileService = new FileService(mock.Object);
            //Act
            await fileService.Order(fileOrderModel);
            //Assert
            mockCollection.VerifyAll();
        }



        [Fact]
        public async Task TestViewController()
        {
            Mock<ISettingService> mocksettingservice = new Mock<ISettingService>();
            Mock<IFileService> mockfileservice = new Mock<IFileService>();
            Mock<IFtpClient> mockftpclient = new Mock<IFtpClient>();
            
            Mock<IFileEncryptionFactory> mockfileencryptionfactory= new Mock<IFileEncryptionFactory>();
            Mock<IMongoService> mockmongoservice = new Mock<IMongoService>();
            Mock<IActionResult> mockactionresult= new Mock<IActionResult>();
            Mock<IMongoCollection<Setting>> collection = new Mock<IMongoCollection<Setting>>();
             FileViewDTO fileViewDTO = new FileViewDTO();
            FileViewModel fileViewModel = new FileViewModel();
          
            fileViewModel.docId = "ddd25d1fe456057652eeb72d";
            fileViewModel.id = "5eb25d1fe519051af2eeb72d";
            fileViewModel.requestId = "abc15d1fe456051af2eeb768";
            fileViewModel.fileId = "5ee33b1e01ba190fe04a21eb";
            Setting setting = new Setting();
            setting.id = "";
            setting.ftpServer = "";
            setting.ftpUser = "";
            setting.ftpPassword = "";

            mockfileservice.Setup(x => x.View(fileViewModel));//.ReturnsAsync(fileViewDTO);
          //  mocksettingservice.Setup(x => x.GetSetting());
            mockmongoservice.Setup(x => x.db.GetCollection<Setting>("Setting",null)).Returns(collection.Object);
            collection.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<Setting>>(),null,default));//.Returns(collection.Object);
            
            mockftpclient.Setup(x => x.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword));
            mockftpclient.Setup(x => x.DownloadAsync(fileViewDTO.serverName,Path.GetTempFileName()));

            FileController controller = new FileController(mockfileservice.Object, mockfileencryptionfactory.Object, mockftpclient.Object, mocksettingservice.Object, null, null);
            //Act

            IActionResult result = await controller.View(fileViewModel.id, fileViewModel.requestId, fileViewModel.docId, fileViewModel.fileId);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);

        }
    }
}
