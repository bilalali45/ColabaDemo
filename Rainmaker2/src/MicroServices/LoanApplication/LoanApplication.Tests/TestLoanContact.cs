using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LoanApplication.Model;
using LoanApplication.Service;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;
using LoanApplication = LoanApplicationDb.Entity.Models.LoanApplication;

namespace LoanApplication.Tests
{
    public class TestLoanContact
    {
        [Fact]
        public async Task TestUpdateDobSsnWhenBorrowerDoesNotExist()
        {
            // Arrange
            DbContextOptions<TenantConfigContext> options2;
            var builder2 = new DbContextOptionsBuilder<TenantConfigContext>();
            builder2.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options2 = builder2.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(options2);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(options);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IEncryptionService> enxryptionService = new Mock<IEncryptionService>();

            var service = new LoanContactService(
                new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories())),
                new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object, enxryptionService.Object);

            

            int fakeUserId = 1;
            int fakeBorrowerId = 1;
            int fakeLoanApplicationId = 1;
            int fakeTenantId = 1;
            int wrongBorrowerId = 99;

            TenantModel tenant = new TenantModel()
            {
                Id = fakeTenantId
            };

            BorrowerDobSsnAddOrUpdate fakePostModel = new BorrowerDobSsnAddOrUpdate()
            {
                BorrowerId = wrongBorrowerId,
                DobUtc = DateTime.UtcNow.AddYears(-18),
                LoanApplicationId = fakeLoanApplicationId,
                State = "Fake State",
                Ssn = "123456789"
            };

            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>()
                    {
                        new Borrower()
                        {
                            TenantId = fakeTenantId,
                            LoanContact_LoanContactId = new LoanContact()
                            {

                            }
                        }
                    }
                };
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();

            // Act
            var results = await service.UpdateDobSsn(tenant, fakeUserId, fakePostModel);

            await tenantContext.Database.EnsureDeletedAsync();
            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.Equal(0, results);
        }

        [Fact]
        public async Task TestUpdateDobSsnWhenBorrowerLoanContactExist()
        {
            // Arrange
            DbContextOptions<TenantConfigContext> options2;
            var builder2 = new DbContextOptionsBuilder<TenantConfigContext>();
            builder2.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options2 = builder2.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(options2);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(options);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IEncryptionService> enxryptionService = new Mock<IEncryptionService>();

            var service = new LoanContactService(
                new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories())),
                new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object, enxryptionService.Object);



            int fakeUserId = 1;
            int fakeBorrowerId = 1;
            int fakeLoanApplicationId = 1;
            int fakeTenantId = 1;

            TenantModel tenant = new TenantModel()
            {
                Id = fakeTenantId
            };

            BorrowerDobSsnAddOrUpdate fakePostModel = new BorrowerDobSsnAddOrUpdate()
            {
                BorrowerId = fakeBorrowerId,
                DobUtc = DateTime.UtcNow.AddYears(-18),
                LoanApplicationId = fakeLoanApplicationId,
                State = "Fake State",
                Ssn = "123456789"
            };

            Borrower fakeBorrower = new Borrower()
            {
                TenantId = fakeTenantId,
                LoanContact_LoanContactId = new LoanContact()
                {

                }
            };
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    UserId = fakeUserId,
                    TenantId = fakeTenantId,
                    Borrowers = new List<Borrower>()
                    {
                        fakeBorrower
                    }
                };
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();

            // Act
            var results = await service.UpdateDobSsn(tenant, fakeUserId, fakePostModel);

            await tenantContext.Database.EnsureDeletedAsync();
            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.Equal(fakeBorrower.LoanContact_LoanContactId.Id, results);
        }

        [Fact]
        public async Task TestUpdateDobSsnWhenBorrowerLoanContactDoesNotExist()
        {
            // Arrange
            DbContextOptions<TenantConfigContext> options2;
            var builder2 = new DbContextOptionsBuilder<TenantConfigContext>();
            builder2.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options2 = builder2.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(options2);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(options);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IEncryptionService> enxryptionService = new Mock<IEncryptionService>();

            var service = new LoanContactService(
                new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories())),
                new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object, enxryptionService.Object);



            int fakeUserId = 1;
            int fakeBorrowerId = 1;
            int fakeLoanApplicationId = 1;
            int fakeTenantId = 1;

            TenantModel tenant = new TenantModel()
            {
                Id = fakeTenantId
            };

            BorrowerDobSsnAddOrUpdate fakePostModel = new BorrowerDobSsnAddOrUpdate()
            {
                BorrowerId = fakeBorrowerId,
                DobUtc = DateTime.UtcNow.AddYears(-18),
                LoanApplicationId = fakeLoanApplicationId,
                State = "Fake State",
                Ssn = "123456789"
            };

            Borrower fakeBorrower = new Borrower()
            {
                TenantId = fakeTenantId,
                LoanContact_LoanContactId = null
            };
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    UserId = fakeUserId,
                    TenantId = fakeTenantId,
                    Borrowers = new List<Borrower>()
                    {
                        fakeBorrower
                    }
                };
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();

            // Act
            var results = await service.UpdateDobSsn(tenant, fakeUserId, fakePostModel);

            await tenantContext.Database.EnsureDeletedAsync();
            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.Equal(fakeBorrower.LoanContact_LoanContactId.Id, results);
        }

        [Fact]
        public async Task TestGetDobSsnWhenBorrowerDoesNotExist()
        {
            // Arrange
            DbContextOptions<TenantConfigContext> options2;
            var builder2 = new DbContextOptionsBuilder<TenantConfigContext>();
            builder2.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options2 = builder2.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(options2);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(options);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IEncryptionService> enxryptionService = new Mock<IEncryptionService>();

            var service = new LoanContactService(
                new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories())),
                new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object, enxryptionService.Object);



            int fakeUserId = 1;
            int fakeBorrowerId = 1;
            int fakeLoanApplicationId = 1;
            int fakeTenantId = 1;

            TenantModel tenant = new TenantModel()
            {
                Id = fakeTenantId
            };

            BorrowerDobSsnAddOrUpdate fakePostModel = new BorrowerDobSsnAddOrUpdate()
            {
                BorrowerId = fakeBorrowerId,
                DobUtc = DateTime.UtcNow.AddYears(-18),
                LoanApplicationId = fakeLoanApplicationId,
                State = "Fake State",
                Ssn = "123456789"
            };

            Borrower fakeBorrower = new Borrower()
            {
                TenantId = fakeTenantId,
                LoanContact_LoanContactId = null
            };
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    UserId = fakeUserId,
                    TenantId = fakeTenantId,
                    Borrowers = null
                };
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();

            // Act
            var results = await service.GetDobSsn(tenant, fakeBorrowerId, fakeLoanApplicationId, fakeUserId);

            await tenantContext.Database.EnsureDeletedAsync();
            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.Null(results);
        }

        [Fact]
        public async Task TestGetDobSsnWhenBorrowerExist()
        {
            // Arrange
            DbContextOptions<TenantConfigContext> options2;
            var builder2 = new DbContextOptionsBuilder<TenantConfigContext>();
            builder2.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options2 = builder2.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(options2);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(options);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IEncryptionService> enxryptionService = new Mock<IEncryptionService>();
            var service = new LoanContactService(
                new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories())),
                new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object, enxryptionService.Object);



            int fakeUserId = 1;
            int fakeBorrowerId = 1;
            int fakeLoanApplicationId = 1;
            int fakeTenantId = 1;

            TenantModel tenant = new TenantModel()
            {
                Id = fakeTenantId
            };

            Borrower fakeBorrower = new Borrower()
            {
                TenantId = fakeTenantId,
                LoanContact_LoanContactId = new LoanContact()
                {
                    DobUtc = DateTime.UtcNow.AddYears(-18),
                    Ssn = "123456789"
                }
            };
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    UserId = fakeUserId,
                    TenantId = fakeTenantId,
                    Borrowers = new List<Borrower>()
                    {
                        fakeBorrower
                    }
                };
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();

            // Act
            var results = await service.GetDobSsn(tenant, fakeBorrowerId, fakeLoanApplicationId, fakeUserId);

            await tenantContext.Database.EnsureDeletedAsync();
            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);
        }
    }
}
