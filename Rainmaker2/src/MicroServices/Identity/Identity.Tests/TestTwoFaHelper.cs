using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Identity.Data;
using Identity.Entity.Models;
using Identity.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace Identity.Tests
{
    public class TestTwoFaHelper
    {
        [Fact]
        public void TestCreateDontAskCookie()
        {
            // Arrange
            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            Mock<IConfiguration> configMock = new Mock<IConfiguration>();
            Mock<IOtpTracingService> otpTracingMock = new Mock<IOtpTracingService>();
            Mock<IKeyStoreService> keyStoreMock = new Mock<IKeyStoreService>();

            DbContextOptions<IdentityContext> options;
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseInMemoryDatabase("Identity");
            options = builder.Options;
            using IdentityContext dataContext = new IdentityContext(options);
            dataContext.Database.EnsureCreated();
            dataContext.SaveChanges();

            var context = new DefaultHttpContext();
            contextAccessorMock.Setup(x => x.HttpContext).Returns(context);

            // Act
            string fakeTenatCode = "fakeTenantCode";
            int fakeUserId = 1;
            var helper = new TwoFaHelper(contextAccessorMock.Object, configMock.Object, otpTracingMock.Object, keyStoreMock.Object, new UnitOfWork<IdentityContext>(dataContext, new RepositoryProvider(new RepositoryFactories())));
            helper.CreateDontAskCookie("fakeemail@gmail.com", true, fakeTenatCode, fakeUserId);
            dataContext.Database.EnsureDeleted();

            // Assert
            Assert.Equal(1, context.Response.Headers.Count);
        }

        [Fact]
        public void TestRead2FaConfigWhenKeyExists()
        {
            //Arrange
            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            Mock<IOtpTracingService> otpTracingMock = new Mock<IOtpTracingService>();
            Mock<IKeyStoreService> keyStoreMock = new Mock<IKeyStoreService>();

            DbContextOptions<IdentityContext> options;
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseInMemoryDatabase("Identity");
            options = builder.Options;
            using IdentityContext dataContext = new IdentityContext(options);
            dataContext.Database.EnsureCreated();
            dataContext.SaveChanges();

            int otpValidity = 10;
            string otpKey = "TwoFaSettings:OtpValidity";
            var inMemorySettings = new Dictionary<string, string> {
                {otpKey, $"{otpValidity}"},
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            //Act
            var helper = new TwoFaHelper(contextAccessorMock.Object, configuration, otpTracingMock.Object, keyStoreMock.Object, new UnitOfWork<IdentityContext>(dataContext, new RepositoryProvider(new RepositoryFactories())));
            var results = helper.Read2FaConfig<int>(otpKey, otpValidity);

            // Assert
            Assert.Equal(otpValidity, results);
        }

        [Fact]
        public void TestRead2FaConfigWhenKeyDoesNotExists()
        {
            //Arrange
            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            Mock<IOtpTracingService> otpTracingMock = new Mock<IOtpTracingService>();
            Mock<IKeyStoreService> keyStoreMock = new Mock<IKeyStoreService>();

            DbContextOptions<IdentityContext> options;
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseInMemoryDatabase("Identity");
            options = builder.Options;
            using IdentityContext dataContext = new IdentityContext(options);
            dataContext.Database.EnsureCreated();
            dataContext.SaveChanges();

            int otpValidity = 15;
            string otpKey = "TwoFaSettings:OtpValidity";
            var inMemorySettings = new Dictionary<string, string> {
                {otpKey, $"{otpValidity}"},
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            //Act
            var helper = new TwoFaHelper(contextAccessorMock.Object, configuration, otpTracingMock.Object, keyStoreMock.Object, new UnitOfWork<IdentityContext>(dataContext, new RepositoryProvider(new RepositoryFactories())));
            var results = helper.Read2FaConfig<int>(string.Concat("_", otpKey), otpValidity);

            // Assert
            Assert.Equal(otpValidity, results);
        }

        [Fact]
        public async Task TestCanSendOtpAsync()
        {
            //Arrange
            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            Mock<IOtpTracingService> otpTracingMock = new Mock<IOtpTracingService>();
            Mock<IKeyStoreService> keyStoreMock = new Mock<IKeyStoreService>();

            DbContextOptions<IdentityContext> options;
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseInMemoryDatabase("Identity");
            options = builder.Options;
            using IdentityContext dataContext = new IdentityContext(options);
            dataContext.Database.EnsureCreated();
            dataContext.SaveChanges();

            OtpTracing fakeOtpTracing = new OtpTracing()
            {
                DateUtc = DateTime.UtcNow,
                OtpCreatedOn = DateTime.UtcNow
            };
            otpTracingMock.Setup(x => x.GetLastSendAttemptAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(fakeOtpTracing);

            int resend2FaIntervalSeconds = 121;
            string resend2FaIntervalSecondsKey = "TwoFaSettings:Resend2FaIntervalSeconds";
            var inMemorySettings = new Dictionary<string, string> {
                {resend2FaIntervalSecondsKey, $"{resend2FaIntervalSeconds}"},
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            // Act
            var helper = new TwoFaHelper(contextAccessorMock.Object, configuration, otpTracingMock.Object, keyStoreMock.Object, new UnitOfWork<IdentityContext>(dataContext, new RepositoryProvider(new RepositoryFactories())));
            var results = await helper.CanSendOtpAsync("fakePhoneNo", "fakeSid");

            // Assert
            Assert.NotNull(results);
        }
    }
}
