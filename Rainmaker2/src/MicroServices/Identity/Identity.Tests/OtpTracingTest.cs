using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Identity.Controllers;
using Identity.Data;
using Identity.Entity.Models;
using Identity.Model.TwoFA;
using Identity.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TenantConfig.Common.DistributedCache;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace Identity.Tests
{
    public class OtpTracingTest
    {
        [Fact]
        public async Task TestAddLogAsync()
        {
            // Arrange
            DbContextOptions<IdentityContext> options;
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseInMemoryDatabase("Identity");
            options = builder.Options;
            using IdentityContext dataContext = new IdentityContext(options);
            dataContext.Database.EnsureCreated();
            dataContext.SaveChanges();

            Mock<IUnitOfWork<IdentityContext>> uowMock = new Mock<IUnitOfWork<IdentityContext>>();
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IActionContextAccessor> accessorMock = new Mock<IActionContextAccessor>();
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            var loggerMock = Mock.Of<ILogger<OtpTracingService>>();
            OtpTracing fakeOtpTracing = new OtpTracing()
            {
                BranchId = 1,
                CarrierName = "fakeCarrierName",
                CarrierType = "fakeCarrierType",
                CodeEntered = "fakeCode",
                ContactId = 1,
                DateUtc = DateTime.UtcNow,
                Email = "fake@email.com",
                EntityIdentifier = Guid.NewGuid(),
                Id = 1,
                IpAddress = "1.1.1.1",
                Message = "fakeMessage",
                OtpCreatedOn = DateTime.UtcNow,
                OtpRequestId = "fakeOtpRequestId",
                OtpUpdatedOn = DateTime.UtcNow.AddMinutes((-5)),
                Phone = "123456789",
                StatusCode = 200,
                TenantId = 1,
                ResponseJson = "fakeJson",
                TracingTypeId = 1,
            };

            TenantModel contexTenantModel = new TenantModel()
            {
                Code = "fakeTenantCode",
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "fakeBranchCode",
                        Id = 1,
                        IsCorporate = true
                    }
                },
                Id = 1
            };

            TwilioTwoFaResponseModel fakeVerifyResponse = new TwilioTwoFaResponseModel()
            {
                Lookup = new Lookup()
                {
                    Carrier = new Carrier()
                    {
                        Name = "fakeCarrierName",
                        Type = "fakeCarrierType"
                    }
                },
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow.AddMinutes(10),
                StatusCode = 200
            };

            //HttpContext fakeContext = new DefaultHttpContext();
            //string fakeToken = "Bearer tokenPayload";
            //accessorMock.Setup(x => x.ActionContext.HttpContext).Returns(fakeContext);
            //fakeContext.Request.Headers.Add("Authorization", fakeToken);



            // Act
            var otpTracing = new OtpTracingService(new UnitOfWork<IdentityContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object, accessorMock.Object,
                tokenServiceMock.Object, loggerMock);
            int results = await otpTracing.AddLogAsync(fakeOtpTracing);
            dataContext.Database.EnsureDeleted();

            // Asert
            Assert.Equal(1, results);
        }

        [Fact]
        public async Task TestCreate2FaLogAsync()
        {
            // Arrange
            DbContextOptions<IdentityContext> options;
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseInMemoryDatabase("Identity");
            options = builder.Options;
            using IdentityContext dataContext = new IdentityContext(options);
            dataContext.Database.EnsureCreated();
            dataContext.SaveChanges();

            Mock<IUnitOfWork<IdentityContext>> uowMock = new Mock<IUnitOfWork<IdentityContext>>();
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IActionContextAccessor> accessorMock = new Mock<IActionContextAccessor>();
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            var loggerMock = Mock.Of<ILogger<OtpTracingService>>();
            OtpTracing fakeOtpTracing = new OtpTracing()
            {
                BranchId = 1,
                CarrierName = "fakeCarrierName",
                CarrierType = "fakeCarrierType",
                CodeEntered = "fakeCode",
                ContactId = 1,
                DateUtc = DateTime.UtcNow,
                Email = "fake@email.com",
                EntityIdentifier = Guid.NewGuid(),
                Id = 1,
                IpAddress = "1.1.1.1",
                Message = "fakeMessage",
                OtpCreatedOn = DateTime.UtcNow,
                OtpRequestId = "fakeOtpRequestId",
                OtpUpdatedOn = DateTime.UtcNow.AddMinutes((-5)),
                Phone = "123456789",
                StatusCode = 200,
                TenantId = 1,
                ResponseJson = "fakeJson",
                TracingTypeId = 1,
            };

            TenantModel contexTenantModel = new TenantModel()
            {
                Code = "fakeTenantCode",
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "fakeBranchCode",
                        Id = 1,
                        IsCorporate = true
                    }
                },
                Id = 1
            };

            TwilioTwoFaResponseModel fakeVerifyResponse = new TwilioTwoFaResponseModel()
            {
                Lookup = new Lookup()
                {
                    Carrier = new Carrier()
                    {
                        Name = "fakeCarrierName",
                        Type = "fakeCarrierType"
                    }
                },
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow.AddMinutes(10),
                StatusCode = 200
            };
            HttpContext fakeContext = new DefaultHttpContext();
            Microsoft.AspNetCore.Mvc.ActionContext actionContext = new ActionContext()
            {
                HttpContext = fakeContext
            };
            
            string fakeToken = "Bearer tokenPayload";
            accessorMock.Setup(x => x.ActionContext).Returns(actionContext);
            //accessorMock.Setup(x => x.ActionContext.HttpContext).Returns(fakeContext);
            fakeContext.Request.Headers.Add("Authorization", fakeToken);

            var idenetity = new ClaimsIdentity();
            idenetity.AddClaim(new Claim("UserProfileId", "1"));
            idenetity.AddClaim(new Claim("ContactId", "1"));
            idenetity.AddClaim(new Claim("Email", "someone@email.com"));
            ClaimsPrincipal principal = new ClaimsPrincipal(idenetity);

            tokenServiceMock.Setup(x => x.GetPrincipalFrom2FaToken(It.IsAny<string>())).ReturnsAsync(principal);


            // Act
            var otpTracing = new OtpTracingService(new UnitOfWork<IdentityContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object, accessorMock.Object,
                tokenServiceMock.Object, loggerMock);
            int results = await otpTracing.Create2FaLogAsync(fakeOtpTracing.Phone, fakeOtpTracing.CodeEntered, fakeOtpTracing.ContactId, fakeOtpTracing.Email, fakeOtpTracing.Message, fakeOtpTracing.OtpRequestId, contexTenantModel, fakeVerifyResponse);
            dataContext.Database.EnsureDeleted();

            // Asert
            Assert.Equal(1, results);
        }

        [Fact]
        public async Task TestCreate2FaLogAsyncWhenLookupIsNull()
        {
            // Arrange
            DbContextOptions<IdentityContext> options;
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseInMemoryDatabase("Identity");
            options = builder.Options;
            using IdentityContext dataContext = new IdentityContext(options);
            dataContext.Database.EnsureCreated();
            dataContext.SaveChanges();

            Mock<IUnitOfWork<IdentityContext>> uowMock = new Mock<IUnitOfWork<IdentityContext>>();
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IActionContextAccessor> accessorMock = new Mock<IActionContextAccessor>();
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            var loggerMock = Mock.Of<ILogger<OtpTracingService>>();
            OtpTracing fakeOtpTracing = new OtpTracing()
            {
                BranchId = 1,
                CarrierName = "fakeCarrierName",
                CarrierType = "fakeCarrierType",
                CodeEntered = "fakeCode",
                ContactId = 1,
                DateUtc = DateTime.UtcNow,
                Email = "fake@email.com",
                EntityIdentifier = Guid.NewGuid(),
                Id = 1,
                IpAddress = "1.1.1.1",
                Message = "fakeMessage",
                OtpCreatedOn = DateTime.UtcNow,
                OtpRequestId = "fakeOtpRequestId",
                OtpUpdatedOn = DateTime.UtcNow.AddMinutes((-5)),
                Phone = "123456789",
                StatusCode = 200,
                TenantId = 1,
                ResponseJson = "fakeJson",
                TracingTypeId = 1,
            };

            TenantModel contexTenantModel = new TenantModel()
            {
                Code = "fakeTenantCode",
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "fakeBranchCode",
                        Id = 1,
                        IsCorporate = true
                    }
                },
                Id = 1
            };

            TwilioTwoFaResponseModel fakeVerifyResponse = new TwilioTwoFaResponseModel()
            {
                Lookup = null,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow.AddMinutes(10),
                StatusCode = 200
            };
            HttpContext fakeContext = new DefaultHttpContext();
            Microsoft.AspNetCore.Mvc.ActionContext actionContext = new ActionContext()
            {
                HttpContext = fakeContext
            };

            string fakeToken = "Bearer tokenPayload";
            accessorMock.Setup(x => x.ActionContext).Returns(actionContext);
            //accessorMock.Setup(x => x.ActionContext.HttpContext).Returns(fakeContext);
            fakeContext.Request.Headers.Add("Authorization", fakeToken);

            var idenetity = new ClaimsIdentity();
            idenetity.AddClaim(new Claim("UserProfileId", "1"));
            idenetity.AddClaim(new Claim("ContactId", "1"));
            idenetity.AddClaim(new Claim("Email", "someone@email.com"));
            ClaimsPrincipal principal = new ClaimsPrincipal(idenetity);

            tokenServiceMock.Setup(x => x.GetPrincipalFrom2FaToken(It.IsAny<string>())).ReturnsAsync(principal);


            // Act
            var otpTracing = new OtpTracingService(new UnitOfWork<IdentityContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object, accessorMock.Object,
                tokenServiceMock.Object, loggerMock);
            int results = await otpTracing.Create2FaLogAsync(fakeOtpTracing.Phone, fakeOtpTracing.CodeEntered, fakeOtpTracing.ContactId, fakeOtpTracing.Email, fakeOtpTracing.Message, fakeOtpTracing.OtpRequestId, contexTenantModel, fakeVerifyResponse);
            dataContext.Database.EnsureDeleted();

            // Asert
            Assert.Equal(1, results);
        }

        [Fact]
        public async Task TestGetVerificatioAttemptsCountAsync()
        {
            // Arrange
            DbContextOptions<IdentityContext> options;
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseInMemoryDatabase("Identity");
            options = builder.Options;
            using IdentityContext dataContext = new IdentityContext(options);
            dataContext.Database.EnsureCreated();
            dataContext.SaveChanges();

            OtpTracing fakeOtpTracing = new OtpTracing()
            {
                BranchId = 1,
                CarrierName = "fakeCarrierName",
                CarrierType = "fakeCarrierType",
                CodeEntered = "fakeCode",
                ContactId = 1,
                DateUtc = DateTime.UtcNow,
                Email = "fake@email.com",
                EntityIdentifier = Guid.NewGuid(),
                Id = 1,
                IpAddress = "1.1.1.1",
                Message = "fakeMessage",
                OtpCreatedOn = DateTime.UtcNow,
                OtpRequestId = "fakeOtpRequestId",
                OtpUpdatedOn = DateTime.UtcNow.AddMinutes((-5)),
                Phone = "123456789",
                StatusCode = 200,
                TenantId = 1,
                ResponseJson = "fakeJson",
                TracingTypeId = 1,
            };
            dataContext.Add<OtpTracing>(fakeOtpTracing);
            dataContext.SaveChanges();

            Mock<IUnitOfWork<IdentityContext>> uowMock = new Mock<IUnitOfWork<IdentityContext>>();
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IActionContextAccessor> accessorMock = new Mock<IActionContextAccessor>();
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            var loggerMock = Mock.Of<ILogger<OtpTracingService>>();

            

            // Act
            var otpTracing = new OtpTracingService(new UnitOfWork<IdentityContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object, accessorMock.Object,
                tokenServiceMock.Object, loggerMock);
            int results = await otpTracing.GetVerificationAttemptsCountAsync(fakeOtpTracing.OtpRequestId);
            dataContext.Database.EnsureDeleted();

            // Asert
            Assert.Equal(1, results);
        }

        [Fact]
        public async Task TestGetSendAttemptsAsync()
        {
            // Arrange
            DbContextOptions<IdentityContext> options;
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseInMemoryDatabase("Identity");
            options = builder.Options;
            using IdentityContext dataContext = new IdentityContext(options);
            dataContext.Database.EnsureCreated();
            dataContext.SaveChanges();

            OtpTracing fakeOtpTracing = new OtpTracing()
            {
                BranchId = 1,
                CarrierName = "fakeCarrierName",
                CarrierType = "fakeCarrierType",
                CodeEntered = "fakeCode",
                ContactId = 1,
                DateUtc = DateTime.UtcNow,
                Email = "fake@email.com",
                EntityIdentifier = Guid.NewGuid(),
                Id = 1,
                IpAddress = "1.1.1.1",
                Message = "fakeMessage",
                OtpCreatedOn = DateTime.UtcNow,
                OtpRequestId = "fakeOtpRequestId",
                OtpUpdatedOn = DateTime.UtcNow.AddMinutes((-5)),
                Phone = "123456789",
                StatusCode = 200,
                TenantId = 1,
                ResponseJson = "fakeJson",
                TracingTypeId = 1,
            };
            dataContext.Add<OtpTracing>(fakeOtpTracing);
            dataContext.SaveChanges();

            Mock<IUnitOfWork<IdentityContext>> uowMock = new Mock<IUnitOfWork<IdentityContext>>();
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IActionContextAccessor> accessorMock = new Mock<IActionContextAccessor>();
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            var loggerMock = Mock.Of<ILogger<OtpTracingService>>();



            // Act
            var otpTracing = new OtpTracingService(new UnitOfWork<IdentityContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object, accessorMock.Object,
                tokenServiceMock.Object, loggerMock);
            var results = await otpTracing.GetSendAttemptsAsync(fakeOtpTracing.OtpRequestId);
            dataContext.Database.EnsureDeleted();

            // Asert
            Assert.NotNull(results);
            Assert.Equal(1, results.Count);
        }

        [Fact]
        public async Task TestGetLastSendAttemptAsyncWhenPhoneNumberIsNull()
        {
            // Arrange
            DbContextOptions<IdentityContext> options;
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseInMemoryDatabase("Identity");
            options = builder.Options;
            using IdentityContext dataContext = new IdentityContext(options);
            dataContext.Database.EnsureCreated();
            dataContext.SaveChanges();

            OtpTracing fakeOtpTracing = new OtpTracing()
            {
                BranchId = 1,
                CarrierName = "fakeCarrierName",
                CarrierType = "fakeCarrierType",
                CodeEntered = "fakeCode",
                ContactId = 1,
                DateUtc = DateTime.UtcNow,
                Email = "fake@email.com",
                EntityIdentifier = Guid.NewGuid(),
                Id = 1,
                IpAddress = "1.1.1.1",
                Message = "fakeMessage",
                OtpCreatedOn = DateTime.UtcNow,
                OtpRequestId = "fakeOtpRequestId",
                OtpUpdatedOn = DateTime.UtcNow.AddMinutes((-5)),
                Phone = null,
                StatusCode = 200,
                TenantId = 1,
                ResponseJson = "fakeJson",
                TracingTypeId = 1,
            };
            dataContext.Add<OtpTracing>(fakeOtpTracing);
            dataContext.SaveChanges();

            Mock<IUnitOfWork<IdentityContext>> uowMock = new Mock<IUnitOfWork<IdentityContext>>();
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IActionContextAccessor> accessorMock = new Mock<IActionContextAccessor>();
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            var loggerMock = Mock.Of<ILogger<OtpTracingService>>();



            // Act
            var otpTracing = new OtpTracingService(new UnitOfWork<IdentityContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object, accessorMock.Object,
                tokenServiceMock.Object, loggerMock);
            var results = await otpTracing.GetLastSendAttemptAsync(fakeOtpTracing.Phone, fakeOtpTracing.OtpRequestId);
            dataContext.Database.EnsureDeleted();

            // Asert
            Assert.NotNull(results);
        }

        [Fact]
        public async Task TestGetLastSendAttemptAsyncWhenPhoneNumberIsNotNull()
        {
            // Arrange
            DbContextOptions<IdentityContext> options;
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseInMemoryDatabase("Identity");
            options = builder.Options;
            using IdentityContext dataContext = new IdentityContext(options);
            await dataContext.Database.EnsureCreatedAsync();
            await dataContext.SaveChangesAsync();

            OtpTracing fakeOtpTracing = new OtpTracing()
            {
                BranchId = 1,
                CarrierName = "fakeCarrierName",
                CarrierType = "fakeCarrierType",
                CodeEntered = "fakeCode",
                ContactId = 1,
                DateUtc = DateTime.UtcNow,
                Email = "fake@email.com",
                EntityIdentifier = Guid.NewGuid(),
                Id = 1,
                IpAddress = "1.1.1.1",
                Message = "fakeMessage",
                OtpCreatedOn = DateTime.UtcNow,
                OtpRequestId = "fakeOtpRequestId",
                OtpUpdatedOn = DateTime.UtcNow.AddMinutes((-5)),
                Phone = "+1963258741",
                StatusCode = 200,
                TenantId = 1,
                ResponseJson = "fakeJson",
                TracingTypeId = 1,
            };
            dataContext.Add<OtpTracing>(fakeOtpTracing);
            dataContext.SaveChanges();

            Mock<IUnitOfWork<IdentityContext>> uowMock = new Mock<IUnitOfWork<IdentityContext>>();
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IActionContextAccessor> accessorMock = new Mock<IActionContextAccessor>();
            Mock<ITokenService> tokenServiceMock = new Mock<ITokenService>();
            var loggerMock = Mock.Of<ILogger<OtpTracingService>>();



            // Act
            var otpTracing = new OtpTracingService(new UnitOfWork<IdentityContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object, accessorMock.Object,
                tokenServiceMock.Object, loggerMock);
            var results = await otpTracing.GetLastSendAttemptAsync(fakeOtpTracing.Phone, fakeOtpTracing.OtpRequestId);
            dataContext.Database.EnsureDeleted();

            // Asert
            Assert.NotNull(results);
        }
    }
}
