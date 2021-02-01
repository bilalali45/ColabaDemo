using DocumentManagement.API.CorrelationHandlersAndMiddleware;
using DocumentManagement.API.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DocumentManagement.Tests
{
    public class HelperTests
    {
        [Fact]
        public void TestAsyncHelperRunSyncResult()
        {
            var result = AsyncHelper.RunSync(() => { return Task.FromResult(1); });
            Assert.Equal(1,result);
        }

        [Fact]
        public void TestAsyncHelperRunSync()
        {
            AsyncHelper.RunSync(() => { return Task.CompletedTask; });
        }

        [Fact]
        public void TestToJsonNull()
        {
            var result = ObjectExtensions.ToJson(null);
            Assert.Null(result);
        }
        [Fact]
        public void TestToJson()
        {
            var result = ObjectExtensions.ToJson(new { a = 1});
            Assert.NotNull(result);
        }
        [Fact]
        public async Task TestExceptionMiddleware()
        {
            using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {

                    })
                    .Configure(app =>
                    {
                        app.UseMiddleware<ExceptionMiddleware>();
                        app.Use(async(httpContext,next) => { throw new Exception(); });
                    });
            })
            .StartAsync();
            using var response = await host.GetTestClient().GetAsync("/");
            Assert.Equal(System.Net.HttpStatusCode.InternalServerError,response.StatusCode);
        }
        [Fact]
        public async Task TestExceptionMiddlewareNoException()
        {
            using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {

                    })
                    .Configure(app =>
                    {
                        app.UseMiddleware<ExceptionMiddleware>();
                        app.Use(async (httpContext, next) => { httpContext.Response.StatusCode = 200; await httpContext.Response.CompleteAsync(); });
                    });
            })
            .StartAsync();
            using var response = await host.GetTestClient().GetAsync("/");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task TestCorrelationIdAccessor()
        {
            Mock<IHttpContextAccessor> mock = new Mock<IHttpContextAccessor>();
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.Items["CorrelationId"]).Returns("123");
            mock.SetupGet(x => x.HttpContext).Returns(httpContext.Object);

            var service = new CorrelationIdAccessor(Mock.Of<ILogger<CorrelationIdAccessor>>(),mock.Object);
            var result = service.GetCorrelationId();

            Assert.Equal("123",result);
        }
        [Fact]
        public async Task TestCorrelationIdAccessorException()
        {
            Mock<IHttpContextAccessor> mock = new Mock<IHttpContextAccessor>();
            var httpContext = new Mock<HttpContext>();
            mock.SetupGet(x => x.HttpContext).Returns(httpContext.Object);

            var service = new CorrelationIdAccessor(Mock.Of<ILogger<CorrelationIdAccessor>>(), mock.Object);
            var result = service.GetCorrelationId();

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public async Task TestRequestHandler()
        {
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NoContent
                });

            using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices((IServiceCollection services) =>
                    {
                        services.AddTransient<RequestHandler>();
                        services.AddHttpClient(name: "clientWithCorrelationId")
                                .ConfigurePrimaryHttpMessageHandler(()=>mockMessageHandler.Object)
                                .AddHttpMessageHandler<RequestHandler>(); //Override SendAsync method 
                        services.AddSingleton(implementationFactory: s => s.GetRequiredService<IHttpClientFactory>().CreateClient(name: "clientWithCorrelationId"));
                        services.AddHttpContextAccessor(); //For http request context accessing
                        services.AddTransient<ICorrelationIdAccessor, CorrelationIdAccessor>();
                    })
                    .Configure(app =>
                    {
                        app.Use(async (httpContext, next) => 
                        {
                            var client = httpContext.RequestServices.GetRequiredService<HttpClient>();
                            var response = await client.GetAsync("http://test.com");
                            httpContext.Response.StatusCode = (int)response.StatusCode; 
                            await httpContext.Response.CompleteAsync(); 
                        });
                    });
            })
            .StartAsync();
            using var response = await host.GetTestClient().GetAsync("/");
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task TestLogHeaderMiddlewareNoId()
        {
            using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {

                    })
                    .Configure(app =>
                    {
                        app.UseMiddleware<LogHeaderMiddleware>();
                        app.Use(async (httpContext, next) => {
                            httpContext.Response.StatusCode = 200;
                            await httpContext.Response.WriteAsync(httpContext.Items["CorrelationId"].ToString());
                            await httpContext.Response.CompleteAsync();
                        });
                    });
            })
            .StartAsync();
            using var response = await host.GetTestClient().GetAsync("/");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<Guid>(Guid.Parse(await response.Content.ReadAsStringAsync()));
        }
        [Fact]
        public async Task TestLogHeaderMiddlewareId()
        {
            using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {

                    })
                    .Configure(app =>
                    {
                        app.UseMiddleware<LogHeaderMiddleware>();
                        app.Use(async (httpContext, next) => {
                            httpContext.Response.StatusCode = 200;
                            await httpContext.Response.WriteAsync(httpContext.Items["CorrelationId"].ToString());
                            await httpContext.Response.CompleteAsync();
                        });
                    });
            })
            .StartAsync();
            string id = "123";
            var server = host.GetTestServer();
            server.BaseAddress = new Uri("https://example.com");

            var context = await server.SendAsync(c =>
            {
                c.Request.Method = HttpMethods.Get;
                c.Request.Path = "/";
                c.Request.Headers.Add("CorrelationId", id); 
            });
            Assert.Equal(200, context.Response.StatusCode);
            Assert.Equal(id, (new StreamReader(context.Response.Body)).ReadToEnd());
        }
    }
}
