export class DocumentEndpoints {
 
    static GET = {
        all: (loanApplicationId: string) =>  `/api/Documentmanagement/admindashboard/GetDocuments?loanApplicationId=${loanApplicationId}&pending=false`,
        categoryDocuments: () => `/api/documentmanagement/template/GetCategoryDocument`,
        viewDocument: (id:string, requestId:string, docId: string, fileId:string) => `/api/documentmanagement/document/view?id=${id}&requestId=${requestId}&docId=${docId}&fileId=${fileId}`,
        checkIsByteProAuto: () =>`/api/documentmanagement/setting/GetTenantSetting`,
        loanInfo: (loanApplicationId: string) => `/api/Rainmaker/admindashboard/getloaninfo?loanApplicationId=${loanApplicationId}`,
    }
  
    static POST = {
        docCategory: () => `/api/DocManager/request/save`,
        filesAddedToCategory: ()=> `/api/DocManager/Request/submit`,
        rename:()=>`/api/documentmanagement/document/mcurename`,
        reassignDoc: ()=> `/api/DocManager/Document/MoveFromoneCategoryToAnotherCategory`,
        delDocCategory: ()=> `/api/documentmanagement/admindashboard/delete`,
        moveFromCategoryToWorkBench: ()=> `/api/DocManager/Document/MoveFromCategoryToWorkBench`,
        moveFromWorkBenchToCategory: ()=> `/api/DocManager/Workbench/MoveFromWorkBenchToCategory`,
        moveFromTrashToCategory: ()=> `/api/DocManager/Trash/MoveFromTrashToCategory`,
        saveCategoryAnnotations: () => `/api/DocManager/Document/SaveCategoryAnnotations`,
        saveTrashAnnotations: () => `/api/DocManager/Trash/SaveTrashAnnotations`,
        saveWorkbenchAnnotations: () => `/api/DocManager/Workbench/SaveWorkbenchAnnotations`,
        ViewCategoryAnnotations: () => `/api/DocManager/Document/ViewCategoryAnnotations`,
        viewWorkbenchAnnotations: () => `/api/DocManager/Workbench/ViewWorkbenchAnnotations`,
        viewTrashhAnnotations: () => `/api/DocManager/Trash/ViewTrashAnnotations`,
        saveCategoryDocument: ()=> `/api/DocManager/Thumbnail/SaveCategoryDocument`,
        saveTrashDocument: ()=> `/api/DocManager/Thumbnail/SaveTrashDocument`,
        saveWorkbenchDocument: ()=> `/api/DocManager/Thumbnail/SaveWorkbenchDocument`,
        syncToLOS: () => `/api/LosIntegration/Document/SendFileToExternalOriginator`,
        DeleteCategoryFile: () => `/api/DocManager/Document/DeleteCategoryFile`,
        DeleteTrashFile: () => `/api/DocManager/Trash/DeleteTrashFile`,
        DeleteWorkbenchFile: () => `/api/DocManager/Workbench/DeleteWorkbenchFile`,
    }
  
    static PUT = { 
    }
  
    static DELETE = {
        
    }
  }
