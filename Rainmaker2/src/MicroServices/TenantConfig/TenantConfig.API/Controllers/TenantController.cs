using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Svg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Model;
using TenantConfig.Service;

namespace TenantConfig.API.Controllers
{
    [Route("api/TenantConfig/[controller]")]
    [ApiController]
    public class TenantController : Controller
    {
        private readonly ITermConditionService _termConditionService;

        public TenantController(ITermConditionService termConditionService)
        {
            _termConditionService = termConditionService;
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        [ResolveWebTenant]
        public async Task<IActionResult> GetRedirectUrl()
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            return Ok(new VerifyModel 
            { 
                Url=$"https://{tenant.Urls[0].Url}/{tenant.Branches[0].Code}/"+(tenant.Branches[0].LoanOfficers.Count<=0?string.Empty: $"{tenant.Branches[0].LoanOfficers[0].Code}/"),
                Path=$"/{tenant.Branches[0].Code}/" 
            });
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        [ResolveWebTenant]
        [ServiceFilter(typeof(ValidateRecaptchaAttribute))]
        public async Task<IActionResult> GetTermCondition(int type)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            return Ok(await _termConditionService.GetTermsConditions(type,tenant.Id,tenant.Branches[0].Id));
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        [ResolveWebTenant]
        [ServiceFilter(typeof(ValidateRecaptchaAttribute))]
        public async Task<IActionResult> GetSetting()
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            return Ok(await _termConditionService.GetSetting(tenant, tenant.Branches[0].Id, tenant.Branches[0].Code));
        }

        [HttpPut("[action]")]
        [AllowAnonymous]
        [ResolveWebTenant]
        [ServiceFilter(typeof(ValidateRecaptchaAttribute))]
        public async Task<IActionResult> PutSetting(SettingModel model)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            await _termConditionService.PutSetting(tenant,model);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> ConvertSvgToPng([FromForm]IFormFile file, [FromForm] int width, [FromForm] int height, [FromForm] string fileName)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            SvgDocument svgDocument = SvgDocument.FromSvg<SvgDocument>(await reader.ReadToEndAsync());
            svgDocument.Width = width;
            svgDocument.Height = height;
            using Bitmap png = svgDocument.Draw();
            MemoryStream memoryStream = new MemoryStream();
            png.Save(memoryStream,ImageFormat.Png);
            memoryStream.Position = 0;
            return File(memoryStream,"image/png",fileName);
        }
    }
}
