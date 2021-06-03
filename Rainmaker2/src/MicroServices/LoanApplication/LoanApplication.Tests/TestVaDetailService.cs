using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
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

namespace LoanApplication.Tests
{
    public class TestVaDetailService
    {
        [Fact]
        public async Task TestAddOrUpdate()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplicationVATestAddOrUpdate");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContextVATestAddOrUpdate");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
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
                            },
                            VaDetails = new List<VaDetail>()
                            {
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Active_Military,
                                    ExpirationDateUtc =  DateTime.UtcNow.AddYears(-5)
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Reserves_National_Guard,
                                    ReserveEverActivated = true
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Surviving_Spouse
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    LoanApplicationId = (int)MilitaryAffiliationEnum.Veteran
                                }
                            }
                        }
                    }
                };
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await applicationContext.SaveChangesAsync();

            List<int> affiliationIds = new EditableList<int>()
            {
                (int) MilitaryAffiliationEnum.Active_Military,
                (int) MilitaryAffiliationEnum.Reserves_National_Guard,
                (int) MilitaryAffiliationEnum.Surviving_Spouse,
                (int) MilitaryAffiliationEnum.Veteran
            };

            VaDetailAddOrUpdate fakeModelToSave = new VaDetailAddOrUpdate()
            {
                BorrowerId = fakeApplication.Borrowers.First().Id,
                ExpirationDateUtc = DateTime.UtcNow.AddYears(-4),
                ReserveEverActivated = true,
                State = "fakeState",
                IsVaEligible = true,
                MilitaryAffiliationIds = affiliationIds.ToArray()
            };

            // Act
            var service = new VaDetailService(
                new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object
                , new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories())));

            var results = await service.AddOrUpdate(fakeTenant, fakeUserId, fakeModelToSave);

            await applicationContext.Database.EnsureDeletedAsync();
            await tenantContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.Equal(fakeApplication.Borrowers.First().Id, results);
        }

        [Fact]
        public async Task TestAddOrUpdateWhenBorrowerNotFound()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplicationTestAddOrUpdateWhenBorrowerNotFound");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContextTestAddOrUpdateWhenBorrowerNotFound");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>()
                    {
                    }
                };
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await applicationContext.SaveChangesAsync();

            List<int> affiliationIds = new EditableList<int>()
            {
                (int) MilitaryAffiliationEnum.Active_Military,
                (int) MilitaryAffiliationEnum.Reserves_National_Guard,
                (int) MilitaryAffiliationEnum.Surviving_Spouse,
                (int) MilitaryAffiliationEnum.Veteran
            };

            VaDetailAddOrUpdate fakeModelToSave = new VaDetailAddOrUpdate()
            {
                BorrowerId = 1,
                ExpirationDateUtc = DateTime.UtcNow.AddYears(-4),
                ReserveEverActivated = true,
                State = "fakeState",
                IsVaEligible = true,
                MilitaryAffiliationIds = affiliationIds.ToArray()
            };

            // Act
            var service = new VaDetailService(
                new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object
                , new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories())));

            var results = await service.AddOrUpdate(fakeTenant, fakeUserId, fakeModelToSave);

            await applicationContext.Database.EnsureDeletedAsync();
            await tenantContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.Equal(0, results);
        }

        [Fact]
        public async Task TestGetVaDetails()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplication");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
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
                            IsVaEligible = true,
                            LoanContact_LoanContactId = new LoanContact()
                            {
                                TenantId = fakeTenant.Id,
                                FirstName = "FirstName",
                                MiddleName = "MiddleName",
                                LastName = "LastName",
                                EmailAddress = "fake@email.com",
                            },
                            VaDetails = new List<VaDetail>()
                            {
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Active_Military,
                                    ExpirationDateUtc =  DateTime.UtcNow.AddYears(-5)
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Reserves_National_Guard,
                                    ReserveEverActivated = true
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Surviving_Spouse
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    LoanApplicationId = (int)MilitaryAffiliationEnum.Veteran
                                }
                            }
                        }
                    }
                };
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await applicationContext.SaveChangesAsync();
            int fakeBorrowerId = fakeApplication.Borrowers.First().Id;

            // Act
            var service = new VaDetailService(
                new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object
                , new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories())));

            var results = await service.GetVaDetails(fakeTenant, fakeUserId, fakeBorrowerId);

            await applicationContext.Database.EnsureDeletedAsync();
            await tenantContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);
        }

        [Fact]
        public async Task TestSetBorrowerVaStatusWhenBorrowerNotFound()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplication");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>()
                    {
                        
                    }
                };
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await applicationContext.SaveChangesAsync();

            List<int> affiliationIds = new EditableList<int>()
            {
                (int) MilitaryAffiliationEnum.Active_Military,
                (int) MilitaryAffiliationEnum.Reserves_National_Guard,
                (int) MilitaryAffiliationEnum.Surviving_Spouse,
                (int) MilitaryAffiliationEnum.Veteran
            };

            BorrowerVaStatusModel fakeModelToSave = new BorrowerVaStatusModel()
            {
                BorrowerId = 1,
                State = "fakeState",
                IsVaEligible = true
            };

            // Act
            var service = new VaDetailService(
                new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object
                , new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories())));

            var results = await service.SetBorrowerVaStatus(fakeTenant, fakeUserId, fakeModelToSave);

            await applicationContext.Database.EnsureDeletedAsync();
            await tenantContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.Equal(0, results);
        }

        [Fact]
        public async Task TestSetBorrowerVaStatus()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplication");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
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
                            IsVaEligible = true,
                            LoanContact_LoanContactId = new LoanContact()
                            {
                                TenantId = fakeTenant.Id,
                                FirstName = "FirstName",
                                MiddleName = "MiddleName",
                                LastName = "LastName",
                                EmailAddress = "fake@email.com"
                            },
                            VaDetails = new List<VaDetail>()
                            {
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Active_Military,
                                    ExpirationDateUtc =  DateTime.UtcNow.AddYears(-5)
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Reserves_National_Guard,
                                    ReserveEverActivated = true
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Surviving_Spouse
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    LoanApplicationId = (int)MilitaryAffiliationEnum.Veteran
                                }
                            }
                        }
                    }
                };
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await applicationContext.SaveChangesAsync();

            List<int> affiliationIds = new EditableList<int>()
            {
                (int) MilitaryAffiliationEnum.Active_Military,
                (int) MilitaryAffiliationEnum.Reserves_National_Guard,
                (int) MilitaryAffiliationEnum.Surviving_Spouse,
                (int) MilitaryAffiliationEnum.Veteran
            };

            BorrowerVaStatusModel fakeModelToSave = new BorrowerVaStatusModel()
            {
                BorrowerId = fakeApplication.Borrowers.First().Id,
                State = "fakeState",
                IsVaEligible = true
            };

            // Act
            var service = new VaDetailService(
                new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object
                , new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories())));

            var results = await service.SetBorrowerVaStatus(fakeTenant, fakeUserId, fakeModelToSave);

            await applicationContext.Database.EnsureDeletedAsync();
            await tenantContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.Equal(fakeApplication.Borrowers.First().Id, results);
        }

        [Fact]
        public async Task TestGetBorrowerVaStatus()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplication");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
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
                            IsVaEligible = true,
                            LoanContact_LoanContactId = new LoanContact()
                            {
                                TenantId = fakeTenant.Id,
                                FirstName = "FirstName",
                                MiddleName = "MiddleName",
                                LastName = "LastName",
                                EmailAddress = "fake@email.com"
                            },
                            VaDetails = new List<VaDetail>()
                            {
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Active_Military,
                                    ExpirationDateUtc =  DateTime.UtcNow.AddYears(-5)
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Reserves_National_Guard,
                                    ReserveEverActivated = true
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Surviving_Spouse
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    LoanApplicationId = (int)MilitaryAffiliationEnum.Veteran
                                }
                            }
                        }
                    }
                };
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await applicationContext.SaveChangesAsync();

            List<int> affiliationIds = new EditableList<int>()
            {
                (int) MilitaryAffiliationEnum.Active_Military,
                (int) MilitaryAffiliationEnum.Reserves_National_Guard,
                (int) MilitaryAffiliationEnum.Surviving_Spouse,
                (int) MilitaryAffiliationEnum.Veteran
            };

            // Act
            var service = new VaDetailService(
                new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object
                , new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories())));

            var results = await service.GetBorrowerVaStatus(fakeTenant,1, fakeApplication.Borrowers.First().Id);

            await applicationContext.Database.EnsureDeletedAsync();
            await tenantContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.Equal(fakeApplication.Borrowers.First().IsVaEligible, results);
        }
    }
}
