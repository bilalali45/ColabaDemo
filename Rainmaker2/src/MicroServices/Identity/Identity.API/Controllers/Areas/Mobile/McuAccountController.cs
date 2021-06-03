using System;
using System.Net;
using Identity.Model;
using Identity.Model.Mobile;
using Identity.Service.Mobile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Identity.CustomAttributes;
using Identity.Service.Helpers.Interfaces;
using Identity.Service.Mobile.Models;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;

namespace Identity.Controllers.Areas.Mobile
{
    public class McuAccountController : McuBaseController
    {
        private readonly IMcuAccountService _mcuService;
        private const string IntermediateUserId = "IntermediateUserId";
        public McuAccountController(IMcuAccountService mcuService)
        {
            _mcuService = mcuService;
        }
        [HttpGet("[action]")]
        [Route("/")]
        public string Index()
        {
            return "Mcu Mobile Account is running";
        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        [Route("/")]

        //   [ServiceFilter(typeof(ValidateRecaptchaAttribute))]
        //[ResolveMcuMobileTenant]
        public async Task<IActionResult> Signin(MobileSigninModel model)
        {
            //TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            var response = await _mcuService.Signin(model);
            if (response.Code != "200")
                return BadRequest(response);
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("[action]")]
        [Route("/")]
        public async Task<IActionResult> ForgotPasswordRequest(ForgotPasswordRequestModel model)
        {
            var response = await _mcuService.ForgotPasswordRequest(model);
            if (response.Code != "200")
                return BadRequest(response);
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("[action]")]
        [Route("/")]
        public async Task<IActionResult> ForgotPasswordResponse(ForgotPasswordResponseModel model)
        {
            
            var response = await _mcuService.ForgotPasswordResponse(model);
            if (response.Code != "200")
                return BadRequest(response);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [Route("/")]
        public async Task<IActionResult> IsPasswordLinkExpired(int userId, string key)
        {
           
            var response = await _mcuService.IsPasswordLinkExpired(userId, key);
            return Ok(response);
        }

        [RequiresIntermediateToken]
        [ResolveMcuIntermediateTenant]
        [HttpPost("[action]")]
        [Route("/")]
        public async Task<IActionResult> SendTwoFa()
        {
            var tenant = (TenantModel)HttpContext.Items[key: Constants.COLABA_TENANT];
            int.TryParse(Convert.ToString(HttpContext.Items[IntermediateUserId]), out var userId);

            var verifiedNumber = await _mcuService.GetVerifiedMobileNumber(userId, tenant);
            if (string.IsNullOrEmpty(verifiedNumber))
            {
                return NotFound(new ApiResponse()
                {
                    Message = "Verified mobile number not found.",
                    Code = Convert.ToString((int)HttpStatusCode.NotFound),
                    Data = null,
                    Status = Convert.ToString(HttpStatusCode.NotFound)
                });
            }
            var response = await _mcuService.SendTwoFaToNumber(verifiedNumber, userId, tenant,
                Request.HttpContext.Connection.RemoteIpAddress.ToString());
            return Ok(response);
        }

        [RequiresIntermediateToken]
        [ResolveMcuIntermediateTenant]
        [HttpPost("[action]")]
        [Route("/")]
        public async Task<IActionResult> SendTwoFaToNumber([FromQuery] SendTwoFaToNumberModel model)
        {
            var tenant = (TenantModel)HttpContext.Items[key: Constants.COLABA_TENANT];
            int.TryParse(Convert.ToString(HttpContext.Items[IntermediateUserId]), out var userId);
            var response = await _mcuService.SendTwoFaToNumber(model.PhoneNumber, userId, tenant, Request.HttpContext.Connection.RemoteIpAddress.ToString());
            return Ok(response);
        }

        [RequiresIntermediateToken]
        [ResolveMcuIntermediateTenant]
        [HttpPost("[action]")]
        [Route("/")]
        public async Task<IActionResult> VerifyTwoFa(VerifyTwoFaModel model)
        {
            var tenant = (TenantModel)HttpContext.Items[key: Constants.COLABA_TENANT];
            int.TryParse(Convert.ToString(HttpContext.Items[IntermediateUserId]), out var userId);
            var response = await _mcuService.VerifyTwoFa(model, userId, tenant,
                Request.HttpContext.Connection.RemoteIpAddress.ToString());
            return Ok(response);
        }

        [RequiresIntermediateToken]
        [ResolveMcuIntermediateTenant]
        [HttpGet("[action]")]
        [Route("/")]
        public async Task<IActionResult> GetMcuTenantTwoFaValues()
        {
            var tenant = (TenantModel)HttpContext.Items[key: Constants.COLABA_TENANT];
            int.TryParse(Convert.ToString(HttpContext.Items[IntermediateUserId]), out var userId);
            var response = await _mcuService.GetTwoFaValuesToSkip(userId, tenant);
            return Ok(response);
        }

        [RequiresIntermediateToken]
        [ResolveMcuIntermediateTenant]
        [HttpPost("[action]")]
        [Route("/")]
        public async Task<IActionResult> SkipTwoFa()
        {
            var tenant = (TenantModel)HttpContext.Items[key: Constants.COLABA_TENANT];
            int.TryParse(Convert.ToString(HttpContext.Items[IntermediateUserId]), out var userId);
            var response = await _mcuService.SkipTwoFa(userId, tenant);
            return Ok(response);
        }

        [Authorize(Roles = "MCU")]
        [ResolveMcuMobileTenant]
        [HttpPost("[action]")]
        [Route("/")]
        public async Task<IActionResult> DontAskTwoFa()
        {
            var tenant = (TenantModel)HttpContext.Items[key: Constants.COLABA_TENANT];
            string token = Convert.ToString(Request.Headers["Authorization"]);
            var response = await _mcuService.DontAskTwoFa(token, tenant.Id, tenant.Code);
            return Ok(response);
        }
    }
}
