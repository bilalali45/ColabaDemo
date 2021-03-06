using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Moq;
using Rainmaker.API.Controllers;
using Rainmaker.Service;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Routing;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using RainMaker.Entity.Models;
using Microsoft.EntityFrameworkCore;
using RainMaker.Data;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Rainmaker.Model;

namespace Rainmaker.Test
{
    public class AdminDashboardTest
    {
        [Fact]
        public async Task TestGetMenuControllerSystemAdminTrue()
        {
            //Arrange
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();
            Mock<ISitemapService> mockSitemapService = new Mock<ISitemapService>();

            var adminDashboardController = new AdminDashboardController(mockSitemapService.Object, mockUserProfileService.Object,null);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            adminDashboardController.ControllerContext = context;

            UserProfile userProfile = new UserProfile();
            userProfile.IsSystemAdmin = true;
            mockUserProfileService.Setup(x => x.GetUserProfile(It.IsAny<int>())).ReturnsAsync(userProfile);

            //Act
            IActionResult result = await adminDashboardController.GetMenu();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestGetMenuControllerSystemAdminFalse()
        {
            //Arrange
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();
            Mock<ISitemapService> mockSitemapService = new Mock<ISitemapService>();

            var adminDashboardController = new AdminDashboardController(mockSitemapService.Object, mockUserProfileService.Object,null);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            adminDashboardController.ControllerContext = context;

            UserProfile userProfile = new UserProfile();
            mockUserProfileService.Setup(x => x.GetUserProfile(It.IsAny<int>())).ReturnsAsync(userProfile);

            //Act
            IActionResult result = await adminDashboardController.GetMenu();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestGetSystemAdminMenuService()
        {
            //Arrange
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            RainMaker.Entity.Models.Sitemap sitemap = new RainMaker.Entity.Models.Sitemap()
            {
                Id = 2,
                IsParent = true,
                IsActive = true,
                EntityTypeId = 1,
                IsDeleted = false,
                DisplayOrder = 1,
                IsPermissive = false
            };
            dataContext.Set<RainMaker.Entity.Models.Sitemap>().Add(sitemap);

            UserPermission userPermission = new UserPermission()
            {
                Id = 2,
                Name = "Salman",
                IsActive = true,
                EntityTypeId = 1,
                IsDeleted = false,
                IsSystem = true
            };
            dataContext.Set<UserPermission>().Add(userPermission);

            dataContext.SaveChanges();

            ISitemapService sitemapService = new SitemapService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            var result = await sitemapService.GetSystemAdminMenu();

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task TestGetMenuService()
        {
            //Arrange
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            UserProfile userProfile = new UserProfile()
            {
                Id = 271,
                UserName = "talha.tayyab",
                LoweredUserName = "talha.tayyab",
                IsAnonymous = false,
                LastActivityDateUtc = DateTime.UtcNow,
                DisplayOrder = 1,
                IsActive = true,
                IsSystem = true,
                EntityTypeId = 1,
                PasswordFormatId = 1,
                IsDeleted = false,
                IsSystemAdmin = true
            };
            dataContext.Set<UserProfile>().Add(userProfile);

            RainMaker.Entity.Models.UserRole userRole = new RainMaker.Entity.Models.UserRole()
            {
                Id = 271,
                RoleName = "Executives",
                LoweredRoleName = "executives",
                DisplayOrder = 1,
                IsActive = true,
                IsSystem = true,
                EntityTypeId = 1,
                IsDeleted = false
            };
            dataContext.Set<RainMaker.Entity.Models.UserRole>().Add(userRole);

            RainMaker.Entity.Models.Sitemap sitemap = new RainMaker.Entity.Models.Sitemap()
            {
                Id = 271,
                IsParent = true,
                IsActive = true,
                EntityTypeId = 1,
                IsDeleted = false,
                DisplayOrder = 1,
                IsPermissive = false
            };
            dataContext.Set<RainMaker.Entity.Models.Sitemap>().Add(sitemap);

            UserPermission userPermission = new UserPermission()
            {
                Id = 271,
                Name = "Talha",
                IsActive = true,
                EntityTypeId = 1,
                IsDeleted = false,
                IsSystem = true
            };
            dataContext.Set<UserPermission>().Add(userPermission);

            dataContext.SaveChanges();

            ISitemapService sitemapService = new SitemapService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            var result = await sitemapService.GetMenu(1);

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task TestGetLoanInfoController()
        {
            //Arrange
            Mock<ILoanApplicationService> mockLoanApplicationService = new Mock<ILoanApplicationService>();

            var adminDashboardController = new AdminDashboardController(null, null, mockLoanApplicationService.Object);

            AdminLoanSummary adminLoanSummary = new AdminLoanSummary();
            adminLoanSummary.CityName = "Karachi";
            adminLoanSummary.CountryName = "Pakistan";
            mockLoanApplicationService.Setup(x => x.GetAdminLoanSummary(It.IsAny<int>())).ReturnsAsync(adminLoanSummary);

            //Act
            IActionResult result = await adminDashboardController.GetLoanInfo(1);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

    }
}
