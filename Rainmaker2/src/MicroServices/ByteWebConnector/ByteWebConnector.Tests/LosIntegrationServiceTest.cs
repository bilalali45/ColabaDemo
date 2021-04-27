using ByteWebConnector.Model.Models.ServiceResponseModels.ByteApi;
using ByteWebConnector.Service.InternalServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ByteWebConnector.Tests
{
    public class LosIntegrationServiceTest
    {
        [Fact]
        public async Task DocumentDeleteService()
        {
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            HttpContext httpContext = new DefaultHttpContext();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            ServiceCallHelper.AppContext.Configure(contextAccessorMock.Object);
            contextAccessorMock.Setup(_ => _.HttpContext).Returns(httpContext);

            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/bytewebconnector/document/senddocument", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("")
            });
            var handlerMock = new Mock<TestMessageHandler>(MockBehavior.Strict);
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
                    Content = new StringContent("", Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
            httpClientFactory.Setup(clientFactory => clientFactory.CreateClient(It.IsAny<string>())).Returns(httpClient);
            ILosIntegrationService byteWebConnectorSdkService = new LosIntegrationService(contextAccessorMock.Object,httpClient, mockConfiguration.Object);
            bool result = await byteWebConnectorSdkService.DocumentDelete("byte");
            Assert.IsType<bool>(result);
        }
        [Fact]
        public async Task DocumentAddDocumentService()
        {
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            HttpContext httpContext = new DefaultHttpContext();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            ServiceCallHelper.AppContext.Configure(contextAccessorMock.Object);
            contextAccessorMock.Setup(_ => _.HttpContext).Returns(httpContext);

            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/losintegration/document/adddocument", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("")
            });
            var handlerMock = new Mock<TestMessageHandler>(MockBehavior.Strict);
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
                    Content = new StringContent("", Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
            httpClientFactory.Setup(clientFactory => clientFactory.CreateClient(It.IsAny<string>())).Returns(httpClient);
            ILosIntegrationService byteWebConnectorSdkService = new LosIntegrationService(contextAccessorMock.Object, httpClient, mockConfiguration.Object);
            List<EmbeddedDoc> embeddedDocs = new List<EmbeddedDoc>();
            bool result = await byteWebConnectorSdkService.DocumentAddDocument(1, embeddedDocs);
            Assert.IsType<bool>(result);
        }
    }
}
