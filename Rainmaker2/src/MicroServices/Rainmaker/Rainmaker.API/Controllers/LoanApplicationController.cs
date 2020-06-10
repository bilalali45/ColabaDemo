using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rainmaker.Service;
using Rainmaker.Service.Helpers;
using RainMaker.Common;
using RainMaker.Service;

namespace Rainmaker.API.Controllers
{
    [ApiController]
    [Route("api/RainMaker/[controller]")]
    public class LoanApplicationController : Controller
    {
        private readonly ILoanApplicationService loanApplicationService;
        private readonly ICommonService commonService;
        private readonly IFtpHelper ftp;
        public LoanApplicationController(ILoanApplicationService loanApplicationService,ICommonService commonService, IFtpHelper ftp)
        {
            this.loanApplicationService = loanApplicationService;
            this.commonService = commonService;
            this.ftp = ftp;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLoanInfo(int loanApplicationId)
        {
            var loanApplication = await loanApplicationService.GetLoanSummary(loanApplicationId);
            return Ok(loanApplication);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLOInfo(int loanApplicationId, int businessUnitId)
        {
            var lo = await loanApplicationService.GetLOInfo(loanApplicationId,businessUnitId);
            return Ok(lo);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPhoto(string photo, int businessUnitId)
        {
            var remoteFilePath = await commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.FtpEmployeePhotoFolder, businessUnitId) + "/" + photo;

            var imageData = await ftp.DownloadStream(remoteFilePath);

            if (imageData != null)

                return File(imageData, "image/jpeg");

            else
                return null;
        }
    }
}