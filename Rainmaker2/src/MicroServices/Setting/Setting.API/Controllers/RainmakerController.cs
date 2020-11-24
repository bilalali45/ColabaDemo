using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Setting.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Setting.API.Controllers
{
    [Route("api/Setting/[controller]")]
    [ApiController]
    public class RainmakerController : ControllerBase
    {
        #region Private Variables
        private readonly IRainmakerService _raniRainmakerService;
        #endregion
        #region Constructors
        public RainmakerController(IRainmakerService rainmakerService)
        {
            _raniRainmakerService = rainmakerService;
        }
        #endregion
        #region Action Methods
        #region GetMethods
        [Authorize(Roles = "MCU")]
        [HttpGet(template: "GetUserRoles")]
        public async Task<IActionResult> GetUserRoles()
        {
            var userRolesQuery = await _raniRainmakerService.GetUserRoles(Request.Headers["Authorization"].Select(x => x.ToString()));
            return Ok(value: userRolesQuery);
        }
        #endregion
        #region PostMethods
        [HttpPost("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> UpdateUserRoles(List<Model.UserRole> userRoles)
        {
            var response = await _raniRainmakerService.UpdateUserRoles(userRoles, Request.Headers["Authorization"].Select(x => x.ToString()));
            if (response)
                return Ok();
            return NotFound();
        }
        #endregion
        #endregion
    }
}
