using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static DocumentManagement.Model.Template;

namespace DocumentManagement.API.Controllers
{
    [Authorize(Roles="MCU")]
    [ApiController]
    [Route("api/DocumentManagement/[controller]")]
    public class TemplateController : Controller
    {
        private readonly ITemplateService templateService;
        public TemplateController(ITemplateService templateService)
        {
            this.templateService = templateService;
        }

        [HttpGet("GetTemplates")]
        public async Task<IActionResult> GetTemplates(int tenantId)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var docQuery = await templateService.GetTemplates(tenantId, userProfileId);
            return Ok(docQuery);
        }
        [HttpGet("GetDocuments")]
        public async Task<IActionResult> GetDocument(string id)
        {
            var docQuery = await templateService.GetDocument(id);
            return Ok(docQuery);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RenameTemplate(string id, int tenantId, string name)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var docQuery = await templateService.RenameTemplate(id, tenantId, name, userProfileId);
            if (docQuery)
                return Ok();
            else
                return NotFound();
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteDocument(string id, int tenantId, string documentId)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var docQuery = await templateService.DeleteDocument(id, tenantId, documentId,   userProfileId);
            if (docQuery)
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteTemplate(string templateId, int tenantId)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var docQuery = await templateService.DeleteTemplate(templateId, tenantId, userProfileId);
            if (docQuery)
                return Ok();
            else
                return NotFound();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> InsertTemplate(InsertTemplateModel insertTemplateModel)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
           
            var docQuery = await templateService.InsertTemplate(insertTemplateModel.tenantId, userProfileId, insertTemplateModel.name);
                  
            return Ok(docQuery);
        }

        [HttpGet("GetCategoryDocument")]
        public async Task<IActionResult> GetCategoryDocument(int tenantId)
        {
            var docQuery = await templateService.GetCategoryDocument(tenantId);
            return Ok(docQuery);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddDocument(AddDocumentModel addDocumentModel)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());

            var docQuery = await templateService.AddDocument(addDocumentModel.templateId, addDocumentModel.tenantId, userProfileId, addDocumentModel.typeId,addDocumentModel.docName);

            if (docQuery)
                return Ok();
            else
                return NotFound();
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> SaveTemplate(AddTemplateModel  model)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            return Ok(await templateService.SaveTemplate(model,userProfileId));
           
        }
    }
}
