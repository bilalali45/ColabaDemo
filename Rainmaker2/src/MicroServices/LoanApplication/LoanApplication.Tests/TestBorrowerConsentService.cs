using LoanApplicationDb.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using LoanApplication.Model;
using LoanApplication.Service;
using LoanApplicationDb.Entity.Models;
using TenantConfig.Common.DistributedCache;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;
using LoanApplication = LoanApplicationDb.Entity.Models.LoanApplication;

namespace LoanApplication.Tests
{
    public class TestBorrowerConsentService
    {
        [Fact]
        public async Task TestAddOrUpdate()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            using LoanApplicationContext dataContext = new LoanApplicationContext(options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            int fakeUserId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };

            LoanApplicationDb.Entity.Models.LoanApplication fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>()
                    {
                        new Borrower ()
                        {
                            TenantId = fakeTenant.Id,
                            LoanContact_LoanContactId = new LoanContact()
                            {
                                TenantId = fakeTenant.Id,
                                FirstName = "FirstName",
                                MiddleName = "MiddleName",
                                LastName = "LastName",
                                EmailAddress = "fake@email.com"
                            }
                        }
                    }
                };
            dataContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await dataContext.SaveChangesAsync();

            BorrowerConsentModel fakeConsentModel = new BorrowerConsentModel()
            {
                ConsentTypeId = 1,
                BorrowerId = fakeApplication.Borrowers.First().Id,
                Description = "Fake Description",
                IsAccepted = true,
                LoanApplicationId = fakeApplication.Id,
                State = "fakeState"
            };




            // Act
            BorrowerConsentService service = new BorrowerConsentService(new UnitOfWork<LoanApplicationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object, dbFunctionServiceMock.Object);
            var results = await service.AddOrUpdate(fakeTenant, fakeConsentModel, "127.0.0.1", fakeUserId);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.Equal(1, results);
        }

        [Fact]
        public async Task TestGetBorrowerAcceptedConsentsWhenLogNotFound()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            using LoanApplicationContext dataContext = new LoanApplicationContext(options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();



            int fakeBorrowerId = 1;
            int fakeUserId = 1;
            int fakeTenantId = 1;
            int fakeLoanApplicationId = 1;

            TenantModel contextTenant = new TenantModel()
            {
                Id = fakeTenantId
            };

            await dataContext.SaveChangesAsync();
            var service = new BorrowerConsentService(new UnitOfWork<LoanApplicationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object, dbFunctionServiceMock.Object);

            // Act
            var results = await service.GetBorrowerAcceptedConsents(contextTenant, fakeUserId, fakeBorrowerId, fakeLoanApplicationId);


            await dataContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);
            Assert.False(results.IsAccepted);
        }

        [Fact]
        public async Task TestGetBorrowerAcceptedConsentsWhenLogFound()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            using LoanApplicationContext dataContext = new LoanApplicationContext(options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            int fakeBorrowerId = 1;
            int fakeUserId = 1;
            int fakeTenantId = 1;
            int fakeLoanApplicationId = 1;

            TenantModel contextTenant = new TenantModel()
            {
                Id = fakeTenantId
            };

            ConsentType consentType = new ConsentType()
            {
                Description = "Consent 1 Text",
                IsActive = true,
                IsDeleted = false
            };
            dataContext.Add<ConsentType>(consentType);
            await dataContext.SaveChangesAsync();

            // Add dummy borrower dummy consents to in memory database
            BorrowerConsent consent1 = new BorrowerConsent()
            {
                BorrowerId = fakeBorrowerId,
                ConsentText = consentType.Description,
                ConsentTypeId = consentType.Id,
                LoanApplicationId = fakeLoanApplicationId,
                LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    UserId = fakeUserId
                }
            };
            BorrowerConsent consent2 = new BorrowerConsent()
            {
                BorrowerId = fakeBorrowerId,
                ConsentText = "Consent 2 text",
                ConsentTypeId = 2,
                LoanApplicationId = fakeLoanApplicationId,
                LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    UserId = fakeUserId
                }
            };

            List<BorrowerConsent> borrowerConsentList = new EditableList<BorrowerConsent>()
            {
                consent1,
                consent2
            };
            dataContext.Add<BorrowerConsent>(consent1);
            dataContext.Add<BorrowerConsent>(consent2);

            //ConsentType fakeConsentType = new ConsentType()
            //{
            //    Description = "Fake consent description",
            //    DisplayOrder = 1,
            //    IsActive = true,
            //    IsDeleted = false,

            //};

            var fakeConsentTypeList = new List<ConsentType>() { consentType };
            var fakeConsentQueryResults = fakeConsentTypeList.AsQueryable();


            dbFunctionServiceMock.Setup(x => x.UdfConsentType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeConsentQueryResults);

            await dataContext.SaveChangesAsync();
            var service = new BorrowerConsentService(
                new UnitOfWork<LoanApplicationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())),
                serviceProviderMock.Object, dbFunctionServiceMock.Object);

            // Act
            var results = await service.GetBorrowerAcceptedConsents(contextTenant, fakeUserId, fakeBorrowerId, fakeLoanApplicationId);

            // Assert
            Assert.NotNull(results);
            Assert.True(results.IsAccepted);
            Assert.NotEmpty(results.AcceptedConsentList);
            await dataContext.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task TestGetBorrowerConsent()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            using LoanApplicationContext dataContext = new LoanApplicationContext(options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            int fakeUserId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };

            LoanApplicationDb.Entity.Models.LoanApplication fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>()
                    {
                        new Borrower ()
                        {
                            TenantId = fakeTenant.Id,
                            OwnTypeId = 1,
                            LoanContact_LoanContactId = new LoanContact()
                            {
                                TenantId = fakeTenant.Id,
                                FirstName = "FirstName",
                                MiddleName = "MiddleName",
                                LastName = "LastName",
                                EmailAddress = "fake@email.com"
                            }
                        }
                    }
                };
            dataContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await dataContext.SaveChangesAsync();

            int fakeBorrowerId = fakeApplication.Borrowers.First().Id;
            int ownTypeId = fakeApplication.Borrowers.First().OwnTypeId.Value;

            List<ConsentType> fakeConsentResults = new EditableList<ConsentType>()
            {
                new ConsentType()
                {
                    Description = "Consent 1 Text",
                    IsActive = true,
                    IsDeleted = false,
                    OwnTypeId = ownTypeId
                }
            };

            dbFunctionServiceMock.Setup(x => x.UdfConsentType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeConsentResults.AsQueryable);

            var service = new BorrowerConsentService(
                new UnitOfWork<LoanApplicationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())),
                serviceProviderMock.Object, dbFunctionServiceMock.Object);

            // Act
            var results = await service.GetBorrowerConsent(fakeTenant, fakeUserId, fakeBorrowerId);

            // Assert
            Assert.NotNull(results);
        }

        [Fact]
        public async Task TestAddOrUpdateMultipleConsentsWhenConsentAlreadyAccepted()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            using LoanApplicationContext dataContext = new LoanApplicationContext(options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            int fakeBorrowerId = 1;
            int fakeUserId = 1;
            int fakeTenantId = 1;
            int fakeLoanApplicationId = 1;

            TenantModel contextTenant = new TenantModel()
            {
                Id = fakeTenantId
            };

            Borrower fakeBorrower = new Borrower()
            {
                LoanApplicationId = fakeLoanApplicationId,

            };

            ConsentType consentType = new ConsentType()
            {
                Description = "Consent 1 Text"
            };
            dataContext.Add<ConsentType>(consentType);
            await dataContext.SaveChangesAsync();

            // Add dummy borrower dummy consents to in memory database
            BorrowerConsent consent1 = new BorrowerConsent()
            {
                BorrowerId = fakeBorrowerId,
                ConsentText = consentType.Description,
                ConsentTypeId = consentType.Id,
                LoanApplicationId = fakeLoanApplicationId,
                LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    UserId = fakeUserId
                }
            };
            List<BorrowerConsent> borrowerConsentList = new EditableList<BorrowerConsent>()
            {
                consent1
            };
            dataContext.Add<BorrowerConsent>(consent1);
            await dataContext.SaveChangesAsync();

            BorrowerMultipleConsentsModel fakeDataToSave = new BorrowerMultipleConsentsModel()
            {
                BorrowerId = fakeBorrowerId,
                LoanApplicationId = fakeLoanApplicationId,
                State = "fake state",
                IsAccepted = true,
                ConsentHash = "some consent hash",
                BorrowerConsents = new List<BorrowerConsentPostModel>()
                {
                    new BorrowerConsentPostModel()
                    {
                        Description = "Some consent description",
                        ConsentTypeId = 1
                    }
                }
            };

            var service = new BorrowerConsentService(new UnitOfWork<LoanApplicationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object, dbFunctionServiceMock.Object);

            // Act
            var results = await service.AddOrUpdateMultipleConsents(contextTenant, fakeDataToSave, "127.0.0.1", fakeUserId);

            // Assert
            Assert.Equal(-1, results);
        }

        [Fact]
        public async Task TestAddOrUpdateMultipleConsentsWhenAcceptedConsentDoesNotExist()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            using LoanApplicationContext dataContext = new LoanApplicationContext(options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            int fakeTenantId = 1;
            int fakeUserId = 1;
            TenantModel tenantContext = new TenantModel()
            {
                Id = fakeTenantId
            };

            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
            {
                UserId = fakeUserId,
                TenantId = fakeTenantId,
                Borrowers = new List<Borrower>()
                    {
                        new Borrower()
                        {
                            TenantId = fakeTenantId,
                            LoanContact_LoanContactId = new LoanContact()
                            {
                                FirstName = "FirstName",
                                MiddleName = "MiddleName",
                                LastName = "LastName",
                                EmailAddress = "someone@memail.com",
                                TenantId = fakeTenantId
                            }
                        }
                    }
            };
            dataContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await dataContext.SaveChangesAsync();

            //BorrowerConsent fakeBorrowerConsent = new BorrowerConsent()
            //{
            //    BorrowerId = fakeLoanApplication.Borrowers.First().Id,
            //    LoanApplicationId = fakeLoanApplication.Id
            //};
            //dataContext.Add<BorrowerConsent>(fakeBorrowerConsent);
            //await dataContext.SaveChangesAsync();

            BorrowerMultipleConsentsModel multipleConsents = new BorrowerMultipleConsentsModel()
            {
                BorrowerId = fakeLoanApplication.Borrowers.First().Id,
                LoanApplicationId = fakeLoanApplication.Id,
                IsAccepted = true,
                State = "Fake State",
                BorrowerConsents = new List<BorrowerConsentPostModel>()
                {
                    new BorrowerConsentPostModel()
                    {
                        Description = "Some consent description",
                        ConsentTypeId = 1
                    }
                }
            };

            var service = new BorrowerConsentService(new UnitOfWork<LoanApplicationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object, dbFunctionServiceMock.Object);

            // Act
            var results = await service.AddOrUpdateMultipleConsents(tenantContext, multipleConsents, "127.0.0.1", fakeUserId);
        }
    }
}
