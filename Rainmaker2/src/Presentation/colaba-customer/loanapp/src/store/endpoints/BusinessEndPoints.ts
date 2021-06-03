export class BusinessEndPoints {
    static GET = {
        GetMyMoneyHomeScreen: (loanApplicationId: number) => `/api/loanapplication/income/GetMyMoneyHomeScreen?LoanApplicationId=${loanApplicationId}`,
        GetMyAssetsHomeScreen: (loanApplicationId: number) => `/api/loanapplication/assets/GetAssetsHomeScreen?LoanApplicationId=${loanApplicationId}`,
        GetAllBusinessTypes: () => `/api/loanapplication/income/GetAllBusinessTypes`,
        GetBusinessIncome: (borrowerId: number, incomeInfoId: number) => `/api/loanapplication/income/GetBusinessIncome?borrowerId=${borrowerId}&incomeInfoId=${incomeInfoId}`,
    };
    static POST = {
        AddOrUpdateBusiness: () => `/api/loanapplication/income/AddOrUpdateBusiness`
    };
    static PUT = {

    };
    static DELETE = {
        DeleteEmploymentIncome: (loanApplicationId: number, borrowerId: number, incomeInfoId: number) => `/api/loanapplication/income/DeleteEmploymentIncome?BorrowerId=${borrowerId}&IncomeInfoId=${incomeInfoId}&LoanApplicationId=${loanApplicationId}`,
        DeleteAsset: (loanApplicationId: number, borrowerId: number, assetId: number) => `/api/loanapplication/assets/DeleteAsset?BorrowerId=${borrowerId}&AssetId=${assetId}&LoanApplicationId=${loanApplicationId}`
    };
};

