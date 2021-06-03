export class DobSSNEndpoints {
    static GET = {
        getBorrowerDobSsn: (loanApplicationId:number, borrowerId:number) => (`/api/loanapplication/Borrower/GetBorrowerDobSsn?LoanApplicationId=${loanApplicationId}&BorrowerId=${borrowerId}`),
    };
    static POST = {
        addOrUpdateInfo: () => (`/api/loanapplication/borrower/AddOrUpdateDobSsn`)
    };
    static PUT = {
       
    };
    static DELETE = {
        
    };
};

