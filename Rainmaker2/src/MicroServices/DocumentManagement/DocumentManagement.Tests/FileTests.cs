using DocumentManagement.API.Controllers;
using DocumentManagement.Entity;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGeneration;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Xunit;
using Microsoft.Extensions.Primitives;

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
            mock.Setup(x => x.Done(It.IsAny<DoneModel>(), It.IsAny<int>(), It.IsAny<int>(),It.IsAny<IEnumerable<string>>())).ReturnsAsync(true);
            var controller = new FileController(mock.Object, null, null, null, null, null, Mock.Of<ILogger<FileController>>(), null,null,null, null);
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
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
            mock.Setup(x => x.Done(It.IsAny<DoneModel>(), It.IsAny<int>(), It.IsAny<int>(),It.IsAny<List<string>>())).ReturnsAsync(false);

            var fileController = new FileController(mock.Object, null, null, null, null, null, Mock.Of<ILogger<FileController>>(), null, null,null, null);
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
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
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Request>> mockCollection = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            var doneModel = new DoneModel() { id = "5eb25d1fe519051af2eeb72d", docId = "aaa25d1fe456051af2eeb72d", requestId = "abc15d1fe456051af2eeb768" };
            mockdb.Setup(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Entity.Request>>(), It.IsAny<UpdateDefinition<Entity.Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act
            IFileService fileService = new FileService(mock.Object, mockActivityLogService.Object, Mock.Of<IRainmakerService>());
            bool result = await fileService.Done(doneModel, 1, 1, new List<string>());
            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task TestDoneFileServiceFalse()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Request>> mockCollection = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            var doneModel = new DoneModel() { id = "5eb25d1fe519051af2eeb72d", docId = "aaa25d1fe456051af2eeb72d", requestId = "abc15d1fe456051af2eeb768" };
            mockdb.Setup(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Entity.Request>>(), It.IsAny<UpdateDefinition<Entity.Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(0, 0, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act
            IFileService fileService = new FileService(mock.Object, mockActivityLogService.Object, Mock.Of<IRainmakerService>());
            bool result = await fileService.Done(doneModel, 1, 1,new List<string>());
            //Assert
            Assert.False(result);
        }
        [Fact]
        public async Task TestRenameControllerTrue()
        {
            //Arrange
            Mock<ISettingService> mocksettingservice = new Mock<ISettingService>();
            Mock<IFileService> mock = new Mock<IFileService>();
            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";
            setting.maxFileSize = 1000000;
            setting.maxFileNameSize = 255;

            FileRenameModel model = new FileRenameModel() { docId = "1", requestId = "1", fileId = "1", fileName = "clientName.txt" };

            mock.Setup(x => x.Rename(It.IsAny<FileRenameModel>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);
            mocksettingservice.Setup(x => x.GetSetting()).ReturnsAsync(setting);

            var controller = new FileController(mock.Object, null, null, mocksettingservice.Object, null, null, Mock.Of<ILogger<FileController>>(), null, null, null, null);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

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
            Mock<ISettingService> mocksettingservice = new Mock<ISettingService>();
            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";
            setting.maxFileSize = 1000000;
            setting.maxFileNameSize = 255;
            FileRenameModel model = new FileRenameModel() { docId = "1", requestId = "1", fileId = "1", fileName = "clientName.txt" };

            mock.Setup(x => x.Rename(It.IsAny<FileRenameModel>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(false);
            mocksettingservice.Setup(x => x.GetSetting()).ReturnsAsync(setting);

            var controller = new FileController(mock.Object, null, null, mocksettingservice.Object, null, null, Mock.Of<ILogger<FileController>>(), null, null, null, null);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;

            //Act
            IActionResult result = await controller.Rename(new FileRenameModel() { docId = "1", requestId = "1", fileId = "1", fileName = "clientName.txt" });
            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestRenameFileNameSizeExceptionController()
        {
            //Arrange
            Mock<ISettingService> mocksettingservice = new Mock<ISettingService>();
            Mock<IFileService> mock = new Mock<IFileService>();

            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";
            setting.maxFileSize = 1000000;
            setting.maxFileNameSize = 255;
            FileRenameModel model = new FileRenameModel() { docId = "1", requestId = "1", fileId = "1", fileName = new string('a', 256) };

            mock.Setup(x => x.Rename(It.IsAny<FileRenameModel>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(false);
            mocksettingservice.Setup(x => x.GetSetting()).ReturnsAsync(setting);

            var controller = new FileController(mock.Object, null, null, mocksettingservice.Object, null, null, Mock.Of<ILogger<FileController>>(), null, null, null, null);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            var result = controller.Rename(model).Result;
            Assert.IsType<BadRequestObjectResult>(result);
            //    await Assert.ThrowsAsync<DocumentManagementException>(async () => { await controller.Rename(model); });

        }
        [Fact]
        public async Task TestRenameServiceTrue()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Request>> mockCollection = new Mock<IMongoCollection<Entity.Request>>();

            var fileRenameModel = new FileRenameModel() { id = "5eb25d1fe519051af2eeb72d", docId = "5eb25d1fe519051af2eeb72d", requestId = "5eb25d1fe519051af2eeb72d", fileId = "5eb25d1fe519051af2eeb72d", fileName = "clientName.txt" };
            mockdb.Setup(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Entity.Request>>(), It.IsAny<UpdateDefinition<Entity.Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(1, 1, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act

            IFileService fileService = new FileService(mock.Object, mockActivityLogService.Object,null);
            bool result = await fileService.Rename(fileRenameModel, 1, 1);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task TestRenameServiceFalse()
        {
            //Arrange
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Request>> mockCollection = new Mock<IMongoCollection<Entity.Request>>();

            var fileRenameModel = new FileRenameModel() { id = "5eb25d1fe519051af2eeb72d", docId = "5eb25d1fe519051af2eeb72d", requestId = "5eb25d1fe519051af2eeb72d", fileId = "5eb25d1fe519051af2eeb72d", fileName = "clientName.txt" };
            mockdb.Setup(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Entity.Request>>(), It.IsAny<UpdateDefinition<Entity.Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(0, 0, BsonInt32.Create(1)));
            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            //Act

            IFileService fileService = new FileService(mock.Object, mockActivityLogService.Object,null);
            bool result = await fileService.Rename(fileRenameModel, 1, 1);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task TestOrderController()
        {
            Mock<IFileService> mock = new Mock<IFileService>();
            FileOrderModel fileOrder = new FileOrderModel();

            mock.Setup(x => x.Order(It.IsAny<FileOrderModel>(), It.IsAny<int>(), It.IsAny<int>()));

            var controller = new FileController(mock.Object, null, null, null, null, null, Mock.Of<ILogger<FileController>>(), null, null, null, null);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

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
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Request>> mockCollection = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            FileOrderModel fileOrderModel = new FileOrderModel { docId = "5eb25d1fe519051af2eeb72d", id = "5eb25d1fe519051af2eeb72d", requestId = "5eb25d1fe519051af2eeb72d" };
            FileNameModel fileNameModel = new FileNameModel() { fileName = "abc", order = 1 };
            fileOrderModel.files = new List<FileNameModel>();
            fileOrderModel.files.Add(fileNameModel);
            mockdb.Setup(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Entity.Request>>(), It.IsAny<UpdateDefinition<Entity.Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).Verifiable();
            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            IFileService fileService = new FileService(mock.Object, mockActivityLogService.Object,null);
            //Act
            await fileService.Order(fileOrderModel, 1, 1);
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
            Mock<IKeyStoreService> mockKeyStoreService = new Mock<IKeyStoreService>();

            var mockfileencryptor = new Mock<IFileEncryptor>();
            Mock<IFileEncryptionFactory> mockfileencryptorfacotry = new Mock<IFileEncryptionFactory>(MockBehavior.Strict);
            mockconfiguration.Setup(x => x["KeyStore:Url"]).Returns("http://test.com");
            mockconfiguration.Setup(x => x["File:FtpKey"]).Returns("FtpKey");
            FileViewDto fileViewDTO = new FileViewDto();
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

            mockfileservice.Setup(x => x.View(It.IsAny<FileViewModel>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(fileViewDTO);
            mocksettingservice.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockftpclient.Setup(x => x.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword));
            mockftpclient.Setup(x => x.DownloadAsync(fileViewDTO.serverName, Path.GetTempFileName())).Verifiable();

            mockKeyStoreService.Setup(x => x.GetFileKey()).ReturnsAsync("this is a very long password");
            mockKeyStoreService.Setup(x => x.GetFtpKey()).ReturnsAsync("this is the long and strong key.");
            mockfileencryptor.Setup(x => x.DecrypeFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new MemoryStream());

            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Connection.RemoteIpAddress).Returns(IPAddress.Parse("127.0.0.1"));
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            // also check the 'http' call was like we expected it
            // Act  
            FileController controller = new FileController(mockfileservice.Object, mockfileencryptorfacotry.Object, mockftpclient.Object, mocksettingservice.Object, mockKeyStoreService.Object, mockconfiguration.Object, Mock.Of<ILogger<FileController>>(), null, null, null, null);
            controller.ControllerContext = context;
            IActionResult result = await controller.View(fileViewModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<FileStreamResult>(result);

        }

        [Fact]
        public async Task TestViewService()
        {
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Request>> mockCollection = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IMongoCollection<ViewLog>> mockViewLogCollection = new Mock<IMongoCollection<ViewLog>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();
            FileViewModel fileViewModel = new FileViewModel();
            fileViewModel.docId = "ddd25d1fe456057652eeb72d";
            fileViewModel.id = "5eb25d1fe519051af2eeb72d";
            fileViewModel.requestId = "abc15d1fe456051af2eeb768";
            fileViewModel.fileId = "5ef049d896f9f41cec4b358f";

            List<BsonDocument> list = new List<BsonDocument>()
            {
                new BsonDocument
                    {
                        { "_id" , "5ef050534f7d102f9c68a95e" },
                        { "serverName" ,  "fa8a95e8-2a94-41f0-9f91-c7cf0e0525b0.enc" },
                        { "encryptionKey" , "FileKey" },
                        { "encryptionAlgorithm" , "AES" },
                        { "clientName" , "Recruitment & Selection Survey - Rainsoft Financials Pvt Ltd. (2).docx" },
                        { "contentType" , "application/vnd.openxmlformats-officedocument.wordprocessingml.document"}
                    }
            };

            var viewLog = new ViewLog
            {
                id = "5ef050534f7d102f9c68a95e",
                userProfileId = 1,
                createdOn = DateTime.Now,
                ipAddress = "127.0.0.1",
                loanApplicationId = "5eb25d1fe519051af2eeb72d",
                requestId = "abc15d1fe456051af2eeb768",
                documentId = "ddd25d1fe456057652eeb72d",
                fileId = "5ef049d896f9f41cec4b358f"
            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockCollection.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);
            mockdb.Setup(x => x.GetCollection<Entity.Request>("Request", It.IsAny<MongoCollectionSettings>())).Returns(mockCollection.Object);
            mockdb.Setup(x => x.GetCollection<ViewLog>("ViewLog", It.IsAny<MongoCollectionSettings>())).Returns(mockViewLogCollection.Object);
            mockViewLogCollection.Setup(s => s.InsertOneAsync(It.IsAny<ViewLog>(), It.IsAny<InsertOneOptions>(), It.IsAny<System.Threading.CancellationToken>()));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);

            var service = new FileService(mock.Object, mockActivityLogService.Object,null);
            //Act
            var dto = await service.View(fileViewModel, 1, "127.0.0.1", 1);
            //Assert
            Assert.NotNull(dto);
            Assert.Equal("5ef050534f7d102f9c68a95e", dto.id);
        }

        [Fact]
        public async Task TestSubmitController()
        {
            Mock<ISettingService> mocksettingservice = new Mock<ISettingService>();
            Mock<IFileService> mockfileservice = new Mock<IFileService>();
            Mock<IFtpClient> mockftpclient = new Mock<IFtpClient>();
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            Mock<IFileEncryptor> mockfileencryptor = new Mock<IFileEncryptor>();
            Mock<IFileEncryptionFactory> mockfileencryptorfacotry = new Mock<IFileEncryptionFactory>(MockBehavior.Strict);
            Mock<IFormFile> mockformFile = new Mock<IFormFile>();
            Mock<IKeyStoreService> mockKeyStoreService = new Mock<IKeyStoreService>();
            Mock<INotificationService> mockNotificationService = new Mock<INotificationService>();
            Mock<IByteProService> mockByteProService = new Mock<IByteProService>();

            string filePath = Path.GetTempFileName();
            MemoryStream ms = new MemoryStream();
            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";
            setting.maxFileSize = 15000000;
            setting.maxFileNameSize = 255;
            setting.allowedExtensions = new string[] { ".tmp" };
            mockconfiguration.Setup(x => x["KeyStore:Url"]).Returns("http://test.com");
            mockconfiguration.Setup(x => x["File:Key"]).Returns("Key");
            mockconfiguration.Setup(x => x["File:Algo"]).Returns("Algo");

            mockKeyStoreService.Setup(x => x.GetFileKey()).ReturnsAsync("this is a very long password");
            mockKeyStoreService.Setup(x => x.GetFtpKey()).ReturnsAsync("this is the long and strong key.");
            mocksettingservice.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockftpclient.Setup(x => x.Setup(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns(filePath);
            mockftpclient.Setup(x => x.UploadAsync(Path.GetFileName(filePath), filePath)).Verifiable();
            mockfileservice.Setup(x => x.Submit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(),It.IsAny<IEnumerable<string>>())).ReturnsAsync(string.Empty);
 
            List<FileViewDto> fileViewDTOs = new List<FileViewDto>();
            FileViewDto fileViewDTO = new FileViewDto();
            fileViewDTO.loanApplicationId = 1;
            fileViewDTO.id = "5f0ede3cce9c4b62509d0dbf";
            fileViewDTOs.Add(fileViewDTO);
            mockfileservice.Setup(x => x.GetFileByDocId(It.IsAny<FileViewModel>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(fileViewDTOs);
            mockfileservice.Setup(x => x.Submit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync("5f0ede3cce9c4b62509d0dbf");
            Tenant tenant = new Tenant();
            tenant.syncToBytePro = (int)SyncToBytePro.Auto;
            tenant.autoSyncToBytePro = (int)AutoSyncToBytePro.OnSubmit;
            mockByteProService.Setup(x => x.GetTenantSetting(It.IsAny<int>())).ReturnsAsync(tenant);

            FileOrderModel model = new FileOrderModel
            {
                id = "1",
                docId = "1",
                requestId = "1",
                files = new List<FileNameModel>()
            };

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Connection.RemoteIpAddress).Returns(IPAddress.Parse("127.0.0.1"));
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );

            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            FileController controller = new FileController(mockfileservice.Object, mockfileencryptorfacotry.Object, mockftpclient.Object, mocksettingservice.Object, mockKeyStoreService.Object, mockconfiguration.Object, Mock.Of<ILogger<FileController>>(), Mock.Of<ILosIntegrationService>(), mockNotificationService.Object, Mock.Of<IRainmakerService>(), mockByteProService.Object);
            controller.ControllerContext = context;
            string id = "5eb25d1fe519051af2eeb72d";
            string requestId = "abc15d1fe456051af2eeb768";
            string docId = "ddd25d1fe456057652eeb72d";
            string order = "0";

            string path = Path.GetTempFileName();

            //Create the file.
            using (FileStream fs = File.Create(path))
            {
                AddText(fs, "This is some text");
                AddText(fs, "This is some more text,");
                AddText(fs, "\r\nand this is on a new line");
                AddText(fs, "\r\n\r\nThe following is a subset of characters:\r\n");
            }
            List<IFormFile> files = new List<IFormFile>();
            //Open the stream and read it back.

            using (FileStream fs = File.OpenRead(path))
            {
                var stream = File.OpenRead(path);
                FormFile _formFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/docx"
                };

                files.Add(_formFile);
            }

            mockformFile.Setup(_ => _.OpenReadStream()).Returns(new MemoryStream());

            mockformFile.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>())).Returns((Stream stream, CancellationToken token) => ms.CopyToAsync(stream)).Verifiable();
            mockfileservice.Setup(x => x.Order(It.IsAny<FileOrderModel>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();

            order = @"[{ 'fileName': null,'order': 0}]";
            IActionResult result = await controller.Submit(id, requestId, docId, order, files);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestSubmitControllerException()
        {
            Mock<ISettingService> mocksettingservice = new Mock<ISettingService>();
            Mock<IFileService> mockfileservice = new Mock<IFileService>();
            Mock<IFtpClient> mockftpclient = new Mock<IFtpClient>();
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            //Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IFileEncryptor> mockfileencryptor = new Mock<IFileEncryptor>();
            Mock<IFileEncryptionFactory> mockfileencryptorfacotry = new Mock<IFileEncryptionFactory>(MockBehavior.Strict);
            //Mock<IFileSystem> mockfilesystem = new Mock<IFileSystem>();
            Mock<IFormFile> mockformFile = new Mock<IFormFile>();
            Mock<IKeyStoreService> mockKeyStoreService = new Mock<IKeyStoreService>();

            string filePath = Path.GetTempFileName();
            MemoryStream ms = new MemoryStream();
            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";
            setting.maxFileSize = 15000000;
            setting.maxFileNameSize = 255;
            setting.allowedExtensions = new string[] { ".tmp" };
            mockconfiguration.Setup(x => x["KeyStore:Url"]).Returns("http://test.com");
            mockconfiguration.Setup(x => x["File:Key"]).Returns("Key");
            mockconfiguration.Setup(x => x["File:Algo"]).Returns("Algo");

            mockKeyStoreService.Setup(x => x.GetFileKey()).ReturnsAsync("this is a very long password");
            mockKeyStoreService.Setup(x => x.GetFtpKey()).ReturnsAsync("this is the long and strong key.");
            mocksettingservice.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockftpclient.Setup(x => x.Setup(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns(filePath);
            mockftpclient.Setup(x => x.UploadAsync(Path.GetFileName(filePath), filePath)).Verifiable();
            mockfileservice.Setup(x => x.Submit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(),It.IsAny<List<string>>())).ReturnsAsync(string.Empty);

            FileOrderModel model = new FileOrderModel
            {
                id = "1",
                docId = "1",
                requestId = "1",
                files = new List<FileNameModel>()
            };
            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Connection.RemoteIpAddress).Returns(IPAddress.Parse("127.0.0.1"));
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            

            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            FileController controller = new FileController(mockfileservice.Object, mockfileencryptorfacotry.Object, mockftpclient.Object, mocksettingservice.Object, mockKeyStoreService.Object, mockconfiguration.Object, Mock.Of<ILogger<FileController>>(), null, null, null, null);
            controller.ControllerContext = context;
            string id = "5eb25d1fe519051af2eeb72d";
            string requestId = "abc15d1fe456051af2eeb768";
            string docId = "ddd25d1fe456057652eeb72d";
            string order = "0";
            //int tenantId = 1;
            //var stream = File.OpenRead(@"C:\NET Unit Testing.docx");

            string path = Path.GetTempFileName();

            //Create the file.
            using (FileStream fs = File.Create(path))
            {
                AddText(fs, "This is some text");
                AddText(fs, "This is some more text,");
                AddText(fs, "\r\nand this is on a new line");
                AddText(fs, "\r\n\r\nThe following is a subset of characters:\r\n");
            }
            List<IFormFile> files = new List<IFormFile>();
            //Open the stream and read it back.

            using (FileStream fs = File.OpenRead(path))
            {
                var stream = File.OpenRead(path);
                FormFile _formFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/docx"
                };

                files.Add(_formFile);
            }

            mockformFile.Setup(_ => _.OpenReadStream()).Returns(new MemoryStream());

            mockformFile.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>())).Returns((Stream stream, CancellationToken token) => ms.CopyToAsync(stream)).Verifiable();
            mockfileservice.Setup(x => x.Order(It.IsAny<FileOrderModel>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();

            order = @"[{ 'fileName': null,'order': 0}]";
            var result = controller.Submit(id, requestId, docId, order, files).Result;
            Assert.IsType<BadRequestObjectResult>(result);
            //await Assert.ThrowsAsync<DocumentManagementException>(async () => { await controller.Submit(id, requestId, docId, order, files); });
        }

        [Fact]
        public async Task TestSubmitControllerExceptionFileSize()
        {
            Mock<ISettingService> mocksettingservice = new Mock<ISettingService>();
            Mock<IFileService> mockfileservice = new Mock<IFileService>();
            Mock<IFtpClient> mockftpclient = new Mock<IFtpClient>();
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IKeyStoreService> mockKeyStoreService = new Mock<IKeyStoreService>();
            Mock<IFileEncryptor> mockfileencryptor = new Mock<IFileEncryptor>();
            Mock<IFileEncryptionFactory> mockfileencryptorfacotry = new Mock<IFileEncryptionFactory>(MockBehavior.Strict);
            Mock<IFileSystem> mockfilesystem = new Mock<IFileSystem>();
            var formFile = new Mock<IFormFile>();

            mockconfiguration.Setup(x => x["KeyStore:Url"]).Returns("http://test.com");
            mockconfiguration.Setup(x => x["File:FtpKey"]).Returns("FtpKey");
            mockconfiguration.Setup(x => x["File:Algo"]).Returns("Algo");
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var handlerMock1 = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var handlerMock2 = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            string filePath = Path.GetTempFileName();
            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";
            setting.maxFileSize = 0;
            setting.allowedExtensions = new string[2] { "txt", "png" };
            mockKeyStoreService.Setup(x => x.GetFileKey()).ReturnsAsync("this is a very long password");
            mockKeyStoreService.Setup(x => x.GetFtpKey()).ReturnsAsync("this is the long and strong key.");
            mocksettingservice.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockftpclient.Setup(x => x.Setup(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns(filePath);
            mockftpclient.Setup(x => x.UploadAsync(Path.GetFileName(filePath), filePath)).Verifiable();

            mockfileservice.Setup(x => x.Submit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(),It.IsAny<List<string>>())).Verifiable();

            FileOrderModel model = new FileOrderModel
            {
                id = "1",
                docId = "1",
                requestId = "1",
                files = new List<FileNameModel>()
            };

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            //Assert
            FileController controller = new FileController(mockfileservice.Object, mockfileencryptorfacotry.Object, mockftpclient.Object, mocksettingservice.Object, mockKeyStoreService.Object, mockconfiguration.Object, Mock.Of<ILogger<FileController>>(), null, null, null, null);
            controller.ControllerContext = context;
            string id = "5eb25d1fe519051af2eeb72d"; string requestId = "abc15d1fe456051af2eeb768";
            string docId = "ddd25d1fe456057652eeb72d"; string order = "0";
            //int tenantId = 1;

            string path = Path.GetTempFileName();

            //Create the file.
            using (FileStream fs = File.Create(path))
            {
                AddText(fs, "This is some text");
                AddText(fs, "This is some more text,");
                AddText(fs, "\r\nand this is on a new line");
                AddText(fs, "\r\n\r\nThe following is a subset of characters:\r\n");
            }
            List<IFormFile> files = new List<IFormFile>();
            //Open the stream and read it back.

            using (FileStream fs = File.OpenRead(path))
            {
                var stream = File.OpenRead(path);
                FormFile _formFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/docx"
                };

                files.Add(_formFile);
            }

            formFile.Setup(_ => _.OpenReadStream()).Returns(new MemoryStream());
            mockfileservice.Setup(x => x.Order(It.IsAny<FileOrderModel>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();
            order = @"[{ 'fileName': null,'order': 0}]";
            var result =await controller.Submit(id, requestId, docId, order, files);
            Assert.IsType<BadRequestObjectResult>(result);
            //await Assert.ThrowsAsync<DocumentManagementException>(async () => { await controller.Submit(id, requestId, docId, order, files); });
        }
        [Fact]
        public async Task TestSubmitIsStartedTrueService()
        {
            Mock<UpdateResult> mockUpdateResult = new Mock<UpdateResult>();
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Request>> mockCollection = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IMongoCollection<Entity.Request>> mockcollectionRequst = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            {
                 new BsonDocument
                {
                    { "_id" ,"5f0e8d014e72f52edcff3885"}
                }
            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockcollectionRequst.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.SetupSequence(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockcollectionRequst.Object).Returns(mockCollection.Object);

            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Entity.Request>>(), It.IsAny<UpdateDefinition<Entity.Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(0, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            IFileService fileService = new FileService(mock.Object, mockActivityLogService.Object, Mock.Of<IRainmakerService>());

            //Act
            string contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            string id = "5eb25d1fe519051af2eeb72d";
            string requestId = "abc15d1fe456051af2eeb768";
            string docId = "ddd25d1fe456057652eeb72d";
            string clientName = "NET Unit Testing.docx";
            string serverName = "99e80b3c-2c09-483c-b85b-bf5d54ad45a0.enc";
            int size = 84989;
            string encryptionKey = "FileKey";
            string encryptionAlgorithm = "";
            int tenantId = 1;
            int userProfileId = 1;
            await fileService.Submit(contentType, id, requestId, docId, clientName, serverName, size, encryptionKey, encryptionAlgorithm, tenantId, userProfileId,null);

            //Assert
            mockCollection.VerifyAll();

        }
        [Fact]
        public async Task TestSubmitIsStartedFalseService()
        {
            Mock<UpdateResult> mockUpdateResult = new Mock<UpdateResult>();
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Request>> mockCollection = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IMongoCollection<Entity.Request>> mockcollectionRequst = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            {
                 new BsonDocument
                {
                    { "_id" ,"5f0e8d014e72f52edcff3885"}
                }
            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockcollectionRequst.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.SetupSequence(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockcollectionRequst.Object).Returns(mockCollection.Object);

            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Entity.Request>>(), It.IsAny<UpdateDefinition<Entity.Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(0, 1, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            IFileService fileService = new FileService(mock.Object, mockActivityLogService.Object,Mock.Of<IRainmakerService>());

            //Act
            string contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            string id = "5eb25d1fe519051af2eeb72d";
            string requestId = "abc15d1fe456051af2eeb768";
            string docId = "ddd25d1fe456057652eeb72d";
            string clientName = "NET Unit Testing.docx";
            string serverName = "99e80b3c-2c09-483c-b85b-bf5d54ad45a0.enc";
            int size = 84989;
            string encryptionKey = "FileKey";
            string encryptionAlgorithm = "";
            int tenantId = 1;
            int userProfileId = 1;
            await fileService.Submit(contentType, id, requestId, docId, clientName, serverName, size, encryptionKey, encryptionAlgorithm, tenantId, userProfileId,new List<string>());

            //Assert
            mockCollection.VerifyAll();

        }

        [Fact]
        public async Task TestSubmitControllerExceptionFileNameSize()
        {
            Mock<ISettingService> mocksettingservice = new Mock<ISettingService>();
            Mock<IFileService> mockfileservice = new Mock<IFileService>();
            Mock<IFtpClient> mockftpclient = new Mock<IFtpClient>();
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IKeyStoreService> mockKeyStoreService = new Mock<IKeyStoreService>();
            Mock<IFileEncryptor> mockfileencryptor = new Mock<IFileEncryptor>();
            Mock<IFileEncryptionFactory> mockfileencryptorfacotry = new Mock<IFileEncryptionFactory>(MockBehavior.Strict);
            Mock<IFileSystem> mockfilesystem = new Mock<IFileSystem>();
            var formFile = new Mock<IFormFile>();

            mockconfiguration.Setup(x => x["KeyStore:Url"]).Returns("http://test.com");
            mockconfiguration.Setup(x => x["File:FtpKey"]).Returns("FtpKey");
            mockconfiguration.Setup(x => x["File:Algo"]).Returns("Algo");
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var handlerMock1 = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var handlerMock2 = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            string filePath = Path.GetTempFileName();
            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";
            setting.maxFileSize = 1000000;
            setting.maxFileNameSize = 255;
            setting.allowedExtensions =new string[2]{"jpg","txt" };
            string FileName = new string('a', 500);
            mockKeyStoreService.Setup(x => x.GetFileKey()).ReturnsAsync("this is a very long password");
            mockKeyStoreService.Setup(x => x.GetFtpKey()).ReturnsAsync("this is the long and strong key.");
            mocksettingservice.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockftpclient.Setup(x => x.Setup(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns(filePath);
            mockftpclient.Setup(x => x.UploadAsync(FileName, filePath)).Verifiable();

            mockfileservice.Setup(x => x.Submit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(),It.IsAny<List<string>>())).Verifiable();

            FileOrderModel model = new FileOrderModel
            {
                id = "1",
                docId = "1",
                requestId = "1",
                files = new List<FileNameModel>()
            };

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            //Assert
            FileController controller = new FileController(mockfileservice.Object, mockfileencryptorfacotry.Object, mockftpclient.Object, mocksettingservice.Object, mockKeyStoreService.Object, mockconfiguration.Object, Mock.Of<ILogger<FileController>>(), null, null, null, null);
            controller.ControllerContext = context;
            string id = "5eb25d1fe519051af2eeb72d"; string requestId = "abc15d1fe456051af2eeb768";
            string docId = "ddd25d1fe456057652eeb72d"; string order = "0";
            //int tenantId = 1;

            string path = Path.GetTempFileName();

            //Create the file.
            using (FileStream fs = File.Create(path))
            {
                AddText(fs, "This is some text");
                AddText(fs, "This is some more text,");
                AddText(fs, "\r\nand this is on a new line");
                AddText(fs, "\r\n\r\nThe following is a subset of characters:\r\n");
            }
            List<IFormFile> files = new List<IFormFile>();
            //Open the stream and read it back.

            using (FileStream fs = File.OpenRead(path))
            {
                var stream = File.OpenRead(path);
                FormFile _formFile = new FormFile(stream, 0, stream.Length, null, FileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/docx"
                };

                files.Add(_formFile);
            }

            formFile.Setup(_ => _.OpenReadStream()).Returns(new MemoryStream());
            mockfileservice.Setup(x => x.Order(It.IsAny<FileOrderModel>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();
            order = @"[{ 'fileName': null,'order': 0}]";
            var result =await controller.Submit(id, requestId, docId, order, files);
            Assert.IsType<BadRequestObjectResult>(result);
           // await Assert.ThrowsAsync<DocumentManagementException>(async () => { await controller.Submit(id, requestId, docId, order, files); });
        }
        [Fact]
        public async Task TestSubmitService()
        {
            Mock<UpdateResult> mockUpdateResult = new Mock<UpdateResult>();
            Mock<IMongoService> mock = new Mock<IMongoService>();
            Mock<IActivityLogService> mockActivityLogService = new Mock<IActivityLogService>();
            Mock<IMongoDatabase> mockdb = new Mock<IMongoDatabase>();
            Mock<IMongoCollection<Entity.Request>> mockCollection = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IMongoCollection<Entity.Request>> mockcollectionRequst = new Mock<IMongoCollection<Entity.Request>>();
            Mock<IAsyncCursor<BsonDocument>> mockCursor = new Mock<IAsyncCursor<BsonDocument>>();

            List<BsonDocument> list = new List<BsonDocument>()
            {
                 new BsonDocument
                {
                    { "_id" ,"5f0e8d014e72f52edcff3885"}
                }
            };

            mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            mockCursor.SetupGet(x => x.Current).Returns(list);

            mockcollectionRequst.Setup(x => x.Aggregate(It.IsAny<PipelineDefinition<Entity.Request, BsonDocument>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).Returns(mockCursor.Object);

            mockdb.SetupSequence(x => x.GetCollection<Entity.Request>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(mockcollectionRequst.Object).Returns(mockCollection.Object);

            mockCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Entity.Request>>(), It.IsAny<UpdateDefinition<Entity.Request>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateResult.Acknowledged(0, 0, BsonInt32.Create(1)));

            mock.SetupGet(x => x.db).Returns(mockdb.Object);
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            IFileService fileService = new FileService(mock.Object, mockActivityLogService.Object, Mock.Of<IRainmakerService>());

            //Act
            string contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            string id = "5eb25d1fe519051af2eeb72d";
            string requestId = "abc15d1fe456051af2eeb768";
            string docId = "ddd25d1fe456057652eeb72d";
            string clientName = "NET Unit Testing.docx";
            string serverName = "99e80b3c-2c09-483c-b85b-bf5d54ad45a0.enc";
            int size = 84989;
            string encryptionKey = "FileKey";
            string encryptionAlgorithm = "";
            int tenantId = 1;
            int userProfileId = 1;
            await fileService.Submit(contentType, id, requestId, docId, clientName, serverName, size, encryptionKey, encryptionAlgorithm, tenantId, userProfileId,new List<string>());

            //Assert
            mockCollection.VerifyAll();

        }

        [Fact]
        public async Task TestSubmitControllerExceptionFileExtension()
        {
            Mock<ISettingService> mocksettingservice = new Mock<ISettingService>();
            Mock<IFileService> mockfileservice = new Mock<IFileService>();
            Mock<IFtpClient> mockftpclient = new Mock<IFtpClient>();
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IKeyStoreService> mockKeyStoreService = new Mock<IKeyStoreService>();
            Mock<IFileEncryptor> mockfileencryptor = new Mock<IFileEncryptor>();
            Mock<IFileEncryptionFactory> mockfileencryptorfacotry = new Mock<IFileEncryptionFactory>(MockBehavior.Strict);
            Mock<IFileSystem> mockfilesystem = new Mock<IFileSystem>();
            var formFile = new Mock<IFormFile>();

            mockconfiguration.Setup(x => x["KeyStore:Url"]).Returns("http://test.com");
            mockconfiguration.Setup(x => x["File:FtpKey"]).Returns("FtpKey");
            mockconfiguration.Setup(x => x["File:Algo"]).Returns("Algo");
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var handlerMock1 = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var handlerMock2 = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            string filePath = Path.GetTempFileName();
            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";
            setting.maxFileSize = 15000000;
            setting.maxFileNameSize = 255;
            setting.allowedExtensions = new string[] { ".doc" };
            mockKeyStoreService.Setup(x => x.GetFileKey()).ReturnsAsync("this is a very long password");
            mockKeyStoreService.Setup(x => x.GetFtpKey()).ReturnsAsync("this is the long and strong key.");
            mocksettingservice.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockftpclient.Setup(x => x.Setup(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns(filePath);
            mockftpclient.Setup(x => x.UploadAsync(Path.GetFileName(filePath), filePath)).Verifiable();

            mockfileservice.Setup(x => x.Submit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(),It.IsAny<List<string>>())).Verifiable();

            FileOrderModel model = new FileOrderModel
            {
                id = "1",
                docId = "1",
                requestId = "1",
                files = new List<FileNameModel>()
            };

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            //Assert
            FileController controller = new FileController(mockfileservice.Object, mockfileencryptorfacotry.Object, mockftpclient.Object, mocksettingservice.Object, mockKeyStoreService.Object, mockconfiguration.Object, Mock.Of<ILogger<FileController>>(), null, null, null, null);
            controller.ControllerContext = context;
            string id = "5eb25d1fe519051af2eeb72d"; string requestId = "abc15d1fe456051af2eeb768";
            string docId = "ddd25d1fe456057652eeb72d"; string order = "0";
            //int tenantId = 1;

            string path = Path.GetTempFileName();

            //Create the file.
            using (FileStream fs = File.Create(path))
            {
                AddText(fs, "This is some text");
                AddText(fs, "This is some more text,");
                AddText(fs, "\r\nand this is on a new line");
                AddText(fs, "\r\n\r\nThe following is a subset of characters:\r\n");
            }
            List<IFormFile> files = new List<IFormFile>();
            //Open the stream and read it back.

            using (FileStream fs = File.OpenRead(path))
            {
                var stream = File.OpenRead(path);
                FormFile _formFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/docx"
                };

                files.Add(_formFile);
            }

            formFile.Setup(_ => _.OpenReadStream()).Returns(new MemoryStream());
            mockfileservice.Setup(x => x.Order(It.IsAny<FileOrderModel>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();
            order = @"[{ 'fileName': null,'order': 0}]";
            var result = controller.Submit(id, requestId, docId, order, files).Result;
            Assert.IsType<BadRequestObjectResult>(result);
          //  await Assert.ThrowsAsync<DocumentManagementException>(async () => { await controller.Submit(id, requestId, docId, order, files); });
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
    }
}

