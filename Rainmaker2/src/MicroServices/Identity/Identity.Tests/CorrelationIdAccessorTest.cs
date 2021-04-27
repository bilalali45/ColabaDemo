using System;
using System.Collections.Generic;
using System.Text;
using Identity.CorrelationHandlersAndMiddleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Identity.Tests
{
    public class CorrelationIdAccessorTest
    {
        [Fact]
        public void TestCorrelationIdAccessor()
        {
            Mock<ILogger<CorrelationIdAccessor>> mockLoggerHandler = new Mock<ILogger<CorrelationIdAccessor>>();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();

            string correlationId = "TestCorrelationId";

            HttpContext httpContext = new DefaultHttpContext();
            Dictionary<object, object> contextItems = new Dictionary<object, object>();
            contextItems.Add("CorrelationId", correlationId);
            httpContext.Request.Headers["CorrelationId"] = correlationId;
            contextAccessorMock.Setup(_ => _.HttpContext).Returns(httpContext);
            contextAccessorMock.Setup(_ => _.HttpContext.Items).Returns(contextItems);


            var service = new CorrelationIdAccessor(mockLoggerHandler.Object,
                                                    contextAccessorMock.Object);

            var result = service.GetCorrelationId();
            Assert.Equal(correlationId, result);
        }
    }
}
