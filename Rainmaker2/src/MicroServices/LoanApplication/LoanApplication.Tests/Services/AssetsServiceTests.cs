using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanApplication.Model;
using LoanApplication.Service;
using LoanApplication.Tests.Helpers;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Moq;
using TenantConfig.Common.DistributedCache;
using TrackableEntities.Common.Core;
using Xunit;

namespace LoanApplication.Tests.Services
{
    public partial class AssetsServiceTests
    {
        private readonly TenantModel _tenant;

        public AssetsServiceTests()
        {
            _tenant = ObjectHelper.GetTenantModel(1);
        }

        [Fact]
        public async Task GetEarnestMoneyDeposit_LoanGoalIsPropertyUnderContract_ShouldReturnDeposit()
        {
            //Arrange
            const string testMethodName = nameof(GetEarnestMoneyDeposit_LoanGoalIsPropertyUnderContract_ShouldReturnDeposit);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var assetsService = new AssetsService(unitOfWork, null, null, null);

            const int userId = 1;
            const int loanApplicationId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = loanApplicationId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.LoanGoalId = (int)LoanGoals.PropertyUnderContract;
            loanApplication.Deposit = 100;

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            await unitOfWork.SaveChangesAsync();

            //Act
            EarnestMoneyDepositModel earnestMoneyDepositModel = await assetsService.GetEarnestMoneyDeposit(_tenant, userId, loanApplicationId);

            //Assert
            Assert.NotNull(earnestMoneyDepositModel.Deposit);
        }

        [Fact]
        public async Task GetEarnestMoneyDeposit_LoanGoalIsNotPropertyUnderContract_ShouldNotReturnDeposit()
        {
            //Arrange
            const string testMethodName = nameof(GetEarnestMoneyDeposit_LoanGoalIsNotPropertyUnderContract_ShouldNotReturnDeposit);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var assetsService = new AssetsService(unitOfWork, null, null, null);

            const int userId = 1;
            const int loanApplicationId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = loanApplicationId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.LoanGoalId = null;
            loanApplication.Deposit = 100;

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            await unitOfWork.SaveChangesAsync();

            //Act
            EarnestMoneyDepositModel earnestMoneyDepositModel = await assetsService.GetEarnestMoneyDeposit(_tenant, userId, loanApplicationId);

            //Assert
            Assert.Null(earnestMoneyDepositModel.Deposit);
        }

        [Fact]
        public async Task UpdateEarnestMoneyDeposit_LoanGoalIsPropertyUnderContract_ShouldUpdateDeposit()
        {
            //Arrange
            const string testMethodName = nameof(UpdateEarnestMoneyDeposit_LoanGoalIsPropertyUnderContract_ShouldUpdateDeposit);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var assetsService = new AssetsService(unitOfWork, null, null, null);

            const int userId = 1;
            const int loanApplicationId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = loanApplicationId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.LoanGoalId = (int)LoanGoals.PropertyUnderContract;
            loanApplication.Deposit = null;

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            await unitOfWork.SaveChangesAsync();

            //Act
            const decimal updatedDeposit = 100;
            var earnestMoneyDepositModel = new EarnestMoneyDepositModel
            {
                LoanApplicationId = loanApplicationId,
                Deposit = updatedDeposit
            };

            await assetsService.UpdateEarnestMoneyDeposit(_tenant, userId, earnestMoneyDepositModel);

            //Assert
            var record = unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == loanApplicationId).Single();

            Assert.NotNull(record);
            Assert.Equal(updatedDeposit, record.Deposit);
        }

        [Fact]
        public async Task UpdateEarnestMoneyDeposit_LoanGoalIsNotPropertyUnderContract_ShouldNotUpdateDeposit()
        {
            //Arrange
            const string testMethodName = nameof(UpdateEarnestMoneyDeposit_LoanGoalIsNotPropertyUnderContract_ShouldNotUpdateDeposit);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var assetsService = new AssetsService(unitOfWork, null, null, null);

            const int userId = 1;
            const int loanApplicationId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = loanApplicationId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.LoanGoalId = null;
            loanApplication.Deposit = null;

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            await unitOfWork.SaveChangesAsync();

            //Act
            const decimal updatedDeposit = 100;
            var earnestMoneyDepositModel = new EarnestMoneyDepositModel
            {
                LoanApplicationId = loanApplicationId,
                Deposit = updatedDeposit
            };

            await assetsService.UpdateEarnestMoneyDeposit(_tenant, userId, earnestMoneyDepositModel);

            //Assert
            var record = unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == loanApplicationId).Single();

            Assert.NotNull(record);
            Assert.NotEqual(updatedDeposit, record.Deposit);
        }

        //[Fact]
        //public async Task GetBankAccountType_ShouldReturnBankAccountType() // Raw Sql Query
        //{
        //    //Arrange
        //    const string testMethodName = nameof(GetBankAccountType_ShouldReturnBankAccountType);
        //    var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
        //    var assetsService = new AssetsService(unitOfWork, null, null, null);

        //    var assetType = new LoanApplicationDb.Entity.Models.AssetType();
        //    assetType.Id = 1;
        //    assetType.Name = "Checking Account";
        //    assetType.AssetCategoryId = (int)LoanApplication.Model.AssetCategory.BankAccount;
        //    assetType.IsActive = true;

        //    unitOfWork.Repository<LoanApplicationDb.Entity.Models.AssetType>().Insert(assetType);
        //    await unitOfWork.SaveChangesAsync();

        //    //Act
        //    List<AssetTypeModel> listAssetTypeModel = await assetsService.GetBankAccountType(_tenant);

        //    //Assert
        //    Assert.NotNull(listAssetTypeModel);
        //}

        [Fact]
        public async Task GetBankAccount_ShouldReturnBankAccount()
        {
            //Arrange
            const string testMethodName = nameof(GetBankAccount_ShouldReturnBankAccount);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var assetsService = new AssetsService(unitOfWork, null, null, null);

            const int userId = 1;
            const int borrowerAssetId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;

            var borrowerAsset = new LoanApplicationDb.Entity.Models.BorrowerAsset()
            {
                Id = 1,
                AssetTypeId = 1,
                TenantId = 1,
                AccountNumber = "ABC",
                InstitutionName = "ABC",
                Value = 2400
            };
            borrowerAsset.AssetType = new LoanApplicationDb.Entity.Models.AssetType()
            {
                Id = 1,
                AssetCategoryId = (int)Model.AssetCategory.BankAccount
            };
            borrowerAsset.AssetBorrowerBinders = new HashSet<AssetBorrowerBinder>
                 {
                     new AssetBorrowerBinder()
                     {
                          BorrowerId = borrowerId,
                          Borrower = new Borrower()
                          {
                              Id = borrowerId,
                              LoanApplicationId =loanApplicationId,
                              LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                              {
                                  Id = loanApplicationId,
                                  UserId = userId,
                              }
                          }
                     }
                 };

            var borrowers = borrowerAsset.AssetBorrowerBinders.Select(x => x.Borrower).ToList();
            var loanApplications = borrowerAsset.AssetBorrowerBinders.Select(x => x.Borrower.LoanApplication).ToList();

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerAsset>().Insert(borrowerAsset);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.AssetType>().Insert(borrowerAsset.AssetType);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.AssetBorrowerBinder>().InsertRange(borrowerAsset.AssetBorrowerBinders);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().InsertRange(borrowers);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().InsertRange(loanApplications);
            await unitOfWork.SaveChangesAsync();

            //Act
            BorrowerAssetModelResponse borrowerAssetModelResponse = await assetsService.GetBankAccount(_tenant, userId , borrowerAssetId, borrowerId , loanApplicationId);

            //Assert
            Assert.NotNull(borrowerAssetModelResponse);
        }

        [Fact]
        public async Task AddOrUpdateBankAccount_ShouldUpdateBankAccount()
        {
            //Arrange
            const string testMethodName = nameof(AddOrUpdateBankAccount_ShouldUpdateBankAccount);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var assetsService = new AssetsService(unitOfWork, null, null, null);

            const int userId = 1;
            const int borrowerAssetId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int tenantId = 1;

            var borrowerAsset = new LoanApplicationDb.Entity.Models.BorrowerAsset()
            {
                Id = 1,
                AssetTypeId = 1,
                TenantId = 1,
                AccountNumber = "ABC",
                InstitutionName = "ABC",
                Value = 2400
            };
            borrowerAsset.AssetType = new LoanApplicationDb.Entity.Models.AssetType()
            {
                Id = 1,
                AssetCategoryId = (int)Model.AssetCategory.BankAccount
            };
            borrowerAsset.AssetBorrowerBinders = new HashSet<AssetBorrowerBinder>
                 {
                     new AssetBorrowerBinder()
                     {
                          BorrowerId = borrowerId,
                          Borrower = new Borrower()
                          {
                              Id = borrowerId,
                              LoanApplicationId =loanApplicationId,
                              LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                              {
                                  Id = loanApplicationId,
                                  UserId = userId,
                                  TenantId = tenantId
                              }
                          }
                     }
                 };

            var borrowers = borrowerAsset.AssetBorrowerBinders.Select(x => x.Borrower).ToList();
            var loanApplications = borrowerAsset.AssetBorrowerBinders.Select(x => x.Borrower.LoanApplication).ToList();

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerAsset>().Insert(borrowerAsset);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.AssetType>().Insert(borrowerAsset.AssetType);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.AssetBorrowerBinder>().InsertRange(borrowerAsset.AssetBorrowerBinders);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().InsertRange(borrowers);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().InsertRange(loanApplications);
            await unitOfWork.SaveChangesAsync();
            

            //Act
            const int borrowerAssetAssetTypeId = 1;
            const decimal updatedborrowerAssetValue = 20;
            var borrowerAssetModelRequest = new BorrowerAssetModelRequest
            {
                Id = borrowerAssetId,
                BorrowerId = borrowerId,
                AssetTypeId = borrowerAssetAssetTypeId,
                InstitutionName = "ABC1",
                AccountNumber ="ABC1",
                Balance = updatedborrowerAssetValue,
                LoanApplicationId = loanApplicationId,
                State = "AAA1"
            };
            await assetsService.AddOrUpdateBankAccount(_tenant, userId, borrowerAssetModelRequest);
            
            //Assert
            var record = unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerAsset>().Query(x => x.Id == borrowerAssetId).Single();

            Assert.NotNull(record);
            Assert.Equal(updatedborrowerAssetValue, record.Value);
            
        }

        [Fact]
        public async Task AddOrUpdateBankAccount_ShouldAddBankAccount()
        {
            //Arrange
            const string testMethodName = nameof(AddOrUpdateBankAccount_ShouldAddBankAccount);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var assetsService = new AssetsService(unitOfWork, null, null, null);

            const int userId = 1;
            const int loanApplicationId = 1;
            const int borrowerId = 1;
            const int borrowerAssetAssetTypeId = 1;
            const int tenantId = 1;
            const int expectedBorrowerAssetId = 1;

            //Act
            var borrowerAssetModelRequest = new BorrowerAssetModelRequest
            {
                Id = null,
                AssetTypeId = borrowerAssetAssetTypeId,
                InstitutionName = "ABC",
                AccountNumber = "ABC",
                Balance = 24,
                LoanApplicationId = loanApplicationId,
                State = "AAA",
                BorrowerId = borrowerId
            };

            LoanApplicationDb.Entity.Models.LoanApplication application = new LoanApplicationDb.Entity.Models.LoanApplication()
            {
                Id = loanApplicationId,
                TenantId = tenantId,
                UserId = userId
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(application);
            await unitOfWork.SaveChangesAsync();

            await assetsService.AddOrUpdateBankAccount(_tenant, userId, borrowerAssetModelRequest);

            //Assert
            var record = unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerAsset>().Query(x => x.Id == 1).Single();

            Assert.NotNull(record);
            Assert.Equal(expectedBorrowerAssetId, record.Id);

        }
        
        [Fact]
        public async Task GetGiftAsset_ShouldReturnGiftAsset()
        {
            //Arrange
            const string testMethodName = nameof(GetGiftAsset_ShouldReturnGiftAsset);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var assetsService = new AssetsService(unitOfWork, null, null, null);

            const int userId = 1;
            const int borrowerAssetId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;

            var borrowerAsset = new BorrowerAsset
            {
                TenantId = 1,
                Id = borrowerAssetId,
                AssetTypeId = 1,
                GiftSourceId = 4,
                Value = 100,
                AssetType = new AssetType
                {
                    Id = 1,
                    AssetCategoryId = (int)Model.AssetCategory.GiftFunds,
                    TrackingState = TrackingState.Added
                },
                AssetBorrowerBinders = new HashSet<AssetBorrowerBinder>
                {
                    new AssetBorrowerBinder
                    {
                        BorrowerId = borrowerId,
                        Borrower = new Borrower
                        {
                            Id = borrowerId,
                            LoanApplicationId = loanApplicationId,
                            LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
                            {
                                TenantId = _tenant.Id,
                                Id = loanApplicationId,
                                UserId = userId,
                                TrackingState = TrackingState.Added
                            },
                            TrackingState = TrackingState.Added
                        },
                        TrackingState = TrackingState.Added
                    }
                },
                TrackingState = TrackingState.Added
            };

            unitOfWork.Repository<BorrowerAsset>().Insert(borrowerAsset);
            await unitOfWork.SaveChangesAsync();

            //Act
            GiftAssetModel giftAssetModel = await assetsService.GetGiftAsset(_tenant, userId, borrowerAssetId, borrowerId, loanApplicationId);

            //Assert
            Assert.NotNull(giftAssetModel);
        }

        [Fact]
        public async Task AddOrUpdateGiftAsset_ShouldUpdateGiftAssetRecord()
        {
            //Arrange
            const string testMethodName = nameof(AddOrUpdateGiftAsset_ShouldUpdateGiftAssetRecord);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var assetsService = new AssetsService(unitOfWork, null, null, null);

            const int assetTypeId = 1;
            const int giftSourceId = 4;
            const int borrowerAssetId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int userId = 1;

            var borrowerAsset = new BorrowerAsset
            {
                TenantId = 1,
                Id = borrowerAssetId,
                AssetTypeId = assetTypeId,
                GiftSourceId = giftSourceId,
                Value = 100,
                AssetType = new AssetType
                {
                    Id = 1, 
                    AssetCategoryId = (int) Model.AssetCategory.GiftFunds,
                    TrackingState = TrackingState.Added
                },
                AssetBorrowerBinders = new HashSet<AssetBorrowerBinder>
                {
                    new AssetBorrowerBinder
                    {
                        BorrowerId = borrowerId,
                        Borrower = new Borrower
                        {
                            Id = borrowerId,
                            LoanApplicationId = loanApplicationId,
                            LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
                            {
                                TenantId = _tenant.Id,
                                Id = loanApplicationId,
                                UserId = userId,
                                TrackingState = TrackingState.Added
                            },
                            TrackingState = TrackingState.Added
                        },
                        TrackingState = TrackingState.Added
                    }
                },
                TrackingState = TrackingState.Added
            };

            unitOfWork.Repository<BorrowerAsset>().Insert(borrowerAsset);
            await unitOfWork.SaveChangesAsync();

            //Act
            const decimal updatedValue = 200;
            var giftAssetModel = new GiftAssetModel
            {
                Id = borrowerAssetId,
                BorrowerId = borrowerId,
                AssetTypeId = assetTypeId,
                LoanApplicationId = loanApplicationId,
                GiftSourceId = giftSourceId,
                Value = updatedValue
            };

            await assetsService.AddOrUpdateGiftAsset(_tenant, userId, giftAssetModel);

            //Assert
            var record = unitOfWork.Repository<BorrowerAsset>().Query(x => x.Id == borrowerAssetId).Single();

            Assert.NotNull(record);
            Assert.Equal(updatedValue, record.Value);
        }

        [Fact]
        public async Task AddOrUpdateGiftAsset_ShouldAddGiftAssetRecord()
        {
            //Arrange
            const string testMethodName = nameof(AddOrUpdateGiftAsset_ShouldAddGiftAssetRecord);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var assetsService = new AssetsService(unitOfWork, null, null, null);

            const int userId = 1;

            var giftAssetModel = new GiftAssetModel
            {
                Id = null,
                BorrowerId = 1,
                AssetTypeId = 1,
                GiftSourceId = 1,
                IsDeposited = true,
                Value = 500,
                LoanApplicationId = 1
            };

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 1,
                TenantId = _tenant.Id,
                UserId = userId
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            await unitOfWork.SaveChangesAsync();

            //Act
            await assetsService.AddOrUpdateGiftAsset(_tenant, userId, giftAssetModel);

            //Assert
            var record = unitOfWork.Repository<BorrowerAsset>().Query().Single();

            Assert.NotNull(record);
        }

        [Fact]
        public async Task GetFinancialAsset_ShouldReturnFinancialAsset()
        {
            //Arrange
            const string testMethodName = nameof(GetFinancialAsset_ShouldReturnFinancialAsset);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var assetsService = new AssetsService(unitOfWork, null, null, null);

            const int userId = 1;
            const int borrowerAssetId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;

            var borrowerAsset = new LoanApplicationDb.Entity.Models.BorrowerAsset()
            {
                Id = 1,
                AssetTypeId = 1,
                TenantId = 1,
                AccountNumber = "ABC",
                InstitutionName = "ABC",
                Value = 2400
            };
            borrowerAsset.AssetType = new LoanApplicationDb.Entity.Models.AssetType()
            {
                Id = 1,
                AssetCategoryId = (int)Model.AssetCategory.FinancialStatement,
                TrackingState = TrackingState.Added
            };
            borrowerAsset.AssetBorrowerBinders = new HashSet<AssetBorrowerBinder>
                 {
                     new AssetBorrowerBinder()
                     {
                          BorrowerId = borrowerId,
                          TrackingState = TrackingState.Added,
                          Borrower = new Borrower()
                          {
                              Id = borrowerId,
                              LoanApplicationId =loanApplicationId,
                              TrackingState = TrackingState.Added,
                              LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                              {
                                  Id = loanApplicationId,
                                  UserId = userId,
                                  TrackingState = TrackingState.Added
                              }
                          }
                     }
                 };
            
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerAsset>().Insert(borrowerAsset);
            await unitOfWork.SaveChangesAsync();

            //Act
            BorrowerAssetFinancialModelResponse borrowerAssetModelResponse = await assetsService.GetFinancialAsset(_tenant, userId, borrowerAssetId, borrowerId, loanApplicationId);

            //Assert
            Assert.NotNull(borrowerAssetModelResponse);
        }

        [Fact]
        public async Task AddOrUpdateFinancialAsset_ShouldAddFinancialAsset()
        {
            //Arrange
            const string testMethodName = nameof(AddOrUpdateFinancialAsset_ShouldAddFinancialAsset);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var assetsService = new AssetsService(unitOfWork, null, null, null);

            const int userId = 1;
            const int loanApplicationId = 1;
            const int borrowerId = 1;
            const int borrowerAssetAssetTypeId = 3;
            const int tenantId = 1;
            const int expectedBorrowerAssetId = 1;

            //Act
            var borrowerAssetModelRequest = new BorrowerAssetFinancialModelRequest
            {
                Id = null,
                AssetTypeId = borrowerAssetAssetTypeId,
                InstitutionName = "ABC",
                AccountNumber = "ABC",
                Balance = 24,
                LoanApplicationId = loanApplicationId,
                State = "AAA",
                BorrowerId = borrowerId
            };

            LoanApplicationDb.Entity.Models.LoanApplication application = new LoanApplicationDb.Entity.Models.LoanApplication()
            {
                Id = loanApplicationId,
                TenantId = tenantId,
                UserId = userId
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(application);
            await unitOfWork.SaveChangesAsync();

            await assetsService.AddOrUpdateFinancialAsset(_tenant, userId, borrowerAssetModelRequest);

            //Assert
            var record = unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerAsset>().Query(x => x.Id == 1).Single();

            Assert.NotNull(record);
            Assert.Equal(expectedBorrowerAssetId, record.Id);

        }

        [Fact]
        public async Task AddOrUpdateFinancialAsset_ShouldUpdateFinancialAsset()
        {
            //Arrange
            const string testMethodName = nameof(AddOrUpdateFinancialAsset_ShouldUpdateFinancialAsset);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var assetsService = new AssetsService(unitOfWork, null, null, null);

            const int userId = 1;
            const int borrowerAssetId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int tenantId = 1;

            var borrowerAsset = new LoanApplicationDb.Entity.Models.BorrowerAsset()
            {
                Id = 1,
                AssetTypeId = 3,
                TenantId = 1,
                AccountNumber = "ABC",
                InstitutionName = "ABC",
                Value = 2400
            };
            borrowerAsset.AssetType = new LoanApplicationDb.Entity.Models.AssetType()
            {
                Id = 1,
                AssetCategoryId = (int)Model.AssetCategory.FinancialStatement,
                TrackingState = TrackingState.Added
            };
            borrowerAsset.AssetBorrowerBinders = new HashSet<AssetBorrowerBinder>
                 {
                     new AssetBorrowerBinder()
                     {
                          BorrowerId = borrowerId,
                          TrackingState = TrackingState.Added,
                          Borrower = new Borrower()
                          {
                              Id = borrowerId,
                              LoanApplicationId =loanApplicationId,
                              TrackingState = TrackingState.Added,
                              LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                              {
                                  Id = loanApplicationId,
                                  UserId = userId,
                                  TenantId = tenantId,
                                  TrackingState = TrackingState.Added
                              }
                          }
                     }
                 };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerAsset>().Insert(borrowerAsset);
            await unitOfWork.SaveChangesAsync();


            //Act
            const int borrowerAssetAssetTypeId = 3;
            const decimal updatedborrowerAssetValue = 20;
            var borrowerAssetModelRequest = new BorrowerAssetFinancialModelRequest
            {
                Id = borrowerAssetId,
                BorrowerId = borrowerId,
                AssetTypeId = borrowerAssetAssetTypeId,
                InstitutionName = "ABC1",
                AccountNumber = "ABC1",
                Balance = updatedborrowerAssetValue,
                LoanApplicationId = loanApplicationId,
                State = "AAA1"
            };
            await assetsService.AddOrUpdateFinancialAsset(_tenant, userId, borrowerAssetModelRequest);

            //Assert
            var record = unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerAsset>().Query(x => x.Id == borrowerAssetId).Single();

            Assert.NotNull(record);
            Assert.Equal(updatedborrowerAssetValue, record.Value);

        }

        [Fact]
        public async Task GetAssetsTypes_ShouldReturnAllAssetTypes()
        {
            //Arrange
            const string testMethodName = nameof(GetAssetsTypes_ShouldReturnAllAssetTypes);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var dbFunctionService = new Mock<IDbFunctionService>();
            var assetsService = new AssetsService(unitOfWork, null, dbFunctionService.Object, null);

            var assetTypes = new List<AssetType>
            {
                new AssetType
                {
                    Id = 1,
                    Name = "Asset Type 1",
                    AssetCategoryId = (int) Model.AssetCategory.GiftFunds,
                    AssetTypeGiftSourceBinders = new List<AssetTypeGiftSourceBinder>
                    {
                        new AssetTypeGiftSourceBinder {AssetTypeId = 1, GiftSourceId = 1}
                    }
                },
                new AssetType
                {
                    Id = 2,
                    Name = "Asset Type 2",
                    AssetCategoryId = (int) Model.AssetCategory.GiftFunds,
                    AssetTypeGiftSourceBinders = new List<AssetTypeGiftSourceBinder>
                    {
                        new AssetTypeGiftSourceBinder {AssetTypeId = 2, GiftSourceId = 1}
                    }
                }
            };

            unitOfWork.Repository<AssetType>().InsertRange(assetTypes);
            await unitOfWork.SaveChangesAsync();

            IQueryable<AssetType> queryable = unitOfWork.Repository<AssetType>().Query();
            dbFunctionService.Setup(x => x.UdfAssetType(1, It.IsAny<int?>())).Returns(queryable);

            //Act
            List<AssetTypeModel> assetTypeModels = await assetsService.GetAssetsTypes(_tenant, 1);

            //Assert
            Assert.NotNull(assetTypeModels);
            Assert.Equal(assetTypes.Count, assetTypeModels.Count);
        }
    }
}