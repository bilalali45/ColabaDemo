export class NeedListEndpoints {
  static GET = {
    loan: {
      info: (loanApplicationId: string) =>
        `/api/Rainmaker/admindashboard/getloaninfo?loanApplicationId=${loanApplicationId}`
    },
    documents: {
      submitted: (loanApplicationId: string, status: boolean) =>
        `/api/Documentmanagement/admindashboard/GetDocuments?loanApplicationId=${loanApplicationId}&pending=${status}`,
      view: (id: string, requestId: string, docId: string, fileId: string) =>
        `/api/documentmanagement/document/view?id=${id}&requestId=${requestId}&docId=${docId}&fileId=${fileId}`,
      files: (id: string, requestId: string, docId: string) =>
        `/api/documentmanagement/document/getfiles?id=${id}&requestId=${requestId}&docId=${docId}`,
      activityLogs: (id: string, typeIdOrDocName: string) =>
        `/api/Documentmanagement/Document/GetActivityLog?id=${id}&typeId=${typeIdOrDocName}`,
      activityLogsDoc: (id: string, docName: string) =>
        `/api/Documentmanagement/Document/GetActivityLog?id=${id}&docName=${docName}`,
      emailLogs: (id: string) =>
        `/api/Documentmanagement/Document/GetEmailLog?id=${id}`
    }
  };

  static POST = {
    documents: {
      renameMCU: () => '/api/documentmanagement/document/mcurename',
      accept: () => '/api/Documentmanagement/document/AcceptDocument',
      reject: () => '/api/Documentmanagement/document/RejectDocument'
    }
  };

  static PUT = {
    documents: {
      deleteDoc: () => `/api/documentmanagement/admindashboard/delete`
    }
  };

  static DELETE = {
    documents: (id: string, documentId: string) =>
      `/api/documentmanagement/template/deletedocument?id=${id}&documentId=${documentId}`
  };
}
