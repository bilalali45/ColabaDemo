using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;

namespace TenantConfig.Common
{
    public class ResolveWebTenantAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey(Constants.COLABA_TENANT))
                throw new Exception($"Unable to find {Constants.COLABA_TENANT} header");
            var tenant = Newtonsoft.Json.JsonConvert.DeserializeObject<TenantModel>(context.HttpContext.Request.Headers[Constants.COLABA_TENANT][0]);
            if(context.HttpContext.User.Identity.IsAuthenticated)
            {
                var tenantCode = context.HttpContext.User.FindFirst("TenantCode").Value;
                var branchCode = context.HttpContext.User.FindFirst("BranchCode").Value;
                if (tenantCode != tenant.Code)
                    throw new Exception("Tenant in token does not match tenant in url");
                if (branchCode != tenant.Branches[0].Code)
                    throw new Exception("Branch in token does not match branch in url");
            }
            context.HttpContext.Items[Constants.COLABA_TENANT] = tenant;
            context.HttpContext.Items["TenantId"] = tenant.Id;
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
