using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using Identity.Controllers;
using Identity.Data;
using Identity.Model;
using Identity.Model.TwoFA;
using Identity.Models.TwoFA;
using Identity.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Entity.Models;
using Xunit;
using IKeyStoreService = Identity.Service.IKeyStoreService;

namespace Identity.Tests
{
    public class TwoFaTest
    {
        [Fact]
        public async Task TestVerify2FaSignRequestWithNoToken()
        {
            // Arrange 
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITwoFactorAuth> twoFaAuthServiceMock = new Mock<ITwoFactorAuth>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
            Mock<IOtpTracingService> tracingLogMock = new Mock<IOtpTracingService>();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            var loggerMock = Mock.Of<ILogger<TwoFaController>>();
            Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();
            Mock<IConfiguration> configMock = new Mock<IConfiguration>();
            Mock<IKeyStoreService> keyStoreMock = new Mock<IKeyStoreService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };
            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;
            
            tracingLogMock.Setup(x => x.VerifyPhoneSidCombination(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);


            // Act
            var controller = new TwoFaController(tokenServiceMock.Object, twoFaAuthServiceMock.Object,
                tenantConfigServiceMock.Object, loggerMock, tracingLogMock.Object, contextAccessorMock.Object, customerServiceMock.Object, twoFaHelperMock.Object, customerAccountServiceMock.Object, configMock.Object, keyStoreMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            Verify2FaModel model = new Verify2FaModel()
            {
                Code = "wrongCode",
                Email = "someone@email.com",
                PhoneNumber = "2142715414",
                RequestSid = ""
            };
            var result = await controller.Verify2FaSignInRequest(model);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task TestVerify2FaSignRequestWhenTokenIsNotValid()
        {
            // Arrange 
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITwoFactorAuth> twoFaAuthServiceMock = new Mock<ITwoFactorAuth>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
            Mock<IOtpTracingService> tracingLogMock = new Mock<IOtpTracingService>();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            var loggerMock = Mock.Of<ILogger<TwoFaController>>();
            Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();
            Mock<IConfiguration> configMock = new Mock<IConfiguration>();
            Mock<IKeyStoreService> keyStoreMock = new Mock<IKeyStoreService>();

            string fakeToken = "Bearer tokenPayload";
            Verify2FaModel verifyModel = new Verify2FaModel();
            ClaimsPrincipal principal = null;

            tokenServiceMock.Setup(x => x.Validate2FaTokenAsync(It.IsAny<string>())).ReturnsAsync(principal);

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };
            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;
            contextMock.HttpContext.Request.Headers.Add("Authorization", fakeToken);

            tracingLogMock.Setup(x => x.VerifyPhoneSidCombination(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var controller = new TwoFaController(tokenServiceMock.Object, twoFaAuthServiceMock.Object,
                tenantConfigServiceMock.Object, loggerMock, tracingLogMock.Object, contextAccessorMock.Object, customerServiceMock.Object, twoFaHelperMock.Object, customerAccountServiceMock.Object, configMock.Object, keyStoreMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            Verify2FaModel model = new Verify2FaModel()
            {
                Code = "wrongCode",
                Email = "someone@email.com",
                PhoneNumber = "2142715414",
                RequestSid = ""
            };
            var result = await controller.Verify2FaSignInRequest(model);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task TestVerify2FaSignRequestWhenTokenIsValidAnd2FaApproved()
        {
            // Arrange 
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITwoFactorAuth> twoFaAuthServiceMock = new Mock<ITwoFactorAuth>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
            Mock<IOtpTracingService> tracingLogMock = new Mock<IOtpTracingService>();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            var loggerMock = Mock.Of<ILogger<TwoFaController>>();
            Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();
            Mock<IConfiguration> configMock = new Mock<IConfiguration>();
            Mock<IKeyStoreService> keyStoreMock = new Mock<IKeyStoreService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "fakeBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                },
                Code = "fakeTenantCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };
            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            string fakeToken = "Bearer tokenPayload";
            Verify2FaModel verifyModel = new Verify2FaModel();
            var idenetity = new ClaimsIdentity();
            idenetity.AddClaim(new Claim("UserProfileId", "1"));
            idenetity.AddClaim(new Claim("ContactId", "1"));
            idenetity.AddClaim(new Claim("Email", "someone@email.com"));
            ClaimsPrincipal principal = new ClaimsPrincipal(idenetity);

            tokenServiceMock.Setup(x => x.Validate2FaTokenAsync(It.IsAny<string>())).ReturnsAsync(principal);

            // Setup tenant 2Fa config object
            TwoFaConfig tenant2FaConfig = new TwoFaConfig()
            {
                BorrowerTwoFaModeId = 1, // Required for All
                BranchId = 1,
                EntityIdentifier = Guid.NewGuid(),
                Id = 1,
                IsActive = true,
                McuTwoFaModeId = 0,
                TenantId = 1,
                TwilioVerifyServiceId = "FakeTwilioSid"
            };
            tenantConfigServiceMock.Setup(x => x.GetTenant2FaConfigAsync(contextTenant.Id, null))
                .ReturnsAsync(tenant2FaConfig);
            twoFaAuthServiceMock.Setup(x => x.SetServiceSid(It.IsAny<string>()));

            Verify2FaModel model = new Verify2FaModel()
            {
                Code = "fakeCode",
                Email = "someone@email.com",
                PhoneNumber = "2142715414",
                RequestSid = "fakeSid"
            };
            TwilioTwoFaResponseModel twoFaResponse = new TwilioTwoFaResponseModel()
            {
                SendCodeAttempts = new List<SendCodeAttempt>(),
                StatusCode = (int) HttpStatusCode.OK,
                AccountSid = tenant2FaConfig.TwilioVerifyServiceId,
                Amount = null,
                Channel = "sms",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow.AddMinutes(10),
                Lookup = new Lookup()
                {
                    Carrier = new Carrier()
                    {
                        Name = "FakeCarrier",
                        Type = "FakeType",
                        ErrorCode = null,
                        MobileCountryCode = "FakeCountryCode",
                        MobileNetworkCode = "FakeNetworkCode"
                    }
                },
                Payee = null,
                ServiceSid = tenant2FaConfig.TwilioVerifyServiceId,
                Sid = tenant2FaConfig.TwilioVerifyServiceId,
                Status = "approved",
                To = model.PhoneNumber,
                Url = "www.faketwiliourl.com",
                Valid = true
            };

            twoFaAuthServiceMock.Setup(x => x.Verify2FaRequestAsync(model.Code, model.RequestSid))
                .ReturnsAsync(twoFaResponse);
            tracingLogMock.Setup(x => x.VerifyPhoneSidCombination(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);
            //configMock.Setup(x => x.GetValue<int>("DontAsk2FaCookieDays")).Returns(10);
            contextMock.HttpContext.Request.Headers.Add("Authorization", fakeToken);

            // Act
            var controller = new TwoFaController(tokenServiceMock.Object, twoFaAuthServiceMock.Object,
                tenantConfigServiceMock.Object, loggerMock, tracingLogMock.Object, contextAccessorMock.Object, customerServiceMock.Object, twoFaHelperMock.Object, customerAccountServiceMock.Object, configMock.Object, keyStoreMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            
            var result = await controller.Verify2FaSignInRequest(model);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestVerify2FaSignRequestWhenTokenIsValidAnd2FaNotApproved()
        {
            // Arrange 
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITwoFactorAuth> twoFaAuthServiceMock = new Mock<ITwoFactorAuth>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
            Mock<IOtpTracingService> tracingLogMock = new Mock<IOtpTracingService>();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            var loggerMock = Mock.Of<ILogger<TwoFaController>>();
            Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();
            Mock<IConfiguration> configMock = new Mock<IConfiguration>();
            Mock<IKeyStoreService> keyStoreMock = new Mock<IKeyStoreService>();

            //// Mock identity context to insert log
            //var builder = new DbContextOptionsBuilder<IdentityContext>();
            //builder.UseInMemoryDatabase("Identity");
            //DbContextOptions<IdentityContext> options = builder.Options;
            //using IdentityContext dataContext = new IdentityContext(options);

            //dataContext.Database.EnsureCreated();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "fakeBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                },
                Code = "fakeTenantCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };
            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            string fakeToken = "Bearer tokenPayload";
            Verify2FaModel verifyModel = new Verify2FaModel();
            var idenetity = new ClaimsIdentity();
            idenetity.AddClaim(new Claim("UserProfileId", "1"));
            idenetity.AddClaim(new Claim("ContactId", "1"));
            idenetity.AddClaim(new Claim("Email", "someone@email.com"));
            ClaimsPrincipal principal = new ClaimsPrincipal(idenetity);

            tokenServiceMock.Setup(x => x.Validate2FaTokenAsync(It.IsAny<string>())).ReturnsAsync(principal);

            // Setup tenant 2Fa config object
            TwoFaConfig tenant2FaConfig = new TwoFaConfig()
            {
                BorrowerTwoFaModeId = 1, // Required for All
                BranchId = 1,
                EntityIdentifier = Guid.NewGuid(),
                Id = 1,
                IsActive = true,
                McuTwoFaModeId = 0,
                TenantId = 1,
                TwilioVerifyServiceId = "FakeTwilioSid"
            };
            tenantConfigServiceMock.Setup(x => x.GetTenant2FaConfigAsync(contextTenant.Id, null))
                .ReturnsAsync(tenant2FaConfig);
            twoFaAuthServiceMock.Setup(x => x.SetServiceSid(It.IsAny<string>()));

            Verify2FaModel model = new Verify2FaModel()
            {
                Code = "fakeWrongCode",
                Email = "someone@email.com",
                PhoneNumber = "xyz",
                RequestSid = "fakeSid"
            };
            TwilioTwoFaResponseModel twoFaResponse = new TwilioTwoFaResponseModel()
            {
                SendCodeAttempts = new List<SendCodeAttempt>(),
                StatusCode = (int)HttpStatusCode.OK,
                AccountSid = tenant2FaConfig.TwilioVerifyServiceId,
                Amount = null,
                Channel = "sms",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow.AddMinutes(10),
                Lookup = new Lookup()
                {
                    Carrier = new Carrier()
                    {
                        Name = "FakeCarrier",
                        Type = "FakeType",
                        ErrorCode = null,
                        MobileCountryCode = "FakeCountryCode",
                        MobileNetworkCode = "FakeNetworkCode"
                    }
                },
                Payee = null,
                ServiceSid = tenant2FaConfig.TwilioVerifyServiceId,
                Sid = tenant2FaConfig.TwilioVerifyServiceId,
                Status = "pending",
                To = model.PhoneNumber,
                Url = "www.faketwiliourl.com",
                Valid = true
            };

            twoFaAuthServiceMock.Setup(x => x.Verify2FaRequestAsync(model.Code, model.RequestSid))
                .ReturnsAsync(twoFaResponse);





            contextMock.HttpContext.Request.Headers.Add("Authorization", fakeToken);

            // Act
            var controller = new TwoFaController(tokenServiceMock.Object, twoFaAuthServiceMock.Object,
                tenantConfigServiceMock.Object, loggerMock, tracingLogMock.Object, contextAccessorMock.Object, customerServiceMock.Object, twoFaHelperMock.Object, customerAccountServiceMock.Object, configMock.Object, keyStoreMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            var result = await controller.Verify2FaSignInRequest(model);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task TestVerify2FaSignRequestWhenSkipped2Fa()
        {
            // Arrange
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITwoFactorAuth> twoFaAuthServiceMock = new Mock<ITwoFactorAuth>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
            Mock<IOtpTracingService> tracingLogMock = new Mock<IOtpTracingService>();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            var loggerMock = Mock.Of<ILogger<TwoFaController>>();
            Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();
            Mock<IConfiguration> configMock = new Mock<IConfiguration>();
            Mock<IKeyStoreService> keyStoreMock = new Mock<IKeyStoreService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "fakeBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                },
                Code = "fakeTenantCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };
            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;
            string fakeToken = "Bearer tokenPayload";
            contextMock.HttpContext.Request.Headers.Add("Authorization", fakeToken);
            Verify2FaModel verifyModel = new Verify2FaModel();
            var idenetity = new ClaimsIdentity();
            idenetity.AddClaim(new Claim("UserProfileId", "1"));
            idenetity.AddClaim(new Claim("ContactId", "1"));
            idenetity.AddClaim(new Claim("Email", "someone@email.com"));
            ClaimsPrincipal principal = new ClaimsPrincipal(idenetity);
            tokenServiceMock.Setup(x => x.Validate2FaTokenAsync(It.IsAny<string>())).ReturnsAsync(principal);
            Verify2FaModel model = new Verify2FaModel()
            {
                Email = "fake@email.com",
                Code = "fakeCode",
                DontAsk2Fa = true,
                MapPhoneNumber = true,
                PhoneNumber = "fakePhoneNumber",
                RequestSid = "fakeRequestSid"
            };

            TwoFaConfig fakeTwoFaConfig = new TwoFaConfig()
            {
                BorrowerTwoFaModeId = 1,
                BranchId = 1,
                EntityIdentifier = Guid.NewGuid(),
                Id = 1,
                IsActive = true,
                McuTwoFaModeId = 1,
                TenantId = 1,
                TwilioVerifyServiceId = "fakeTwoFaServiceId"
            };
            tenantConfigServiceMock.Setup(x => x.GetTenant2FaConfigAsync(contextTenant.Id, null))
                .ReturnsAsync(fakeTwoFaConfig);

            TwilioTwoFaResponseModel fakeTwilioResponse = new TwilioTwoFaResponseModel()
            {
                CanSend2Fa = true,
                TwoFaRecycleMinutes = 10,
                Next2FaInSeconds = 120,
                AccountSid = "fakeAccountSid",
                Amount = null,
                Channel = "sms",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow.AddMinutes(10),
                OtpValidTill = DateTime.UtcNow.AddMinutes(10),
                SendCodeAttempts = new EditableList<SendCodeAttempt>(),
                ServiceSid = "fakeServiceId",
                Sid = "fakeSid",
                StatusCode = 200,
                To = "1111111111",
                Status = "approved",
                Url = "www.fakeUrl.com",
                Valid = true,
                VerifyAttemptsCount = 1
            };
            twoFaAuthServiceMock.Setup(x => x.Verify2FaRequestAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(fakeTwilioResponse);
            tracingLogMock.Setup(x => x.VerifyPhoneSidCombination(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            ApiResponse fakePhoneMapResponse = new ApiResponse()
            {
                Code = "200",
                Message = "Fake success message.",
                Status = "OK"
            };
            customerServiceMock.Setup(x => x.MapPhoneNumberFromOtpTracingAsync(It.IsAny<int>(), contextTenant.Id, model.Email, model.RequestSid, true)).ReturnsAsync(fakePhoneMapResponse);

            // Act
            var controller = new TwoFaController(tokenServiceMock.Object, twoFaAuthServiceMock.Object,
                tenantConfigServiceMock.Object, loggerMock, tracingLogMock.Object, contextAccessorMock.Object, customerServiceMock.Object, twoFaHelperMock.Object, customerAccountServiceMock.Object, configMock.Object, keyStoreMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            var result = await controller.Verify2FaSignInRequest(model);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestSend2FaRequestWhenFirstAttempt()
        {
            // Arrange 
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITwoFactorAuth> twoFaAuthServiceMock = new Mock<ITwoFactorAuth>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
            Mock<IOtpTracingService> tracingLogMock = new Mock<IOtpTracingService>();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            var loggerMock = Mock.Of<ILogger<TwoFaController>>();
            Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();
            Mock<IConfiguration> configMock = new Mock<IConfiguration>();
            Mock<IKeyStoreService> keyStoreMock = new Mock<IKeyStoreService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "fakeBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                },
                Code = "fakeTenantCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };
            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            // Setup tenant 2Fa config object
            TwoFaConfig tenant2FaConfig = new TwoFaConfig()
            {
                BorrowerTwoFaModeId = 1, // Required for All
                BranchId = 1,
                EntityIdentifier = Guid.NewGuid(),
                Id = 1,
                IsActive = true,
                McuTwoFaModeId = 0,
                TenantId = 1,
                TwilioVerifyServiceId = "FakeTwilioSid"
            };
            tenantConfigServiceMock.Setup(x => x.GetTenant2FaConfigAsync(contextTenant.Id, null))
                .ReturnsAsync(tenant2FaConfig);
            twoFaAuthServiceMock.Setup(x => x.SetServiceSid(It.IsAny<string>()));
            tracingLogMock.Setup(x => x.VerifyPhoneSidCombination(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            Resend2FaModel model = new Resend2FaModel()
            {
                Email = "fake@email.com",
                PhoneNumber = "2111111111"
            };
            TwilioTwoFaResponseModel fakeTwoFaResponse = new TwilioTwoFaResponseModel()
            {
                SendCodeAttempts = new List<SendCodeAttempt>()
                {
                    new SendCodeAttempt()
                },
                StatusCode = (int)HttpStatusCode.OK,
                AccountSid = tenant2FaConfig.TwilioVerifyServiceId,
                Amount = null,
                Channel = "sms",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow.AddMinutes(10),
                Lookup = new Lookup()
                {
                    Carrier = new Carrier()
                    {
                        Name = "FakeCarrier",
                        Type = "FakeType",
                        ErrorCode = null,
                        MobileCountryCode = "FakeCountryCode",
                        MobileNetworkCode = "FakeNetworkCode"
                    }
                },
                Payee = null,
                ServiceSid = tenant2FaConfig.TwilioVerifyServiceId,
                Sid = tenant2FaConfig.TwilioVerifyServiceId,
                Status = "pending",
                To = model.PhoneNumber,
                Url = "www.faketwiliourl.com",
                Valid = true,
                CanSend2Fa = true
            };
            twoFaAuthServiceMock.Setup(x => x.Create2FaRequestAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(fakeTwoFaResponse);

            // Act
            var controller = new TwoFaController(tokenServiceMock.Object, twoFaAuthServiceMock.Object,
                tenantConfigServiceMock.Object, loggerMock, tracingLogMock.Object, contextAccessorMock.Object, customerServiceMock.Object, twoFaHelperMock.Object, customerAccountServiceMock.Object, configMock.Object, keyStoreMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            var result = await controller.Send2FaRequest(model);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestSend2FaRequestWhenMultipleAttempts()
        {
            // Arrange 
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITwoFactorAuth> twoFaAuthServiceMock = new Mock<ITwoFactorAuth>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
            Mock<IOtpTracingService> tracingLogMock = new Mock<IOtpTracingService>();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            var loggerMock = Mock.Of<ILogger<TwoFaController>>();
            Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();
            Mock<IConfiguration> configMock = new Mock<IConfiguration>();
            Mock<IKeyStoreService> keyStoreMock = new Mock<IKeyStoreService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "fakeBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                },
                Code = "fakeTenantCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };
            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            // Setup tenant 2Fa config object
            TwoFaConfig tenant2FaConfig = new TwoFaConfig()
            {
                BorrowerTwoFaModeId = 1, // Required for All
                BranchId = 1,
                EntityIdentifier = Guid.NewGuid(),
                Id = 1,
                IsActive = true,
                McuTwoFaModeId = 0,
                TenantId = 1,
                TwilioVerifyServiceId = "FakeTwilioSid"
            };
            tenantConfigServiceMock.Setup(x => x.GetTenant2FaConfigAsync(contextTenant.Id, null))
                .ReturnsAsync(tenant2FaConfig);
            twoFaAuthServiceMock.Setup(x => x.SetServiceSid(It.IsAny<string>()));

            Resend2FaModel model = new Resend2FaModel()
            {
                Email = "fake@email.com",
                PhoneNumber = "1111111111"
            };
            TwilioTwoFaResponseModel fakeTwoFaResponse = new TwilioTwoFaResponseModel()
            {
                SendCodeAttempts = new List<SendCodeAttempt>()
                {
                    new SendCodeAttempt(),
                    new SendCodeAttempt(),
                },
                StatusCode = (int)HttpStatusCode.OK,
                AccountSid = tenant2FaConfig.TwilioVerifyServiceId,
                Amount = null,
                Channel = "sms",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow.AddMinutes(10),
                Lookup = new Lookup()
                {
                    Carrier = new Carrier()
                    {
                        Name = "FakeCarrier",
                        Type = "FakeType",
                        ErrorCode = null,
                        MobileCountryCode = "FakeCountryCode",
                        MobileNetworkCode = "FakeNetworkCode"
                    }
                },
                Payee = null,
                ServiceSid = tenant2FaConfig.TwilioVerifyServiceId,
                Sid = tenant2FaConfig.TwilioVerifyServiceId,
                Status = "pending",
                To = model.PhoneNumber,
                Url = "www.faketwiliourl.com",
                Valid = true,
                CanSend2Fa = true
            };
            twoFaAuthServiceMock.Setup(x => x.Create2FaRequestAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(fakeTwoFaResponse);

            // Act
            var controller = new TwoFaController(tokenServiceMock.Object, twoFaAuthServiceMock.Object,
                tenantConfigServiceMock.Object, loggerMock, tracingLogMock.Object, contextAccessorMock.Object, customerServiceMock.Object, twoFaHelperMock.Object, customerAccountServiceMock.Object, configMock.Object, keyStoreMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            var result = await controller.Send2FaRequest(model);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestSend2FaRequestWhenMaxAttemptsReached()
        {
            // Arrange 
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITwoFactorAuth> twoFaAuthServiceMock = new Mock<ITwoFactorAuth>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
            Mock<IOtpTracingService> tracingLogMock = new Mock<IOtpTracingService>();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            var loggerMock = Mock.Of<ILogger<TwoFaController>>();
            Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();
            Mock<IConfiguration> configMock = new Mock<IConfiguration>();
            Mock<IKeyStoreService> keyStoreMock = new Mock<IKeyStoreService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "fakeBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                },
                Code = "fakeTenantCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };
            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            // Setup tenant 2Fa config object
            TwoFaConfig tenant2FaConfig = new TwoFaConfig()
            {
                BorrowerTwoFaModeId = 1, // Required for All
                BranchId = 1,
                EntityIdentifier = Guid.NewGuid(),
                Id = 1,
                IsActive = true,
                McuTwoFaModeId = 0,
                TenantId = 1,
                TwilioVerifyServiceId = "FakeTwilioSid"
            };
            tenantConfigServiceMock.Setup(x => x.GetTenant2FaConfigAsync(contextTenant.Id, null))
                .ReturnsAsync(tenant2FaConfig);
            twoFaAuthServiceMock.Setup(x => x.SetServiceSid(It.IsAny<string>()));

            Resend2FaModel model = new Resend2FaModel()
            {
                Email = "fake@email.com",
                PhoneNumber = "1111111111"
            };
            TwilioTwoFaResponseModel fakeTwoFaResponse = new TwilioTwoFaResponseModel()
            {
                SendCodeAttempts = new List<SendCodeAttempt>()
                {
                    new SendCodeAttempt(),
                    new SendCodeAttempt(),
                    new SendCodeAttempt(),
                    new SendCodeAttempt(),
                    new SendCodeAttempt(),
                    new SendCodeAttempt(),
                },
                StatusCode = (int)HttpStatusCode.OK,
                AccountSid = tenant2FaConfig.TwilioVerifyServiceId,
                Amount = null,
                Channel = "sms",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow.AddMinutes(10),
                Lookup = new Lookup()
                {
                    Carrier = new Carrier()
                    {
                        Name = "FakeCarrier",
                        Type = "FakeType",
                        ErrorCode = null,
                        MobileCountryCode = "FakeCountryCode",
                        MobileNetworkCode = "FakeNetworkCode"
                    }
                },
                Payee = null,
                ServiceSid = tenant2FaConfig.TwilioVerifyServiceId,
                Sid = tenant2FaConfig.TwilioVerifyServiceId,
                Status = "max_attempts_reached",
                To = model.PhoneNumber,
                Url = "www.faketwiliourl.com",
                Valid = true,
                CanSend2Fa = true
            };
            twoFaAuthServiceMock.Setup(x => x.Create2FaRequestAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(fakeTwoFaResponse);

            // Act
            var controller = new TwoFaController(tokenServiceMock.Object, twoFaAuthServiceMock.Object,
                tenantConfigServiceMock.Object, loggerMock, tracingLogMock.Object, contextAccessorMock.Object, customerServiceMock.Object, twoFaHelperMock.Object, customerAccountServiceMock.Object, configMock.Object, keyStoreMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            var result = await controller.Send2FaRequest(model);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestSend2FaWhenSidNotFound()
        {
            // Arrange 
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITwoFactorAuth> twoFaAuthServiceMock = new Mock<ITwoFactorAuth>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
            Mock<IOtpTracingService> tracingLogMock = new Mock<IOtpTracingService>();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            var loggerMock = Mock.Of<ILogger<TwoFaController>>();
            Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();
            Mock<IConfiguration> configMock = new Mock<IConfiguration>();
            Mock<IKeyStoreService> keyStoreMock = new Mock<IKeyStoreService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "fakeBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                },
                Code = "fakeTenantCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };
            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            // Setup tenant 2Fa config object
            TwoFaConfig tenant2FaConfig = new TwoFaConfig()
            {
                BorrowerTwoFaModeId = 1, // Required for All
                BranchId = 1,
                EntityIdentifier = Guid.NewGuid(),
                Id = 1,
                IsActive = true,
                McuTwoFaModeId = 0,
                TenantId = 1,
                TwilioVerifyServiceId = "FakeTwilioSid"
            };
            tenantConfigServiceMock.Setup(x => x.GetTenant2FaConfigAsync(contextTenant.Id, null))
                .ReturnsAsync(tenant2FaConfig);
            twoFaAuthServiceMock.Setup(x => x.SetServiceSid(It.IsAny<string>()));

            Resend2FaModel model = new Resend2FaModel()
            {
                Email = "fake@email.com",
                PhoneNumber = "fakePhoneNumber"
            };
            TwilioTwoFaResponseModel fakeTwoFaResponse = new TwilioTwoFaResponseModel()
            {
                SendCodeAttempts = new List<SendCodeAttempt>()
                {
                    new SendCodeAttempt(),
                    new SendCodeAttempt(),
                    new SendCodeAttempt(),
                    new SendCodeAttempt(),
                    new SendCodeAttempt(),
                    new SendCodeAttempt(),
                },
                StatusCode = (int)HttpStatusCode.NotFound,
                AccountSid = tenant2FaConfig.TwilioVerifyServiceId,
                Amount = null,
                Channel = "sms",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow.AddMinutes(10),
                Lookup = new Lookup()
                {
                    Carrier = new Carrier()
                    {
                        Name = "FakeCarrier",
                        Type = "FakeType",
                        ErrorCode = null,
                        MobileCountryCode = "FakeCountryCode",
                        MobileNetworkCode = "FakeNetworkCode"
                    }
                },
                Payee = null,
                ServiceSid = tenant2FaConfig.TwilioVerifyServiceId,
                Sid = tenant2FaConfig.TwilioVerifyServiceId,
                Status = "max_attempts_reached",
                To = model.PhoneNumber,
                Url = "www.faketwiliourl.com",
                Valid = true
            };
            twoFaAuthServiceMock.Setup(x => x.Create2FaRequestAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(fakeTwoFaResponse);

            // Act
            var controller = new TwoFaController(tokenServiceMock.Object, twoFaAuthServiceMock.Object,
                tenantConfigServiceMock.Object, loggerMock, tracingLogMock.Object, contextAccessorMock.Object, customerServiceMock.Object, twoFaHelperMock.Object, customerAccountServiceMock.Object, configMock.Object, keyStoreMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            var result = await controller.Send2FaRequest(model);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task TestSend2FaWhenReturnsBadRequest()
        {
            // Arrange 
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITwoFactorAuth> twoFaAuthServiceMock = new Mock<ITwoFactorAuth>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
            Mock<IOtpTracingService> tracingLogMock = new Mock<IOtpTracingService>();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            var loggerMock = Mock.Of<ILogger<TwoFaController>>();
            Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();
            Mock<IConfiguration> configMock = new Mock<IConfiguration>();
            Mock<IKeyStoreService> keyStoreMock = new Mock<IKeyStoreService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "fakeBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                },
                Code = "fakeTenantCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };
            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            // Setup tenant 2Fa config object
            TwoFaConfig tenant2FaConfig = new TwoFaConfig()
            {
                BorrowerTwoFaModeId = 1, // Required for All
                BranchId = 1,
                EntityIdentifier = Guid.NewGuid(),
                Id = 1,
                IsActive = true,
                McuTwoFaModeId = 0,
                TenantId = 1,
                TwilioVerifyServiceId = "FakeTwilioSid"
            };
            tenantConfigServiceMock.Setup(x => x.GetTenant2FaConfigAsync(contextTenant.Id, null))
                .ReturnsAsync(tenant2FaConfig);
            twoFaAuthServiceMock.Setup(x => x.SetServiceSid(It.IsAny<string>()));

            Resend2FaModel model = new Resend2FaModel()
            {
                Email = "fake@email.com",
                PhoneNumber = "fakePhoneNumber"
            };
            TwilioTwoFaResponseModel fakeTwoFaResponse = new TwilioTwoFaResponseModel()
            {
                SendCodeAttempts = new List<SendCodeAttempt>()
                {
                    new SendCodeAttempt(),
                    new SendCodeAttempt(),
                    new SendCodeAttempt(),
                    new SendCodeAttempt(),
                    new SendCodeAttempt(),
                    new SendCodeAttempt(),
                },
                StatusCode = (int)HttpStatusCode.BadRequest,
                AccountSid = tenant2FaConfig.TwilioVerifyServiceId,
                Amount = null,
                Channel = "sms",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow.AddMinutes(10),
                Lookup = new Lookup()
                {
                    Carrier = new Carrier()
                    {
                        Name = "FakeCarrier",
                        Type = "FakeType",
                        ErrorCode = null,
                        MobileCountryCode = "FakeCountryCode",
                        MobileNetworkCode = "FakeNetworkCode"
                    }
                },
                Payee = null,
                ServiceSid = tenant2FaConfig.TwilioVerifyServiceId,
                Sid = tenant2FaConfig.TwilioVerifyServiceId,
                Status = "max_attempts_reached",
                To = model.PhoneNumber,
                Url = "www.faketwiliourl.com",
                Valid = true
            };
            twoFaAuthServiceMock.Setup(x => x.Create2FaRequestAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(fakeTwoFaResponse);

            // Act
            var controller = new TwoFaController(tokenServiceMock.Object, twoFaAuthServiceMock.Object,
                tenantConfigServiceMock.Object, loggerMock, tracingLogMock.Object, contextAccessorMock.Object, customerServiceMock.Object, twoFaHelperMock.Object, customerAccountServiceMock.Object, configMock.Object, keyStoreMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            var result = await controller.Send2FaRequest(model);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task TestVerify2FaRequestWithCorrectCode()
        {
            // Arrange 
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITwoFactorAuth> twoFaAuthServiceMock = new Mock<ITwoFactorAuth>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
            Mock<IOtpTracingService> tracingLogMock = new Mock<IOtpTracingService>();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            var loggerMock = Mock.Of<ILogger<TwoFaController>>();
            Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();
            Mock<IConfiguration> configMock = new Mock<IConfiguration>();
            Mock<IKeyStoreService> keyStoreMock = new Mock<IKeyStoreService>();

            // Arrange tenant model in context item
            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "fakeBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                },
                Code = "fakeTenantCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };
            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            // Setup tenant 2Fa config object
            TwoFaConfig tenant2FaConfig = new TwoFaConfig()
            {
                BorrowerTwoFaModeId = 1, // Required for All
                BranchId = 1,
                EntityIdentifier = Guid.NewGuid(),
                Id = 1,
                IsActive = true,
                McuTwoFaModeId = 0,
                TenantId = 1,
                TwilioVerifyServiceId = "FakeTwilioSid"
            };
            tenantConfigServiceMock.Setup(x => x.GetTenant2FaConfigAsync(contextTenant.Id, null))
                .ReturnsAsync(tenant2FaConfig);
            twoFaAuthServiceMock.Setup(x => x.SetServiceSid(It.IsAny<string>()));

            Verify2FaModel model = new Verify2FaModel()
            {
                Code = "fakeCode",
                Email = "someone@email.com",
                PhoneNumber = "2111111111",
                RequestSid = "fakeSid"
            };
            TwilioTwoFaResponseModel twoFaResponse = new TwilioTwoFaResponseModel()
            {
                SendCodeAttempts = new List<SendCodeAttempt>(),
                StatusCode = (int)HttpStatusCode.OK,
                AccountSid = tenant2FaConfig.TwilioVerifyServiceId,
                Amount = null,
                Channel = "sms",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow.AddMinutes(10),
                Lookup = new Lookup()
                {
                    Carrier = new Carrier()
                    {
                        Name = "FakeCarrier",
                        Type = "FakeType",
                        ErrorCode = null,
                        MobileCountryCode = "FakeCountryCode",
                        MobileNetworkCode = "FakeNetworkCode"
                    }
                },
                Payee = null,
                ServiceSid = tenant2FaConfig.TwilioVerifyServiceId,
                Sid = tenant2FaConfig.TwilioVerifyServiceId,
                Status = "approved",
                To = model.PhoneNumber,
                Url = "www.faketwiliourl.com",
                Valid = true,
                CanSend2Fa = true
            };

            twoFaAuthServiceMock.Setup(x => x.Verify2FaRequestAsync(model.Code, model.RequestSid))
                .ReturnsAsync(twoFaResponse);
            tracingLogMock.Setup(x => x.VerifyPhoneSidCombination(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var controller = new TwoFaController(tokenServiceMock.Object, twoFaAuthServiceMock.Object,
                tenantConfigServiceMock.Object, loggerMock, tracingLogMock.Object, contextAccessorMock.Object, customerServiceMock.Object, twoFaHelperMock.Object, customerAccountServiceMock.Object, configMock.Object, keyStoreMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            var result = await controller.Verify2FaRequest(model);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestVerify2FaRequestWithIncorrectCode()
        {
            // Arrange 
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITwoFactorAuth> twoFaAuthServiceMock = new Mock<ITwoFactorAuth>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
            Mock<IOtpTracingService> tracingLogMock = new Mock<IOtpTracingService>();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            var loggerMock = Mock.Of<ILogger<TwoFaController>>();
            Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();
            Mock<IConfiguration> configMock = new Mock<IConfiguration>();
            Mock<IKeyStoreService> keyStoreMock = new Mock<IKeyStoreService>();

            //// Mock identity context to insert log
            //var builder = new DbContextOptionsBuilder<IdentityContext>();
            //builder.UseInMemoryDatabase("Identity");
            //DbContextOptions<IdentityContext> options = builder.Options;
            //using IdentityContext dataContext = new IdentityContext(options);

            //dataContext.Database.EnsureCreated();

            // Arrange tenant model in context item
            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "fakeBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                },
                Code = "fakeTenantCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };
            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            // Setup tenant 2Fa config object
            TwoFaConfig tenant2FaConfig = new TwoFaConfig()
            {
                BorrowerTwoFaModeId = 1, // Required for All
                BranchId = 1,
                EntityIdentifier = Guid.NewGuid(),
                Id = 1,
                IsActive = true,
                McuTwoFaModeId = 0,
                TenantId = 1,
                TwilioVerifyServiceId = "FakeTwilioSid"
            };
            tenantConfigServiceMock.Setup(x => x.GetTenant2FaConfigAsync(contextTenant.Id, null))
                .ReturnsAsync(tenant2FaConfig);
            twoFaAuthServiceMock.Setup(x => x.SetServiceSid(It.IsAny<string>()));

            Verify2FaModel model = new Verify2FaModel()
            {
                Code = "fakeWrongCode",
                Email = "someone@email.com",
                PhoneNumber = "xyz",
                RequestSid = "fakeSid"
            };
            TwilioTwoFaResponseModel twoFaResponse = new TwilioTwoFaResponseModel()
            {
                SendCodeAttempts = new List<SendCodeAttempt>(),
                StatusCode = (int)HttpStatusCode.OK,
                AccountSid = tenant2FaConfig.TwilioVerifyServiceId,
                Amount = null,
                Channel = "sms",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow.AddMinutes(10),
                Lookup = new Lookup()
                {
                    Carrier = new Carrier()
                    {
                        Name = "FakeCarrier",
                        Type = "FakeType",
                        ErrorCode = null,
                        MobileCountryCode = "FakeCountryCode",
                        MobileNetworkCode = "FakeNetworkCode"
                    }
                },
                Payee = null,
                ServiceSid = tenant2FaConfig.TwilioVerifyServiceId,
                Sid = tenant2FaConfig.TwilioVerifyServiceId,
                Status = "pending",
                To = model.PhoneNumber,
                Url = "www.faketwiliourl.com",
                Valid = true
            };

            twoFaAuthServiceMock.Setup(x => x.Verify2FaRequestAsync(model.Code, model.RequestSid))
                .ReturnsAsync(twoFaResponse);

            // Act
            var controller = new TwoFaController(tokenServiceMock.Object, twoFaAuthServiceMock.Object,
                tenantConfigServiceMock.Object, loggerMock, tracingLogMock.Object, contextAccessorMock.Object, customerServiceMock.Object, twoFaHelperMock.Object, customerAccountServiceMock.Object, configMock.Object, keyStoreMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            var result = await controller.Verify2FaRequest(model);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task TestGetTenant2FaConfig()
        {
            // Arrange
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITwoFactorAuth> twoFaAuthServiceMock = new Mock<ITwoFactorAuth>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
            Mock<IOtpTracingService> tracingLogMock = new Mock<IOtpTracingService>();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            var loggerMock = Mock.Of<ILogger<TwoFaController>>();
            Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();
            Mock<IConfiguration> configMock = new Mock<IConfiguration>();
            Mock<IKeyStoreService> keyStoreMock = new Mock<IKeyStoreService>();

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;
            TwoFaConfig fakeTenantConfig = new TwoFaConfig()
            {
                BorrowerTwoFaModeId = 1, // Required For All
                BranchId = 1,
                EntityIdentifier = Guid.NewGuid(),
                Id = 1,
                McuTwoFaModeId = 3, // User Preference
                TenantId = 1,
                TwilioVerifyServiceId = "FakeServiceId"
            };
            tenantConfigServiceMock
                .Setup(x => x.GetTenant2FaConfigAsync(contextTenant.Id, It.IsAny<List<TwoFaConfigEntities>>()))
                .ReturnsAsync(fakeTenantConfig);

            // Act 
            var controller = new TwoFaController(tokenServiceMock.Object, twoFaAuthServiceMock.Object,
                tenantConfigServiceMock.Object, loggerMock, tracingLogMock.Object, contextAccessorMock.Object, customerServiceMock.Object, twoFaHelperMock.Object, customerAccountServiceMock.Object, configMock.Object, keyStoreMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            var results = await controller.GetTenant2FaConfig(true);

            // Assert
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestSkip2FaSignIn()
        {
            // Arrange
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITwoFactorAuth> twoFaAuthServiceMock = new Mock<ITwoFactorAuth>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
            Mock<IOtpTracingService> tracingLogMock = new Mock<IOtpTracingService>();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            var loggerMock = Mock.Of<ILogger<TwoFaController>>();
            Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();
            Mock<IConfiguration> configMock = new Mock<IConfiguration>();
            Mock<IKeyStoreService> keyStoreMock = new Mock<IKeyStoreService>();

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            string fakeToken = "Bearer tokenPayload";
            contextMock.HttpContext.Request.Headers.Add("Authorization", fakeToken);
            Verify2FaModel verifyModel = new Verify2FaModel();
            var idenetity = new ClaimsIdentity();
            idenetity.AddClaim(new Claim("UserProfileId", "1"));
            idenetity.AddClaim(new Claim("ContactId", "1"));
            idenetity.AddClaim(new Claim("Email", "someone@email.com"));
            ClaimsPrincipal principal = new ClaimsPrincipal(idenetity);

            tokenServiceMock.Setup(x => x.Validate2FaTokenAsync(It.IsAny<string>())).ReturnsAsync(principal);
            customerAccountServiceMock.Setup(x => x.GenerateNewAccessToken(It.IsAny<int>(), It.IsAny<int>(), "fakeBranchCode"))
                .ReturnsAsync(new ApiResponse());

            // Act
            var controller = new TwoFaController(tokenServiceMock.Object, twoFaAuthServiceMock.Object,
                tenantConfigServiceMock.Object, loggerMock, tracingLogMock.Object, contextAccessorMock.Object, customerServiceMock.Object, twoFaHelperMock.Object, customerAccountServiceMock.Object, configMock.Object, keyStoreMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.Skip2FaSignIn();

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestSkip2FaSignInWhenAuthTokenNullOrEmpty()
        {
            // Arrange
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITwoFactorAuth> twoFaAuthServiceMock = new Mock<ITwoFactorAuth>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
            Mock<IOtpTracingService> tracingLogMock = new Mock<IOtpTracingService>();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            var loggerMock = Mock.Of<ILogger<TwoFaController>>();
            Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();
            Mock<IConfiguration> configMock = new Mock<IConfiguration>();
            Mock<IKeyStoreService> keyStoreMock = new Mock<IKeyStoreService>();

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            string fakeToken = null;
            contextMock.HttpContext.Request.Headers.Add("Authorization", fakeToken);
            Verify2FaModel verifyModel = new Verify2FaModel();
            var idenetity = new ClaimsIdentity();
            idenetity.AddClaim(new Claim("UserProfileId", "1"));
            idenetity.AddClaim(new Claim("ContactId", "1"));
            idenetity.AddClaim(new Claim("Email", "someone@email.com"));
            ClaimsPrincipal principal = new ClaimsPrincipal(idenetity);

            tokenServiceMock.Setup(x => x.Validate2FaTokenAsync(It.IsAny<string>())).ReturnsAsync(principal);
            customerAccountServiceMock.Setup(x => x.GenerateNewAccessToken(It.IsAny<int>(), It.IsAny<int>(), "fakeBranchCode"))
                .ReturnsAsync(new ApiResponse());

            // Act
            var controller = new TwoFaController(tokenServiceMock.Object, twoFaAuthServiceMock.Object,
                tenantConfigServiceMock.Object, loggerMock, tracingLogMock.Object, contextAccessorMock.Object, customerServiceMock.Object, twoFaHelperMock.Object, customerAccountServiceMock.Object, configMock.Object, keyStoreMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.Skip2FaSignIn();

            // Assert
            Assert.NotNull(results);
            Assert.IsType<UnauthorizedObjectResult>(results);
        }

        [Fact]
        public async Task TestSkip2FaSignInWhenAuthTokenIsNotValid()
        {
            // Arrange
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITwoFactorAuth> twoFaAuthServiceMock = new Mock<ITwoFactorAuth>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
            Mock<IOtpTracingService> tracingLogMock = new Mock<IOtpTracingService>();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            var loggerMock = Mock.Of<ILogger<TwoFaController>>();
            Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();
            Mock<IConfiguration> configMock = new Mock<IConfiguration>();
            Mock<IKeyStoreService> keyStoreMock = new Mock<IKeyStoreService>();

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            string fakeToken = "Bearer someFakeTokenPayload";
            contextMock.HttpContext.Request.Headers.Add("Authorization", fakeToken);

            tokenServiceMock.Setup(x => x.Validate2FaTokenAsync(It.IsAny<string>())).ReturnsAsync((ClaimsPrincipal) null);
            customerAccountServiceMock.Setup(x => x.GenerateNewAccessToken(It.IsAny<int>(), It.IsAny<int>(), "fakeBranchCode"))
                .ReturnsAsync(new ApiResponse());

            // Act
            var controller = new TwoFaController(tokenServiceMock.Object, twoFaAuthServiceMock.Object,
                tenantConfigServiceMock.Object, loggerMock, tracingLogMock.Object, contextAccessorMock.Object, customerServiceMock.Object, twoFaHelperMock.Object, customerAccountServiceMock.Object, configMock.Object, keyStoreMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.Skip2FaSignIn();

            // Assert
            Assert.NotNull(results);
            Assert.IsType<UnauthorizedObjectResult>(results);
        }

        [Fact]
        public async Task TestRead2FaConfigWhenKeyExists()
        {
            //Arrange
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITwoFactorAuth> twoFaAuthServiceMock = new Mock<ITwoFactorAuth>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ICustomerAccountService> customerAccountServiceMock = new Mock<ICustomerAccountService>();
            Mock<IOtpTracingService> tracingLogMock = new Mock<IOtpTracingService>();
            Mock<IHttpContextAccessor> contextAccessorMock = new Mock<IHttpContextAccessor>();
            var loggerMock = Mock.Of<ILogger<TwoFaController>>();
            Mock<ICustomerService> customerServiceMock = new Mock<ICustomerService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();
            Mock<IConfiguration> configMock = new Mock<IConfiguration>();
            Mock<IKeyStoreService> keyStoreMock = new Mock<IKeyStoreService>();

            int twoFaIntervalSeconds = 120;
            string twoFaIntervalKey = "TwoFaSettings:Resend2FaIntervalSeconds";
            var inMemorySettings = new Dictionary<string, string> {
                {twoFaIntervalKey, $"{twoFaIntervalSeconds}"},
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            //Act
            var controller = new TwoFaController(tokenServiceMock.Object, twoFaAuthServiceMock.Object,
                tenantConfigServiceMock.Object, loggerMock, tracingLogMock.Object, contextAccessorMock.Object, customerServiceMock.Object, twoFaHelperMock.Object, customerAccountServiceMock.Object, configMock.Object, keyStoreMock.Object);
            var results = await controller.Get2FaIntervalValue();

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
    }
}
