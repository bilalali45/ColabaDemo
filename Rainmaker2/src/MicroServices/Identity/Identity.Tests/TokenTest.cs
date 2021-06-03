using Identity.Controllers;
using Identity.Data;
using Identity.Models;
using Identity.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TokenCacheHelper.TokenManager;
using URF.Core.Abstractions;
using Xunit;

namespace Identity.Tests
{
    public class TokenTest
    {
        [Fact]
        public async Task TestRefreshController()
        {
            Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
            Mock<ITenantConfigService> mockTenantConfig = new Mock<ITenantConfigService>();
            ILogger<TokenController> loggerMock = Mock.Of<ILogger<TokenController>>();
            Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
            Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();
            Mock<ITokenManager> mockTokenmanager = new Mock<ITokenManager>();

            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://localhost:5031");
            var idenetity = new ClaimsIdentity();
            idenetity.AddClaim(new Claim(ClaimTypes.Name, "rainsoft"));
            idenetity.AddClaim(new Claim("UserId", "1"));
            var claimsPrincipal = new ClaimsPrincipal(idenetity);

            var userProfile = new UserProfile();
            userProfile.Id = 1;
            userProfile.UserName = "rainsoft";

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
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
                   Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(userProfile), Encoding.UTF8, "application/json"),
               })
               .Verifiable();
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            mockTokenService.Setup(x => x.GetPrincipalFromExpiredToken(It.IsAny<string>())).ReturnsAsync(claimsPrincipal);

            var jwtSecurityToken = new JwtSecurityToken(issuer: It.IsAny<string>(),
                                           audience: It.IsAny<string>(),
                                           expires: It.IsAny<DateTime?>(),
                                           signingCredentials: It.IsAny<SigningCredentials>(),
                                           claims: It.IsAny<IEnumerable<Claim>>());

            mockTokenService.Setup(x => x.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>())).ReturnsAsync(jwtSecurityToken);

            //TokenPair jwtToken = new TokenPair();
            //jwtToken.JwtToken = "Token";
            //jwtToken.RefreshToken = "RefreshToken";
            //List<TokenPair> tokenPairs = new List<TokenPair>();
            //tokenPairs.Add(jwtToken);

            //TokenPair refreshToken = new TokenPair();
            //refreshToken.RefreshToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNQ1UiLCJVc2VyUHJvZmlsZUlkIjoiNjMxMCIsIlVzZXJOYW1lIjoiZGFuaXNoIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6ImRhbmlzaCIsIkZpcnN0TmFtZSI6IkRhbmlzaCIsIkxhc3ROYW1lIjoiRmFpeiIsIlRlbmFudElkIjoiMSIsIkVtcGxveWVlSWQiOiI2NyIsImV4cCI6MTU5ODg1OTExMiwiaXNzIjoicmFpbnNvZnRmbiIsImF1ZCI6InJlYWRlcnMifQ.9FJGVJoTkB_hQ-P9aorzjgdkZ82dYnGcexCy-bpQYdg";
            
            //tokenPairs.Add(refreshToken);

            //TokenService.RefreshTokens.Add("rainsoft", tokenPairs);

            var httpContext = new Mock<HttpContext>();

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new TokenController(httpClientFactory.Object, mockConfiguration.Object, mockTokenService.Object, mockTwoFactorAuth.Object, mockTenantConfig.Object, loggerMock, customerAccountServiceMock.Object, customerServiceMock.Object, mockTokenmanager.Object);

            controller.ControllerContext = context;

            //Act
            RefreshTokenRequest refreshTokenRequest = new RefreshTokenRequest();
            refreshTokenRequest.Token = "Token";
            refreshTokenRequest.RefreshToken = "RefreshToken";
            IActionResult result = await controller.Refresh(refreshTokenRequest);

            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task TestRefreshTokenPairNullController()
        {
            Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
            Mock<ITenantConfigService> mockTenantConfig = new Mock<ITenantConfigService>();
            Mock<ITokenManager> mockTokenmanager = new Mock<ITokenManager>();

            mockConfiguration.SetupGet(x => x[It.IsAny<string>()]).Returns("http://localhost:5031");
            var idenetity = new ClaimsIdentity();
            idenetity.AddClaim(new Claim(ClaimTypes.Name,"rainsoft"));
            idenetity.AddClaim(new Claim("UserId","1"));


            var claimsPrincipal = new ClaimsPrincipal(idenetity);

            var userProfile = new UserProfile();
            userProfile.Id = 1;
            userProfile.UserName = "rainsoft";
          

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
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
                   Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(userProfile), Encoding.UTF8, "application/json"),
               })
               .Verifiable();
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

       
            mockTokenService.Setup(x=>x.GetPrincipalFromExpiredToken(It.IsAny<string>())).ReturnsAsync(claimsPrincipal);

            var httpContext = new Mock<HttpContext>();

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            var controller = new TokenController(httpClientFactory.Object, mockConfiguration.Object, mockTokenService.Object, mockTwoFactorAuth.Object, mockTenantConfig.Object, null, null,null, mockTokenmanager.Object);

            controller.ControllerContext = context;

            //Act
            RefreshTokenRequest refreshTokenRequest = new RefreshTokenRequest();
            refreshTokenRequest.Token = "Token";
            refreshTokenRequest.RefreshToken = "RefreshToken";
            IActionResult result = await controller.Refresh(refreshTokenRequest);
            
            //Assert
            Assert.NotNull(result);
        }
        //[Fact]
        //public async Task TestRevokeController()
        //{
        //    Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
        //    Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
        //    Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
        //    Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
        //    Mock<ITenantConfigService> mockTenantConfig = new Mock<ITenantConfigService>();
        //    ILogger<TokenController> loggerMock = Mock.Of<ILogger<TokenController>>();
        //    Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
        //    Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();

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

        //    var controller = new TokenController(httpClientFactory.Object, mockConfiguration.Object, mockTokenService.Object, mockTwoFactorAuth.Object, mockTenantConfig.Object, loggerMock, customerAccountServiceMock.Object, customerServiceMock.Object);

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
        //    Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
        //    Mock<ITenantConfigService> mockTenantConfig = new Mock<ITenantConfigService>();
        //    ILogger<TokenController> loggerMock = Mock.Of<ILogger<TokenController>>();
        //    Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
        //    Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();

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

        //    var controller = new TokenController(httpClientFactory.Object, mockConfiguration.Object, mockTokenService.Object, mockTwoFactorAuth.Object, mockTenantConfig.Object, loggerMock, customerAccountServiceMock.Object, customerServiceMock.Object);

        //    controller.ControllerContext = context;

        //    //Act
        //    IActionResult result = await controller.Revoke();

        //    //Assert
        //    Assert.NotNull(result);
        //    Assert.IsType<OkObjectResult>(result);
        //}
        //[Fact]
        //public async Task TestAuthorizeController()
        //{
        //    Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
        //    Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
        //    Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
        //    Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
        //    Mock<ITenantConfigService> mockTenantConfig = new Mock<ITenantConfigService>();
        //    ILogger<TokenController> loggerMock = Mock.Of<ILogger<TokenController>>();
        //    Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
        //    Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();

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

        //    lstCustomer.Add(customer);

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

        //    var controller = new TokenController(httpClientFactory.Object, mockConfiguration.Object, mockTokenService.Object, mockTwoFactorAuth.Object, mockTenantConfig.Object, loggerMock, customerAccountServiceMock.Object, customerServiceMock.Object);

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
        //[Fact]
        //public async Task TestAuthorizeNotFoundController()
        //{
        //    Mock<ITokenService> mockTokenService = new Mock<ITokenService>();
        //    Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
        //    Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
        //    Mock<ITwoFactorAuth> mockTwoFactorAuth = new Mock<ITwoFactorAuth>();
        //    Mock<ITenantConfigService> mockTenantConfig = new Mock<ITenantConfigService>();
        //    ILogger<TokenController> loggerMock = Mock.Of<ILogger<TokenController>>();
        //    Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
        //    Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();

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

        //    lstCustomer.Add(customer);

        //    List<Employee> employees = new List<Employee>();
        //    Employee employee = new Employee();
        //    employee.Id = 1;
        //    employee.ContactId = 1;
        //    employee.Contact = contact;

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
        //    mockTokenService.Setup(x => x.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>())).ReturnsAsync(jwtSecurityToken);

        //    var httpContext = new Mock<HttpContext>();

        //    var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

        //    var controller = new TokenController(httpClientFactory.Object, mockConfiguration.Object, mockTokenService.Object, mockTwoFactorAuth.Object, mockTenantConfig.Object, loggerMock, customerAccountServiceMock.Object, customerServiceMock.Object);

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
        public async Task TestGenerateAccessTokenService()
        {
            //Arrange
            Mock<IKeyStoreService> mockKeyStoreService = new Mock<IKeyStoreService>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            Mock<IUnitOfWork<IdentityContext>> identityContextMock = new Mock<IUnitOfWork<IdentityContext>>();
            Mock<IServiceProvider> serviceProvideMock = new Mock<IServiceProvider>();

            mockKeyStoreService.Setup(x=>x.GetJwtSecurityKeyAsync()).ReturnsAsync("123");
            mockConfiguration.Setup(x => x["Token:TimeoutInMinutes"]).Returns("30");
            var usersClaims = new List<Claim>
                              {
                                  new Claim(type: "UserProfileId",
                                            value: "1"),
                                  new Claim(type: "UserName",
                                            value:"rainsoft"),
                              };

            //Act
            ITokenService tokenService = new TokenService(mockConfiguration.Object, mockKeyStoreService.Object, identityContextMock.Object, serviceProvideMock.Object, null);
            JwtSecurityToken result = await tokenService.GenerateAccessToken(usersClaims);

            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void TestGenerateRefreshTokenService()
        {
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            Mock<IKeyStoreService> mockKeyStoreService = new Mock<IKeyStoreService>();
            Mock<IUnitOfWork<IdentityContext>> identityContextMock = new Mock<IUnitOfWork<IdentityContext>>();
            Mock<IServiceProvider> serviceProvideMock = new Mock<IServiceProvider>();

            mockConfiguration.Setup(x => x["Token:TimeoutInMinutes"]).Returns("30");
            ITokenService tokenService = new TokenService(mockConfiguration.Object, mockKeyStoreService.Object, identityContextMock.Object, serviceProvideMock.Object, null);
            string result = tokenService.GenerateRefreshToken();
        }
        [Fact]
        public async Task TestGetPrincipalFromExpiredTokenService()
        {
            //Arrange
            Mock<IKeyStoreService> mockKeyStoreService = new Mock<IKeyStoreService>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            Mock<IUnitOfWork<IdentityContext>> identityContextMock = new Mock<IUnitOfWork<IdentityContext>>();
            Mock<IServiceProvider> serviceProvideMock = new Mock<IServiceProvider>();
            mockKeyStoreService.Setup(x => x.GetJwtSecurityKeyAsync()).ReturnsAsync("this_is_our_supper_long_security_key_for_token_vortex$rainsoft");
         
            //Act
            ITokenService tokenService = new TokenService(mockConfiguration.Object, mockKeyStoreService.Object, identityContextMock.Object, serviceProvideMock.Object, null);
            ClaimsPrincipal result = await tokenService.GetPrincipalFromExpiredToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNQ1UiLCJVc2VyUHJvZmlsZUlkIjoiNjMxMCIsIlVzZXJOYW1lIjoiZGFuaXNoIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6ImRhbmlzaCIsIkZpcnN0TmFtZSI6IkRhbmlzaCIsIkxhc3ROYW1lIjoiRmFpeiIsIlRlbmFudElkIjoiMSIsIkVtcGxveWVlSWQiOiI2NyIsImV4cCI6MTU5ODg1OTExMiwiaXNzIjoicmFpbnNvZnRmbiIsImF1ZCI6InJlYWRlcnMifQ.9FJGVJoTkB_hQ-P9aorzjgdkZ82dYnGcexCy-bpQYdg");

            //Assert
           Assert.NotNull(result);
        }
    }
}
