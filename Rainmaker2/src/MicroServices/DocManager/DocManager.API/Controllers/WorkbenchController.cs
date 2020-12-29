using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocManager.Model;
using DocManager.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocManager.API.Controllers
{
    [Route("api/DocManager/[controller]")]
    [ApiController]
    [Authorize(Roles ="MCU")]
    public class WorkbenchController : Controller
    {
        private readonly IWorkbenchService workbenchService;
        public WorkbenchController(IWorkbenchService workbenchService)
        {
            this.workbenchService = workbenchService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetWorkbenchDocuments(int loanApplicationId)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            return Ok(await workbenchService.GetWorkbenchFiles(loanApplicationId,tenantId));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> MoveFromWorkBenchToTrash(MoveFromWorkBenchToTrash moveFromWorkBenchToTrash)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            return Ok(await workbenchService.MoveFromWorkBenchToTrash(moveFromWorkBenchToTrash, tenantId));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> MoveFromWorkBenchToCategory(MoveFromWorkBenchToCategory  moveFromWorkBenchToCategory)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            return Ok(await workbenchService.MoveFromWorkBenchToCategory(moveFromWorkBenchToCategory, tenantId));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> ViewWorkbenchAnnotations(ViewWorkbenchAnnotations  viewWorkbenchAnnotations)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            return Ok(await workbenchService.ViewWorkbenchAnnotations(viewWorkbenchAnnotations, tenantId));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> SaveWorkbenchAnnotations(SaveWorkbenchAnnotations  saveWorkbenchAnnotations)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            return Ok(await workbenchService.SaveWorkbenchAnnotations(saveWorkbenchAnnotations, tenantId));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteWorkbenchFile(DeleteTrashFile deleteModel)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var res = await workbenchService.DeleteWorkbenchFile(deleteModel.id, tenantId, deleteModel.fileId);

            return Ok(res);
        }
    }
}
