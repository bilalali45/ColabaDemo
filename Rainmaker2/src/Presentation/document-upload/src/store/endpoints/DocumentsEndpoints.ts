export class DocumentsEndpoints {

    static GET = {
        pendingDocuments: (loanApplicationId: string, tenentId: string) => `/api/Documentmanagement/dashboard/GetPendingDocuments?loanApplicationId=${loanApplicationId}&tenantId=${tenentId}`,
        
        submittedDocuments: (loanApplicationId: string, tenentId: string) => `/api/Documentmanagement/dashboard/GetSubmittedDocuments?loanApplicationId=${loanApplicationId}&tenantId=${tenentId}`,

        viewDocuments: (id: string, requestId: string, docId: string, fileId: string, tenantId: string ) => `/api/documentmanagement/file/view?id=${id}&requestId=${requestId}& docId=${docId}&fileId=${fileId}&tenantId=${tenantId}`

    }

    static POST = {
        submitDocuments: () => `/api/Documentmanagement/file/submit`
    }

    static PUT = {
        finishDocument: () => `/api/Documentmanagement/file/done`
    }

    static DELETE = {

    }
}   