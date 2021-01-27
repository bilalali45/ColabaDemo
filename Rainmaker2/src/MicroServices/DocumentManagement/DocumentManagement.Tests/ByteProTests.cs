﻿using DocumentManagement.API.Controllers;
using DocumentManagement.Entity;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static DocumentManagement.Model.Template;

namespace DocumentManagement.Tests
{
    public class ByteProTests
    {
        [Fact]
        public async Task TestViewController()
        {

            Mock<IByteProService> mock = new Mock<IByteProService>();
            Mock<ISettingService> mockSettingService = new Mock<ISettingService>();
            Mock<IFtpClient> mockFtpClient = new Mock<IFtpClient>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            Mock<IKeyStoreService> mockKeyStoreService = new Mock<IKeyStoreService>();

            var mockFileEcryptor = new Mock<IFileEncryptor>();
            Mock<IFileEncryptionFactory> mockFileEncryptorFacotry = new Mock<IFileEncryptionFactory>(MockBehavior.Strict);

            FileViewDto fileViewDTO = new FileViewDto();
            fileViewDTO.serverName = "a69ad17f-7505-492d-a92e-f32967cecff8.enc";
            fileViewDTO.encryptionKey = "FileKey";
            fileViewDTO.encryptionAlgorithm = "AES";
            fileViewDTO.clientName = "NET Unit Testing.docx";
            fileViewDTO.contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

            Setting setting = new Setting();
            setting.ftpServer = "ftp://rsserver1/Product2.0/BorrowerDocument";
            setting.ftpUser = "ftpuser";
            setting.ftpPassword = "HRp0cc2dbNNWxpm3kjp8aQ==";

            mock.Setup(x => x.View(It.IsAny<AdminFileViewModel>(), It.IsAny<int>())).ReturnsAsync(fileViewDTO);
            mockSettingService.Setup(x => x.GetSetting()).ReturnsAsync(setting);
            mockFtpClient.Setup(x => x.Setup(setting.ftpServer, setting.ftpUser, setting.ftpPassword)).Verifiable();
            mockFtpClient.Setup(x => x.DownloadAsync(fileViewDTO.serverName, It.IsAny<string>())).Verifiable();

            mockKeyStoreService.Setup(x => x.GetFileKey()).ReturnsAsync("this is a very long password");
            mockKeyStoreService.Setup(x => x.GetFtpKey()).ReturnsAsync("this is the long and strong key.");
            mockFileEcryptor.Setup(x => x.DecrypeFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new MemoryStream());

            mockFileEncryptorFacotry.Setup(x => x.GetEncryptor(It.IsAny<string>())).Returns(mockFileEcryptor.Object);
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            // Act  
            ByteProController controller = new ByteProController(Mock.Of<IDocumentService>(), mockFileEncryptorFacotry.Object, mockFtpClient.Object, mockSettingService.Object, mockKeyStoreService.Object, Mock.Of<ILogger<DocumentController>>(), mock.Object,null, null);
            controller.ControllerContext = context;
            View view = new View();
            view.id = "5eb25d1fe519051af2eeb72d";
            view.requestId = "abc15d1fe456051af2eeb768";
            view.docId = "ddd25d1fe456057652eeb72d";
            view.fileId = "5eeb19874db574137c1fabde";
            IActionResult result = await controller.View(view);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<FileStreamResult>(result);
        }

        [Fact]
        public async Task TestGetDocumentsController()
        {
            //Arrange
            Mock<IAdminDashboardService> mock = new Mock<IAdminDashboardService>();
            List<AdminDashboardDto> list = new List<AdminDashboardDto>() { { new AdminDashboardDto() { docId = "1" } } };

            mock.Setup(x => x.GetDocument(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<int>())).ReturnsAsync(list);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var admindashboardController = new ByteProController(Mock.Of<IDocumentService>(), Mock.Of<IFileEncryptionFactory>(),null,null,null, Mock.Of<ILogger<DocumentController>>(),null,mock.Object,null);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            admindashboardController.ControllerContext = context;

            GetDocuments moGetDocuments = new GetDocuments();
            moGetDocuments.loanApplicationId = 1;
            moGetDocuments.pending = true;
            //Act
            IActionResult result = await admindashboardController.GetDocuments(moGetDocuments);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as List<AdminDashboardDto>;
            Assert.Single(content);
            Assert.Equal("1", content[0].docId);
        }

        [Fact]
        public async Task TestGetCategoryDocumentController()
        {
            //Arrange
            Mock<ITemplateService> mock = new Mock<ITemplateService>();
            List<CategoryDocumentTypeModel> list = new List<CategoryDocumentTypeModel>() { { new CategoryDocumentTypeModel() { catId = "5ebabbfb3845be1cf1edce50" } } };

            mock.Setup(x => x.GetCategoryDocument(It.IsAny<int>())).ReturnsAsync(list);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var templateController = new ByteProController(null,null,null,null,null,Mock.Of<ILogger<DocumentController>>(),null,null, mock.Object);
            templateController.ControllerContext = context;

            //Act
            IActionResult result = await templateController.GetCategoryDocument();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as List<CategoryDocumentTypeModel>;
            Assert.Single(content);
            Assert.Equal("5ebabbfb3845be1cf1edce50", content[0].catId);
        }
        [Fact]
        public async Task TestUpdateByteProStatus()
        {
            //Arrange
            Mock<IDocumentService> mock = new Mock<IDocumentService>();

            mock.Setup(x => x.UpdateByteProStatus(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var admindashboardController = new ByteProController(mock.Object, Mock.Of<IFileEncryptionFactory>(), null, null, null, Mock.Of<ILogger<DocumentController>>(), null, null, null);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            admindashboardController.ControllerContext = context;

            //Act
            IActionResult result = await admindashboardController.UpdateByteProStatus(new UpdateByteProStatus());
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestUpdateByteProStatusNotFound()
        {
            //Arrange
            Mock<IDocumentService> mock = new Mock<IDocumentService>();

            mock.Setup(x => x.UpdateByteProStatus(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(false);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("TenantId")).Returns(new Claim("TenantId", "1"));
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var admindashboardController = new ByteProController(mock.Object, Mock.Of<IFileEncryptionFactory>(), null, null, null, Mock.Of<ILogger<DocumentController>>(), null, null, null);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            admindashboardController.ControllerContext = context;

            //Act
            IActionResult result = await admindashboardController.UpdateByteProStatus(new UpdateByteProStatus());
            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
