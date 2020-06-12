export class LoanEndpoints {
    static GET = {
        officer: (loanApplicationId: string, businessUnitId: string) => `/api/Rainmaker/LoanApplication/GetLOInfo?loanApplicationId=${loanApplicationId}&businessUnitId=${businessUnitId}`,
        info: (loanApplicationId: string) => `/api/Rainmaker/LoanApplication/GetLoanInfo?loanApplicationId=${loanApplicationId}`,
        getLOPhoto: (lOPhotoId?: string, businessUnitId?: string) => `/api/Rainmaker/LoanApplication/GetPhoto?photo=${lOPhotoId}&businessUnitId=${businessUnitId}`
    }

    static POST = {

    }

    static PUT = {

    }
    
    static DELETE = {
        // deleteLoanApplication: (id: any) => `/api/deleteLoanApplication/${id}`
    }
}