export class EmploymentEndpoints {
    static GET = {
        getEmployerInfo: (loanApplicationId: number, borrowerId: number, IncomeInfoId:number) => (`/api/loanapplication/income/GetEmploymentDetail?BorrowerId=${borrowerId}&IncomeInfoId=${IncomeInfoId}&LoanApplicationId=${loanApplicationId}`),
        getEmploymentOtherDefaultIncomeTypes: () => (`/api/loanapplication/income/GetEmploymentOtherDefaultIncomeTypes`)
    };
    static POST = {
        addOrUpdateCurrentEmploymentDetail: () => (`/api/loanapplication/income/AddOrUpdateCurrentEmploymentDetail`)
    };
    static PUT = {

    };
    static DELETE = {

    };
};

