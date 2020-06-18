using AutoFixture;
using DocumentManagement.API.Controllers;
using DocumentManagement.Entity;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
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
            mock.Setup(x => x.Done(It.IsAny<DoneModel>(), It.IsAny<int>())).ReturnsAsync(true);
            var controller = new FileController(mock.Object, null, null, null,null,null);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

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
            mock.Setup(x => x.Done(It.IsAny<DoneModel>(), It.IsAny<int>())).ReturnsAsync(false);

            var fileController = new FileController(mock.Object, null, null, null, null, null);

            //var dashboardController = new DashboardController(mock.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            fileController.ControllerContext = context;

            //Act
            IActionResult result = await fileController.Done(obj);
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
            bool result = await fileService.Done(doneModel,1);
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
            bool result = await fileService.Done(doneModel,1);
            //Assert
            Assert.False(result);
        }
        [Fact]
        public async Task TestRenameControllerTrue()
        {
            //Arrange
            Mock<IFileService> mock = new Mock<IFileService>();
            FileRenameModel model = new FileRenameModel() { docId = "1", requestId = "1", fileId = "1", fileName = "clientName.txt" };

            mock.Setup(x => x.Rename(It.IsAny<FileRenameModel>(), It.IsAny<int>())).ReturnsAsync(true);

            var controller = new FileController(mock.Object, null, null, null, null, null);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

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

            mock.Setup(x => x.Rename(It.IsAny<FileRenameModel>(), It.IsAny<int>())).ReturnsAsync(false);

            var controller = new FileController(mock.Object, null, null, null, null, null);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

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
            bool result = await fileService.Rename(fileRenameModel,1);

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
            bool result = await fileService.Rename(fileRenameModel,1);

            //Assert
            Assert.False(result);
        }



        [Fact]
        public async Task TestOrderController()
        {
            Mock<IFileService> mock = new Mock<IFileService>();
            FileOrderModel fileOrder = new FileOrderModel();

            mock.Setup(x => x.Order(It.IsAny<FileOrderModel>(), It.IsAny<int>()));

            var controller = new FileController(mock.Object, null, null, null, null, null);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

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
            await fileService.Order(fileOrderModel,1);
            //Assert
            mockCollection.VerifyAll();
        }



        [Fact]
        public async Task TestViewController()
        {
            Mock<ISettingService> mocksettingservice = new Mock<ISettingService>();
            Mock<IFileService> mockfileservice = new Mock<IFileService>();
            Mock<IFtpClient> mockftpclient = new Mock<IFtpClient>();
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            var  mockfileencryptor = new Mock<IFileEncryptor>();
            Mock<IFileEncryptionFactory> mockfileencryptorfacotry = new Mock<IFileEncryptionFactory>(MockBehavior.Strict);
            mockconfiguration.Setup(x => x["KeyStore:Url"]).Returns("http://test.com");
            mockconfiguration.Setup(x => x["File:FtpKey"]).Returns("FtpKey");
            FileViewDTO fileViewDTO = new FileViewDTO();
            fileViewDTO.serverName = "a69ad17f-7505-492d-a92e-f32967cecff8.enc";
            fileViewDTO.encryptionKey = "FileKey";
            fileViewDTO.encryptionAlgorithm = "AES";
            fileViewDTO.clientName = "NET Unit Testing.docx";
            fileViewDTO.contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
           
            FileViewModel fileViewModel = new FileViewModel();
            fileViewModel.docId = "ddd25d1fe456057652eeb72d";
            fileViewModel.id = "5eb25d1fe519051af2eeb72d";
            fileViewModel.requestId = "abc15d1fe456051af2eeb768";
            fileViewModel.fileId = "5ee9c912264e4c28acf5526e";
            
            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";

            mockfileservice.Setup(x => x.View(It.IsAny<FileViewModel>())).ReturnsAsync(fileViewDTO);
            mocksettingservice.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockftpclient.Setup(x => x.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword));
            mockftpclient.Setup(x => x.DownloadAsync(fileViewDTO.serverName, Path.GetTempFileName())).Verifiable();

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var handlerMock1 = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost:5041/api/keystore/keystore?key=FtpKey"),
            };
            // ACT
            
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                   "SendAsync",
                   ItExpr.IsAny<HttpRequestMessage>(),
                   ItExpr.IsAny<CancellationToken>()
                )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,

                    Content = new StringContent("this is the long and strong key."),
                }).Verifiable();
            var httpClient1 = new HttpClient(handlerMock1.Object)
            {
                BaseAddress = new Uri("http://localhost:5041/api/keystore/keystore?key=FileKey"),
            };
            handlerMock1
                  .Protected()
                  // Setup the PROTECTED method to mock
                  .Setup<Task<HttpResponseMessage>>(
                     "SendAsync",
                     ItExpr.IsAny<HttpRequestMessage>(),
                     ItExpr.IsAny<CancellationToken>()
                  )
                  // prepare the expected response of the mocked http call
                  .ReturnsAsync(new HttpResponseMessage()
                  {
                      StatusCode = HttpStatusCode.OK,

                      Content = new StringContent("this is a very long password"),
                  }).Verifiable(); ;

            httpClientFactory.SetupSequence(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient).Returns(httpClient1);
            mockfileencryptor.Setup(x => x.DecrypeFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns( new MemoryStream());

            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            
            // also check the 'http' call was like we expected it
            // Act  
            FileController controller = new FileController(mockfileservice.Object, mockfileencryptorfacotry.Object, mockftpclient.Object, mocksettingservice.Object, httpClientFactory.Object, mockconfiguration.Object);
            IActionResult result = await controller.View(fileViewModel.id, fileViewModel.requestId, fileViewModel.docId, fileViewModel.fileId);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<FileStreamResult>(result);

        }



    }
}

