using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rainmaker.Service;

namespace Rainmaker.API.Controllers
{
    [Route("api/rainmaker/[controller]")]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAssignedUsers(int loanApplicationId)
        {
            return Ok(await _notificationService.GetAssignedUsers(loanApplicationId));
        }
    }
}
