using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocManager.Model;
using DocManager.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocManager.API.Controllers
{
    [Route("api/docmanager/[controller]")]
    [ApiController]
    [Authorize(Roles = "MCU")]
    public class DocumentController : Controller
    {
        private readonly IDocumentService documentService;
        public DocumentController(IDocumentService documentService)
        {
            this.documentService = documentService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> MoveFromCategoryToTrash(MoveFromCategoryToTrash  moveFromCategoryToTrash)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            return Ok(await documentService.MoveFromCategoryToTrash(moveFromCategoryToTrash, tenantId));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> MoveFromCategoryToWorkBench(MoveFromCategoryToWorkBench  moveFromCategoryToworkbench)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            return Ok(await documentService.MoveFromCategoryToWorkBench(moveFromCategoryToworkbench, tenantId));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> MoveFromoneCategoryToAnotherCategory(MoveFromOneCategoryToAnotherCategory moveFromoneCategoryToAnotherCategory )
        { 
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            return Ok(await documentService.MoveFromoneCategoryToAnotherCategory(moveFromoneCategoryToAnotherCategory, tenantId));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> ViewCategoryAnnotations(ViewCategoryAnnotations  viewCategoryAnnotations)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            return Ok(await documentService.ViewCategoryAnnotations(viewCategoryAnnotations, tenantId));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SaveCategoryAnnotations(SaveCategoryAnnotations  saveCategoryAnnotations)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            return Ok(await documentService.SaveCategoryAnnotations(saveCategoryAnnotations, tenantId));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete(DeleteModel deleteModel)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            return Ok(await documentService.Delete(deleteModel, tenantId));
        }
    }
}
