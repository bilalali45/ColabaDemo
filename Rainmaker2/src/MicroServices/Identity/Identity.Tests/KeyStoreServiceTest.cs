using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Identity.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Xunit;

namespace Identity.Tests
{
    public class KeyStoreServiceTest
    {
        [Fact]
        public async Task TestGetJwtSecurityKeyAsync()
        {
            Mock<IHttpClientFactory> httpClientFactoryMock = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> configurationMock = new Mock<IConfiguration>();

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                                                  "SendAsync", // GetAsync eventually calls SendAsync internally
                                                  ItExpr.IsAny<HttpRequestMessage>(),
                                                  ItExpr.IsAny<CancellationToken>()
                                                 )
                .ReturnsAsync(new HttpResponseMessage()
                              {
                                  StatusCode = HttpStatusCode.OK,
                                  Content = new StringContent("fakesecuritykey", Encoding.UTF8, "text/html"),
                              });
            
            var httpClient = new HttpClient(handlerMock.Object)
                             {
                                 BaseAddress = new Uri("http://test.com/"),
                             };
            httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var service = new KeyStoreService(httpClientFactoryMock.Object, configurationMock.Object);
            var result = await service.GetJwtSecurityKeyAsync();
            Assert.Equal("fakesecuritykey", result);
        }

        [Fact]
        public void TestGetJwtSecurityKey()
        {
            Mock<IHttpClientFactory> httpClientFactoryMock = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> configurationMock = new Mock<IConfiguration>();

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                                                  "SendAsync", // GetAsync eventually calls SendAsync internally
                                                  ItExpr.IsAny<HttpRequestMessage>(),
                                                  ItExpr.IsAny<CancellationToken>()
                                                 )
                .ReturnsAsync(new HttpResponseMessage()
                              {
                                  StatusCode = HttpStatusCode.OK,
                                  Content = new StringContent("fakesecuritykey", Encoding.UTF8, "text/html"),
                              });
            
            var httpClient = new HttpClient(handlerMock.Object)
                             {
                                 BaseAddress = new Uri("http://test.com/"),
                             };
            httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var service = new KeyStoreService(httpClientFactoryMock.Object, configurationMock.Object);
            var result = service.GetJwtSecurityKey();
            Assert.Equal("fakesecuritykey", result);
        }
    }
}
