export class DocumentManagerEndpoints {
    static GET = {
        templates: {
            all: (tenantId: string) => `/api/documentmanagement/template/gettemplates?tenantId=${tenantId}`
        },
        documents: {
            submitted: (loanApplicationId: string, tenentId: string) => `/api/Documentmanagement/admindashboard/GetDocuments?loanApplicationId=${loanApplicationId}&tenantId=${tenentId}&pending=false`,
            isDocumentDraft: (id: string) => `/api/Documentmanagement/AdminDashboard/IsDocumentDraft?loanApplicationId=${id}`
        }
    }

    static POST = {
        
    }
    
    static PUT = {

    }

    static DELETE = {
        template: (tenantId: string, templateId: string) => `/api/documentmanagement/template/deletetemplate?tenantId=${tenantId}&templateId=${templateId}`
    }
}