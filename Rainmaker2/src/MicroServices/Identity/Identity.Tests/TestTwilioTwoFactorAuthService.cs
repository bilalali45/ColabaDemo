using Identity.Model.TwoFA;
using Identity.Service;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Identity.Tests
{
    public class TestTwilioTwoFactorAuthService
    {
        [Fact]
        public async Task TestCreate2FaRequestAsyncWhenOkResult()
        {
            // Arrange
            Mock<IRestClient> restClientMock = new Mock<IRestClient>();
            Mock<IKeyStoreService> keyStoreServiceMock = new Mock<IKeyStoreService>();
            ILogger<TwilioTwoFactorAuthService> loggerMock = Mock.Of<ILogger<TwilioTwoFactorAuthService>>();
            Mock<IOtpTracingService> otpTracingServiceMock = new Mock<IOtpTracingService>();
            Mock<IActionContextAccessor> accessorMock = new Mock<IActionContextAccessor>();
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();

            TwilioTwoFaResponseModel twoFaResponse = new TwilioTwoFaResponseModel()
            {
                SendCodeAttempts = new List<SendCodeAttempt>(),
                StatusCode = (int)HttpStatusCode.OK,
                AccountSid = "fakeAccountId",
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
                ServiceSid = "fakeServiceSid",
                Sid = "fakeVerificationSid",
                Status = "pending",
                To = "9876543219",
                Url = "www.faketwiliourl.com",
                Valid = true
            };

            IRestResponse fakeResponse = new RestResponse()
            {
                Content = JsonConvert.SerializeObject(twoFaResponse),
                StatusCode = HttpStatusCode.Created
            };

            restClientMock.Setup(x => x.ExecuteAsync(
                    It.IsAny<IRestRequest>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse()
                {
                    Content = JsonConvert.SerializeObject(twoFaResponse),
                    StatusCode = HttpStatusCode.Created
                });

            CanSend2FaModel fakeCanSendModel = new CanSend2FaModel()
            {
                Next2FaInSeconds = 120,
                TwoFaRecycleMinutes = 10,
                CanSend2Fa = true
            };
            twoFaHelperMock.Setup(x => x.CanSendOtpAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(fakeCanSendModel);

            // Act
            TwilioTwoFactorAuthService twoFaAuthService = new TwilioTwoFactorAuthService(restClientMock.Object,
                keyStoreServiceMock.Object, loggerMock, otpTracingServiceMock.Object, accessorMock.Object,
                tokenServiceMock.Object, tenantConfigServiceMock.Object, twoFaHelperMock.Object);
            twoFaAuthService.SetServiceSid(twoFaResponse.ServiceSid);
            var results = await twoFaAuthService.Create2FaRequestAsync(twoFaResponse.To, twoFaResponse.Sid);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<TwilioTwoFaResponseModel>(results);
        }

        [Fact]
        public async Task TestCreate2FaRequestAsyncWhenTooManyAttempts()
        {
            // Arrange
            Mock<IRestClient> restClientMock = new Mock<IRestClient>();
            Mock<IKeyStoreService> keyStoreServiceMock = new Mock<IKeyStoreService>();
            ILogger<TwilioTwoFactorAuthService> loggerMock = Mock.Of<ILogger<TwilioTwoFactorAuthService>>();
            Mock<IOtpTracingService> otpTracingServiceMock = new Mock<IOtpTracingService>();
            Mock<IActionContextAccessor> accessorMock = new Mock<IActionContextAccessor>();
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();

            TwilioTwoFaResponseModel twoFaResponse = new TwilioTwoFaResponseModel()
            {
                SendCodeAttempts = new List<SendCodeAttempt>(),
                StatusCode = (int)HttpStatusCode.TooManyRequests,
                AccountSid = "fakeAccountId",
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
                ServiceSid = "fakeServiceSid",
                Sid = "fakeVerificationSid",
                Status = "pending",
                To = "9876543219",
                Url = "www.faketwiliourl.com",
                Valid = true
            };

            IRestResponse fakeResponse = new RestResponse()
            {
                Content = JsonConvert.SerializeObject(twoFaResponse),
                StatusCode = HttpStatusCode.Created
            };

            restClientMock.Setup(x => x.ExecuteAsync(
                    It.IsAny<IRestRequest>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse()
                {
                    Content = JsonConvert.SerializeObject(twoFaResponse),
                    StatusCode = HttpStatusCode.TooManyRequests
                });

            CanSend2FaModel fakeCanSendModel = new CanSend2FaModel()
            {
                Next2FaInSeconds = 120,
                TwoFaRecycleMinutes = 10,
                CanSend2Fa = true
            };
            twoFaHelperMock.Setup(x => x.CanSendOtpAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(fakeCanSendModel);

            // Act
            TwilioTwoFactorAuthService twoFaAuthService = new TwilioTwoFactorAuthService(restClientMock.Object,
                keyStoreServiceMock.Object, loggerMock, otpTracingServiceMock.Object, accessorMock.Object,
                tokenServiceMock.Object, tenantConfigServiceMock.Object, twoFaHelperMock.Object);
            twoFaAuthService.SetServiceSid(twoFaResponse.ServiceSid);
            var results = await twoFaAuthService.Create2FaRequestAsync(twoFaResponse.To, twoFaResponse.Sid);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<TwilioTwoFaResponseModel>(results);
        }

        [Fact]
        public async Task TestCreate2FaRequestAsyncWhenOtpNotSent()
        {
            // Arrange
            Mock<IRestClient> restClientMock = new Mock<IRestClient>();
            Mock<IKeyStoreService> keyStoreServiceMock = new Mock<IKeyStoreService>();
            ILogger<TwilioTwoFactorAuthService> loggerMock = Mock.Of<ILogger<TwilioTwoFactorAuthService>>();
            Mock<IOtpTracingService> otpTracingServiceMock = new Mock<IOtpTracingService>();
            Mock<IActionContextAccessor> accessorMock = new Mock<IActionContextAccessor>();
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();

            TwilioTwoFaResponseModel twoFaResponse = new TwilioTwoFaResponseModel()
            {
                SendCodeAttempts = new List<SendCodeAttempt>(),
                StatusCode = (int)HttpStatusCode.TooManyRequests,
                AccountSid = "fakeAccountId",
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
                ServiceSid = "fakeServiceSid",
                Sid = "fakeVerificationSid",
                Status = "pending",
                To = "9876543219",
                Url = "www.faketwiliourl.com",
                Valid = true
            };

            IRestResponse fakeResponse = new RestResponse()
            {
                Content = JsonConvert.SerializeObject(twoFaResponse),
                StatusCode = HttpStatusCode.Created
            };

            restClientMock.Setup(x => x.ExecuteAsync(
                    It.IsAny<IRestRequest>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse()
                {
                    Content = JsonConvert.SerializeObject(twoFaResponse),
                    StatusCode = HttpStatusCode.TooManyRequests
                });

            CanSend2FaModel fakeCanSendModel = new CanSend2FaModel()
            {
                Next2FaInSeconds = 120,
                TwoFaRecycleMinutes = 10,
                CanSend2Fa = false,
                OtpValidity = DateTime.UtcNow.AddMinutes(10)
            };
            twoFaHelperMock.Setup(x => x.CanSendOtpAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(fakeCanSendModel);

            // Act
            TwilioTwoFactorAuthService twoFaAuthService = new TwilioTwoFactorAuthService(restClientMock.Object,
                keyStoreServiceMock.Object, loggerMock, otpTracingServiceMock.Object, accessorMock.Object,
                tokenServiceMock.Object, tenantConfigServiceMock.Object, twoFaHelperMock.Object);
            twoFaAuthService.SetServiceSid(twoFaResponse.ServiceSid);
            var results = await twoFaAuthService.Create2FaRequestAsync(twoFaResponse.To, twoFaResponse.Sid);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<TwilioTwoFaResponseModel>(results);
        }

        [Fact]
        public async Task TestVerify2FaRequestAsyncWhenOkResult()
        {
            // Arrange
            Mock<IRestClient> restClientMock = new Mock<IRestClient>();
            Mock<IKeyStoreService> keyStoreServiceMock = new Mock<IKeyStoreService>();
            ILogger<TwilioTwoFactorAuthService> loggerMock = Mock.Of<ILogger<TwilioTwoFactorAuthService>>();
            Mock<IOtpTracingService> otpTracingServiceMock = new Mock<IOtpTracingService>();
            Mock<IActionContextAccessor> accessorMock = new Mock<IActionContextAccessor>();
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();

            TwilioTwoFaResponseModel twoFaResponse = new TwilioTwoFaResponseModel()
            {
                SendCodeAttempts = new List<SendCodeAttempt>(),
                StatusCode = (int)HttpStatusCode.OK,
                AccountSid = "fakeAccountId",
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
                ServiceSid = "fakeServiceSid",
                Sid = "fakeVerificationSid",
                Status = "approved",
                To = "9876543219",
                Url = "www.faketwiliourl.com",
                Valid = true
            };

            IRestResponse fakeResponse = new RestResponse()
            {
                Content = JsonConvert.SerializeObject(twoFaResponse),
                StatusCode = HttpStatusCode.Created
            };

            restClientMock.Setup(x => x.ExecuteAsync(
                    It.IsAny<IRestRequest>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse()
                {
                    Content = JsonConvert.SerializeObject(twoFaResponse),
                    StatusCode = HttpStatusCode.OK
                });

            // Act
            ITwoFactorAuth twoFaAuthService = new TwilioTwoFactorAuthService(restClientMock.Object,
                keyStoreServiceMock.Object, loggerMock, otpTracingServiceMock.Object, accessorMock.Object,
                tokenServiceMock.Object, tenantConfigServiceMock.Object, twoFaHelperMock.Object);
            twoFaAuthService.SetServiceSid(twoFaResponse.ServiceSid);
            var results = await twoFaAuthService.Verify2FaRequestAsync(twoFaResponse.To, twoFaResponse.Sid);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<TwilioTwoFaResponseModel>(results);
            Assert.Equal(results.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task TestVerify2FaRequestAsyncWhenResultNotOk()
        {
            // Arrange
            Mock<IRestClient> restClientMock = new Mock<IRestClient>();
            Mock<IKeyStoreService> keyStoreServiceMock = new Mock<IKeyStoreService>();
            ILogger<TwilioTwoFactorAuthService> loggerMock = Mock.Of<ILogger<TwilioTwoFactorAuthService>>();
            Mock<IOtpTracingService> otpTracingServiceMock = new Mock<IOtpTracingService>();
            Mock<IActionContextAccessor> accessorMock = new Mock<IActionContextAccessor>();
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();

            TwilioTwoFaResponseModel twoFaResponse = new TwilioTwoFaResponseModel()
            {
                SendCodeAttempts = new List<SendCodeAttempt>(),
                StatusCode = (int)HttpStatusCode.OK,
                AccountSid = "fakeAccountId",
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
                ServiceSid = "fakeServiceSid",
                Sid = "fakeVerificationSid",
                Status = "pending",
                To = "9876543219",
                Url = "www.faketwiliourl.com",
                Valid = true
            };

            IRestResponse fakeResponse = new RestResponse()
            {
                Content = JsonConvert.SerializeObject(twoFaResponse),
                StatusCode = HttpStatusCode.Created
            };

            restClientMock.Setup(x => x.ExecuteAsync(
                    It.IsAny<IRestRequest>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse()
                {
                    Content = JsonConvert.SerializeObject(twoFaResponse),
                    StatusCode = HttpStatusCode.BadRequest
                });

            // Act
            TwilioTwoFactorAuthService twoFaAuthService = new TwilioTwoFactorAuthService(restClientMock.Object,
                keyStoreServiceMock.Object, loggerMock, otpTracingServiceMock.Object, accessorMock.Object,
                tokenServiceMock.Object, tenantConfigServiceMock.Object, twoFaHelperMock.Object);
            twoFaAuthService.SetServiceSid(twoFaResponse.ServiceSid);
            var results = await twoFaAuthService.Verify2FaRequestAsync(twoFaResponse.To, twoFaResponse.Sid);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<TwilioTwoFaResponseModel>(results);
            Assert.NotEqual(results.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task TestVerify2FaRequestAsyncWhenWrongCode()
        {
            // Arrange
            Mock<IRestClient> restClientMock = new Mock<IRestClient>();
            Mock<IKeyStoreService> keyStoreServiceMock = new Mock<IKeyStoreService>();
            ILogger<TwilioTwoFactorAuthService> loggerMock = Mock.Of<ILogger<TwilioTwoFactorAuthService>>();
            Mock<IOtpTracingService> otpTracingServiceMock = new Mock<IOtpTracingService>();
            Mock<IActionContextAccessor> accessorMock = new Mock<IActionContextAccessor>();
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            Mock<ITenantConfigService> tenantConfigServiceMock = new Mock<ITenantConfigService>();
            Mock<ITwoFaHelper> twoFaHelperMock = new Mock<ITwoFaHelper>();

            TwilioTwoFaResponseModel twoFaResponse = new TwilioTwoFaResponseModel()
            {
                SendCodeAttempts = new List<SendCodeAttempt>(),
                StatusCode = (int)HttpStatusCode.OK,
                AccountSid = "fakeAccountId",
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
                ServiceSid = "fakeServiceSid",
                Sid = "fakeVerificationSid",
                Status = "pending",
                To = "9876543219",
                Url = "www.faketwiliourl.com",
                Valid = true
            };

            IRestResponse fakeResponse = new RestResponse()
            {
                Content = JsonConvert.SerializeObject(twoFaResponse),
                StatusCode = HttpStatusCode.Created
            };

            restClientMock.Setup(x => x.ExecuteAsync(
                    It.IsAny<IRestRequest>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse()
                {
                    Content = JsonConvert.SerializeObject(twoFaResponse),
                    StatusCode = HttpStatusCode.OK
                });

            // Act
            TwilioTwoFactorAuthService twoFaAuthService = new TwilioTwoFactorAuthService(restClientMock.Object,
                keyStoreServiceMock.Object, loggerMock, otpTracingServiceMock.Object, accessorMock.Object,
                tokenServiceMock.Object, tenantConfigServiceMock.Object, twoFaHelperMock.Object);
            twoFaAuthService.SetServiceSid(twoFaResponse.ServiceSid);
            var results = await twoFaAuthService.Verify2FaRequestAsync("fakeWrongCode", twoFaResponse.Sid);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<TwilioTwoFaResponseModel>(results);
            Assert.Equal(results.StatusCode, (int)HttpStatusCode.OK);
        }
    }
}
