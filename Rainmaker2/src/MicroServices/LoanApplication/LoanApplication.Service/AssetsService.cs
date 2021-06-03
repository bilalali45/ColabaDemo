using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanApplication.Model;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TenantConfig.Common.DistributedCache;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;
using TenantConfig.Common;

namespace LoanApplication.Service
{
    public partial class AssetsService : ServiceBase<LoanApplicationContext, LoanApplicationDb.Entity.Models.LoanApplication>, IAssetsService
    {
        public const int LoanApplicationNotFound = -1;
        public const int BorrowerDetailNotFound = -2;
        public const int AssetTypeNotFound = -3;
        public const int AssetCategoryNotFound = -4;
        public const int AssetTypeDoesNotBelongToCategory = -5;
        public const int AssetCategoryNotAllowed = -6;
        public const int AssetCollateralNotAllowed = -7;
        public const int BorrowerAssetDetailNotFound = -8;

        private readonly IDbFunctionService _dbFunctionService;
        private readonly ILogger<AssetsService> _logger;

        public Dictionary<int, string> ErrorMessages { get; } = null;

        public AssetsService(IUnitOfWork<LoanApplicationContext> previousUow, IServiceProvider services, IDbFunctionService dbFunctionService, ILogger<AssetsService> logger)
            : base(previousUow, services)
        {
            _dbFunctionService = dbFunctionService;
            _logger = logger;
            ErrorMessages = new Dictionary<int, string>()
            {
                [LoanApplicationNotFound] = "Loan application not found",
                [BorrowerDetailNotFound] = "Borrower detail not found",
                [AssetTypeNotFound] = "Asset type not found",
                [AssetCategoryNotFound] = "Asset category not found",
                [AssetTypeDoesNotBelongToCategory] = "Asset type does not belong to category",
                [AssetCategoryNotAllowed] = "Asset category is not allowed with this end point",
                [AssetCollateralNotAllowed] = "Asset collateral type is not allowed with this end point",
                [BorrowerAssetDetailNotFound] = "Borrower asset detail not found"

            };
        }

        public async Task<int> UpdateEarnestMoneyDeposit(TenantModel tenant, int userId, EarnestMoneyDepositModel model)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.TenantId == tenant.Id && x.UserId == userId && x.Id == model.LoanApplicationId)
                .SingleAsync();

            if (loanApplication.LoanGoalId == (int)LoanGoals.PropertyUnderContract)
            {
                loanApplication.IsEarnestMoneyProvided = model.IsEarnestMoneyProvided;
                loanApplication.Deposit = model.Deposit;
            }
            else
            {
                loanApplication.IsEarnestMoneyProvided = null;
                loanApplication.Deposit = null;
            }

            loanApplication.State = model.State;
            loanApplication.TrackingState = TrackingState.Modified;

            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
            await Uow.SaveChangesAsync();

            return loanApplication.Id;
        }

        public async Task<EarnestMoneyDepositModel> GetEarnestMoneyDeposit(TenantModel tenant, int userId, int loanApplicationId)
        {
            return await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.TenantId == tenant.Id && x.UserId == userId && x.Id == loanApplicationId)
                .Select(x => new EarnestMoneyDepositModel()
                {
                    LoanApplicationId = x.Id,
                    IsEarnestMoneyProvided = x.IsEarnestMoneyProvided,
                    Deposit = x.LoanGoalId == (int)LoanGoals.PropertyUnderContract ? x.Deposit : null
                })
                .SingleAsync();
        }
        public async Task DeleteEarnestMoneyDeposit(TenantModel tenant, int userId, int loanApplicationId)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.TenantId == tenant.Id && x.UserId == userId && x.Id == loanApplicationId)
                .SingleAsync();
            loanApplication.TrackingState = TrackingState.Modified;
            loanApplication.IsEarnestMoneyProvided = null;
            loanApplication.Deposit = null;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
            await Uow.SaveChangesAsync();
        }
        public async Task<List<GiftSourceModel>> GetAllGiftSources(TenantModel tenant)
        {
            var results = _dbFunctionService.UdfGiftSource(tenant.Id).ToList();

            return await _dbFunctionService.UdfGiftSource(tenant.Id)
                .Select(x => new GiftSourceModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = CommonHelper.GenerateGlobalCdnUrl(tenant, x.Image)
                })
                .ToListAsync();
        }

        public async Task<List<AssetTypeModel>> GetBankAccountType(TenantModel tenant)
        {
            return await Uow.DataContext.UdfAssetType(tenant.Id)
                .Where(x => x.AssetCategoryId == (int)Model.AssetCategory.BankAccount && x.IsActive == true)
                .Select(x => new AssetTypeModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }

        public async Task<int> AddOrUpdateBankAccount(TenantModel tenant, int userId, BorrowerAssetModelRequest model)
        {   
            LoanApplicationDb.Entity.Models.BorrowerAsset borrowerAsset = default(LoanApplicationDb.Entity.Models.BorrowerAsset);
            if (!model.Id.HasValue)
            {
                borrowerAsset = new LoanApplicationDb.Entity.Models.BorrowerAsset
                {
                    TrackingState = TrackingState.Added,
                    AssetBorrowerBinders = new HashSet<AssetBorrowerBinder>
                    {
                        new AssetBorrowerBinder()
                        {
                            TrackingState = TrackingState.Added,
                            BorrowerId = model.BorrowerId,
                            TenantId = tenant.Id
                        }
                    }
                };
                Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerAsset>().Insert(borrowerAsset);
            }
            else
            {
                borrowerAsset = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerAsset>()
                                      .Query(query: x => x.TenantId == tenant.Id && x.Id == model.Id && x.AssetType.AssetCategoryId == (int)Model.AssetCategory.BankAccount && x.AssetBorrowerBinders
                .Any(y => y.BorrowerId == model.BorrowerId && y.Borrower.LoanApplicationId == model.LoanApplicationId && y.Borrower.LoanApplication.UserId == userId)).FirstOrDefaultAsync();
                borrowerAsset.TrackingState = TrackingState.Modified;
            }
            borrowerAsset.AssetTypeId = model.AssetTypeId;
            borrowerAsset.AccountNumber = model.AccountNumber;
            borrowerAsset.InstitutionName = model.InstitutionName;
            borrowerAsset.Value = model.Balance;
            borrowerAsset.TenantId = tenant.Id;

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);

            await SaveChangesAsync();
            return borrowerAsset.Id;
        }

        public async Task<BorrowerAssetModelResponse> GetBankAccount(TenantModel tenant, int userId, int borrowerAssetId, int borrowerId, int loanApplicationId)
        {
            return await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerAsset>()
                .Query(x => x.TenantId == tenant.Id && x.Id == borrowerAssetId && x.AssetType.AssetCategoryId == (int)Model.AssetCategory.BankAccount && x.AssetBorrowerBinders
                .Any(y => y.BorrowerId == borrowerId && y.Borrower.LoanApplicationId == loanApplicationId && y.Borrower.LoanApplication.UserId == userId))
                .Select(x => new BorrowerAssetModelResponse()
                {
                    Id = x.Id,
                    AssetTypeId = x.AssetTypeId.Value,
                    AccountNumber = x.AccountNumber,
                    InstitutionName = x.InstitutionName,
                    Balance = x.Value,
                    LoanApplicationId = loanApplicationId
                })
                .SingleAsync();
        }

        public async Task<RetirementAccountModel> GetRetirementAccount(TenantModel tenant, int userId, int loanApplicationId, int borrowerId, int borrowerAssetId)
        {

            return await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerAsset>()
               .Query(x => x.TenantId == tenant.Id && x.Id == borrowerAssetId && x.AssetType.AssetCategoryId == (int)Model.AssetCategory.RetirementAccount && x.AssetBorrowerBinders
               .Any(y => y.BorrowerId == borrowerId && y.Borrower.LoanApplicationId == loanApplicationId && y.Borrower.LoanApplication.UserId == userId))
               .Select(x => new RetirementAccountModel()
               {
                   Id = x.Id,
                   AccountNumber = x.AccountNumber,
                   InstitutionName = x.InstitutionName,
                   Value = x.Value,
                   LoanApplicationId = loanApplicationId
               })
               .SingleAsync();

        }

        public async Task<int> UpdateRetirementAccount(TenantModel tenant, int userId, RetirementAccountModel model)
        {

            LoanApplicationDb.Entity.Models.BorrowerAsset borrowerAsset = default(LoanApplicationDb.Entity.Models.BorrowerAsset);
            if (!model.Id.HasValue)
            {
                borrowerAsset = new LoanApplicationDb.Entity.Models.BorrowerAsset
                {
                    TrackingState = TrackingState.Added,
                    AssetBorrowerBinders = new HashSet<AssetBorrowerBinder>
                    {
                        new AssetBorrowerBinder()
                        {
                            TrackingState = TrackingState.Added,
                            BorrowerId = model.BorrowerId,
                            TenantId = tenant.Id
                        }
                    }
                };
                Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerAsset>().Insert(borrowerAsset);
            }
            else
            {
                borrowerAsset = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerAsset>()
                                      .Query(query: x => x.TenantId == tenant.Id && x.Id == model.Id && x.AssetType.AssetCategoryId == (int)Model.AssetCategory.RetirementAccount && x.AssetBorrowerBinders
                .Any(y => y.BorrowerId == model.BorrowerId && y.Borrower.LoanApplicationId == model.LoanApplicationId && y.Borrower.LoanApplication.UserId == userId)).FirstOrDefaultAsync();
                borrowerAsset.TrackingState = TrackingState.Modified;
            }
            borrowerAsset.AssetTypeId = 9; // to b replace by enumeration
            borrowerAsset.AccountNumber = model.AccountNumber;
            borrowerAsset.InstitutionName = model.InstitutionName;
            borrowerAsset.Value = model.Value;
            borrowerAsset.TenantId = tenant.Id;

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);

            await SaveChangesAsync();
            return borrowerAsset.Id;
        }

        public async Task<List<AssetTypeFinancialModel>> GetAllFinancialAsset(TenantModel tenant)
        {
            return await Uow.DataContext.UdfAssetType(tenant.Id)
                .Where(x => x.AssetCategoryId == (int)Model.AssetCategory.FinancialStatement && x.IsActive == true)
                .Select(x => new AssetTypeFinancialModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }

        public async Task<int> AddOrUpdateFinancialAsset(TenantModel tenant, int userId, BorrowerAssetFinancialModelRequest model)
        {
            LoanApplicationDb.Entity.Models.BorrowerAsset borrowerAsset = default(LoanApplicationDb.Entity.Models.BorrowerAsset);
            if (!model.Id.HasValue)
            {
                borrowerAsset = new LoanApplicationDb.Entity.Models.BorrowerAsset
                {
                    TrackingState = TrackingState.Added,
                    AssetBorrowerBinders = new HashSet<AssetBorrowerBinder>
                    {
                        new AssetBorrowerBinder()
                        {
                            TrackingState = TrackingState.Added,
                            BorrowerId = model.BorrowerId,
                            TenantId = tenant.Id
                        }
                    }
                };
                Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerAsset>().Insert(borrowerAsset);
            }
            else
            {
                borrowerAsset = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerAsset>()
                                      .Query(query: x => x.TenantId == tenant.Id && x.Id == model.Id && x.AssetType.AssetCategoryId == (int)Model.AssetCategory.FinancialStatement && x.AssetBorrowerBinders
                .Any(y => y.BorrowerId == model.BorrowerId && y.Borrower.LoanApplicationId == model.LoanApplicationId && y.Borrower.LoanApplication.UserId == userId)).FirstOrDefaultAsync();
                borrowerAsset.TrackingState = TrackingState.Modified;
            }
            borrowerAsset.AssetTypeId = model.AssetTypeId;
            borrowerAsset.AccountNumber = model.AccountNumber;
            borrowerAsset.InstitutionName = model.InstitutionName;
            borrowerAsset.Value = model.Balance;
            borrowerAsset.TenantId = tenant.Id;

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);

            await SaveChangesAsync();
            return borrowerAsset.Id;
        }

        public async Task<int> AddOrUpdateGiftAsset(TenantModel tenant, int userId, GiftAssetModel model)
        {
            BorrowerAsset borrowerAsset;

            if (!model.Id.HasValue)
            {
                borrowerAsset = new BorrowerAsset
                {
                    TrackingState = TrackingState.Added,
                    AssetBorrowerBinders = new HashSet<AssetBorrowerBinder>
                    {
                        new AssetBorrowerBinder
                        {
                            TrackingState = TrackingState.Added,
                            BorrowerId = model.BorrowerId,
                            TenantId = tenant.Id
                        }
                    }
                };

                Uow.Repository<BorrowerAsset>().Insert(borrowerAsset);
            }
            else
            {
                borrowerAsset = await Uow.Repository<BorrowerAsset>()
                    .Query(x => x.TenantId == tenant.Id && x.Id == model.Id && x.AssetType.AssetCategoryId == (int)Model.AssetCategory.GiftFunds
                                && x.AssetBorrowerBinders.Any(y => y.BorrowerId == model.BorrowerId && y.Borrower.LoanApplicationId == model.LoanApplicationId
                                                                && y.Borrower.LoanApplication.UserId == userId))
                    .FirstOrDefaultAsync();

                borrowerAsset.TrackingState = TrackingState.Modified;
            }

            borrowerAsset.TenantId = tenant.Id;
            borrowerAsset.AssetTypeId = model.AssetTypeId;
            borrowerAsset.GiftSourceId = model.GiftSourceId;
            borrowerAsset.IsDeposited = model.IsDeposited;
            borrowerAsset.Value = model.Value;
            borrowerAsset.ValueDate = model.ValueDate;

            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId)
                .SingleAsync();

            loanApplication.State = model.State;

            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
            await SaveChangesAsync();

            return borrowerAsset.Id;
        }

        public async Task<GiftAssetModel> GetGiftAsset(TenantModel tenant, int userId, int borrowerAssetId, int borrowerId, int loanApplicationId)
        {
            var giftAssetModel = await Uow.Repository<BorrowerAsset>()
                .Query(x => x.TenantId == tenant.Id && x.Id == borrowerAssetId &&
                            x.AssetType.AssetCategoryId == (int)Model.AssetCategory.GiftFunds
                            && x.AssetBorrowerBinders.Any(y => y.BorrowerId == borrowerId &&
                                                               y.Borrower.LoanApplicationId == loanApplicationId &&
                                                               y.Borrower.LoanApplication.UserId == userId))
                .Select(x => new GiftAssetModel
                {
                    Id = x.Id,
                    LoanApplicationId = loanApplicationId,
                    BorrowerId = borrowerId,
                    AssetTypeId = x.AssetTypeId ?? 0,
                    GiftSourceId = x.GiftSourceId.Value,
                    IsDeposited = x.IsDeposited,
                    Value = x.Value,
                    ValueDate = x.ValueDate,
                    State = x.AssetBorrowerBinders.First().Borrower.LoanApplication.State
                })
                .SingleAsync();

            return giftAssetModel;
        }

        public async Task<BorrowerAssetFinancialModelResponse> GetFinancialAsset(TenantModel tenant, int userId, int borrowerAssetId, int borrowerId, int loanApplicationId)
        {
            return await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerAsset>()
                .Query(x => x.TenantId == tenant.Id && x.Id == borrowerAssetId && x.AssetType.AssetCategoryId == (int)Model.AssetCategory.FinancialStatement && x.AssetBorrowerBinders
                .Any(y => y.BorrowerId == borrowerId && y.Borrower.LoanApplicationId == loanApplicationId && y.Borrower.LoanApplication.UserId == userId))
                .Select(x => new BorrowerAssetFinancialModelResponse()
                {
                    Id = x.Id,
                    AssetTypeId = x.AssetTypeId.Value,
                    AccountNumber = x.AccountNumber,
                    InstitutionName = x.InstitutionName,
                    Balance = x.Value,
                    LoanApplicationId = loanApplicationId
                })
                .SingleAsync();
        }

        public async Task<List<AssetTypeModel>> GetAssetsTypes(TenantModel tenant, int giftSourceId)
        {
            return await _dbFunctionService.UdfAssetType(tenant.Id)
                .Where(x => x.AssetCategoryId == (int)Model.AssetCategory.GiftFunds &&
                            x.AssetTypeGiftSourceBinders.Any(y => y.GiftSourceId == giftSourceId))
                .Select(x => new AssetTypeModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }
    }

}