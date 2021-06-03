export class TransactionProceedsEndpoints  {
   static GET = {
       GetFromLoanRealState: (BorrowerAssetId: number, AssetTypeId: number, BorrowerId: number, LoanApplicationId :number) => `/api/loanapplication/Assets/GetFromLoanRealState?BorrowerAssetId=${BorrowerAssetId}&AssetTypeId=${AssetTypeId}&BorrowerId=${BorrowerId}&LoanApplicationId=${LoanApplicationId}`,
       GetFromLoanNonRealState: (BorrowerAssetId: number, AssetTypeId: number, BorrowerId: number, LoanApplicationId :number) => `/api/loanapplication/Assets/GetFromLoanNonRealState?BorrowerAssetId=${BorrowerAssetId}&AssetTypeId=${AssetTypeId}&BorrowerId=${BorrowerId}&LoanApplicationId=${LoanApplicationId}`,
       GetProceedsfromloan: (BorrowerAssetId: number, AssetTypeId: number, BorrowerId: number, LoanApplicationId :number) => `/api/loanapplication/Assets/GetProceedsfromloan?BorrowerAssetId=${BorrowerAssetId}&AssetTypeId=${AssetTypeId}&BorrowerId=${BorrowerId}&LoanApplicationId=${LoanApplicationId}`
   }

   static POST = {
    AddOrUpdateAssestsRealState: () => `/api/loanapplication/Assets/AddOrUpdateAssestsRealState`,
    AddOrUpdateAssestsNonRealState: () => `/api/loanapplication/Assets/AddOrUpdateAssestsNonRealState`,
    AddOrUpdateProceedsfromloan: () => `/api/loanapplication/Assets/AddOrUpdateProceedsfromloan`,
    AddOrUpdateProceedsfromloanOther: () => `/api/loanapplication/Assets/AddOrUpdateProceedsfromloanOther`
   }

    static PUT = {

    }

    static DELETE = {

    }
}
