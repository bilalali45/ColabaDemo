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
        public IActionResult ValidateUser(ValidateUserRequest request)
        {
            var userProfile = _membershipService.ValidateUser(userName: request.UserName,
                                                              password: request.Password,
                                                              employee: request.Employee);

            if (userProfile != null)
                return Ok(value: userProfile);
            return NotFound();
        }
    }

    public class ValidateUserRequest

    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Employee { get; set; }
    }
}


 