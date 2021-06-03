using System.Threading.Tasks;
using Extensions.ExtensionClasses;
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
        //[Authorize(Roles = "Customer")]
        //[ResolveWebTenant]
        //[HttpGet(template: "[action]")]
        //public IActionResult GetAllIncomeCategories() //reviewed
        //{
        //    var tenant = (TenantModel) HttpContext.Items[key: Constants.COLABA_TENANT];
        //    return Ok(value: _incomeService.GetAllIncomeCategories(tenant: tenant));
        //}


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet(template: "[action]")]
        public IActionResult GetAllIncomeGroupsWithOtherIncomeTypes() //reviewed
        {
            var tenant = (TenantModel) HttpContext.Items[key: Constants.COLABA_TENANT];
            return Ok(value: _incomeService.GetAllIncomeGroupsWithOtherIncomeTypes(tenant: tenant));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet(template: "[action]")]
        public async Task<IActionResult> GetOtherIncomeInfo([FromQuery] int incomeInfoId) //reviewed
        {
            var tenant = (TenantModel) HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);
            return Ok(value: await _incomeService.GetOtherIncomeInfo(tenant: tenant,
                                                                     userId: userId,
                                                                     incomeInfoId: incomeInfoId));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> AddOrUpdateOtherIncome(AddOrUpdateOtherIncomeModel model) //reviewed
        { 
            if (!(model.IncomeTypeId == IncomeTypes.Alimony ||
                  model.IncomeTypeId == IncomeTypes.ChildSupport ||
                  model.IncomeTypeId == IncomeTypes.SeparateMaintenance ||
                  model.IncomeTypeId == IncomeTypes.FosterCare ||
                  model.IncomeTypeId == IncomeTypes.Annuity ||
                  model.IncomeTypeId == IncomeTypes.CapitalGains ||
                  model.IncomeTypeId == IncomeTypes.InterestDividends ||
                  model.IncomeTypeId == IncomeTypes.NotesReceivable ||
                  model.IncomeTypeId == IncomeTypes.Trust ||
                  model.IncomeTypeId == IncomeTypes.HousingOrParsonage ||
                  model.IncomeTypeId == IncomeTypes.MortgageCreditCertificate ||
                  model.IncomeTypeId == IncomeTypes.MortgageDiąerentialPayments ||
                  model.IncomeTypeId == IncomeTypes.PublicAssistance ||
                  model.IncomeTypeId == IncomeTypes.UnemploymentBenefits ||
                  model.IncomeTypeId == IncomeTypes.VACompensation ||
                  model.IncomeTypeId == IncomeTypes.AutomobileAllowance ||
                  model.IncomeTypeId == IncomeTypes.BoarderIncome ||
                  model.IncomeTypeId == IncomeTypes.RoyaltyPayments ||
                  model.IncomeTypeId == IncomeTypes.Disability ||
                  model.IncomeTypeId == IncomeTypes.OtherIncomeSource)
            )
                return BadRequest(error: "Income Type not allowed");

            if (model.IncomeTypeId == IncomeTypes.Alimony ||
                model.IncomeTypeId == IncomeTypes.ChildSupport ||
                model.IncomeTypeId == IncomeTypes.SeparateMaintenance ||
                model.IncomeTypeId == IncomeTypes.FosterCare ||
                model.IncomeTypeId == IncomeTypes.AutomobileAllowance ||
                model.IncomeTypeId == IncomeTypes.BoarderIncome ||
                model.IncomeTypeId == IncomeTypes.Disability ||
                model.IncomeTypeId == IncomeTypes.RoyaltyPayments ||
                model.IncomeTypeId == IncomeTypes.HousingOrParsonage ||
                model.IncomeTypeId == IncomeTypes.MortgageDiąerentialPayments ||
                model.IncomeTypeId == IncomeTypes.MortgageCreditCertificate ||
                model.IncomeTypeId == IncomeTypes.PublicAssistance ||
                model.IncomeTypeId == IncomeTypes.UnemploymentBenefits ||
                model.IncomeTypeId == IncomeTypes.VACompensation ||
                model.IncomeTypeId == IncomeTypes.NotesReceivable ||
                model.IncomeTypeId == IncomeTypes.NotesReceivable ||
                model.IncomeTypeId == IncomeTypes.Trust)
            {
                if (!model.MonthlyBaseIncome.HasValue())
                    return BadRequest(error: "MonthlyBaseIncome required.");
                if (model.AnnualBaseIncome.HasValue())
                    return BadRequest(error: "AnnualBaseIncome is NOT required in this incomeType.");
                if (model.Description.HasValue())
                    return BadRequest(error: "Description is NOT required in this incomeType.");
            }

            if (model.IncomeTypeId == IncomeTypes.Annuity)
            {
                if (!model.MonthlyBaseIncome.HasValue())
                    return BadRequest(error: "MonthlyBaseIncome required.");
                if (!model.Description.HasValue())
                    return BadRequest(error: "Description required.");
                if (model.AnnualBaseIncome.HasValue())
                    return BadRequest(error: "AnnualBaseIncome is NOT required in this incomeType.");
            }

            if (model.IncomeTypeId == IncomeTypes.InterestDividends ||
                model.IncomeTypeId == IncomeTypes.CapitalGains)
            {
                if (!model.AnnualBaseIncome.HasValue())
                    return BadRequest(error: "AnnualBaseIncome required.");
                if (model.MonthlyBaseIncome.HasValue())
                    return BadRequest(error: "MonthlyBaseIncome is not required in this incomeType.");
                if (model.Description.HasValue())
                    return BadRequest(error: "Description is NOT required in this incomeType.");
            }

            if (model.IncomeTypeId == IncomeTypes.OtherIncomeSource)
            {
                if (!model.AnnualBaseIncome.HasValue())
                    return BadRequest(error: "AnnualBaseIncome required.");
                if (!model.Description.HasValue())
                    return BadRequest(error: "Description required.");
                if (model.MonthlyBaseIncome.HasValue())
                    return BadRequest(error: "MonthlyBaseIncome is NOT required in this incomeType.");
            }

            var tenant = (TenantModel) HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);
            return Ok(value: await _incomeService.AddOrUpdateEmployerOtherMonthyIncome(tenant: tenant,
                                                                                       userId: userId,
                                                                                       model: model));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet(template: "[action]")]
        public async Task<IActionResult> GetSummary([FromQuery] int loanApplicationId)
        {
            var tenant = (TenantModel) HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);
            return Ok(value: await _incomeService.GetSummary(tenant: tenant,
                                                             userId: userId,
                                                             loanApplicationId: loanApplicationId));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet(template: "[action]")]
        public async Task<IActionResult> GetMyMoneyHomeScreen(int loanApplicationId)
        {
            var tenant = (TenantModel) HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);
            return Ok(value: await _incomeService.GetAllIncomesForHomeScreen(tenant: tenant,
                                                                             userId: userId,
                                                                             loanApplicationId: loanApplicationId));
        }
    }
}