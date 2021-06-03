export class AssetsEndpoints  {
   static GET = {
    GetAllAssetCategories: () => `/api/loanapplication/Assets/GetAllAssetCategories`,
    GetAssetTypesByCategory: (categoryId: number) => `/api/loanapplication/Assets/GetAssetTypesByCategory?categoryId=${categoryId}`,
    GetEarnestMoneyDeposit: (loanApplicationId :number) => `/api/loanapplication/Assets/GetEarnestMoneyDeposit?loanApplicationId=${loanApplicationId}`,
    GetBorrowerAssetDetail: (LoanApplicationId: number, BorrowerId: number, BorrowerAssetId: number) => `/api/loanapplication/assets/GetBorrowerAssetDetail?LoanApplicationId=${LoanApplicationId}&BorrowerId=${BorrowerId}&BorrowerAssetId=${BorrowerAssetId}`,
    GetCollateralAssetTypes: () => `/api/loanapplication/assets/GetCollateralAssetTypes`,
    GetRetirementAccount: (LoanApplicationId: number, BorrowerId: number, BorrowerAssetId: number) => `/api/loanapplication/Assets/GetRetirementAccount?loanApplicationId=${LoanApplicationId}&borrowerId=${BorrowerId}&borrowerAssetId=${BorrowerAssetId}`,
    GetAllFinancialAsset: () => `/api/loanapplication/Assets/GetAllFinancialAsset`,
    GetFinancialAsset: (LoanApplicationId: number, BorrowerId: number, BorrowerAssetId: number) => `/api/loanapplication/Assets/GetFinancialAsset?borrowerAssetId=${BorrowerAssetId}&borrowerId=${BorrowerId}&loanApplicationId=${LoanApplicationId}`
   }

   static POST = {
    UpdateEarnestMoneyDeposit: () => `/api/loanapplication/Assets/UpdateEarnestMoneyDeposit`,
    AddOrUpdateBorrowerAsset: () => `/api/loanapplication/assets/AddOrUpdateBorrowerAsset`,
    UpdateRetirementAccount: () => `/api/loanapplication/Assets/UpdateRetirementAccount`,
    AddOrUpdateFinancialAsset: () => `/api/loanapplication/Assets/AddOrUpdateFinancialAsset`
   }

    static PUT = {

    }

    static DELETE = {

    }
}