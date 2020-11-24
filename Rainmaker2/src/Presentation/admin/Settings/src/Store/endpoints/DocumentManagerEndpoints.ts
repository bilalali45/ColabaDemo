export class DocumentManagerEndpoints {
    static GET = {
      templates: {
        all: () => `/api/documentmanagement/template/gettemplates`,
      },
      documents: {
        submitted: (loanApplicationId: string) =>
          `/api/Documentmanagement/admindashboard/GetDocuments?loanApplicationId=${loanApplicationId}&pending=false`,
        isDocumentDraft: (id: string) =>
          `/api/Documentmanagement/AdminDashboard/IsDocumentDraft?loanApplicationId=${id}`,
      },
    };
  
    static POST = {};
  
    static PUT = {};
  
    static DELETE = {
      template: (templateId: string) =>
        `/api/documentmanagement/template/deletetemplate?templateId=${templateId}`,
    };
  }
  