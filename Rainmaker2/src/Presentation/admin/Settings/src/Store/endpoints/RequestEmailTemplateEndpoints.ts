export class RequestEmailTemplateEndpoints {
    
    static GET = {
        emailTemplates: () => `/api/Documentmanagement/EmailTemplate/GetEmailTemplates`,
        tokens: () => `/api/Documentmanagement/EmailTemplate/GetTokens`
    }

    static POST ={
        insertEmailTemplate: () => `/api/Documentmanagement/EmailTemplate/InsertEmailTemplate`,
        updateSortOrder: () => `/api/Documentmanagement/EmailTemplate/UpdateSortOrder`,
        updateEmailTemplate: () => `/api/Documentmanagement/EmailTemplate/UpdateEmailTemplate`
    }

    static DELETE = {
        deleteEmailTemplate: () => `/api/Documentmanagement/EmailTemplate/DeleteEmailTemplate`
    }
}