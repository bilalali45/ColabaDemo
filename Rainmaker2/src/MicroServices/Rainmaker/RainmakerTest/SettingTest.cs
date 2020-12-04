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
            Mock<ILoanApplicationService> mockLoanApplicationService = new Mock<ILoanApplicationService>();

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

            List<LoanApplication> lstLoanApplication = new List<LoanApplication>();
            LoanApplication loanApplication1 = new LoanApplication();
            loanApplication1.Id = 4188;

            mockUserProfileService.Setup(x => x.GetUserProfileEmployeeDetail(It.IsAny<int?>(), It.IsAny<UserProfileService.RelatedEntities>())).ReturnsAsync(profile);
            mockLoanApplicationService.Setup(x => x.GetLoanApplicationWithDetails(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<LoanApplicationService.RelatedEntities>())).Returns(lstLoanApplication);
            ISettingService settingService = new SettingService(mockOpportunityService.Object, mockUserProfileService.Object, mockLoanApplicationService.Object,new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            EmailTemplate res = await settingService.RenderEmailTokens(1, 199, 199, "###LoginUserEmail###", "###LoginUserEmail###", "You have new tasks to complete for your ###BusinessUnitName### loan application", "<p>Hello ###CustomerFirstname###</p>\n<p>Please submit following documents</p>\n<p>###DoucmentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n", lstTokenModels);

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
            Mock<ISettingService> mockSettingService = new Mock<ISettingService>();

            var settingController = new SettingController(null, mockSettingService.Object);

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
            Mock<ISettingService> mockSettingService = new Mock<ISettingService>();

            var settingController = new SettingController(null, mockSettingService.Object);

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
            Mock<ISettingService> mockSettingService = new Mock<ISettingService>();

            var settingController = new SettingController(null, mockSettingService.Object);

            //Act
            IActionResult result = await settingController.GetBusinessUnits();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestUpdateByteOrganizationCodeController()
        {
            //Arrange
            Mock<ISettingService> mockSettingService = new Mock<ISettingService>();

            var settingController = new SettingController(null, mockSettingService.Object);

            List<Model.ByteBusinessUnitModel> lstByteBusinessUnitModel = new List<Model.ByteBusinessUnitModel>();
            Model.ByteBusinessUnitModel model = new Model.ByteBusinessUnitModel();
            model.id = 1;
            model.name = "AHCLending";
            model.byteOrganizationCode = "100390";
            lstByteBusinessUnitModel.Add(model);

            Model.ByteBusinessUnitModel model2 = new Model.ByteBusinessUnitModel();
            model2.id = 2;
            model2.name = "Texas Trust Home Loans";
            model2.byteOrganizationCode = "302309";
            lstByteBusinessUnitModel.Add(model2);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new Microsoft.AspNetCore.Mvc.ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            settingController.ControllerContext = context;

            //Act
            IActionResult result = await settingController.UpdateByteOrganizationCode(lstByteBusinessUnitModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestGetLoanOfficersService()
        {
            //Arrange
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            RainMaker.Entity.Models.UserProfile userProfile = new RainMaker.Entity.Models.UserProfile()
            {
                Id = 201,
                UserName = "minaz.karim",
                ByteUserName = "Minaz Karim",
                IsDeleted = false,
                IsActive = true
            };
            dataContext.Set<RainMaker.Entity.Models.UserProfile>().Add(userProfile);

            Employee employee = new Employee()
            {
                Id = 201,
                ContactId = 201,
                UserId = 201,
                IsDeleted = false
            };
            dataContext.Set<Employee>().Add(employee);

            Contact contact = new Contact()
            {
                Id = 201,
                FirstName = "Minaz",
                LastName = "Karim",
                IsDeleted = false
            };
            dataContext.Set<Contact>().Add(contact);

            UserInRole userInRole = new UserInRole()
            {
                UserId = 201,
                RoleId = 12
            };
            dataContext.Set<UserInRole>().Add(userInRole);

            dataContext.SaveChanges();

            ISettingService settingService = new SettingService(null,null,null,new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            List<Model.ByteUserNameModel> res = await settingService.GetLoanOfficers();

            // Assert
            Assert.NotNull(res);
            Assert.Equal(201, res[0].userId);
            Assert.Equal("minaz.karim", res[0].userName);
            Assert.Equal("Minaz Karim", res[0].byteUserName);
            Assert.Equal("Minaz Karim", res[0].fullName);
        }
        [Fact]
        public async Task TestGetBusinessUnitsService()
        {
            //Arrange
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            BusinessUnit businessUnit = new BusinessUnit()
            {
                Id = 12,
                Name = "AHCLending",
                ByteOrganizationCode = "302039",
                IsDeleted = false,
                IsActive = true
            };
            dataContext.Set<BusinessUnit>().Add(businessUnit);

            dataContext.SaveChanges();

            ISettingService settingService = new SettingService(null, null,null, new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            //Act
            List<Model.ByteBusinessUnitModel> res = await settingService.GetBusinessUnits();

            // Assert
            Assert.NotNull(res);
            Assert.Equal(12, res[0].id);
            Assert.Equal("AHCLending", res[0].name);
            Assert.Equal("302039", res[0].byteOrganizationCode);
        }
        [Fact]
        public async Task TestUpdateByteUserNameService()
        {
            //Arrange
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            RainMaker.Entity.Models.UserProfile userProfile1 = new RainMaker.Entity.Models.UserProfile()
            {
                Id = 202,
                UserName = "minaz.karim",
                ByteUserName = "Minaz Karim",
                IsDeleted = false,
                IsActive = true
            };
            dataContext.Set<RainMaker.Entity.Models.UserProfile>().Add(userProfile1);

            RainMaker.Entity.Models.UserProfile userProfile2 = new RainMaker.Entity.Models.UserProfile()
            {
                Id = 203,
                UserName = "Tanner.Holland",
                ByteUserName = null,
                IsDeleted = false,
                IsActive = true
            };
            dataContext.Set<RainMaker.Entity.Models.UserProfile>().Add(userProfile2);

            dataContext.SaveChanges();

            ISettingService settingService = new SettingService(null, null,null, new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            List<Model.ByteUserNameModel> lstModel = new List<Model.ByteUserNameModel>();

            Model.ByteUserNameModel byteUserNameModel1 = new Model.ByteUserNameModel();
            byteUserNameModel1.userId = 202;
            byteUserNameModel1.userName = "minaz.karim";
            byteUserNameModel1.byteUserName = "Minaz Karim";
            byteUserNameModel1.fullName = "Minaz Karim";
            lstModel.Add(byteUserNameModel1);

            Model.ByteUserNameModel byteUserNameModel2 = new Model.ByteUserNameModel();
            byteUserNameModel2.userId = 203;
            byteUserNameModel2.userName = "Tanner.Holland";
            byteUserNameModel2.byteUserName = null;
            byteUserNameModel2.fullName = "Tanner Holland";
            lstModel.Add(byteUserNameModel2);

            await settingService.UpdateByteUserName(lstModel, 1);
        }
        [Fact]
        public async Task TestUpdateByteOrganizationCodeService()
        {
            //Arrange
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            RainMaker.Entity.Models.BusinessUnit businessUnit1 = new RainMaker.Entity.Models.BusinessUnit()
            {
                Id = 1,
                Name = "AHCLending",
                ByteOrganizationCode = "100390",
                IsDeleted = false,
                IsActive = true
            };
            dataContext.Set<RainMaker.Entity.Models.BusinessUnit>().Add(businessUnit1);

            RainMaker.Entity.Models.BusinessUnit businessUnit2 = new RainMaker.Entity.Models.BusinessUnit()
            {
                Id = 2,
                Name = "Texas Trust Home Loans",
                ByteOrganizationCode = "302309",
                IsDeleted = false,
                IsActive = true
            };
            dataContext.Set<RainMaker.Entity.Models.BusinessUnit>().Add(businessUnit2);

            dataContext.SaveChanges();

            ISettingService settingService = new SettingService(null, null, null, new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            List<Model.ByteBusinessUnitModel> lstByteBusinessUnitModel = new List<Model.ByteBusinessUnitModel>();
            Model.ByteBusinessUnitModel model = new Model.ByteBusinessUnitModel();
            model.id = 1;
            model.name = "AHCLending";
            model.byteOrganizationCode = "100390";
            lstByteBusinessUnitModel.Add(model);

            Model.ByteBusinessUnitModel model2 = new Model.ByteBusinessUnitModel();
            model2.id = 2;
            model2.name = "Texas Trust Home Loans";
            model2.byteOrganizationCode = "302309";
            lstByteBusinessUnitModel.Add(model2);

            await settingService.UpdateByteOrganizationCode(lstByteBusinessUnitModel, 1);
        }
    }
}
