export class DocumentsEndpoints {
  static GET = {
    pendingDocuments: (loanApplicationId: string) =>
      `/api/Documentmanagement/dashboard/GetPendingDocuments?loanApplicationId=${loanApplicationId}`,

    submittedDocuments: (loanApplicationId: string) =>
      `/api/Documentmanagement/dashboard/GetSubmittedDocuments?loanApplicationId=${loanApplicationId}`,

    viewDocuments: (
      id: string,
      requestId: string,
      docId: string,
      fileId: string
    ) =>
      `/api/documentmanagement/file/view?id=${id}&requestId=${requestId}& docId=${docId}&fileId=${fileId}`,
  };

  static POST = {
    submitDocuments: () => `/api/Documentmanagement/file/submit`,
  };

  static PUT = {
    finishDocument: () => `/api/Documentmanagement/file/done`,
  };

  static DELETE = {};
}
