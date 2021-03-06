using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Notification.Entity.Models;
using Notification.Model;
using Notification.Service;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

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
            model.Id = await _notificationService.Add(model);
            bool completed = await _redisService.SendNotification(model);
            if (!completed)
                await _redisService.InsertInCache(model);
            return Ok(model.Id);
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
        public async Task<IActionResult> Read([FromBody] NotificationRead model, [FromServices] IConnectionMultiplexer connectionMultiplexer)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var list = await _notificationService.Read(model.ids,userProfileId);
            await ServerHub.NotificationRead(_context, userProfileId, list.ToArray(), connectionMultiplexer);
            return Ok(list);
        }

        [HttpPut("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> Seen([FromBody] NotificationSeen model, [FromServices] IConnectionMultiplexer connectionMultiplexer)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            await _notificationService.Seen(model.ids);
            await ServerHub.NotificationSeen(_context,userProfileId,model.ids.ToArray(), connectionMultiplexer);
            return Ok();
        }

        [HttpPut("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> Delete([FromBody] NotificationDelete model, [FromServices] IConnectionMultiplexer connectionMultiplexer)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            await _notificationService.Delete(model.id);
            await ServerHub.NotificationDelete(_context, userProfileId, model.id, connectionMultiplexer);
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
        public async Task<IActionResult> DeleteAll([FromServices] IConnectionMultiplexer connectionMultiplexer)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            await _notificationService.DeleteAll();
            await ServerHub.NotificationDeleteAll(_context, userProfileId, connectionMultiplexer);
            return Ok();
        }
        [HttpGet("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> GetTenantSetting()
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            return Ok(await _notificationService.GetTenantSetting(tenantId));
        }
        [HttpPost("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> SetTenantSetting(TenantSettingModel model)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            await _notificationService.SetTenantSetting(tenantId,model);
            return Ok();
        }
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> TestSignalR()
        {
            await ServerHub.TestSignalR(_context);
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendNotification(NotificationModel model)
        {
            if(await _redisService.SendNotification(model))
                return Ok();
            return BadRequest();
        }
    }
}
