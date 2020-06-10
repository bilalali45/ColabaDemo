export class LoanEndpoints {
    static GET = {
        status: (params: any) => `/api/getloanStatus/${params}`
    }

    static POST = {

    }

    static PUT = {

    }
    
    static DELETE = {
        deleteLoanApplication: (id: any) => `/api/deleteLoanApplication/${id}`
    }
}