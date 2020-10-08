using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
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

            Request.Headers.TryGetValue("CorrelationId",
                                        out StringValues value);
            var userProfile = _membershipService.ValidateUser(userName: request.UserName,
                                                              password: request.Password,
                                                              employee: request.Employee);

            if (userProfile != null)
                return Ok(value: userProfile);
            return NotFound();
        }


        [HttpPost(template: "[action]")]
        public IActionResult GetUser(GetUserRequest request)
        {
            RainMaker.Entity.Models.UserProfile userProfile;
            if (!request.Employee)
                userProfile = _membershipService.GetUser(userName: request.UserName
                                                        );
            else
                userProfile = _membershipService.GetEmployeeUser(userName: request.UserName
                                                                );

            if (userProfile != null)
                return Ok(value: userProfile);
            return NotFound();
        }
    }
}