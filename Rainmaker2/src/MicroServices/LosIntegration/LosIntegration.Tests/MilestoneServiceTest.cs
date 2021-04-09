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
namespace LosIntegration.Tests
{
    public class MilestoneServiceTest
    {
        [Fact]
        public async Task TestSyncRainmakerLoanStatusFromByteService()
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
            messages.Add("http://test.com/api/milestone/milestone/setlosmilestone", new HttpResponseMessage()
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
            IMilestoneService milestone = new MilestoneService(Mock.Of<ILogger<MilestoneService>>(), httpClient, mockConfiguration.Object);
            Microsoft.AspNetCore.Mvc.IActionResult result = await milestone.SyncRainmakerLoanStatusFromByte(1, 1, 1, "a");

        }
        [Fact]
        public async Task TestGetMappingAllService()
        {
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IMilestoneService> milestoneService = new Mock<IMilestoneService>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            HttpContext httpContext = new DefaultHttpContext();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            ServiceCallHelper.AppContext.Configure(contextAccessorMock.Object);
            contextAccessorMock.Setup(_ => _.HttpContext).Returns(httpContext);
            List<MilestoneMappingResponse> milestoneMappingResponses = new List<MilestoneMappingResponse>()
            {
                new MilestoneMappingResponse() {
                    Id = 1,
                    ExternalOriginatorId = 1,
                    Name = "",
                    StatusId = 2,
                    TenantId = 1
                } 
            };
            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/milestone/milestone/getmappingall?tenantid=1&losid=1", new HttpResponseMessage()
            {

                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(milestoneMappingResponses.ToJson())
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
            IMilestoneService milestone = new MilestoneService(Mock.Of<ILogger<MilestoneService>>(), httpClient, mockConfiguration.Object);
            List<Model.Model.ServiceResponseModels.MilestoneMappingResponse> lists = await milestone.GetMappingAll(1, 1);

        }
    }
}
