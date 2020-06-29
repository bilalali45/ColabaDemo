using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rainmaker.Model;
using Rainmaker.Service;

namespace Rainmaker.API.Controllers
{
    [ApiController]
    [Route("api/RainMaker/[controller]")]
    [Authorize(Roles = "MCU")]
    public class AdminDashboardController : Controller
    {
        private readonly ISitemapService sitemapService;
        private readonly IUserProfileService userProfileService;

        public AdminDashboardController(ISitemapService sitemapService,IUserProfileService userProfileService)
        {
            this.sitemapService = sitemapService;
            this.userProfileService = userProfileService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetMenu()
        {            
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());

            var userProfile = await userProfileService.GetUserProfile(userProfileId);

            if(userProfile.IsSystemAdmin)
                return Ok(sitemapService.GeSystemAdmintMenu());
            return Ok(sitemapService.GetMenu(userProfileId));
        }
    }
}
