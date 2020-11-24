using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notification.Model;
using Notification.Service;

namespace Notification.API.Controllers
{
    [Route("api/Notification/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService settingService;
        public SettingController(ISettingService settingService)
        {
            this.settingService = settingService;
        }
        [Authorize(Roles = "MCU")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSettings()
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            int tenantId = int.Parse(User.FindFirst("TenantId").Value.ToString());
            return Ok(await settingService.GetSettings(tenantId,userProfileId));
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> UpdateSettings(UpdateSettingModel updateSettingModel)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            int tenantId = int.Parse(User.FindFirst("TenantId").Value.ToString());
            await settingService.UpdateSettings(tenantId, userProfileId, updateSettingModel.notificationTypeId,updateSettingModel.deliveryModeId,updateSettingModel.delayedInterval);
            return Ok();
        }
    }
}
