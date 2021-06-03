export class IncomeEndpoints  {
   static GET = {
    GetSourceOfIncomeList: () => `/api/loanapplication/income/GetAllIncomeCategories`,
    GetAllIncomeGroupsWithOtherIncomeTypes: () => `/api/loanapplication/income/GetAllIncomeGroupsWithOtherIncomeTypes`,
    GetOtherIncomeInfo: (incomeInfoId: number) => `/api/loanapplication/income/GetOtherIncomeInfo?incomeInfoId=${incomeInfoId}`,
    GetIncomeSectionReview: (loanApplicationId: number) => `/api/loanapplication/income/GetIncomeSectionReview?LoanApplicationId=${loanApplicationId}`,
    GetSelfBusinessIncome: (borrowerId : number, incomeInfoId: number) => `/api/loanapplication/income/GetSelfBusinessIncome?borrowerId=${borrowerId}&incomeInfoId=${incomeInfoId}`,
    GetBorrowerIncomesForReview: (loanApplicationId: number) => `/api/loanapplication/income/GetBorrowerIncomesForReview?LoanApplicationId=${loanApplicationId}`,
   }

   static POST = {
    AddOrUpdateOtherIncome: () => `/api/loanapplication/income/AddOrUpdateOtherIncome`,
    AddOrUpdateSelfBusiness: () => `/api/loanapplication/income/AddOrUpdateSelfBusiness`,
    
   }

    static PUT = {

    }

    static DELETE = {

    }
}