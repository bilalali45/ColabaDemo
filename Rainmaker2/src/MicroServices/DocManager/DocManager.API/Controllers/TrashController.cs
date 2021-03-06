using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocManager.Model;
using DocManager.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DocManager.API.Controllers
{
    [Route("api/docmanager/[controller]")]
    [ApiController]
    [Authorize(Roles ="MCU")]
    public class TrashController : Controller
    {
        private readonly ITrashService trashService;
        public TrashController(ITrashService trashService)
        {
            this.trashService = trashService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTrashDocuments(int loanApplicationId)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            return Ok(await trashService.GetTrashFiles(loanApplicationId, tenantId));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> MoveFromTrashToWorkBench(MoveFromTrashToWorkBench moveFromTrashToWorkBench)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            return Ok(await trashService.MoveFromTrashToWorkBench(moveFromTrashToWorkBench, tenantId));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> MoveFromTrashToCategory(MoveFromTrashToCategory moveFromTrashToCategory)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            return Ok(await trashService.MoveFromTrashToCategory(moveFromTrashToCategory, tenantId));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> ViewTrashAnnotations(ViewTrashAnnotations viewTrashAnnotations)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            return Ok(await trashService.ViewTrashAnnotations(viewTrashAnnotations, tenantId));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> SaveTrashAnnotations(SaveTrashAnnotations  saveTrashAnnotations)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            return Ok(await trashService.SaveTrashAnnotations(saveTrashAnnotations, tenantId));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteTrashFile(DeleteTrashFile deleteModel)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var res = await trashService.DeleteTrashFile(deleteModel.id, tenantId,deleteModel.fileId);
            
            return Ok(res);
        }

    }
}
