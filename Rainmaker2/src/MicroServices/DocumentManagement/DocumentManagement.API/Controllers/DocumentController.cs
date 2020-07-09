using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DocumentManagement.API.Controllers
{
    [Authorize(Roles = "MCU")]
    [ApiController]
    [Route("api/DocumentManagement/[controller]")]
    public class DocumentController : Controller
    {
        private readonly IDocumentService documentService;
        public DocumentController(IDocumentService documentService)
        {
            this.documentService = documentService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetDocumemntsByTemplateIds(TemplateIdModel templateIdsModel)
        {
            var docQuery = await documentService.GetDocumemntsByTemplateIds(templateIdsModel);

            return Ok(docQuery);
        }
     

        [HttpPost("[action]")]
        public async Task<IActionResult> GetFiles(string id, string requestId, string docId)
        {
            return Ok(await documentService.GetFiles(id, requestId, docId));
        }
       
        [HttpPost("[action]")]
        public async Task<IActionResult> GetActivityLog(string id, string typeId, string docId)
     
        {
            return Ok(await documentService.GetActivityLog(id, typeId, docId));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetEmailLog(string id)

        {
            return Ok(await documentService.GetEmailLog(id));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> mcuRename(string id, string requestId, string docId, string fileId, string newName)

        {
            var docQuery = await documentService.mcuRename(id, requestId, docId, fileId, newName);
            if (docQuery)
                return Ok();
            else
                return NotFound();
            
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AcceptDocument(AcceptDocumentModel acceptDocumentModel)
        {
            var docQuery = await documentService.AcceptDocument(acceptDocumentModel.id, acceptDocumentModel.requestId, acceptDocumentModel.docId);
            if (docQuery)
                return Ok();
            else
                return NotFound();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RejectDocument(RejectDocumentModel rejectDocumentModel)
        {
            var docQuery = await documentService.RejectDocument(rejectDocumentModel.id, rejectDocumentModel.requestId, rejectDocumentModel.docId, rejectDocumentModel.message);
            if (docQuery)
                return Ok();
            else
                return NotFound();
        }
    }
}
