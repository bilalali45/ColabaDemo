 
using DocManager.API.Controllers;
using DocManager.Model;
using DocManager.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DocManager.Tests
{
    public partial class UnitTest
    {
        [Fact]
        public async Task TestGetTrashDocuments()
        {
            Mock<ITrashService> mockTrashService = new Mock<ITrashService>();
            Mock<IMongoService> mockMongoService = new Mock<IMongoService>();

            mockTrashService.Setup(x => x.GetTrashFiles(It.IsAny<int>(), It.IsAny<int>()));
            var controller = new TrashController(mockTrashService.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.GetTrashDocuments(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }
        [Fact]
        public async Task TestMoveFromTrashToWorkBench()
        {
            Mock<ITrashService> mockTrashService = new Mock<ITrashService>();
            Mock<IMongoService> mockMongoService = new Mock<IMongoService>();
            MoveFromTrashToWorkBench moveFromTrashToWorkBench = new MoveFromTrashToWorkBench();
            moveFromTrashToWorkBench.fromFileId = "5fc0f7f5dd5c73a764eafa38";
            moveFromTrashToWorkBench.id = "5fc0f5eddd5c73a764eafa2c";
            mockTrashService.Setup(x => x.MoveFromTrashToWorkBench(moveFromTrashToWorkBench, It.IsAny<int>())).ReturnsAsync(true);
            var controller = new TrashController(mockTrashService.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.MoveFromTrashToWorkBench(moveFromTrashToWorkBench);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }


        [Fact]
        public async Task TestGetWorkbenchDocuments()
        {
            Mock<IWorkbenchService> workbenchService = new Mock<IWorkbenchService>();
            Mock<IMongoService> mockMongoService = new Mock<IMongoService>();

            workbenchService.Setup(x => x.GetWorkbenchFiles(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new System.Collections.Generic.List<WorkbenchFile>());
            var controller = new WorkbenchController(workbenchService.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.GetWorkbenchDocuments(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task TestMoveFromWorkBenchToTrash()
        {
            Mock<IWorkbenchService> workbenchService = new Mock<IWorkbenchService>();
            Mock<IMongoService> mockMongoService = new Mock<IMongoService>();
            MoveFromWorkBenchToTrash moveFromWorkBenchToTrash = new MoveFromWorkBenchToTrash();
            moveFromWorkBenchToTrash.fromFileId = "5fc0f7f5dd5c73a764eafa38";
            workbenchService.Setup(x => x.MoveFromWorkBenchToTrash(moveFromWorkBenchToTrash, It.IsAny<int>())).ReturnsAsync(true);
            var controller = new WorkbenchController(workbenchService.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.MoveFromWorkBenchToTrash(moveFromWorkBenchToTrash);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task TestMoveFromWorkBenchToCategory()
        {
            Mock<IWorkbenchService> workbenchService = new Mock<IWorkbenchService>();
            Mock<IMongoService> mockMongoService = new Mock<IMongoService>();
            MoveFromWorkBenchToCategory moveFromWorkBenchToCategory = new MoveFromWorkBenchToCategory();
            moveFromWorkBenchToCategory.fromFileId = "5fc0f7f5dd5c73a764eafa38";
            moveFromWorkBenchToCategory.id = "5fc0f5eddd5c73a764eafa2c";
            moveFromWorkBenchToCategory.toDocId = "5fc0f5eedd5c73a764eafa2e";
            moveFromWorkBenchToCategory.toRequestId = "5fc0f5eedd5c73a764eafa2d";

            workbenchService.Setup(x => x.MoveFromWorkBenchToCategory(moveFromWorkBenchToCategory, It.IsAny<int>())).ReturnsAsync(true);
            var controller = new WorkbenchController(workbenchService.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.MoveFromWorkBenchToCategory(moveFromWorkBenchToCategory);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }

       
        [Fact]
        public async Task TestMoveFromCategoryToTrash()
        {
            Mock<IDocumentService> documentService = new Mock<IDocumentService>();
            Mock<IMongoService> mockMongoService = new Mock<IMongoService>();
            MoveFromCategoryToTrash moveFromCategoryToTrash = new MoveFromCategoryToTrash();

            moveFromCategoryToTrash.id = "5fc0f5eddd5c73a764eafa2c";
            moveFromCategoryToTrash.fromDocId = "5fc0f5eedd5c73a764eafa2e";
            moveFromCategoryToTrash.fromRequestId = "5fc0f5eedd5c73a764eafa2d";
            moveFromCategoryToTrash.fromFileId = "5fc0f7f5dd5c73a764eafa38";
            documentService.Setup(x => x.MoveFromCategoryToTrash(moveFromCategoryToTrash, It.IsAny<int>())).ReturnsAsync(true);
            var controller = new DocumentController(documentService.Object,Mock.Of<IRainmakerService>());

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.MoveFromCategoryToTrash(moveFromCategoryToTrash);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }


        [Fact]
        public async Task TestMoveFromCategoryToWorkBench()
        {
            Mock<IDocumentService> documentService = new Mock<IDocumentService>();
            Mock<IMongoService> mockMongoService = new Mock<IMongoService>();
            MoveFromCategoryToWorkBench moveFromCategoryToworkbench = new MoveFromCategoryToWorkBench();
            moveFromCategoryToworkbench.fromFileId = "5fc0f7f5dd5c73a764eafa37";
            moveFromCategoryToworkbench.id = "5fc0f5eddd5c73a764eafa2c";
            moveFromCategoryToworkbench.fromDocId = "5fc0f5eedd5c73a764eafa2e";
            moveFromCategoryToworkbench.fromRequestId = "5fc0f5eedd5c73a764eafa2d";

            documentService.Setup(x => x.MoveFromCategoryToWorkBench(moveFromCategoryToworkbench, It.IsAny<int>())).ReturnsAsync(true);
            var controller = new DocumentController(documentService.Object, Mock.Of<IRainmakerService>());

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.MoveFromCategoryToWorkBench(moveFromCategoryToworkbench);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }



        [Fact]
        public async Task TestMoveFromoneCategoryToAnotherCategory()
        {
            Mock<IDocumentService> documentService = new Mock<IDocumentService>();
            Mock<IMongoService> mockMongoService = new Mock<IMongoService>();
            MoveFromOneCategoryToAnotherCategory moveFromoneCategoryToAnotherCategory = new MoveFromOneCategoryToAnotherCategory();
            moveFromoneCategoryToAnotherCategory.fromFileId = "5fc0f7f5dd5c73a764eafa38";
            moveFromoneCategoryToAnotherCategory.id = "5fc0f5eddd5c73a764eafa2c";
            moveFromoneCategoryToAnotherCategory.fromDocId = "5fc0f5eedd5c73a764eafa2e";
            moveFromoneCategoryToAnotherCategory.fromRequestId = "5fc0f5eedd5c73a764eafa2d";

            documentService.Setup(x => x.MoveFromoneCategoryToAnotherCategory(moveFromoneCategoryToAnotherCategory, It.IsAny<int>())).ReturnsAsync(true);
            var controller = new DocumentController(documentService.Object, Mock.Of<IRainmakerService>());

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.MoveFromoneCategoryToAnotherCategory(moveFromoneCategoryToAnotherCategory);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }
        [Fact]
        public async Task TestGetTrashDocumentsService()
        {
            Mock<ITrashService> mockTrashService = new Mock<ITrashService>();
            Mock<IMongoService> mockMongoService = new Mock<IMongoService>();

            mockTrashService.Setup(x => x.GetTrashFiles(It.IsAny<int>(), It.IsAny<int>()));
            var controller = new TrashController(mockTrashService.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.GetTrashDocuments(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }
        /*
        [Fact]
        public async Task TestDelete()
        {
            Mock<IDocumentService> mockDocumentService = new Mock<IDocumentService>();
            Mock<IMongoService> mockMongoService = new Mock<IMongoService>();
            DeleteModel deleteModel = new DeleteModel();
            deleteModel.id = "5fc0f7f5dd5c73a764eafa38";
            deleteModel.requestId = "5fc0f5eddd5c73a764eafa2c";
            deleteModel.docId = "5fc0f5eedd5c73a764eafa2e";


            mockDocumentService.Setup(x => x.Delete(deleteModel, It.IsAny<int>()));
            var controller = new DocumentController(mockDocumentService.Object, Mock.Of<IRainmakerService>());
            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
           
  
 
            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.Delete(deleteModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }
        */
        [Fact]
        public async Task TestSaveCategoryAnnotations()
        {
            Mock<IDocumentService> mockDocumentService = new Mock<IDocumentService>();
            Mock<IMongoService> mockMongoService = new Mock<IMongoService>();
            SaveCategoryAnnotations saveCategoryAnnotations = new SaveCategoryAnnotations();
            saveCategoryAnnotations.id = "5fc0f7f5dd5c73a764eafa38";
            saveCategoryAnnotations.requestId = "5fc0f5eddd5c73a764eafa2c";
            saveCategoryAnnotations.docId = "5fc0f5eedd5c73a764eafa2e";
            saveCategoryAnnotations.fileId = "5fc0f5eedd5c73a764eafa2e";
            saveCategoryAnnotations.annotations = "abc";
            mockDocumentService.Setup(x => x.SaveCategoryAnnotations(saveCategoryAnnotations, It.IsAny<int>()));
            var controller = new DocumentController(mockDocumentService.Object, Mock.Of<IRainmakerService>());

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.SaveCategoryAnnotations(saveCategoryAnnotations);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task TestViewCategoryAnnotations()
        {
            Mock<IDocumentService> mockDocumentService = new Mock<IDocumentService>();
            Mock<IMongoService> mockMongoService = new Mock<IMongoService>();
            ViewCategoryAnnotations viewCategoryAnnotations = new ViewCategoryAnnotations();
            viewCategoryAnnotations.id = "5fc0f7f5dd5c73a764eafa38";
            viewCategoryAnnotations.fromRequestId = "5fc0f5eddd5c73a764eafa2c";
            viewCategoryAnnotations.fromDocId = "5fc0f5eedd5c73a764eafa2e";
            viewCategoryAnnotations.fromFileId = "5fc0f5eedd5c73a764eafa2e";

            mockDocumentService.Setup(x => x.ViewCategoryAnnotations(viewCategoryAnnotations, It.IsAny<int>()));
            var controller = new DocumentController(mockDocumentService.Object, Mock.Of<IRainmakerService>());

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.ViewCategoryAnnotations(viewCategoryAnnotations);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }


        [Fact]
        public async Task TestSaveCategoryDocument()
        {
            Mock<IThumbnailService> mockThumbnailService = new Mock<IThumbnailService>();
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            Mock<IFileEncryptor> mockfileencryptor = new Mock<IFileEncryptor>();
            Mock<ISettingService> mockSettingService = new Mock<ISettingService>();
            Mock<IFtpClient> mockFtpClient = new Mock<IFtpClient>();
            Mock<IKeyStoreService> mockKeyStoreService = new Mock<IKeyStoreService>();
            Mock<ILogger<ThumbnailController>> mockLogger = new Mock<ILogger<ThumbnailController>>();
            Mock<IFileEncryptionFactory> mockfileencryptorfacotry = new Mock<IFileEncryptionFactory>();
            Mock<IFormFile> mockformFile = new Mock<IFormFile>();
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
            mockconfiguration.Setup(x => x["File:FtpKey"]).Returns("FtpKey");
            mockconfiguration.Setup(x => x["File:Key"]).Returns("Key");
            mockconfiguration.Setup(x => x["File:Algo"]).Returns("Algo");
            FileViewDto fileViewDTO = new FileViewDto();
            fileViewDTO.serverName = "a69ad17f-7505-492d-a92e-f32967cecff8.enc";
            fileViewDTO.encryptionKey = "FileKey";
            fileViewDTO.encryptionAlgorithm = "AES";
            fileViewDTO.clientName = "NET Unit Testing.docx";
            fileViewDTO.contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

            Mock<IMongoService> mockMongoService = new Mock<IMongoService>();
         
            mockThumbnailService.Setup(x => x.SaveCategoryDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()
                , It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            mockSettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockFtpClient.Setup(x => x.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword));
            mockFtpClient.Setup(x => x.DownloadAsync(fileViewDTO.serverName, Path.GetTempFileName())).Verifiable();

            mockKeyStoreService.Setup(x => x.GetFileKey()).ReturnsAsync("this is a very long password");
            mockKeyStoreService.Setup(x => x.GetFtpKey()).ReturnsAsync("this is the long and strong key.");
            var controller = new ThumbnailController(mockThumbnailService.Object, mockconfiguration.Object,
                mockSettingService.Object, mockFtpClient.Object, mockKeyStoreService.Object, mockLogger.Object, mockfileencryptorfacotry.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("FirstName")).Returns(new Claim("FirstName", "abc"));
            httpContext.Setup(m => m.User.FindFirst("LastName")).Returns(new Claim("LastName", "cde"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
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

            string id = "5eb25d1fe519051af2eeb72d";
            string requestId = "abc15d1fe456051af2eeb768";
            string docId = "ddd25d1fe456057652eeb72d";
            string fileId = "5fc0fb11ad4581295f8ddd58";
            mockformFile.Setup(_ => _.OpenReadStream()).Returns(new MemoryStream());
            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns(filePath);
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.fileId = "5f0ede3cce9c4b62509d0dbf";
            mockThumbnailService.Setup(x => x.SaveCategoryDocument(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(saveWorkbenchDocument);

            IActionResult result = await controller.SaveCategoryDocument(id, requestId, docId,
               fileId, files[0]);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }
        



        [Fact]
        public async Task TestAcquireLock()
        {
            Mock<ILockService>  mockLockService = new Mock<ILockService>();
            Mock<IMongoService> mockMongoService = new Mock<IMongoService>();
           
            
            LockModel  lockModel   = new LockModel();
         
            lockModel.loanApplicationId =1002;

            Lock _lock = new Lock();
            _lock.id = "5fb51519e223e0428d82c41b";
            _lock.loanApplicationId = 1002;
            _lock.lockDateTime = DateTime.UtcNow;
            _lock.lockUserId = 1;
            mockLockService.Setup(x => x.AcquireLock(lockModel,It.IsAny<int>(),It.IsAny<string>())).ReturnsAsync(_lock);
            var controller = new LockController(mockLockService.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("FirstName")).Returns(new Claim("FirstName", "abc"));
            httpContext.Setup(m => m.User.FindFirst("LastName")).Returns(new Claim("LastName", "def"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.AcquireLock(lockModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task TestRetainLock()
        {
            Mock<ILockService> mockLockService = new Mock<ILockService>();
            Mock<IMongoService> mockMongoService = new Mock<IMongoService>();
            LockModel lockModel = new LockModel();
            lockModel.loanApplicationId = 1002;

            Lock _lock = new Lock();
            _lock.id = "5fb51519e223e0428d82c41b";
            _lock.loanApplicationId = 1002;
            _lock.lockDateTime = DateTime.UtcNow;
            _lock.lockUserId = 1;
            mockLockService.Setup(x => x.RetainLock(lockModel, It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(_lock);
            var controller = new LockController(mockLockService.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("FirstName")).Returns(new Claim("FirstName", "abc"));
            httpContext.Setup(m => m.User.FindFirst("LastName")).Returns(new Claim("LastName", "def"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.RetainLock(lockModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }


        [Fact]
        public async Task TestDeleteCategoryFile()
        {
            Mock<IDocumentService> mockDocumentService = new Mock<IDocumentService>();
            Mock<IMongoService> mockMongoService = new Mock<IMongoService>();
            DeleteCategoryFile deleteModel = new DeleteCategoryFile();
            deleteModel.id = "5fc0f7f5dd5c73a764eafa38";
            deleteModel.requestId = "5fc0f5eddd5c73a764eafa2c";
            deleteModel.docId = "5fc0f5eedd5c73a764eafa2e";
            deleteModel.fileId = "5fc0f5eedd5c73a764eafa2e";

            mockDocumentService.Setup(x => x.DeleteCategoryFile(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            var controller = new DocumentController(mockDocumentService.Object, Mock.Of<IRainmakerService>());
            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));



            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.DeleteCategoryFile(deleteModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }


        [Fact]
        public async Task TestSaveWorkbenchDocument()
        {
            Mock<IThumbnailService> mockThumbnailService = new Mock<IThumbnailService>();
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            Mock<IFileEncryptor> mockfileencryptor = new Mock<IFileEncryptor>();
            Mock<ISettingService> mockSettingService = new Mock<ISettingService>();
            Mock<IFtpClient> mockFtpClient = new Mock<IFtpClient>();
            Mock<IKeyStoreService> mockKeyStoreService = new Mock<IKeyStoreService>();
            Mock<ILogger<ThumbnailController>> mockLogger = new Mock<ILogger<ThumbnailController>>();
            Mock<IFileEncryptionFactory> mockfileencryptorfacotry = new Mock<IFileEncryptionFactory>();
            Mock<IFormFile> mockformFile = new Mock<IFormFile>();
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
            mockconfiguration.Setup(x => x["File:FtpKey"]).Returns("FtpKey");
            mockconfiguration.Setup(x => x["File:Key"]).Returns("Key");
            mockconfiguration.Setup(x => x["File:Algo"]).Returns("Algo");
            FileViewDto fileViewDTO = new FileViewDto();
            fileViewDTO.serverName = "a69ad17f-7505-492d-a92e-f32967cecff8.enc";
            fileViewDTO.encryptionKey = "FileKey";
            fileViewDTO.encryptionAlgorithm = "AES";
            fileViewDTO.clientName = "NET Unit Testing.docx";
            fileViewDTO.contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

            Mock<IMongoService> mockMongoService = new Mock<IMongoService>();
             
            mockThumbnailService.Setup(x => x.SaveWorkbenchDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>() , It.IsAny<string>()));

            mockSettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockFtpClient.Setup(x => x.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword));
            mockFtpClient.Setup(x => x.DownloadAsync(fileViewDTO.serverName, Path.GetTempFileName())).Verifiable();

            mockKeyStoreService.Setup(x => x.GetFileKey()).ReturnsAsync("this is a very long password");
            mockKeyStoreService.Setup(x => x.GetFtpKey()).ReturnsAsync("this is the long and strong key.");
            var controller = new ThumbnailController(mockThumbnailService.Object, mockconfiguration.Object,
                mockSettingService.Object, mockFtpClient.Object, mockKeyStoreService.Object, mockLogger.Object, mockfileencryptorfacotry.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("FirstName")).Returns(new Claim("FirstName", "abc"));
            httpContext.Setup(m => m.User.FindFirst("LastName")).Returns(new Claim("LastName", "cde"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
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

            string id = "5eb25d1fe519051af2eeb72d";
            string requestId = "abc15d1fe456051af2eeb768";
            string docId = "ddd25d1fe456057652eeb72d";
            string fileId = "5fc0fb11ad4581295f8ddd58";
            mockformFile.Setup(_ => _.OpenReadStream()).Returns(new MemoryStream());
            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns(filePath);
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.fileId = "5f0ede3cce9c4b62509d0dbf";
            mockThumbnailService.Setup(x => x.SaveCategoryDocument(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(saveWorkbenchDocument);

            IActionResult result = await controller.SaveCategoryDocument(id, requestId, docId,
               fileId, files[0]);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task TestSaveTrashDocument()
        {
            Mock<IThumbnailService> mockThumbnailService = new Mock<IThumbnailService>();
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            Mock<IFileEncryptor> mockfileencryptor = new Mock<IFileEncryptor>();
            Mock<ISettingService> mockSettingService = new Mock<ISettingService>();
            Mock<IFtpClient> mockFtpClient = new Mock<IFtpClient>();
            Mock<IKeyStoreService> mockKeyStoreService = new Mock<IKeyStoreService>();
            Mock<ILogger<ThumbnailController>> mockLogger = new Mock<ILogger<ThumbnailController>>();
            Mock<IFileEncryptionFactory> mockfileencryptorfacotry = new Mock<IFileEncryptionFactory>();
            Mock<IFormFile> mockformFile = new Mock<IFormFile>();
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
            mockconfiguration.Setup(x => x["File:FtpKey"]).Returns("FtpKey");
            mockconfiguration.Setup(x => x["File:Key"]).Returns("Key");
            mockconfiguration.Setup(x => x["File:Algo"]).Returns("Algo");
            FileViewDto fileViewDTO = new FileViewDto();
            fileViewDTO.serverName = "a69ad17f-7505-492d-a92e-f32967cecff8.enc";
            fileViewDTO.encryptionKey = "FileKey";
            fileViewDTO.encryptionAlgorithm = "AES";
            fileViewDTO.clientName = "NET Unit Testing.docx";
            fileViewDTO.contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

            Mock<IMongoService> mockMongoService = new Mock<IMongoService>();

            mockThumbnailService.Setup(x => x.SaveTrashDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            mockSettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockFtpClient.Setup(x => x.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword));
            mockFtpClient.Setup(x => x.DownloadAsync(fileViewDTO.serverName, Path.GetTempFileName())).Verifiable();

            mockKeyStoreService.Setup(x => x.GetFileKey()).ReturnsAsync("this is a very long password");
            mockKeyStoreService.Setup(x => x.GetFtpKey()).ReturnsAsync("this is the long and strong key.");
            var controller = new ThumbnailController(mockThumbnailService.Object, mockconfiguration.Object,
                mockSettingService.Object, mockFtpClient.Object, mockKeyStoreService.Object, mockLogger.Object, mockfileencryptorfacotry.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("FirstName")).Returns(new Claim("FirstName", "abc"));
            httpContext.Setup(m => m.User.FindFirst("LastName")).Returns(new Claim("LastName", "cde"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
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

            string id = "5eb25d1fe519051af2eeb72d";
            string requestId = "abc15d1fe456051af2eeb768";
            string docId = "ddd25d1fe456057652eeb72d";
            string fileId = "5fc0fb11ad4581295f8ddd58";
            mockformFile.Setup(_ => _.OpenReadStream()).Returns(new MemoryStream());
            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns(filePath);
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.fileId = "5f0ede3cce9c4b62509d0dbf";
            mockThumbnailService.Setup(x => x.SaveCategoryDocument(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(saveWorkbenchDocument);

            IActionResult result = await controller.SaveCategoryDocument(id, requestId, docId,
               fileId, files[0]);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }
        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
       
    }
}
