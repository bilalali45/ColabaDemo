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
    [ApiController]
    public class MyPropertyController : BaseController
    {
        private readonly IMyPropertyService _myPropertyService;

        public MyPropertyController(IMyPropertyService myPropertyService)
        {
            _myPropertyService = myPropertyService;
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPropertyList(int loanApplicationId, int borrowerId)
        {
            return Ok(await _myPropertyService.GetPropertyList(Tenant, UserId, loanApplicationId, borrowerId));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> DoYouHaveFirstMortgage(int loanApplicationId, int borrowerPropertyId)
        {
            return Ok(await _myPropertyService.DoYouHaveFirstMortgage(Tenant, UserId, loanApplicationId, borrowerPropertyId));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> DoYouHaveSecondMortgage(int loanApplicationId, int borrowerPropertyId)
        {
            return Ok(await _myPropertyService.DoYouHaveSecondMortgage(Tenant, UserId, loanApplicationId, borrowerPropertyId));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateHasSecondMortgage(HasSecondMortgageModel model)
        {
            return Ok(await _myPropertyService.AddOrUpdateHasSecondMortgage(Tenant, UserId, model));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateHasFirstMortgage(HasFirstMortgageModel model)
        {
            return Ok(await _myPropertyService.AddOrUpdateHasFirstMortgage(Tenant, UserId, model));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPrimaryBorrowerAddressDetail(int loanApplicationId)
        {
            var tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            var userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _myPropertyService.GetPrimaryBorrowerAddressDetail(tenant,  userId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdatePrimaryPropertyType(BorrowerPropertyRequestModel model)
        {
            return Ok(await _myPropertyService.AddOrUpdatePrimaryPropertyType(Tenant, UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBorrowerPrimaryPropertyType(int borrowerPropertyId, int loanApplicationId)
        {
            return Ok(await _myPropertyService.GetBorrowerPrimaryPropertyType(Tenant, UserId, borrowerPropertyId, loanApplicationId));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPropertyValue(int borrowerPropertyId, int loanApplicationId)
        {
            return Ok(await _myPropertyService.GetPropertyValue(Tenant, UserId, borrowerPropertyId, loanApplicationId));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBorrowerAdditionalPropertyType(int borrowerPropertyId, int loanApplicationId)
        {
            return Ok(await _myPropertyService.GetBorrowerAdditionalPropertyType(Tenant, UserId, borrowerPropertyId, loanApplicationId));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateAdditionalPropertyType(BorrowerAdditionalPropertyRequestModel model)
        {
            return Ok(await _myPropertyService.AddOrUpdateAdditionalPropertyType(Tenant, UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdatePropertyValue(CurrentResidenceModel model)
        {
            return Ok(await _myPropertyService.AddOrUpdatePropertyValue(Tenant, UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateSecondMortgageValue(SecondMortgageModel model)
        {
            return Ok(await _myPropertyService.AddOrUpdateSecondMortgageValue(Tenant, UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSecondMortgageValue(int borrowerPropertyId, int loanApplicationId)
        {
            return Ok(await _myPropertyService.GetSecondMortgageValue(Tenant, UserId, borrowerPropertyId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateFirstMortgageValue(FirstMortgageModel model)
        {
            return Ok(await _myPropertyService.AddOrUpdateFirstMortgageValue(Tenant, UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFirstMortgageValue(int  borrowerPropertyId, int loanApplicationId)
        {
            return Ok(await _myPropertyService.GetFirstMortgageValue(Tenant, UserId, borrowerPropertyId, loanApplicationId ));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateAdditionalPropertyInfo(BorrowerAdditionalPropertyInfoRequestModel model)
        {
            return Ok(await _myPropertyService.AddOrUpdateAdditionalPropertyInfo(Tenant, UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBorrowerAdditionalPropertyInfo(int borrowerPropertyId, int loanApplicationId)
        {
            return Ok(await _myPropertyService.GetBorrowerAdditionalPropertyInfo(Tenant, UserId, borrowerPropertyId, loanApplicationId));
        }



        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdatBorrowerAdditionalPropertyAddress(BorrowerAdditionalPropertyAddressRequestModel model)
        {
            return Ok(await _myPropertyService.AddOrUpdatBorrowerAdditionalPropertyAddress(Tenant, UserId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBorrowerAdditionalPropertyAddress(int loanApplicationId, int borrowerPropertyId)
        {
            return Ok(await _myPropertyService.GetBorrowerAdditionalPropertyAddress(Tenant, UserId, loanApplicationId, borrowerPropertyId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFinalScreenReview(int borrowerId, int loanApplicationId)
        {
            return Ok(await _myPropertyService.GetFinalScreenReview(Tenant, UserId, loanApplicationId, borrowerId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> DoYouOwnAdditionalProperty(int loanApplicationId, int borrowerId)
        {
            var tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            var userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _myPropertyService.DoYouOwnAdditionalProperty(tenant, userId, loanApplicationId, borrowerId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteProperty(int loanApplicationId, int borrowerPropertyId)
        {
            return Ok(await _myPropertyService.DeleteProperty(Tenant, UserId, loanApplicationId, borrowerPropertyId));
        }

      
    }
}
