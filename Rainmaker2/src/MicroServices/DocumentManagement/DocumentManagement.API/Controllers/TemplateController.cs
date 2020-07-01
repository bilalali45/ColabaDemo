using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.API.Controllers
{
    [ApiController]
    [Route("api/TemplateController/[controller]")]
    public class TemplateController : Controller
    {
        private readonly ITemplateService templateService;
        public TemplateController(ITemplateService templateService)
        {
            this.templateService = templateService;
        }

        [HttpGet("GetTemplates")]
        public async Task<IActionResult> GetTemplates(int? tenantId)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var docQuery = await templateService.GetTemplates(tenantId, userProfileId);
            return Ok(docQuery);
        }
    }
}
