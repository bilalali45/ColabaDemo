using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Identity.Data;
using Identity.Service;
using Microsoft.EntityFrameworkCore;
using Moq;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using TrackableEntities.Common.Core;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace Identity.Tests
{
    public class TestTenantConfigService
    {
        [Fact]
        public async Task TestGetTenant2FaConfigAsyncWithNoInclude()
        {
            // Arrange
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            dataContext.SaveChanges();

            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();

            TwoFaConfig fakeConfig = new TwoFaConfig()
            {
                BorrowerTwoFaModeId = 3,
                BranchId = 1,
                EntityIdentifier = Guid.NewGuid(),
                IsActive = true,
                McuTwoFaModeId = 1,
                TenantId = 1,
                TrackingState = TrackingState.Added,
                TwilioVerifyServiceId = "fakeServiceId",
            };
            dataContext.Add<TwoFaConfig>(fakeConfig);
            dataContext.SaveChanges();

            Tenant fakeTenant = new Tenant()
            {
                IsActive = true,
                Id = 1
            };
            dataContext.Add<Tenant>(fakeTenant);
            dataContext.SaveChanges();

            // Act
            var service = new TenantConfigService(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object);
            var results = await service.GetTenant2FaConfigAsync(fakeTenant.Id, null);
            dataContext.Database.EnsureDeleted();

            // Assert
            Assert.NotNull(results);
            Assert.IsType<TwoFaConfig>(results);
        }

        [Fact]
        public async Task TestGetTenantById()
        {
            // Arrange
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantContext");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            dataContext.SaveChanges();

            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            dataContext.SaveChanges();

            Tenant fakeTenant = new Tenant()
            {
                IsActive = true,
                Id = 1
            };
            dataContext.Add<Tenant>(fakeTenant);
            dataContext.SaveChanges();

            // Act
            var service = new TenantConfigService(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object);
            var results = await service.GetTenantById(fakeTenant.Id);
            dataContext.Database.EnsureDeleted();

            // Assert
            Assert.NotNull(results);
            Assert.IsType<Tenant>(results);
        }
    }
}
