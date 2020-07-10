export class TemplateManagerEndpoints {

    static GET = {
        templates: (tenantId: string) => `/api/documentmanagement/template/gettemplates?tenantId=${tenantId}`,
        categoryDocuments: () => `/api/documentmanagement/template/GetCategoryDocument`,
        templateDocuments: (id: string) => `/api/Documentmanagement/template/getdocuments?id=${id}`
    }

    static POST = {
        insertTemplate: () => `api/documentmanagement/template/inserttemplate`,
        renameTemplate: (tenantId: string, id: string, name: string) => `/api/documentmanagement/template/RenameTemplate?tenantId=${tenantId}&id=${id}&name=${name}`
    }

    static PUT = {
    }

    static DELETE = {
        templateById: (tenantId: string, templateId: string) => `/api/documentmanagement/template/deletetemplate?tenantId=${tenantId}&templateId=${templateId}`
    }
}