export class DocumentsEndpoints {

    static GET = {
        pendingDocuments: (loanApplicationId: string, tenentId: string) => `/api/Documentmanagement/dashboard/GetPendingDocuments?loanApplicationId=${loanApplicationId}&tenantId=${tenentId}`,

        submittedDocuments: (loanApplicationId: string, tenentId: string) => `/api/Documentmanagement/dashboard/GetSubmittedDocuments?=${loanApplicationId}&tenantId=${tenentId}`,

        documentsProgress: (loanApplicationId: string, tenentId: string) => `/api/Documentmanagement/dashboard/GetDashboardStatus?loanApplicationId=${loanApplicationId}&tenantId=${tenentId}`
    }

    static POST = {

    }

    static PUT = {

    }

    static DELETE = {

    }
}