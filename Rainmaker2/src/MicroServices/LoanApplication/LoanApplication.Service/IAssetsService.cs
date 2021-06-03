using System.Collections.Generic;
using System.Threading.Tasks;
using LoanApplication.Model;
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.Service
{
    public interface IAssetsService : IServiceBase<LoanApplicationDb.Entity.Models.LoanApplication>
    {
        public Dictionary<int, string> ErrorMessages { get; }

        Task DeleteEarnestMoneyDeposit(TenantModel tenant, int userId, int loanApplicationId);
        public Task<int> AddOrUpdateAssestsNonRealState(TenantModel tenant, int userId,
            AddOrUpdateAssetModelNonRealState model);

        public Task<int> AddOrUpdateAssestsRealState(TenantModel tenant, int userId,
            AddOrUpdateAssetModelRealState model);

        public Task<int> AddOrUpdateBankAccount(TenantModel tenant, int userId, BorrowerAssetModelRequest model);
        public Task<int> AddOrUpdateBorrowerAsset(TenantModel tenant, int userId, AddOrUpdateBorrowerAssetModel model);
        public List<GetAllAssetCategoriesModel> GetAllAssetCategories(TenantModel tenant);
        public Task<List<GiftSourceModel>> GetAllGiftSources(TenantModel tenant);
        public List<GetAssetTypesByCategoryModel> GetAssetTypesByCategory(TenantModel tenant, int categoryId);

        public Task<BorrowerAssetModelResponse> GetBankAccount(TenantModel tenant, int userId, int borrowerAssetId,
            int borrowerId, int loanApplicationId);

        public Task<List<AssetTypeModel>> GetBankAccountType(TenantModel tenant);

        public Task<RetirementAccountModel> GetRetirementAccount(TenantModel tenant, int userId, int loanApplicationId,
            int borrowerId, int intborrowerAssetId);

        public Task<int> UpdateRetirementAccount(TenantModel tenant, int userId, RetirementAccountModel model);
        public Task<int> UpdateEarnestMoneyDeposit(TenantModel tenant, int userId, EarnestMoneyDepositModel model);

        Task<EarnestMoneyDepositModel> GetEarnestMoneyDeposit(TenantModel tenant, int userId, int loanApplicationId);
        Task<ProceedsFromNonRealState> GetFromLoanNonRealState(TenantModel tenant, int userId, int BorrowerAssetId, int AssetTypeId, int BorrowerId, int LoanApplicationId);
        Task<ProceedsFromRealState> GetFromLoanRealState(TenantModel tenant, int userId, int BorrowerAssetId, int AssetTypeId, int BorrowerId, int LoanApplicationId);
        Task<LoanApplicationBorrowersAssets> GetLoanApplicationBorrowersAssets(TenantModel tenant, int userId, int loanApplicationId);

        Task<BorrowerAssetsGetModel> GetBorrowerAssets(TenantModel tenant, int userId, int loanApplicationId,
            int borrowerId);

        Task<BorrowerAssetsGetModel> GetBorrowerAssetDetail(TenantModel tenant, int userId,
            BorrowerAssetDetailGetModel model);

        Task<int> AddOrUpdateGiftAsset(TenantModel tenant, int userId, GiftAssetModel model);
        Task<GiftAssetModel> GetGiftAsset(TenantModel tenant, int userId, int borrowerAssetId, int borrowerId, int loanApplicationId);
        Task<int> AddOrUpdateProceedsfromloan(TenantModel tenant, int userId, ProceedFromLoanModel model);

        Task<int> AddOrUpdateProceedsfromloanOther(TenantModel tenant, int userId, ProceedFromLoanOtherModel model);
        Task<ProceedsFromLoan> GetProceedsfromloan(TenantModel tenant, int userId, int BorrowerAssetId, int AssetTypeId, int BorrowerId, int LoanApplicationId);
        Task<List<AssetTypeFinancialModel>> GetAllFinancialAsset(TenantModel tenant);
        Task<int> AddOrUpdateFinancialAsset(TenantModel tenant, int userId, BorrowerAssetFinancialModelRequest model);
        Task<BorrowerAssetFinancialModelResponse> GetFinancialAsset(TenantModel tenant, int userId, int borrowerAssetId, int borrowerId, int loanApplicationId);

        Task<List<GetGiftSourceAssetsModel>> GetGiftSourceAssets(TenantModel tenant, int giftSourceId);
        List<GetCollateralAssetTypesModel> GetCollateralAssetTypes(TenantModel tenant);

        Task<List<AssetTypeModel>> GetAssetsTypes(TenantModel tenant, int giftSourceId);


        Task<GetAllAssetsForHomeScreenModel> GetAllAssetsForHomeScreen(TenantModel tenant,
                                                                       int userId,
                                                                       int loanApplicationId);


        Task<object> GetOtherAssetInfo(TenantModel tenant,
                                       int userId,
                                       int assetId);


        Task<object> AddOrUpdateOtherAssetInfo(TenantModel tenant,
                                               int userId,
                                               AddOrUpdateOtherAssetsInfoModel model);


        Task<GetBorrowerWithAssetsForReviewModel> GetBorrowerAssetsForReview(TenantModel tenant,
                                                                             int userId,
                                                                             int loanApplicationId);
        Task<int> DeleteAsset(TenantModel tenant, int userId, AssetDeleteModel model);

        
    }
}