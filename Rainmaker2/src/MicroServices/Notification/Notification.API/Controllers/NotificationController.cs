using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("[action]")]
        public async Task<IActionResult> Test()
        {
            return Ok(await _notificationService.Test());
        }
    }
}
