using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanApplication.Model;
using LoanApplication.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Query;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;


namespace LoanApplication.API.Controllers
{
    [Route("api/LoanApplication/[controller]")]
    [ApiController]
    public partial class IncomeController : Controller
    {
        private readonly IIncomeService _incomeService;
        private readonly IInfoService _infoService;
        private readonly IEmploymentService _employmentService;


        public IncomeController(IIncomeService incomeService, IInfoService infoService,
                                IEmploymentService employmentService)
        {
            _incomeService = incomeService;
            _infoService = infoService;
            _employmentService = employmentService;
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllBusinessTypes()
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            //return Ok(await _infoService.GetAllBusinessTypes(tenant));
            return Ok(await _infoService.GetIncomeTypes(tenant, Model.IncomeCategory.Business));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateBusiness(BusinessModel model)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            if (!(model.IncomeTypeId == (int)IncomeTypes.Cooperation || model.IncomeTypeId == (int)IncomeTypes.Partnership)
                 )
                return BadRequest(error: "Income Type not allowed");

            return Ok(await _incomeService.AddOrUpdateBusiness(tenant, model, userId));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBusinessIncome(int borrowerId, int incomeInfoId)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _incomeService.GetBusinessIncome(tenant, borrowerId, incomeInfoId, userId));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public IActionResult GetAllIncomeCategories()
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            return Ok(_incomeService.GetAllIncomeCategories(tenant));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetRetirementIncomeTypes()
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            //return Ok(await _infoService.GetRetirementIncomeTypes(tenant));
            return Ok(await _infoService.GetIncomeTypes(tenant, IncomeCategory.Retirement));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateRetirementIncomeInfo(RetirementIncomeInfoModel model)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await this._incomeService.AddOrUpdateRetirementIncomeInfo(model, tenant, userId));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMilitaryIncome(int borrowerId, int incomeInfoId)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);

            return Ok(await _incomeService.GetMilitaryIncome(tenant, userId, borrowerId, incomeInfoId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateMilitaryIncome(MilitaryIncomeModel model)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);

            return Ok(await _incomeService.AddOrUpdateMilitaryIncome(tenant, userId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetRetirementIncomeInfo(int incomeInfoId, int borrowerId)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _incomeService.GetRetirementIncomeInfo(incomeInfoId, borrowerId, userId, tenant));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateSelfBusiness(SelfBusinessModel model)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _incomeService.AddOrUpdateSelfBusiness(tenant, model, userId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSelfBusinessIncome(int borrowerId, int incomeInfoId)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _incomeService.GetSelfBusinessIncome(tenant, borrowerId, incomeInfoId, userId));
        }
    }
}
