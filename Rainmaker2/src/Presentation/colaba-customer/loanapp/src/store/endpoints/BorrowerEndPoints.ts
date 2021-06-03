export class BorrowerEndPoints {
    static GET = {
        getAllBorrower: (loanapplicationid: number) => `/api/loanapplication/borrower/GetAllBorrower?loanapplicationId=${loanapplicationid}`,
        getBorrowerInfo: (loanapplicationId: number, borrowerId: number) => `/api/loanapplication/borrower/GetBorrowerInfo?loanapplicationId=${loanapplicationId}&borrowerId=${borrowerId}`,
        populatePrimaryBorrower: () => `/api/loanapplication/borrower/populateprimaryborrowerinfo`,
        getReviewBorrowerInfoSection: (loanApplicationId:Number) => (`/api/loanapplication/loan/GetReviewBorrowerInfoSection?LoanApplicationId=${loanApplicationId}`),
        getAllConsentTypes: (borrowerId: number) => `/api/loanapplication/borrower/GetBorrowerConsent?BorrowerId=${borrowerId}`,
        getBorrowerAcceptedConsents: (loanApplicationId: number, borrowerId: number) => `/api/loanapplication/borrower/GetBorrowerAcceptedConsents?BorrowerId=${borrowerId}&LoanApplicationId=${loanApplicationId}`,
        getBorrowersForFirstReview: (loanapplicationid: number) => `/api/loanapplication/borrower/GetBorrowersForFirstReview?loanapplicationId=${loanapplicationid}`,
        getBorrowersForSecondReview: (loanapplicationid: number) => `/api/loanapplication/borrower/GetBorrowersForSecondReview?loanapplicationId=${loanapplicationid}`,
    };
    static POST = {
         addOrUpdateBorrowerInfo: () => `/api/loanapplication/borrower/Addorupdateborrowerinfo`,
         addOrUpdateBorrowerVaStatus: () => ('/api/loanapplication/va/AddOrUpdateBorrowerVaStatus'),
         addOrUpdateBorrowerConsent: () => `/api/loanapplication/borrower/AddOrUpdateBorrowerConsents`
    };
    static PUT = {
       
    };
    static DELETE = {
        deleteSecondaryBorrower: ()=>('/api/loanapplication/borrower/DeleteSecondaryBorrower')
    };
};

