using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetDocumemntsByTemplateIds(TemplateIdModel templateIdsModel)
        {
            var docQuery = await documentService.GetDocumemntsByTemplateIds(templateIdsModel);

            return Ok(docQuery);
        }
    }
}
