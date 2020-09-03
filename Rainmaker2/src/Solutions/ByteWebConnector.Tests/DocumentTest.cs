using ByteWebConnector.API.Controllers;
using ByteWebConnector.API.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Rainmaker.Service;
using RainMaker.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ByteWebConnector.API.Models.ClientModels.Document;
using DeleteRequest = ByteWebConnector.API.Models.Document.DeleteRequest;
using ByteWebConnector.API.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Net.Http;
using Moq.Protected;
using System.Threading;
using System.Net;
using ByteWebConnector.API.Models.Document;
using RainMaker.Service;
using System.IO;
using HttpWebAdapters;

namespace ByteWebConnector.Tests
{
    public class DocumentTest
    {
        [Fact]
        public  async  Task  TestSendDocumentController()
        {
            //Arrange
            Mock<ILoanApplicationService> mockLoanApplicationService = new Mock<ILoanApplicationService>();

            LoanApplication loanApplication = new LoanApplication();
            loanApplication.Id = 1;
            loanApplication.EncompassNumber = "abc";

            List<LoanApplication> loanApplications = new List<LoanApplication>();
            loanApplications.Add(loanApplication);

            mockLoanApplicationService.Setup(x => x.GetLoanApplicationWithDetails(It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<LoanApplicationService.RelatedEntity?>())).Returns(loanApplications);
            string[] lstMediaTypesToBeWrappedInPdf = new string[1];
            lstMediaTypesToBeWrappedInPdf[0] = "image/jpeg";

            var mockConfiguration = new Mock<IConfiguration>();

            var configurationSection = new Mock<IConfigurationSection>();
            //configurationSection.Setup(a => a.Get<string[]>()).Returns(lstMediaTypesToBeWrappedInPdf);

            //mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "MediaTypesToBeWrappedInPdf"))).Returns(configurationSection.Object);
            //var controller = new DocumentController(mockLoanApplicationService.Object, null, null, mockConfiguration.Object, Mock.Of<ILogger<DocumentController>>());

            //Act
            SendDocumentRequest sendDocumentRequest = new SendDocumentRequest();
            sendDocumentRequest.LoanApplicationId = 1;
            sendDocumentRequest.MediaType = "image/jpeg";
            sendDocumentRequest.DocumentExension = "jpeg";
            //ApiResponse result = controller.SendDocument(sendDocumentRequest);

            //Assert
            //Assert.NotNull(result);
        }
        [Fact]
        public async Task TestDeleteController()
        {
            //Arrange
          
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://localhost:5031");
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Bearer")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var userProfile = new UserProfile();
            userProfile.Id = 1;
            userProfile.UserName = "rainsoft";

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            // ARRANGE
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
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
                   Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(userProfile), Encoding.UTF8, "application/json")
               })
               .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var controller = new DocumentController(httpClient, mockConfiguration.Object, Mock.Of<ILogger<DocumentController>>(),null ,null );

            controller.ControllerContext = context;

            //Act
            DeleteRequest deleteRequest = new DeleteRequest();
            deleteRequest.GetLosModel().ToJsonString();
            IActionResult result = await controller.Delete(deleteRequest);

            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task TestDeleteBadRequestController()
        {
            //Arrange

            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://localhost:5031");
            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Bearer")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var userProfile = new UserProfile();
            userProfile.Id = 1;
            userProfile.UserName = "rainsoft";

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            // ARRANGE
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
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
                   StatusCode = HttpStatusCode.BadRequest,
                   Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(userProfile), Encoding.UTF8, "application/json")
               })
               .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var controller = new DocumentController(httpClient, mockConfiguration.Object, Mock.Of<ILogger<DocumentController>>(),null,null);

            controller.ControllerContext = context;

            //Act
            DeleteRequest deleteRequest = new DeleteRequest();
            deleteRequest.GetLosModel().ToJsonString();
            IActionResult result = await controller.Delete(deleteRequest);

            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task TestDocumentAddedController()
        {
            //Arrange
            Mock<ICommonService> mockCommonService = new Mock<ICommonService>();
            mockCommonService.Setup(x => x.GetSettingValueByKeyAsync<string>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<string>())).ReturnsAsync("http://localhost:8080/");
           
            //var controller = new DocumentController(null, mockCommonService.Object, null, null, Mock.Of<ILogger<DocumentController>>());
            //Act
            DocumentAddedRequest documentAddedRequest = new DocumentAddedRequest();
            documentAddedRequest.FileDataId = 1;
            
            //var expected = "response content";
            //var expectedBytes = Encoding.UTF8.GetBytes(expected);
            //var responseStream = new MemoryStream();
            //responseStream.Write(expectedBytes, 0, expectedBytes.Length);
            //responseStream.Seek(0, SeekOrigin.Begin);

            //var response = new Mock<IHttpWebResponse>();
            //response.Setup(c => c.GetResponseStream()).Returns(responseStream);

            //var request = new Mock<IHttpWebRequest>();
            //request.Setup(c => c.GetResponse()).Returns(response.Object);

            //var factory = new Mock<IHttpWebRequestFactory>();
            //factory.Setup(c => c.Create(It.IsAny<string>()))
            //    .Returns(request.Object);

            //// act
            //var actualRequest = factory.Object.Create("http://www.google.com");
            //actualRequest.Method = WebRequestMethods.Http.Get;

            //string actual;

            //using (var httpWebResponse = actualRequest.GetResponse())
            //{
            //    using (var streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            //    {
            //        actual = streamReader.ReadToEnd();
            //    }
            //}

            //IActionResult result = await controller.DocumentAdded(documentAddedRequest);
            //Assert
            //Assert.NotNull(result);
        }
    }
}
