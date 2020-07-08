using DocumentManagement.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DocumentManagement.API.Controllers
{
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
        public async Task<IActionResult> GetFiles(string id, string requestId, string docId)
        {
            return Ok(await documentService.GetFiles(id, requestId, docId));
        }
       
        [HttpPost("[action]")]
        public async Task<IActionResult> GetActivityLog(string id, string requestId, string docId)
     
        {
            return Ok(await documentService.GetActivityLog(id, requestId, docId));
        }
    }
}
