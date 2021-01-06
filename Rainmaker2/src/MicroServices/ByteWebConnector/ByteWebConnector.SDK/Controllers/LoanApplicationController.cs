using ByteWebConnector.SDK.Mismo;
using ByteWebConnector.SDK.Models.ControllerModels.Document.Response;
using ByteWebConnector.SDK.Models.ControllerModels.Loan;
using LOSAutomation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ByteWebConnector.SDK.Models;
using ByteWebConnector.SDK.Models.Rainmaker;
using System.Collections.Generic;
using ByteWebConnector.SDK.Abstraction;

namespace ByteWebConnector.SDK.Controllers
{
    [Route(template: "api/ByteWebConnectorSdk/[controller]")]
    [ApiController]
    public class LoanApplicationController : ControllerBase
    {
        private readonly ILogger<LoanApplicationController> _logger;
        private readonly IByteSDKHelper _byteSDKHelper;

        public LoanApplicationController(IByteSDKHelper byteSdkHelper, ILogger<LoanApplicationController> logger)
        {
            this._logger = logger;
            this._byteSDKHelper = byteSdkHelper;
        }

        [Route(template: "[action]")]
        [HttpPost]
        public async Task<IActionResult> PostLoanApplicationToByte(SdkSendLoanApplicationRequest requestModel)
        {
            var response = new ApiResponse<SendSdkLoanApplicationResponse>(); 
            try
            {
                response = this._byteSDKHelper.CreateByteFile(requestModel);

            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Code = ApiResponseStatus.Error;
                _logger.LogInformation(e.Message);
                _logger.LogInformation(e.StackTrace);
            }

            return Ok(response);
        }


        [Route(template: "[action]")]
        [HttpPost]
        public async Task<IActionResult> GetLoanApplicationStatusName(SdkLoanApplicationStatusNameRequest request)
        {
            var response = new ApiResponse<string>();
            try
            {
                response = this._byteSDKHelper.GetLoanStatusName(request);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Code = ApiResponseStatus.Error;
                _logger.LogInformation(e.Message);
                _logger.LogInformation(e.StackTrace);
            }
            return Ok(response);
        }
    }
}
