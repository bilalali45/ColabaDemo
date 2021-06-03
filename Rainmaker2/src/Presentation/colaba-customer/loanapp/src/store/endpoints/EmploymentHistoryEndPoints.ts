export class EmploymentHistoryEndpoints {
    static GET = {
        getBorrowersEmploymentHistory: (loanApplicationId: number) => (`/api/loanapplication/income/GetBorrowersEmploymentHistory?LoanApplicationId=${loanApplicationId}`),
        getBorrowerIncomes:(loanApplicationId:number, borrowerId:number) => (`/api/loanapplication/income/GetBorrowerIncomes?LoanApplicationId=${loanApplicationId}&BorrowerId=${borrowerId}`)
    };
    static POST = {
        addOrUpdatePreviousEmploymentDetail: () => (`/api/loanapplication/income/AddOrUpdatePreviousEmploymentDetail`)
    };
    static PUT = {

    };
    static DELETE = {

    };
};

