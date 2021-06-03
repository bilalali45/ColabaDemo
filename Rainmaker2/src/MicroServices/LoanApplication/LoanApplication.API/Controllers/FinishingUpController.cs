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
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.API.Controllers
{
    [Route("api/loanapplication/[controller]")]
    public class FinishingUpController :  BaseController
    {
        private readonly IFinishingUpService _finishingUpService;

        public FinishingUpController(IFinishingUpService finishingUpService)
        {
            _finishingUpService = finishingUpService;
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBorrowerPrimaryAddressDetail(int loanApplicationId, int borrowerId)
        {
            var tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            var userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _finishingUpService.GetBorrowerPrimaryAddressDetail(tenant, userId, loanApplicationId, borrowerId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateBorrowerCurrentResidenceMoveInDate(CurrentResidenceRequestModel model)
        {
            return Ok(await _finishingUpService.AddOrUpdateBorrowerCurrentResidenceMoveInDate(Tenant, UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBorrowerCurrentResidenceMoveInDate(int loanApplicationId , int borrowerResidenceId)
        {
            return Ok(await _finishingUpService.GetBorrowerCurrentResidenceMoveInDate(Tenant, UserId, borrowerResidenceId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCoborrowerResidence(int loanApplicationId)
        {
            return Ok(await _finishingUpService.GetCoborrowerResidence(Tenant, UserId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetResidenceHistory(int loanApplicationId)
        {
            return Ok(await _finishingUpService.GetResidenceHistory(Tenant, UserId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetResidenceDetails(int borrowerId ,int loanApplicationId)
        {
            return Ok(await _finishingUpService.GetResidenceDetails(Tenant, UserId, borrowerId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateBorrowerCitizenship(BorrowerCitizenshipRequestModel model)
        {
            return Ok(await _finishingUpService.AddOrUpdateBorrowerCitizenship(Tenant, UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBorrowerCitizenship(int borrowerId, int loanApplicationId)
        {
            return Ok(await _finishingUpService.GetBorrowerCitizenship(Tenant, UserId, borrowerId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateSecondaryAddress(BorrowerSecondaryAddressRequestModel model)
        {
            return Ok(await _finishingUpService.AddOrUpdateSecondaryAddress(Tenant, UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSecondaryAddress(int borrowerResidenceId, int loanApplicationId)
        {
            return Ok(await _finishingUpService.GetSecondaryAddress(Tenant, UserId, borrowerResidenceId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteSecondaryAddress(int borrowerResidenceId, int loanApplicationId)
        {
            await _finishingUpService.DeleteSecondaryAddress(Tenant, UserId, borrowerResidenceId, loanApplicationId);
            return Ok();
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDependentinfo(int loanApplicationId, int borrowerId)
        {
            return Ok(await _finishingUpService.GetDependentinfo(Tenant, UserId, loanApplicationId, borrowerId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateDependentinfo(DependentModel model)
        {
            return Ok(await _finishingUpService.AddOrUpdateDependentinfo(Tenant, UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllSpouseInfo(int loanApplicationId)
        {
            return Ok(await _finishingUpService.GetAllSpouseInfo(Tenant, UserId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateSpouseInfo(SpouseInfoRequestModel model)
        {
            return Ok(await _finishingUpService.AddOrUpdateSpouseInfo(Tenant, UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSpouseInfo(int borrowerId , int spouseLoanContactId, int loanApplicationId)
        {
            return Ok(await _finishingUpService.GetSpouseInfo(Tenant, UserId, borrowerId, spouseLoanContactId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> ReviewBorrowerAndAllCoBorrowersInfo(int loanApplicationId)
        {
            return Ok(await _finishingUpService.ReviewBorrowerAndAllCoBorrowersInfo(Tenant, UserId, loanApplicationId));
        }
    }
}
