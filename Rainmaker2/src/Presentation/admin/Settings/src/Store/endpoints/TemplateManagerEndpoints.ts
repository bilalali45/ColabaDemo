export class TemplateManagerEndpoints {
 
    static GET = {
        templates: () => `/api/documentmanagement/template/gettemplates`,
        categoryDocuments: () => `/api/documentmanagement/template/GetCategoryDocument`,
        templateDocuments: (id: string) => `/api/Documentmanagement/template/getdocuments?id=${id}`,
        getEmailTemplate: () => `/api/Documentmanagement/request/GetEmailTemplate`
    }
  
    static POST = {
        insertTemplate: () => `/api/documentmanagement/template/inserttemplate`,
        renameTemplate: () => `/api/documentmanagement/template/RenameTemplate`,
        addDocument: () => `/api/documentmanagement/template/adddocument`,
        
        insertTenantTemplate: () => `/api/Documentmanagement/template/InsertTenantTemplate`,
        renameTenantTemplate: () => `/api/Documentmanagement/template/RenameTenantTemplate`,
        addTenantDocument: () => `/api/Documentmanagement/template/AddTenantDocument`,
    }
  
    static PUT = {
    }
  
    static DELETE = {
        template: () => `/api/documentmanagement/template/deletetemplate`,
        deleteTemplateDocument: () => `/api/documentmanagement/template/deletedocument`,

        tenantTemplate: () => `/api/Documentmanagement/template/DeleteTenantTemplate`,
        deleteTenantTemplateDocument: () => `/api/Documentmanagement/template/DeleteTenantDocument`
    }
  }