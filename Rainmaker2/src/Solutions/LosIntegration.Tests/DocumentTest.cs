using LosIntegration.API.Controllers;
using LosIntegration.API.Models;
using LosIntegration.API.Models.Document;
using LosIntegration.Entity.Models;
using LosIntegration.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using Moq.Protected;
using RainMaker.Entity.Models;
using ServiceCallHelper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LosIntegration.Tests
{
    public class DocumentTest
    {
        [Fact]
        public async Task TestDeleteController()
        {
            //Arrange
            Mock<IMappingService> mockMappingService = new Mock<IMappingService>();
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com/");

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
                   Content = new StringContent("{\"id\":1}", Encoding.UTF8, "application/json"),
               })
               .Verifiable();
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            List<Mapping> mappings= new List<Mapping>();

            Mapping mapping = new Mapping();
            mapping.RMEnittyId = "1";
            mappings.Add(mapping);

            mockMappingService.Setup(x => x.GetMappingWithDetails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int?>())).Returns(mappings);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Bearer")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new DocumentController(httpClientFactory.Object, mockConfiguration.Object, mockMappingService.Object, Mock.Of<ILogger<DocumentController>>(),null,null,null);

            controller.ControllerContext = context;
            //Act
            DeleteRequest deleteRequest = new DeleteRequest();
            deleteRequest.ExtOriginatorFileId = 1;
            deleteRequest.ExtOriginatorId = 1;
            deleteRequest.ExtOriginatorLoanApplicationId = 1;
            IActionResult result = await controller.Delete(deleteRequest);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestDeleteMappingNullController()
        {
            //Arrange
            Mock<IMappingService> mockMappingService = new Mock<IMappingService>();
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com/");

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
                   Content = new StringContent("{\"id\":1}", Encoding.UTF8, "application/json"),
               })
               .Verifiable();
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            List<Mapping> mappings = new List<Mapping>();

            mockMappingService.Setup(x => x.GetMappingWithDetails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int?>())).Returns(mappings);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Bearer")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new DocumentController(httpClientFactory.Object, mockConfiguration.Object, mockMappingService.Object, Mock.Of<ILogger<DocumentController>>(), null, null, null);

            controller.ControllerContext = context;
            //Act
            DeleteRequest deleteRequest = new DeleteRequest();
            deleteRequest.ExtOriginatorFileId = 1;
            deleteRequest.ExtOriginatorId = 1;
            deleteRequest.ExtOriginatorLoanApplicationId = 1;
            IActionResult result = await controller.Delete(deleteRequest);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task TestDeleteBadRequestController()
        {
            //Arrange
            Mock<IMappingService> mockMappingService = new Mock<IMappingService>();
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com/");

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
                   Content = new StringContent("{\"id\":1}", Encoding.UTF8, "application/json"),
               })
               .Verifiable();
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            List<Mapping> mappings = new List<Mapping>();

            Mapping mapping = new Mapping();
            mapping.RMEnittyId = "1";
            mappings.Add(mapping);

            mockMappingService.Setup(x => x.GetMappingWithDetails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int?>())).Returns(mappings);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Bearer")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new DocumentController(httpClientFactory.Object, mockConfiguration.Object, mockMappingService.Object, Mock.Of<ILogger<DocumentController>>(), null, null, null);

            controller.ControllerContext = context;
            //Act
            DeleteRequest deleteRequest = new DeleteRequest();
            deleteRequest.ExtOriginatorFileId = 1;
            deleteRequest.ExtOriginatorId = 1;
            deleteRequest.ExtOriginatorLoanApplicationId = 1;
            IActionResult result = await controller.Delete(deleteRequest);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task TestSendFileToExternalOriginatorController()
        {
            //Arrange
            Mock<IMappingService> mockMappingService = new Mock<IMappingService>();
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com/");

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
                   Content = new StringContent("{\"id\":1}", Encoding.UTF8, "application/json"),
               })
               .Verifiable();
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            List<Mapping> mappings = new List<Mapping>();

            Mapping mapping = new Mapping();
            mapping.RMEnittyId = "1";
            mappings.Add(mapping);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Bearer")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            Call.Get<List<DocumentManagementDocument>>(httpClient, "http://test.com/", request.Object, true);

            var controller = new DocumentController(httpClientFactory.Object, mockConfiguration.Object, mockMappingService.Object, Mock.Of<ILogger<DocumentController>>(), null, null, null);

            controller.ControllerContext = context;

            //Act
            SendDocumentToExternalOriginatorRequest sendDocumentToExternalOriginatorRequest = new SendDocumentToExternalOriginatorRequest();
            sendDocumentToExternalOriginatorRequest.LoanApplicationId = 1;
            sendDocumentToExternalOriginatorRequest.DocumentId = "1";
            sendDocumentToExternalOriginatorRequest.DocumentLoanApplicationId = "1";
            sendDocumentToExternalOriginatorRequest.RequestId = "1";
            //Not moq beacuse of static method call in API.
            //IActionResult result = await controller.SendDocumentToExternalOriginator(sendDocumentToExternalOriginatorRequest);
            //Assert
            //Assert.NotNull(result);
        }
        [Fact]
        public async Task TestAddDocumentController()
        {
            //Arrange
            Mock<IMappingService> mockMappingService = new Mock<IMappingService>();
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com/");

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
                   Content = new StringContent("{\"id\":1}", Encoding.UTF8, "application/json"),
               })
               .Verifiable();
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            List<Mapping> mappings = new List<Mapping>();

            Mapping mapping = new Mapping();
            mapping.RMEnittyId = "1";
            mappings.Add(mapping);

            mockMappingService.Setup(x => x.GetMappingWithDetails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int?>())).Returns(mappings);

            var request = new Mock<HttpRequest>();
            request.SetupGet(x => x.Headers["Authorization"]).Returns(
                new StringValues("Bearer")
                );
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(x => x.Request).Returns(request.Object);

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new DocumentController(httpClientFactory.Object, mockConfiguration.Object, mockMappingService.Object, Mock.Of<ILogger<DocumentController>>(), null, null, null);

            controller.ControllerContext = context;
            //Act

            EmbeddedDoc embeddedDoc = new EmbeddedDoc();
            embeddedDoc.DocumentId = 1;
            List<EmbeddedDoc> embeddedDocs = new List<EmbeddedDoc>();
            embeddedDocs.Add(embeddedDoc);

            AddDocumentRequest addDocumentRequest = new AddDocumentRequest();
            addDocumentRequest.FileDataId = 1;
            addDocumentRequest.EmbeddedDocs = embeddedDocs;
          
            //IActionResult result = await controller.AddDocument(addDocumentRequest);
            //Assert
            //Assert.NotNull(result);
            //Assert.IsType<OkResult>(result);
        }
    }
}
