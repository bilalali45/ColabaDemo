using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using TenantConfig.Common.DistributedCache;

namespace TenantConfig.Common
{
    public class ResolveMcuMobileTenantAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                throw new UnauthorizedAccessException("Authorization token not found.");
            }
            var tenant = Newtonsoft.Json.JsonConvert.DeserializeObject<TenantModel>(context.HttpContext.Request.Headers[Constants.COLABA_TENANT][0]);
            context.HttpContext.Items[Constants.COLABA_TENANT] = tenant;
            context.HttpContext.Items["TenantId"] = tenant.Id;
            await base.OnActionExecutionAsync(context, next);
        }
    }

    public class ResolveMcuIntermediateTenantAttribute : ActionFilterAttribute
    {
        private const string IntermediateUserId = "IntermediateUserId";
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("IntermediateToken"))
            {
                throw new UnauthorizedAccessException("Authorization token not found.");
            }
            var tenant = Newtonsoft.Json.JsonConvert.DeserializeObject<TenantModel>(context.HttpContext.Request.Headers[Constants.COLABA_TENANT][0]);
            context.HttpContext.Items[Constants.COLABA_TENANT] = tenant;
            context.HttpContext.Items["TenantId"] = tenant.Id;
            context.HttpContext.Items[IntermediateUserId] =
                context.HttpContext.Request.Headers[IntermediateUserId][0];
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
