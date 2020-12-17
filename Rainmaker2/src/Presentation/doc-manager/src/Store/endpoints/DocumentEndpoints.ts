export class DocumentEndpoints {
 
    static GET = {
        all: (loanApplicationId: string) =>  `/api/Documentmanagement/admindashboard/GetDocuments?loanApplicationId=${loanApplicationId}&pending=false`,
        categoryDocuments: () => `/api/documentmanagement/template/GetCategoryDocument`,
        viewDocument: (id:string, requestId:string, docId: string, fileId:string) => `/api/documentmanagement/document/view?id=${id}&requestId=${requestId}&docId=${docId}&fileId=${fileId}`,
    }
  
    static POST = {
        docCategory: () => `/api/DocManager/request/save`,
        filesAddedToCategory: ()=> `/api/DocManager/Request/submit`,
        rename:()=>`/api/documentmanagement/document/mcurename`,
        reassignDoc: ()=> `/api/DocManager/Document/MoveFromoneCategoryToAnotherCategory`,
        delDocCategory: ()=> `/api/DocManager/Document/Delete`,
        moveFromCategoryToWorkBench: ()=> `/api/DocManager/Document/MoveFromCategoryToWorkBench`,
        moveFromWorkBenchToCategory: ()=> `/api/DocManager/Workbench/MoveFromWorkBenchToCategory`,
        saveCategoryAnnotations: () => `/api/DocManager/Document/SaveCategoryAnnotations`,
        saveTrashAnnotations: () => `/api/DocManager/Trash/SaveTrashAnnotations`,
        saveWorkbenchAnnotations: () => `/api/DocManager/Workbench/SaveWorkbenchAnnotations`,
        ViewCategoryAnnotations: () => `/api/DocManager/Document/ViewCategoryAnnotations`,
        viewWorkbenchAnnotations: () => `/api/DocManager/Workbench/ViewWorkbenchAnnotations`,
        saveCategoryDocument: ()=> `/api/DocManager/Thumbnail/SaveCategoryDocument`,
        saveTrashDocument: ()=> `/api/DocManager/Thumbnail/SaveTrashDocument`,
        saveWorkbenchDocument: ()=> `/api/DocManager/Thumbnail/SaveWorkbenchDocument`,
        syncToLOS: () => `/api/LosIntegration/Document/SendFileToExternalOriginator`,
    }
  
    static PUT = { 
    }
  
    static DELETE = {
        
    }
  }
