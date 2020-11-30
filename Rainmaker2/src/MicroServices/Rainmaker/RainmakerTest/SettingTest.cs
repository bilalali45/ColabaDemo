using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Moq;
using Rainmaker.API.Controllers;
using Rainmaker.Model;
using Rainmaker.Service;
using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace Rainmaker.Test
{
    public class SettingTest
    {
        [Fact]
        public async Task TestGetUserRolesController()
        {
            //Arrange
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

            var settingController = new SettingController(mockUserProfileService.Object, null);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new Microsoft.AspNetCore.Mvc.ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            settingController.ControllerContext = context;

            //Act
            IActionResult result = await settingController.GetUserRoles();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestUpdateUserRolesController()
        {
            //Arrange
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

            var settingController = new SettingController(mockUserProfileService.Object, null);

            List<Model.UserRole> lstUserRoles = new List<Model.UserRole>();
            Model.UserRole userRole = new Model.UserRole();
            userRole.RoleName = "Executives";
            userRole.RoleId = 2;
            userRole.IsRoleAssigned = true;
            lstUserRoles.Add(userRole);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new Microsoft.AspNetCore.Mvc.ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            settingController.ControllerContext = context;

            //Act
            IActionResult result = await settingController.UpdateUserRoles(lstUserRoles);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestGetUserRolesService()
        {
            //Arrange
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            RainMaker.Entity.Models.UserRole userRole = new RainMaker.Entity.Models.UserRole()
            {
                Id = 2,
                RoleName = "Executives",
                IsDeleted = false,
                IsActive = true,
                IsCustomerRole = false
            };
            dataContext.Set<RainMaker.Entity.Models.UserRole>().Add(userRole);

            UserInRole userInRole = new UserInRole()
            {
                UserId = 1,
                RoleId = 2
            };
            dataContext.Set<UserInRole>().Add(userInRole);

            dataContext.SaveChanges();

            IUserProfileService userProfileService = new UserProfileService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            List<Model.UserRole> res = await userProfileService.GetUserRoles(1);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(2, res[0].RoleId);
            Assert.Equal("Executives", res[0].RoleName);
        }
        [Fact]
        public async Task TestUpdateUserRolesService()
        {
            //Arrange
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            UserInRole userInRole = new UserInRole()
            {
                UserId = 1,
                RoleId = 3
            };
            dataContext.Set<UserInRole>().Add(userInRole);

            RainMaker.Entity.Models.UserRole role = new RainMaker.Entity.Models.UserRole()
            {
                Id = 3,
                RoleName = "Staff Employee"
            };
            dataContext.Set<RainMaker.Entity.Models.UserRole>().Add(role);

            dataContext.SaveChanges();

            IUserProfileService userProfileService = new UserProfileService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            List<Model.UserRole> lstUserRoles = new List<Model.UserRole>();
            Model.UserRole userRole = new Model.UserRole();
            userRole.RoleName = "Staff Employee";
            userRole.RoleId = 3;
            userRole.IsRoleAssigned = true;
            lstUserRoles.Add(userRole);

            await userProfileService.UpdateUserRoles(lstUserRoles, 1);
        }
        [Fact]
        public async Task TestRenderEmailTokensController()
        {
            //Arrange
            Mock<ISettingService> mock = new Mock<ISettingService>();

            var settingController = new SettingController(null, mock.Object);

            List<TokenModel> lstTokenModels = new List<TokenModel>();

            lstTokenModels.Add(new TokenModel() { description = "Key for enabling user email address Key for enabling user email address", key = "LoginUserEmail", name = "Login User Email", symbol = "###LoginUserEmail###" });

            EmailTemplateModel emailTemplateModel = new EmailTemplateModel();
            emailTemplateModel.loanApplicationId = 1;
            emailTemplateModel.tenantId = 1;
            emailTemplateModel.templateName = "Template No 1";
            emailTemplateModel.templateDescription = "Sed ut perspiciatis unde omnis iste natus";
            emailTemplateModel.fromAddress = "talha@gmail.com";
            emailTemplateModel.subject = "You have new tasks to complete for your ###BusinessUnitName### loan application";
            emailTemplateModel.emailBody = "<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n";
            emailTemplateModel.lstTokens = lstTokenModels;

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new Microsoft.AspNetCore.Mvc.ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            settingController.ControllerContext = context;

            //Act
            IActionResult result = await settingController.RenderEmailTokens(emailTemplateModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestRenderEmailTokensService()
        {
            //Arrange
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            RainMaker.Entity.Models.LoanApplication loanApplication = new RainMaker.Entity.Models.LoanApplication()
            {
                Id = 199,
                OpportunityId = 199,
                BusinessUnitId = 199,
                IsDeleted = false,
                IsActive = true
            };
            dataContext.Set<RainMaker.Entity.Models.LoanApplication>().Add(loanApplication);

            RainMaker.Entity.Models.UserProfile userProfile = new RainMaker.Entity.Models.UserProfile()
            {
                Id = 199,
                IsDeleted = false,
                IsActive = true
            };
            dataContext.Set<RainMaker.Entity.Models.UserProfile>().Add(userProfile);

            dataContext.SaveChanges();

            List<TokenModel> lstTokenModels = new List<TokenModel>();

            lstTokenModels.Add(new TokenModel() { description = "Key for enabling user email address", key = "LoginUserEmail", name = "Login User Email", symbol = "###LoginUserEmail###" });
            lstTokenModels.Add(new TokenModel() { description = "Key for enabling customer first name", key = "CustomerFirstName", name = "Customer First Name", symbol = "###CustomerFirstname###" });
            lstTokenModels.Add(new TokenModel() { description = "Business Unit Name", key = "BusinessUnitName", name = "Business Unit Name", symbol = "###BusinessUnitName###" });

            Mock<IOpportunityService> mockOpportunityService = new Mock<IOpportunityService>();
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

            ContactEmailInfo contactEmailInfo = new ContactEmailInfo();
            contactEmailInfo.Email = "hammad@gmail.com";
            contactEmailInfo.IsPrimary = true;
            contactEmailInfo.ValidityId = 1;

            Contact contact = new Contact();
            contact.Id = 199;
            contact.ContactEmailInfoes.Add(contactEmailInfo);

            Customer customer = new Customer();
            customer.Id = 199;
            customer.Contact = contact;

            Opportunity opportunity = new Opportunity();
            opportunity.Id = 199;
            opportunity.OpportunityLeadBinders.Add(new OpportunityLeadBinder() { OwnTypeId = 1 , Customer = customer });
            mockOpportunityService.Setup(x => x.GetSingleOpportunity(It.IsAny<int>())).ReturnsAsync(opportunity);

            EmailAccount eAccount = new EmailAccount();
            eAccount.Id = 199;
            eAccount.Email = "talha@gmail.com";

            EmployeeBusinessUnitEmail employeeBusinessUnit = new EmployeeBusinessUnitEmail();
            employeeBusinessUnit.Id = 199;
            employeeBusinessUnit.EmployeeId = 199;
            employeeBusinessUnit.BusinessUnitId = 199;
            employeeBusinessUnit.EmailAccount = eAccount;

            Employee emp = new Employee();
            emp.Id = 199;
            emp.IsDeleted = false;
            emp.IsActive = true;
            emp.EmployeeBusinessUnitEmails.Add(employeeBusinessUnit);

            UserProfile profile = new UserProfile();
            profile.BusinessUnitId = 199;
            profile.Employees.Add(emp);

            mockUserProfileService.Setup(x => x.GetUserProfileEmployeeDetail(It.IsAny<int?>(), It.IsAny<UserProfileService.RelatedEntities>())).ReturnsAsync(profile);

            ISettingService settingService = new SettingService(mockOpportunityService.Object, mockUserProfileService.Object, new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            EmailTemplate res = await settingService.RenderEmailTokens(1, 199, 199, "###LoginUserEmail###", "You have new tasks to complete for your ###BusinessUnitName### loan application", "<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n", lstTokenModels);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(1, res.id);
            Assert.Equal("talha@gmail.com", res.fromAddress);
            Assert.Equal("hammad@gmail.com", res.toAddress);
            Assert.Equal("You have new tasks to complete for your  loan application",res.subject);
        }
        [Fact]
        public async Task TestGetLoanOfficersController()
        {
            //Arrange
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

            var settingController = new SettingController(mockUserProfileService.Object, null);

            //Act
            IActionResult result = await settingController.GetLoanOfficers();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestUpdateByteUsersNameController()
        {
            //Arrange
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

            var settingController = new SettingController(mockUserProfileService.Object, null);

            List<Model.ByteUserNameModel> lstByteUserNameModel = new List<Model.ByteUserNameModel>();
            Model.ByteUserNameModel model = new Model.ByteUserNameModel();
            model.userId = 21;
            model.userName = "Tanner.Holland";
            model.byteUserName = "Tunner";
            model.fullName = "Tanner Holland";
            lstByteUserNameModel.Add(model);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new Microsoft.AspNetCore.Mvc.ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            settingController.ControllerContext = context;

            //Act
            IActionResult result = await settingController.UpdateByteUsersName(lstByteUserNameModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestGetBusinessUnitsController()
        {
            //Arrange
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();

            var settingController = new SettingController(mockUserProfileService.Object, null);

            //Act
            IActionResult result = await settingController.GetBusinessUnits();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
