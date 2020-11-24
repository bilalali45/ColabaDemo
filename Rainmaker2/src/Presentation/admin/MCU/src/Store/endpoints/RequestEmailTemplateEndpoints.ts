export class RequestEmailTemplateEndpoints {
    
    static GET = {
        emailTemplates: () => `/api/Documentmanagement/EmailTemplate/GetEmailTemplates`,
        GetDraftEmailTemplateById: (id?: string, loanApplicationId?: string) => `/api/Documentmanagement/EmailTemplate/GetRenderEmailTemplateById?id=${id}&loanApplicationId=${loanApplicationId}`,
        tokens: () => `/api/Setting/EmailTemplate/GetTokens`
    }

    static POST ={
        insertEmailTemplate: () => `/api/Setting/EmailTemplate/InsertEmailTemplate`,
        updateSortOrder: () => `/api/Setting/EmailTemplate/UpdateSortOrder`
    }

    static DELETE = {
        deleteEmailTemplate: () => `/api/Setting/EmailTemplate/DeleteEmailTemplate`
    }
}