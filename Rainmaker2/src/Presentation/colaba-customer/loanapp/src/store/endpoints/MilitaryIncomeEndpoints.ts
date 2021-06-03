export class MilitaryIncomeEndpoints  {
    static GET = {
        GetMilitaryIncome: (borrowerId: number, incomeInfoId: number) => `/api/loanapplication/income/GetMilitaryIncome?borrowerId=${borrowerId}&incomeInfoId=${incomeInfoId}`
    }
 
    static POST = {
        AddOrUpdateMilitaryIncome: () => `/api/loanapplication/income/AddOrUpdateMilitaryIncome`
    }
 
     static PUT = {
 
     }
 
     static DELETE = {
 
     }
 }