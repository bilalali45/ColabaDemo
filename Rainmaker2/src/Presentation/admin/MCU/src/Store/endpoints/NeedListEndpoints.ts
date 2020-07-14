export class NeedListEndpoints {
    static GET = {
        loan: {
            info: (loanApplicationId: string) => `/api/Rainmaker/admindashboard/getloaninfo?loanApplicationId=${loanApplicationId}`
        },
        documents: {
            submitted: (loanApplicationId: string, tenentId: string, status: boolean) => `/api/Documentmanagement/admindashboard/GetDocuments?loanApplicationId=${loanApplicationId}&tenantId=${tenentId}&pending=${status}`
        }
    }

    static POST = {
        
    }
    
    static PUT = {

    }

    static DELETE = {
        documents: (id: string, tenantId: string, documentId: string) => `/api/documentmanagement/template/deletedocument?id=${id}&tenantId=${tenantId}&documentId=${documentId}`
    }
}