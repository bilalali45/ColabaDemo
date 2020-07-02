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
        [HttpGet("GetDocument")]
        public async Task<IActionResult> GetDocument(string id, int tenantId)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var docQuery = await templateService.GetDocument(id, tenantId, userProfileId);
            return Ok(docQuery);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RenameTemplate(string id, int tenantid, string newname)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var docQuery = await templateService.RenameTemplate(id, tenantid, newname, userProfileId);
            if (docQuery)
                return Ok();
            else
                return NotFound();
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(string id, int tenantid, string documentid)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var docQuery = await templateService.DeleteDocument(id, tenantid, documentid,   userProfileId);
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
    }
}
