using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Extensions.ExtensionClasses;
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
    public class BorrowerController : ControllerBase
    {
        private readonly IBorrowerConsentService _borrowerConsentService;
        private readonly IBorrowerService _borrowerService;
        private readonly IInfoService _infoService;
        private readonly ILoanContactService _loanContactService;

        public BorrowerController(ILoanContactService loanContactService,
            IBorrowerConsentService borrowerConsentService, IInfoService infoService, IBorrowerService borrowerService)
        {
            _loanContactService = loanContactService;
            _borrowerConsentService = borrowerConsentService;
            _infoService = infoService;
            _borrowerService = borrowerService;
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBorrowerDobSsn([FromQuery] BorrowerDobSsnGetModel model)
        {
            var tenant = (TenantModel) HttpContext.Items[Constants.COLABA_TENANT];
            var userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _loanContactService.GetDobSsn(tenant, model.BorrowerId, model.LoanApplicationId, userId));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> AddOrUpdateDobSsn(BorrowerDobSsnAddOrUpdate model)
        {
            var tenant = (TenantModel) HttpContext.Items[key: Constants.COLABA_TENANT];
            var userId = int.Parse(s: User.FindFirst(type: "UserId").Value);
            if (model.DobUtc.HasValue && model.Ssn.HasValue())
                //ok

                return Ok(value: await _loanContactService.UpdateDobSsn(tenant: tenant,
                                                                        userId: userId,
                                                                        model: model));

            if (model.DobUtc.HasValue && !model.Ssn.HasValue())
                //ok
                return Ok(value: await _loanContactService.UpdateDobSsn(tenant: tenant,
                                                                        userId: userId,
                                                                        model: model));

            if (!model.DobUtc.HasValue && model.Ssn.HasValue())
                // not ok return validation error
                return BadRequest(error: new ErrorModel
                                         {
                                             Message = "DOB is required when SSN is provided",
                                             Code = (int) HttpStatusCode.BadRequest
                                         });

            if (!model.DobUtc.HasValue && !model.Ssn.HasValue())
            {
                // not ok return validation error 
                // although this case is a valid case but api shud not be called in this case
            }

            return NoContent();
            //TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            //int userId = int.Parse(User.FindFirst("UserId").Value);
            //return Ok(await _loanContactService.UpdateDobSsn(tenant,userId,model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllConsentTypes()
        {
            var tenant = (TenantModel) HttpContext.Items[Constants.COLABA_TENANT];
            return Ok(_borrowerConsentService.GetAllConsentType(tenant));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBorrowerConsent(int borrowerId)
        {
            var tenant = (TenantModel) HttpContext.Items[Constants.COLABA_TENANT];
            var userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _borrowerConsentService.GetBorrowerConsent(tenant, userId, borrowerId));
        }


        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateBorrowerConsent(BorrowerConsentModel model) // todo dani consent hash is not matched in this action , do we need this action
        {
            var tenant = (TenantModel) HttpContext.Items[Constants.COLABA_TENANT];
            var userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _borrowerConsentService.AddOrUpdate(tenant, model,
                Convert.ToString(HttpContext.Connection.RemoteIpAddress), userId)); // TODO sohail IP Address
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateBorrowerConsents(BorrowerMultipleConsentsBase model)
        {
            var tenant = (TenantModel) HttpContext.Items[Constants.COLABA_TENANT];
            var userId = int.Parse(User.FindFirst("UserId").Value);

            //var tenantConsents = _borrowerConsentService.GetAllConsentType(tenant);
            var tenantConsents = await _borrowerConsentService.GetBorrowerConsent(tenant,userId,model.BorrowerId);
            if (tenantConsents != null)
            {
                //var consentHash = this._infoService.ComputeConsentHash(tenantConsents.ConsentHash)
                if (model.ConsentHash != tenantConsents.ConsentHash)
                    return BadRequest(new ErrorModel
                        {Message = "Consent hash does not match.", Code = (int) HttpStatusCode.BadRequest});

                var modelToSave = new BorrowerMultipleConsentsModel
                {
                    BorrowerId = model.BorrowerId,
                    LoanApplicationId = model.LoanApplicationId,
                    State = model.State,
                    ConsentHash = model.ConsentHash,
                    IsAccepted = model.IsAccepted
                };

                modelToSave.BorrowerConsents = tenantConsents.ConsentList
                    .Select(consent => new BorrowerConsentPostModel
                    {
                        Description = consent.Description,
                        ConsentTypeId = consent.Id
                    }).ToList();
                var results = await _borrowerConsentService.AddOrUpdateMultipleConsents(tenant, modelToSave,
                    Convert.ToString(HttpContext.Connection.RemoteIpAddress), userId);
                if (results == -1) // -1 equals to already accepted.
                    return BadRequest(new ErrorModel
                        {Message = "Consent already accepted by borrower.", Code = (int) HttpStatusCode.BadRequest});
                return Ok(results); // TODO sohail IP Address
            }

            return BadRequest(new ErrorModel
                {Message = "Cannot fetch tenant consents.", Code = (int) HttpStatusCode.BadRequest});
            //return Ok(await this._borrowerConsentService.AddOrUpdateMultipleConsents(tenant, model, Convert.ToString(HttpContext.Connection.RemoteIpAddress), userId)); // TODO sohail IP Address
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> IsRelationAlreadyMapped(int loanApplicationId, int borrowerId)
        {
            var tenant = (TenantModel) HttpContext.Items[Constants.COLABA_TENANT];
            var userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _borrowerService.IsRelationAlreadyMapped(tenant, userId, loanApplicationId, borrowerId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllBorrower(int loanApplicationId)
        {
            var tenant = (TenantModel) HttpContext.Items[Constants.COLABA_TENANT];
            var userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _borrowerService.GetAllBorrower(tenant, userId, loanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMaritalStatus(int loanApplicationId, int borrowerId)
        {
            var tenant = (TenantModel) HttpContext.Items[Constants.COLABA_TENANT];
            var userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _borrowerService.GetMaritalStatus(tenant, userId, loanApplicationId, borrowerId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateMaritalStatus(BorrowerMaritalStatusModel model)
        {
            var tenant = (TenantModel) HttpContext.Items[Constants.COLABA_TENANT];
            var userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _borrowerService.AddOrUpdateMaritalStatus(tenant, userId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBorrowerInfo(int loanApplicationId, int borrowerId)
        {
            var tenant = (TenantModel) HttpContext.Items[Constants.COLABA_TENANT];
            var userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _borrowerService.GetBorrowerInfo(tenant, userId, loanApplicationId, borrowerId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> PopulatePrimaryBorrowerInfo()
        {
            var tenant = (TenantModel) HttpContext.Items[Constants.COLABA_TENANT];
            var userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _borrowerService.PopulatePrimaryBorrowerInfo(tenant, userId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateBorrowerInfo(BorrowerInfoModel model)
        {
            var tenant = (TenantModel) HttpContext.Items[Constants.COLABA_TENANT];
            var userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _borrowerService.AddOrUpdateBorrowerInfo(tenant, userId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBorrowerAddress(int loanApplicationId, int borrowerId)
        {
            var tenant = (TenantModel) HttpContext.Items[Constants.COLABA_TENANT];
            var userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _borrowerService.GetBorrowerAddress(tenant, userId, loanApplicationId, borrowerId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdateAddress(BorrowerAddressModel model)
        {
            var tenant = (TenantModel) HttpContext.Items[Constants.COLABA_TENANT];
            var userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _borrowerService.AddOrUpdateAddress(tenant, userId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteSecondaryBorrower(DeleteBorrowerModel model)
        {
            var tenant = (TenantModel) HttpContext.Items[Constants.COLABA_TENANT];
            var userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _borrowerService.DeleteSecondaryBorrower(tenant, userId, model.LoanApplicationId,
                model.BorrowerId, model.State));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBorrowerAcceptedConsents([FromQuery] BorrowerAcceptedConsentsGetModel model)
        {
            var tenant = HttpContext.Items[Constants.COLABA_TENANT] as TenantModel;
            var userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _borrowerConsentService.GetBorrowerAcceptedConsents(tenant, userId, model.BorrowerId,
                model.LoanApplicationId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBorrowersForFirstReview([FromQuery] LoanApplicationIdModel model)
        {
            var tenant = (TenantModel) HttpContext.Items[Constants.COLABA_TENANT];
            var userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _borrowerService.GetBorrowersForFirstReview(tenant, userId, model.LoanApplicationId));
        }
    }
}