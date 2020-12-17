export class WorkbenchEndpoints {
    static GET = {
        list:(loanApplicationId: string) => `/api/docmanager/workbench/GetWorkbenchDocuments?loanApplicationId=${loanApplicationId}`
        
    };
  
    static POST = {
        moveToTrash: () => `/api/DocManager/Workbench/MoveFromWorkBenchToTrash`
    };
  
    static PUT = {};
  
    static DELETE = {};
  }