using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using Identity.Controllers;
using Identity.Models;
using Identity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using TokenCacheHelper.Models;
using TokenCacheHelper.TokenManager;
using URF.Core.Abstractions;
using URF.Core.Abstractions.Trackable;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace Identity.Controllers.Tests
{
    public class TokenControllerTests
    {

        //[Fact]
        //public async Task TestRevokeController()
        //{
        //    Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
        //    Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
        //    Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
        //    Mock<ITokenManager> mockTokenManager = new Mock<ITokenManager>();
        //    mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://localhost:5031");
        //    var idenetity = new ClaimsIdentity();
        //    idenetity.AddClaim(new Claim(ClaimTypes.Name, "rainsoft"));
        //    var claimsPrincipal = new ClaimsPrincipal(idenetity);

        //    var userProfile = new UserProfile();
        //    userProfile.Id = 1;
        //    userProfile.UserName = "rainsoft";

        //    var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        //    handlerMock
        //       .Protected()
        //       // Setup the PROTECTED method to mock
        //       .Setup<Task<HttpResponseMessage>>(
        //          "SendAsync",
        //          ItExpr.IsAny<HttpRequestMessage>(),
        //          ItExpr.IsAny<CancellationToken>()
        //       )
        //       // prepare the expected response of the mocked http call
        //       .ReturnsAsync(new HttpResponseMessage()
        //       {
        //           StatusCode = HttpStatusCode.OK,
        //           Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(userProfile), Encoding.UTF8, "application/json"),
        //       })
        //       .Verifiable();
        //    var httpClient = new HttpClient(handlerMock.Object)
        //    {
        //        BaseAddress = new Uri("http://test.com/"),
        //    };

        //    httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

        //    mockTokenService.Setup(x => x.GetPrincipalFromExpiredToken(It.IsAny<string>())).ReturnsAsync(claimsPrincipal);

        //    var httpContext = new Mock<HttpContext>();
        //    httpContext.Setup(x => x.User).Returns(claimsPrincipal);
        //    var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

        //    var controller = new TokenController(httpClientFactory.Object, mockConfiguration.Object, mockTokenService.Object, mockTokenManager.Object);

        //    controller.ControllerContext = context;

        //    //Act
        //    IActionResult result = await controller.Revoke();

        //    //Assert
        //    Assert.NotNull(result);
        //    Assert.IsType<OkObjectResult>(result);
        //}
        //[Fact]
        //public async Task TestRevokeUserNotFoundController()
        //{
        //    Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
        //    Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
        //    Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
        //    Mock<ITokenManager> mockTokenManager = new Mock<ITokenManager>();
        //    mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://localhost:5031");
        //    var idenetity = new ClaimsIdentity();
        //    idenetity.AddClaim(new Claim(ClaimTypes.Name, "rainsoft"));
        //    var claimsPrincipal = new ClaimsPrincipal(idenetity);

        //    var userProfile = new UserProfile();
        //    userProfile.Id = 1;
        //    userProfile.UserName = "rainsoft";

        //    var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        //    handlerMock
        //       .Protected()
        //       // Setup the PROTECTED method to mock
        //       .Setup<Task<HttpResponseMessage>>(
        //          "SendAsync",
        //          ItExpr.IsAny<HttpRequestMessage>(),
        //          ItExpr.IsAny<CancellationToken>()
        //       )
        //       // prepare the expected response of the mocked http call
        //       .ReturnsAsync(new HttpResponseMessage()
        //       {
        //           StatusCode = HttpStatusCode.NotFound,
        //           Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(userProfile), Encoding.UTF8, "application/json"),
        //       })
        //       .Verifiable();
        //    var httpClient = new HttpClient(handlerMock.Object)
        //    {
        //        BaseAddress = new Uri("http://test.com/"),
        //    };

        //    httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

        //    mockTokenService.Setup(x => x.GetPrincipalFromExpiredToken(It.IsAny<string>())).ReturnsAsync(claimsPrincipal);

        //    var httpContext = new Mock<HttpContext>();
        //    httpContext.Setup(x => x.User).Returns(claimsPrincipal);
        //    var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

        //    var controller = new TokenController(httpClientFactory.Object, mockConfiguration.Object, mockTokenService.Object, mockTokenManager.Object);

        //    controller.ControllerContext = context;

        //    //Act
        //    IActionResult result = await controller.Revoke();


        //    //Assert
        //    Assert.NotNull(result);
        //    Assert.IsType<OkObjectResult>(result);
        //}
        [Fact]
        public async Task TestAuthorizeWhenCustomerIsNullController()
        {
            var mockTokenService = new Mock<ITokenService>();
            var httpClientFactory = new Mock<IHttpClientFactory>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenManager = new Mock<ITokenManager>();

            mockConfiguration.SetupGet(expression: x => x["Token:RefreshTokenExpiryInMinutes"]).Returns(value: "10");
            //mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://localhost:5031");

            var idenetity = new ClaimsIdentity();
            idenetity.AddClaim(claim: new Claim(type: ClaimTypes.Name,
                                                value: "rainsoft"));
            var claimsPrincipal = new ClaimsPrincipal(identity: idenetity);
            var jwtSecurityToken = new JwtSecurityToken(issuer: It.IsAny<string>(),
                                                        audience: It.IsAny<string>(),
                                                        expires: It.IsAny<DateTime?>(),
                                                        signingCredentials: It.IsAny<SigningCredentials>(),
                                                        claims: It.IsAny<IEnumerable<Claim>>());

            var contact = new Contact();
            contact.Id = 1;
            contact.FirstName = "System";
            contact.LastName = "Administrator";

            var lstCustomer = new List<Customer>();
            //Customer customer = new Customer();
            //customer.Contact = contact;

            //lstCustomer.Add(customer);

            var employees = new List<Employee>();
            var employee = new Employee();
            employee.Id = 1;
            employee.ContactId = 1;
            employee.Contact = contact;

            employees.Add(item: employee);

            var userProfile = new UserProfile();
            userProfile.Id = 1;
            userProfile.UserName = "rainsoft";
            userProfile.Customers = lstCustomer;
            userProfile.Employees = employees;

            var handlerMock = new Mock<HttpMessageHandler>(behavior: MockBehavior.Strict);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                                                  methodOrPropertyName: "SendAsync",
                                                  ItExpr.IsAny<HttpRequestMessage>(),
                                                  ItExpr.IsAny<CancellationToken>()
                                                 )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(value: new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(content: JsonConvert.SerializeObject(value: userProfile),
                                                                     encoding: Encoding.UTF8,
                                                                     mediaType: "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(handler: handlerMock.Object)
            {
                BaseAddress = new Uri(uriString: "http://test.com/"),
            };

            httpClientFactory.Setup(expression: x => x.CreateClient(It.IsAny<string>())).Returns(value: httpClient);

            mockTokenService.Setup(expression: x => x.GetPrincipalFromExpiredToken(It.IsAny<string>())).ReturnsAsync(value: claimsPrincipal);
            mockTokenService.Setup(expression: x => x.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>())).ReturnsAsync(value: jwtSecurityToken);

            var httpContext = new Mock<HttpContext>();

            var context = new ControllerContext(context: new ActionContext(httpContext: httpContext.Object,
                                                                           routeData: new RouteData(),
                                                                           actionDescriptor: new ControllerActionDescriptor()));

            var controller = new TokenController(clientFactory: httpClientFactory.Object,
                                                 configuration: mockConfiguration.Object,
                                                 tokenService: mockTokenService.Object,
                                                 tokenManager: mockTokenManager.Object);

            controller.ControllerContext = context;

            //Act
            var generateTokenRequest = new GenerateTokenRequest();
            generateTokenRequest.UserName = "rainsoft";
            generateTokenRequest.Password = "rainsoft";
            generateTokenRequest.Employee = true;
            var result = await controller.Authorize(request: generateTokenRequest);

            //Assert
            Assert.NotNull(@object: result);
        }


        //[Fact]
        //public async Task TestAuthorizeController2()
        //{
        //    Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
        //    Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
        //    Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
        //    Mock<ITokenManager> mockTokenManager = new Mock<ITokenManager>();
        //    mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://localhost:5031");
        //    var idenetity = new ClaimsIdentity();
        //    idenetity.AddClaim(new Claim(ClaimTypes.Name, "rainsoft"));
        //    var claimsPrincipal = new ClaimsPrincipal(idenetity);
        //    var jwtSecurityToken = new JwtSecurityToken(issuer: It.IsAny<string>(),
        //                                     audience: It.IsAny<string>(),
        //                                     expires: It.IsAny<DateTime?>(),
        //                                     signingCredentials: It.IsAny<SigningCredentials>(),
        //                                     claims: It.IsAny<IEnumerable<Claim>>());

        //    Contact contact = new Contact();
        //    contact.Id = 1;
        //    contact.FirstName = "System";
        //    contact.LastName = "Administrator";

        //    List<Customer> lstCustomer = new List<Customer>();
        //    Customer customer = new Customer();
        //    customer.Contact = contact;

        //    //lstCustomer.Add(customer);

        //    List<Employee> employees = new List<Employee>();
        //    Employee employee = new Employee();
        //    employee.Id = 1;
        //    employee.ContactId = 1;
        //    employee.Contact = contact;

        //    employees.Add(employee);

        //    var userProfile = new UserProfile();
        //    userProfile.Id = 1;
        //    userProfile.UserName = "rainsoft";
        //    userProfile.Customers = lstCustomer;
        //    userProfile.Employees = employees;

        //    var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        //    handlerMock
        //       .Protected()
        //       // Setup the PROTECTED method to mock
        //       .Setup<Task<HttpResponseMessage>>(
        //          "SendAsync",
        //          ItExpr.IsAny<HttpRequestMessage>(),
        //          ItExpr.IsAny<CancellationToken>()
        //       )
        //       // prepare the expected response of the mocked http call
        //       .ReturnsAsync(new HttpResponseMessage()
        //       {
        //           StatusCode = HttpStatusCode.OK,
        //           Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(userProfile), Encoding.UTF8, "application/json"),
        //       })
        //       .Verifiable();
        //    var httpClient = new HttpClient(handlerMock.Object)
        //    {
        //        BaseAddress = new Uri("http://test.com/"),
        //    };

        //    httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

        //    mockTokenService.Setup(x => x.GetPrincipalFromExpiredToken(It.IsAny<string>())).ReturnsAsync(claimsPrincipal);
        //    mockTokenService.Setup(x => x.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>())).ReturnsAsync(jwtSecurityToken);

        //    var httpContext = new Mock<HttpContext>();

        //    var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

        //    var controller = new TokenController(httpClientFactory.Object, mockConfiguration.Object, mockTokenService.Object, mockTokenManager.Object);

        //    controller.ControllerContext = context;

        //    //Act
        //    GenerateTokenRequest generateTokenRequest = new GenerateTokenRequest();
        //    generateTokenRequest.UserName = "rainsoft";
        //    generateTokenRequest.Password = "rainsoft";
        //    generateTokenRequest.Employee = true;
        //    IActionResult result = await controller.Authorize(generateTokenRequest);


        //    //Assert
        //    Assert.NotNull(result);
        //}
        [Fact]
        public async Task TestAuthorizeNotFoundController()
        {
            var mockTokenService = new Mock<ITokenService>();
            var httpClientFactory = new Mock<IHttpClientFactory>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenManager = new Mock<ITokenManager>();
            mockConfiguration.SetupGet(expression: x => x[It.IsAny<string>()]).Returns(value: "http://localhost:5031");
            var idenetity = new ClaimsIdentity();
            idenetity.AddClaim(claim: new Claim(type: ClaimTypes.Name,
                                                value: "rainsoft"));
            var claimsPrincipal = new ClaimsPrincipal(identity: idenetity);
            var jwtSecurityToken = new JwtSecurityToken(issuer: It.IsAny<string>(),
                                                        audience: It.IsAny<string>(),
                                                        expires: It.IsAny<DateTime?>(),
                                                        signingCredentials: It.IsAny<SigningCredentials>(),
                                                        claims: It.IsAny<IEnumerable<Claim>>());

            var contact = new Contact();
            contact.Id = 1;
            contact.FirstName = "System";
            contact.LastName = "Administrator";

            var lstCustomer = new List<Customer>();
            var customer = new Customer();
            customer.Contact = contact;

            lstCustomer.Add(item: customer);

            var employees = new List<Employee>();
            var employee = new Employee();
            employee.Id = 1;
            employee.ContactId = 1;
            employee.Contact = contact;

            var userProfile = new UserProfile();
            userProfile.Id = 1;
            userProfile.UserName = "rainsoft";
            userProfile.Customers = lstCustomer;
            userProfile.Employees = employees;

            var handlerMock = new Mock<HttpMessageHandler>(behavior: MockBehavior.Strict);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                                                  methodOrPropertyName: "SendAsync",
                                                  ItExpr.IsAny<HttpRequestMessage>(),
                                                  ItExpr.IsAny<CancellationToken>()
                                                 )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(value: new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent(content: JsonConvert.SerializeObject(value: userProfile),
                                                                     encoding: Encoding.UTF8,
                                                                     mediaType: "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(handler: handlerMock.Object)
            {
                BaseAddress = new Uri(uriString: "http://test.com/"),
            };

            httpClientFactory.Setup(expression: x => x.CreateClient(It.IsAny<string>())).Returns(value: httpClient);

            mockTokenService.Setup(expression: x => x.GetPrincipalFromExpiredToken(It.IsAny<string>())).ReturnsAsync(value: claimsPrincipal);
            mockTokenService.Setup(expression: x => x.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>())).ReturnsAsync(value: jwtSecurityToken);

            var httpContext = new Mock<HttpContext>();

            var context = new ControllerContext(context: new ActionContext(httpContext: httpContext.Object,
                                                                           routeData: new RouteData(),
                                                                           actionDescriptor: new ControllerActionDescriptor()));

            var controller = new TokenController(clientFactory: httpClientFactory.Object,
                                                 configuration: mockConfiguration.Object,
                                                 tokenService: mockTokenService.Object,
                                                 tokenManager: mockTokenManager.Object);

            controller.ControllerContext = context;

            //Act
            var generateTokenRequest = new GenerateTokenRequest();
            generateTokenRequest.UserName = "rainsoft";
            generateTokenRequest.Password = "rainsoft";
            generateTokenRequest.Employee = true;
            var result = await controller.Authorize(request: generateTokenRequest);

            //Assert
            Assert.NotNull(@object: result);
        }


        [Fact]
        public async Task TestAuthorizeEmployeeNullController()
        {
            var mockTokenService = new Mock<ITokenService>();
            var httpClientFactory = new Mock<IHttpClientFactory>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenManager = new Mock<ITokenManager>();

            mockConfiguration.SetupGet(expression: x => x["Token:RefreshTokenExpiryInMinutes"]).Returns(value: "10");
            //mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://localhost:5031");

            var idenetity = new ClaimsIdentity();
            idenetity.AddClaim(claim: new Claim(type: ClaimTypes.Name,
                                                value: "rainsoft"));
            var claimsPrincipal = new ClaimsPrincipal(identity: idenetity);
            var jwtSecurityToken = new JwtSecurityToken(issuer: It.IsAny<string>(),
                                                        audience: It.IsAny<string>(),
                                                        expires: It.IsAny<DateTime?>(),
                                                        signingCredentials: It.IsAny<SigningCredentials>(),
                                                        claims: It.IsAny<IEnumerable<Claim>>());

            var contact = new Contact();
            contact.Id = 1;
            contact.FirstName = "System";
            contact.LastName = "Administrator";

            var lstCustomer = new List<Customer>();
            var customer = new Customer();
            customer.Contact = contact;

            lstCustomer.Add(item: customer);

            var employees = new List<Employee>();
            var employee = new Employee();
            employee.Id = 1;
            employee.ContactId = 1;
            employee.Contact = contact;

            var userProfile = new UserProfile();
            userProfile.Id = 1;
            userProfile.UserName = "rainsoft";
            userProfile.Customers = lstCustomer;
            //userProfile.Employees = employees;
            userProfile.Employees = new List<Employee>();

            var handlerMock = new Mock<HttpMessageHandler>(behavior: MockBehavior.Strict);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                                                  methodOrPropertyName: "SendAsync",
                                                  ItExpr.IsAny<HttpRequestMessage>(),
                                                  ItExpr.IsAny<CancellationToken>()
                                                 )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(value: new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(content: JsonConvert.SerializeObject(value: userProfile),
                                                                     encoding: Encoding.UTF8,
                                                                     mediaType: "application/json"),
                })
                .Verifiable();
            var httpClient = new HttpClient(handler: handlerMock.Object)
            {
                BaseAddress = new Uri(uriString: "http://test.com/"),
            };

            httpClientFactory.Setup(expression: x => x.CreateClient(It.IsAny<string>())).Returns(value: httpClient);

            mockTokenService.Setup(expression: x => x.GetPrincipalFromExpiredToken(It.IsAny<string>())).ReturnsAsync(value: claimsPrincipal);
            mockTokenService.Setup(expression: x => x.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>())).ReturnsAsync(value: jwtSecurityToken);

            var httpContext = new Mock<HttpContext>();

            var context = new ControllerContext(context: new ActionContext(httpContext: httpContext.Object,
                                                                           routeData: new RouteData(),
                                                                           actionDescriptor: new ControllerActionDescriptor()));

            var controller = new TokenController(clientFactory: httpClientFactory.Object,
                                                 configuration: mockConfiguration.Object,
                                                 tokenService: mockTokenService.Object,
                                                 tokenManager: mockTokenManager.Object);

            controller.ControllerContext = context;

            //Act
            var generateTokenRequest = new GenerateTokenRequest();
            generateTokenRequest.UserName = "rainsoft";
            generateTokenRequest.Password = "rainsoft";
            generateTokenRequest.Employee = true;
            var result = await controller.Authorize(request: generateTokenRequest);

            //Assert
            Assert.NotNull(@object: result);
        }


        //[Fact]
        //public async Task TestGenerateAccessTokenService()
        //{
        //    //Arrange
        //    DbContextOptions<IdentityContext> options;
        //    var builder = new DbContextOptionsBuilder<IdentityContext>();
        //    builder.UseInMemoryDatabase(databaseName: "Identity");
        //    options = builder.Options;
        //    using var dataContext = new IdentityContext(options: options);

        //    var mockKeyStoreService = new Mock<IKeyStoreService>();
        //    var mockConfiguration = new Mock<IConfiguration>();
        //    mockKeyStoreService.Setup(expression: x => x.GetJwtSecurityKeyAsync()).ReturnsAsync(value: "123");
        //    mockConfiguration.Setup(expression: x => x["Token:TimeoutInMinutes"]).Returns(value: "30");
        //    var usersClaims = new List<Claim>
        //                      {
        //                          new Claim(type: "UserProfileId",
        //                                    value: "1"),
        //                          new Claim(type: "UserName",
        //                                    value: "rainsoft"),
        //                      };

        //    //Act
        //    ITokenService tokenService = new TokenService(previousUow: new UnitOfWork<IdentityContext>(context: dataContext,
        //                                                                                               repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
        //                                                  services: null,
        //                                                  configuration: mockConfiguration.Object,
        //                                                  keyStoreService: mockKeyStoreService.Object);
        //    var result = await tokenService.GenerateAccessToken(claims: usersClaims);

        //    //Assert
        //    Assert.NotNull(@object: result);
        //}


        //[Fact]
        //public async void TestInsertToken()
        //{
        //    DbContextOptions<IdentityContext> options;
        //    var builder = new DbContextOptionsBuilder<IdentityContext>();
        //    builder.UseInMemoryDatabase(databaseName: "Identity");
        //    options = builder.Options;
        //    using var dataContext = new IdentityContext(options: options);

        //    var mockKeyStoreService = new Mock<IKeyStoreService>();
        //    var mockConfiguration = new Mock<IConfiguration>();
        //    mockKeyStoreService.Setup(expression: x => x.GetJwtSecurityKeyAsync()).ReturnsAsync(value: "123");
        //    mockConfiguration.Setup(expression: x => x["Token:TimeoutInMinutes"]).Returns(value: "30");
        //    var usersClaims = new List<Claim>
        //                      {
        //                          new Claim(type: "UserProfileId",
        //                                    value: "1"),
        //                          new Claim(type: "UserName",
        //                                    value: "rainsoft"),
        //                      };

        //    //Act
        //    ITokenService tokenService = new TokenService(previousUow: new UnitOfWork<IdentityContext>(context: dataContext,
        //                                                                                               repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
        //                                                  services: null,
        //                                                  configuration: mockConfiguration.Object,
        //                                                  keyStoreService: mockKeyStoreService.Object);
        //    await tokenService.InsertToken(tokenString: It.IsAny<string>(),
        //                                   newRefreshToken: It.IsAny<string>(),
        //                                   dateTime: DateTime.UtcNow,
        //                                   userId: 1);
        //}


        //[Fact]
        //public async void TestGetForSignOn()
        //{
        //    DbContextOptions<IdentityContext> options;
        //    var builder = new DbContextOptionsBuilder<IdentityContext>();
        //    builder.UseInMemoryDatabase(databaseName: "Identity");
        //    options = builder.Options;
        //    using var dataContext = new IdentityContext(options: options);

        //    var mockKeyStoreService = new Mock<IKeyStoreService>();
        //    var mockConfiguration = new Mock<IConfiguration>();
        //    mockKeyStoreService.Setup(expression: x => x.GetJwtSecurityKeyAsync()).ReturnsAsync(value: "123");
        //    mockConfiguration.Setup(expression: x => x["Token:TimeoutInMinutes"]).Returns(value: "30");
        //    var usersClaims = new List<Claim>
        //                      {
        //                          new Claim(type: "UserProfileId",
        //                                    value: "1"),
        //                          new Claim(type: "UserName",
        //                                    value: "rainsoft"),
        //                      };

        //    //Act
        //    var tokenString = "someTokenString";
        //    var refreshToken = "someRefreshToken";
        //    ITokenService tokenService = new TokenService(previousUow: new UnitOfWork<IdentityContext>(context: dataContext,
        //                                                                                               repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
        //                                                  services: null,
        //                                                  configuration: mockConfiguration.Object,
        //                                                  keyStoreService: mockKeyStoreService.Object);
        //    await tokenService.InsertToken(tokenString: tokenString,
        //                                   newRefreshToken: refreshToken,
        //                                   dateTime: DateTime.UtcNow,
        //                                   userId: 1);
        //    var result = tokenService.GetForSignOn(key: refreshToken);

        //    Assert.NotNull(@object: result);
        //}


        //[Fact]
        //public void TestGenerateRefreshTokenService()
        //{
        //    DbContextOptions<IdentityContext> options;
        //    var builder = new DbContextOptionsBuilder<IdentityContext>();
        //    builder.UseInMemoryDatabase(databaseName: "Identity");
        //    options = builder.Options;
        //    using var dataContext = new IdentityContext(options: options);
        //    ITokenService tokenService = new TokenService(previousUow: new UnitOfWork<IdentityContext>(context: dataContext,
        //                                                                                               repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
        //                                                  services: null,
        //                                                  configuration: null,
        //                                                  keyStoreService: null);
        //    var result = tokenService.GenerateRefreshToken();
        //}

        
        //[Fact]
        //public async Task GetPrincipalFromExpiredToken_ValidExpiredToken_ClaimsPrincipal()
        //{
        //    //Arrange
        //    DbContextOptions<IdentityContext> options;
        //    var builder = new DbContextOptionsBuilder<IdentityContext>();
        //    builder.UseInMemoryDatabase(databaseName: "Identity");
        //    options = builder.Options;
        //    using var dataContext = new IdentityContext(options: options);
        //    var mockKeyStoreService = new Mock<IKeyStoreService>();
        //    mockKeyStoreService.Setup(expression: x => x.GetJwtSecurityKeyAsync()).ReturnsAsync(value: "securityKeysecurityKeysecurityKeysecurityKey");

        //    //Act
        //    ITokenService tokenService = new TokenService(previousUow: new UnitOfWork<IdentityContext>(context: dataContext,
        //                                                                                               repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
        //                                                  services: null,
        //                                                  configuration: null,
        //                                                  keyStoreService: mockKeyStoreService.Object);
        //    var result = await tokenService.GetPrincipalFromExpiredToken(token: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNQ1UiLCJVc2VyUHJvZmlsZUlkIjoiMSIsIlVzZXJOYW1lIjoicmFpbnNvZnQiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicmFpbnNvZnQiLCJGaXJzdE5hbWUiOiJTeXN0ZW0iLCJMYXN0TmFtZSI6IkFkbWluaXN0cmF0b3IiLCJUZW5hbnRJZCI6IjEiLCJFbXBsb3llZUlkIjoiMSIsImV4cCI6MTYxMzM2MDQzMiwiaXNzIjoicmFpbnNvZnRmbiIsImF1ZCI6InJlYWRlcnMifQ.RWcDh3eYYFXLzBJ5ohcSk24EyjYFrRSN6d1dNqm_t-w");

        //    //Assert
        //    Assert.NotNull(@object: result);
        //}

        //[Fact]
        //public async Task GetPrincipalFromExpiredToken_SecurityAlgoInvalid_500()
        //{
        //    //Arrange
        //    DbContextOptions<IdentityContext> options;
        //    var builder = new DbContextOptionsBuilder<IdentityContext>();
        //    builder.UseInMemoryDatabase(databaseName: "Identity");
        //    options = builder.Options;
        //    using var dataContext = new IdentityContext(options: options);
        //    var mockKeyStoreService = new Mock<IKeyStoreService>();
        //    mockKeyStoreService.Setup(expression: x => x.GetJwtSecurityKeyAsync()).ReturnsAsync(value: "securityKeysecurityKeysecurityKeysecurityKey");

        //    //Act
        //    ITokenService tokenService = new TokenService(previousUow: new UnitOfWork<IdentityContext>(context: dataContext,
        //                                                                                               repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
        //                                                  services: null,
        //                                                  configuration: null,
        //                                                  keyStoreService: mockKeyStoreService.Object);
        //    var result = await tokenService.GetPrincipalFromExpiredToken(token: "eyJhbGciOiJIUzM4NCIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNQ1UiLCJVc2VyUHJvZmlsZUlkIjoiMSIsIlVzZXJOYW1lIjoicmFpbnNvZnQiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicmFpbnNvZnQiLCJGaXJzdE5hbWUiOiJTeXN0ZW0iLCJMYXN0TmFtZSI6IkFkbWluaXN0cmF0b3IiLCJUZW5hbnRJZCI6IjEiLCJFbXBsb3llZUlkIjoiMSIsImV4cCI6MTYxMzM2MDU3MiwiaXNzIjoicmFpbnNvZnRmbiIsImF1ZCI6InJlYWRlcnMifQ.xeDILifcP7REmQBzD4sYaTUDQWAXaMoKOGrtjfhErdcJommp28Q-rXlrwB59JNpg");

        //    //Assert
        //    Assert.NotNull(@object: result);
        //}


        [Fact]
        public async Task TestRevokeAllAuthTokensController()
        {
            var mockTokenService = new Mock<ITokenService>();
            var httpClientFactory = new Mock<IHttpClientFactory>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenManager = new Mock<ITokenManager>();
            var request = new Mock<HttpRequest>();

            request.SetupGet(expression: x => x.Headers["Authorization"]).Returns(
                                                                                  value: new StringValues(value: "Test")
                                                                                 );

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(expression: m => m.User.FindFirst("TenantId")).Returns(value: new Claim(type: "TenantId",
                                                                                                      value: "1"));
            httpContext.SetupGet(expression: x => x.Request).Returns(value: request.Object);

            var context = new ControllerContext(context: new ActionContext(httpContext: httpContext.Object,
                                                                           routeData: new RouteData(),
                                                                           actionDescriptor: new ControllerActionDescriptor()));

            var controller = new TokenController(clientFactory: httpClientFactory.Object,
                                                 configuration: mockConfiguration.Object,
                                                 tokenService: mockTokenService.Object,
                                                 tokenManager: mockTokenManager.Object);

            controller.ControllerContext = context;

            //Act

            var result = await controller.RevokeAllAuthTokens();

            //Assert
            Assert.NotNull(@object: result);
            Assert.IsType<OkResult>(@object: result);
        }


        [Fact]
        public async Task TestRevokeAllAuthTokensDoesNoteExistsController()
        {
            var mockTokenService = new Mock<ITokenService>();
            var httpClientFactory = new Mock<IHttpClientFactory>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenManager = new Mock<ITokenManager>();
            var request = new Mock<HttpRequest>();

            request.SetupGet(expression: x => x.Headers["Authorization"]).Returns(
                                                                                  value: new StringValues(value: string.Empty)
                                                                                 );

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(expression: m => m.User.FindFirst("TenantId")).Returns(value: new Claim(type: "TenantId",
                                                                                                      value: "1"));
            httpContext.SetupGet(expression: x => x.Request).Returns(value: request.Object);

            var context = new ControllerContext(context: new ActionContext(httpContext: httpContext.Object,
                                                                           routeData: new RouteData(),
                                                                           actionDescriptor: new ControllerActionDescriptor()));

            var controller = new TokenController(clientFactory: httpClientFactory.Object,
                                                 configuration: mockConfiguration.Object,
                                                 tokenService: mockTokenService.Object,
                                                 tokenManager: mockTokenManager.Object);

            controller.ControllerContext = context;

            //Act

            var result = await controller.RevokeAllAuthTokens();

            //Assert
            Assert.NotNull(@object: result);
            Assert.IsType<BadRequestObjectResult>(@object: result);
        }


        [Fact]
        public async Task TestRevokeAllAuthTokensNullableRequestController()
        {
            var mockTokenService = new Mock<ITokenService>();
            var httpClientFactory = new Mock<IHttpClientFactory>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenManager = new Mock<ITokenManager>();
            var request = new Mock<HttpRequest>();

            request.SetupGet(expression: x => x.Headers["Authorization"]);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(expression: m => m.User.FindFirst("TenantId")).Returns(value: new Claim(type: "TenantId",
                                                                                                      value: "1"));
            httpContext.SetupGet(expression: x => x.Request).Returns(value: request.Object);

            var context = new ControllerContext(context: new ActionContext(httpContext: httpContext.Object,
                                                                           routeData: new RouteData(),
                                                                           actionDescriptor: new ControllerActionDescriptor()));

            var controller = new TokenController(clientFactory: httpClientFactory.Object,
                                                 configuration: mockConfiguration.Object,
                                                 tokenService: mockTokenService.Object,
                                                 tokenManager: mockTokenManager.Object);

            controller.ControllerContext = context;

            //Act

            var result = await controller.RevokeAllAuthTokens();

            //Assert
            Assert.NotNull(@object: result);
            Assert.IsType<BadRequestObjectResult>(@object: result);
        }


        [Fact]
        public async Task TestRevokeAuthTokenController()
        {
            var mockTokenService = new Mock<ITokenService>();
            var httpClientFactory = new Mock<IHttpClientFactory>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenManager = new Mock<ITokenManager>();
            var request = new Mock<HttpRequest>();

            request.SetupGet(expression: x => x.Headers["Authorization"]).Returns(
                                                                                  value: new StringValues(value: "Test")
                                                                                 );

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(expression: m => m.User.FindFirst("TenantId")).Returns(value: new Claim(type: "TenantId",
                                                                                                      value: "1"));
            httpContext.SetupGet(expression: x => x.Request).Returns(value: request.Object);

            var context = new ControllerContext(context: new ActionContext(httpContext: httpContext.Object,
                                                                           routeData: new RouteData(),
                                                                           actionDescriptor: new ControllerActionDescriptor()));

            var controller = new TokenController(clientFactory: httpClientFactory.Object,
                                                 configuration: mockConfiguration.Object,
                                                 tokenService: mockTokenService.Object,
                                                 tokenManager: mockTokenManager.Object);

            controller.ControllerContext = context;

            //Act

            var result = await controller.RevokeAuthToken();

            //Assert
            Assert.NotNull(@object: result);
            Assert.IsType<OkResult>(@object: result);
        }


        [Fact]
        public async Task TestRevokeAuthTokenDoesNoteExistsController()
        {
            var mockTokenService = new Mock<ITokenService>();
            var httpClientFactory = new Mock<IHttpClientFactory>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenManager = new Mock<ITokenManager>();
            var request = new Mock<HttpRequest>();

            request.SetupGet(expression: x => x.Headers["Authorization"]).Returns(
                                                                                  value: new StringValues(value: string.Empty)
                                                                                 );

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(expression: m => m.User.FindFirst("TenantId")).Returns(value: new Claim(type: "TenantId",
                                                                                                      value: "1"));
            httpContext.SetupGet(expression: x => x.Request).Returns(value: request.Object);

            var context = new ControllerContext(context: new ActionContext(httpContext: httpContext.Object,
                                                                           routeData: new RouteData(),
                                                                           actionDescriptor: new ControllerActionDescriptor()));

            var controller = new TokenController(clientFactory: httpClientFactory.Object,
                                                 configuration: mockConfiguration.Object,
                                                 tokenService: mockTokenService.Object,
                                                 tokenManager: mockTokenManager.Object);

            controller.ControllerContext = context;

            //Act

            var result = await controller.RevokeAuthToken();

            //Assert
            Assert.NotNull(@object: result);
            Assert.IsType<BadRequestObjectResult>(@object: result);
        }


        [Fact]
        public async Task TestRevokeAuthTokenNullableRequestController()
        {
            var mockTokenService = new Mock<ITokenService>();
            var httpClientFactory = new Mock<IHttpClientFactory>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenManager = new Mock<ITokenManager>();
            var request = new Mock<HttpRequest>();

            request.SetupGet(expression: x => x.Headers["Authorization"]);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(expression: m => m.User.FindFirst("TenantId")).Returns(value: new Claim(type: "TenantId",
                                                                                                      value: "1"));
            httpContext.SetupGet(expression: x => x.Request).Returns(value: request.Object);

            var context = new ControllerContext(context: new ActionContext(httpContext: httpContext.Object,
                                                                           routeData: new RouteData(),
                                                                           actionDescriptor: new ControllerActionDescriptor()));

            var controller = new TokenController(clientFactory: httpClientFactory.Object,
                                                 configuration: mockConfiguration.Object,
                                                 tokenService: mockTokenService.Object,
                                                 tokenManager: mockTokenManager.Object);

            controller.ControllerContext = context;

            //Act

            var result = await controller.RevokeAuthToken();

            //Assert
            Assert.NotNull(@object: result);
            Assert.IsType<BadRequestObjectResult>(@object: result);
        }
        //[Fact]
        //public async Task TestGetPrincipalFromExpiredTokenExceptionService()
        //{
        //    //Arrange
        //    Mock<IKeyStoreService> mockKeyStoreService = new Mock<IKeyStoreService>();
        //    mockKeyStoreService.Setup(x => x.GetJwtSecurityKeyAsync()).ReturnsAsync("this_is_our_supper_long_security_key_for_token_vortex$rainsoft");


        //    //Act
        //    ITokenService tokenService = new TokenService(null, null, null, mockKeyStoreService.Object);
        //    await Assert.ThrowsAsync<SecurityTokenException>(async () => { await tokenService.GetPrincipalFromExpiredToken("eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzM4NCJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE2MTE4MzM0MzUsImV4cCI6MTY0MzM2OTQzNSwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIkdpdmVuTmFtZSI6IkpvaG5ueSIsIlN1cm5hbWUiOiJSb2NrZXQiLCJFbWFpbCI6Impyb2NrZXRAZXhhbXBsZS5jb20iLCJSb2xlIjpbIk1hbmFnZXIiLCJQcm9qZWN0IEFkbWluaXN0cmF0b3IiXX0.dUoL8AS3dGrfEVBzzM8LSlV2yeAy3-1_C-f6eFxBqTrylwVb8hnhpwc9hy0voRH6"); });

        //}



        [Fact()]
        public async Task SingleSignOn_RefreshTokenFound_Ok()
        {
            var mockTokenService = new Mock<ITokenService>();
            var httpClientFactory = new Mock<IHttpClientFactory>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenManager = new Mock<ITokenManager>();

            var controller = new TokenController(clientFactory: httpClientFactory.Object,
                                                 configuration: mockConfiguration.Object,
                                                 tokenService: mockTokenService.Object,
                                                 tokenManager: mockTokenManager.Object);

            var tokenData = new TokenData
            {
                ValidTo = DateTime.Parse(s: "2020-01-27"),
                ValidFrom = DateTime.Parse(s: "2020-01-27"),
                Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE1ODAwODMyMDAsImV4cCI6MTYwOTQ1OTIwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.Vphx6HtU-yQAPx7oHWnOq91fLIE4coErE8xYUfdkk84",
                RefreshToken = "refreshToken"
            };

            mockTokenManager.Setup(tokenManager => tokenManager.GetForSignOn(It.IsAny<string>())).ReturnsAsync(tokenData);

            //Act
            var result = await controller.SingleSignOn(key: "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzM4NCJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE2MTE4MzM0MzUsImV4cCI6MTY0MzM2OTQzNSwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIkdpdmVuTmFtZSI6IkpvaG5ueSIsIlN1cm5hbWUiOiJSb2NrZXQiLCJFbWFpbCI6Impyb2NrZXRAZXhhbXBsZS5jb20iLCJSb2xlIjpbIk1hbmFnZXIiLCJQcm9qZWN0IEFkbWluaXN0cmF0b3IiXX0.dUoL8AS3dGrfEVBzzM8LSlV2yeAy3-1_C-f6eFxBqTrylwVb8hnhpwc9hy0voRH6");

            //Assert
            Assert.NotNull(@object: result);
            Assert.IsType<OkObjectResult>(@object: result);
        }

        [Fact]
        public async Task SingleSignOn_RefreshTokenNotFound_BadRequest()
        {
            //Arrange
            var mockTokenService = new Mock<ITokenService>();
            var httpClientFactory = new Mock<IHttpClientFactory>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenManager = new Mock<ITokenManager>();

            //var fakeToken = new Token();
            //mockTokenService.Setup(expression: x => x.GetForSignOn(It.IsAny<string>())).ReturnsAsync(value: fakeToken);

            var controller = new TokenController(clientFactory: httpClientFactory.Object,
                                                 configuration: mockConfiguration.Object,
                                                 tokenService: mockTokenService.Object,
                                                 tokenManager: mockTokenManager.Object);

            //Act
            var result = await controller.SingleSignOn(key: "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzM4NCJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE2MTE4MzM0MzUsImV4cCI6MTY0MzM2OTQzNSwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIkdpdmVuTmFtZSI6IkpvaG5ueSIsIlN1cm5hbWUiOiJSb2NrZXQiLCJFbWFpbCI6Impyb2NrZXRAZXhhbXBsZS5jb20iLCJSb2xlIjpbIk1hbmFnZXIiLCJQcm9qZWN0IEFkbWluaXN0cmF0b3IiXX0.dUoL8AS3dGrfEVBzzM8LSlV2yeAy3-1_C-f6eFxBqTrylwVb8hnhpwc9hy0voRH6");

            //Assert
            Assert.NotNull(@object: result);
            Assert.IsType<BadRequestObjectResult>(@object: result);
        }

        [Fact]
        public async Task Refresh_ValidTokenPair_Success()
        {
            //DbContextOptions<IdentityContext> options;
            //var builder = new DbContextOptionsBuilder<IdentityContext>();
            //builder.UseInMemoryDatabase(databaseName: "Identity");
            //options = builder.Options;
            //await using var dataContext = new IdentityContext(options: options);

            var expiredTokenPair = new TokenData
            {
                ValidTo = DateTime.Parse(s: "2020-01-27"),
                ValidFrom = DateTime.Parse(s: "2020-01-27"),
                Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE1ODAwODMyMDAsImV4cCI6MTYwOTQ1OTIwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.Vphx6HtU-yQAPx7oHWnOq91fLIE4coErE8xYUfdkk84",
                RefreshToken = "refreshToken"
            };

            //var uowMock = new Mock<IUnitOfWork<IdentityContext>>();
            //List<Token> tokenList = new EditableList<Token>();

            var refreshToken = expiredTokenPair.RefreshToken;
            var accessToken = expiredTokenPair.Token;

            //var tokenRepoMock = new Mock<ITrackableRepository<Token>>();
            //tokenRepoMock.Setup(expression: x => x.Query(x => x.RefreshToken == refreshToken && x.AccessToken == accessToken && x.RefreshTokenExpiry >= DateTime.UtcNow)).Returns(value: tokenList.AsQueryable());

            //uowMock.Setup(expression: x => x.Repository<Token>()).Returns(value: tokenRepoMock.Object);

            var httpClientFactory = new Mock<IHttpClientFactory>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenManager = new Mock<ITokenManager>();

            var mockTokenService = new Mock<ITokenService>();

            mockConfiguration.SetupGet(expression: configuration => configuration["Token:RefreshTokenExpiryInMinutes"]).Returns(value: "10");

            var identity = new ClaimsIdentity();
            identity.AddClaim(claim: new Claim(type: ClaimTypes.Name,
                                               value: "rainsoft"));
            identity.AddClaim(claim: new Claim(type: "UserProfileId",
                                               value: "1"));
            var claimsPrincipal = new ClaimsPrincipal(identity: identity);

            var userProfile = new UserProfile();
            userProfile.Id = 1;
            userProfile.UserName = "rainsoft";

            var handlerMock = new Mock<HttpMessageHandler>(behavior: MockBehavior.Strict);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                                                  methodOrPropertyName: "SendAsync",
                                                  ItExpr.IsAny<HttpRequestMessage>(),
                                                  ItExpr.IsAny<CancellationToken>()
                                                 )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(value: new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(content: JsonConvert.SerializeObject(value: userProfile),
                                                                     encoding: Encoding.UTF8,
                                                                     mediaType: "application/json"),
                })
                .Verifiable();

            var httpClient = new HttpClient(handler: handlerMock.Object)
            {
                BaseAddress = new Uri(uriString: "http://test.com/"),
            };

            httpClientFactory.Setup(expression: x => x.CreateClient(It.IsAny<string>())).Returns(value: httpClient);

            mockTokenService.Setup(expression: x => x.GetPrincipalFromExpiredToken(It.IsAny<string>())).ReturnsAsync(value: claimsPrincipal);
            var jwtSecurityToken = new JwtSecurityToken(issuer: It.IsAny<string>(),
                                                        audience: It.IsAny<string>(),
                                                        expires: It.IsAny<DateTime?>(),
                                                        signingCredentials: It.IsAny<SigningCredentials>(),
                                                        claims: It.IsAny<IEnumerable<Claim>>());

            mockTokenService.Setup(expression: x => x.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>())).ReturnsAsync(value: jwtSecurityToken);
            mockTokenService.Setup(expression: x => x.GenerateRefreshToken()).Returns(value: "Jb0emTyjwDAawFrkZSNTV+JbQ4N/Nx986/z+ww4RD50=");

            mockTokenManager.Setup(expression: x => x.CheckPair(It.Is<string>(s => s != null),
                                                                It.Is<string>(s => s != null))).ReturnsAsync(value: true);

            var httpContext = new Mock<HttpContext>();

            var context = new ControllerContext(context: new ActionContext(httpContext: httpContext.Object,
                                                                           routeData: new RouteData(),
                                                                           actionDescriptor: new ControllerActionDescriptor()));


            var controller = new TokenController(clientFactory: httpClientFactory.Object,
                                                 configuration: mockConfiguration.Object,
                                                 tokenService: mockTokenService.Object,
                                                 tokenManager: mockTokenManager.Object);

            controller.ControllerContext = context;

            //Act
            var refreshTokenRequest = new RefreshTokenRequest();
            refreshTokenRequest.Token = accessToken;
            refreshTokenRequest.RefreshToken = refreshToken;
            var result = await controller.Refresh(request: refreshTokenRequest);

            //Assert
            Assert.NotNull(@object: result);
            var res = Assert.IsType<OkObjectResult>(@object: result);
            var s = Assert.IsType<ApiResponse>(@object: res.Value);
            Assert.Equal(expected: ApiResponse.ApiResponseStatus.Success,
                         actual: s.Status);
        }

        [Fact]
        public async Task Refresh_WithInvalidRefreshToken_Failure()

        {
            var expiredTokenPair = new TokenData
            {
                ValidTo = DateTime.Parse(s: "2020-01-27"),
                ValidFrom = DateTime.Parse(s: "2020-01-27"),
                Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE1ODAwODMyMDAsImV4cCI6MTYwOTQ1OTIwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.Vphx6HtU-yQAPx7oHWnOq91fLIE4coErE8xYUfdkk84",
                RefreshToken = "refreshToken"
            };

            var httpClientFactory = new Mock<IHttpClientFactory>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenManager = new Mock<ITokenManager>();
            var mockTokenService = new Mock<ITokenService>();

            mockConfiguration.SetupGet(expression: configuration => configuration["Token:RefreshTokenExpiryInMinutes"]).Returns(value: "10");

            var userProfile = new UserProfile
            {
                Id = 1,
                UserName = "rainsoft"
            };

            var handlerMock = new Mock<HttpMessageHandler>(behavior: MockBehavior.Strict);
            handlerMock.Protected()
                       // Setup the PROTECTED method to mock
                       .Setup<Task<HttpResponseMessage>>(
                                                         methodOrPropertyName: "SendAsync",
                                                         ItExpr.IsAny<HttpRequestMessage>(),
                                                         ItExpr.IsAny<CancellationToken>()
                                                        )
                       // prepare the expected response of the mocked http call
                       .ReturnsAsync(value: new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(content: JsonConvert.SerializeObject(value: userProfile),
                                                                            encoding: Encoding.UTF8,
                                                                            mediaType: "application/json"),
                       })
                       .Verifiable();

            var httpClient = new HttpClient(handler: handlerMock.Object)
            {
                BaseAddress = new Uri(uriString: "http://test.com/"),
            };

            httpClientFactory.Setup(expression: x => x.CreateClient(It.IsAny<string>())).Returns(value: httpClient);

            var identity = new ClaimsIdentity();
            identity.AddClaim(claim: new Claim(type: ClaimTypes.Name,
                                               value: "rainsoft"));
            identity.AddClaim(claim: new Claim(type: "UserProfileId",
                                               value: "1"));
            var claimsPrincipal = new ClaimsPrincipal(identity: identity);

            mockTokenService.Setup(expression: x => x.GetPrincipalFromExpiredToken(It.IsAny<string>())).ReturnsAsync(value: claimsPrincipal);
            var jwtSecurityToken = new JwtSecurityToken(issuer: It.IsAny<string>(),
                                                        audience: It.IsAny<string>(),
                                                        expires: It.IsAny<DateTime?>(),
                                                        signingCredentials: It.IsAny<SigningCredentials>(),
                                                        claims: It.IsAny<IEnumerable<Claim>>());

            mockTokenService.Setup(expression: x => x.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>())).ReturnsAsync(value: jwtSecurityToken);
            mockTokenService.Setup(expression: x => x.GenerateRefreshToken()).Returns(value: "Jb0emTyjwDAawFrkZSNTV+JbQ4N/Nx986/z+ww4RD50=");
            mockTokenManager.Setup(expression: x => x.CheckPair(It.Is<string>(s => s != null),
                                                                It.Is<string>(s => s != null))).ReturnsAsync(value: false);

            var httpContext = new Mock<HttpContext>();

            var context = new ControllerContext(context: new ActionContext(httpContext: httpContext.Object,
                                                                           routeData: new RouteData(),
                                                                           actionDescriptor: new ControllerActionDescriptor()));



            var controller = new TokenController(clientFactory: httpClientFactory.Object,
                                                 configuration: mockConfiguration.Object,
                                                 tokenService: mockTokenService.Object,
                                                 tokenManager: mockTokenManager.Object);

            controller.ControllerContext = context;

            //Act
            var refreshTokenRequest = new RefreshTokenRequest();
            refreshTokenRequest.Token = expiredTokenPair.Token;
            refreshTokenRequest.RefreshToken = expiredTokenPair.RefreshToken;
            var result = await controller.Refresh(request: refreshTokenRequest);

            //Assert
            Assert.NotNull(@object: result);
            var res = Assert.IsType<BadRequestObjectResult>(@object: result);
            var s = Assert.IsType<ApiResponse>(@object: res.Value);
            Assert.Equal(expected: ApiResponse.ApiResponseStatus.Fail,
                         actual: s.Status);
        }

        [Fact()]
        public async Task Refresh_UserDoesNotExists_Fail()
        {
            var expiredTokenPair = new TokenData
            {
                ValidTo = DateTime.Parse(s: "2020-01-27"),
                ValidFrom = DateTime.Parse(s: "2020-01-27"),
                Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE1ODAwODMyMDAsImV4cCI6MTYwOTQ1OTIwMCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIlVzZXJQcm9maWxlSWQiOiIxIiwiVGVuYW50SWQiOiIxIn0.Vphx6HtU-yQAPx7oHWnOq91fLIE4coErE8xYUfdkk84",
                RefreshToken = "refreshToken"
            };

            var httpClientFactory = new Mock<IHttpClientFactory>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockTokenManager = new Mock<ITokenManager>();
            var mockTokenService = new Mock<ITokenService>();

            mockConfiguration.SetupGet(expression: configuration => configuration["Token:RefreshTokenExpiryInMinutes"]).Returns(value: "10");

            var userProfile = new UserProfile
            {
                Id = 1,
                UserName = "rainsoft"
            };

            var handlerMock = new Mock<HttpMessageHandler>(behavior: MockBehavior.Strict);
            handlerMock.Protected()
                       // Setup the PROTECTED method to mock
                       .Setup<Task<HttpResponseMessage>>(
                                                         methodOrPropertyName: "SendAsync",
                                                         ItExpr.IsAny<HttpRequestMessage>(),
                                                         ItExpr.IsAny<CancellationToken>()
                                                        )
                       // prepare the expected response of the mocked http call
                       .ReturnsAsync(value: new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.BadRequest,
                           Content = new StringContent(content: JsonConvert.SerializeObject(value: userProfile),
                                                                            encoding: Encoding.UTF8,
                                                                            mediaType: "application/json"),
                       })
                       .Verifiable();

            var httpClient = new HttpClient(handler: handlerMock.Object)
            {
                BaseAddress = new Uri(uriString: "http://test.com/"),
            };

            httpClientFactory.Setup(expression: x => x.CreateClient(It.IsAny<string>())).Returns(value: httpClient);

            var identity = new ClaimsIdentity();
            identity.AddClaim(claim: new Claim(type: ClaimTypes.Name,
                                               value: "rainsoft"));
            identity.AddClaim(claim: new Claim(type: "UserProfileId",
                                               value: "1"));
            var claimsPrincipal = new ClaimsPrincipal(identity: identity);

            mockTokenService.Setup(expression: x => x.GetPrincipalFromExpiredToken(It.IsAny<string>())).ReturnsAsync(value: claimsPrincipal);
            var jwtSecurityToken = new JwtSecurityToken(issuer: It.IsAny<string>(),
                                                        audience: It.IsAny<string>(),
                                                        expires: It.IsAny<DateTime?>(),
                                                        signingCredentials: It.IsAny<SigningCredentials>(),
                                                        claims: It.IsAny<IEnumerable<Claim>>());

            mockTokenService.Setup(expression: x => x.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>())).ReturnsAsync(value: jwtSecurityToken);
            mockTokenService.Setup(expression: x => x.GenerateRefreshToken()).Returns(value: "Jb0emTyjwDAawFrkZSNTV+JbQ4N/Nx986/z+ww4RD50=");
            mockTokenManager.Setup(expression: x => x.CheckPair(It.Is<string>(s => s != null),
                                                                It.Is<string>(s => s != null))).ReturnsAsync(value: true);

            var httpContext = new Mock<HttpContext>();

            var context = new ControllerContext(context: new ActionContext(httpContext: httpContext.Object,
                                                                           routeData: new RouteData(),
                                                                           actionDescriptor: new ControllerActionDescriptor()));



            var controller = new TokenController(clientFactory: httpClientFactory.Object,
                                                 configuration: mockConfiguration.Object,
                                                 tokenService: mockTokenService.Object,
                                                 tokenManager: mockTokenManager.Object);

            controller.ControllerContext = context;

            //Act
            var refreshTokenRequest = new RefreshTokenRequest();
            refreshTokenRequest.Token = expiredTokenPair.Token;
            refreshTokenRequest.RefreshToken = expiredTokenPair.RefreshToken;
            var result = await controller.Refresh(request: refreshTokenRequest);

            //Assert
            Assert.NotNull(@object: result);
            
            var res = Assert.IsType<BadRequestObjectResult>(@object: result);
            var s = Assert.IsType<ApiResponse>(@object: res.Value);
            Assert.Equal(expected: ApiResponse.ApiResponseStatus.Fail,
                         actual: s.Status);
        }







        //[Fact]
        //public async Task TestRefreshUserNullController()
        //{
        //    Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
        //    Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
        //    Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
        //    Mock<ITokenManager> mockTokenManager = new Mock<ITokenManager>();
        //    mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://localhost:5031");
        //    var idenetity = new ClaimsIdentity();
        //    idenetity.AddClaim(new Claim(ClaimTypes.Name, "rainsoft1"));
        //    var claimsPrincipal = new ClaimsPrincipal(idenetity);

        //    var userProfile = new UserProfile();
        //    userProfile.Id = 1;
        //    userProfile.UserName = "rainsoft";

        //    var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        //    handlerMock
        //       .Protected()
        //       // Setup the PROTECTED method to mock
        //       .Setup<Task<HttpResponseMessage>>(
        //          "SendAsync",
        //          ItExpr.IsAny<HttpRequestMessage>(),
        //          ItExpr.IsAny<CancellationToken>()
        //       )
        //       // prepare the expected response of the mocked http call
        //       .ReturnsAsync(new HttpResponseMessage()
        //       {
        //           StatusCode = HttpStatusCode.BadRequest,
        //           Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(userProfile), Encoding.UTF8, "application/json"),
        //       })
        //       .Verifiable();
        //    var httpClient = new HttpClient(handlerMock.Object)
        //    {
        //        BaseAddress = new Uri("http://test.com/"),
        //    };

        //    httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

        //    mockTokenService.Setup(x => x.GetPrincipalFromExpiredToken(It.IsAny<string>())).ReturnsAsync(claimsPrincipal);

        //    var jwtSecurityToken = new JwtSecurityToken(issuer: It.IsAny<string>(),
        //                                   audience: It.IsAny<string>(),
        //                                   expires: It.IsAny<DateTime?>(),
        //                                   signingCredentials: It.IsAny<SigningCredentials>(),
        //                                   claims: It.IsAny<IEnumerable<Claim>>());

        //    mockTokenService.Setup(x => x.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>())).ReturnsAsync(jwtSecurityToken);

        //    TokenPair jwtToken = new TokenPair();
        //    jwtToken.JwtToken = "Token";
        //    jwtToken.RefreshToken = "RefreshToken";
        //    List<TokenPair> tokenPairs = new List<TokenPair>();
        //    tokenPairs.Add(jwtToken);

        //    //TokenPair refreshToken = new TokenPair();
        //    //refreshToken.RefreshToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNQ1UiLCJVc2VyUHJvZmlsZUlkIjoiNjMxMCIsIlVzZXJOYW1lIjoiZGFuaXNoIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6ImRhbmlzaCIsIkZpcnN0TmFtZSI6IkRhbmlzaCIsIkxhc3ROYW1lIjoiRmFpeiIsIlRlbmFudElkIjoiMSIsIkVtcGxveWVlSWQiOiI2NyIsImV4cCI6MTU5ODg1OTExMiwiaXNzIjoicmFpbnNvZnRmbiIsImF1ZCI6InJlYWRlcnMifQ.9FJGVJoTkB_hQ-P9aorzjgdkZ82dYnGcexCy-bpQYdg";

        //    //tokenPairs.Add(refreshToken);

        //    TokenService.RefreshTokens.Add("rainsoft1", tokenPairs);

        //    var httpContext = new Mock<HttpContext>();

        //    var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

        //    var controller = new TokenController(httpClientFactory.Object, mockConfiguration.Object, mockTokenService.Object, mockTokenManager.Object);

        //    controller.ControllerContext = context;

        //    //Act
        //    RefreshTokenRequest refreshTokenRequest = new RefreshTokenRequest();
        //    refreshTokenRequest.Token = "Token";
        //    refreshTokenRequest.RefreshToken = "RefreshToken";
        //    IActionResult result = await controller.Refresh(refreshTokenRequest);

        //    //Assert
        //    Assert.NotNull(result);

        //    var res = Assert.IsType<OkObjectResult>(result);
        //    var s = Assert.IsType<ApiResponse>(res.Value);
        //    Assert.Equal("Fail", s.Status);


        //}
    }
}