using System.Threading.Tasks;
using LoanApplication.Model;
using LoanApplication.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.API.Controllers
{
    [Route("api/loanapplication/[controller]")]
    [ApiController]
    public class VaController : Controller
    {
        private readonly IInfoService _infoService;
        private readonly IVaDetailService _vaDetailService;

        public VaController(IInfoService infoService, IVaDetailService vaDetailService)
        {
            _infoService = infoService;
            _vaDetailService = vaDetailService;
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBorrowerVaDetail([FromQuery] BorrowerIdModel model)
        {
            var tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            var userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _vaDetailService.GetVaDetails(tenant, userId, model.BorrowerId));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateBorrowerVaStatus(VaDetailAddOrUpdate model)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _vaDetailService.AddOrUpdate(tenant, userId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMilitaryAffiliationsList()
        {
            var tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];

            return Ok(this._infoService.GetAllMilitaryAffiliation(tenant));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> SetBorrowerVaStatus(BorrowerVaStatusModel model)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _vaDetailService.SetBorrowerVaStatus(tenant, userId, model));
        }

        //[Authorize(Roles = "Customer")]
        //[ResolveWebTenant]
        //[HttpPost("[action]")]
        //public async Task<IActionResult> GetBorrowerVaStatus(BorrowerIdModel model)
        //{
        //    TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
        //    return Ok(await _vaDetailService.GetBorrowerVaStatus(tenant, model.BorrowerId));
        //}
    }
}