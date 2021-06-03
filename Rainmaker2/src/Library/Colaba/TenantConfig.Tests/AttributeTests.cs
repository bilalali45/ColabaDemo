using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TenantConfig.Common;
using Xunit;
using TenantConfig.Common.DistributedCache;
using System.Security.Claims;

namespace TenantConfig.Tests
{
    public class AttributeTests
    {
        [Fact]
        public async Task ResolveWebTenantAttributeNoHeader()
        {
            //Arrange
            var controller = Mock.Of<Controller>();

            var httpContext = new DefaultHttpContext();
            
            var actionContext = new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            };
            var metadata = new List<IFilterMetadata>();

            var context = new ActionExecutingContext(
                actionContext,
                metadata,
                new Dictionary<string, object>(),
                controller);

            ActionExecutionDelegate next = () => {
                var ctx = new ActionExecutedContext(actionContext, metadata, controller);
                return Task.FromResult(ctx);
            };
            var attribute = new ResolveWebTenantAttribute();
            //Act
            await Assert.ThrowsAsync<Exception>(()=> attribute.OnActionExecutionAsync(context, next));
        }
        [Fact]
        public async Task ResolveWebTenantAttributeTenantFound()
        {
            //Arrange
            var controller = Mock.Of<Controller>();
            TenantModel model = new TenantModel
            {
                Id = 1,
                Code = "1",
                Urls = new List<UrlModel>(),
                Branches = new List<BranchModel>()
                {
                    new BranchModel
                    {
                        Id=1,
                        Code="1",
                        IsCorporate=true,
                        LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Add(Constants.COLABA_TENANT,Newtonsoft.Json.JsonConvert.SerializeObject(model));
            var actionContext = new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            };
            var metadata = new List<IFilterMetadata>();

            var context = new ActionExecutingContext(
                actionContext,
                metadata,
                new Dictionary<string, object>(),
                controller);

            ActionExecutionDelegate next = () => {
                var ctx = new ActionExecutedContext(actionContext, metadata, controller);
                return Task.FromResult(ctx);
            };
            var attribute = new ResolveWebTenantAttribute();
            //Act
            await attribute.OnActionExecutionAsync(context, next);
            //Assert
            Assert.Equal(1, ((TenantModel)httpContext.Items[Constants.COLABA_TENANT]).Id);
        }
        [Fact]
        public async Task ResolveWebTenantAttributeTenantCodeDoesNotMatch()
        {
            //Arrange
            var controller = Mock.Of<Controller>();
            TenantModel model = new TenantModel
            {
                Id = 1,
                Code = "1",
                Urls = new List<UrlModel>(),
                Branches = new List<BranchModel>()
                {
                    new BranchModel
                    {
                        Id=1,
                        Code="1",
                        IsCorporate=true,
                        LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };
            var httpContext = new DefaultHttpContext();
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new List<Claim> { new Claim("TenantCode","2"), new Claim("BranchCode", "1") },"Bearer");
            httpContext.User = new ClaimsPrincipal(claimsIdentity);
            httpContext.Request.Headers.Add(Constants.COLABA_TENANT, Newtonsoft.Json.JsonConvert.SerializeObject(model));
            var actionContext = new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            };
            var metadata = new List<IFilterMetadata>();

            var context = new ActionExecutingContext(
                actionContext,
                metadata,
                new Dictionary<string, object>(),
                controller);

            ActionExecutionDelegate next = () => {
                var ctx = new ActionExecutedContext(actionContext, metadata, controller);
                return Task.FromResult(ctx);
            };
            var attribute = new ResolveWebTenantAttribute();
            //Assert
            await Assert.ThrowsAsync<Exception>(() => attribute.OnActionExecutionAsync(context, next));
        }
        [Fact]
        public async Task ResolveWebTenantAttributeBranchCodeDoesNotMatch()
        {
            //Arrange
            var controller = Mock.Of<Controller>();
            TenantModel model = new TenantModel
            {
                Id = 1,
                Code = "1",
                Urls = new List<UrlModel>(),
                Branches = new List<BranchModel>()
                {
                    new BranchModel
                    {
                        Id=1,
                        Code="1",
                        IsCorporate=true,
                        LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };
            var httpContext = new DefaultHttpContext();
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new List<Claim> { new Claim("TenantCode", "1"), new Claim("BranchCode", "2") }, "Bearer");
            httpContext.User = new ClaimsPrincipal(claimsIdentity);
            httpContext.Request.Headers.Add(Constants.COLABA_TENANT, Newtonsoft.Json.JsonConvert.SerializeObject(model));
            var actionContext = new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            };
            var metadata = new List<IFilterMetadata>();

            var context = new ActionExecutingContext(
                actionContext,
                metadata,
                new Dictionary<string, object>(),
                controller);

            ActionExecutionDelegate next = () => {
                var ctx = new ActionExecutedContext(actionContext, metadata, controller);
                return Task.FromResult(ctx);
            };
            var attribute = new ResolveWebTenantAttribute();
            //Assert
            await Assert.ThrowsAsync<Exception>(() => attribute.OnActionExecutionAsync(context, next));
        }
    }
}
