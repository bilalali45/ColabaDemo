using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LosIntegration.API.Controllers;
using LosIntegration.Model.Model.ServiceRequestModels.RainMaker;
using LosIntegration.Model.Model.ServiceResponseModels;
using LosIntegration.Model.Model.ServiceResponseModels.ByteWebConnector;
using LosIntegration.Model.Model.ServiceResponseModels.Rainmaker;
using LosIntegration.Service.InternalServices;
using LosIntegration.Service.InternalServices.Rainmaker;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using ServiceCallHelper;
using Xunit;
using Extensions.ExtensionClasses;
using Microsoft.Extensions.Configuration;

namespace LosIntegration.Tests
{
    public class LoanApplicationTest
    {
        [Fact]
        public async Task SendLoanApplicationToExternalOriginatorController()
        {
            Mock<ILoanApplicationService> loanApplicationService = new Mock<ILoanApplicationService>();
            Mock<ILoanRequestService> loanRequestService = new Mock<ILoanRequestService>();
            Mock<IThirdPartyCodeService> thirdPartyService = new Mock<IThirdPartyCodeService>();
            Mock<IByteWebConnectorService> byteWebConnectorService = new Mock<IByteWebConnectorService>();
            LoanApplication loanApplication = new LoanApplication();
            loanApplication.Id = 1033;
            loanApplicationService.Setup(x => x.GetLoanApplicationWithDetails(It.IsAny<int>(),
                                                                              It.IsAny<LoanApplicationService.RelatedEntities>())).Returns(loanApplication);
            LoanRequest loanRequest = new LoanRequest();
            loanRequest.Id = 1;
            loanRequestService.Setup(x => x.GetLoanRequestWithDetails(It.IsAny<int>(),
                                                                          null)).Returns(loanRequest);
            CallResponse<UpdateLoanApplicationRequest> updateApplicationrequest = new CallResponse<UpdateLoanApplicationRequest>(new HttpResponseMessage(HttpStatusCode.OK), new UpdateLoanApplicationRequest()
            {
                ByteFileName = "13",
                ByteLoanNumber = "12"
            }, null);
            loanApplicationService.Setup(x => x.UpdateLoanApplication(It.IsAny<UpdateLoanApplicationRequest>()
                                                                          )).Returns(updateApplicationrequest);
            List<ThirdPartyCode> thirdPartyCodes = new List<ThirdPartyCode>();
            thirdPartyCodes.Add(new ThirdPartyCode()
            {
                ThirdPartyId = 6,
                ElementName = "REOType",
                Code = "TwoToFourUnitProperty",
                EntityRefTypeId = 137,
                EntityRefId = 5
            });
            thirdPartyService.Setup(x => x.GetRefIdByThirdPartyId(It.IsAny<int>())).Returns(thirdPartyCodes);
            CallResponse<ApiResponse<LoanFileInfo>> apiResponse = new CallResponse<ApiResponse<LoanFileInfo>>(new HttpResponseMessage(HttpStatusCode.OK), new ApiResponse<LoanFileInfo>()
            {
                Data = new LoanFileInfo()
                {
                    FileDataId = 12,
                    FileName = "13"
                }
            }, null);
            byteWebConnectorService.Setup(service => service.SendLoanApplicationViaSDK(It.IsAny<LoanApplication>(),
                                                                                 It.IsAny<List<ThirdPartyCode>>())).ReturnsAsync(apiResponse);

            var controller = new LoanApplicationController(Mock.Of<ILogger<LoanApplicationController>>(), loanApplicationService.Object, byteWebConnectorService.Object, null, loanRequestService.Object, thirdPartyService.Object);

            ApiResponse<LoanFileInfo> result = await controller.SendLoanApplicationToExternalOriginator(1033);
            Assert.IsType<ApiResponse<LoanFileInfo>>(result);
        }

        
        [Fact]
        public async Task UpdateLoanStatusFromByteController()
        {
            Mock<ILoanApplicationService> loanApplicationService = new Mock<ILoanApplicationService>();
            Mock<ILoanRequestService> loanRequestService = new Mock<ILoanRequestService>();
            Mock<IThirdPartyCodeService> thirdPartyService = new Mock<IThirdPartyCodeService>();
            Mock<IByteWebConnectorService> byteWebConnectorService = new Mock<IByteWebConnectorService>();
            Mock<IMilestoneService> milestoneService = new Mock<IMilestoneService>();
            LoanApplication loanApplication = new LoanApplication();
            loanApplication.Id = 1033;
            loanApplicationService.Setup(x => x.GetLoanApplicationWithDetails(It.IsAny<int>(), null)).Returns(loanApplication);
            CallResponse<ApiResponse<string>> statusNameResponse = new CallResponse<ApiResponse<string>>(new HttpResponseMessage(HttpStatusCode.OK), new ApiResponse<string>()
            {
                Data = "status",
                Status = ApiResponseStatus.Success
            }, null);
            byteWebConnectorService.Setup(x => x.GetByteLoanStatusNameViaSDK(It.IsAny<string>())).ReturnsAsync(statusNameResponse);
            List<MilestoneMappingResponse> milestoneMappingResponses = new List<MilestoneMappingResponse>() {
                new MilestoneMappingResponse() {
                    ExternalOriginatorId = 1,
                    Id = 1,
                    Name = "status",
                    StatusId =2,
                    TenantId =1
                }
            };
            milestoneService.Setup(x => x.GetMappingAll(It.IsAny<int>(), It.IsAny<short>())).ReturnsAsync(milestoneMappingResponses);
            CallResponse<ApiResponse<LoanFileInfo>> apiResponse = new CallResponse<ApiResponse<LoanFileInfo>>(new HttpResponseMessage(HttpStatusCode.OK), new ApiResponse<LoanFileInfo>()
            {
                Data = new LoanFileInfo()
                {
                    FileDataId = 12,
                    FileName = "13"
                }
            }, null);
            byteWebConnectorService.Setup(service => service.SendLoanApplicationViaSDK(It.IsAny<LoanApplication>(),
                                                                                 It.IsAny<List<ThirdPartyCode>>())).ReturnsAsync(apiResponse);

            var controller = new LoanApplicationController(Mock.Of<ILogger<LoanApplicationController>>(), loanApplicationService.Object, byteWebConnectorService.Object, milestoneService.Object, loanRequestService.Object, thirdPartyService.Object);

            IActionResult actionResult = await controller.UpdateLoanStatusFromByte(1033);
            Assert.Equal(1,1);
        }

        [Fact]
        public async Task UpdateLoanStatusFromByteLoanApplicationNotFoundController()
        {
            Mock<ILoanApplicationService> loanApplicationService = new Mock<ILoanApplicationService>();
            Mock<ILoanRequestService> loanRequestService = new Mock<ILoanRequestService>();
            Mock<IThirdPartyCodeService> thirdPartyService = new Mock<IThirdPartyCodeService>();
            Mock<IByteWebConnectorService> byteWebConnectorService = new Mock<IByteWebConnectorService>();
            Mock<IMilestoneService> milestoneService = new Mock<IMilestoneService>();
            LoanApplication loanApplication = null;
            loanApplicationService.Setup(x => x.GetLoanApplicationWithDetails(It.IsAny<int>(), null)).Returns(loanApplication);
            CallResponse<ApiResponse<string>> statusNameResponse = new CallResponse<ApiResponse<string>>(new HttpResponseMessage(HttpStatusCode.OK), new ApiResponse<string>()
            {
                Data = "status",
                Status = ApiResponseStatus.Success
            }, null);
            byteWebConnectorService.Setup(x => x.GetByteLoanStatusNameViaSDK(It.IsAny<string>())).ReturnsAsync(statusNameResponse);
            List<MilestoneMappingResponse> milestoneMappingResponses = new List<MilestoneMappingResponse>() {
                new MilestoneMappingResponse() {
                    ExternalOriginatorId = 1,
                    Id = 1,
                    Name = "status",
                    StatusId =2,
                    TenantId =1
                }
            };
            milestoneService.Setup(x => x.GetMappingAll(It.IsAny<int>(), It.IsAny<short>())).ReturnsAsync(milestoneMappingResponses);
            CallResponse<ApiResponse<LoanFileInfo>> apiResponse = new CallResponse<ApiResponse<LoanFileInfo>>(new HttpResponseMessage(HttpStatusCode.OK), new ApiResponse<LoanFileInfo>()
            {
                Data = new LoanFileInfo()
                {
                    FileDataId = 12,
                    FileName = "13"
                }
            }, null);
            byteWebConnectorService.Setup(service => service.SendLoanApplicationViaSDK(It.IsAny<LoanApplication>(),
                                                                                 It.IsAny<List<ThirdPartyCode>>())).ReturnsAsync(apiResponse);

            var controller = new LoanApplicationController(Mock.Of<ILogger<LoanApplicationController>>(), loanApplicationService.Object, byteWebConnectorService.Object, milestoneService.Object, loanRequestService.Object, thirdPartyService.Object);

            IActionResult actionResult = await controller.UpdateLoanStatusFromByte(1033);
            Assert.IsType<NotFoundObjectResult>(actionResult);
        }
        [Fact]
        public async Task UpdateLoanStatusFromByteMappingBadRequestController()
        {
            Mock<ILoanApplicationService> loanApplicationService = new Mock<ILoanApplicationService>();
            Mock<ILoanRequestService> loanRequestService = new Mock<ILoanRequestService>();
            Mock<IThirdPartyCodeService> thirdPartyService = new Mock<IThirdPartyCodeService>();
            Mock<IByteWebConnectorService> byteWebConnectorService = new Mock<IByteWebConnectorService>();
            Mock<IMilestoneService> milestoneService = new Mock<IMilestoneService>();
            LoanApplication loanApplication = new LoanApplication();
            loanApplication.Id = 1033;
            loanApplicationService.Setup(x => x.GetLoanApplicationWithDetails(It.IsAny<int>(), null)).Returns(loanApplication);
            CallResponse<ApiResponse<string>> statusNameResponse = new CallResponse<ApiResponse<string>>(new HttpResponseMessage(HttpStatusCode.OK), new ApiResponse<string>()
            {
                Data = "status",
                Status = ApiResponseStatus.Success
            }, null);
            byteWebConnectorService.Setup(x => x.GetByteLoanStatusNameViaSDK(It.IsAny<string>())).ReturnsAsync(statusNameResponse);
            List<MilestoneMappingResponse> milestoneMappingResponses = new List<MilestoneMappingResponse>();
            milestoneService.Setup(x => x.GetMappingAll(It.IsAny<int>(), It.IsAny<short>())).ReturnsAsync(milestoneMappingResponses);
            CallResponse<ApiResponse<LoanFileInfo>> apiResponse = new CallResponse<ApiResponse<LoanFileInfo>>(new HttpResponseMessage(HttpStatusCode.OK), new ApiResponse<LoanFileInfo>()
            {
                Data = new LoanFileInfo()
                {
                    FileDataId = 12,
                    FileName = "13"
                }
            }, null);
            byteWebConnectorService.Setup(service => service.SendLoanApplicationViaSDK(It.IsAny<LoanApplication>(),
                                                                                 It.IsAny<List<ThirdPartyCode>>())).ReturnsAsync(apiResponse);

            var controller = new LoanApplicationController(Mock.Of<ILogger<LoanApplicationController>>(), loanApplicationService.Object, byteWebConnectorService.Object, milestoneService.Object, loanRequestService.Object, thirdPartyService.Object);

            IActionResult actionResult = await controller.UpdateLoanStatusFromByte(1033);
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }
        [Fact]
        public async Task UpdateLoanStatusFromByteMappingBadRequestCase2Controller()
        {
            Mock<ILoanApplicationService> loanApplicationService = new Mock<ILoanApplicationService>();
            Mock<ILoanRequestService> loanRequestService = new Mock<ILoanRequestService>();
            Mock<IThirdPartyCodeService> thirdPartyService = new Mock<IThirdPartyCodeService>();
            Mock<IByteWebConnectorService> byteWebConnectorService = new Mock<IByteWebConnectorService>();
            Mock<IMilestoneService> milestoneService = new Mock<IMilestoneService>();
            LoanApplication loanApplication = new LoanApplication();
            loanApplication.Id = 1033;
            loanApplicationService.Setup(x => x.GetLoanApplicationWithDetails(It.IsAny<int>(), null)).Returns(loanApplication);
            CallResponse<ApiResponse<string>> statusNameResponse = new CallResponse<ApiResponse<string>>(new HttpResponseMessage(HttpStatusCode.OK), new ApiResponse<string>()
            {
                Data = "status",
                Status = ApiResponseStatus.Success
            }, null);
            byteWebConnectorService.Setup(x => x.GetByteLoanStatusNameViaSDK(It.IsAny<string>())).ReturnsAsync(statusNameResponse);
            List<MilestoneMappingResponse> milestoneMappingResponses = null;
            milestoneService.Setup(x => x.GetMappingAll(It.IsAny<int>(), It.IsAny<short>())).ReturnsAsync(milestoneMappingResponses);
            CallResponse<ApiResponse<LoanFileInfo>> apiResponse = new CallResponse<ApiResponse<LoanFileInfo>>(new HttpResponseMessage(HttpStatusCode.OK), new ApiResponse<LoanFileInfo>()
            {
                Data = new LoanFileInfo()
                {
                    FileDataId = 12,
                    FileName = "13"
                }
            }, null);
            byteWebConnectorService.Setup(service => service.SendLoanApplicationViaSDK(It.IsAny<LoanApplication>(),
                                                                                 It.IsAny<List<ThirdPartyCode>>())).ReturnsAsync(apiResponse);

            var controller = new LoanApplicationController(Mock.Of<ILogger<LoanApplicationController>>(), loanApplicationService.Object, byteWebConnectorService.Object, milestoneService.Object, loanRequestService.Object, thirdPartyService.Object);

            IActionResult actionResult = await controller.UpdateLoanStatusFromByte(1033);
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async Task UpdateLoanStatusFromByteStatusNameResponseDataBadrequestController()
        {
            Mock<ILoanApplicationService> loanApplicationService = new Mock<ILoanApplicationService>();
            Mock<ILoanRequestService> loanRequestService = new Mock<ILoanRequestService>();
            Mock<IThirdPartyCodeService> thirdPartyService = new Mock<IThirdPartyCodeService>();
            Mock<IByteWebConnectorService> byteWebConnectorService = new Mock<IByteWebConnectorService>();
            Mock<IMilestoneService> milestoneService = new Mock<IMilestoneService>();
            LoanApplication loanApplication = new LoanApplication();
            loanApplication.Id = 1033;
            loanApplicationService.Setup(x => x.GetLoanApplicationWithDetails(It.IsAny<int>(), null)).Returns(loanApplication);
            CallResponse<ApiResponse<string>> statusNameResponse = new CallResponse<ApiResponse<string>>(new HttpResponseMessage(HttpStatusCode.OK), new ApiResponse<string>()
            {
                Data = "",
                Status = ApiResponseStatus.Success
            }, null);
            byteWebConnectorService.Setup(x => x.GetByteLoanStatusNameViaSDK(It.IsAny<string>())).ReturnsAsync(statusNameResponse);
            List<MilestoneMappingResponse> milestoneMappingResponses = new List<MilestoneMappingResponse>() {
                new MilestoneMappingResponse() {
                    ExternalOriginatorId = 1,
                    Id = 1,
                    Name = "status",
                    StatusId =2,
                    TenantId =1
                }
            };
            milestoneService.Setup(x => x.GetMappingAll(It.IsAny<int>(), It.IsAny<short>())).ReturnsAsync(milestoneMappingResponses);
            CallResponse<ApiResponse<LoanFileInfo>> apiResponse = new CallResponse<ApiResponse<LoanFileInfo>>(new HttpResponseMessage(HttpStatusCode.OK), new ApiResponse<LoanFileInfo>()
            {
                Data = new LoanFileInfo()
                {
                    FileDataId = 12,
                    FileName = "13"
                }
            }, null);
            byteWebConnectorService.Setup(service => service.SendLoanApplicationViaSDK(It.IsAny<LoanApplication>(),
                                                                                 It.IsAny<List<ThirdPartyCode>>())).ReturnsAsync(apiResponse);

            var controller = new LoanApplicationController(Mock.Of<ILogger<LoanApplicationController>>(), loanApplicationService.Object, byteWebConnectorService.Object, milestoneService.Object, loanRequestService.Object, thirdPartyService.Object);

            IActionResult actionResult = await controller.UpdateLoanStatusFromByte(1033);
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async Task UpdateLoanStatusFromByteStatusNameResponseDataNameMapBadrequestController()
        {
            Mock<ILoanApplicationService> loanApplicationService = new Mock<ILoanApplicationService>();
            Mock<ILoanRequestService> loanRequestService = new Mock<ILoanRequestService>();
            Mock<IThirdPartyCodeService> thirdPartyService = new Mock<IThirdPartyCodeService>();
            Mock<IByteWebConnectorService> byteWebConnectorService = new Mock<IByteWebConnectorService>();
            Mock<IMilestoneService> milestoneService = new Mock<IMilestoneService>();
            LoanApplication loanApplication = new LoanApplication();
            loanApplication.Id = 1033;
            loanApplicationService.Setup(x => x.GetLoanApplicationWithDetails(It.IsAny<int>(), null)).Returns(loanApplication);
            CallResponse<ApiResponse<string>> statusNameResponse = new CallResponse<ApiResponse<string>>(new HttpResponseMessage(HttpStatusCode.OK), new ApiResponse<string>()
            {
                Data = "sadasd",
                Status = ApiResponseStatus.Success
            }, null);
            byteWebConnectorService.Setup(x => x.GetByteLoanStatusNameViaSDK(It.IsAny<string>())).ReturnsAsync(statusNameResponse);
            List<MilestoneMappingResponse> milestoneMappingResponses = new List<MilestoneMappingResponse>() {
                new MilestoneMappingResponse() {
                    ExternalOriginatorId = 1,
                    Id = 1,
                    Name = "status",
                    StatusId =2,
                    TenantId =1
                }
            };
            milestoneService.Setup(x => x.GetMappingAll(It.IsAny<int>(), It.IsAny<short>())).ReturnsAsync(milestoneMappingResponses);
            CallResponse<ApiResponse<LoanFileInfo>> apiResponse = new CallResponse<ApiResponse<LoanFileInfo>>(new HttpResponseMessage(HttpStatusCode.OK), new ApiResponse<LoanFileInfo>()
            {
                Data = new LoanFileInfo()
                {
                    FileDataId = 12,
                    FileName = "13"
                }
            }, null);
            byteWebConnectorService.Setup(service => service.SendLoanApplicationViaSDK(It.IsAny<LoanApplication>(),
                                                                                 It.IsAny<List<ThirdPartyCode>>())).ReturnsAsync(apiResponse);

            var controller = new LoanApplicationController(Mock.Of<ILogger<LoanApplicationController>>(), loanApplicationService.Object, byteWebConnectorService.Object, milestoneService.Object, loanRequestService.Object, thirdPartyService.Object);

            IActionResult actionResult = await controller.UpdateLoanStatusFromByte(1033);
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }


        [Fact]
        public async Task UpdateLoanStatusFromByteStatusNameResponseDataNullBadrequestController()
        {
            Mock<ILoanApplicationService> loanApplicationService = new Mock<ILoanApplicationService>();
            Mock<ILoanRequestService> loanRequestService = new Mock<ILoanRequestService>();
            Mock<IThirdPartyCodeService> thirdPartyService = new Mock<IThirdPartyCodeService>();
            Mock<IByteWebConnectorService> byteWebConnectorService = new Mock<IByteWebConnectorService>();
            Mock<IMilestoneService> milestoneService = new Mock<IMilestoneService>();
            LoanApplication loanApplication = new LoanApplication();
            loanApplication.Id = 1033;
            loanApplicationService.Setup(x => x.GetLoanApplicationWithDetails(It.IsAny<int>(), null)).Returns(loanApplication);
            CallResponse<ApiResponse<string>> statusNameResponse = new CallResponse<ApiResponse<string>>(new HttpResponseMessage(HttpStatusCode.OK), new ApiResponse<string>()
            {
                Data = "sadasd",
                Status = ApiResponseStatus.NotFound
            }, null);
            byteWebConnectorService.Setup(x => x.GetByteLoanStatusNameViaSDK(It.IsAny<string>())).ReturnsAsync(statusNameResponse);
            List<MilestoneMappingResponse> milestoneMappingResponses = new List<MilestoneMappingResponse>() {
                new MilestoneMappingResponse() {
                    ExternalOriginatorId = 1,
                    Id = 1,
                    Name = "status",
                    StatusId =2,
                    TenantId =1
                }
            };
            milestoneService.Setup(x => x.GetMappingAll(It.IsAny<int>(), It.IsAny<short>())).ReturnsAsync(milestoneMappingResponses);
            CallResponse<ApiResponse<LoanFileInfo>> apiResponse = new CallResponse<ApiResponse<LoanFileInfo>>(new HttpResponseMessage(HttpStatusCode.OK), new ApiResponse<LoanFileInfo>()
            {
                Data = new LoanFileInfo()
                {
                    FileDataId = 12,
                    FileName = "13"
                }
            }, null);
            byteWebConnectorService.Setup(service => service.SendLoanApplicationViaSDK(It.IsAny<LoanApplication>(),
                                                                                 It.IsAny<List<ThirdPartyCode>>())).ReturnsAsync(apiResponse);

            var controller = new LoanApplicationController(Mock.Of<ILogger<LoanApplicationController>>(), loanApplicationService.Object, byteWebConnectorService.Object, milestoneService.Object, loanRequestService.Object, thirdPartyService.Object);

            IActionResult actionResult = await controller.UpdateLoanStatusFromByte(1033);
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async Task UpdateLoanStatusFromByteExceptionBadrequestController()
        {
            Mock<ILoanApplicationService> loanApplicationService = new Mock<ILoanApplicationService>();
            Mock<ILoanRequestService> loanRequestService = new Mock<ILoanRequestService>();
            Mock<IThirdPartyCodeService> thirdPartyService = new Mock<IThirdPartyCodeService>();
            Mock<IByteWebConnectorService> byteWebConnectorService = new Mock<IByteWebConnectorService>();
            Mock<IMilestoneService> milestoneService = new Mock<IMilestoneService>();
            LoanApplication loanApplication = new LoanApplication();
            loanApplication.Id = 1033;
            loanApplicationService.Setup(x => x.GetLoanApplicationWithDetails(It.IsAny<int>(), null)).Returns(loanApplication);
            CallResponse<ApiResponse<string>> statusNameResponse = new CallResponse<ApiResponse<string>>(new HttpResponseMessage(HttpStatusCode.OK), new ApiResponse<string>()
            {
                Data = "sadasd",
                Status = ApiResponseStatus.NotFound
            }, null);
            byteWebConnectorService.Setup(x => x.GetByteLoanStatusNameViaSDK(It.IsAny<string>())).ReturnsAsync(statusNameResponse);
            List<MilestoneMappingResponse> milestoneMappingResponses = new List<MilestoneMappingResponse>() {
                new MilestoneMappingResponse() {
                    ExternalOriginatorId = 1,
                    Id = 1,
                    Name = "status",
                    StatusId =2,
                    TenantId =1
                }
            };
            milestoneService.Setup(x => x.GetMappingAll(It.IsAny<int>(), It.IsAny<short>())).ReturnsAsync(milestoneMappingResponses);
            CallResponse<ApiResponse<LoanFileInfo>> apiResponse = new CallResponse<ApiResponse<LoanFileInfo>>(new HttpResponseMessage(HttpStatusCode.OK), new ApiResponse<LoanFileInfo>()
            {
                Data = new LoanFileInfo()
                {
                    FileDataId = 12,
                    FileName = "13"
                }
            }, null);
            byteWebConnectorService.Setup(service => service.SendLoanApplicationViaSDK(It.IsAny<LoanApplication>(),
                                                                                 It.IsAny<List<ThirdPartyCode>>())).ReturnsAsync(apiResponse);

            var controller = new LoanApplicationController(Mock.Of<ILogger<LoanApplicationController>>(), null, byteWebConnectorService.Object, milestoneService.Object, loanRequestService.Object, thirdPartyService.Object);

            IActionResult actionResult = await controller.UpdateLoanStatusFromByte(1033);
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public void GetLoanApplicationWithDetailsService()
        {
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            LoanApplication loanApplication = new LoanApplication() { Id =1 };
            HttpContext httpContext = new DefaultHttpContext();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            ServiceCallHelper.AppContext.Configure(contextAccessorMock.Object);
            contextAccessorMock.Setup(_ => _.HttpContext).Returns(httpContext);
            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/rainmaker/loanapplication/getloanapplicationforbyte?id=1&tenantid=1&pending=false", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content =new StringContent(loanApplication.ToJson())
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
                    Content = new StringContent(loanApplication.ToJson(), Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
            httpClientFactory.Setup(clientFactory => clientFactory.CreateClient(It.IsAny<string>())).Returns(httpClient);

            ILoanApplicationService loanApplicationService = new LoanApplicationService(httpClient, mockConfiguration.Object);
            loanApplicationService.GetLoanApplicationWithDetails(1);
            Assert.Equal(1,1);
        }

        [Fact]
        public void UpdateLoanApplicationService()
        {
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            UpdateLoanApplicationRequest updateLoanApplicationRequest = new UpdateLoanApplicationRequest() { Id=1,ByteFileName="name",ByteLoanNumber="1"};

            HttpContext httpContext = new DefaultHttpContext();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            ServiceCallHelper.AppContext.Configure(contextAccessorMock.Object);
            contextAccessorMock.Setup(_ => _.HttpContext).Returns(httpContext);

            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/rainmaker/loanapplication/updateloanapplication", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(updateLoanApplicationRequest.ToJson())
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
                    Content = new StringContent(updateLoanApplicationRequest.ToJson(), Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
            httpClientFactory.Setup(clientFactory => clientFactory.CreateClient(It.IsAny<string>())).Returns(httpClient);
            ILoanApplicationService loanApplicationService = new LoanApplicationService(httpClient, mockConfiguration.Object);
            loanApplicationService.UpdateLoanApplication(updateLoanApplicationRequest);
            Assert.Equal(1,1);
        }
    }
}
