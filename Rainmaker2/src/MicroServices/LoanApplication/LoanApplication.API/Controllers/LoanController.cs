using LoanApplication.Model;
using LoanApplication.Service;
using Microsoft.AspNetCore.Authorization;
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
    public class LoanController : Controller
    {
        private readonly IInfoService _infoService;
        private readonly ILoanService _loanService;
        public LoanController(IInfoService infoService, ILoanService loanService)
        {
            _infoService = infoService;
            _loanService = loanService;
        }
        
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllMaritalStatus()
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            return Ok(_infoService.GetAllMaritalStatus(tenant));
        }
        
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLoInfo()
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            if (tenant.Branches[0].LoanOfficers.Count == 1)
            {
                return Ok(await _infoService.GetLoInfo(tenant.Branches[0].LoanOfficers[0].Id, tenant));
            }
            else
            {
                return Ok(await _infoService.GetBranchInfo(tenant.Branches[0].Id, tenant));
            }
        }
        
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPendingLoanApplication(int? loanApplicationId)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _loanService.GetPendingLoanApplication(tenant,userId,loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDashboardLoanInfo()
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _loanService.GetDashboardLoanInfo(tenant, userId));
        }

        //[Authorize(Roles = "Customer")]
        //[ResolveWebTenant]
        //[HttpGet("[action]")]
        //public async Task<IActionResult> GetReviewBorrowerInfoSection([FromQuery] LoanApplicationIdModel model)
        //{
        //    TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
        //    int userId = int.Parse(User.FindFirst("UserId").Value);
        //    return Ok(await _loanService.GetLoanApplicationForBorrowersInfoSectionReview(tenant, userId, model.LoanApplicationId));
        //}

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLoanApplicationFirstReview([FromQuery] LoanApplicationIdModel model)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _loanService.GetLoanApplicationForFirstReview(tenant, userId, model.LoanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLoanApplicationSecondReview([FromQuery] LoanApplicationIdModel model)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _loanService.GetLoanApplicationForSecondReview(tenant, userId, model.LoanApplicationId));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateState(UpdateStateModel model)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _loanService.UpdateState(tenant, userId, model));
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> SubmitLoanApplication(LoanCommentsModel model)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _loanService.SubmitLoanApplication(tenant, userId, model));
        }
    }
}
