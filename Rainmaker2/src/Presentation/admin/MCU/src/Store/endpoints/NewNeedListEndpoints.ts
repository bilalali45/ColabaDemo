export class NewNeedListEndpoints {
    static GET = {
        isDraft: (loanApplicationId: string) => `/api/Documentmanagement/AdminDashboard/IsDocumentDraft?loanApplicationId=${loanApplicationId}`,
        getDraft: (loanApplicationId: string, tenantId: string) => `/api/Documentmanagement/request/GetDraft?loanApplicationId=${loanApplicationId}&tenantId=${tenantId}`


    };
    static POST = {
        save: (isDraft: boolean) => `/api/Documentmanagement/request/Save?isDraft=${isDraft}`,
        saveAsTemplate: () => `/api/Documentmanagement/template/SaveTemplate`,
        getByTemplateIds: () => `/api/Documentmanagement/document/GetDocumentsByTemplateIds`
    };
    static PUT = {};
    static DELETE = {};
}