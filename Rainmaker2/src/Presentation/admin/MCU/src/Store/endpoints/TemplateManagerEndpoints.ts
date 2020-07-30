export class TemplateManagerEndpoints {

    static GET = {
        templates: (tenantId: string) => `/api/documentmanagement/template/gettemplates?tenantId=${tenantId}`,
        categoryDocuments: () => `/api/documentmanagement/template/GetCategoryDocument`,
        templateDocuments: (id: string) => `/api/Documentmanagement/template/getdocuments?id=${id}`
    }

    static POST = {
        insertTemplate: () => `/api/documentmanagement/template/inserttemplate`,
        renameTemplate: () => `/api/documentmanagement/template/RenameTemplate`,
        addDocument: () => `/api/documentmanagement/template/adddocument`,
    }

    static PUT = {
    }

    static DELETE = {
        template: () => `/api/documentmanagement/template/deletetemplate`,
        deleteTemplateDocument: () => `/api/documentmanagement/template/deletedocument`
    }
}