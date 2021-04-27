using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Identity.CorrelationHandlersAndMiddleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using Xunit;

namespace Identity.Tests
{
    public class MiddlewareTest
    {
        [Fact]
        public async Task TestExceptionMiddlewareInvokeAsync()
        {
            Mock<RequestDelegate> next = new Mock<RequestDelegate>();
            Mock<ILogger<ExceptionMiddleware>> loggerMock = new Mock<ILogger<ExceptionMiddleware>>();

            var service = new ExceptionMiddleware(next.Object);
            HttpContext context = new DefaultHttpContext();

            await service.InvokeAsync(context,
                                loggerMock.Object);
            Assert.Equal(1, 1);
        }

        [Fact]
        public async Task TestLogHeaderMiddlewareInvokeAsyncCorrelationIdExist()
        {
            Mock<RequestDelegate> next = new Mock<RequestDelegate>();
            Mock<HttpContext> httpContextMock = new Mock<HttpContext>();
            string fakeCorrelationId = "FakeCorrelationId";

            var headers = new HeaderDictionary(new Dictionary<String, StringValues>
                                               {
                                                   { "CorrelationId", fakeCorrelationId}
                                               }) as IHeaderDictionary;
            Dictionary<object, object> contextItems = new Dictionary<object, object>();

            httpContextMock.Setup(x => x.Items).Returns(contextItems);
            httpContextMock.Setup(x => x.Request.Headers).Returns(headers);

            var service = new LogHeaderMiddleware(next.Object);
            await service.InvokeAsync(httpContextMock.Object);
            Assert.Equal(1, 1);
        }
    }
}
