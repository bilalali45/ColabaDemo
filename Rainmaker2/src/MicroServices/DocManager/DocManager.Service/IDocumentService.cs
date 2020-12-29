using DocManager.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocManager.Service
{
    public interface IDocumentService
    {
        Task<bool> MoveFromCategoryToTrash(MoveFromCategoryToTrash  moveFromCategoryToTrash, int tenantId);
        Task<bool> MoveFromCategoryToWorkBench(MoveFromCategoryToWorkBench moveFromCategoryToworkbench, int tenantId);
        Task<bool> MoveFromoneCategoryToAnotherCategory(MoveFromOneCategoryToAnotherCategory moveFromoneCategoryToAnotherCategory, int tenantId);
        Task<string> ViewCategoryAnnotations(ViewCategoryAnnotations viewCategoryAnnotations, int tenantId);
        Task<bool> SaveCategoryAnnotations(SaveCategoryAnnotations saveCategoryAnnotations, int tenantId);
        Task<bool> Delete(DeleteModel deleteModel, int tenantId);
        Task<bool> DeleteCategoryFile(string id, int tenantid, string fromRequestId, string fromDocId, string fromFileId);
        Task<bool> DeleteCategoryMcuFile(string id, int tenantid, string fromRequestId, string fromDocId, string fromFileId);
    }
}
