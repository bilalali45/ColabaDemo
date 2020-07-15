using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DocumentManagement.API.Controllers;
using DocumentManagement.Entity;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq.Protected;
using Xunit;
using LoanApplication = DocumentManagement.Model.LoanApplication;

namespace DocumentManagement.Tests
{
    public class RequestTest
    {
        [Fact]
        public async Task TestSaveControllerTrue()
        {
            //Arrange
            Mock<IRequestService> mock = new Mock<IRequestService>();
            Mock<IRainmakerService> mockRainMock = new Mock<IRainmakerService>();
            mock.Setup(x => x.Save(It.IsAny<Model.LoanApplication>(), It.IsAny<bool>())).ReturnsAsync(true);
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("FirstName")).Returns(new Claim("FirstName", "Danish"));
            httpContext.Setup(m => m.User.FindFirst("LastName")).Returns(new Claim("LastName", "Faiz"));
            httpContext.SetupGet(x => x.Connection.RemoteIpAddress).Returns(IPAddress.Parse("127.0.0.1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            mockRainMock.Setup(x => x.PostLoanApplication(It.IsAny<int>(),It.IsAny<bool>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync("{userId:59,userName:'Melissa Merritt'}");

            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );

            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var controller = new RequestController(mock.Object, mockRainMock.Object);

            controller.ControllerContext = context;

            //Act
            LoanApplication loanApplication = new LoanApplication();
            loanApplication.id = "5f0e8d014e72f52edcff3885";
            loanApplication.loanApplicationId = 14;
            loanApplication.tenantId = 1;
            loanApplication.userId = 59;
            loanApplication.userName = "Melissa Merritt";
            loanApplication.requests = new List<Request>();

            Request requestModel = new Request();
            requestModel.id = "5f0ede3cce9c4b62509d0dc0";
            requestModel.userId = 3842;
            requestModel.userName = "Danish Faiz";
            requestModel.message = "Hi Mark";
            requestModel.createdOn = DateTime.UtcNow;
            requestModel.status = DocumentStatus.BorrowerTodo;
            requestModel.documents = new List<RequestDocument>();

            RequestDocument document = new RequestDocument();
            document.id = "5f0ede3cce9c4b62509d0dc1";
            document.activityId = "5f0ede3cce9c4b62509d0dc2";
            document.status = DocumentStatus.BorrowerTodo;
            document.typeId = "5eb257a3e519051af2eeb624";
            document.displayName = "W2 2020";
            document.message = "please upload salary slip";
            document.files = new List<RequestFile>();

            RequestFile requestFile = new RequestFile();
            requestFile.id = "abc15d1fe456051af2eeb768";
            requestFile.clientName = "3e06ed7f-0620-42f2-b6f5-e7b8ee1f2778.pdf";
            requestFile.serverName = "0c550bb7-7e4b-4384-98eb-5264d9411737.enc";
            requestFile.fileUploadedOn = DateTime.UtcNow;
            requestFile.size = 1904942;
            requestFile.encryptionKey = "FileKey";
            requestFile.encryptionAlgorithm = "AES";
            requestFile.order = 0;
            requestFile.mcuName = "abc";
            requestFile.contentType = "application/pdf";
            requestFile.status = FileStatus.SubmittedToMcu;
            requestFile.byteProStatus = "Active";
            
            document.files.Add(requestFile);

            requestModel.documents.Add(document);

            loanApplication.requests.Add(requestModel);

            IActionResult result = await controller.Save(loanApplication,false);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestSaveControllerFalse()
        {
            //Arrange
            Mock<IRequestService> mock = new Mock<IRequestService>();
            Mock<IRainmakerService> mockRainMock = new Mock<IRainmakerService>();
            mock.Setup(x => x.Save(It.IsAny<Model.LoanApplication>(), It.IsAny<bool>())).ReturnsAsync(false);
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));
            httpContext.Setup(m => m.User.FindFirst("FirstName")).Returns(new Claim("FirstName", "Danish"));
            httpContext.Setup(m => m.User.FindFirst("LastName")).Returns(new Claim("LastName", "Faiz"));
            httpContext.SetupGet(x => x.Connection.RemoteIpAddress).Returns(IPAddress.Parse("127.0.0.1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            mockRainMock.Setup(x => x.PostLoanApplication(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync("");

            var request = new Mock<HttpRequest>();

            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Test")
                );

            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var controller = new RequestController(mock.Object, mockRainMock.Object);

            controller.ControllerContext = context;

            //Act
            LoanApplication loanApplication = new LoanApplication();
            loanApplication.id = "5f0e8d014e72f52edcff3885";
            loanApplication.loanApplicationId = 14;
            loanApplication.tenantId = 1;
            loanApplication.userId = 59;
            loanApplication.userName = "Melissa Merritt";
            loanApplication.requests = new List<Request>();

            Request requestModel = new Request();
            requestModel.id = "5f0ede3cce9c4b62509d0dc0";
            requestModel.userId = 3842;
            requestModel.userName = "Danish Faiz";
            requestModel.message = "Hi Mark";
            requestModel.createdOn = DateTime.UtcNow;
            requestModel.status = DocumentStatus.BorrowerTodo;
            requestModel.documents = new List<RequestDocument>();

            RequestDocument document = new RequestDocument();
            document.id = "5f0ede3cce9c4b62509d0dc1";
            document.activityId = "5f0ede3cce9c4b62509d0dc2";
            document.status = DocumentStatus.BorrowerTodo;
            document.typeId = "5eb257a3e519051af2eeb624";
            document.displayName = "W2 2020";
            document.message = "please upload salary slip";
            document.files = new List<RequestFile>();

            RequestFile requestFile = new RequestFile();
            requestFile.id = "abc15d1fe456051af2eeb768";
            requestFile.clientName = "3e06ed7f-0620-42f2-b6f5-e7b8ee1f2778.pdf";
            requestFile.serverName = "0c550bb7-7e4b-4384-98eb-5264d9411737.enc";
            requestFile.fileUploadedOn = DateTime.UtcNow;
            requestFile.size = 1904942;
            requestFile.encryptionKey = "FileKey";
            requestFile.encryptionAlgorithm = "AES";
            requestFile.order = 0;
            requestFile.mcuName = "abc";
            requestFile.contentType = "application/pdf";
            requestFile.status = FileStatus.SubmittedToMcu;
            requestFile.byteProStatus = "Active";

            document.files.Add(requestFile);

            requestModel.documents.Add(document);

            loanApplication.requests.Add(requestModel);

            IActionResult result = await controller.Save(loanApplication, false);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
