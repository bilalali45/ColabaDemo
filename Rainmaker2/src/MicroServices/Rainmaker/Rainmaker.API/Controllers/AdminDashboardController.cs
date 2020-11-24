using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rainmaker.Service;
using System.Threading.Tasks;

namespace Rainmaker.API.Controllers
{
    [ApiController]
    [Route("api/RainMaker/[controller]")]
    [Authorize(Roles = "MCU")]
    public class AdminDashboardController : Controller
    {
        private readonly ISitemapService sitemapService;
        private readonly IUserProfileService userProfileService;
        private readonly ILoanApplicationService loanApplicationService;

        public AdminDashboardController(ISitemapService sitemapService,IUserProfileService userProfileService, ILoanApplicationService loanApplicationService)
        {
            this.sitemapService = sitemapService;
            this.userProfileService = userProfileService;
            this.loanApplicationService = loanApplicationService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetMenu()
        {            
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());

            var userProfile = await userProfileService.GetUserProfile(userProfileId);

            if (userProfile.IsSystemAdmin)
            {
                var result = await sitemapService.GetSystemAdminMenu();
                return Ok(result);
            }
            else
            {
                var result = await sitemapService.GetMenu(userProfileId);
                return Ok(result);
            }
               
           
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLoanInfo(int loanApplicationId)
        {
            var loanApplication = await loanApplicationService.GetAdminLoanSummary(loanApplicationId);
            return Ok(loanApplication);
        }

    }
}
