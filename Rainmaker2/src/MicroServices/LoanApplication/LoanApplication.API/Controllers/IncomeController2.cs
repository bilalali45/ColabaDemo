using System.Threading.Tasks;
using LoanApplication.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.API.Controllers
{
    // [Route(template: "api/LoanApplication/[controller]")] // not required in partial class as long as present in one
    // [ApiController]// not required in partial class as long as present in one
    public partial class IncomeController
    {
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> AddOrUpdateCurrentEmploymentDetail(AddOrUpdateEmploymentDetailModel model)
        {
            if (model == null) return BadRequest(error: "Employer detail missing");
            var tenant = (TenantModel) HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);
            var results = await _employmentService.AddOrUpdateCurrentEmploymentDetail(tenant: tenant,
                                                                               userId: userId,
                                                                               model: model);

            if (results <= 0)
            {
                var errorMessage = _employmentService.ErrorMessages[results];
                return BadRequest(error: errorMessage);
            }

            return Ok(results);
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet(template: "[action]")]
        public async Task<IActionResult> GetEmploymentDetail([FromQuery] GetEmploymentDetailModel model)
        {
            var tenant = (TenantModel) HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);
            var modelToReturn = await _employmentService.GetEmploymentDetail(tenant: tenant,
                                                                             userId: userId,
                                                                             loanApplicationId: model.LoanApplicationId,
                                                                             borrowerId: model.BorrowerId,
                                                                             incomeInfoId: model.IncomeInfoId);
            if (!string.IsNullOrEmpty(value: modelToReturn.ErrorMessage)) return BadRequest(error: modelToReturn.ErrorMessage);

            return Ok(value: modelToReturn);
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> AddOrUpdatePreviousEmploymentDetail(AddOrUpdatePreviousEmploymentDetailModel model)
        {
            if (model == null) return BadRequest(error: "Employer detail missing");
            var tenant = (TenantModel)HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);
            var results = await _employmentService.AddOrUpdatePreviousEmploymentDetail(tenant: tenant,
                userId: userId,
                model: model);

            if (results <= 0)
            {
                var errorMessage = _employmentService.ErrorMessages[results];

                return BadRequest(error: errorMessage);
            }

            return Ok(results);
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet(template: "[action]")]
        public IActionResult GetEmploymentOtherDefaultIncomeTypes()
        {
            var tenant = (TenantModel)HttpContext.Items[key: Constants.COLABA_TENANT];
            return Ok(_employmentService.GetEmploymentOtherDefaultIncomeTypes(tenant));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpDelete(template: "[action]")]
        public async Task<IActionResult> DeleteEmploymentIncome([FromQuery] CurrentEmploymentDeleteModel model)
        {
            var tenant = (TenantModel) HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);
            var results = await _employmentService.DeleteIncomeDetail(tenant, userId, model);
            if (results <= 0)
            {
                var errorMessage = _employmentService.ErrorMessages[results];

                return BadRequest(error: errorMessage);
            }

            return Ok(results);
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet(template: "[action]")]
        public async Task<IActionResult> GetIncomeSectionReview([FromQuery] LoanApplicationIdModel model)
        {
            var tenant = (TenantModel)HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);
            var modelToReturn = await _employmentService.GetAllBorrowerWithIncome(tenant, userId, model.LoanApplicationId);

            return Ok(value: modelToReturn);
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBorrowersEmploymentHistory([FromQuery] LoanApplicationIdModel model)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            var modelToReturn = await _employmentService.GetEmploymentHistory(tenant, userId, model.LoanApplicationId);
            if (!string.IsNullOrEmpty(modelToReturn.ErrorMessage))
            {
                return BadRequest(modelToReturn.ErrorMessage);
            }

            return Ok(modelToReturn);
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBorrowerIncomes([FromQuery] GetBorrowerIncomesModel model)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            var results = await _incomeService.GetBorrowerIncomes(tenant, userId, model);
            if (!string.IsNullOrEmpty(results.ErrorMessage))
            {
                return BadRequest(results.ErrorMessage);
            }

            return Ok(results);
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet(template: "[action]")]
        public async Task<IActionResult> GetBorrowerIncomesForReview([FromQuery] LoanApplicationIdModel model)
        {
            var tenant = (TenantModel)HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);
            var results =
                await this._employmentService.GetBorrowerIncomesForReview(tenant, userId, model.LoanApplicationId);
            if(!string.IsNullOrEmpty(results.ErrorMessage))
            {
                return BadRequest(results.ErrorMessage);
            }

            return Ok(results);
        }
    }
}