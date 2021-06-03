using LoanApplication.Model;
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
    public class PropertyController : ControllerBase
    {
        private readonly IInfoService _infoService;
        private readonly IPropertyService _propertyService;
        public PropertyController(IInfoService infoService, IPropertyService propertyService)
        {
            this._infoService = infoService;
            this._propertyService = propertyService;
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllPropertyTypes(int sectionId=1)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            return Ok(_infoService.GetAllPropertyTypes(tenant,sectionId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPropertyType(int loanApplicationId)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _propertyService.GetPropertyType(tenant,loanApplicationId,userId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdatePropertyType(AddPropertyTypeModel model)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _propertyService.AddOrUpdatePropertyType(tenant, model, userId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllPropertyUsages(int sectionId=1)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            return Ok(_infoService.GetAllPropertyUsages(tenant,sectionId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPropertyUsage(int loanApplicationId)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _propertyService.GetPropertyUsage(tenant, loanApplicationId, userId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdatePropertyUsage(AddPropertyUsageModel model)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _propertyService.AddOrUpdatePropertyUsage(tenant, model, userId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPropertyAddress(int loanApplicationId)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await this._propertyService.GetPropertyAddress(tenant, userId, loanApplicationId));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdatePropertyAddress(AddPropertyAddressModel model)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _propertyService.AddOrUpdatePropertyAddress(tenant, model, userId));
        }
    }
}
