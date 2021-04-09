using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LosIntegration.Data;
using LosIntegration.Entity.Models;
using LosIntegration.Model.Model.ServiceResponseModels;
using LosIntegration.Service;
using LosIntegration.Service.Interface;
using LosIntegration.Service.InternalServices;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;
using Extensions.ExtensionClasses;
using LosIntegration.Model.Model.ServiceRequestModels.ByteWebConnector;
using LosIntegration.Model.Model.ServiceRequestModels.Document;
using LosIntegration.Model.Model.ServiceResponseModels.ByteWebConnector;
using LosIntegration.Model.Model.ServiceResponseModels.Rainmaker;

namespace LosIntegration.Tests
{
    public class BWCServiceTest
    {
        [Fact]
        public async Task TestSendDocumentService()
        {
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IMilestoneService> milestoneService = new Mock<IMilestoneService>();
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
            IByteWebConnectorService byteWebConnectorService = new ByteWebConnectorService(Mock.Of<ILogger<ByteWebConnectorService>>(), httpClient, mockConfiguration.Object);
            SendDocumentRequest sendDocumentRequest = new SendDocumentRequest();
            ServiceCallHelper.CallResponse<Model.Model.ServiceResponseModels.ByteWebConnector.SendSdkDocumentResponse> callResponse = await byteWebConnectorService.SendDocument(sendDocumentRequest);

        }
        [Fact]
        public async Task TestGetDocumentDataFromByteService()
        {
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IMilestoneService> milestoneService = new Mock<IMilestoneService>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            HttpContext httpContext = new DefaultHttpContext();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            ServiceCallHelper.AppContext.Configure(contextAccessorMock.Object);
            contextAccessorMock.Setup(_ => _.HttpContext).Returns(httpContext);
            EmbeddedDoc embedded = new EmbeddedDoc();
            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/bytewebconnector/document/getdocumentdatafrombyte", new HttpResponseMessage()
            {

                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(embedded.ToJson())
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
                    Content = new StringContent(embedded.ToJson(), Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
            httpClientFactory.Setup(clientFactory => clientFactory.CreateClient(It.IsAny<string>())).Returns(httpClient);
            IByteWebConnectorService byteWebConnectorService = new ByteWebConnectorService(Mock.Of<ILogger<ByteWebConnectorService>>(), httpClient, mockConfiguration.Object);
            GetDocumentDataRequest getDocumentDataRequest = new GetDocumentDataRequest();
            EmbeddedDoc embeddedDoc = await byteWebConnectorService.GetDocumentDataFromByte(getDocumentDataRequest);

        }
        [Fact]
        public async Task TestUploadFileService()
        {
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IMilestoneService> milestoneService = new Mock<IMilestoneService>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            HttpContext httpContext = new DefaultHttpContext();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            ServiceCallHelper.AppContext.Configure(contextAccessorMock.Object);
            contextAccessorMock.Setup(_ => _.HttpContext).Returns(httpContext);
            UploadFileResponse mockuploadFileResponse = new UploadFileResponse();
            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/documentmanagement/request/uploadfile", new HttpResponseMessage()
            {

                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(mockuploadFileResponse.ToJson())
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
                    Content = new StringContent(mockuploadFileResponse.ToJson(), Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
            httpClientFactory.Setup(clientFactory => clientFactory.CreateClient(It.IsAny<string>())).Returns(httpClient);
            IByteWebConnectorService byteWebConnectorService = new ByteWebConnectorService(Mock.Of<ILogger<ByteWebConnectorService>>(), httpClient, mockConfiguration.Object);
            GetDocumentDataRequest getDocumentDataRequest = new GetDocumentDataRequest();
            UploadFileResponse uploadFileResponse = await byteWebConnectorService.UploadFile("");

        }
        [Fact]
        public async Task TestSendLoanApplicationService()
        {
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IMilestoneService> milestoneService = new Mock<IMilestoneService>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            HttpContext httpContext = new DefaultHttpContext();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            ServiceCallHelper.AppContext.Configure(contextAccessorMock.Object);
            contextAccessorMock.Setup(_ => _.HttpContext).Returns(httpContext);
            LoanFileInfo mockupLoanFileInfo = new LoanFileInfo();
            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/bytewebconnector/loanfile/sendloanfile", new HttpResponseMessage()
            {

                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(mockupLoanFileInfo.ToJson())
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
                    Content = new StringContent(mockupLoanFileInfo.ToJson(), Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
            httpClientFactory.Setup(clientFactory => clientFactory.CreateClient(It.IsAny<string>())).Returns(httpClient);
            IByteWebConnectorService byteWebConnectorService = new ByteWebConnectorService(Mock.Of<ILogger<ByteWebConnectorService>>(), httpClient, mockConfiguration.Object);
            LoanApplication loanApplication = new LoanApplication();
            LoanRequest loanRequest = new LoanRequest();
            List<ThirdPartyCode> byteProCodeList = new List<ThirdPartyCode>();
            ServiceCallHelper.CallResponse<ApiResponse<LoanFileInfo>> callResponse = await byteWebConnectorService.SendLoanApplication(loanApplication,loanRequest,byteProCodeList);

        }
        [Fact]
        public async Task TestSendLoanApplicationViaSDKService()
        {
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IMilestoneService> milestoneService = new Mock<IMilestoneService>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            HttpContext httpContext = new DefaultHttpContext();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            ServiceCallHelper.AppContext.Configure(contextAccessorMock.Object);
            contextAccessorMock.Setup(_ => _.HttpContext).Returns(httpContext);
            LoanFileInfo mockupLoanFileInfo = new LoanFileInfo();
            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/bytewebconnector/loanfile/sendloanfileviasdk", new HttpResponseMessage()
            {

                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(mockupLoanFileInfo.ToJson())
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
                    Content = new StringContent(mockupLoanFileInfo.ToJson(), Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
            httpClientFactory.Setup(clientFactory => clientFactory.CreateClient(It.IsAny<string>())).Returns(httpClient);
            IByteWebConnectorService byteWebConnectorService = new ByteWebConnectorService(Mock.Of<ILogger<ByteWebConnectorService>>(), httpClient, mockConfiguration.Object);
            LoanApplication loanApplication = new LoanApplication();
            LoanRequest loanRequest = new LoanRequest();
            List<ThirdPartyCode> byteProCodeList = new List<ThirdPartyCode>();
            ServiceCallHelper.CallResponse<ApiResponse<LoanFileInfo>> callResponse = await byteWebConnectorService.SendLoanApplicationViaSDK(loanApplication, byteProCodeList);

        }
        [Fact]
        public async Task TestGetByteLoanStatusNameViaSDKService()
        {
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IMilestoneService> milestoneService = new Mock<IMilestoneService>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            HttpContext httpContext = new DefaultHttpContext();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            ServiceCallHelper.AppContext.Configure(contextAccessorMock.Object);
            contextAccessorMock.Setup(_ => _.HttpContext).Returns(httpContext);
            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/bytewebconnector/loanfile/getloanstatusnameviasdk?bytefilename=byte", new HttpResponseMessage()
            {

                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{   \"status\": \"sta\"}")
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
                    Content = new StringContent("status", Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
            httpClientFactory.Setup(clientFactory => clientFactory.CreateClient(It.IsAny<string>())).Returns(httpClient);
            IByteWebConnectorService byteWebConnectorService = new ByteWebConnectorService(Mock.Of<ILogger<ByteWebConnectorService>>(), httpClient, mockConfiguration.Object);
            ServiceCallHelper.CallResponse<ApiResponse<string>> callResponse = await byteWebConnectorService.GetByteLoanStatusNameViaSDK("byte");

        }
    }
}
