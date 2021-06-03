export class LoanApplicationEndpoints {
    static GET = {
        getLoanApplicationFirstReview:(LoanApplicationId :number)=>(`/api/loanapplication/loan/GetLoanApplicationFirstReview?LoanApplicationId=${LoanApplicationId}`),
        getLoanApplicationSecondReview:(LoanApplicationId :number)=>(`/api/loanapplication/loan/GetLoanApplicationSecondReview?LoanApplicationId=${LoanApplicationId}`)
    }
}