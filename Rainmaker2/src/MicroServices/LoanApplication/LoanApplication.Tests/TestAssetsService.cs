using System;
using System.Collections.Generic;
using System.Text;
using Castle.Components.DictionaryAdapter;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using LoanApplication.Model;
using LoanApplication.Service;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Collections.Concurrent;
using LoanApplication.Tests.Services;
using Microsoft.AspNetCore.Http;
using AssetCategory = LoanApplication.Model.AssetCategory;
using LoanApplication = LoanApplicationDb.Entity.Models.LoanApplication;
using OwnType = LoanApplicationDb.Entity.Models.OwnType;

namespace LoanApplication.Tests
{
    public class TestAssetsService : UnitTestBase
    {
        [Fact]
        public async Task TestGetFromLoanNonRealState_CalledNormally_Ok()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            Mock<ILogger<AssetsService>> IloggerEmploymentServiceMock = new Mock<ILogger<AssetsService>>();
            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenantId,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 1, // Primary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "PrimaryFirstName",
                                    MiddleName = "PrimaryMiddleName",
                                    LastName = "PrimaryLastName",
                                    EmailAddress = "primaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                        AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=1,
                                        BorrowerAssetId=1,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=1,
                                            AssetTypeId=1
                                        }
                                    }
                                }



                            },
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 2, // Secondary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "SecondaryFirstName",
                                    MiddleName = "SecondaryMiddleName",
                                    LastName = "SecondaryLastName",
                                    EmailAddress = "secondaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=2,
                                        BorrowerAssetId=2,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=2,
                                            AssetTypeId=2
                                        }
                                    }
                                }



                            },

                   },


              };

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

            List<LoanApplicationDb.Entity.Models.AssetType> fakeAssetType = new List<LoanApplicationDb.Entity.Models.AssetType>()
            {
                new AssetType()
                {
                    Id = 1,
                    Name = "xyz",
                    AssetCategoryId=1
                }
            };
            List<LoanApplicationDb.Entity.Models.AssetCategory> fakeAssetCategory = new List<LoanApplicationDb.Entity.Models.AssetCategory>()
            {
                new LoanApplicationDb.Entity.Models.AssetCategory()
                {
                    Id = 1,
                    Name = "xyz"
                }
            };
            int fakeBorrowerAssetId = 1;
            int fakeAssetTypeId = 1;
            int fakeBorrowerId = 1;
            int fakeLoanApplicationId = 1;
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new AssetsService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, dbFunctionServiceMock.Object, IloggerEmploymentServiceMock.Object);

            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetType.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetCategory.AsQueryable);


            var results = await service.GetFromLoanNonRealState(fakeTenant, fakeUserId, fakeBorrowerAssetId, fakeAssetTypeId, fakeBorrowerId, fakeLoanApplicationId);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);



        }
        [Fact]
        public async Task TestGetFromLoanRealState_CalledNormally_Ok()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            Mock<ILogger<AssetsService>> IloggerEmploymentServiceMock = new Mock<ILogger<AssetsService>>();
            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenantId,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 1, // Primary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "PrimaryFirstName",
                                    MiddleName = "PrimaryMiddleName",
                                    LastName = "PrimaryLastName",
                                    EmailAddress = "primaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                        AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=1,
                                        BorrowerAssetId=1,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=1,
                                            AssetTypeId=1
                                        }
                                    }
                                }



                            },
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 2, // Secondary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "SecondaryFirstName",
                                    MiddleName = "SecondaryMiddleName",
                                    LastName = "SecondaryLastName",
                                    EmailAddress = "secondaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=2,
                                        BorrowerAssetId=2,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=2,
                                            AssetTypeId=2
                                        }
                                    }
                                }



                            },

                   },


              };

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

            List<LoanApplicationDb.Entity.Models.AssetType> fakeAssetType = new List<LoanApplicationDb.Entity.Models.AssetType>()
            {
                new AssetType()
                {
                    Id = 1,
                    Name = "xyz",
                    AssetCategoryId=1
                }
            };
            List<LoanApplicationDb.Entity.Models.AssetCategory> fakeAssetCategory = new List<LoanApplicationDb.Entity.Models.AssetCategory>()
            {
                new LoanApplicationDb.Entity.Models.AssetCategory()
                {
                    Id = 1,
                    Name = "xyz"
                }
            };
            int fakeBorrowerAssetId = 1;
            int fakeAssetTypeId = 1;
            int fakeBorrowerId = 1;
            int fakeLoanApplicationId = 1;
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new AssetsService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, dbFunctionServiceMock.Object, IloggerEmploymentServiceMock.Object);

            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetType.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetCategory.AsQueryable);


            var results = await service.GetFromLoanRealState(fakeTenant, fakeUserId, fakeBorrowerAssetId, fakeAssetTypeId, fakeBorrowerId, fakeLoanApplicationId);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);



        }
        [Fact]
        public async Task TestGetProceedsfromloan_CalledNormally_Ok()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            Mock<ILogger<AssetsService>> IloggerEmploymentServiceMock = new Mock<ILogger<AssetsService>>();
            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenantId,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 1, // Primary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "PrimaryFirstName",
                                    MiddleName = "PrimaryMiddleName",
                                    LastName = "PrimaryLastName",
                                    EmailAddress = "primaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                        AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=1,
                                        BorrowerAssetId=1,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=1,
                                            AssetTypeId=1,
                                            CollateralAssetType= new CollateralAssetType()
                                            {
                                                Id=1

                                            }
                                        }
                                    }
                                }



                            },
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 2, // Secondary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "SecondaryFirstName",
                                    MiddleName = "SecondaryMiddleName",
                                    LastName = "SecondaryLastName",
                                    EmailAddress = "secondaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=2,
                                        BorrowerAssetId=2,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=2,
                                            AssetTypeId=2,
                                            CollateralAssetType= new CollateralAssetType()
                                            {
                                                Id=2

                                            }
                                        }
                                    }
                                }



                            },

                   },


              };

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

            List<LoanApplicationDb.Entity.Models.AssetType> fakeAssetType = new List<LoanApplicationDb.Entity.Models.AssetType>()
            {
                new AssetType()
                {
                    Id = 1,
                    Name = "xyz",
                    AssetCategoryId=1
                }
            };
            List<LoanApplicationDb.Entity.Models.AssetCategory> fakeAssetCategory = new List<LoanApplicationDb.Entity.Models.AssetCategory>()
            {
                new LoanApplicationDb.Entity.Models.AssetCategory()
                {
                    Id = 1,
                    Name = "xyz"
                }
            };
            List<LoanApplicationDb.Entity.Models.CollateralAssetType> fakeCollateralAssetType = new List<LoanApplicationDb.Entity.Models.CollateralAssetType>()
            {
                new LoanApplicationDb.Entity.Models.CollateralAssetType()
                {
                    Id = 1,
                    Name = "xyz"
                }
            };
            //

            int fakeBorrowerAssetId = 1;
            int fakeAssetTypeId = 1;
            int fakeBorrowerId = 1;
            int fakeLoanApplicationId = 1;
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new AssetsService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, dbFunctionServiceMock.Object, IloggerEmploymentServiceMock.Object);

            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetType.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetCategory.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfCollateralAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeCollateralAssetType.AsQueryable);


            var results = await service.GetProceedsfromloan(fakeTenant, fakeUserId, fakeBorrowerAssetId, fakeAssetTypeId, fakeBorrowerId, fakeLoanApplicationId);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);



        }

        [Fact]
        public async Task TestAddOrUpdateAssestsNonRealState_BorrowerAssestIDNotExist_UpdatedOK()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            Mock<ILogger<AssetsService>> IloggerEmploymentServiceMock = new Mock<ILogger<AssetsService>>();
            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenantId,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 1, // Primary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "PrimaryFirstName",
                                    MiddleName = "PrimaryMiddleName",
                                    LastName = "PrimaryLastName",
                                    EmailAddress = "primaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                        AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=1,
                                        BorrowerAssetId=1,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=1,
                                            AssetTypeId=1
                                        }
                                    }
                                }



                            },
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 2, // Secondary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "SecondaryFirstName",
                                    MiddleName = "SecondaryMiddleName",
                                    LastName = "SecondaryLastName",
                                    EmailAddress = "secondaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=2,
                                        BorrowerAssetId=2,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=2,
                                            AssetTypeId=2
                                        }
                                    }
                                }



                            },

                   },


              };

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

            List<LoanApplicationDb.Entity.Models.AssetType> fakeAssetType = new List<LoanApplicationDb.Entity.Models.AssetType>()
            {
                new AssetType()
                {
                    Id = 1,
                    Name = "xyz",
                    AssetCategoryId=1
                }
            };
            List<LoanApplicationDb.Entity.Models.AssetCategory> fakeAssetCategory = new List<LoanApplicationDb.Entity.Models.AssetCategory>()
            {
                new LoanApplicationDb.Entity.Models.AssetCategory()
                {
                    Id = 6,
                    Name = "xyz"
                }
            };

            AddOrUpdateAssetModelNonRealState fakemodel = new AddOrUpdateAssetModelNonRealState()
            {
                LoanApplicationId = 1,
                BorrowerAssetId = 1,
                AssetTypeId = 1,
                AssetCategoryId = 6,
                Description = "fake desc",
                AssetValue = 1,
                BorrowerId = 1

            };
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new AssetsService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, dbFunctionServiceMock.Object, IloggerEmploymentServiceMock.Object);

            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetType.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetCategory.AsQueryable);


            var results = await service.AddOrUpdateAssestsNonRealState(fakeTenant, fakeUserId, fakemodel);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            //Assert.NotNull(results);
            Assert.Equal(1, results);



        }
        [Fact]
        public async Task TestAddOrUpdateAssestsNonRealState_BorrowerAssetsIDExist_AddedOK()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            Mock<ILogger<AssetsService>> IloggerEmploymentServiceMock = new Mock<ILogger<AssetsService>>();
            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenantId,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 1, // Primary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "PrimaryFirstName",
                                    MiddleName = "PrimaryMiddleName",
                                    LastName = "PrimaryLastName",
                                    EmailAddress = "primaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                        AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=1,
                                        BorrowerAssetId=1,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=1,
                                            AssetTypeId=1
                                        }
                                    }
                                }



                            },
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 2, // Secondary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "SecondaryFirstName",
                                    MiddleName = "SecondaryMiddleName",
                                    LastName = "SecondaryLastName",
                                    EmailAddress = "secondaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=2,
                                        BorrowerAssetId=2,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=2,
                                            AssetTypeId=2
                                        }
                                    }
                                }



                            },

                   },


              };

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

            List<LoanApplicationDb.Entity.Models.AssetType> fakeAssetType = new List<LoanApplicationDb.Entity.Models.AssetType>()
            {
                new AssetType()
                {
                    Id = 1,
                    Name = "xyz",
                    AssetCategoryId=1
                }
            };
            List<LoanApplicationDb.Entity.Models.AssetCategory> fakeAssetCategory = new List<LoanApplicationDb.Entity.Models.AssetCategory>()
            {
                new LoanApplicationDb.Entity.Models.AssetCategory()
                {
                    Id = 6,
                    Name = "xyz"
                }
            };

            AddOrUpdateAssetModelNonRealState fakemodel = new AddOrUpdateAssetModelNonRealState()
            {
                LoanApplicationId = 1,

                AssetTypeId = 1,
                AssetCategoryId = 6,
                Description = "fake desc",
                AssetValue = 1,
                BorrowerId = 1

            };
            int fakeResult = 3;
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new AssetsService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, dbFunctionServiceMock.Object, IloggerEmploymentServiceMock.Object);

            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetType.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetCategory.AsQueryable);


            var results = await service.AddOrUpdateAssestsNonRealState(fakeTenant, fakeUserId, fakemodel);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            //Assert.NotNull(results);
            Assert.Equal(fakeResult, results);



        }
        [Fact]
        public async Task TestAddOrUpdateAssestsRealState_BorrowerAssestIDNotExist_AddedOK()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            Mock<ILogger<AssetsService>> IloggerEmploymentServiceMock = new Mock<ILogger<AssetsService>>();
            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenantId,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 1, // Primary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "PrimaryFirstName",
                                    MiddleName = "PrimaryMiddleName",
                                    LastName = "PrimaryLastName",
                                    EmailAddress = "primaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                        AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=1,
                                        BorrowerAssetId=1,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=1,
                                            AssetTypeId=1
                                        }
                                    }
                                }



                            },
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 2, // Secondary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "SecondaryFirstName",
                                    MiddleName = "SecondaryMiddleName",
                                    LastName = "SecondaryLastName",
                                    EmailAddress = "secondaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=2,
                                        BorrowerAssetId=2,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=2,
                                            AssetTypeId=2
                                        }
                                    }
                                }



                            },

                   },


              };

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

            List<LoanApplicationDb.Entity.Models.AssetType> fakeAssetType = new List<LoanApplicationDb.Entity.Models.AssetType>()
            {
                new AssetType()
                {
                    Id = 1,
                    Name = "xyz",
                    AssetCategoryId=1
                }
            };
            List<LoanApplicationDb.Entity.Models.AssetCategory> fakeAssetCategory = new List<LoanApplicationDb.Entity.Models.AssetCategory>()
            {
                new LoanApplicationDb.Entity.Models.AssetCategory()
                {
                    Id = 6,
                    Name = "xyz"
                }
            };

            AddOrUpdateAssetModelRealState fakemodel = new AddOrUpdateAssetModelRealState()
            {
                LoanApplicationId = 1,

                AssetTypeId = 1,
                AssetCategoryId = 6,
                Description = "fake desc",
                AssetValue = 1,
                BorrowerId = 1

            };
            int fakeResult = 3;
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new AssetsService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, dbFunctionServiceMock.Object, IloggerEmploymentServiceMock.Object);

            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetType.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetCategory.AsQueryable);


            var results = await service.AddOrUpdateAssestsRealState(fakeTenant, fakeUserId, fakemodel);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            //Assert.NotNull(results);
            Assert.Equal(fakeResult, results);



        }

        [Fact]
        public async Task TestAddOrUpdateAssestsRealState_BorrowerAssestIDExist_AddedOK()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            Mock<ILogger<AssetsService>> IloggerEmploymentServiceMock = new Mock<ILogger<AssetsService>>();
            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenantId,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 1, // Primary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "PrimaryFirstName",
                                    MiddleName = "PrimaryMiddleName",
                                    LastName = "PrimaryLastName",
                                    EmailAddress = "primaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                        AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=1,
                                        BorrowerAssetId=1,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=1,
                                            AssetTypeId=1
                                        }
                                    }
                                }



                            },
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 2, // Secondary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "SecondaryFirstName",
                                    MiddleName = "SecondaryMiddleName",
                                    LastName = "SecondaryLastName",
                                    EmailAddress = "secondaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=2,
                                        BorrowerAssetId=2,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=2,
                                            AssetTypeId=2
                                        }
                                    }
                                }



                            },

                   },


              };

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

            List<LoanApplicationDb.Entity.Models.AssetType> fakeAssetType = new List<LoanApplicationDb.Entity.Models.AssetType>()
            {
                new AssetType()
                {
                    Id = 1,
                    Name = "xyz",
                    AssetCategoryId=1
                }
            };
            List<LoanApplicationDb.Entity.Models.AssetCategory> fakeAssetCategory = new List<LoanApplicationDb.Entity.Models.AssetCategory>()
            {
                new LoanApplicationDb.Entity.Models.AssetCategory()
                {
                    Id = 6,
                    Name = "xyz"
                }
            };

            AddOrUpdateAssetModelRealState fakemodel = new AddOrUpdateAssetModelRealState()
            {
                LoanApplicationId = 1,
                BorrowerAssetId = 1,
                AssetTypeId = 1,
                AssetCategoryId = 6,
                Description = "fake desc",
                AssetValue = 1,
                BorrowerId = 1

            };
            int fakeResult = 3;
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new AssetsService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, dbFunctionServiceMock.Object, IloggerEmploymentServiceMock.Object);

            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetType.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetCategory.AsQueryable);


            var results = await service.AddOrUpdateAssestsRealState(fakeTenant, fakeUserId, fakemodel);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            //Assert.NotNull(results);
            Assert.Equal(1, results);



        }
        [Fact]
        public async Task TestAddOrUpdateProceedsfromloan_BorrowerAssestIDNotExist_AddedOK()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            Mock<ILogger<AssetsService>> IloggerEmploymentServiceMock = new Mock<ILogger<AssetsService>>();
            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenantId,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 1, // Primary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "PrimaryFirstName",
                                    MiddleName = "PrimaryMiddleName",
                                    LastName = "PrimaryLastName",
                                    EmailAddress = "primaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                        AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=1,
                                        BorrowerAssetId=1,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=1,
                                            AssetTypeId=1
                                        }
                                    }
                                }



                            },
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 2, // Secondary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "SecondaryFirstName",
                                    MiddleName = "SecondaryMiddleName",
                                    LastName = "SecondaryLastName",
                                    EmailAddress = "secondaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=2,
                                        BorrowerAssetId=2,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=2,
                                            AssetTypeId=2
                                        }
                                    }
                                }



                            },

                   },


              };

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

            List<LoanApplicationDb.Entity.Models.AssetType> fakeAssetType = new List<LoanApplicationDb.Entity.Models.AssetType>()
            {
                new AssetType()
                {
                    Id = 1,
                    Name = "xyz",
                    AssetCategoryId=1
                }
            };
            List<LoanApplicationDb.Entity.Models.AssetCategory> fakeAssetCategory = new List<LoanApplicationDb.Entity.Models.AssetCategory>()
            {
                new LoanApplicationDb.Entity.Models.AssetCategory()
                {
                    Id = 6,
                    Name = "xyz"
                }
            };

            ProceedFromLoanModel fakemodel = new ProceedFromLoanModel()
            {
                LoanApplicationId = 1,

                AssetTypeId = 1,
                AssetCategoryId = 6,
                AssetValue = 1,
                BorrowerId = 1,
                ColletralAssetTypeId = 1,
                SecuredByColletral = true


            };
            int fakeResult = 3;
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new AssetsService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, dbFunctionServiceMock.Object, IloggerEmploymentServiceMock.Object);

            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetType.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetCategory.AsQueryable);


            var results = await service.AddOrUpdateProceedsfromloan(fakeTenant, fakeUserId, fakemodel);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            //Assert.NotNull(results);
            Assert.Equal(fakeResult, results);



        }
        [Fact]
        public async Task TestAddOrUpdateProceedsfromloan_BorrowerAssestIDExist_AddedOK()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            Mock<ILogger<AssetsService>> IloggerEmploymentServiceMock = new Mock<ILogger<AssetsService>>();
            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenantId,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 1, // Primary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "PrimaryFirstName",
                                    MiddleName = "PrimaryMiddleName",
                                    LastName = "PrimaryLastName",
                                    EmailAddress = "primaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                        AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=1,
                                        BorrowerAssetId=1,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=1,
                                            AssetTypeId=1
                                        }
                                    }
                                }



                            },
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 2, // Secondary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "SecondaryFirstName",
                                    MiddleName = "SecondaryMiddleName",
                                    LastName = "SecondaryLastName",
                                    EmailAddress = "secondaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=2,
                                        BorrowerAssetId=2,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=2,
                                            AssetTypeId=2
                                        }
                                    }
                                }



                            },

                   },


              };

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

            List<LoanApplicationDb.Entity.Models.AssetType> fakeAssetType = new List<LoanApplicationDb.Entity.Models.AssetType>()
            {
                new AssetType()
                {
                    Id = 1,
                    Name = "xyz",
                    AssetCategoryId=1
                }
            };
            List<LoanApplicationDb.Entity.Models.AssetCategory> fakeAssetCategory = new List<LoanApplicationDb.Entity.Models.AssetCategory>()
            {
                new LoanApplicationDb.Entity.Models.AssetCategory()
                {
                    Id = 6,
                    Name = "xyz"
                }
            };

            ProceedFromLoanModel fakemodel = new ProceedFromLoanModel()
            {
                LoanApplicationId = 1,

                AssetTypeId = 1,
                AssetCategoryId = 6,
                AssetValue = 1,
                BorrowerId = 1,
                ColletralAssetTypeId = 1,
                SecuredByColletral = true,
                BorrowerAssetId = 1


            };
            int fakeResult = 3;
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new AssetsService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, dbFunctionServiceMock.Object, IloggerEmploymentServiceMock.Object);

            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetType.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetCategory.AsQueryable);


            var results = await service.AddOrUpdateProceedsfromloan(fakeTenant, fakeUserId, fakemodel);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            //Assert.NotNull(results);
            Assert.Equal(1, results);



        }

        [Fact]
        public async Task TestAddOrUpdateProceedsfromloan_BorrowerAssestIDNotExistANDNotSecuredByColletral_AddedOK()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            Mock<ILogger<AssetsService>> IloggerEmploymentServiceMock = new Mock<ILogger<AssetsService>>();
            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenantId,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 1, // Primary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "PrimaryFirstName",
                                    MiddleName = "PrimaryMiddleName",
                                    LastName = "PrimaryLastName",
                                    EmailAddress = "primaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                        AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=1,
                                        BorrowerAssetId=1,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=1,
                                            AssetTypeId=1
                                        }
                                    }
                                }



                            },
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 2, // Secondary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "SecondaryFirstName",
                                    MiddleName = "SecondaryMiddleName",
                                    LastName = "SecondaryLastName",
                                    EmailAddress = "secondaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=2,
                                        BorrowerAssetId=2,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=2,
                                            AssetTypeId=2
                                        }
                                    }
                                }



                            },

                   },


              };

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

            List<LoanApplicationDb.Entity.Models.AssetType> fakeAssetType = new List<LoanApplicationDb.Entity.Models.AssetType>()
            {
                new AssetType()
                {
                    Id = 1,
                    Name = "xyz",
                    AssetCategoryId=1
                }
            };
            List<LoanApplicationDb.Entity.Models.AssetCategory> fakeAssetCategory = new List<LoanApplicationDb.Entity.Models.AssetCategory>()
            {
                new LoanApplicationDb.Entity.Models.AssetCategory()
                {
                    Id = 6,
                    Name = "xyz"
                }
            };

            ProceedFromLoanModel fakemodel = new ProceedFromLoanModel()
            {
                LoanApplicationId = 1,

                AssetTypeId = 1,
                AssetCategoryId = 6,
                AssetValue = 1,
                BorrowerId = 1,
                ColletralAssetTypeId = 1,
                SecuredByColletral = false


            };
            int fakeResult = 3;
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new AssetsService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, dbFunctionServiceMock.Object, IloggerEmploymentServiceMock.Object);

            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetType.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetCategory.AsQueryable);


            var results = await service.AddOrUpdateProceedsfromloan(fakeTenant, fakeUserId, fakemodel);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            //Assert.NotNull(results);
            Assert.Equal(fakeResult, results);



        }
        [Fact]
        public async Task TestAddOrUpdateProceedsfromloanOther_BorrowerAssestIDNotExist_AddedOK()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            Mock<ILogger<AssetsService>> IloggerEmploymentServiceMock = new Mock<ILogger<AssetsService>>();
            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenantId,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 1, // Primary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "PrimaryFirstName",
                                    MiddleName = "PrimaryMiddleName",
                                    LastName = "PrimaryLastName",
                                    EmailAddress = "primaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                        AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=1,
                                        BorrowerAssetId=1,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=1,
                                            AssetTypeId=1
                                        }
                                    }
                                }



                            },
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 2, // Secondary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "SecondaryFirstName",
                                    MiddleName = "SecondaryMiddleName",
                                    LastName = "SecondaryLastName",
                                    EmailAddress = "secondaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=2,
                                        BorrowerAssetId=2,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=2,
                                            AssetTypeId=2
                                        }
                                    }
                                }



                            },

                   },


              };

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

            List<LoanApplicationDb.Entity.Models.AssetType> fakeAssetType = new List<LoanApplicationDb.Entity.Models.AssetType>()
            {
                new AssetType()
                {
                    Id = 1,
                    Name = "xyz",
                    AssetCategoryId=1
                }
            };
            List<LoanApplicationDb.Entity.Models.AssetCategory> fakeAssetCategory = new List<LoanApplicationDb.Entity.Models.AssetCategory>()
            {
                new LoanApplicationDb.Entity.Models.AssetCategory()
                {
                    Id = 6,
                    Name = "xyz"
                }
            };

            ProceedFromLoanOtherModel fakemodel = new ProceedFromLoanOtherModel()
            {
                LoanApplicationId = 1,

                AssetTypeId = 1,
                AssetCategoryId = 6,
                AssetValue = 1,
                BorrowerId = 1,
                ColletralAssetTypeId = 4,
                CollateralAssetDescription = "fake desc"


            };
            int fakeResult = 3;
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new AssetsService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, dbFunctionServiceMock.Object, IloggerEmploymentServiceMock.Object);

            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetType.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetCategory.AsQueryable);


            var results = await service.AddOrUpdateProceedsfromloanOther(fakeTenant, fakeUserId, fakemodel);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            //Assert.NotNull(results);
            Assert.Equal(fakeResult, results);



        }
        [Fact]
        public async Task TestAddOrUpdateProceedsfromloanOther_BorrowerAssestIDExist_AddedOK()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            Mock<ILogger<AssetsService>> IloggerEmploymentServiceMock = new Mock<ILogger<AssetsService>>();
            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenantId,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 1, // Primary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "PrimaryFirstName",
                                    MiddleName = "PrimaryMiddleName",
                                    LastName = "PrimaryLastName",
                                    EmailAddress = "primaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                        AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=1,
                                        BorrowerAssetId=1,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=1,
                                            AssetTypeId=1
                                        }
                                    }
                                }



                            },
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 2, // Secondary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "SecondaryFirstName",
                                    MiddleName = "SecondaryMiddleName",
                                    LastName = "SecondaryLastName",
                                    EmailAddress = "secondaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=2,
                                        BorrowerAssetId=2,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=2,
                                            AssetTypeId=2
                                        }
                                    }
                                }



                            },

                   },


              };

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

            List<LoanApplicationDb.Entity.Models.AssetType> fakeAssetType = new List<LoanApplicationDb.Entity.Models.AssetType>()
            {
                new AssetType()
                {
                    Id = 1,
                    Name = "xyz",
                    AssetCategoryId=1
                }
            };
            List<LoanApplicationDb.Entity.Models.AssetCategory> fakeAssetCategory = new List<LoanApplicationDb.Entity.Models.AssetCategory>()
            {
                new LoanApplicationDb.Entity.Models.AssetCategory()
                {
                    Id = 6,
                    Name = "xyz"
                }
            };

            ProceedFromLoanOtherModel fakemodel = new ProceedFromLoanOtherModel()
            {
                LoanApplicationId = 1,
                BorrowerAssetId = 1,
                AssetTypeId = 1,
                AssetCategoryId = 6,
                AssetValue = 1,
                BorrowerId = 1,
                ColletralAssetTypeId = 4,
                CollateralAssetDescription = "fake desc"


            };

            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new AssetsService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, dbFunctionServiceMock.Object, IloggerEmploymentServiceMock.Object);

            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetType.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetCategory.AsQueryable);


            var results = await service.AddOrUpdateProceedsfromloanOther(fakeTenant, fakeUserId, fakemodel);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            //Assert.NotNull(results);
            Assert.Equal(1, results);



        }
        [Fact]
        public async Task TestGetCollateralAssetTypes_CalledNormally_Ok()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            Mock<ILogger<AssetsService>> IloggerEmploymentServiceMock = new Mock<ILogger<AssetsService>>();
            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenantId,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 1, // Primary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "PrimaryFirstName",
                                    MiddleName = "PrimaryMiddleName",
                                    LastName = "PrimaryLastName",
                                    EmailAddress = "primaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                        AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=1,
                                        BorrowerAssetId=1,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=1,
                                            AssetTypeId=1
                                        }
                                    }
                                }



                            },
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 2, // Secondary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "SecondaryFirstName",
                                    MiddleName = "SecondaryMiddleName",
                                    LastName = "SecondaryLastName",
                                    EmailAddress = "secondaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=2,
                                        BorrowerAssetId=2,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=2,
                                            AssetTypeId=2
                                        }
                                    }
                                }



                            },

                   },


              };

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

            List<LoanApplicationDb.Entity.Models.AssetType> fakeAssetType = new List<LoanApplicationDb.Entity.Models.AssetType>()
            {
                new AssetType()
                {
                    Id = 1,
                    Name = "xyz",
                    AssetCategoryId=1
                }
            };
            List<LoanApplicationDb.Entity.Models.AssetCategory> fakeAssetCategory = new List<LoanApplicationDb.Entity.Models.AssetCategory>()
            {
                new LoanApplicationDb.Entity.Models.AssetCategory()
                {
                    Id = 1,
                    Name = "xyz"
                }
            };
            List<LoanApplicationDb.Entity.Models.CollateralAssetType> fakeCollateralAssetType = new List<LoanApplicationDb.Entity.Models.CollateralAssetType>()
            {
                new LoanApplicationDb.Entity.Models.CollateralAssetType()
                {
                    Id = 1,
                    Name = "xyz"
                }
            };



            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new AssetsService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, dbFunctionServiceMock.Object, IloggerEmploymentServiceMock.Object);

            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetType.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetCategory.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfCollateralAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeCollateralAssetType.AsQueryable);


            var results = service.GetCollateralAssetTypes(fakeTenant);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);



        }
        [Fact]
        public async Task TestGetOtherAssetInfo_CalledNormally_Ok()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            Mock<ILogger<AssetsService>> IloggerAssertServiceMock = new Mock<ILogger<AssetsService>>();
            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenantId,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 1, // Primary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "PrimaryFirstName",
                                    MiddleName = "PrimaryMiddleName",
                                    LastName = "PrimaryLastName",
                                    EmailAddress = "primaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                        AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=1,
                                        BorrowerAssetId=1,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=1,
                                            AssetTypeId=1,
                                            TenantId=1
                                        }
                                    }
                                }



                            },
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 2, // Secondary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "SecondaryFirstName",
                                    MiddleName = "SecondaryMiddleName",
                                    LastName = "SecondaryLastName",
                                    EmailAddress = "secondaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=2,
                                        BorrowerAssetId=2,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=2,
                                            AssetTypeId=2,
                                             TenantId=1
                                        }
                                    }
                                }



                            },

                   },


              };

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

            List<LoanApplicationDb.Entity.Models.AssetType> fakeAssetType = new List<LoanApplicationDb.Entity.Models.AssetType>()
            {
                new AssetType()
                {
                    Id = 1,
                    Name = "xyz",
                    AssetCategoryId=1
                }
            };
            List<LoanApplicationDb.Entity.Models.AssetCategory> fakeAssetCategory = new List<LoanApplicationDb.Entity.Models.AssetCategory>()
            {
                new LoanApplicationDb.Entity.Models.AssetCategory()
                {
                    Id = 1,
                    Name = "xyz"
                }
            };
            List<LoanApplicationDb.Entity.Models.CollateralAssetType> fakeCollateralAssetType = new List<LoanApplicationDb.Entity.Models.CollateralAssetType>()
            {
                new LoanApplicationDb.Entity.Models.CollateralAssetType()
                {
                    Id = 1,
                    Name = "xyz"
                }
            };
            int fakeAssetId = 1;


            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new AssetsService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, dbFunctionServiceMock.Object, IloggerAssertServiceMock.Object);

            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetType.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetCategory.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfCollateralAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeCollateralAssetType.AsQueryable);


            var results = service.GetOtherAssetInfo(fakeTenant, fakeUserId, fakeAssetId);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);



        }

        [Fact]
        public async Task TestAddOrUpdateOtherAssetInfo_AssetIdExist_UpdatedOk()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            Mock<ILogger<AssetsService>> IloggerAssertServiceMock = new Mock<ILogger<AssetsService>>();

            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenantId,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 1, // Primary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "PrimaryFirstName",
                                    MiddleName = "PrimaryMiddleName",
                                    LastName = "PrimaryLastName",
                                    EmailAddress = "primaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                        AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=1,
                                        BorrowerAssetId=1,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=1,
                                            AssetTypeId=1,
                                            TenantId=1
                                        }
                                    }
                                }



                            },
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 2, // Secondary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "SecondaryFirstName",
                                    MiddleName = "SecondaryMiddleName",
                                    LastName = "SecondaryLastName",
                                    EmailAddress = "secondaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=2,
                                        BorrowerAssetId=2,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=2,
                                            AssetTypeId=2,
                                             TenantId=1
                                        }
                                    }
                                }



                            },

                   },


              };

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

            List<LoanApplicationDb.Entity.Models.AssetType> fakeAssetType = new List<LoanApplicationDb.Entity.Models.AssetType>()
            {
                new AssetType()
                {
                    Id = 1,
                    Name = "xyz",
                    AssetCategoryId=1
                }
            };
            List<LoanApplicationDb.Entity.Models.AssetCategory> fakeAssetCategory = new List<LoanApplicationDb.Entity.Models.AssetCategory>()
            {
                new LoanApplicationDb.Entity.Models.AssetCategory()
                {
                    Id = 1,
                    Name = "xyz"
                }
            };
            List<LoanApplicationDb.Entity.Models.CollateralAssetType> fakeCollateralAssetType = new List<LoanApplicationDb.Entity.Models.CollateralAssetType>()
            {
                new LoanApplicationDb.Entity.Models.CollateralAssetType()
                {
                    Id = 1,
                    Name = "xyz"
                }
            };
            int fakeAssetId = 1;
            AddOrUpdateOtherAssetsInfoModel fakemodel = new AddOrUpdateOtherAssetsInfoModel()
            {
                AssetTypeId = AssetTypes.SavingsAccoount,
                Value = 1,
                AssetId = 1,
                InstitutionName = "fake institute name",
                AccountNumber = "fake acc",
                Description = "fake desc",
                BorrowerId = 1
            };

            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new AssetsService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, dbFunctionServiceMock.Object, IloggerAssertServiceMock.Object);

            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetType.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetCategory.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfCollateralAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeCollateralAssetType.AsQueryable);


            var results = service.AddOrUpdateOtherAssetInfo(fakeTenant, fakeUserId, fakemodel);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);



        }
        [Fact]
        public async Task TestAddOrUpdateOtherAssetInfo_AssetIdNotExist_AddedOk()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            Mock<ILogger<AssetsService>> IloggerAssertServiceMock = new Mock<ILogger<AssetsService>>();
            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenantId,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 1, // Primary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "PrimaryFirstName",
                                    MiddleName = "PrimaryMiddleName",
                                    LastName = "PrimaryLastName",
                                    EmailAddress = "primaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                        AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=1,
                                        BorrowerAssetId=1,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=1,
                                            AssetTypeId=1,
                                            TenantId=1
                                        }
                                    }
                                }



                            },
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 2, // Secondary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "SecondaryFirstName",
                                    MiddleName = "SecondaryMiddleName",
                                    LastName = "SecondaryLastName",
                                    EmailAddress = "secondaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=2,
                                        BorrowerAssetId=2,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=2,
                                            AssetTypeId=2,
                                             TenantId=1
                                        }
                                    }
                                }



                            },

                   },


              };

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

            List<LoanApplicationDb.Entity.Models.AssetType> fakeAssetType = new List<LoanApplicationDb.Entity.Models.AssetType>()
            {
                new AssetType()
                {
                    Id = 1,
                    Name = "xyz",
                    AssetCategoryId=1
                }
            };
            List<LoanApplicationDb.Entity.Models.AssetCategory> fakeAssetCategory = new List<LoanApplicationDb.Entity.Models.AssetCategory>()
            {
                new LoanApplicationDb.Entity.Models.AssetCategory()
                {
                    Id = 1,
                    Name = "xyz"
                }
            };
            List<LoanApplicationDb.Entity.Models.CollateralAssetType> fakeCollateralAssetType = new List<LoanApplicationDb.Entity.Models.CollateralAssetType>()
            {
                new LoanApplicationDb.Entity.Models.CollateralAssetType()
                {
                    Id = 1,
                    Name = "xyz"
                }
            };
            int fakeAssetId = 1;
            AddOrUpdateOtherAssetsInfoModel fakemodel = new AddOrUpdateOtherAssetsInfoModel()
            {
                AssetTypeId = AssetTypes.SavingsAccoount,
                Value = 1,

                InstitutionName = "fake institute name",
                AccountNumber = "fake acc",
                Description = "fake desc",
                BorrowerId = 1
            };

            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new AssetsService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, dbFunctionServiceMock.Object, IloggerAssertServiceMock.Object);

            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetType.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetCategory.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfCollateralAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeCollateralAssetType.AsQueryable);


            var results = service.AddOrUpdateOtherAssetInfo(fakeTenant, fakeUserId, fakemodel);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);



        }
        [Fact]
        public async Task TestGetAllAssetsForHomeScreen_CalledNormally_Ok()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            Mock<ILogger<AssetsService>> IloggerAssetsServiceMock = new Mock<ILogger<AssetsService>>();
            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;

            Borrower primaryBorrower = new Borrower()
            {
                TenantId = 1,
                OwnTypeId = 1, // Primary
                LoanContact_LoanContactId = new LoanContact()
                {
                    TenantId = fakeTenant.Id,
                    FirstName = "PrimaryFirstName",
                    MiddleName = "PrimaryMiddleName",
                    LastName = "PrimaryLastName",
                    EmailAddress = "primaryborrower@emailinator.com",
                    MaritalStatusId = (int)MaritalStatus.Married,
                },
            };

            Borrower secondaryBorrower = new Borrower()
            {
                TenantId = 1,
                OwnTypeId = 2, // Secondary
                LoanContact_LoanContactId = new LoanContact()
                {
                    TenantId = fakeTenant.Id,
                    FirstName = "SecondaryFirstName",
                    MiddleName = "SecondaryMiddleName",
                    LastName = "SecondaryLastName",
                    EmailAddress = "secondaryborrower@emailinator.com",
                    MaritalStatusId = (int)MaritalStatus.Married
                }
            };

            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenantId,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                        primaryBorrower,
                        secondaryBorrower


                   },


              };
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();





            BorrowerAsset primaryBorrowerAsset = new BorrowerAsset()
            {
                InstitutionName = "Primary Borrower Fake Institute Name",
                TenantId = fakeTenantId,
                AssetTypeId = 1
            };
            applicationContext.Add<BorrowerAsset>(primaryBorrowerAsset);

            BorrowerAsset secondaryBorrowerAsset = new BorrowerAsset()
            {
                InstitutionName = "Secondary Borrower Fake Institute Name",
                TenantId = fakeTenantId,
                AssetTypeId = 1
            };
            applicationContext.Add<BorrowerAsset>(secondaryBorrowerAsset);
            await applicationContext.SaveChangesAsync();

            AssetBorrowerBinder primaryBorrowerBinder = new AssetBorrowerBinder()
            {
                BorrowerAssetId = primaryBorrowerAsset.Id,
                BorrowerId = primaryBorrower.Id
            };
            applicationContext.Add<AssetBorrowerBinder>(primaryBorrowerBinder);

            AssetBorrowerBinder secondaryBorrowerBinder = new AssetBorrowerBinder()
            {
                BorrowerAssetId = secondaryBorrowerAsset.Id,
                BorrowerId = secondaryBorrower.Id
            };
            applicationContext.Add<AssetBorrowerBinder>(secondaryBorrowerBinder);
            await applicationContext.SaveChangesAsync();

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

            List<LoanApplicationDb.Entity.Models.AssetType> fakeAssetType = new List<LoanApplicationDb.Entity.Models.AssetType>()
            {
                new AssetType()
                {
                    Id = 1,
                    Name = "xyz",
                    AssetCategoryId=1
                }
            };
            List<LoanApplicationDb.Entity.Models.AssetCategory> fakeAssetCategory = new List<LoanApplicationDb.Entity.Models.AssetCategory>()
            {
                new LoanApplicationDb.Entity.Models.AssetCategory()
                {
                    Id = 1,
                    Name = "xyz"
                }
            };
            List<LoanApplicationDb.Entity.Models.CollateralAssetType> fakeCollateralAssetType = new List<LoanApplicationDb.Entity.Models.CollateralAssetType>()
            {
                new LoanApplicationDb.Entity.Models.CollateralAssetType()
                {
                    Id = 1,
                    Name = "xyz"
                }
            };
            int fakeAssetId = 1;
            AddOrUpdateOtherAssetsInfoModel fakemodel = new AddOrUpdateOtherAssetsInfoModel()
            {
                AssetTypeId = AssetTypes.SavingsAccoount,
                Value = 1,

                InstitutionName = "fake institute name",
                AccountNumber = "fake acc",
                Description = "fake desc",
                BorrowerId = 1
            };


            var service = new AssetsService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, dbFunctionServiceMock.Object, IloggerAssetsServiceMock.Object);

            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetType.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetCategory.AsQueryable);
            dbFunctionServiceMock.Setup(x => x.UdfCollateralAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeCollateralAssetType.AsQueryable);


            var results = service.GetAllAssetsForHomeScreen(fakeTenant, fakeUserId, fakeModelReceived.LoanApplicationId);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);



        }
        //

        [Fact]
        public async Task TestGetGiftSourceAssets_CalledNormally_Ok()
        {
            // Arrange
            using LoanApplicationContext applicationContext = new LoanApplicationContext(base.LoanApplicationContextOptions);
            await applicationContext.Database.EnsureCreatedAsync();
            await applicationContext.SaveChangesAsync();

            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();
            var loggerMock = new Mock<ILogger<AssetsService>>();

            int fakeCategoryId = 1;
            int fakeTenantId = 1;
            int fakeAssetTypeId = 10;
            int fakeGiftSourceId = 1;

            TenantModel fakeTenant = new TenantModel()
            {
                Id = fakeTenantId
            };

            List<AssetType> fakeAssetTypes = new EditableList<AssetType>()
            {
                new AssetType()
                {
                    Id = fakeAssetTypeId,
                    Name = "Fake Asset Type Name",
                    AssetCategoryId = fakeCategoryId
                }
            };
            dbFunctionServiceMock.Setup(x => x.UdfAssetType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeAssetTypes.AsQueryable);

            List<LoanApplicationDb.Entity.Models.AssetCategory> fakeCategories =
                new EditableList<LoanApplicationDb.Entity.Models.AssetCategory>()
                {
                    new LoanApplicationDb.Entity.Models.AssetCategory()
                    {
                        Id = fakeCategoryId
                    }
                };
            dbFunctionServiceMock.Setup(x => x.UdfAssetCategory(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeCategories.AsQueryable);
            
            Borrower primaryBorrower = new Borrower()
            {
                LoanContact_LoanContactId = new LoanContact()
                {
                    FirstName = "Fake Primary First Name",
                    LastName = "Fake Primary Last Name"
                }
            };

            AssetTypeGiftSourceBinder giftSourceBinder = new AssetTypeGiftSourceBinder()
            {
                AssetTypeId = fakeAssetTypeId,
                GiftSourceId = fakeGiftSourceId
            };
            applicationContext.Add<AssetTypeGiftSourceBinder>(giftSourceBinder);
            await applicationContext.SaveChangesAsync();

            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    Borrowers = new List<Borrower>()
                    {
                        primaryBorrower
                    }
                };
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();

            int fakeBorrowerId = primaryBorrower.Id;

            BorrowerAsset primaryBorrowerAsset = new BorrowerAsset()
            {
                AssetTypeId = fakeAssetTypeId // Cash Gift
            };
            applicationContext.Add<BorrowerAsset>(primaryBorrowerAsset);
            await applicationContext.SaveChangesAsync();

            AssetBorrowerBinder binder = new AssetBorrowerBinder()
            {
                BorrowerId = primaryBorrower.Id,
                TenantId = fakeTenantId,
                BorrowerAssetId = primaryBorrowerAsset.Id
            };
            applicationContext.Add<AssetBorrowerBinder>(binder);
            await applicationContext.SaveChangesAsync();

            var service = new AssetsService(base.GetUnitOfWorkInstance<LoanApplicationContext>(applicationContext),
                serviceProviderMock.Object, dbFunctionServiceMock.Object, loggerMock.Object);

            // Act
            var results = await service.GetGiftSourceAssets(fakeTenant, fakeGiftSourceId);

            // Assert
            Assert.NotNull(results);
            Assert.NotEmpty(results);
        }

        [Fact]
        public async Task TestDeleteAsset_CalledNormally_Ok()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            Mock<ILogger<AssetsService>> IloggerEmploymentServiceMock = new Mock<ILogger<AssetsService>>();
            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenantId,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 1, // Primary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "PrimaryFirstName",
                                    MiddleName = "PrimaryMiddleName",
                                    LastName = "PrimaryLastName",
                                    EmailAddress = "primaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                        AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=1,
                                        BorrowerAssetId=1,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=1,
                                            AssetTypeId=1
                                        }
                                    }
                                }



                            },
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 2, // Secondary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "SecondaryFirstName",
                                    MiddleName = "SecondaryMiddleName",
                                    LastName = "SecondaryLastName",
                                    EmailAddress = "secondaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                                AssetBorrowerBinders= new List<AssetBorrowerBinder>()
                                {
                                    new AssetBorrowerBinder()
                                    {
                                        BorrowerId=2,
                                        BorrowerAssetId=2,
                                        BorrowerAsset= new BorrowerAsset()
                                        {
                                            Id=2,
                                            AssetTypeId=2
                                        }
                                    }
                                }



                            },

                   },


              };

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

            AssetDeleteModel fakeModelPost = new AssetDeleteModel()
                                             {
                                                 AssetId = 1,
                                                 BorrowerId = 1,
                                                 LoanApplicationId = 1
                                             };
            int fakeBorrowerAssetId = 1;
            int fakeAssetTypeId = 1;
            int fakeBorrowerId = 1;
            int fakeLoanApplicationId = 1;
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new AssetsService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, dbFunctionServiceMock.Object, IloggerEmploymentServiceMock.Object);


            var results = await service.DeleteAsset(fakeTenant, fakeUserId, fakeModelPost);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);



        }
    }
}
