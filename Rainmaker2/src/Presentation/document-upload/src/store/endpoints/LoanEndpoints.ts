export class LoanEndpoints {
    static GET = {
        officer: (loanApplicationId: string, businessUnitId: string) => `/api/Rainmaker/LoanApplication/GetLOInfo?loanApplicationId=${loanApplicationId}&businessUnitId=${businessUnitId}`,
        info: (loanApplicationId: string) => `/api/Rainmaker/LoanApplication/GetLoanInfo?loanApplicationId=5976`,
        // info: (loanApplicationId: string) => `/api/Rainmaker/LoanApplication/GetLoanInfo?loanApplicationId=${Number(loanApplicationId)}`,
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