using LosIntegration.Model.Model.ServiceResponseModels.Rainmaker;
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
using Extensions.ExtensionClasses;
using LosIntegration.Service.InternalServices.Rainmaker;
using LosIntegration.Service.InternalServices;

namespace LosIntegration.Tests
{
    public class ThirdPartyCodeTest
    {
        [Fact]
        public async Task TestGetRefIdByThirdPartyIdService()
        {
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            IEnumerable<string> authHeader = new string[] { "Authorization" };
            List<ThirdPartyCode> thirdPartyCodes = new List<ThirdPartyCode>()
            {
                new ThirdPartyCode()
                {
                    Id = 1,Code ="1"
                }
            };
            HttpContext httpContext = new DefaultHttpContext();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            ServiceCallHelper.AppContext.Configure(contextAccessorMock.Object);
            contextAccessorMock.Setup(_ => _.HttpContext).Returns(httpContext);
            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/rainmaker/thirdpartycode/getrefidbythirdpartyid?thirdpartyid=1", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(thirdPartyCodes.ToJson())
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
                    Content = new StringContent(thirdPartyCodes.ToJson(), Encoding.UTF8, "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(new TestMessageHandler(messages));
            httpClientFactory.Setup(clientFactory => clientFactory.CreateClient(It.IsAny<string>())).Returns(httpClient);
            IThirdPartyCodeService thirdPartyCodeService = new ThirdPartyCodeService(httpClient,mockConfiguration.Object);
            List<ThirdPartyCode> lists = thirdPartyCodeService.GetRefIdByThirdPartyId(1);
        }
    }
}
