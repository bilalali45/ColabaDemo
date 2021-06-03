using LoanApplication.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.API.Controllers
{
    [Route("api/loanapplication/[controller]")]
    [ApiController]
    public class LoanPurposeController : ControllerBase
    {
        private readonly IInfoService _infoService;
        public LoanPurposeController(IInfoService infoService)
        {
            _infoService = infoService;
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllLoanPurpose()
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            return Ok(_infoService.GetAllLoanPurpose(tenant));
        }
    }
}
