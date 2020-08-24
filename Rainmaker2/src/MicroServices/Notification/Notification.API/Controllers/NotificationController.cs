using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using Notification.Entity.Models;
using Notification.Model;
using Notification.Service;

namespace Notification.API.Controllers
{
    [Route("api/Notification/[controller]")]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly IHubContext<ServerHub, IClientHub> _context;
        private readonly IRedisService _redisService;
        public NotificationController(INotificationService notificationService,
            IHubContext<ServerHub, IClientHub> context,
            IRedisService redisService)
        {
            _notificationService = notificationService;
            _context = context;
            _redisService = redisService;
        }
        [HttpPost("[action]")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Add(NotificationModel model)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            model.DateTime = DateTime.UtcNow;
            model.userId = userProfileId;
            model.tenantId = tenantId;
            TenantSetting setting = await _notificationService.GetTenantSetting(tenantId,model.NotificationType);
            if (setting.DeliveryModeId == (short) Notification.Common.DeliveryModeEnum.Express)
            {
                long id = await _notificationService.Add(model, userProfileId, tenantId,setting);
                await _redisService.SendNotification(id);
                return Ok(id);
            }
            else if (setting.DeliveryModeId == (short)Notification.Common.DeliveryModeEnum.Queued)
            {
                await _redisService.InsertInCache(model);
            }
            return Ok(-1L);
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> GetCount()
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            return Ok(await _notificationService.GetCount(userProfileId));
        }
        [HttpGet("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> GetPaged(long lastId, int mediumId,int pageSize=10)
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
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            await _notificationService.Read(model.ids,userProfileId);
            return Ok();
        }

        [HttpPut("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> Seen(NotificationSeen model)
        {
            await _notificationService.Seen(model.ids);
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
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> TestSignalR()
        {
            await ServerHub.TestSignalR(_context);
            return Ok();
        }
        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult DumpSignalR()
        {
            return Ok(ClientConnection<int>._connections);
        }
    }
}
