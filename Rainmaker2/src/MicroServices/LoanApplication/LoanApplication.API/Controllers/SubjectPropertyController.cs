using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LoanApplication.Model;
using LoanApplication.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.API.Controllers
{
    [Route("api/loanapplication/[controller]")]
    [ApiController]
    public class SubjectPropertyController : ControllerBase
    {
        private readonly ILogger<SubjectPropertyController> _logger;
        private readonly ISubjectPropertyService _subjectPropertyService;

        public SubjectPropertyController(ILogger<SubjectPropertyController> logger, ISubjectPropertyService subjectPropertyService)
        {
            _logger = logger;
            _subjectPropertyService = subjectPropertyService;
        }

        //[Authorize(Roles = "Customer")]
        //[ResolveWebTenant]
        //[HttpPost("[action]")]
        //public async Task<IActionResult> AddOrUpdateSubjectPropertyAddress(RainmakerAddressInfoModel model)
        //{
        //    TenantModel tenant = HttpContext.Items[Constants.COLABA_TENANT] as TenantModel;
        //    int userId = int.Parse(User.FindFirst("UserId").Value);
        //    return Ok(await _subjectPropertyService.AddOrUpdateSubjectPropertyAddress(tenant, model, userId));
        //}

        //[Authorize(Roles = "Customer")]
        //[ResolveWebTenant]
        //[HttpGet("[action]")]
        //public async Task<IActionResult> GetSubjectPropertyAddress([FromQuery] LoanApplicationIdModel model)
        //{
        //    TenantModel tenant = HttpContext.Items[Constants.COLABA_TENANT] as TenantModel;
        //    int userId = int.Parse(User.FindFirst("UserId").Value);
        //    return Ok(await _subjectPropertyService.GetSubjectPropertyAddress(tenant, model.LoanApplicationId, userId));
        //}

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateSubjectPropertyState(UpdateSubjectPropertyStateModel model)
        {
            TenantModel tenant = HttpContext.Items[Constants.COLABA_TENANT] as TenantModel;
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _subjectPropertyService.AddOrUpdateSubjectPropertyState(tenant, model, userId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSubjectPropertyState([FromQuery] LoanApplicationIdModel model)
        {
            TenantModel tenant = HttpContext.Items[Constants.COLABA_TENANT] as TenantModel;
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _subjectPropertyService.GetSubjectPropertyState(tenant, model.LoanApplicationId, userId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateLoanAmountDetail(LoanAmountDetailModel model)
        {
            if (model.GiftPartOfDownPayment)
            {
                if (model.GiftPartReceived == true && model.DateOfTransfer > DateTime.Now) // TODO discus if UTC Date to use
                {
                    return BadRequest(new ErrorModel() { Message = "Date of transfer cannot be future date.", Code = ((int)HttpStatusCode.BadRequest)});
                }

                if (model.GiftPartReceived == false && model.DateOfTransfer < DateTime.Now) // TODO discus if UTC Date to use
                {
                    return BadRequest(new ErrorModel() { Message = "Date of transfer cannot be past date.", Code = ((int)HttpStatusCode.BadRequest) });
                }
            }

            TenantModel tenant = HttpContext.Items[Constants.COLABA_TENANT] as TenantModel;
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _subjectPropertyService.AddOrUpdateLoanAmountDetail(tenant, model, userId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSubjectPropertyLoanAmountDetail([FromQuery] LoanApplicationIdModel model)
        {
            TenantModel tenant = HttpContext.Items[Constants.COLABA_TENANT] as TenantModel;
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _subjectPropertyService.GetSubjectPropertyLoanAmountDetail(tenant, model.LoanApplicationId, userId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdatePropertyIdentified(PropertyIdentifiedModel model)
        {
            if (model.IsIdentified == false && (model.StateId <= 0 || model.StateId == null))
            {
                return BadRequest("State Id is required when property is not identified");
            }
            TenantModel tenant = HttpContext.Items[Constants.COLABA_TENANT] as TenantModel;
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _subjectPropertyService.UpdatePropertyIdentifiedFlag(tenant, userId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPropertyIdentifiedFlag([FromQuery] LoanApplicationIdModel model)
        {
            TenantModel tenant = HttpContext.Items[Constants.COLABA_TENANT] as TenantModel;
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _subjectPropertyService.GetPropertyIdentifiedFlag(tenant, userId, model.LoanApplicationId));
        }
    }
}
