using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rainmaker.Service;

namespace Rainmaker.API.Controllers
{
    [ApiController]
    [Route(template: "api/[controller]")]
    public class MembershipController : Controller
    {
        private readonly IMembershipService _membershipService;


        public MembershipController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }


        [HttpGet(template: "[action]")]
        public  IActionResult ValidateUser(string userName,
                                                  string password,
                                                  bool employee = false)
        {
            var loanApplication =  _membershipService.ValidateUser(userName,
                                                                        password,
                                                                        employee);
            return Ok(value: loanApplication);
        }
    }
}