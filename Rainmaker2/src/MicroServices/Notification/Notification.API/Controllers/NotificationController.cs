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

        public NotificationController(INotificationService notificationService,
            IHubContext<ServerHub, IClientHub> context)
        {
            _notificationService = notificationService;
            _context = context;
        }
        [HttpPost("[action]")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Add(NotificationModel model)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            long id = await _notificationService.Add(model, userProfileId, tenantId,
                Request.Headers["Authorization"].Select(x => x.ToString()));
            await SendNotification(id);
            return Ok(id);
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
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> TestSignalR()
        {
            await ServerHub.TestSignalR(_context);
            return Ok();
        }

        private async Task SendNotification(long id)
        {
            NotificationObject notificationObject = await _notificationService.GetByIdForTemplate(id);
            foreach (var recep in notificationObject.NotificationRecepients)
            {
                foreach (var medium in recep.NotificationRecepientMediums)
                {
                    if (medium.DeliveryModeId == (short)Notification.Common.DeliveryModeEnum.Express &&
                        medium.NotificationMediumid == (int)Notification.Common.NotificationMediumEnum.InApp)
                    {
                        NotificationMediumModel model = new NotificationMediumModel()
                        {
                            id = medium.Id,
                            payload = string.IsNullOrEmpty(medium.SentTextJson) ? new JObject() : JObject.Parse(medium.SentTextJson),
                            status = recep.StatusListEnum.Name
                        };
                        await ServerHub.SendNotification(_context,recep.RecipientId.Value,model);
                    }
                }
            }
        }
    }
}
