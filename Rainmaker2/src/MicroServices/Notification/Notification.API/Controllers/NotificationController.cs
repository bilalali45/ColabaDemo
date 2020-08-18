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
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpPost("[action]")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Add(NotificationModel model)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);

            return Ok(await _notificationService.Add(model,userProfileId,tenantId, Request.Headers["Authorization"].Select(x => x.ToString())));
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> GetPaged(int pageSize,long lastId,int mediumId)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            if (lastId == -1)
            {
                lastId = long.MaxValue;
            }
            return Ok(await _notificationService.GetPaged(pageSize,lastId,mediumId,userProfileId));
        }

        [HttpPut("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> Read(NotificationRead model)
        {
            await _notificationService.Read(model.id);
            return Ok();
        }

        [HttpPut("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> Delete(NotificationDelete model)
        {
            await _notificationService.Delete(model.id);
            return Ok();
        }
        [HttpPut("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> Undelete(NotificationUndelete model)
        {
            return Ok(await _notificationService.Undelete(model.id));
        }
        [HttpPut("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> DeleteAll()
        {
            await _notificationService.DeleteAll();
            return Ok();
        }
    }
}
