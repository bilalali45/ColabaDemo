using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Rainmaker.Model;
using Rainmaker.Service;
using System.Threading.Tasks;

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
        public async Task<IActionResult> ValidateUser(ValidateUserRequest request)
        {

            var tenantId = 1; // to be read from DB once we have tenant entity defined in rainmaker
            var userProfile = await _membershipService.ValidateUser(tenantId: tenantId,userName: request.UserName,
                                                              password: request.Password,
                                                              employee: request.Employee);

            if (userProfile != null)
                return Ok(value: userProfile);
            return NotFound(new ErrorModel { Message = "unable to find user" });
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
            return NotFound(new ErrorModel { Message = "unable to find user" });
        }
    }
}