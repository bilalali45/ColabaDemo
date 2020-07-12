export class LoanEndpoints{
    static GET = {
        loanInfo: (loanApplicationId: string) => `/api/Rainmaker/admindashboard/getloaninfo?loanApplicationId=${Number(loanApplicationId)}`

    }
    static POST = {

    }

    static PUT = {

    }
    static DELETE = {

    }
}