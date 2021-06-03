using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common;
using Xunit;
using TenantConfig.Common.DistributedCache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading;
using System.Net;

namespace TenantConfig.Tests
{
    public class TestMessageHandler : HttpMessageHandler
    {
        private readonly IDictionary<string, HttpResponseMessage> messages;

        public TestMessageHandler(IDictionary<string, HttpResponseMessage> messages)
        {
            this.messages = messages;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);
            if (messages.ContainsKey(request.RequestUri.ToString().ToLower()))
                response = messages[request.RequestUri.ToString().ToLower()] ?? new HttpResponseMessage(HttpStatusCode.NoContent);
            response.RequestMessage = request;
            return Task.FromResult(response);
        }
    }
    public class RecaptchaTests
    {
        [Fact]
        public async Task CheckRecaptchaBadResult()
        {
            //Arrange
            var controller = Mock.Of<Controller>();

            var httpContext = new DefaultHttpContext();

            var actionContext = new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            };
            var metadata = new List<IFilterMetadata>();

            var context = new ActionExecutingContext(
                actionContext,
                metadata,
                new Dictionary<string, object>(),
                controller);

            ActionExecutionDelegate next = () => {
                var ctx = new ActionExecutedContext(actionContext, metadata, controller);
                return Task.FromResult(ctx);
            };
            var attribute = new ValidateRecaptchaAttribute(null,null,null);
            await attribute.OnActionExecutionAsync(context, next);
            Assert.IsType<BadRequestObjectResult>(context.Result);
        }
        [Fact]
        public async Task CheckRecaptchaDevConfiguration()
        {
            //Arrange
            var controller = Mock.Of<Controller>();

            var httpContext = new DefaultHttpContext();

            var actionContext = new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            };
            var metadata = new List<IFilterMetadata>();

            var context = new ActionExecutingContext(
                actionContext,
                metadata,
                new Dictionary<string, object>(),
                controller);

            ActionExecutionDelegate next = () => {
                var ctx = new ActionExecutedContext(actionContext, metadata, controller);
                ctx.Result = new OkResult();
                return Task.FromResult(ctx);
            };
            httpContext.Request.Headers.Add(Constants.RECAPTCHA_CODE,"rainsoft");
            var mockConfiguration = new Mock<IConfiguration>();
            var mockSection = new Mock<IConfigurationSection>();
            mockSection.SetupGet(x => x.Key).Returns("DevToken");
            mockConfiguration.Setup(x => x.GetChildren()).Returns(new List<IConfigurationSection> { mockSection.Object });
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("rainsoft");
            var attribute = new ValidateRecaptchaAttribute(mockConfiguration.Object, Mock.Of<ILogger<ValidateRecaptchaAttribute>>(), null);
            await attribute.OnActionExecutionAsync(context, next);
            Assert.Null(context.Result);
        }

        [Fact]
        public async Task CheckRecaptchaKeystoreError()
        {
            //Arrange
            var controller = Mock.Of<Controller>();

            var httpContext = new DefaultHttpContext();

            var actionContext = new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            };
            var metadata = new List<IFilterMetadata>();

            var context = new ActionExecutingContext(
                actionContext,
                metadata,
                new Dictionary<string, object>(),
                controller);

            ActionExecutionDelegate next = () => {
                var ctx = new ActionExecutedContext(actionContext, metadata, controller);
                ctx.Result = new OkResult();
                return Task.FromResult(ctx);
            };
            httpContext.Request.Headers.Add(Constants.RECAPTCHA_CODE, "rainsoft");
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetChildren()).Returns(new List<IConfigurationSection> { });
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/keystore/keystore?key=RecaptchaKey", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("rainsoft")
            });
            var attribute = new ValidateRecaptchaAttribute(mockConfiguration.Object, Mock.Of<ILogger<ValidateRecaptchaAttribute>>(), new HttpClient(new TestMessageHandler(messages)));
            await attribute.OnActionExecutionAsync(context, next);
            Assert.IsType<BadRequestObjectResult>(context.Result);
        }

        [Fact]
        public async Task CheckRecaptchaGoogleError()
        {
            //Arrange
            var controller = Mock.Of<Controller>();

            var httpContext = new DefaultHttpContext();

            var actionContext = new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            };
            var metadata = new List<IFilterMetadata>();

            var context = new ActionExecutingContext(
                actionContext,
                metadata,
                new Dictionary<string, object>(),
                controller);

            ActionExecutionDelegate next = () => {
                var ctx = new ActionExecutedContext(actionContext, metadata, controller);
                ctx.Result = new OkResult();
                return Task.FromResult(ctx);
            };
            httpContext.Request.Headers.Add(Constants.RECAPTCHA_CODE, "rainsoft");
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetChildren()).Returns(new List<IConfigurationSection> { });
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/keystore/keystore?key=recaptchakey", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("rainsoft")
            });
            messages.Add("https://www.google.com/recaptcha/api/siteverify?secret=rainsoft&response=rainsoft", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("rainsoft")
            });
            var attribute = new ValidateRecaptchaAttribute(mockConfiguration.Object, Mock.Of<ILogger<ValidateRecaptchaAttribute>>(), new HttpClient(new TestMessageHandler(messages)));
            await attribute.OnActionExecutionAsync(context, next);
            Assert.IsType<BadRequestObjectResult>(context.Result);
        }

        [Fact]
        public async Task CheckRecaptchaGoogleSuccess()
        {
            //Arrange
            var controller = Mock.Of<Controller>();

            var httpContext = new DefaultHttpContext();

            var actionContext = new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            };
            var metadata = new List<IFilterMetadata>();

            var context = new ActionExecutingContext(
                actionContext,
                metadata,
                new Dictionary<string, object>(),
                controller);

            ActionExecutionDelegate next = () => {
                var ctx = new ActionExecutedContext(actionContext, metadata, controller);
                ctx.Result = new OkResult();
                return Task.FromResult(ctx);
            };
            httpContext.Request.Headers.Add(Constants.RECAPTCHA_CODE, "rainsoft");
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetChildren()).Returns(new List<IConfigurationSection> { });
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/keystore/keystore?key=recaptchakey", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("rainsoft")
            });
            messages.Add("https://www.google.com/recaptcha/api/siteverify?secret=rainsoft&response=rainsoft", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""success"":false}")
            });
            var attribute = new ValidateRecaptchaAttribute(mockConfiguration.Object, Mock.Of<ILogger<ValidateRecaptchaAttribute>>(), new HttpClient(new TestMessageHandler(messages)));
            await attribute.OnActionExecutionAsync(context, next);
            Assert.IsType<BadRequestObjectResult>(context.Result);
        }

        [Fact]
        public async Task CheckRecaptchaGoogleScore()
        {
            //Arrange
            var controller = Mock.Of<Controller>();

            var httpContext = new DefaultHttpContext();

            var actionContext = new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            };
            var metadata = new List<IFilterMetadata>();

            var context = new ActionExecutingContext(
                actionContext,
                metadata,
                new Dictionary<string, object>(),
                controller);

            ActionExecutionDelegate next = () => {
                var ctx = new ActionExecutedContext(actionContext, metadata, controller);
                ctx.Result = new OkResult();
                return Task.FromResult(ctx);
            };
            httpContext.Request.Headers.Add(Constants.RECAPTCHA_CODE, "rainsoft");
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetChildren()).Returns(new List<IConfigurationSection> { });
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/keystore/keystore?key=recaptchakey", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("rainsoft")
            });
            messages.Add("https://www.google.com/recaptcha/api/siteverify?secret=rainsoft&response=rainsoft", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""success"":true,""score"":0}")
            });
            var attribute = new ValidateRecaptchaAttribute(mockConfiguration.Object, Mock.Of<ILogger<ValidateRecaptchaAttribute>>(), new HttpClient(new TestMessageHandler(messages)));
            await attribute.OnActionExecutionAsync(context, next);
            Assert.IsType<BadRequestObjectResult>(context.Result);
        }

        [Fact]
        public async Task CheckRecaptchaGoogleAction()
        {
            //Arrange
            var controller = Mock.Of<Controller>();

            var httpContext = new DefaultHttpContext();

            var actionContext = new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            };
            var metadata = new List<IFilterMetadata>();

            var context = new ActionExecutingContext(
                actionContext,
                metadata,
                new Dictionary<string, object>(),
                controller);

            ActionExecutionDelegate next = () => {
                var ctx = new ActionExecutedContext(actionContext, metadata, controller);
                ctx.Result = new OkResult();
                return Task.FromResult(ctx);
            };
            httpContext.Request.Headers.Add(Constants.RECAPTCHA_CODE, "rainsoft");
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetChildren()).Returns(new List<IConfigurationSection> { });
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/keystore/keystore?key=recaptchakey", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("rainsoft")
            });
            messages.Add("https://www.google.com/recaptcha/api/siteverify?secret=rainsoft&response=rainsoft", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""success"":true,""score"":0.9,""action"":""test""}")
            });
            var attribute = new ValidateRecaptchaAttribute(mockConfiguration.Object, Mock.Of<ILogger<ValidateRecaptchaAttribute>>(), new HttpClient(new TestMessageHandler(messages)));
            await attribute.OnActionExecutionAsync(context, next);
            Assert.IsType<BadRequestObjectResult>(context.Result);
        }

        [Fact]
        public async Task CheckRecaptchaGoogleRecaptcha()
        {
            //Arrange
            var controller = Mock.Of<Controller>();

            var httpContext = new DefaultHttpContext();

            var actionContext = new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            };
            var metadata = new List<IFilterMetadata>();

            var context = new ActionExecutingContext(
                actionContext,
                metadata,
                new Dictionary<string, object>(),
                controller);

            ActionExecutionDelegate next = () => {
                var ctx = new ActionExecutedContext(actionContext, metadata, controller);
                ctx.Result = new OkResult();
                return Task.FromResult(ctx);
            };
            httpContext.Request.Headers.Add(Constants.RECAPTCHA_CODE, "rainsoft");
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetChildren()).Returns(new List<IConfigurationSection> { });
            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://test.com");
            Dictionary<string, HttpResponseMessage> messages = new Dictionary<string, HttpResponseMessage>();
            messages.Add("http://test.com/api/keystore/keystore?key=recaptchakey", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("rainsoft")
            });
            messages.Add("https://www.google.com/recaptcha/api/siteverify", new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""success"":true,""score"":0.9,""action"":""submit""}")
            });
            var attribute = new ValidateRecaptchaAttribute(mockConfiguration.Object, Mock.Of<ILogger<ValidateRecaptchaAttribute>>(), new HttpClient(new TestMessageHandler(messages)));
            await attribute.OnActionExecutionAsync(context, next);
            Assert.Null(context.Result);
        }
    }
}
