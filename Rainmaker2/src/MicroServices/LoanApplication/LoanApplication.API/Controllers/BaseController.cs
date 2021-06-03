using Microsoft.AspNetCore.Mvc;
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.API.Controllers
{
    [Route("api/loanapplication/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public TenantModel Tenant => (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];

        public int UserId => int.Parse(User.FindFirst("UserId").Value);

    }
}