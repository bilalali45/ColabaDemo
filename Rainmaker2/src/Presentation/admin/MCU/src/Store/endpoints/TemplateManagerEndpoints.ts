export class TemplateManagerEndpoints {
  static GET = {
    templates: () => `/api/documentmanagement/template/gettemplates`,
    categoryDocuments: () =>
      `/api/documentmanagement/template/GetCategoryDocument`,
    templateDocuments: (id: string) =>
      `/api/Documentmanagement/template/getdocuments?id=${id}`,
  };

  static GET = {
    templates: (tenantId: string) =>
      `/api/documentmanagement/template/gettemplates?tenantId=${tenantId}`,
    categoryDocuments: () =>
      `/api/documentmanagement/template/GetCategoryDocument`,
    templateDocuments: (id: string) =>
      `/api/Documentmanagement/template/getdocuments?id=${id}`,
    getEmailTemplate: (tenantId: string) =>
      `/api/Documentmanagement/request/GetEmailTemplate?=${tenantId}`,
  };

  static PUT = {};

  static DELETE = {
    template: () => `/api/documentmanagement/template/deletetemplate`,
    deleteTemplateDocument: () =>
      `/api/documentmanagement/template/deletedocument`,
  };
}
