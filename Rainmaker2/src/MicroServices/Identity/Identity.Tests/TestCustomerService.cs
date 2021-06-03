using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Identity.Data;
using Identity.Entity.Models;
using Identity.Model;
using Identity.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using TrackableEntities.Common.Core;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace Identity.Tests
{
    public class TestCustomerService
    {
        [Fact]
        public async Task TestGetCustomerByUserIdAsync()
        {
            // Arrange
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            var tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            await tenantContext.Database.EnsureCreatedAsync();
            await tenantContext.SaveChangesAsync();

            var identityBuilder = new DbContextOptionsBuilder<IdentityContext>();
            identityBuilder.UseInMemoryDatabase("IdentityContext");
            var identityOptions = identityBuilder.Options;
            using IdentityContext identityContext = new IdentityContext(identityOptions);
            await identityContext.Database.EnsureCreatedAsync();
            await identityContext.SaveChangesAsync();

            Customer fakeCustomer = new Customer()
            {
                IsActive = true,
                ContactId = 1,
                CreatedBy = 1,
                CreatedOnUtc = DateTime.UtcNow,
                EntityIdentifier = Guid.NewGuid(),
                Id = 1,
                Remarks = "fake remarks",
                TenantId = 1,
                TrackingState = TrackingState.Added,
                UserId = 1
            };
            tenantContext.Add<Customer>(fakeCustomer);
            await tenantContext.SaveChangesAsync();

            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var loggerMock = Mock.Of<ILogger<CustomerService>>();

            // Act
            var service = new CustomerService( new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories())),
                    serviceProviderMock.Object, new UnitOfWork<IdentityContext>(identityContext, new RepositoryProvider(new RepositoryFactories())), loggerMock);
            var results = await service.GetCustomerByUserIdAsync(fakeCustomer.UserId.Value, fakeCustomer.TenantId.Value, null);

            // Assert
            Assert.NotNull(results);
        }

        [Fact]
        public async Task TestMapPhoneNumberFromOtpTracingWhenNotFound()
        {
            // Arrange 
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            var tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            await tenantContext.Database.EnsureCreatedAsync();
            await tenantContext.SaveChangesAsync();

            var identityBuilder = new DbContextOptionsBuilder<IdentityContext>();
            identityBuilder.UseInMemoryDatabase("IdentityContext");
            var identityOptions = identityBuilder.Options;
            using IdentityContext identityContext = new IdentityContext(identityOptions);
            await identityContext.Database.EnsureCreatedAsync();
            await identityContext.SaveChangesAsync();

            //OtpTracing fakeOtpTracing = new OtpTracing()
            //{
            //    StatusCode = 200,
            //    Message = "Ok",
            //    OtpRequestId = "fakeOtpRequestId",
            //    DateUtc = DateTime.UtcNow,
            //    ResponseJson = "fakeJsonResponse",
            //    Phone = "fakePhone",
            //    BranchId = 1,
            //    CarrierName = "fakeCarrierName",
            //    CarrierType = "fakeCarrierType",
            //    CodeEntered = "fake12",
            //    ContactId = 1,
            //    Email = "fake@email.com",
            //    EntityIdentifier = Guid.NewGuid(),
            //    IpAddress = "1.1.1.1",
            //    OtpCreatedOn = DateTime.UtcNow,
            //    OtpUpdatedOn = DateTime.UtcNow.AddMinutes(10),
            //    TenantId = 1,
            //    TracingTypeId = null
            //};

            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var loggerMock = Mock.Of<ILogger<CustomerService>>();

            // Act
            var service = new CustomerService(new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories())),
                serviceProviderMock.Object, new UnitOfWork<IdentityContext>(identityContext, new RepositoryProvider(new RepositoryFactories())), loggerMock);
            var results = await service.MapPhoneNumberFromOtpTracingAsync(1, 1, "fake@email.com", "fakeSid",true);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<ApiResponse>(results);
            Assert.Equal(Convert.ToString((int)HttpStatusCode.NotFound), results.Code);
        }

        [Fact]
        public async Task TestMapPhoneNumberFromOtpTracingWhenCustomerNotFound()
        {
            // Arrange 
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            var tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            await tenantContext.Database.EnsureCreatedAsync();
            await tenantContext.SaveChangesAsync();

            var identityBuilder = new DbContextOptionsBuilder<IdentityContext>();
            identityBuilder.UseInMemoryDatabase("IdentityContext");
            var identityOptions = identityBuilder.Options;
            using IdentityContext identityContext = new IdentityContext(identityOptions);
            await identityContext.Database.EnsureCreatedAsync();
            await identityContext.SaveChangesAsync();

            OtpTracing fakeOtpTracing = new OtpTracing()
            {
                StatusCode = 200,
                Message = "Ok",
                OtpRequestId = "fakeOtpRequestId",
                DateUtc = DateTime.UtcNow,
                ResponseJson = "fakeJsonResponse",
                Phone = "fakePhone",
                BranchId = 1,
                CarrierName = "fakeCarrierName",
                CarrierType = "fakeCarrierType",
                CodeEntered = "fake12",
                ContactId = 1,
                Email = "fake@email.com",
                EntityIdentifier = Guid.NewGuid(),
                IpAddress = "1.1.1.1",
                OtpCreatedOn = DateTime.UtcNow,
                OtpUpdatedOn = DateTime.UtcNow.AddMinutes(10),
                TenantId = 1,
                TracingTypeId = null
            };
            identityContext.Add<OtpTracing>(fakeOtpTracing);
            await identityContext.SaveChangesAsync();

            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var loggerMock = Mock.Of<ILogger<CustomerService>>();

            // Act
            var service = new CustomerService(new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories())),
                serviceProviderMock.Object, new UnitOfWork<IdentityContext>(identityContext, new RepositoryProvider(new RepositoryFactories())), loggerMock);
            var results = await service.MapPhoneNumberFromOtpTracingAsync(1, fakeOtpTracing.TenantId.Value, fakeOtpTracing.Email, fakeOtpTracing.OtpRequestId, true);
            await tenantContext.Database.EnsureDeletedAsync();
            await identityContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);
            Assert.IsType<ApiResponse>(results);
            Assert.Equal(Convert.ToString((int)HttpStatusCode.NotFound), results.Code);
        }

        [Fact]
        public async Task TestMapPhoneNumberFromOtpTracingWhenCustomerContactNotFound()
        {
            // Arrange 
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            var tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            await tenantContext.Database.EnsureCreatedAsync();
            await tenantContext.SaveChangesAsync();

            var identityBuilder = new DbContextOptionsBuilder<IdentityContext>();
            identityBuilder.UseInMemoryDatabase("IdentityContext");
            var identityOptions = identityBuilder.Options;
            using IdentityContext identityContext = new IdentityContext(identityOptions);
            await identityContext.Database.EnsureCreatedAsync();
            await identityContext.SaveChangesAsync();

            OtpTracing fakeOtpTracing = new OtpTracing()
            {
                StatusCode = 200,
                Message = "Ok",
                OtpRequestId = "fakeOtpRequestId",
                DateUtc = DateTime.UtcNow,
                ResponseJson = "fakeJsonResponse",
                Phone = "fakePhone",
                BranchId = 1,
                CarrierName = "fakeCarrierName",
                CarrierType = "fakeCarrierType",
                CodeEntered = "fake12",
                ContactId = 1,
                Email = "fake@email.com",
                EntityIdentifier = Guid.NewGuid(),
                IpAddress = "1.1.1.1",
                OtpCreatedOn = DateTime.UtcNow,
                OtpUpdatedOn = DateTime.UtcNow.AddMinutes(10),
                TenantId = 1,
                TracingTypeId = null
            };
            identityContext.Add<OtpTracing>(fakeOtpTracing);
            await identityContext.SaveChangesAsync();

            Customer fakeCustomer = new Customer()
            {
                IsActive = true,
                TenantId = 1,
                EntityIdentifier = Guid.NewGuid(),
                ContactId = 1,
                UserId = 1
            };
            tenantContext.Add<Customer>(fakeCustomer);
            await tenantContext.SaveChangesAsync();

            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var loggerMock = Mock.Of<ILogger<CustomerService>>();

            // Act
            var service = new CustomerService(new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories())),
                serviceProviderMock.Object, new UnitOfWork<IdentityContext>(identityContext, new RepositoryProvider(new RepositoryFactories())), loggerMock);
            var results = await service.MapPhoneNumberFromOtpTracingAsync(1, fakeOtpTracing.TenantId.Value, fakeOtpTracing.Email, fakeOtpTracing.OtpRequestId, true);
            await tenantContext.Database.EnsureDeletedAsync();
            await identityContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);
            Assert.IsType<ApiResponse>(results);
            Assert.Equal(Convert.ToString((int)HttpStatusCode.NotFound), results.Code);
        }

        [Fact]
        public async Task TestMapPhoneNumberFromOtpTracingWhenCustomerPhoneInfoMissing()
        {
            // Arrange 
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            var tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            await tenantContext.Database.EnsureCreatedAsync();
            await tenantContext.SaveChangesAsync();

            var identityBuilder = new DbContextOptionsBuilder<IdentityContext>();
            identityBuilder.UseInMemoryDatabase("IdentityContext");
            var identityOptions = identityBuilder.Options;
            using IdentityContext identityContext = new IdentityContext(identityOptions);
            await identityContext.Database.EnsureCreatedAsync();
            await identityContext.SaveChangesAsync();

            OtpTracing fakeOtpTracing = new OtpTracing()
            {
                StatusCode = 200,
                Message = "Ok",
                OtpRequestId = "fakeOtpRequestId",
                DateUtc = DateTime.UtcNow,
                ResponseJson = "fakeJsonResponse",
                Phone = "fakePhone",
                BranchId = 1,
                CarrierName = "fakeCarrierName",
                CarrierType = "fakeCarrierType",
                CodeEntered = "fake12",
                ContactId = 1,
                Email = "fake@email.com",
                EntityIdentifier = Guid.NewGuid(),
                IpAddress = "1.1.1.1",
                OtpCreatedOn = DateTime.UtcNow,
                OtpUpdatedOn = DateTime.UtcNow.AddMinutes(10),
                TenantId = 1,
                TracingTypeId = null
            };
            identityContext.Add<OtpTracing>(fakeOtpTracing);
            await identityContext.SaveChangesAsync();

            Customer fakeCustomer = new Customer()
            {
                IsActive = true,
                TenantId = 1,
                Contact = new Contact()
                {
                    EntityIdentifier = Guid.NewGuid(),
                    TenantId = 1,
                    //ContactPhoneInfoes = new List<ContactPhoneInfo>()
                    //{
                    //    new ContactPhoneInfo()
                    //    {
                    //        Phone = "11111111",
                    //        TenantId = 1,
                    //        EntityIdentifier = Guid.NewGuid(),
                    //        ContactId = 1,
                    //        IsDeleted = false,
                    //        IsValid = true,

                    //    }
                    //}
                },
                EntityIdentifier = Guid.NewGuid(),
                ContactId = 1,
                UserId = 1
            };
            tenantContext.Add<Customer>(fakeCustomer);
            await tenantContext.SaveChangesAsync();

            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var loggerMock = Mock.Of<ILogger<CustomerService>>();

            // Act
            var service = new CustomerService(new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories())),
                serviceProviderMock.Object, new UnitOfWork<IdentityContext>(identityContext, new RepositoryProvider(new RepositoryFactories())), loggerMock);
            var results = await service.MapPhoneNumberFromOtpTracingAsync(1, fakeOtpTracing.TenantId.Value, fakeOtpTracing.Email, fakeOtpTracing.OtpRequestId, true);
            await tenantContext.Database.EnsureDeletedAsync();
            await identityContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);
            Assert.IsType<ApiResponse>(results);
            Assert.Equal(Convert.ToString((int)HttpStatusCode.Created), results.Code);
        }

        [Fact]
        public async Task TestMapPhoneNumberFromOtpTracingWhenCustomerPhoneNotVerified()
        {
            // Arrange 
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            var tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            await tenantContext.Database.EnsureCreatedAsync();
            await tenantContext.SaveChangesAsync();

            var identityBuilder = new DbContextOptionsBuilder<IdentityContext>();
            identityBuilder.UseInMemoryDatabase("IdentityContext");
            var identityOptions = identityBuilder.Options;
            using IdentityContext identityContext = new IdentityContext(identityOptions);
            await identityContext.Database.EnsureCreatedAsync();
            await identityContext.SaveChangesAsync();

            OtpTracing fakeOtpTracing = new OtpTracing()
            {
                StatusCode = 200,
                Message = "Ok",
                OtpRequestId = "fakeOtpRequestId",
                DateUtc = DateTime.UtcNow,
                ResponseJson = "fakeJsonResponse",
                Phone = "11111111",
                BranchId = 1,
                CarrierName = "fakeCarrierName",
                CarrierType = "fakeCarrierType",
                CodeEntered = "fake12",
                ContactId = 1,
                Email = "fake@email.com",
                EntityIdentifier = Guid.NewGuid(),
                IpAddress = "1.1.1.1",
                OtpCreatedOn = DateTime.UtcNow,
                OtpUpdatedOn = DateTime.UtcNow.AddMinutes(10),
                TenantId = 1,
                TracingTypeId = null
            };
            identityContext.Add<OtpTracing>(fakeOtpTracing);
            await identityContext.SaveChangesAsync();

            Customer fakeCustomer = new Customer()
            {
                IsActive = true,
                TenantId = 1,
                Contact = new Contact()
                {
                    EntityIdentifier = Guid.NewGuid(),
                    TenantId = 1,
                    ContactPhoneInfoes = new List<ContactPhoneInfo>()
                    {
                        new ContactPhoneInfo()
                        {
                            Phone = "11111111",
                            TenantId = 1,
                            EntityIdentifier = Guid.NewGuid(),
                            ContactId = 1,
                            IsDeleted = false,
                            IsValid = false,
                        }
                    }
                },
                EntityIdentifier = Guid.NewGuid(),
                ContactId = 1,
                UserId = 1
            };
            tenantContext.Add<Customer>(fakeCustomer);
            await tenantContext.SaveChangesAsync();

            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var loggerMock = Mock.Of<ILogger<CustomerService>>();

            // Act
            var service = new CustomerService(new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories())),
                serviceProviderMock.Object, new UnitOfWork<IdentityContext>(identityContext, new RepositoryProvider(new RepositoryFactories())), loggerMock);
            var results = await service.MapPhoneNumberFromOtpTracingAsync(1, fakeOtpTracing.TenantId.Value, fakeOtpTracing.Email, fakeOtpTracing.OtpRequestId, true);
            await tenantContext.Database.EnsureDeletedAsync();
            await identityContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);
            Assert.IsType<ApiResponse>(results);
            Assert.Equal(Convert.ToString((int)HttpStatusCode.OK), results.Code);
        }
    }
}
