using System;
using System.Collections.Concurrent;
using System.Security.Claims;
using LoanApplication.API.Controllers;
using LoanApplication.Tests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.Tests.Extensions
{
    public static class ControllerExtensions
    {
        public static void MockHttpContext(this ControllerBase controller, int tenantId, int userId)
        {
            TenantModel tenantModel = ObjectHelper.GetTenantModel(tenantId);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("UserId", userId.ToString()) }, "mock"));

            var contextMock = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = tenantModel;

            controller.ControllerContext.HttpContext = contextMock.HttpContext;
        }

        public static void MockHttpContext(this BaseController controller, int tenantId, int userId)
        {
            TenantModel tenantModel = ObjectHelper.GetTenantModel(tenantId);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("UserId", userId.ToString()) }, "mock"));

            var contextMock = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = tenantModel;

            controller.ControllerContext.HttpContext = contextMock.HttpContext;
        }
    }
}