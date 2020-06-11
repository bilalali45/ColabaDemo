using Microsoft.AspNetCore.Mvc;
using Rainmaker.Service;

namespace Rainmaker.API.Controllers
{
    [ApiController]
    [Route(template: "api/rainmaker/[controller]")]
    public class MembershipController : Controller
    {
        private readonly IMembershipService _membershipService;


        public MembershipController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }


        [HttpPost(template: "[action]")]
        public IActionResult ValidateUser([FromForm] string userName,
                                          [FromForm] string password,
                                          [FromForm] bool employee = false)
        {
            var userProfile = _membershipService.ValidateUser(userName: userName,
                                                              password: password,
                                                              employee: employee);

            if (userProfile != null)
                return Ok(value: userProfile);
            return NotFound();
        }
    }
}