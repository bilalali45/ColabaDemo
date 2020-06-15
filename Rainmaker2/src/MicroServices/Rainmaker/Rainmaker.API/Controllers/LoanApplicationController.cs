using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rainmaker.Service;
using Rainmaker.Service.Helpers;
using RainMaker.Common;
using RainMaker.Common.FTP;
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
        public async Task<string> GetPhoto(string photo, int businessUnitId)
        {
            var remoteFilePath = await commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.FtpEmployeePhotoFolder, businessUnitId) + "/" + photo;
        
            var imageData = await ftp.DownloadStream(remoteFilePath);
            var data = new byte[imageData.Length];
            int res = 0;
            do
            {
                int i = imageData.Read(data, res, data.Length-res);
                res += i;
            }
            while (res<data.Length-1);

            if (imageData != null)

                return Convert.ToBase64String(data);

            else
                return null;
        }
    }
}