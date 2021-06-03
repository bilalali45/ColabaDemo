using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Model;
using TenantConfig.Service;

namespace TenantConfig.API.Controllers
{
    [Route("api/TenantConfig/[controller]")]
    [ApiController]

    public class LogController : Controller
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }
        [HttpPost("[action]")]
        [AllowAnonymous]
        [ResolveWebTenant]
        [ServiceFilter(typeof(ValidateRecaptchaAttribute))]
        public async Task<IActionResult> InsertContactEmailLog(ContactEmailLogModel logModel)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            await _logService.InsertLogContactEmail(logModel.FirstName, logModel.LastName, logModel.Email, tenant.Id);
            return Ok();

        }
        [HttpPost("[action]")]
        [AllowAnonymous]
        [ResolveWebTenant]
        [ServiceFilter(typeof(ValidateRecaptchaAttribute))]
        public async Task<IActionResult> InsertContactEmailPhoneLog(ContactEmailPhoneLogModel logModel)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            await _logService.InsertLogContactEmailPhone(logModel.FirstName, logModel.LastName, logModel.Email, logModel.Phone, tenant.Id);
            return Ok();

        }
    }
}
