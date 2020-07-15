export class NeedListEndpoints {
  static GET = {
    loan: {
      info: (loanApplicationId: string) =>
        `/api/Rainmaker/admindashboard/getloaninfo?loanApplicationId=${loanApplicationId}`,
    },
    documents: {
      submitted: (
        loanApplicationId: string,
        tenentId: string,
        status: boolean
      ) =>
        `/api/Documentmanagement/admindashboard/GetDocuments?loanApplicationId=${loanApplicationId}&tenantId=${tenentId}&pending=${status}`,
      view: (
        id: string,
        requestId: string,
        docId: string,
        fileId: string,
        tenantId: string
      ) =>
        `/api/documentmanagement/document/view?id=${id}&requestId=${requestId}&docId=${docId}&fileId=${fileId}&tenantId=${tenantId}`,
      files: (
        id: string,
        requestId: string,
        docId: string
      ) =>
        `/api/documentmanagement/document/getfiles?id=${id}&requestId=${requestId}&docId=${docId}`
    },
  };

  static POST = {
    documents: {
      renameMCU: () => '/api/documentmanagement/document/mcurename'
    }
  };

  static PUT = {};

  static DELETE = {
    documents: (id: string, tenantId: string, documentId: string) =>
      `/api/documentmanagement/template/deletedocument?id=${id}&tenantId=${tenantId}&documentId=${documentId}`,
  };
}
