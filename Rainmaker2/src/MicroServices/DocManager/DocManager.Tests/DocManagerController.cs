
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
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading;
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
           
            List<WorkbenchFile> workbenchFiles = new List<WorkbenchFile>()
            {
                new WorkbenchFile()
                { id="5fb51519e223e0428d82c41b"
                }
            };
            mockTrashService.Setup(x => x.GetTrashFiles(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(workbenchFiles);
            var controller = new TrashController(mockTrashService.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.GetTrashDocuments(1);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("5fb51519e223e0428d82c41b", (res.Value as List<WorkbenchFile>)[0].id);
        }
        [Fact]
        public async Task TestMoveFromTrashToWorkBench()
        {
            Mock<ITrashService> mockTrashService = new Mock<ITrashService>();
           
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
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(true, res.Value);
        }


        [Fact]
        public async Task TestGetWorkbenchDocuments()
        {
            Mock<IWorkbenchService> workbenchService = new Mock<IWorkbenchService>();
            
            List<WorkbenchFile> workbenchFiles = new List<WorkbenchFile>()
            {
                new WorkbenchFile ()
                {
                    id="5fb51519e223e0428d82c41b"
                }
            };
            workbenchService.Setup(x => x.GetWorkbenchFiles(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(workbenchFiles);
            var controller = new WorkbenchController(workbenchService.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.GetWorkbenchDocuments(1);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("5fb51519e223e0428d82c41b", (res.Value as List<WorkbenchFile>)[0].id);
        }

        [Fact]
        public async Task TestMoveFromWorkBenchToTrash()
        {
            Mock<IWorkbenchService> workbenchService = new Mock<IWorkbenchService>();
            
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
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(true, res.Value);

        }

        [Fact]
        public async Task TestMoveFromWorkBenchToCategory()
        {
            Mock<IWorkbenchService> workbenchService = new Mock<IWorkbenchService>();
            
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
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(true, res.Value);
        }


        [Fact]
        public async Task TestMoveFromCategoryToTrash()
        {
            Mock<IDocumentService> documentService = new Mock<IDocumentService>();
            
            MoveFromCategoryToTrash moveFromCategoryToTrash = new MoveFromCategoryToTrash();

            moveFromCategoryToTrash.id = "5fc0f5eddd5c73a764eafa2c";
            moveFromCategoryToTrash.fromDocId = "5fc0f5eedd5c73a764eafa2e";
            moveFromCategoryToTrash.fromRequestId = "5fc0f5eedd5c73a764eafa2d";
            moveFromCategoryToTrash.fromFileId = "5fc0f7f5dd5c73a764eafa38";
            documentService.Setup(x => x.MoveFromCategoryToTrash(moveFromCategoryToTrash, It.IsAny<int>())).ReturnsAsync(true);
            var controller = new DocumentController(documentService.Object, Mock.Of<IRainmakerService>());

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.MoveFromCategoryToTrash(moveFromCategoryToTrash);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(true, res.Value);

        }


        [Fact]
        public async Task TestMoveFromCategoryToWorkBench()
        {
            Mock<IDocumentService> documentService = new Mock<IDocumentService>();
          
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
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(true, res.Value);
        }



        [Fact]
        public async Task TestMoveFromoneCategoryToAnotherCategory()
        {
            Mock<IDocumentService> documentService = new Mock<IDocumentService>();
           
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
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(true, res.Value);
        }
        [Fact]
        public async Task TestGetTrashDocumentsService()
        {
            Mock<ITrashService> mockTrashService = new Mock<ITrashService>();
         
            List<WorkbenchFile> workbenchFiles = new List<WorkbenchFile>()
            {
                new WorkbenchFile()
                { id="5fb51519e223e0428d82c41b"
                }
            };
            mockTrashService.Setup(x => x.GetTrashFiles(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(workbenchFiles);
            var controller = new TrashController(mockTrashService.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.GetTrashDocuments(1);



            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("5fb51519e223e0428d82c41b", (res.Value as List<WorkbenchFile>)[0].id);

        }
       
        [Fact]
        public async Task TestSaveCategoryAnnotations()
        {
            Mock<IDocumentService> mockDocumentService = new Mock<IDocumentService>();
            
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
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(false, res.Value);
        }

        [Fact]
        public async Task TestViewCategoryAnnotations()
        {
            Mock<IDocumentService> mockDocumentService = new Mock<IDocumentService>();
           
            ViewCategoryAnnotations viewCategoryAnnotations = new ViewCategoryAnnotations();
            viewCategoryAnnotations.id = "5fc0f7f5dd5c73a764eafa38";
            viewCategoryAnnotations.fromRequestId = "5fc0f5eddd5c73a764eafa2c";
            viewCategoryAnnotations.fromDocId = "5fc0f5eedd5c73a764eafa2e";
            viewCategoryAnnotations.fromFileId = "5fc0f5eedd5c73a764eafa2e";

            mockDocumentService.Setup(x => x.ViewCategoryAnnotations(viewCategoryAnnotations, It.IsAny<int>())).ReturnsAsync("abc");
            var controller = new DocumentController(mockDocumentService.Object, Mock.Of<IRainmakerService>());

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.ViewCategoryAnnotations(viewCategoryAnnotations);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("abc", res.Value);
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

            

            mockThumbnailService.Setup(x => x.SaveCategoryDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()
                , It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            mockSettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockFtpClient.Setup(x => x.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword));
            mockFtpClient.Setup(x => x.DownloadAsync(fileViewDTO.serverName, new MemoryStream())).Verifiable();

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
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns((new MemoryStream(),""));
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.fileId = "5f0ede3cce9c4b62509d0dbf";
            mockThumbnailService.Setup(x => x.SaveCategoryDocument(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(saveWorkbenchDocument);

            IActionResult result = await controller.SaveCategoryDocument(id, requestId, docId,
               fileId, files[0]);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("5f0ede3cce9c4b62509d0dbf", res.Value);
        }


        [Fact]
        public async Task TestSaveCategoryDocumentExtensionNotAllowed()
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
           
           
            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";
            setting.maxFileSize = 15000000;
            setting.maxFileNameSize = 255;
            setting.allowedExtensions = new string[] { ".pdf" };

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

           

            mockThumbnailService.Setup(x => x.SaveCategoryDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()
                , It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),It.IsAny<string>()));

            mockSettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockFtpClient.Setup(x => x.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword));
            mockFtpClient.Setup(x => x.DownloadAsync(fileViewDTO.serverName,new MemoryStream())).Verifiable();

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
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns((new MemoryStream(),""));
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.fileId = "5f0ede3cce9c4b62509d0dbf";
            mockThumbnailService.Setup(x => x.SaveCategoryDocument(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(saveWorkbenchDocument);

            IActionResult result = await controller.SaveCategoryDocument(id, requestId, docId,
               fileId, files[0]);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);

        }
      
        [ Fact]
        public async Task TestSaveCategoryDocumentMaxFileNameSize()
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
          
           
            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";
            setting.maxFileSize = 15000000;
            setting.maxFileNameSize = 0;
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

            

            mockThumbnailService.Setup(x => x.SaveCategoryDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()
                , It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            mockSettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockFtpClient.Setup(x => x.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword));
            mockFtpClient.Setup(x => x.DownloadAsync(fileViewDTO.serverName,new MemoryStream())).Verifiable();

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
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns((new MemoryStream(),""));
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.fileId = "5f0ede3cce9c4b62509d0dbf";
            mockThumbnailService.Setup(x => x.SaveCategoryDocument(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(saveWorkbenchDocument);

            IActionResult result = await controller.SaveCategoryDocument(id, requestId, docId,
               fileId, files[0]);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);

        }
        [Fact]
        public async Task TestSaveCategoryDocumentFileLength()
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
           
            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";
            setting.maxFileSize = 0;
            setting.maxFileNameSize = 255;
            setting.allowedExtensions = new string[] { ".temp" };

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

            

            mockThumbnailService.Setup(x => x.SaveCategoryDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()
                , It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            mockSettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockFtpClient.Setup(x => x.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword));
            mockFtpClient.Setup(x => x.DownloadAsync(fileViewDTO.serverName,new MemoryStream())).Verifiable();

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
            
           

           
            
            
            
            
            
            
            
            
            
            
            
            string id = "5eb25d1fe519051af2eeb72d";
            string requestId = "abc15d1fe456051af2eeb768";
            string docId = "ddd25d1fe456057652eeb72d";
            string fileId = "5fc0fb11ad4581295f8ddd58";
            mockformFile.Setup(_ => _.OpenReadStream()).Returns(new MemoryStream());
            mockformFile.SetupGet(x => x.Length).Returns(0);
            mockformFile.SetupGet(x => x.FileName).Returns("abc.temp");
            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns((new MemoryStream(),""));
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.fileId = "5f0ede3cce9c4b62509d0dbf";
            mockThumbnailService.Setup(x => x.SaveCategoryDocument(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(saveWorkbenchDocument);

            IActionResult result = await controller.SaveCategoryDocument(id, requestId, docId,
               fileId, mockformFile.Object);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);

        }
        [Fact]
        public async Task TestAcquireLock()
        {
            Mock<ILockService> mockLockService = new Mock<ILockService>();
           


            LockModel lockModel = new LockModel();

            lockModel.loanApplicationId = 1002;

            Lock _lock = new Lock();
            _lock.id = "5fb51519e223e0428d82c41b";
            _lock.loanApplicationId = 1002;
            _lock.lockDateTime = DateTime.UtcNow;
            _lock.lockUserId = 1;
            mockLockService.Setup(x => x.AcquireLock(lockModel, It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(_lock);
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
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("5fb51519e223e0428d82c41b", (res.Value as Lock).id);
        }
        [Fact]
        public async Task TestAcquireLockException()
        {
            Mock<ILockService> mockLockService = new Mock<ILockService>();
            


            LockModel lockModel = new LockModel();

            lockModel.loanApplicationId = 1002;

            Lock _lock = new Lock();
            _lock.id = "5fb51519e223e0428d82c41b";
            _lock.loanApplicationId = 0;
            _lock.lockDateTime = DateTime.UtcNow;
            _lock.lockUserId = 1;
            mockLockService.Setup(x => x.AcquireLock(null, It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(_lock);
            var controller = new LockController(mockLockService.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("FirstName")).Returns(new Claim("FirstName", "abc"));
            httpContext.Setup(m => m.User.FindFirst("LastName")).Returns(new Claim("LastName", "def"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
         
           
             //Assert
           
            await Assert.ThrowsAsync<DocManagerException>(async () => { await controller.AcquireLock(lockModel); });


        }
        [Fact]
        public async Task TestAcquireLockBadRequest()
        {
            Mock<ILockService> mockLockService = new Mock<ILockService>();
           

            LockModel lockModel = new LockModel();

            lockModel.loanApplicationId = 1002;

            Lock _lock = new Lock();
            _lock.id = "5fb51519e223e0428d82c41b";
            _lock.loanApplicationId = 1002;
            _lock.lockDateTime = DateTime.UtcNow;
            _lock.lockUserId = 0;
            mockLockService.Setup(x => x.AcquireLock(lockModel, It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(_lock);
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
            Assert.IsType<BadRequestObjectResult>(result);

        }
        [Fact]
        public async Task TestRetainLockBadRequestResult()
        {
            Mock<ILockService> mockLockService = new Mock<ILockService>();
           
            LockModel lockModel = new LockModel();
            lockModel.loanApplicationId = 1002;

            Lock _lock = new Lock();
            _lock.id = "5fb51519e223e0428d82c41b";
            _lock.loanApplicationId = 1002;
            _lock.lockDateTime = DateTime.UtcNow;
            _lock.lockUserId = 0;
            mockLockService.Setup(x => x.RetainLock(null, It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(_lock);
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
            Assert.IsType<BadRequestObjectResult>(result);

        }
        [Fact]
        public async Task TestRetainLock()
        {
            Mock<ILockService> mockLockService = new Mock<ILockService>();
          
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
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("5fb51519e223e0428d82c41b", (res.Value as Lock).id);

        }


        [Fact]
        public async Task TestDeleteCategoryFile()
        {
            Mock<IDocumentService> mockDocumentService = new Mock<IDocumentService>();
            
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
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(false, res.Value);
        }
        [Fact]
        public async Task TestDeleteCategoryFileIsNotNull()
        {
            Mock<IDocumentService> mockDocumentService = new Mock<IDocumentService>();
            
            DeleteCategoryFile deleteModel = new DeleteCategoryFile();
            deleteModel.id = "5fc0f7f5dd5c73a764eafa38";
            deleteModel.requestId = "5fc0f5eddd5c73a764eafa2c";
            deleteModel.docId = "5fc0f5eedd5c73a764eafa2e";
            deleteModel.fileId = "5fc0f5eedd5c73a764eafa2e";

            mockDocumentService.Setup(x => x.DeleteCategoryFile(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
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
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(true, res.Value);
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
            
            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";
            setting.maxFileSize = 15000000;
            setting.maxFileNameSize = 255;
            setting.maxMcuFileSize = 15000000;
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

           
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.fileId = "5f0ede3cce9c4b62509d0dbf";
            mockThumbnailService.Setup(x => x.SaveWorkbenchDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>() , It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(saveWorkbenchDocument);

            mockSettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockFtpClient.Setup(x => x.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword));
            mockFtpClient.Setup(x => x.DownloadAsync(fileViewDTO.serverName, new MemoryStream())).Verifiable();

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
            
           
            string fileId = "5fc0fb11ad4581295f8ddd58";
            mockformFile.Setup(_ => _.OpenReadStream()).Returns(new MemoryStream());
            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns((new MemoryStream(),""));

            mockThumbnailService.Setup(x => x.SaveCategoryDocument(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(saveWorkbenchDocument);

            IActionResult result = await controller.SaveWorkbenchDocument(id,
               fileId, files[0]);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var res = result as OkObjectResult;
            Assert.Equal("5f0ede3cce9c4b62509d0dbf", res.Value);

        }
        [Fact]
        public async Task TestSaveWorkbenchDocumentExtensionNotAllowed()
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

            

            mockThumbnailService.Setup(x => x.SaveWorkbenchDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            mockSettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockFtpClient.Setup(x => x.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword));
            mockFtpClient.Setup(x => x.DownloadAsync(fileViewDTO.serverName, new MemoryStream())).Verifiable();

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
            
            

            
            
            
            
            
            
            
            
            
            
            
            
            string id = "5eb25d1fe519051af2eeb72d";
            
            string fileId = "5fc0fb11ad4581295f8ddd58";
            mockformFile.Setup(_ => _.OpenReadStream()).Returns(new MemoryStream());
            mockformFile.SetupGet(x => x.Length).Returns(0);
            mockformFile.SetupGet(x => x.FileName).Returns("abc.temp");
            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns((new MemoryStream(),""));
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.fileId = "5f0ede3cce9c4b62509d0dbf";
            mockThumbnailService.Setup(x => x.SaveWorkbenchDocument(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(saveWorkbenchDocument);

            IActionResult result = await controller.SaveWorkbenchDocument(id,
               fileId, mockformFile.Object);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);

        }
        [Fact]
        public async Task TestSaveWorkbenchDocumentMaxMcuFileSize()
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
            
            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";
            setting.maxMcuFileSize = 0;
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

           

            mockThumbnailService.Setup(x => x.SaveWorkbenchDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            mockSettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockFtpClient.Setup(x => x.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword));
            mockFtpClient.Setup(x => x.DownloadAsync(fileViewDTO.serverName, new MemoryStream())).Verifiable();

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
           
           

           
            
            
            
            
            
            
            
            
            
            
            
            string id = "5eb25d1fe519051af2eeb72d";
           
            string fileId = "5fc0fb11ad4581295f8ddd58";
            mockformFile.Setup(_ => _.OpenReadStream()).Returns(new MemoryStream());
            mockformFile.SetupGet(x => x.Length).Returns(10);
            mockformFile.SetupGet(x => x.FileName).Returns("abc.tmp");
            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns((new MemoryStream(),""));
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.fileId = "5f0ede3cce9c4b62509d0dbf";
            mockThumbnailService.Setup(x => x.SaveWorkbenchDocument(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(saveWorkbenchDocument);

            IActionResult result = await controller.SaveWorkbenchDocument(id,
               fileId, mockformFile.Object);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);

        }
        [Fact]
        public async Task TestSaveWorkbenchDocumentMaxFileNameSize()
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
            
            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";
            setting.maxMcuFileSize = 15000000;
            setting.maxFileNameSize = 0;
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

            

            mockThumbnailService.Setup(x => x.SaveWorkbenchDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            mockSettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockFtpClient.Setup(x => x.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword));
            mockFtpClient.Setup(x => x.DownloadAsync(fileViewDTO.serverName, new MemoryStream())).Verifiable();

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
            
            string fileId = "5fc0fb11ad4581295f8ddd58";
            mockformFile.Setup(_ => _.OpenReadStream()).Returns(new MemoryStream());
            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns((new MemoryStream(),""));
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.fileId = "5f0ede3cce9c4b62509d0dbf";
            mockThumbnailService.Setup(x => x.SaveWorkbenchDocument(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(saveWorkbenchDocument);

            IActionResult result = await controller.SaveWorkbenchDocument(id,
               fileId, files[0]);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task TestSaveWorkbenchDocumentFileNotExists()
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
            
            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";
            setting.maxFileSize = 15000000;
            setting.maxFileNameSize = 255;
            setting.maxMcuFileSize = 15000000;
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

            

            mockThumbnailService.Setup(x => x.SaveWorkbenchDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            mockSettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockFtpClient.Setup(x => x.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword));
            mockFtpClient.Setup(x => x.DownloadAsync(fileViewDTO.serverName, new MemoryStream())).Verifiable();

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
           
            

            
            
            
            
            
            
            
            
            
            
            
            
            string id = "5eb25d1fe519051af2eeb72d";
            
            string fileId = "5fc0fb11ad4581295f8ddd58";
            mockformFile.Setup(_ => _.OpenReadStream()).Returns(new MemoryStream());
            mockformFile.Setup(_ => _.FileName).Returns("abc.tmp");
            mockformFile.Setup(_ => _.Length).Returns(0);

            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns((new MemoryStream(),""));
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.fileId = "5f0ede3cce9c4b62509d0dbf";
            mockThumbnailService.Setup(x => x.SaveWorkbenchDocument(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(saveWorkbenchDocument);

            IActionResult result = await controller.SaveWorkbenchDocument(id,
               fileId, mockformFile.Object);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
  

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

            

            mockThumbnailService.Setup(x => x.SaveTrashDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            mockSettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockFtpClient.Setup(x => x.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword));
            mockFtpClient.Setup(x => x.DownloadAsync(fileViewDTO.serverName, new MemoryStream())).Verifiable();

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
            
            string fileId = "5fc0fb11ad4581295f8ddd58";
            mockformFile.Setup(_ => _.OpenReadStream()).Returns(new MemoryStream());
            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns((new MemoryStream(), ""));
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.fileId = "5f0ede3cce9c4b62509d0dbf";

            mockThumbnailService.Setup(x => x.SaveTrashDocument(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(saveWorkbenchDocument);

            IActionResult result = await controller.SaveTrashDocument(id, fileId, files[0]);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("5f0ede3cce9c4b62509d0dbf", res.Value);
        }
        [Fact]
        public async Task TestSaveTrashDocumentExtensionNotAllowed()
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
            
            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";
            setting.maxFileSize = 15000000;
            setting.maxFileNameSize = 255;
            setting.allowedExtensions = new string[] { ".pdf" };
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

           

            mockThumbnailService.Setup(x => x.SaveTrashDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            mockSettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockFtpClient.Setup(x => x.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword));
            mockFtpClient.Setup(x => x.DownloadAsync(fileViewDTO.serverName, new MemoryStream())).Verifiable();

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
           
            string fileId = "5fc0fb11ad4581295f8ddd58";
            mockformFile.Setup(_ => _.OpenReadStream()).Returns(new MemoryStream());
            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns((new MemoryStream(), ""));
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.fileId = "5f0ede3cce9c4b62509d0dbf";

            mockThumbnailService.Setup(x => x.SaveTrashDocument(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(saveWorkbenchDocument);

            IActionResult result = await controller.SaveTrashDocument(id, fileId, files[0]);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);

        }
        [Fact]
        public async Task TestSaveTrashDocumentFileLength ()
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

          

            mockThumbnailService.Setup(x => x.SaveTrashDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            mockSettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockFtpClient.Setup(x => x.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword));
            mockFtpClient.Setup(x => x.DownloadAsync(fileViewDTO.serverName, new MemoryStream())).Verifiable();

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
            
            
        
            
            
            
            
            
            
            
            
            
            
            
            
            string id = "5eb25d1fe519051af2eeb72d";
            
            string fileId = "5fc0fb11ad4581295f8ddd58";
            mockformFile.Setup(_ => _.OpenReadStream()).Returns(new MemoryStream());
            mockformFile.SetupGet(x => x.Length).Returns(0);
            mockformFile.SetupGet(x => x.FileName).Returns("abc.tmp");
            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns((new MemoryStream(), ""));
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.fileId = "5f0ede3cce9c4b62509d0dbf";

            mockThumbnailService.Setup(x => x.SaveTrashDocument(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(saveWorkbenchDocument);

            IActionResult result = await controller.SaveTrashDocument(id, fileId, mockformFile.Object);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task TestSaveTrashDocumentMaxFileNameSize()
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
           
            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";
            setting.maxFileSize = 15000000;
            setting.maxFileNameSize = 0;
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

            

            mockThumbnailService.Setup(x => x.SaveTrashDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            mockSettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockFtpClient.Setup(x => x.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword));
            mockFtpClient.Setup(x => x.DownloadAsync(fileViewDTO.serverName, new MemoryStream())).Verifiable();

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
           
            

            
            
            
            
            
            
            
            
            
            
            
            
            string id = "5eb25d1fe519051af2eeb72d";
           
            string fileId = "5fc0fb11ad4581295f8ddd58";
            mockformFile.Setup(_ => _.OpenReadStream()).Returns(new MemoryStream());
            mockformFile.SetupGet(x => x.Length).Returns(0);
            mockformFile.SetupGet(x => x.FileName).Returns("abc.tmp");
           
            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns((new MemoryStream(),""));
            SaveWorkbenchDocument saveWorkbenchDocument = new SaveWorkbenchDocument();
            saveWorkbenchDocument.fileId = "5f0ede3cce9c4b62509d0dbf";
            mockThumbnailService.Setup(x => x.SaveCategoryDocument(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(saveWorkbenchDocument);

            mockThumbnailService.Setup(x => x.SaveTrashDocument(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(saveWorkbenchDocument);

            IActionResult result = await controller.SaveTrashDocument(id, fileId, mockformFile.Object);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);

        }
        
        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        [Fact]
        public async Task TestMoveFromTrashToCategory()
        {
            Mock<ITrashService> trashService = new Mock<ITrashService>();
           
            MoveFromTrashToCategory moveFromTrashToCategory = new MoveFromTrashToCategory();
            moveFromTrashToCategory.id = "5fc0f7f5dd5c73a764eafa38";
            moveFromTrashToCategory.toRequestId = "5fc0f7f5dd5c73a764eafa38";
            moveFromTrashToCategory.toDocId = "5fc0f7f5dd5c73a764eafa38";
            moveFromTrashToCategory.fromFileId = "5fc0f7f5dd5c73a764eafa38";
            trashService.Setup(x => x.MoveFromTrashToCategory(moveFromTrashToCategory, It.IsAny<int>())).ReturnsAsync(true);
            var controller = new TrashController(trashService.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.MoveFromTrashToCategory(moveFromTrashToCategory);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(true, res.Value);
        }

        [Fact]
        public async Task TestSaveTrashAnnotations()
        {
            Mock<ITrashService> mockTrashServiceService = new Mock<ITrashService>();
            
            SaveTrashAnnotations saveTrashAnnotations = new SaveTrashAnnotations();
            saveTrashAnnotations.id = "5fc0f7f5dd5c73a764eafa38";
            saveTrashAnnotations.fileId = "5fc0f5eedd5c73a764eafa2e";
            saveTrashAnnotations.annotations = "abc";
            mockTrashServiceService.Setup(x => x.SaveTrashAnnotations(saveTrashAnnotations, It.IsAny<int>()));
            var controller = new TrashController(mockTrashServiceService.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.SaveTrashAnnotations(saveTrashAnnotations);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(false, res.Value);
        }
        [Fact]
        public async Task TestSaveWorkbenchAnnotations()
        {
            Mock<IWorkbenchService> mockTrashServiceService = new Mock<IWorkbenchService>();
           
            SaveWorkbenchAnnotations saveWorkbenchAnnotations = new SaveWorkbenchAnnotations();
            saveWorkbenchAnnotations.id = "5fc0f7f5dd5c73a764eafa38";
            saveWorkbenchAnnotations.fileId = "5fc0f5eedd5c73a764eafa2e";
            saveWorkbenchAnnotations.annotations = "abc";
            mockTrashServiceService.Setup(x => x.SaveWorkbenchAnnotations(saveWorkbenchAnnotations, It.IsAny<int>()));
            var controller = new WorkbenchController(mockTrashServiceService.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.SaveWorkbenchAnnotations(saveWorkbenchAnnotations);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(false, res.Value);
        }

        [Fact]
        public async Task TestViewTrashAnnotations()
        {

            Mock<ITrashService> mockTrashService = new Mock<ITrashService>();
            
            ViewTrashAnnotations viewTrashAnnotations = new ViewTrashAnnotations();
            viewTrashAnnotations.id = "5fc0f7f5dd5c73a764eafa38";
            viewTrashAnnotations.fromFileId = "5fc0f5eedd5c73a764eafa2e";

            mockTrashService.Setup(x => x.ViewTrashAnnotations(viewTrashAnnotations, It.IsAny<int>())).ReturnsAsync("abc");
            var controller = new TrashController(mockTrashService.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.ViewTrashAnnotations(viewTrashAnnotations);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("abc", res.Value);
        }

        [Fact]
        public async Task TestViewWorkbenchAnnotations()
        {

            Mock<IWorkbenchService> mockWorkbenchService = new Mock<IWorkbenchService>();
            
            ViewWorkbenchAnnotations viewWorkbenchAnnotations = new ViewWorkbenchAnnotations();
            viewWorkbenchAnnotations.id = "5fc0f7f5dd5c73a764eafa38";
            viewWorkbenchAnnotations.fromFileId = "5fc0f5eedd5c73a764eafa2e";

            mockWorkbenchService.Setup(x => x.ViewWorkbenchAnnotations(viewWorkbenchAnnotations, It.IsAny<int>())).ReturnsAsync("abc");
            var controller = new WorkbenchController(mockWorkbenchService.Object);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.ViewWorkbenchAnnotations(viewWorkbenchAnnotations);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("abc", res.Value);
        }


        [Fact]
        public async Task TestDeleteTrashFile()
        {
            Mock<ITrashService> mockTrashService = new Mock<ITrashService>();
           
            DeleteTrashFile deleteTrashFile = new DeleteTrashFile();
            deleteTrashFile.id = "5fc0f7f5dd5c73a764eafa38";
            deleteTrashFile.fileId = "5fc0f5eedd5c73a764eafa2e";

            mockTrashService.Setup(x => x.DeleteTrashFile(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()));
            var controller = new TrashController(mockTrashService.Object);
            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));



            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.DeleteTrashFile(deleteTrashFile);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(false, res.Value);
        }


        [Fact]
        public async Task TestDeleteWorkbenchFile()
        {
            Mock<IWorkbenchService> mockWorkbenchService = new Mock<IWorkbenchService>();
            
            DeleteTrashFile deleteTrashFile = new DeleteTrashFile();
            deleteTrashFile.id = "5fc0f7f5dd5c73a764eafa38";
            deleteTrashFile.fileId = "5fc0f5eedd5c73a764eafa2e";

            mockWorkbenchService.Setup(x => x.DeleteWorkbenchFile(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()));
            var controller = new WorkbenchController(mockWorkbenchService.Object);
            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));



            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.DeleteWorkbenchFile(deleteTrashFile);

            //Assert
            Assert.NotNull(result);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(false, res.Value);
        }
        [Fact]
        public async Task TestSaveOkResult()
        {
            Mock<IRequestService> mockrequestService = new Mock<IRequestService>();
             Mock<IRainmakerService> mockrainmakerService = new Mock<IRainmakerService>();
            Mock<IConfiguration> mockconfig = new Mock<IConfiguration>();
            Mock<ISettingService> mocksettingService = new Mock<ISettingService>();
            Mock<IKeyStoreService> mockkeyStoreService = new Mock<IKeyStoreService>();
            Mock<ILogger<RequestController>> mocklogger = new Mock<ILogger<RequestController>>();
            Mock<IFtpClient> mockftpClient = new Mock<IFtpClient>();
            Mock<IFileEncryptionFactory> mockfileEncryptionFactory = new Mock<IFileEncryptionFactory>();
            Mock<IByteProService> mockbyteProService = new Mock<IByteProService>();
            Mock<ILosIntegrationService> mocklosIntegration = new Mock<ILosIntegrationService>();
           
            SaveModel saveModel = new SaveModel();
            saveModel.id = "5fc0f7f5dd5c73a764eafa38";
            saveModel.status = "5fc0f7f5dd5c73a764eafa38";
            saveModel.loanApplicationId = 1;
            saveModel.tenantId = 1;
            saveModel.userId = 1;
            saveModel.userName = "5fc0f7f5dd5c73a764eafa38";
            saveModel.request = new Request();
            User user = new User
            {
                userId = 1,
                userName = "rainsoft"
            };
            mockrequestService.Setup(x => x.Save(saveModel, new List<string>()));
            
            mockrainmakerService.Setup(x => x.PostLoanApplication(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(Newtonsoft.Json.JsonConvert.SerializeObject(user));
        
            var controller = new RequestController(mockrequestService.Object, mockrainmakerService.Object, mockconfig.Object, mocksettingService.Object, mockkeyStoreService.Object, mocklogger.Object, mockftpClient.Object, mockfileEncryptionFactory.Object, mockbyteProService.Object, mocklosIntegration.Object);
            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("FirstName")).Returns(new Claim("FirstName", "tunner"));
            httpContext.Setup(m => m.User.FindFirst("LastName")).Returns(new Claim("LastName", "holland"));

            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.Save(saveModel);

            //Assert
            Assert.NotNull(result);
           Assert.IsType<OkResult>(result);
            
        }
        [Fact]
        public async Task TestSaveNotFoundResult()
        {
            Mock<IRequestService> mockrequestService = new Mock<IRequestService>();
            
            Mock<IConfiguration> mockconfig = new Mock<IConfiguration>();
            Mock<ISettingService> mocksettingService = new Mock<ISettingService>();
            Mock<IKeyStoreService> mockkeyStoreService = new Mock<IKeyStoreService>();
            Mock<ILogger<RequestController>> mocklogger = new Mock<ILogger<RequestController>>();
            Mock<IFtpClient> mockftpClient = new Mock<IFtpClient>();
            Mock<IFileEncryptionFactory> mockfileEncryptionFactory = new Mock<IFileEncryptionFactory>();
            Mock<IByteProService> mockbyteProService = new Mock<IByteProService>();
            Mock<ILosIntegrationService> mocklosIntegration = new Mock<ILosIntegrationService>();
            
            SaveModel saveModel = new SaveModel();
            saveModel.id = "5fc0f7f5dd5c73a764eafa38";
            saveModel.status = "5fc0f7f5dd5c73a764eafa38";
            saveModel.loanApplicationId = 1;
            saveModel.tenantId = 1;
            saveModel.userId = 1;
            saveModel.userName = "5fc0f7f5dd5c73a764eafa38";
            saveModel.request = new Request();
            mockrequestService.Setup(x => x.Save(saveModel, new List<string>()));
            var controller = new RequestController(mockrequestService.Object, Mock.Of<IRainmakerService>(), mockconfig.Object, mocksettingService.Object, mockkeyStoreService.Object, mocklogger.Object, mockftpClient.Object, mockfileEncryptionFactory.Object, mockbyteProService.Object, mocklosIntegration.Object);
            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("FirstName")).Returns(new Claim("FirstName", "tunner"));
            httpContext.Setup(m => m.User.FindFirst("LastName")).Returns(new Claim("LastName", "holland"));

            httpContext.SetupGet(x => x.Request).Returns(request.Object);
            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            controller.ControllerContext = context;
            IActionResult result = await controller.Save(saveModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result);

        }

        [Fact]
        public async Task TestSubmitOk()
        {
            Mock<IRequestService> mockrequestService = new Mock<IRequestService>();
            Mock<IConfiguration> mockconfig = new Mock<IConfiguration>();
            Mock<ISettingService> mocksettingService = new Mock<ISettingService>();
            Mock<ILogger<RequestController>> mocklogger = new Mock<ILogger<RequestController>>();

            Mock<ILosIntegrationService> mocklosIntegration = new Mock<ILosIntegrationService>();

            Mock<IFtpClient> mockftpclient = new Mock<IFtpClient>();
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            Mock<IFileEncryptor> mockfileencryptor = new Mock<IFileEncryptor>();
            Mock<IFileEncryptionFactory> mockfileencryptorfacotry = new Mock<IFileEncryptionFactory>(MockBehavior.Strict);
            Mock<IFormFile> mockformFile = new Mock<IFormFile>();
            Mock<IKeyStoreService> mockKeyStoreService = new Mock<IKeyStoreService>();
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
            setting.maxMcuFileSize = 113;
            mockconfiguration.Setup(x => x["KeyStore:Url"]).Returns("http://test.com");
            mockconfiguration.Setup(x => x["File:FtpKey"]).Returns("FtpKey");
            mockconfiguration.Setup(x => x["File:Key"]).Returns("Key");
            mockconfiguration.Setup(x => x["File:Algo"]).Returns("Algo");

            mockKeyStoreService.Setup(x => x.GetFileKey()).ReturnsAsync("this is a very long password");
            mockKeyStoreService.Setup(x => x.GetFtpKey()).ReturnsAsync("this is the long and strong key.");
            mocksettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);

            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns((new MemoryStream(), ""));
            mockftpclient.Setup(x => x.UploadAsync(Path.GetFileName(filePath), It.IsAny<MemoryStream>())).Verifiable();
            mockrequestService.Setup(x => x.Submit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                 It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                 It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<IEnumerable<string>>(), It.IsAny<string>())).ReturnsAsync(" ");

            List<FileViewDto> fileViewDtos = new List<FileViewDto>
            {
                new FileViewDto
                {
                    id = "5fb51519e223e0428d82c41b"
                }
            };

            mockrequestService.Setup(x => x.GetFileByDocId(It.IsAny<FileViewModel>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(fileViewDtos);

            mockrequestService.Setup(x => x.Submit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<IEnumerable<string>>(), It.IsAny<string>())).ReturnsAsync("5f0ede3cce9c4b62509d0dbf");
            Tenant tenant = new Tenant();
            tenant.syncToBytePro = (int)SyncToBytePro.Auto;
            tenant.autoSyncToBytePro = (int)AutoSyncToBytePro.OnSubmit;
            mockByteProService.Setup(x => x.GetTenantSetting(It.IsAny<int>())).ReturnsAsync(tenant);


            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("FirstName")).Returns(new Claim("FirstName", "tunner"));
            httpContext.Setup(m => m.User.FindFirst("LastName")).Returns(new Claim("LastName", "holland"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Connection.RemoteIpAddress).Returns(IPAddress.Parse("127.0.0.1"));
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );

            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            RequestController controller = new RequestController(mockrequestService.Object, Mock.Of<IRainmakerService>(), mockconfig.Object, mocksettingService.Object, mockKeyStoreService.Object, mocklogger.Object, mockftpclient.Object, mockfileencryptorfacotry.Object, mockByteProService.Object, mocklosIntegration.Object);
            controller.ControllerContext = context;
            string id = "5eb25d1fe519051af2eeb72d";
            string requestId = "abc15d1fe456051af2eeb768";
            string docId = "ddd25d1fe456057652eeb72d";
           

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
           


            IActionResult result = await controller.Submit(id, requestId, docId, files);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestSubmitBadRequest()
        {
            Mock<IRequestService> mockrequestService = new Mock<IRequestService>();
            Mock<IConfiguration> mockconfig = new Mock<IConfiguration>();
            Mock<ISettingService> mocksettingService = new Mock<ISettingService>();
            Mock<ILogger<RequestController>> mocklogger = new Mock<ILogger<RequestController>>();
            Mock<IByteProService> mockbyteProService = new Mock<IByteProService>();
            Mock<ILosIntegrationService> mocklosIntegration = new Mock<ILosIntegrationService>();

            Mock<IFtpClient> mockftpclient = new Mock<IFtpClient>();
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            Mock<IFileEncryptor> mockfileencryptor = new Mock<IFileEncryptor>();
            Mock<IFileEncryptionFactory> mockfileencryptorfacotry = new Mock<IFileEncryptionFactory>(MockBehavior.Strict);
            Mock<IFormFile> mockformFile = new Mock<IFormFile>();
            Mock<IKeyStoreService> mockKeyStoreService = new Mock<IKeyStoreService>();
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
            mockconfiguration.Setup(x => x["File:FtpKey"]).Returns("FtpKey");
            mockconfiguration.Setup(x => x["File:Key"]).Returns("Key");
            mockconfiguration.Setup(x => x["File:Algo"]).Returns("Algo");

            mockKeyStoreService.Setup(x => x.GetFileKey()).ReturnsAsync("this is a very long password");
            mockKeyStoreService.Setup(x => x.GetFtpKey()).ReturnsAsync("this is the long and strong key.");
            mocksettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);

            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns((new MemoryStream(), ""));
            mockftpclient.Setup(x => x.UploadAsync(Path.GetFileName(filePath), It.IsAny<MemoryStream>())).Verifiable();

            List<FileViewDto> fileViewDTOs = new List<FileViewDto>();
            FileViewDto fileViewDTO = new FileViewDto();
            fileViewDTO.loanApplicationId = 1;
            fileViewDTO.id = "5f0ede3cce9c4b62509d0dbf";
            fileViewDTOs.Add(fileViewDTO);
            
            Tenant tenant = new Tenant();
            tenant.syncToBytePro = (int)SyncToBytePro.Auto;
            tenant.autoSyncToBytePro = (int)AutoSyncToBytePro.OnSubmit;
            mockByteProService.Setup(x => x.GetTenantSetting(It.IsAny<int>())).ReturnsAsync(tenant);

            

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("FirstName")).Returns(new Claim("FirstName", "tunner"));
            httpContext.Setup(m => m.User.FindFirst("LastName")).Returns(new Claim("LastName", "holland"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Connection.RemoteIpAddress).Returns(IPAddress.Parse("127.0.0.1"));
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );

            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            RequestController controller = new RequestController(mockrequestService.Object, Mock.Of<IRainmakerService>(), mockconfig.Object, mocksettingService.Object, mockKeyStoreService.Object, mocklogger.Object, mockftpclient.Object, mockfileencryptorfacotry.Object, mockbyteProService.Object, mocklosIntegration.Object);
            controller.ControllerContext = context;
            string id = "5eb25d1fe519051af2eeb72d";
            string requestId = "abc15d1fe456051af2eeb768";
            string docId = "ddd25d1fe456057652eeb72d";
           

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
           


            IActionResult result = await controller.Submit(id, requestId, docId, files);
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task TestSubmitBadRequestFileNameSize()
        {
            Mock<IRequestService> mockrequestService = new Mock<IRequestService>();
            Mock<IConfiguration> mockconfig = new Mock<IConfiguration>();
            Mock<ISettingService> mocksettingService = new Mock<ISettingService>();
            Mock<ILogger<RequestController>> mocklogger = new Mock<ILogger<RequestController>>();
            Mock<IByteProService> mockbyteProService = new Mock<IByteProService>();
            Mock<ILosIntegrationService> mocklosIntegration = new Mock<ILosIntegrationService>();

            Mock<IFtpClient> mockftpclient = new Mock<IFtpClient>();
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            Mock<IFileEncryptor> mockfileencryptor = new Mock<IFileEncryptor>();
            Mock<IFileEncryptionFactory> mockfileencryptorfacotry = new Mock<IFileEncryptionFactory>(MockBehavior.Strict);
            Mock<IFormFile> mockformFile = new Mock<IFormFile>();
            Mock<IKeyStoreService> mockKeyStoreService = new Mock<IKeyStoreService>();
            Mock<IByteProService> mockByteProService = new Mock<IByteProService>();

            string filePath = Path.GetTempFileName();
            MemoryStream ms = new MemoryStream();
            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";
            setting.maxFileSize = 15000000;
            setting.maxFileNameSize = 0;
            setting.maxMcuFileSize = 15000000;
            setting.maxFileNameSize = 0;
            setting.allowedExtensions = new string[] { ".tmp" };
            mockconfiguration.Setup(x => x["KeyStore:Url"]).Returns("http://test.com");
            mockconfiguration.Setup(x => x["File:FtpKey"]).Returns("FtpKey");
            mockconfiguration.Setup(x => x["File:Key"]).Returns("Key");
            mockconfiguration.Setup(x => x["File:Algo"]).Returns("Algo");

            mockKeyStoreService.Setup(x => x.GetFileKey()).ReturnsAsync("this is a very long password");
            mockKeyStoreService.Setup(x => x.GetFtpKey()).ReturnsAsync("this is the long and strong key.");
            mocksettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);

            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns((new MemoryStream(), ""));
            mockftpclient.Setup(x => x.UploadAsync(Path.GetFileName(filePath), It.IsAny<MemoryStream>())).Verifiable();

            List<FileViewDto> fileViewDTOs = new List<FileViewDto>();
            FileViewDto fileViewDTO = new FileViewDto();
            fileViewDTO.loanApplicationId = 1;
            fileViewDTO.id = "5f0ede3cce9c4b62509d0dbf";
            fileViewDTOs.Add(fileViewDTO);
            
            Tenant tenant = new Tenant();
            tenant.syncToBytePro = (int)SyncToBytePro.Auto;
            tenant.autoSyncToBytePro = (int)AutoSyncToBytePro.OnSubmit;
            mockByteProService.Setup(x => x.GetTenantSetting(It.IsAny<int>())).ReturnsAsync(tenant);

            

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("FirstName")).Returns(new Claim("FirstName", "tunner"));
            httpContext.Setup(m => m.User.FindFirst("LastName")).Returns(new Claim("LastName", "holland"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Connection.RemoteIpAddress).Returns(IPAddress.Parse("127.0.0.1"));
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );

            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            RequestController controller = new RequestController(mockrequestService.Object, Mock.Of<IRainmakerService>(), mockconfig.Object, mocksettingService.Object, mockKeyStoreService.Object, mocklogger.Object, mockftpclient.Object, mockfileencryptorfacotry.Object, mockbyteProService.Object, mocklosIntegration.Object);
            controller.ControllerContext = context;
            string id = "5eb25d1fe519051af2eeb72d";
            string requestId = "abc15d1fe456051af2eeb768";
            string docId = "ddd25d1fe456057652eeb72d";
            

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
            


            IActionResult result = await controller.Submit(id, requestId, docId, files);
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
 
        [Fact]
        public async Task TestSubmitBadRequestFileExtension()
        {
            Mock<IRequestService> mockrequestService = new Mock<IRequestService>();
            Mock<IConfiguration> mockconfig = new Mock<IConfiguration>();
            Mock<ISettingService> mocksettingService = new Mock<ISettingService>();
            Mock<ILogger<RequestController>> mocklogger = new Mock<ILogger<RequestController>>();
            Mock<IByteProService> mockbyteProService = new Mock<IByteProService>();
            Mock<ILosIntegrationService> mocklosIntegration = new Mock<ILosIntegrationService>();

            Mock<IFtpClient> mockftpclient = new Mock<IFtpClient>();
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            Mock<IFileEncryptor> mockfileencryptor = new Mock<IFileEncryptor>();
            Mock<IFileEncryptionFactory> mockfileencryptorfacotry = new Mock<IFileEncryptionFactory>(MockBehavior.Strict);
            Mock<IFormFile> mockformFile = new Mock  <IFormFile>();
            Mock<IKeyStoreService> mockKeyStoreService = new Mock<IKeyStoreService>();
            Mock<IByteProService> mockByteProService = new Mock<IByteProService>();

            string filePath = Path.GetTempFileName();
            MemoryStream ms = new MemoryStream();
            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";
            setting.maxFileSize = 15000000;
            setting.maxFileNameSize = 255;
            setting.allowedExtensions = new string[] { ".pdf" };
            mockconfiguration.Setup(x => x["KeyStore:Url"]).Returns("http://test.com");
            mockconfiguration.Setup(x => x["File:FtpKey"]).Returns("FtpKey");
            mockconfiguration.Setup(x => x["File:Key"]).Returns("Key");
            mockconfiguration.Setup(x => x["File:Algo"]).Returns("Algo");

            mockKeyStoreService.Setup(x => x.GetFileKey()).ReturnsAsync("this is a very long password");
            mockKeyStoreService.Setup(x => x.GetFtpKey()).ReturnsAsync("this is the long and strong key.");
            mocksettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);

            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns((new MemoryStream(), ""));
            mockftpclient.Setup(x => x.UploadAsync(Path.GetFileName(filePath), It.IsAny<MemoryStream>())).Verifiable();

            List<FileViewDto> fileViewDTOs = new List<FileViewDto>();
            FileViewDto fileViewDTO = new FileViewDto();
            fileViewDTO.loanApplicationId = 1;
            fileViewDTO.id = "5f0ede3cce9c4b62509d0dbf";
            fileViewDTOs.Add(fileViewDTO);
           
            Tenant tenant = new Tenant();
            tenant.syncToBytePro = (int)SyncToBytePro.Auto;
            tenant.autoSyncToBytePro = (int)AutoSyncToBytePro.OnSubmit;
            mockByteProService.Setup(x => x.GetTenantSetting(It.IsAny<int>())).ReturnsAsync(tenant);

            

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("FirstName")).Returns(new Claim("FirstName", "tunner"));
            httpContext.Setup(m => m.User.FindFirst("LastName")).Returns(new Claim("LastName", "holland"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Connection.RemoteIpAddress).Returns(IPAddress.Parse("127.0.0.1"));
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );

            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            RequestController controller = new RequestController(mockrequestService.Object, Mock.Of<IRainmakerService>(), mockconfig.Object, mocksettingService.Object, mockKeyStoreService.Object, mocklogger.Object, mockftpclient.Object, mockfileencryptorfacotry.Object, mockbyteProService.Object, mocklosIntegration.Object);
            controller.ControllerContext = context;
            string id = "5eb25d1fe519051af2eeb72d";
            string requestId = "abc15d1fe456051af2eeb768";
            string docId = "ddd25d1fe456057652eeb72d";
           

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
   
            mockformFile.SetupGet(x => x.FileName).Returns("abc.temp");
            mockformFile.SetupGet(x => x.Length).Returns(255);
            mockformFile.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>())).Returns((Stream stream, CancellationToken token) => ms.CopyToAsync(stream)).Verifiable();


            IActionResult result = await controller.Submit(id, requestId, docId, new List<IFormFile> { mockformFile.Object });
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task TestSubmitBadRequestFileLenght()
        {
            Mock<IRequestService> mockrequestService = new Mock<IRequestService>();
            Mock<IConfiguration> mockconfig = new Mock<IConfiguration>();
            Mock<ISettingService> mocksettingService = new Mock<ISettingService>();
            Mock<ILogger<RequestController>> mocklogger = new Mock<ILogger<RequestController>>();
            Mock<IByteProService> mockbyteProService = new Mock<IByteProService>();
            Mock<ILosIntegrationService> mocklosIntegration = new Mock<ILosIntegrationService>();

            Mock<IFtpClient> mockftpclient = new Mock<IFtpClient>();
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            Mock<IFileEncryptor> mockfileencryptor = new Mock<IFileEncryptor>();
            Mock<IFileEncryptionFactory> mockfileencryptorfacotry = new Mock<IFileEncryptionFactory>(MockBehavior.Strict);
            Mock<IFormFile> mockformFile = new Mock<IFormFile>();
            Mock<IKeyStoreService> mockKeyStoreService = new Mock<IKeyStoreService>();
            Mock<IByteProService> mockByteProService = new Mock<IByteProService>();

            string filePath = Path.GetTempFileName();
            MemoryStream ms = new MemoryStream();
            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";
            setting.maxFileSize = 15000000;
            setting.maxFileNameSize = 0;
            setting.allowedExtensions = new string[] { ".tmp" };
            mockconfiguration.Setup(x => x["KeyStore:Url"]).Returns("http://test.com");
            mockconfiguration.Setup(x => x["File:FtpKey"]).Returns("FtpKey");
            mockconfiguration.Setup(x => x["File:Key"]).Returns("Key");
            mockconfiguration.Setup(x => x["File:Algo"]).Returns("Algo");

            mockKeyStoreService.Setup(x => x.GetFileKey()).ReturnsAsync("this is a very long password");
            mockKeyStoreService.Setup(x => x.GetFtpKey()).ReturnsAsync("this is the long and strong key.");
            mocksettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);

            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns((new MemoryStream(), ""));
            mockftpclient.Setup(x => x.UploadAsync(Path.GetFileName(filePath), It.IsAny<MemoryStream>())).Verifiable();

            List<FileViewDto> fileViewDTOs = new List<FileViewDto>();
            FileViewDto fileViewDTO = new FileViewDto();
            fileViewDTO.loanApplicationId = 1;
            fileViewDTO.id = "5f0ede3cce9c4b62509d0dbf";
            fileViewDTOs.Add(fileViewDTO);
            
            Tenant tenant = new Tenant();
            tenant.syncToBytePro = (int)SyncToBytePro.Auto;
            tenant.autoSyncToBytePro = (int)AutoSyncToBytePro.OnSubmit;
            mockByteProService.Setup(x => x.GetTenantSetting(It.IsAny<int>())).ReturnsAsync(tenant);

            

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("FirstName")).Returns(new Claim("FirstName", "tunner"));
            httpContext.Setup(m => m.User.FindFirst("LastName")).Returns(new Claim("LastName", "holland"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Connection.RemoteIpAddress).Returns(IPAddress.Parse("127.0.0.1"));
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );

            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            RequestController controller = new RequestController(mockrequestService.Object, Mock.Of<IRainmakerService>(), mockconfig.Object, mocksettingService.Object, mockKeyStoreService.Object, mocklogger.Object, mockftpclient.Object, mockfileencryptorfacotry.Object, mockbyteProService.Object, mocklosIntegration.Object);
            controller.ControllerContext = context;
            string id = "5eb25d1fe519051af2eeb72d";
            string requestId = "abc15d1fe456051af2eeb768";
            string docId = "ddd25d1fe456057652eeb72d";
           

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

            mockformFile.SetupGet(x => x.Length).Returns(0);

            mockformFile.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>())).Returns((Stream stream, CancellationToken token) => ms.CopyToAsync(stream)).Verifiable();


            IActionResult result = await controller.Submit(id, requestId, docId, new List<IFormFile> { mockformFile.Object });
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public async Task TestSubmitDocQueryEmpty()
        {
            Mock<IRequestService> mockrequestService = new Mock<IRequestService>();
            Mock<IConfiguration> mockconfig = new Mock<IConfiguration>();
            Mock<ISettingService> mocksettingService = new Mock<ISettingService>();
            Mock<ILogger<RequestController>> mocklogger = new Mock<ILogger<RequestController>>();

            Mock<ILosIntegrationService> mocklosIntegration = new Mock<ILosIntegrationService>();

            Mock<IFtpClient> mockftpclient = new Mock<IFtpClient>();
            Mock<IConfiguration> mockconfiguration = new Mock<IConfiguration>();
            Mock<IFileEncryptor> mockfileencryptor = new Mock<IFileEncryptor>();
            Mock<IFileEncryptionFactory> mockfileencryptorfacotry = new Mock<IFileEncryptionFactory>(MockBehavior.Strict);
            Mock<IFormFile> mockformFile = new Mock<IFormFile>();
            Mock<IKeyStoreService> mockKeyStoreService = new Mock<IKeyStoreService>();
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
            setting.maxMcuFileSize = 113;
            mockconfiguration.Setup(x => x["KeyStore:Url"]).Returns("http://test.com");
            mockconfiguration.Setup(x => x["File:FtpKey"]).Returns("FtpKey");
            mockconfiguration.Setup(x => x["File:Key"]).Returns("Key");
            mockconfiguration.Setup(x => x["File:Algo"]).Returns("Algo");

            mockKeyStoreService.Setup(x => x.GetFileKey()).ReturnsAsync("this is a very long password");
            mockKeyStoreService.Setup(x => x.GetFtpKey()).ReturnsAsync("this is the long and strong key.");
            mocksettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);

            mockfileencryptorfacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockfileencryptor.Object);
            mockfileencryptor.Setup(x => x.EncryptFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns((new MemoryStream(), ""));
            mockftpclient.Setup(x => x.UploadAsync(Path.GetFileName(filePath), It.IsAny<MemoryStream>())).Verifiable();
            mockrequestService.Setup(x => x.Submit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                 It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                 It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<IEnumerable<string>>(), It.IsAny<string>())).ReturnsAsync(" ");

            List<FileViewDto> fileViewDtos = new List<FileViewDto>
            {
                new FileViewDto
                {
                    id = "5fb51519e223e0428d82c41b"
                }
            };

            mockrequestService.Setup(x => x.GetFileByDocId(It.IsAny<FileViewModel>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(fileViewDtos);

            mockrequestService.Setup(x => x.Submit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<IEnumerable<string>>(), It.IsAny<string>())).ReturnsAsync(string.Empty);
            Tenant tenant = new Tenant();
            tenant.syncToBytePro = (int)SyncToBytePro.Auto;
            tenant.autoSyncToBytePro = (int)AutoSyncToBytePro.OnSubmit;
            mockByteProService.Setup(x => x.GetTenantSetting(It.IsAny<int>())).ReturnsAsync(tenant);


            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("FirstName")).Returns(new Claim("FirstName", "tunner"));
            httpContext.Setup(m => m.User.FindFirst("LastName")).Returns(new Claim("LastName", "holland"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.SetupGet(x => x.Connection.RemoteIpAddress).Returns(IPAddress.Parse("127.0.0.1"));
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );

            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            RequestController controller = new RequestController(mockrequestService.Object, Mock.Of<IRainmakerService>(), mockconfig.Object, mocksettingService.Object, mockKeyStoreService.Object, mocklogger.Object, mockftpclient.Object, mockfileencryptorfacotry.Object, mockByteProService.Object, mocklosIntegration.Object);
            controller.ControllerContext = context;
            string id = "5eb25d1fe519051af2eeb72d";
            string requestId = "abc15d1fe456051af2eeb768";
            string docId = "ddd25d1fe456057652eeb72d";
           

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


            IActionResult result = await controller.Submit(id, requestId, docId, files);
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
