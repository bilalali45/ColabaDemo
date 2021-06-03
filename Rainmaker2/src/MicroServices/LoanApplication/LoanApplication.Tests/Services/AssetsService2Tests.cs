using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using LoanApplication.Model;
using LoanApplication.Service;
using LoanApplication.Tests.Helpers;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TenantConfig.Common.DistributedCache;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;
using AssetCategory = LoanApplicationDb.Entity.Models.AssetCategory;
using LoanApplication = LoanApplicationDb.Entity.Models.LoanApplication;

namespace LoanApplication.Tests.Services
{
    public partial class AssetsServiceTests : UnitTestBase
    {
        [Fact]
        public void TestGetAllAssetCategories()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var logger = new Mock<ILogger<AssetsService>>();

            using LoanApplicationContext applicationContext = new LoanApplicationContext(base.LoanApplicationContextOptions);
            applicationContext.Database.EnsureCreated();
            applicationContext.SaveChanges();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            AssetCategory fakeAssetCategory = new AssetCategory()
            {
                Id = 1,
                Name = "Fake Name",
                DisplayName = "Fake Display Name"
            };

            List<LoanApplicationDb.Entity.Models.AssetCategory> fakeAssetCategories =
                new List<LoanApplicationDb.Entity.Models.AssetCategory>()
                {
                    fakeAssetCategory
                };

            applicationContext.Add<LoanApplicationDb.Entity.Models.AssetCategory>(fakeAssetCategory);
            applicationContext.SaveChangesAsync();

            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetCategories.AsQueryable());

            var service =
                new AssetsService(base.GetUnitOfWorkInstance<LoanApplicationContext>(applicationContext), serviceProviderMock.Object,
                    dbFunctionServiceMock.Object, logger.Object);

            // Act
            var results = service.GetAllAssetCategories(fakeTenant);
            applicationContext.Database.EnsureDeleted();

            // Assert
            Assert.NotNull(results);
            Assert.NotEmpty(results);
        }

        [Fact]
        public void TestGetAssetTypesByCategory()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var logger = new Mock<ILogger<AssetsService>>();

            using LoanApplicationContext applicationContext = new LoanApplicationContext(base.LoanApplicationContextOptions);
            applicationContext.Database.EnsureCreated();
            applicationContext.SaveChanges();

            int fakeTenantId = 1;
            int fakeAssetCategoryId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            AssetType fakeAssetType = new AssetType()
            {
                Id = 1,
                Name = "Fake Name",
                DisplayName = "Fake Display Name",
                AssetCategoryId = 1
            };

            List<LoanApplicationDb.Entity.Models.AssetType> fakeAssetTypes =
                new List<LoanApplicationDb.Entity.Models.AssetType>()
                {
                    fakeAssetType
                };

            applicationContext.Add<LoanApplicationDb.Entity.Models.AssetType>(fakeAssetType);
            applicationContext.SaveChangesAsync();

            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetTypes.AsQueryable());

            var service =
                new AssetsService(base.GetUnitOfWorkInstance<LoanApplicationContext>(applicationContext), serviceProviderMock.Object,
                    dbFunctionServiceMock.Object, logger.Object);

            // Act
            var results = service.GetAssetTypesByCategory(fakeTenant, fakeAssetCategoryId);
            applicationContext.Database.EnsureDeleted();

            // Assert
            Assert.NotNull(results);
            Assert.NotEmpty(results);
        }

        #region AddOrUpdateBorrowerAsset Unit Tests
        [Fact]
        public async Task TestAddOrUpdateBorrowerAssetWhenCategoryNotAllowed()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var logger = new Mock<ILogger<AssetsService>>();

            int invalidCategoryId = (int)BorrowerAssetCategory.Credits;
            AddOrUpdateBorrowerAssetModel fakeModelReceived = new AddOrUpdateBorrowerAssetModel()
            {
                AssetCategoryId = invalidCategoryId
            };

            int fakeTenantId = 1;
            int fakeUserId = 1;

            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            using LoanApplicationContext applicationContext = new LoanApplicationContext(base.LoanApplicationContextOptions);
            await applicationContext.Database.EnsureCreatedAsync();
            await applicationContext.SaveChangesAsync();

            var service =
                new AssetsService(base.GetUnitOfWorkInstance<LoanApplicationContext>(applicationContext), serviceProviderMock.Object,
                    dbFunctionServiceMock.Object, logger.Object);

            // Act
            var results = await service.AddOrUpdateBorrowerAsset(tenant: fakeTenant, fakeUserId, fakeModelReceived);

            // Assert
            Assert.Equal(AssetsService.AssetCategoryNotAllowed, results);
        }

        [Fact]
        public async Task TestAddOrUpdateBorrowerAssetWhenLoanApplicationNotFound()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var logger = new Mock<ILogger<AssetsService>>();

            using LoanApplicationContext applicationContext = new LoanApplicationContext(base.LoanApplicationContextOptions);
            await applicationContext.Database.EnsureCreatedAsync();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            int fakeUserId = 1;

            Borrower fakeBorrower = new Borrower()
            {
                TenantId = fakeTenantId
            };

            TenantModel fakeTenant = new TenantModel()
            {
                Id = fakeTenantId
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

            BorrowerAsset fakeBorrowerAsset = new BorrowerAsset()
            {
                TenantId = fakeTenantId,
                AccountNumber = "123xyz",
                AccountTitle = "Fake Account Title",
                AssetTypeId = 1
            };
            applicationContext.Add<BorrowerAsset>(fakeBorrowerAsset);
            await applicationContext.SaveChangesAsync();

            AssetBorrowerBinder assetBorrowerBinder = new AssetBorrowerBinder()
            {
                BorrowerId = fakeLoanApplication.Borrowers.First().Id,
                TenantId = fakeTenantId,
                BorrowerAssetId = fakeBorrowerAsset.Id
            };

            AddOrUpdateBorrowerAssetModel fakeModelReceived = new AddOrUpdateBorrowerAssetModel()
            {
                AssetCategoryId = (int)BorrowerAssetCategory.BankAccount
            };

            // Act
            var service = new AssetsService(base.GetUnitOfWorkInstance<LoanApplicationContext>(applicationContext), serviceProviderMock.Object, dbFunctionServiceMock.Object, logger.Object);

            var results = await service.AddOrUpdateBorrowerAsset(fakeTenant, userId: fakeUserId + 1, fakeModelReceived);
            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.Equal(AssetsService.LoanApplicationNotFound, results);
        }

        [Fact]
        public async Task TestAddOrUpdateBorrowerAssetBorrowerNotFound()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var logger = new Mock<ILogger<AssetsService>>();

            using LoanApplicationContext applicationContext = new LoanApplicationContext(base.LoanApplicationContextOptions);
            await applicationContext.Database.EnsureCreatedAsync();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            int fakeUserId = 1;

            Borrower fakeBorrower = new Borrower()
            {
                TenantId = fakeTenantId
            };

            TenantModel fakeTenant = new TenantModel()
            {
                Id = fakeTenantId
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

            BorrowerAsset fakeBorrowerAsset = new BorrowerAsset()
            {
                TenantId = fakeTenantId,
                AccountNumber = "123xyz",
                AccountTitle = "Fake Account Title",
                AssetTypeId = 1
            };
            applicationContext.Add<BorrowerAsset>(fakeBorrowerAsset);
            await applicationContext.SaveChangesAsync();

            AssetBorrowerBinder assetBorrowerBinder = new AssetBorrowerBinder()
            {
                BorrowerId = fakeLoanApplication.Borrowers.First().Id,
                TenantId = fakeTenantId,
                BorrowerAssetId = fakeBorrowerAsset.Id
            };

            AddOrUpdateBorrowerAssetModel fakeModelReceived = new AddOrUpdateBorrowerAssetModel()
            {
                AssetCategoryId = (int)BorrowerAssetCategory.BankAccount,
                BorrowerId = 2,
                LoanApplicationId = fakeLoanApplication.Id
            };

            // Act
            var service = new AssetsService(base.GetUnitOfWorkInstance<LoanApplicationContext>(applicationContext), serviceProviderMock.Object, dbFunctionServiceMock.Object, logger.Object);

            var results = await service.AddOrUpdateBorrowerAsset(fakeTenant, userId: fakeUserId, fakeModelReceived);
            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.Equal(AssetsService.BorrowerDetailNotFound, results);
        }

        [Fact]
        public async Task TestAddOrUpdateAssetTypeNotFound()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var logger = new Mock<ILogger<AssetsService>>();

            List<AssetType> fakeAssetTypeList = new List<AssetType>();
            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetTypeList.AsQueryable);

            using LoanApplicationContext applicationContext = new LoanApplicationContext(base.LoanApplicationContextOptions);
            await applicationContext.Database.EnsureCreatedAsync();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            int fakeUserId = 1;

            Borrower fakeBorrower = new Borrower()
            {
                TenantId = fakeTenantId
            };

            TenantModel fakeTenant = new TenantModel()
            {
                Id = fakeTenantId
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

            BorrowerAsset fakeBorrowerAsset = new BorrowerAsset()
            {
                TenantId = fakeTenantId,
                AccountNumber = "123xyz",
                AccountTitle = "Fake Account Title",
                AssetTypeId = 1
            };
            applicationContext.Add<BorrowerAsset>(fakeBorrowerAsset);
            await applicationContext.SaveChangesAsync();

            AssetBorrowerBinder assetBorrowerBinder = new AssetBorrowerBinder()
            {
                BorrowerId = fakeLoanApplication.Borrowers.First().Id,
                TenantId = fakeTenantId,
                BorrowerAssetId = fakeBorrowerAsset.Id
            };

            AddOrUpdateBorrowerAssetModel fakeModelReceived = new AddOrUpdateBorrowerAssetModel()
            {
                AssetCategoryId = (int)BorrowerAssetCategory.BankAccount,
                BorrowerId = 1,
                LoanApplicationId = fakeLoanApplication.Id
            };

            // Act
            var service = new AssetsService(base.GetUnitOfWorkInstance<LoanApplicationContext>(applicationContext), serviceProviderMock.Object, dbFunctionServiceMock.Object, logger.Object);

            var results = await service.AddOrUpdateBorrowerAsset(fakeTenant, userId: fakeUserId, fakeModelReceived);
            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.Equal(AssetsService.AssetTypeNotFound, results);
        }

        [Fact]
        public async Task TestAddOrUpdateAssetCategoryNotFound()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var logger = new Mock<ILogger<AssetsService>>();


            using LoanApplicationContext applicationContext = new LoanApplicationContext(base.LoanApplicationContextOptions);
            await applicationContext.Database.EnsureCreatedAsync();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            int fakeUserId = 1;
            int fakeAssetTypeId = 1;

            List<AssetType> fakeAssetTypeList = new List<AssetType>()
            {
                new AssetType()
                {
                    Id = fakeAssetTypeId
                }
            };
            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetTypeList.AsQueryable);

            List<AssetCategory> fakeAssetCategories = new EditableList<AssetCategory>();
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>()))
                .Returns(fakeAssetCategories.AsQueryable);

            Borrower fakeBorrower = new Borrower()
            {
                TenantId = fakeTenantId
            };

            TenantModel fakeTenant = new TenantModel()
            {
                Id = fakeTenantId
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

            BorrowerAsset fakeBorrowerAsset = new BorrowerAsset()
            {
                TenantId = fakeTenantId,
                AccountNumber = "123xyz",
                AccountTitle = "Fake Account Title",
                AssetTypeId = 1
            };
            applicationContext.Add<BorrowerAsset>(fakeBorrowerAsset);
            await applicationContext.SaveChangesAsync();

            AssetBorrowerBinder assetBorrowerBinder = new AssetBorrowerBinder()
            {
                BorrowerId = fakeLoanApplication.Borrowers.First().Id,
                TenantId = fakeTenantId,
                BorrowerAssetId = fakeBorrowerAsset.Id
            };

            AddOrUpdateBorrowerAssetModel fakeModelReceived = new AddOrUpdateBorrowerAssetModel()
            {
                AssetCategoryId = (int)BorrowerAssetCategory.BankAccount,
                BorrowerId = 1,
                LoanApplicationId = fakeLoanApplication.Id,
                AssetTypeId = fakeAssetTypeId
            };

            // Act
            var service = new AssetsService(base.GetUnitOfWorkInstance<LoanApplicationContext>(applicationContext), serviceProviderMock.Object, dbFunctionServiceMock.Object, logger.Object);

            var results = await service.AddOrUpdateBorrowerAsset(fakeTenant, userId: fakeUserId, fakeModelReceived);
            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.Equal(AssetsService.AssetCategoryNotFound, results);
        }

        [Fact]
        public async Task TestAddOrUpdateAssetWhenAssetDoesNotExist()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var logger = new Mock<ILogger<AssetsService>>();


            using LoanApplicationContext applicationContext = new LoanApplicationContext(base.LoanApplicationContextOptions);
            await applicationContext.Database.EnsureCreatedAsync();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            int fakeUserId = 1;
            int fakeAssetTypeId = 1;
            int fakeCategoryId = 1;

            List<AssetType> fakeAssetTypeList = new List<AssetType>()
            {
                new AssetType()
                {
                    Id = fakeAssetTypeId,
                    AssetCategoryId = fakeCategoryId
                }
            };
            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetTypeList.AsQueryable);

            List<AssetCategory> fakeAssetCategories = new EditableList<AssetCategory>()
            {
                new AssetCategory()
                {
                    Id = fakeAssetTypeId
                }
            };
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>()))
                .Returns(fakeAssetCategories.AsQueryable);

            Borrower fakeBorrower = new Borrower()
            {
                TenantId = fakeTenantId
            };

            TenantModel fakeTenant = new TenantModel()
            {
                Id = fakeTenantId
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

            //BorrowerAsset fakeBorrowerAsset = new BorrowerAsset()
            //{
            //    TenantId = fakeTenantId,
            //    AccountNumber = "123xyz",
            //    AccountTitle = "Fake Account Title",
            //    AssetTypeId = 1
            //};
            //applicationContext.Add<BorrowerAsset>(fakeBorrowerAsset);
            //await applicationContext.SaveChangesAsync();

            //AssetBorrowerBinder assetBorrowerBinder = new AssetBorrowerBinder()
            //{
            //    BorrowerId = fakeLoanApplication.Borrowers.First().Id,
            //    TenantId = fakeTenantId,
            //    BorrowerAssetId = fakeBorrowerAsset.Id
            //};

            AddOrUpdateBorrowerAssetModel fakeModelReceived = new AddOrUpdateBorrowerAssetModel()
            {
                AssetCategoryId = (int)BorrowerAssetCategory.BankAccount,
                BorrowerId = fakeLoanApplication.Borrowers.First().Id,
                LoanApplicationId = fakeLoanApplication.Id,
                AccountNumber = "abc123",
                InstitutionName = "Fake Institute",
                AssetTypeId = fakeAssetTypeId,
                AssetValue = 12345,
                State = "Some State",
                BorrowerAssetId = 0
            };

            // Act
            var service = new AssetsService(base.GetUnitOfWorkInstance<LoanApplicationContext>(applicationContext), serviceProviderMock.Object, dbFunctionServiceMock.Object, logger.Object);

            var results = await service.AddOrUpdateBorrowerAsset(fakeTenant, userId: fakeUserId, fakeModelReceived);
            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.Equal(1, results);
        }

        [Fact]
        public async Task TestAddOrUpdateAssetWhenAssetExist()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var logger = new Mock<ILogger<AssetsService>>();


            using LoanApplicationContext applicationContext = new LoanApplicationContext(base.LoanApplicationContextOptions);
            await applicationContext.Database.EnsureCreatedAsync();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            int fakeUserId = 1;
            int fakeAssetTypeId = 1;
            int fakeCategoryId = 1;

            List<AssetType> fakeAssetTypeList = new List<AssetType>()
            {
                new AssetType()
                {
                    Id = fakeAssetTypeId,
                    AssetCategoryId = fakeCategoryId
                }
            };
            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetTypeList.AsQueryable);

            List<AssetCategory> fakeAssetCategories = new EditableList<AssetCategory>()
            {
                new AssetCategory()
                {
                    Id = fakeAssetTypeId
                }
            };
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>()))
                .Returns(fakeAssetCategories.AsQueryable);

            Borrower fakeBorrower = new Borrower()
            {
                TenantId = fakeTenantId
            };

            TenantModel fakeTenant = new TenantModel()
            {
                Id = fakeTenantId
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

            BorrowerAsset fakeBorrowerAsset = new BorrowerAsset()
            {
                TenantId = fakeTenantId,
                AccountNumber = "123xyz",
                AccountTitle = "Fake Account Title",
                AssetTypeId = 1
            };
            applicationContext.Add<BorrowerAsset>(fakeBorrowerAsset);
            await applicationContext.SaveChangesAsync();

            AssetBorrowerBinder assetBorrowerBinder = new AssetBorrowerBinder()
            {
                BorrowerId = fakeLoanApplication.Borrowers.First().Id,
                TenantId = fakeTenantId,
                BorrowerAssetId = fakeBorrowerAsset.Id
            };
            applicationContext.Add<AssetBorrowerBinder>(assetBorrowerBinder);
            await applicationContext.SaveChangesAsync();

            AddOrUpdateBorrowerAssetModel fakeModelReceived = new AddOrUpdateBorrowerAssetModel()
            {
                AssetCategoryId = (int)BorrowerAssetCategory.BankAccount,
                BorrowerId = fakeLoanApplication.Borrowers.First().Id,
                LoanApplicationId = fakeLoanApplication.Id,
                AccountNumber = "abc123",
                InstitutionName = "Fake Institute",
                AssetTypeId = fakeAssetTypeId,
                AssetValue = 12345,
                State = "Some State",
                BorrowerAssetId = fakeBorrowerAsset.Id
            };

            // Act
            var service = new AssetsService(base.GetUnitOfWorkInstance<LoanApplicationContext>(applicationContext), serviceProviderMock.Object, dbFunctionServiceMock.Object, logger.Object);

            var results = await service.AddOrUpdateBorrowerAsset(fakeTenant, userId: fakeUserId, fakeModelReceived);
            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.Equal(1, results);
        }
        #endregion

        #region GetLoanApplicationBorrowersAssets Unit Tests
        [Fact]
        public async Task TesGetLoanApplicationBorrowersAssetsWhenLoanApplicationNotFound()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var logger = new Mock<ILogger<AssetsService>>();

            using LoanApplicationContext applicationContext = new LoanApplicationContext(base.LoanApplicationContextOptions);
            await applicationContext.Database.EnsureCreatedAsync();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            int fakeUserId = 1;

            Borrower fakeBorrower = new Borrower()
            {
                TenantId = fakeTenantId
            };

            TenantModel fakeTenant = new TenantModel()
            {
                Id = fakeTenantId
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

            BorrowerAsset fakeBorrowerAsset = new BorrowerAsset()
            {
                TenantId = fakeTenantId,
                AccountNumber = "123xyz",
                AccountTitle = "Fake Account Title",
                AssetTypeId = 1
            };
            applicationContext.Add<BorrowerAsset>(fakeBorrowerAsset);
            await applicationContext.SaveChangesAsync();

            AssetBorrowerBinder assetBorrowerBinder = new AssetBorrowerBinder()
            {
                BorrowerId = fakeLoanApplication.Borrowers.First().Id,
                TenantId = fakeTenantId,
                BorrowerAssetId = fakeBorrowerAsset.Id
            };

            AddOrUpdateBorrowerAssetModel fakeModelReceived = new AddOrUpdateBorrowerAssetModel()
            {
                AssetCategoryId = (int)BorrowerAssetCategory.BankAccount
            };

            // Act
            var service = new AssetsService(base.GetUnitOfWorkInstance<LoanApplicationContext>(applicationContext), serviceProviderMock.Object, dbFunctionServiceMock.Object, logger.Object);

            var results = await service.GetLoanApplicationBorrowersAssets(fakeTenant, userId: fakeUserId + 1, fakeLoanApplication.Id);
            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);
            Assert.NotNull(results.ErrorMessage);
        }

        [Fact]
        public async Task TesGetLoanApplicationBorrowersAssetsWhenLoanApplicationFound()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var logger = new Mock<ILogger<AssetsService>>();

            using LoanApplicationContext applicationContext = new LoanApplicationContext(base.LoanApplicationContextOptions);
            await applicationContext.Database.EnsureCreatedAsync();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            int fakeUserId = 1;

            Borrower fakeBorrower = new Borrower()
            {
                TenantId = fakeTenantId,
                LoanContact_LoanContactId = new LoanContact()
                {
                    FirstName = "Fake First Name",
                    LastName = "Fake Last Name"
                }
            };

            TenantModel fakeTenant = new TenantModel()
            {
                Id = fakeTenantId
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

            BorrowerAsset fakeBorrowerAsset = new BorrowerAsset()
            {
                TenantId = fakeTenantId,
                AccountNumber = "123xyz",
                AccountTitle = "Fake Account Title",
                AssetTypeId = 1
            };
            applicationContext.Add<BorrowerAsset>(fakeBorrowerAsset);
            await applicationContext.SaveChangesAsync();

            AssetBorrowerBinder assetBorrowerBinder = new AssetBorrowerBinder()
            {
                BorrowerId = fakeLoanApplication.Borrowers.First().Id,
                TenantId = fakeTenantId,
                BorrowerAssetId = fakeBorrowerAsset.Id
            };
            applicationContext.Add<AssetBorrowerBinder>(assetBorrowerBinder);

            AddOrUpdateBorrowerAssetModel fakeModelReceived = new AddOrUpdateBorrowerAssetModel()
            {
                AssetCategoryId = (int)BorrowerAssetCategory.BankAccount
            };

            AssetCategory fakeAssetCategory = new AssetCategory()
            {
                DisplayName = "Fake Category Display Name",
                Name = "Fake Category Name",
                Id = 1
            };
            List<AssetCategory> fakeAssetCategories = new List<AssetCategory>()
            {
                fakeAssetCategory
            };
            List<AssetType> fakeAssetTypes = new List<AssetType>()
            {
                new AssetType()
                {
                    Id = 1,
                    Name = "Fake Asset Name",
                    DisplayName = "Fake Display Name",
                    AssetCategoryId = fakeAssetCategory.Id
                }
            };
            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetTypes.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetCategories.AsQueryable);


            // Act
            var service = new AssetsService(base.GetUnitOfWorkInstance<LoanApplicationContext>(applicationContext), serviceProviderMock.Object, dbFunctionServiceMock.Object, logger.Object);

            var results = await service.GetLoanApplicationBorrowersAssets(fakeTenant, userId: fakeUserId, fakeLoanApplication.Id);
            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);
            Assert.Null(results.ErrorMessage);
        }
        #endregion

        #region GetBorrowerAssets Unit Tests
        [Fact]
        public async Task TeGetBorrowerAssetsWhenLoanApplicationNotFound()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var logger = new Mock<ILogger<AssetsService>>();

            using LoanApplicationContext applicationContext = new LoanApplicationContext(base.LoanApplicationContextOptions);
            await applicationContext.Database.EnsureCreatedAsync();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            int fakeUserId = 1;

            Borrower fakeBorrower = new Borrower()
            {
                TenantId = fakeTenantId
            };

            TenantModel fakeTenant = new TenantModel()
            {
                Id = fakeTenantId
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

            BorrowerAsset fakeBorrowerAsset = new BorrowerAsset()
            {
                TenantId = fakeTenantId,
                AccountNumber = "123xyz",
                AccountTitle = "Fake Account Title",
                AssetTypeId = 1
            };
            applicationContext.Add<BorrowerAsset>(fakeBorrowerAsset);
            await applicationContext.SaveChangesAsync();

            AssetBorrowerBinder assetBorrowerBinder = new AssetBorrowerBinder()
            {
                BorrowerId = fakeLoanApplication.Borrowers.First().Id,
                TenantId = fakeTenantId,
                BorrowerAssetId = fakeBorrowerAsset.Id
            };

            AddOrUpdateBorrowerAssetModel fakeModelReceived = new AddOrUpdateBorrowerAssetModel()
            {
                AssetCategoryId = (int)BorrowerAssetCategory.BankAccount
            };


            // Act
            var service = new AssetsService(base.GetUnitOfWorkInstance<LoanApplicationContext>(applicationContext), serviceProviderMock.Object, dbFunctionServiceMock.Object, logger.Object);

            var results = await service.GetBorrowerAssets(fakeTenant, userId: fakeUserId + 1, fakeLoanApplication.Id, fakeBorrower.Id);
            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);
            Assert.NotNull(results.ErrorMessage);
        }

        [Fact]
        public async Task TestGetBorrowerAssetsWhenBorrowerDetailNotFound()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var logger = new Mock<ILogger<AssetsService>>();

            using LoanApplicationContext applicationContext = new LoanApplicationContext(base.LoanApplicationContextOptions);
            await applicationContext.Database.EnsureCreatedAsync();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            int fakeUserId = 1;

            Borrower fakeBorrower = new Borrower()
            {
                TenantId = fakeTenantId
            };

            TenantModel fakeTenant = new TenantModel()
            {
                Id = fakeTenantId
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

            BorrowerAsset fakeBorrowerAsset = new BorrowerAsset()
            {
                TenantId = fakeTenantId,
                AccountNumber = "123xyz",
                AccountTitle = "Fake Account Title",
                AssetTypeId = 1
            };
            applicationContext.Add<BorrowerAsset>(fakeBorrowerAsset);
            await applicationContext.SaveChangesAsync();

            AssetBorrowerBinder assetBorrowerBinder = new AssetBorrowerBinder()
            {
                BorrowerId = fakeLoanApplication.Borrowers.First().Id,
                TenantId = fakeTenantId,
                BorrowerAssetId = fakeBorrowerAsset.Id
            };

            AddOrUpdateBorrowerAssetModel fakeModelReceived = new AddOrUpdateBorrowerAssetModel()
            {
                AssetCategoryId = (int)BorrowerAssetCategory.BankAccount
            };


            // Act
            var service = new AssetsService(base.GetUnitOfWorkInstance<LoanApplicationContext>(applicationContext), serviceProviderMock.Object, dbFunctionServiceMock.Object, logger.Object);

            var results = await service.GetBorrowerAssets(fakeTenant, userId: fakeUserId, fakeLoanApplication.Id, fakeBorrower.Id + 1);
            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);
            Assert.NotNull(results.ErrorMessage);
        }

        [Fact]
        public async Task TestGetBorrowerAssets()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var logger = new Mock<ILogger<AssetsService>>();


            using LoanApplicationContext applicationContext = new LoanApplicationContext(base.LoanApplicationContextOptions);
            await applicationContext.Database.EnsureCreatedAsync();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            int fakeUserId = 1;
            int fakeAssetTypeId = 1;
            int fakeCategoryId = 1;

            List<AssetType> fakeAssetTypeList = new List<AssetType>()
            {
                new AssetType()
                {
                    Id = fakeAssetTypeId,
                    AssetCategoryId = fakeCategoryId
                }
            };
            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetTypeList.AsQueryable);

            List<AssetCategory> fakeAssetCategories = new EditableList<AssetCategory>()
            {
                new AssetCategory()
                {
                    Id = fakeAssetTypeId
                }
            };
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>()))
                .Returns(fakeAssetCategories.AsQueryable);

            Borrower fakeBorrower = new Borrower()
            {
                TenantId = fakeTenantId,
                LoanContact_LoanContactId = new LoanContact()
                {
                    FirstName = "Fake First Name",
                    LastName = "Fake Last Name"
                }
            };

            TenantModel fakeTenant = new TenantModel()
            {
                Id = fakeTenantId
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

            BorrowerAsset fakeBorrowerAsset = new BorrowerAsset()
            {
                TenantId = fakeTenantId,
                AccountNumber = "123xyz",
                AccountTitle = "Fake Account Title",
                AssetTypeId = 1
            };
            applicationContext.Add<BorrowerAsset>(fakeBorrowerAsset);
            await applicationContext.SaveChangesAsync();

            AssetBorrowerBinder assetBorrowerBinder = new AssetBorrowerBinder()
            {
                BorrowerId = fakeLoanApplication.Borrowers.First().Id,
                TenantId = fakeTenantId,
                BorrowerAssetId = fakeBorrowerAsset.Id
            };
            applicationContext.Add<AssetBorrowerBinder>(assetBorrowerBinder);
            await applicationContext.SaveChangesAsync();

            AddOrUpdateBorrowerAssetModel fakeModelReceived = new AddOrUpdateBorrowerAssetModel()
            {
                AssetCategoryId = (int)BorrowerAssetCategory.BankAccount,
                BorrowerId = fakeLoanApplication.Borrowers.First().Id,
                LoanApplicationId = fakeLoanApplication.Id,
                AccountNumber = "abc123",
                InstitutionName = "Fake Institute",
                AssetTypeId = fakeAssetTypeId,
                AssetValue = 12345,
                State = "Some State",
                BorrowerAssetId = fakeBorrowerAsset.Id
            };

            // Act
            var service = new AssetsService(base.GetUnitOfWorkInstance<LoanApplicationContext>(applicationContext), serviceProviderMock.Object, dbFunctionServiceMock.Object, logger.Object);

            var results = await service.GetBorrowerAssets(fakeTenant, userId: fakeUserId, fakeLoanApplication.Id, fakeBorrower.Id);
            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);
        }
        #endregion

        #region GetBorrowerAssetDetail Unit Tests
        [Fact]
        public async Task TestGetBorrowerAssetDetailWhenLoanApplicationNotFound()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var logger = new Mock<ILogger<AssetsService>>();

            using LoanApplicationContext applicationContext = new LoanApplicationContext(base.LoanApplicationContextOptions);
            await applicationContext.Database.EnsureCreatedAsync();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            int fakeUserId = 1;

            Borrower fakeBorrower = new Borrower()
            {
                TenantId = fakeTenantId
            };

            TenantModel fakeTenant = new TenantModel()
            {
                Id = fakeTenantId
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

            BorrowerAsset fakeBorrowerAsset = new BorrowerAsset()
            {
                TenantId = fakeTenantId,
                AccountNumber = "123xyz",
                AccountTitle = "Fake Account Title",
                AssetTypeId = 1
            };
            applicationContext.Add<BorrowerAsset>(fakeBorrowerAsset);
            await applicationContext.SaveChangesAsync();

            AssetBorrowerBinder assetBorrowerBinder = new AssetBorrowerBinder()
            {
                BorrowerId = fakeLoanApplication.Borrowers.First().Id,
                TenantId = fakeTenantId,
                BorrowerAssetId = fakeBorrowerAsset.Id
            };

            BorrowerAssetDetailGetModel fakeModelReceived = new BorrowerAssetDetailGetModel()
            {
                
            };

            // Act
            var service = new AssetsService(base.GetUnitOfWorkInstance<LoanApplicationContext>(applicationContext), serviceProviderMock.Object, dbFunctionServiceMock.Object, logger.Object);

            var results = await service.GetBorrowerAssetDetail(fakeTenant, userId: fakeUserId + 1, fakeModelReceived);
            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);
            Assert.Equal(service.ErrorMessages[AssetsService.LoanApplicationNotFound], results.ErrorMessage);
        }

        [Fact]
        public async Task TestGetBorrowerAssetDetailWhenBorrowerDetailNotFound()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var logger = new Mock<ILogger<AssetsService>>();

            using LoanApplicationContext applicationContext = new LoanApplicationContext(base.LoanApplicationContextOptions);
            await applicationContext.Database.EnsureCreatedAsync();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            int fakeUserId = 1;

            Borrower fakeBorrower = new Borrower()
            {
                TenantId = fakeTenantId
            };

            TenantModel fakeTenant = new TenantModel()
            {
                Id = fakeTenantId
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

            BorrowerAsset fakeBorrowerAsset = new BorrowerAsset()
            {
                TenantId = fakeTenantId,
                AccountNumber = "123xyz",
                AccountTitle = "Fake Account Title",
                AssetTypeId = 1
            };
            applicationContext.Add<BorrowerAsset>(fakeBorrowerAsset);
            await applicationContext.SaveChangesAsync();

            AssetBorrowerBinder assetBorrowerBinder = new AssetBorrowerBinder()
            {
                BorrowerId = fakeLoanApplication.Borrowers.First().Id,
                TenantId = fakeTenantId,
                BorrowerAssetId = fakeBorrowerAsset.Id
            };

            BorrowerAssetDetailGetModel fakeModelReceived = new BorrowerAssetDetailGetModel()
            {
                BorrowerId = fakeBorrower.Id + 1,
                LoanApplicationId = fakeLoanApplication.Id
            };


            // Act
            var service = new AssetsService(base.GetUnitOfWorkInstance<LoanApplicationContext>(applicationContext), serviceProviderMock.Object, dbFunctionServiceMock.Object, logger.Object);

            var results = await service.GetBorrowerAssetDetail(fakeTenant, userId: fakeUserId, fakeModelReceived);
            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);
            Assert.NotNull(results.ErrorMessage);
            Assert.Equal(service.ErrorMessages[AssetsService.BorrowerDetailNotFound], results.ErrorMessage);
        }

        [Fact]
        public async Task TestGetBorrowerAssetDetailWhenBorrowerAssetDetailNotFound()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var logger = new Mock<ILogger<AssetsService>>();

            using LoanApplicationContext applicationContext = new LoanApplicationContext(base.LoanApplicationContextOptions);
            await applicationContext.Database.EnsureCreatedAsync();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            int fakeUserId = 1;

            Borrower fakeBorrower = new Borrower()
            {
                TenantId = fakeTenantId
            };

            TenantModel fakeTenant = new TenantModel()
            {
                Id = fakeTenantId
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

            BorrowerAsset fakeBorrowerAsset = new BorrowerAsset()
            {
                TenantId = fakeTenantId,
                AccountNumber = "123xyz",
                AccountTitle = "Fake Account Title",
                AssetTypeId = 1
            };
            applicationContext.Add<BorrowerAsset>(fakeBorrowerAsset);
            await applicationContext.SaveChangesAsync();

            AssetBorrowerBinder assetBorrowerBinder = new AssetBorrowerBinder()
            {
                BorrowerId = fakeLoanApplication.Borrowers.First().Id,
                TenantId = fakeTenantId,
                BorrowerAssetId = fakeBorrowerAsset.Id
            };

            BorrowerAssetDetailGetModel fakeModelReceived = new BorrowerAssetDetailGetModel()
            {
                BorrowerId = fakeBorrower.Id,
                LoanApplicationId = fakeLoanApplication.Id,
                BorrowerAssetId = fakeBorrowerAsset.Id + 1
            };


            // Act
            var service = new AssetsService(base.GetUnitOfWorkInstance<LoanApplicationContext>(applicationContext), serviceProviderMock.Object, dbFunctionServiceMock.Object, logger.Object);

            var results = await service.GetBorrowerAssetDetail(fakeTenant, userId: fakeUserId, fakeModelReceived);
            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);
            Assert.NotNull(results.ErrorMessage);
            Assert.Equal(service.ErrorMessages[AssetsService.BorrowerAssetDetailNotFound], results.ErrorMessage);
        }

        [Fact]
        public async Task TestGetBorrowerAssetDetail()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var logger = new Mock<ILogger<AssetsService>>();

            using LoanApplicationContext applicationContext = new LoanApplicationContext(base.LoanApplicationContextOptions);
            await applicationContext.Database.EnsureCreatedAsync();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            int fakeUserId = 1;
            int fakeAssetTypeId = 1;
            int fakeCategoryId = 1;

            Borrower fakeBorrower = new Borrower()
            {
                TenantId = fakeTenantId,
                LoanContact_LoanContactId = new LoanContact()
                {
                    FirstName = "Fake First Name",
                    LastName = "Fake Last Name"
                }
            };

            TenantModel fakeTenant = new TenantModel()
            {
                Id = fakeTenantId
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

            BorrowerAsset fakeBorrowerAsset = new BorrowerAsset()
            {
                TenantId = fakeTenantId,
                AccountNumber = "123xyz",
                AccountTitle = "Fake Account Title",
                AssetTypeId = fakeAssetTypeId
            };
            applicationContext.Add<BorrowerAsset>(fakeBorrowerAsset);
            await applicationContext.SaveChangesAsync();

            AssetBorrowerBinder assetBorrowerBinder = new AssetBorrowerBinder()
            {
                BorrowerId = fakeLoanApplication.Borrowers.First().Id,
                TenantId = fakeTenantId,
                BorrowerAssetId = fakeBorrowerAsset.Id
            };
            applicationContext.Add<AssetBorrowerBinder>(assetBorrowerBinder);
            await applicationContext.SaveChangesAsync();

            BorrowerAssetDetailGetModel fakeModelReceived = new BorrowerAssetDetailGetModel()
            {
                BorrowerId = fakeBorrower.Id,
                LoanApplicationId = fakeLoanApplication.Id,
                BorrowerAssetId = fakeBorrowerAsset.Id
            };

            List<AssetType> fakeAssetTypeList = new List<AssetType>()
            {
                new AssetType()
                {
                    Id = fakeAssetTypeId,
                    AssetCategoryId = fakeCategoryId
                }
            };
            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetTypeList.AsQueryable);

            List<AssetCategory> fakeAssetCategories = new EditableList<AssetCategory>()
            {
                new AssetCategory()
                {
                    Id = fakeCategoryId
                }
            };
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>()))
                .Returns(fakeAssetCategories.AsQueryable);


            // Act
            var service = new AssetsService(base.GetUnitOfWorkInstance<LoanApplicationContext>(applicationContext), serviceProviderMock.Object, dbFunctionServiceMock.Object, logger.Object);

            var results = await service.GetBorrowerAssetDetail(fakeTenant, userId: fakeUserId, fakeModelReceived);
            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);
            Assert.Null(results.ErrorMessage);
            Assert.NotNull(results.BorrowerAssets);
            Assert.NotEmpty(results.BorrowerAssets);
        }
        #endregion
    }
}
