using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Setting.Model;
using Setting.Service;
using System.Linq;
using System.Threading.Tasks;

namespace Setting.API.Controllers
{
    [Route("api/Setting/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        #region Private Variables
        private readonly INotificationService _notificationService;
        #endregion

        #region Constructors
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        #endregion

        #region Action Methods
        #region GetMethods
        [Authorize(Roles = "MCU")]
        [HttpGet(template: "GetSettings")]
        public async Task<IActionResult> GetSettings()
        {
            var settingQuery = await _notificationService.GetSettings(Request.Headers["Authorization"].Select(x => x.ToString()));
            return Ok(value: settingQuery);
        }
        #endregion
        #region PostMethods
        [HttpPost("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> UpdateSettings(UpdateSettingModel updateSettingModel)
        {
            var response = await _notificationService.UpdateSettings(updateSettingModel.notificationTypeId, updateSettingModel.deliveryModeId, updateSettingModel.delayedInterval, Request.Headers["Authorization"].Select(x => x.ToString()));

            if (response)
                return Ok();
            return NotFound();
        }
        #endregion
        #endregion
    }
}
