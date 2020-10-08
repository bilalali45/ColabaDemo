using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DocumentManagement.API.Controllers
{
    [Route("api/documentmanagement/[controller]")]
    [ApiController]
    [Authorize(Roles ="MCU,Customer")]
    public class SettingController : ControllerBase
    {
        private readonly IByteProService byteProService;
        public SettingController(IByteProService byteProService)
        {
            this.byteProService = byteProService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTenantSetting()
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var setting = await byteProService.GetTenantSetting(tenantId);
            return Ok(setting);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> SetTenantSetting(TenantSetting setting)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            await byteProService.SetTenantSetting(tenantId,setting);
            return Ok();
        }
    }
}
