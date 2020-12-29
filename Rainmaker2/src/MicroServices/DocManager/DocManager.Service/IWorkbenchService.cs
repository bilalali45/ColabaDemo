using DocManager.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocManager.Service
{
    public interface IWorkbenchService
    {
        Task<List<WorkbenchFile>> GetWorkbenchFiles(int loanApplicationId, int tenantId);
        Task<bool> MoveFromWorkBenchToTrash(MoveFromWorkBenchToTrash moveFromWorkBenchToTrash, int tenantId);
        Task<bool> MoveFromWorkBenchToCategory(MoveFromWorkBenchToCategory moveFromWorkBenchToCategory, int tenantId);
        Task<string> ViewWorkbenchAnnotations(ViewWorkbenchAnnotations  viewWorkbenchAnnotations, int tenantId);
        Task<bool> SaveWorkbenchAnnotations(SaveWorkbenchAnnotations  saveWorkbenchAnnotations, int tenantId);
        Task<bool> DeleteWorkbenchFile(string id, int tenantid, string fromFileId);
    }
}
