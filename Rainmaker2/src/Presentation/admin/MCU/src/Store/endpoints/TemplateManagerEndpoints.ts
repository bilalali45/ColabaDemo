export class TemplateManagerEndpoints {

    static GET = {
        templates: (tenantId: string) => `/api/documentmanagement/template/gettemplates?tenantId=${tenantId}`,
        categoryDocuments: () => `/api/documentmanagement/template/GetCategoryDocument`,
        templateDocuments: (id: string) => `/api/Documentmanagement/template/getdocuments?id=${id}`
    }

    static POST = {
        insertTemplate: () => `api/documentmanagement/template/inserttemplate`,
        renameTemplate: () => `/api/documentmanagement/template/RenameTemplate`,
        addDocument: () => `/api/documentmanagement/template/adddocument`
    }

    static PUT = {
    }

    static DELETE = {
        templateById: (tenantId: string, templateId: string) => `/api/documentmanagement/template/deletetemplate?tenantId=${tenantId}&templateId=${templateId}`,
        deleteTemplateDocument: (tenantId: string, templateId: string, documentId: string) => `/api/documentmanagement/template/deletedocument?tenantId=${tenantId}&templateId=${templateId}&documentId=${documentId}`
    }
}