using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> InsertTemplate([FromForm] int tenantId, string name)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
           
            // insert into mongo
            //var docQuery = await fileService.Submit(formFile.ContentType, id, requestId, docId, formFile.FileName, Path.GetFileName(filePath), (int)formFile.Length, key, algo, tenantId, userProfileId);
                  
            return Ok();
        }
    }
}
