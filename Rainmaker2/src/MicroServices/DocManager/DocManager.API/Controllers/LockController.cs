using DocManager.Model;
using DocManager.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocManager.API.Controllers
{
    [Route("api/docmanager/[controller]")]
    [ApiController]
    [Authorize(Roles ="MCU")]
    public class LockController : Controller
    {

        private readonly ILockService lockService;
        public LockController(ILockService lockService)
        {
            this.lockService = lockService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AcquireLock(LockModel lockModel)
        {
            var UserId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var UserName = User.FindFirst("FirstName").Value.ToString() + ' ' + User.FindFirst("LastName").Value.ToString();
            var l = await lockService.AcquireLock(lockModel, UserId, UserName);
            if (l == null)
                throw new DocManagerException("Unable to check lock");
            if (l.lockUserId == UserId)
                return Ok(l);
            else
                return BadRequest(l);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RetainLock(LockModel lockModel)
        {
            var UserId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var UserName = User.FindFirst("FirstName").Value.ToString() + ' ' + User.FindFirst("LastName").Value.ToString();
            var l = await lockService.RetainLock(lockModel, UserId, UserName);
            if (l == null)
                return BadRequest();
            else
                return Ok(l);
        }
    }
}
