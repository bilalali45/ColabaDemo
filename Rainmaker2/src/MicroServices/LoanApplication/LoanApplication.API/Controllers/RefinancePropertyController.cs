using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanApplication.Model;
using LoanApplication.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenantConfig.Common;
using static LoanApplication.Model.RefinancePropertyModel;

namespace LoanApplication.API.Controllers
{
    [Route("api/loanapplication/[controller]")]
    [ApiController]
    public class RefinancePropertyController : BaseController
    {
        private readonly IRefinancePropertyService _refinancePropertyService;
        public RefinancePropertyController(IInfoService infoService, IRefinancePropertyService refinancePropertyService)
        {
            this._refinancePropertyService = refinancePropertyService;
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBorrowerResidenceAddress(int borrowerResidenceId, int borrowerId , int loanApplicationId)
        {
            return Ok(await _refinancePropertyService.GetBorrowerResidenceAddress(Tenant, UserId, borrowerResidenceId, borrowerId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPrimaryBorrowerResidenceHousingStatus(int loanApplicationId, int borrowerId)
        {
            return Ok(await _refinancePropertyService.GetPrimaryBorrowerResidenceHousingStatus(base.Tenant, base.UserId, loanApplicationId, borrowerId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPropertyType(int loanApplicationId)
        {
            return Ok(await _refinancePropertyService.GetPropertyType(base.Tenant, base.UserId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateIsSameAsPropertyAddress(SameAsPropertyAddress model)
        {
            return Ok(await _refinancePropertyService.AddOrUpdateIsSameAsPropertyAddress(Tenant, UserId, model));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPropertyUsageRent(int loanApplicationId)
        {
            return Ok(await _refinancePropertyService.GetPropertyUsageRent(base.Tenant, base.UserId, loanApplicationId));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdatePropertyUsageRent(AddPropertyUsageRefinanceModel model)
        {
            return Ok(await _refinancePropertyService.AddOrUpdatePropertyUsageRent(base.Tenant, base.UserId, model));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdatePropertyType(AddPropertyTypeModel model)
        {
            return Ok(await _refinancePropertyService.AddOrUpdatePropertyType(Tenant,UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdatePropertyUsageOwn(AddPropertyUsagerequestModel model)
        {
            return Ok(await _refinancePropertyService.AddOrUpdatePropertyUsageOwn(Tenant, model, UserId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPropertyUsageOwn(int loanApplicationId)
        {
            return Ok(await _refinancePropertyService.GetPropertyUsageOwn(base.Tenant, loanApplicationId, base.UserId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPropertyAddress(int loanApplicationId)
        {
            return Ok(await _refinancePropertyService.GetPropertyAddress(base.Tenant, base.UserId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdatePropertyAddress(AddPropertyAddressModel model)
        {
            return Ok(await _refinancePropertyService.AddOrUpdatePropertyAddress(Tenant, model, UserId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSubjectPropertyDetails(int loanApplicationId)
        {
            return Ok(await _refinancePropertyService.GetSubjectPropertyDetails(base.Tenant, base.UserId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateSubjectPropertyDetails(SubjectPropertyDetailsRequestModel model)
        {
            return Ok(await _refinancePropertyService.AddOrUpdateSubjectPropertyDetails(Tenant, model, UserId));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateFirstMortgageValue(FirstMortgageModel model)
        {
            return Ok(await _refinancePropertyService.AddOrUpdateFirstMortgageValue(Tenant, UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFirstMortgageValue(int loanApplicationId)
        {
            return Ok(await _refinancePropertyService.GetFirstMortgageValue(Tenant, UserId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> DoYouHaveFirstMortgage(int loanApplicationId)
        {
            return Ok(await _refinancePropertyService.DoYouHaveFirstMortgage(Tenant, UserId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateHasFirstMortgage(HasMortgageModel model)
        {
            return Ok(await _refinancePropertyService.AddOrUpdateHasFirstMortgage(base.Tenant, base.UserId, model));
        }
    }
}
