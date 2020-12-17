export class TrashEndpoints {
    static GET = {
        trash: {
            list: (loanApplicationId: string) => `/api/DocManager/Trash/GetTrashDocuments?loanApplicationId=${loanApplicationId}`
        }
    };

    static POST = {
        moveCatFileToTrash: () => `/api/DocManager/Document/MoveFromCategoryToTrash`,
        moveWorkBenchFileToTrash: () => `/api/DocManager/Workbench/MoveFromWorkBenchToTrash`,
        moveTrashFileToWorkBench: () => `/api/DocManager/Trash/MoveFromTrashToWorkBench`
    };

    static PUT = {};

    static DELETE = {};
}
