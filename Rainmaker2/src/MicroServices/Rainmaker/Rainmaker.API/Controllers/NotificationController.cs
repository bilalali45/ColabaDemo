using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rainmaker.Service;
using System.Threading.Tasks;

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
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLoanInfo(int loanApplicationId)
        {
            var loanApplication = await _notificationService.GetLoanSummary(loanApplicationId);
            return Ok(loanApplication);
        }
    }
}
