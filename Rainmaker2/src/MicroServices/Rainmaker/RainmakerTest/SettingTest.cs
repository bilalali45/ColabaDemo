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
                Id = 4188,
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

            lstTokenModels.Add(new TokenModel() { description = "Current Date", key = "Date", name = "Date", symbol = "###Date###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Email Address", key = "PrimaryBorrowerEmailAddress", name = "Primary Borrower Email Address", symbol = "###PrimaryBorrowerEmailAddress###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower First Name", key = "PrimaryBorrowerFirstName", name = "Primary Borrower First Name", symbol = "###PrimaryBorrowerFirstName###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Last Name", key = "PrimaryBorrowerLastName", name = "Primary Borrower Last Name", symbol = "###PrimaryBorrowerLastName###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower First Name", key = "Co-BorrowerFirstName", name = "Co-Borrower First Name", symbol = "###Co-BorrowerFirstName###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Last Name", key = "Co-BorrowerLastName", name = "Co-Borrower Last Name", symbol = "###Co-BorrowerLastName###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Email Address", key = "Co-BorrowerEmailAddress", name = "Co-Borrower Email Address", symbol = "###Co-BorrowerEmailAddress###" });
            lstTokenModels.Add(new TokenModel() { description = "Unique email tag for identifying email sender", key = "EmailTag", name = "EmailTag", symbol = "###EmailTag###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Portal Url", key = "LoanPortalUrl", name = "Loan Portal Url", symbol = "###LoanPortalUrl###" });
            lstTokenModels.Add(new TokenModel() { description = "Status of Borrower Loan Application on Colaba", key = "LoanStatus", name = "Loan Status", symbol = "###LoanStatus###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property Address", key = "SubjectPropertyAddress", name = "Subject Property Address", symbol = "###SubjectPropertyAddress###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property State", key = "SubjectPropertyState", name = "Subject Property State", symbol = "###SubjectPropertyState###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property State Abbreviation", key = "SubjectPropertyStateAbbreviation", name = "Subject Property State Abbreviation", symbol = "###SubjectPropertyStateAbbreviation###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property County", key = "SubjectPropertyCounty", name = "Subject Property County", symbol = "###SubjectPropertyCounty###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property City", key = "SubjectPropertyCity", name = "Subject Property City", symbol = "###SubjectPropertyCity###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property Zip Code", key = "SubjectPropertyZipCode", name = "Subject Property ZipCode", symbol = "###SubjectPropertyZipCode###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Purpose", key = "LoanPurpose", name = "Loan Purpose", symbol = "###LoanPurpose###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Application Loan Amount", key = "LoanAmount", name = "Loan Amount", symbol = "###LoanAmount###" });
            lstTokenModels.Add(new TokenModel() { description = "Property Value", key = "PropertyValue", name = "Property Value", symbol = "###PropertyValue###" });
            lstTokenModels.Add(new TokenModel() { description = "Property Type", key = "PropertyType", name = "Property Type", symbol = "###PropertyType###" });
            lstTokenModels.Add(new TokenModel() { description = "Property Usage", key = "PropertyUsage", name = "Property Usage", symbol = "###PropertyUsage###" });
            lstTokenModels.Add(new TokenModel() { description = "Residency Type", key = "ResidencyType", name = "Residency Type", symbol = "###ResidencyType###" });
            lstTokenModels.Add(new TokenModel() { description = "Branch Nmls No", key = "BranchNmlsNo", name = "Branch Nmls No", symbol = "###BranchNmlsNo###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Application Business Unit Name", key = "BusinessUnitName", name = "Business Unit Name", symbol = "###BusinessUnitName###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Application Business Unit Toll Free Number", key = "BusinessUnitPhoneNumber", name = "Business Unit Phone Number", symbol = "###BusinessUnitPhoneNumber###" });
            lstTokenModels.Add(new TokenModel() { description = "Business Unit Website URL", key = "BusinessUnitWebSiteUrl", name = "Business Unit WebSite Url", symbol = "###BusinessUnitWebSiteUrl###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Application Login Link", key = "LoanApplicationLoginLink", name = "Loan Application Login Link", symbol = "###LoanApplicationLoginLink###" });
            lstTokenModels.Add(new TokenModel() { description = "Web Page of Loan Officer", key = "LoanOfficerPageUrl", name = "Loan Officer Page Url", symbol = "###LoanOfficerPageUrl###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Officer First Name", key = "LoanOfficerFirstName", name = "Loan Officer First Name", symbol = "###LoanOfficerLastName###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Officer Last Name", key = "LoanOfficerLastName", name = "Loan Officer Last Name", symbol = "###BusinessUnitName###" });
            lstTokenModels.Add(new TokenModel() { description = "Bullet Point list of Documents being requested", key = "RequestDocumentList", name = "Request Document List", symbol = "###RequestDocumentList###" });
            lstTokenModels.Add(new TokenModel() { description = "Email Address of the Needs List requestor", key = "RequestorUserEmail", name = "Requestor User Email", symbol = "###RequestorUserEmail###" });
            lstTokenModels.Add(new TokenModel() { description = "NMLS number of your company", key = "CompanyNMLSNo.", name = "Company NMLS No.", symbol = "###CompanyNMLSNo.###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Officer Email Address", key = "LoanOfficerEmailAddress", name = "Loan Officer Email Address", symbol = "###LoanOfficerEmailAddress###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Officer Office Phone Number", key = "LoanOfficerOfficePhoneNumber", name = "Loan Officer Office Phone Number", symbol = "###LoanOfficerOfficePhoneNumber###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Officer Cell Phone Number", key = "LoanOfficerCellPhoneNumber", name = "Loan Officer Cell Phone Number", symbol = "###LoanOfficerCellPhoneNumber###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Present Street Address", key = "PrimaryBorrowerPresentStreetAddress", name = "Primary Borrower Present Street Address", symbol = "###PrimaryBorrowerPresentStreetAddress###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Present Unit No.", key = "PrimaryBorrowerPresentUnitNo.", name = "Primary Borrower Present Unit No.", symbol = "###PrimaryBorrowerPresentUnitNo.###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Present City", key = "PrimaryBorrowerPresentCity", name = "Primary Borrower Present City", symbol = "###PrimaryBorrowerPresentCity###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Present State", key = "PrimaryBorrowerPresentState", name = "Primary Borrower Present State", symbol = "###PrimaryBorrowerPresentState###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Present State Abbreviation", key = "PrimaryBorrowerPresentStateAbbreviation", name = "Primary Borrower Present State Abbreviation", symbol = "###PrimaryBorrowerPresentStateAbbreviation###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Present Zip Code", key = "PrimaryBorrowerPresentZipCode", name = "Primary Borrower Present Zip Code", symbol = "###PrimaryBorrowerPresentZipCode###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Present Street Address", key = "Co-BorrowerPresentStreetAddress", name = "Co-Borrower Present Street Address", symbol = "###Co-BorrowerPresentStreetAddress###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Present Unit No.", key = "Co-BorrowerPresentUnitNo.", name = "Co-Borrower Present Unit No.", symbol = "###Co-BorrowerPresentUnitNo.###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Present City", key = "Co-BorrowerPresentCity", name = "Co-Borrower Present City", symbol = "###Co-BorrowerPresentCity###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Present State", key = "Co-BorrowerPresentState", name = "Co-Borrower Present State", symbol = "###Co-BorrowerPresentState###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Present State Abbreviation", key = "Co-BorrowerPresentStateAbbreviation", name = "Co-Borrower Present State Abbreviation", symbol = "###Co-BorrowerPresentStateAbbreviation###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Present Zip Code", key = "Co-BorrowerPresentZipCode", name = "Co-Borrower Present Zip Code", symbol = "###Co-BorrowerPresentZipCode###" });
            lstTokenModels.Add(new TokenModel() { description = "Button directing recepient to document upload module", key = "DocumentUploadButton", name = "Document Upload Button", symbol = "###DocumentUploadButton###" });
            lstTokenModels.Add(new TokenModel() { description = "Button directing recepient to loan portal home screen", key = "LoanPortalHomeButton", name = "Loan Portal Home Button", symbol = "###LoanPortalHomeButton###" });
            lstTokenModels.Add(new TokenModel() { description = "Button directing recepient to documents page on loan portal", key = "DocumentsPageButton", name = "Documents Page Button", symbol = "###DocumentsPageButton###" });

            Mock<IOpportunityService> mockOpportunityService = new Mock<IOpportunityService>();
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();
            Mock<ILoanApplicationService> mockLoanApplicationService = new Mock<ILoanApplicationService>();

            List<LoanApplication> lstLoanApplication = new List<LoanApplication>();
            LoanApplication loanApplication1 = new LoanApplication();
            loanApplication1.Id = 4188;

            State stateBorrower = new State();
            stateBorrower.Id = 9;
            stateBorrower.Name = "Alaska";
            stateBorrower.Abbreviation = "AK";

            AddressInfo addressInfoBorrower = new AddressInfo();
            addressInfoBorrower.Id = 7390;
            addressInfoBorrower.Name = "Texas";
            addressInfoBorrower.UnitNo = "428";
            addressInfoBorrower.StreetAddress = "568 Broadway";
            addressInfoBorrower.StateName = "Texas";
            addressInfoBorrower.CountryName = "America";
            addressInfoBorrower.CountyName = "Harris";
            addressInfoBorrower.CityName = "Houston";
            addressInfoBorrower.ZipCode = "77055";
            addressInfoBorrower.State = stateBorrower;

            BorrowerResidence borrowerResidence = new BorrowerResidence();
            borrowerResidence.LoanAddress = addressInfoBorrower;

            Borrower borrower1 = new Borrower();
            borrower1.Id = 6194;
            borrower1.LoanContactId = 6189;
            borrower1.LoanApplicationId = 4188;
            borrower1.BorrowerResidences.Add(borrowerResidence);
            loanApplication1.Borrowers.Add(borrower1);

            State stateCoBorrower = new State();
            stateCoBorrower.Id = 10;
            stateCoBorrower.Name = "Alaska";
            stateCoBorrower.Abbreviation = "AK";

            AddressInfo addressInfoCoBorrower = new AddressInfo();
            addressInfoCoBorrower.Id = 7391;
            addressInfoCoBorrower.Name = "Texas";
            addressInfoCoBorrower.UnitNo = "428";
            addressInfoCoBorrower.StreetAddress = "568 Broadway";
            addressInfoCoBorrower.StateName = "Texas";
            addressInfoCoBorrower.CountryName = "America";
            addressInfoCoBorrower.CountyName = "Harris";
            addressInfoCoBorrower.CityName = "Houston";
            addressInfoCoBorrower.ZipCode = "77055";
            addressInfoCoBorrower.State = stateBorrower;

            BorrowerResidence coBorrowerResidence = new BorrowerResidence();
            coBorrowerResidence.LoanAddress = addressInfoBorrower;

            Borrower borrower2 = new Borrower();
            borrower2.Id = 6195;
            borrower2.LoanContactId = 6190;
            borrower2.LoanApplicationId = 4188;
            borrower2.BorrowerResidences.Add(coBorrowerResidence);
            loanApplication1.Borrowers.Add(borrower2);

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
            opportunity.OpportunityLeadBinders.Add(new OpportunityLeadBinder() { OwnTypeId = 1, Customer = customer });
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

            loanApplication1.Opportunity = opportunity;

            lstLoanApplication.Add(loanApplication1);

            mockUserProfileService.Setup(x => x.GetUserProfileEmployeeDetail(It.IsAny<int?>(), It.IsAny<UserProfileService.RelatedEntities>())).ReturnsAsync(profile);
            mockLoanApplicationService.Setup(x => x.GetLoanApplicationWithDetails(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<LoanApplicationService.RelatedEntities>())).Returns(lstLoanApplication);
            ISettingService settingService = new SettingService(mockOpportunityService.Object, mockUserProfileService.Object, mockLoanApplicationService.Object,null,null,new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            EmailTemplate res = await settingService.RenderEmailTokens(1, 4188, 199, "###RequestorUserEmail###", "###RequestorUserEmail###", "You have new tasks to complete for your ###BusinessUnitName### loan application", "<p>Please submit following documents</p>\n<p>###RequestDocumentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n", lstTokenModels);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(1, res.id);
            Assert.Equal("talha@gmail.com", res.fromAddress);
            Assert.Equal("talha@gmail.com", res.CCAddress);
            Assert.Equal("You have new tasks to complete for your  loan application", res.subject);
        }
        [Fact]
        public async Task TestPartiallyRenderEmailTokensService()
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
                Id = 4192,
                OpportunityId = 209,
                BusinessUnitId = 209,
                IsDeleted = false,
                IsActive = true
            };
            dataContext.Set<RainMaker.Entity.Models.LoanApplication>().Add(loanApplication);

            RainMaker.Entity.Models.UserProfile userProfile = new RainMaker.Entity.Models.UserProfile()
            {
                Id = 209,
                IsDeleted = false,
                IsActive = true
            };
            dataContext.Set<RainMaker.Entity.Models.UserProfile>().Add(userProfile);

            dataContext.SaveChanges();

            List<TokenModel> lstTokenModels = new List<TokenModel>();

            lstTokenModels.Add(new TokenModel() { description = "Current Date", key = "Date", name = "Date", symbol = "###Date###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Email Address", key = "PrimaryBorrowerEmailAddress", name = "Primary Borrower Email Address", symbol = "###PrimaryBorrowerEmailAddress###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower First Name", key = "PrimaryBorrowerFirstName", name = "Primary Borrower First Name", symbol = "###PrimaryBorrowerFirstName###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Last Name", key = "PrimaryBorrowerLastName", name = "Primary Borrower Last Name", symbol = "###PrimaryBorrowerLastName###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower First Name", key = "Co-BorrowerFirstName", name = "Co-Borrower First Name", symbol = "###Co-BorrowerFirstName###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Last Name", key = "Co-BorrowerLastName", name = "Co-Borrower Last Name", symbol = "###Co-BorrowerLastName###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Email Address", key = "Co-BorrowerEmailAddress", name = "Co-Borrower Email Address", symbol = "###Co-BorrowerEmailAddress###" });
            lstTokenModels.Add(new TokenModel() { description = "Unique email tag for identifying email sender", key = "EmailTag", name = "EmailTag", symbol = "###EmailTag###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Portal Url", key = "LoanPortalUrl", name = "Loan Portal Url", symbol = "###LoanPortalUrl###" });
            lstTokenModels.Add(new TokenModel() { description = "Status of Borrower Loan Application on Colaba", key = "LoanStatus", name = "Loan Status", symbol = "###LoanStatus###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property Address", key = "SubjectPropertyAddress", name = "Subject Property Address", symbol = "###SubjectPropertyAddress###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property State", key = "SubjectPropertyState", name = "Subject Property State", symbol = "###SubjectPropertyState###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property State Abbreviation", key = "SubjectPropertyStateAbbreviation", name = "Subject Property State Abbreviation", symbol = "###SubjectPropertyStateAbbreviation###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property County", key = "SubjectPropertyCounty", name = "Subject Property County", symbol = "###SubjectPropertyCounty###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property City", key = "SubjectPropertyCity", name = "Subject Property City", symbol = "###SubjectPropertyCity###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property Zip Code", key = "SubjectPropertyZipCode", name = "Subject Property ZipCode", symbol = "###SubjectPropertyZipCode###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Purpose", key = "LoanPurpose", name = "Loan Purpose", symbol = "###LoanPurpose###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Application Loan Amount", key = "LoanAmount", name = "Loan Amount", symbol = "###LoanAmount###" });
            lstTokenModels.Add(new TokenModel() { description = "Property Value", key = "PropertyValue", name = "Property Value", symbol = "###PropertyValue###" });
            lstTokenModels.Add(new TokenModel() { description = "Property Type", key = "PropertyType", name = "Property Type", symbol = "###PropertyType###" });
            lstTokenModels.Add(new TokenModel() { description = "Property Usage", key = "PropertyUsage", name = "Property Usage", symbol = "###PropertyUsage###" });
            lstTokenModels.Add(new TokenModel() { description = "Residency Type", key = "ResidencyType", name = "Residency Type", symbol = "###ResidencyType###" });
            lstTokenModels.Add(new TokenModel() { description = "Branch Nmls No", key = "BranchNmlsNo", name = "Branch Nmls No", symbol = "###BranchNmlsNo###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Application Business Unit Name", key = "BusinessUnitName", name = "Business Unit Name", symbol = "###BusinessUnitName###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Application Business Unit Toll Free Number", key = "BusinessUnitPhoneNumber", name = "Business Unit Phone Number", symbol = "###BusinessUnitPhoneNumber###" });
            lstTokenModels.Add(new TokenModel() { description = "Business Unit Website URL", key = "BusinessUnitWebSiteUrl", name = "Business Unit WebSite Url", symbol = "###BusinessUnitWebSiteUrl###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Application Login Link", key = "LoanApplicationLoginLink", name = "Loan Application Login Link", symbol = "###LoanApplicationLoginLink###" });
            lstTokenModels.Add(new TokenModel() { description = "Web Page of Loan Officer", key = "LoanOfficerPageUrl", name = "Loan Officer Page Url", symbol = "###LoanOfficerPageUrl###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Officer First Name", key = "LoanOfficerFirstName", name = "Loan Officer First Name", symbol = "###LoanOfficerLastName###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Officer Last Name", key = "LoanOfficerLastName", name = "Loan Officer Last Name", symbol = "###BusinessUnitName###" });
            lstTokenModels.Add(new TokenModel() { description = "Bullet Point list of Documents being requested", key = "RequestDocumentList", name = "Request Document List", symbol = "###RequestDocumentList###" });
            lstTokenModels.Add(new TokenModel() { description = "Email Address of the Needs List requestor", key = "RequestorUserEmail", name = "Requestor User Email", symbol = "###RequestorUserEmail###" });
            lstTokenModels.Add(new TokenModel() { description = "NMLS number of your company", key = "CompanyNMLSNo.", name = "Company NMLS No.", symbol = "###CompanyNMLSNo.###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Officer Email Address", key = "LoanOfficerEmailAddress", name = "Loan Officer Email Address", symbol = "###LoanOfficerEmailAddress###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Officer Office Phone Number", key = "LoanOfficerOfficePhoneNumber", name = "Loan Officer Office Phone Number", symbol = "###LoanOfficerOfficePhoneNumber###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Officer Cell Phone Number", key = "LoanOfficerCellPhoneNumber", name = "Loan Officer Cell Phone Number", symbol = "###LoanOfficerCellPhoneNumber###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Present Street Address", key = "PrimaryBorrowerPresentStreetAddress", name = "Primary Borrower Present Street Address", symbol = "###PrimaryBorrowerPresentStreetAddress###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Present Unit No.", key = "PrimaryBorrowerPresentUnitNo.", name = "Primary Borrower Present Unit No.", symbol = "###PrimaryBorrowerPresentUnitNo.###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Present City", key = "PrimaryBorrowerPresentCity", name = "Primary Borrower Present City", symbol = "###PrimaryBorrowerPresentCity###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Present State", key = "PrimaryBorrowerPresentState", name = "Primary Borrower Present State", symbol = "###PrimaryBorrowerPresentState###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Present State Abbreviation", key = "PrimaryBorrowerPresentStateAbbreviation", name = "Primary Borrower Present State Abbreviation", symbol = "###PrimaryBorrowerPresentStateAbbreviation###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Present Zip Code", key = "PrimaryBorrowerPresentZipCode", name = "Primary Borrower Present Zip Code", symbol = "###PrimaryBorrowerPresentZipCode###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Present Street Address", key = "Co-BorrowerPresentStreetAddress", name = "Co-Borrower Present Street Address", symbol = "###Co-BorrowerPresentStreetAddress###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Present Unit No.", key = "Co-BorrowerPresentUnitNo.", name = "Co-Borrower Present Unit No.", symbol = "###Co-BorrowerPresentUnitNo.###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Present City", key = "Co-BorrowerPresentCity", name = "Co-Borrower Present City", symbol = "###Co-BorrowerPresentCity###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Present State", key = "Co-BorrowerPresentState", name = "Co-Borrower Present State", symbol = "###Co-BorrowerPresentState###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Present State Abbreviation", key = "Co-BorrowerPresentStateAbbreviation", name = "Co-Borrower Present State Abbreviation", symbol = "###Co-BorrowerPresentStateAbbreviation###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Present Zip Code", key = "Co-BorrowerPresentZipCode", name = "Co-Borrower Present Zip Code", symbol = "###Co-BorrowerPresentZipCode###" });
            lstTokenModels.Add(new TokenModel() { description = "Button directing recepient to document upload module", key = "DocumentUploadButton", name = "Document Upload Button", symbol = "###DocumentUploadButton###" });
            lstTokenModels.Add(new TokenModel() { description = "Button directing recepient to loan portal home screen", key = "LoanPortalHomeButton", name = "Loan Portal Home Button", symbol = "###LoanPortalHomeButton###" });
            lstTokenModels.Add(new TokenModel() { description = "Button directing recepient to documents page on loan portal", key = "DocumentsPageButton", name = "Documents Page Button", symbol = "###DocumentsPageButton###" });

            Mock<IOpportunityService> mockOpportunityService = new Mock<IOpportunityService>();
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();
            Mock<ILoanApplicationService> mockLoanApplicationService = new Mock<ILoanApplicationService>();

            BusinessUnit businessUnit = new BusinessUnit();
            businessUnit.Id = 209;
            businessUnit.Name = "AHCLending";
            businessUnit.LoanUrl = "https://www.ahclending.com/LoanApplication/Home";
            businessUnit.WebUrl = "https://localAhc.RainsoftFn.com";
            businessUnit.LoanLoginUrl = "https://www.ahclending.com/LoanApplication/Home";

            StatusList statusList = new StatusList();
            statusList.Id = 1;
            statusList.Name = "Floating";

            State state = new State();
            state.Id = 1;
            state.Name = "Alaska";
            state.Abbreviation = "AK";

            PropertyInfo propertyInfo = new PropertyInfo();
            propertyInfo.Id = 7487;

            LoanPurpose loanPurpose = new LoanPurpose();
            loanPurpose.Id = 1;
            loanPurpose.Name = "Purchase";

            List<LoanApplication> lstLoanApplication = new List<LoanApplication>();
            LoanApplication loanApplication1 = new LoanApplication();
            loanApplication1.Id = 4192;
            loanApplication1.BusinessUnit = businessUnit;
            loanApplication1.StatusList = statusList;
            loanApplication1.PropertyInfo = propertyInfo;
            loanApplication1.LoanAmount = 800000;
            loanApplication1.LoanPurpose = loanPurpose;

            State stateBorrower = new State();
            stateBorrower.Id = 14;
            stateBorrower.Name = "Alaska";
            stateBorrower.Abbreviation = "AK";

            AddressInfo addressInfoBorrower = new AddressInfo();
            addressInfoBorrower.Id = 7398;
            addressInfoBorrower.Name = "Texas";
            addressInfoBorrower.UnitNo = "428";
            addressInfoBorrower.StreetAddress = "568 Broadway";
            addressInfoBorrower.StateName = "Texas";
            addressInfoBorrower.CountryName = "America";
            addressInfoBorrower.CountyName = "Harris";
            addressInfoBorrower.CityName = "Houston";
            addressInfoBorrower.ZipCode = "77055";
            addressInfoBorrower.State = stateBorrower;

            BorrowerResidence borrowerResidence = new BorrowerResidence();
            borrowerResidence.LoanAddress = addressInfoBorrower;

            State stateCoBorrower = new State();
            stateCoBorrower.Id = 15;
            stateCoBorrower.Name = "Alaska";
            stateCoBorrower.Abbreviation = "AK";

            AddressInfo addressInfoCoBorrower = new AddressInfo();
            addressInfoCoBorrower.Id = 7399;
            addressInfoCoBorrower.Name = "Texas";
            addressInfoCoBorrower.UnitNo = "428";
            addressInfoCoBorrower.StreetAddress = "568 Broadway";
            addressInfoCoBorrower.StateName = "Texas";
            addressInfoCoBorrower.CountryName = "America";
            addressInfoCoBorrower.CountyName = "Harris";
            addressInfoCoBorrower.CityName = "Houston";
            addressInfoCoBorrower.ZipCode = "77055";
            addressInfoCoBorrower.State = stateBorrower;

            BorrowerResidence coBorrowerResidence = new BorrowerResidence();
            coBorrowerResidence.LoanAddress = addressInfoBorrower;

            Borrower borrower1 = new Borrower();
            borrower1.Id = 7194;
            borrower1.LoanContactId = 7189;
            borrower1.LoanApplicationId = 7192;
            borrower1.BorrowerResidences.Add(borrowerResidence);
            loanApplication1.Borrowers.Add(borrower1);

            ContactEmailInfo contactEmailInfo = new ContactEmailInfo();
            contactEmailInfo.Email = "hammad@gmail.com";
            contactEmailInfo.IsPrimary = true;
            contactEmailInfo.ValidityId = 1;

            Contact contact = new Contact();
            contact.Id = 209;
            contact.ContactEmailInfoes.Add(contactEmailInfo);

            Customer customer = new Customer();
            customer.Id = 209;
            customer.Contact = contact;

            CompanyPhoneInfo companyPhoneInfo = new CompanyPhoneInfo();
            companyPhoneInfo.Id = 20;
            companyPhoneInfo.Phone = "9725733900";

            EmployeePhoneBinder employeePhoneBinder = new EmployeePhoneBinder();
            employeePhoneBinder.Id = 1;
            employeePhoneBinder.EmployeeId = 2;
            employeePhoneBinder.CompanyPhoneInfoId = 20;
            employeePhoneBinder.TypeId = 3;
            employeePhoneBinder.CompanyPhoneInfo = companyPhoneInfo;

            Employee employee = new Employee();
            employee.Id = 2;
            employee.EmployeePhoneBinders.Add(employeePhoneBinder);

            Opportunity opportunity = new Opportunity();
            opportunity.Id = 209;
            opportunity.Owner = employee;
            opportunity.OpportunityLeadBinders.Add(new OpportunityLeadBinder() { OwnTypeId = 1, Customer = customer });
            mockOpportunityService.Setup(x => x.GetSingleOpportunity(It.IsAny<int>())).ReturnsAsync(opportunity);

            EmailAccount eAccount = new EmailAccount();
            eAccount.Id = 209;
            eAccount.Email = "talha@gmail.com";

            EmployeeBusinessUnitEmail employeeBusinessUnit = new EmployeeBusinessUnitEmail();
            employeeBusinessUnit.Id = 209;
            employeeBusinessUnit.EmployeeId = 209;
            employeeBusinessUnit.BusinessUnitId = 209;
            employeeBusinessUnit.EmailAccount = eAccount;

            Employee emp = new Employee();
            emp.Id = 209;
            emp.IsDeleted = false;
            emp.IsActive = true;
            employee.Contact = contact;
            emp.EmployeeBusinessUnitEmails.Add(employeeBusinessUnit);

            UserProfile profile = new UserProfile();
            profile.BusinessUnitId = 209;
            profile.Employees.Add(emp);

            loanApplication1.Opportunity = opportunity;

            lstLoanApplication.Add(loanApplication1);

            mockUserProfileService.Setup(x => x.GetUserProfileEmployeeDetail(It.IsAny<int?>(), It.IsAny<UserProfileService.RelatedEntities>())).ReturnsAsync(profile);
            mockLoanApplicationService.Setup(x => x.GetLoanApplicationWithDetails(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<LoanApplicationService.RelatedEntities>())).Returns(lstLoanApplication);
            ISettingService settingService = new SettingService(mockOpportunityService.Object, mockUserProfileService.Object, mockLoanApplicationService.Object, null, null, new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            EmailTemplate res = await settingService.RenderEmailTokens(1, 4192, 209, "###RequestorUserEmail###", "###RequestorUserEmail###", "You have new tasks to complete for your ###BusinessUnitName### loan application", "<p>Please submit following documents</p>\n<p>###RequestDocumentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n", lstTokenModels);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(1, res.id);
            Assert.Equal("talha@gmail.com", res.fromAddress);
            Assert.Equal("talha@gmail.com", res.CCAddress);
            Assert.Equal("You have new tasks to complete for your AHCLending loan application", res.subject);
        }
        [Fact]
        public async Task TestSetTokenValuesService()
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
                Id = 4189,
                OpportunityId = 205,
                BusinessUnitId = 205,
                IsDeleted = false,
                IsActive = true
            };
            dataContext.Set<RainMaker.Entity.Models.LoanApplication>().Add(loanApplication);

            RainMaker.Entity.Models.UserProfile userProfile = new RainMaker.Entity.Models.UserProfile()
            {
                Id = 205,
                IsDeleted = false,
                IsActive = true
            };
            dataContext.Set<RainMaker.Entity.Models.UserProfile>().Add(userProfile);

            dataContext.SaveChanges();

            List<TokenModel> lstTokenModels = new List<TokenModel>();

            lstTokenModels.Add(new TokenModel() { description = "Current Date", key = "Date", name = "Date", symbol = "###Date###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Email Address", key = "PrimaryBorrowerEmailAddress", name = "Primary Borrower Email Address", symbol = "###PrimaryBorrowerEmailAddress###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower First Name", key = "PrimaryBorrowerFirstName", name = "Primary Borrower First Name", symbol = "###PrimaryBorrowerFirstName###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Last Name", key = "PrimaryBorrowerLastName", name = "Primary Borrower Last Name", symbol = "###PrimaryBorrowerLastName###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower First Name", key = "Co-BorrowerFirstName", name = "Co-Borrower First Name", symbol = "###Co-BorrowerFirstName###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Last Name", key = "Co-BorrowerLastName", name = "Co-Borrower Last Name", symbol = "###Co-BorrowerLastName###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Email Address", key = "Co-BorrowerEmailAddress", name = "Co-Borrower Email Address", symbol = "###Co-BorrowerEmailAddress###" });
            lstTokenModels.Add(new TokenModel() { description = "Unique email tag for identifying email sender", key = "EmailTag", name = "EmailTag", symbol = "###EmailTag###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Portal Url", key = "LoanPortalUrl", name = "Loan Portal Url", symbol = "###LoanPortalUrl###" });
            lstTokenModels.Add(new TokenModel() { description = "Status of Borrower Loan Application on Colaba", key = "LoanStatus", name = "Loan Status", symbol = "###LoanStatus###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property Address", key = "SubjectPropertyAddress", name = "Subject Property Address", symbol = "###SubjectPropertyAddress###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property State", key = "SubjectPropertyState", name = "Subject Property State", symbol = "###SubjectPropertyState###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property State Abbreviation", key = "SubjectPropertyStateAbbreviation", name = "Subject Property State Abbreviation", symbol = "###SubjectPropertyStateAbbreviation###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property County", key = "SubjectPropertyCounty", name = "Subject Property County", symbol = "###SubjectPropertyCounty###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property City", key = "SubjectPropertyCity", name = "Subject Property City", symbol = "###SubjectPropertyCity###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property Zip Code", key = "SubjectPropertyZipCode", name = "Subject Property ZipCode", symbol = "###SubjectPropertyZipCode###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Purpose", key = "LoanPurpose", name = "Loan Purpose", symbol = "###LoanPurpose###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Application Loan Amount", key = "LoanAmount", name = "Loan Amount", symbol = "###LoanAmount###" });
            lstTokenModels.Add(new TokenModel() { description = "Property Value", key = "PropertyValue", name = "Property Value", symbol = "###PropertyValue###" });
            lstTokenModels.Add(new TokenModel() { description = "Property Type", key = "PropertyType", name = "Property Type", symbol = "###PropertyType###" });
            lstTokenModels.Add(new TokenModel() { description = "Property Usage", key = "PropertyUsage", name = "Property Usage", symbol = "###PropertyUsage###" });
            lstTokenModels.Add(new TokenModel() { description = "Residency Type", key = "ResidencyType", name = "Residency Type", symbol = "###ResidencyType###" });
            lstTokenModels.Add(new TokenModel() { description = "Branch Nmls No", key = "BranchNmlsNo", name = "Branch Nmls No", symbol = "###BranchNmlsNo###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Application Business Unit Name", key = "BusinessUnitName", name = "Business Unit Name", symbol = "###BusinessUnitName###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Application Business Unit Toll Free Number", key = "BusinessUnitPhoneNumber", name = "Business Unit Phone Number", symbol = "###BusinessUnitPhoneNumber###" });
            lstTokenModels.Add(new TokenModel() { description = "Business Unit Website URL", key = "BusinessUnitWebSiteUrl", name = "Business Unit WebSite Url", symbol = "###BusinessUnitWebSiteUrl###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Application Login Link", key = "LoanApplicationLoginLink", name = "Loan Application Login Link", symbol = "###LoanApplicationLoginLink###" });
            lstTokenModels.Add(new TokenModel() { description = "Web Page of Loan Officer", key = "LoanOfficerPageUrl", name = "Loan Officer Page Url", symbol = "###LoanOfficerPageUrl###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Officer First Name", key = "LoanOfficerFirstName", name = "Loan Officer First Name", symbol = "###LoanOfficerLastName###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Officer Last Name", key = "LoanOfficerLastName", name = "Loan Officer Last Name", symbol = "###BusinessUnitName###" });
            lstTokenModels.Add(new TokenModel() { description = "Bullet Point list of Documents being requested", key = "RequestDocumentList", name = "Request Document List", symbol = "###RequestDocumentList###" });
            lstTokenModels.Add(new TokenModel() { description = "Email Address of the Needs List requestor", key = "RequestorUserEmail", name = "Requestor User Email", symbol = "###RequestorUserEmail###" });
            lstTokenModels.Add(new TokenModel() { description = "NMLS number of your company", key = "CompanyNMLSNo.", name = "Company NMLS No.", symbol = "###CompanyNMLSNo.###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Officer Email Address", key = "LoanOfficerEmailAddress", name = "Loan Officer Email Address", symbol = "###LoanOfficerEmailAddress###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Officer Office Phone Number", key = "LoanOfficerOfficePhoneNumber", name = "Loan Officer Office Phone Number", symbol = "###LoanOfficerOfficePhoneNumber###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Officer Cell Phone Number", key = "LoanOfficerCellPhoneNumber", name = "Loan Officer Cell Phone Number", symbol = "###LoanOfficerCellPhoneNumber###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Present Street Address", key = "PrimaryBorrowerPresentStreetAddress", name = "Primary Borrower Present Street Address", symbol = "###PrimaryBorrowerPresentStreetAddress###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Present Unit No.", key = "PrimaryBorrowerPresentUnitNo.", name = "Primary Borrower Present Unit No.", symbol = "###PrimaryBorrowerPresentUnitNo.###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Present City", key = "PrimaryBorrowerPresentCity", name = "Primary Borrower Present City", symbol = "###PrimaryBorrowerPresentCity###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Present State", key = "PrimaryBorrowerPresentState", name = "Primary Borrower Present State", symbol = "###PrimaryBorrowerPresentState###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Present State Abbreviation", key = "PrimaryBorrowerPresentStateAbbreviation", name = "Primary Borrower Present State Abbreviation", symbol = "###PrimaryBorrowerPresentStateAbbreviation###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Present Zip Code", key = "PrimaryBorrowerPresentZipCode", name = "Primary Borrower Present Zip Code", symbol = "###PrimaryBorrowerPresentZipCode###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Present Street Address", key = "Co-BorrowerPresentStreetAddress", name = "Co-Borrower Present Street Address", symbol = "###Co-BorrowerPresentStreetAddress###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Present Unit No.", key = "Co-BorrowerPresentUnitNo.", name = "Co-Borrower Present Unit No.", symbol = "###Co-BorrowerPresentUnitNo.###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Present City", key = "Co-BorrowerPresentCity", name = "Co-Borrower Present City", symbol = "###Co-BorrowerPresentCity###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Present State", key = "Co-BorrowerPresentState", name = "Co-Borrower Present State", symbol = "###Co-BorrowerPresentState###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Present State Abbreviation", key = "Co-BorrowerPresentStateAbbreviation", name = "Co-Borrower Present State Abbreviation", symbol = "###Co-BorrowerPresentStateAbbreviation###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Present Zip Code", key = "Co-BorrowerPresentZipCode", name = "Co-Borrower Present Zip Code", symbol = "###Co-BorrowerPresentZipCode###" });
            lstTokenModels.Add(new TokenModel() { description = "Button directing recepient to document upload module", key = "DocumentUploadButton", name = "Document Upload Button", symbol = "###DocumentUploadButton###" });
            lstTokenModels.Add(new TokenModel() { description = "Button directing recepient to loan portal home screen", key = "LoanPortalHomeButton", name = "Loan Portal Home Button", symbol = "###LoanPortalHomeButton###" });
            lstTokenModels.Add(new TokenModel() { description = "Button directing recepient to documents page on loan portal", key = "DocumentsPageButton", name = "Documents Page Button", symbol = "###DocumentsPageButton###" });

            Mock<IOpportunityService> mockOpportunityService = new Mock<IOpportunityService>();
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();
            Mock<ILoanApplicationService> mockLoanApplicationService = new Mock<ILoanApplicationService>();

            BusinessUnit businessUnit = new BusinessUnit();
            businessUnit.Id = 205;
            businessUnit.Name = "AHCLending";
            businessUnit.LoanUrl = "https://www.ahclending.com/LoanApplication/Home";
            businessUnit.WebUrl = "https://localAhc.RainsoftFn.com";
            businessUnit.LoanLoginUrl = "https://www.ahclending.com/LoanApplication/Home";

            StatusList statusList = new StatusList();
            statusList.Id = 1;
            statusList.Name = "Floating";

            State state = new State();
            state.Id = 1;
            state.Name = "Alaska";
            state.Abbreviation = "AK";

            AddressInfo addressInfo = new AddressInfo();
            addressInfo.Id = 7387;
            addressInfo.Name = "Texas";
            addressInfo.UnitNo = "428";
            addressInfo.StreetAddress = "568 Broadway";
            addressInfo.StateName = "Texas";
            addressInfo.CountryName = "America";
            addressInfo.CountyName = "Harris";
            addressInfo.CityName = "Houston";
            addressInfo.ZipCode = "77055";
            addressInfo.State = state;

            PropertyUsage propertyUsage = new PropertyUsage();
            propertyUsage.Id = 1;
            propertyUsage.Name = "Primary Residence";

            PropertyType propertyType = new PropertyType();
            propertyType.Id = 1;
            propertyType.Name = "Single Family Detached";

            PropertyInfo propertyInfo = new PropertyInfo();
            propertyInfo.Id = 7387;
            propertyInfo.AddressInfo = addressInfo;
            propertyInfo.PropertyType = propertyType;
            propertyInfo.PropertyUsage = propertyUsage;

            LoanPurpose loanPurpose = new LoanPurpose();
            loanPurpose.Id = 1;
            loanPurpose.Name = "Purchase";

            List<LoanApplication> lstLoanApplication = new List<LoanApplication>();
            LoanApplication loanApplication1 = new LoanApplication();
            loanApplication1.Id = 4189;
            loanApplication1.BusinessUnit = businessUnit;
            loanApplication1.StatusList = statusList;
            loanApplication1.PropertyInfo = propertyInfo;
            loanApplication1.LoanAmount = 800000;
            loanApplication1.LoanPurpose = loanPurpose;

            ResidencyState residencyState1 = new ResidencyState();
            residencyState1.Id = 1;
            residencyState1.Name = "US Citizen";

            ResidencyState residencyState2 = new ResidencyState();
            residencyState2.Id = 2;
            residencyState2.Name = "Permanent Resident";

            LoanContact loanContact1 = new LoanContact();
            loanContact1.Id = 1;
            loanContact1.FirstName = "tunner";
            loanContact1.LastName = "holland";
            loanContact1.EmailAddress = "tunner.holland@gmail.com";
            loanContact1.ResidencyState = residencyState1;

            LoanContact loanContact2 = new LoanContact();
            loanContact2.Id = 2;
            loanContact2.FirstName = "minaz";
            loanContact2.LastName = "karim";
            loanContact2.EmailAddress = "minaz.karim@gmail.com";
            loanContact1.ResidencyState = residencyState2;

            State stateBorrower = new State();
            stateBorrower.Id = 11;
            stateBorrower.Name = "Alaska";
            stateBorrower.Abbreviation = "AK";

            AddressInfo addressInfoBorrower = new AddressInfo();
            addressInfoBorrower.Id = 7395;
            addressInfoBorrower.Name = "Texas";
            addressInfoBorrower.UnitNo = "428";
            addressInfoBorrower.StreetAddress = "568 Broadway";
            addressInfoBorrower.StateName = "Texas";
            addressInfoBorrower.CountryName = "America";
            addressInfoBorrower.CountyName = "Harris";
            addressInfoBorrower.CityName = "Houston";
            addressInfoBorrower.ZipCode = "77055";
            addressInfoBorrower.State = stateBorrower;

            BorrowerResidence borrowerResidence = new BorrowerResidence();
            borrowerResidence.LoanAddress = addressInfoBorrower;

            State stateCoBorrower = new State();
            stateCoBorrower.Id = 12;
            stateCoBorrower.Name = "Alaska";
            stateCoBorrower.Abbreviation = "AK";

            AddressInfo addressInfoCoBorrower = new AddressInfo();
            addressInfoCoBorrower.Id = 7396;
            addressInfoCoBorrower.Name = "Texas";
            addressInfoCoBorrower.UnitNo = "428";
            addressInfoCoBorrower.StreetAddress = "568 Broadway";
            addressInfoCoBorrower.StateName = "Texas";
            addressInfoCoBorrower.CountryName = "America";
            addressInfoCoBorrower.CountyName = "Harris";
            addressInfoCoBorrower.CityName = "Houston";
            addressInfoCoBorrower.ZipCode = "77055";
            addressInfoCoBorrower.State = stateBorrower;

            BorrowerResidence coBorrowerResidence = new BorrowerResidence();
            coBorrowerResidence.LoanAddress = addressInfoBorrower;

            Borrower borrower1 = new Borrower();
            borrower1.Id = 6194;
            borrower1.LoanContactId = 6189;
            borrower1.LoanApplicationId = 4189;
            borrower1.LoanContact = loanContact1;
            borrower1.BorrowerResidences.Add(borrowerResidence);
            loanApplication1.Borrowers.Add(borrower1);

            Borrower borrower2 = new Borrower();
            borrower2.Id = 6195;
            borrower2.LoanContactId = 6190;
            borrower2.LoanApplicationId = 4189;
            borrower2.LoanContact = loanContact2;
            borrower2.BorrowerResidences.Add(coBorrowerResidence);
            loanApplication1.Borrowers.Add(borrower2);

            ContactEmailInfo contactEmailInfo = new ContactEmailInfo();
            contactEmailInfo.Email = "hammad@gmail.com";
            contactEmailInfo.IsPrimary = true;
            contactEmailInfo.ValidityId = 1;

            Contact contact = new Contact();
            contact.Id = 205;
            contact.FirstName = "Sal";
            contact.LastName = "Prasla";
            contact.ContactEmailInfoes.Add(contactEmailInfo);

            Customer customer = new Customer();
            customer.Id = 205;
            customer.Contact = contact;

            //CompanyPhoneInfo companyPhoneInfo = new CompanyPhoneInfo();
            //companyPhoneInfo.Id = 20;
            //companyPhoneInfo.Phone = "9725733900";
            EmailAccount eAccount = new EmailAccount();
            eAccount.Id = 205;
            eAccount.Email = "talha@gmail.com";

            EmployeePhoneBinder employeePhoneBinder = new EmployeePhoneBinder();
            employeePhoneBinder.Id = 1;
            employeePhoneBinder.EmployeeId = 2;
            employeePhoneBinder.CompanyPhoneInfoId = 20;
            employeePhoneBinder.TypeId = 3;

            EmployeeBusinessUnitEmail employeeBusinessUnit = new EmployeeBusinessUnitEmail();
            employeeBusinessUnit.Id = 205;
            employeeBusinessUnit.EmployeeId = 205;
            employeeBusinessUnit.BusinessUnitId = 205;
            employeeBusinessUnit.EmailAccount = eAccount;

            Employee employee = new Employee();
            employee.Id = 2;
            employee.NmlsNo = "286821";
            employee.CmsName = "sal-prasla";
            employee.EmployeeBusinessUnitEmails.Add(employeeBusinessUnit);
            employee.EmployeePhoneBinders.Add(employeePhoneBinder);

            Opportunity opportunity = new Opportunity();
            opportunity.Id = 205;
            opportunity.Owner = employee;
            opportunity.OpportunityLeadBinders.Add(new OpportunityLeadBinder() { OwnTypeId = 1, Customer = customer });
            mockOpportunityService.Setup(x => x.GetSingleOpportunity(It.IsAny<int>())).ReturnsAsync(opportunity);

            Employee emp = new Employee();
            emp.Id = 205;
            emp.IsDeleted = false;
            emp.IsActive = true;
            employee.Contact = contact;
            emp.EmployeeBusinessUnitEmails.Add(employeeBusinessUnit);

            UserProfile profile = new UserProfile();
            profile.BusinessUnitId = 205;
            profile.Employees.Add(emp);

            loanApplication1.Opportunity = opportunity;

            lstLoanApplication.Add(loanApplication1);

            mockUserProfileService.Setup(x => x.GetUserProfileEmployeeDetail(It.IsAny<int?>(), It.IsAny<UserProfileService.RelatedEntities>())).ReturnsAsync(profile);
            mockLoanApplicationService.Setup(x => x.GetLoanApplicationWithDetails(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<LoanApplicationService.RelatedEntities>())).Returns(lstLoanApplication);
            ISettingService settingService = new SettingService(mockOpportunityService.Object, mockUserProfileService.Object, mockLoanApplicationService.Object,null,null,new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            EmailTemplate res = await settingService.RenderEmailTokens(1, 4189, 205, "###RequestorUserEmail###", "###RequestorUserEmail###", "You have new tasks to complete for your ###BusinessUnitName### loan application", "<p>Please submit following documents</p>\n<p>###RequestDocumentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n", lstTokenModels);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(1, res.id);
            Assert.Equal("talha@gmail.com", res.fromAddress);
            Assert.Equal("talha@gmail.com", res.CCAddress);
            Assert.Equal("tunner.holland@gmail.com", res.toAddress);
            Assert.Equal("You have new tasks to complete for your AHCLending loan application", res.subject);
        }
        [Fact]
        public async Task TestGetLoginUserEmailNullService()
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
                Id = 4190,
                OpportunityId = 206,
                BusinessUnitId = 206,
                IsDeleted = false,
                IsActive = true
            };
            dataContext.Set<RainMaker.Entity.Models.LoanApplication>().Add(loanApplication);

            RainMaker.Entity.Models.UserProfile userProfile = new RainMaker.Entity.Models.UserProfile()
            {
                Id = 206,
                IsDeleted = false,
                IsActive = true
            };
            dataContext.Set<RainMaker.Entity.Models.UserProfile>().Add(userProfile);

            dataContext.SaveChanges();

            List<TokenModel> lstTokenModels = new List<TokenModel>();

            lstTokenModels.Add(new TokenModel() { description = "Current Date", key = "Date", name = "Date", symbol = "###Date###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Email Address", key = "PrimaryBorrowerEmailAddress", name = "Primary Borrower Email Address", symbol = "###PrimaryBorrowerEmailAddress###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower First Name", key = "PrimaryBorrowerFirstName", name = "Primary Borrower First Name", symbol = "###PrimaryBorrowerFirstName###" });
            lstTokenModels.Add(new TokenModel() { description = "Primary Borrower Last Name", key = "PrimaryBorrowerLastName", name = "Primary Borrower Last Name", symbol = "###PrimaryBorrowerLastName###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower First Name", key = "Co-BorrowerFirstName", name = "Co-Borrower First Name", symbol = "###Co-BorrowerFirstName###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Last Name", key = "Co-BorrowerLastName", name = "Co-Borrower Last Name", symbol = "###Co-BorrowerLastName###" });
            lstTokenModels.Add(new TokenModel() { description = "Co-Borrower Email Address", key = "Co-BorrowerEmailAddress", name = "Co-Borrower Email Address", symbol = "###Co-BorrowerEmailAddress###" });
            lstTokenModels.Add(new TokenModel() { description = "Unique email tag for identifying email sender", key = "EmailTag", name = "EmailTag", symbol = "###EmailTag###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Portal Url", key = "LoanPortalUrl", name = "Loan Portal Url", symbol = "###LoanPortalUrl###" });
            lstTokenModels.Add(new TokenModel() { description = "Status of Borrower Loan Application on Colaba", key = "LoanStatus", name = "Loan Status", symbol = "###LoanStatus###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property Address", key = "SubjectPropertyAddress", name = "Subject Property Address", symbol = "###SubjectPropertyAddress###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property State", key = "SubjectPropertyState", name = "Subject Property State", symbol = "###SubjectPropertyState###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property State Abbreviation", key = "SubjectPropertyStateAbbreviation", name = "Subject Property State Abbreviation", symbol = "###SubjectPropertyStateAbbreviation###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property County", key = "SubjectPropertyCounty", name = "Subject Property County", symbol = "###SubjectPropertyCounty###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property City", key = "SubjectPropertyCity", name = "Subject Property City", symbol = "###SubjectPropertyCity###" });
            lstTokenModels.Add(new TokenModel() { description = "Subject Property Zip Code", key = "SubjectPropertyZipCode", name = "Subject Property ZipCode", symbol = "###SubjectPropertyZipCode###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Purpose", key = "LoanPurpose", name = "Loan Purpose", symbol = "###LoanPurpose###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Application Loan Amount", key = "LoanAmount", name = "Loan Amount", symbol = "###LoanAmount###" });
            lstTokenModels.Add(new TokenModel() { description = "Property Value", key = "PropertyValue", name = "Property Value", symbol = "###PropertyValue###" });
            lstTokenModels.Add(new TokenModel() { description = "Property Type", key = "PropertyType", name = "Property Type", symbol = "###PropertyType###" });
            lstTokenModels.Add(new TokenModel() { description = "Property Usage", key = "PropertyUsage", name = "Property Usage", symbol = "###PropertyUsage###" });
            lstTokenModels.Add(new TokenModel() { description = "Residency Type", key = "ResidencyType", name = "Residency Type", symbol = "###ResidencyType###" });
            lstTokenModels.Add(new TokenModel() { description = "Branch Nmls No", key = "BranchNmlsNo", name = "Branch Nmls No", symbol = "###BranchNmlsNo###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Application Business Unit Name", key = "BusinessUnitName", name = "Business Unit Name", symbol = "###BusinessUnitName###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Application Business Unit Toll Free Number", key = "BusinessUnitPhoneNumber", name = "Business Unit Phone Number", symbol = "###BusinessUnitPhoneNumber###" });
            lstTokenModels.Add(new TokenModel() { description = "Business Unit Website URL", key = "BusinessUnitWebSiteUrl", name = "Business Unit WebSite Url", symbol = "###BusinessUnitWebSiteUrl###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Application Login Link", key = "LoanApplicationLoginLink", name = "Loan Application Login Link", symbol = "###LoanApplicationLoginLink###" });
            lstTokenModels.Add(new TokenModel() { description = "Web Page of Loan Officer", key = "LoanOfficerPageUrl", name = "Loan Officer Page Url", symbol = "###LoanOfficerPageUrl###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Officer First Name", key = "LoanOfficerFirstName", name = "Loan Officer First Name", symbol = "###LoanOfficerLastName###" });
            lstTokenModels.Add(new TokenModel() { description = "Loan Officer Last Name", key = "LoanOfficerLastName", name = "Loan Officer Last Name", symbol = "###BusinessUnitName###" });
            lstTokenModels.Add(new TokenModel() { description = "Bullet Point list of Documents being requested", key = "RequestDocumentList", name = "Request Document List", symbol = "###RequestDocumentList###" });
            lstTokenModels.Add(new TokenModel() { description = "Email Address of the Needs List requestor", key = "RequestorUserEmail", name = "Requestor User Email", symbol = "###RequestorUserEmail###" });

            Mock<IOpportunityService> mockOpportunityService = new Mock<IOpportunityService>();
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();
            Mock<ILoanApplicationService> mockLoanApplicationService = new Mock<ILoanApplicationService>();

            List<LoanApplication> lstLoanApplication = new List<LoanApplication>();
            LoanApplication loanApplication1 = new LoanApplication();
            loanApplication1.Id = 4190;

            Borrower borrower1 = new Borrower();
            borrower1.Id = 6194;
            borrower1.LoanContactId = 6189;
            borrower1.LoanApplicationId = 4190;
            loanApplication1.Borrowers.Add(borrower1);

            Borrower borrower2 = new Borrower();
            borrower2.Id = 6195;
            borrower2.LoanContactId = 6190;
            borrower2.LoanApplicationId = 4190;
            loanApplication1.Borrowers.Add(borrower2);


            ContactEmailInfo contactEmailInfo = new ContactEmailInfo();
            contactEmailInfo.Email = "hammad@gmail.com";
            contactEmailInfo.IsPrimary = true;
            contactEmailInfo.ValidityId = 1;

            Contact contact = new Contact();
            contact.Id = 206;
            contact.ContactEmailInfoes.Add(contactEmailInfo);

            Customer customer = new Customer();
            customer.Id = 206;
            customer.Contact = contact;

            Opportunity opportunity = new Opportunity();
            opportunity.Id = 206;
            opportunity.OpportunityLeadBinders.Add(new OpportunityLeadBinder() { OwnTypeId = 1, Customer = customer });
            mockOpportunityService.Setup(x => x.GetSingleOpportunity(It.IsAny<int>())).ReturnsAsync(opportunity);

            EmailAccount eAccount = new EmailAccount();
            eAccount.Id = 206;
            eAccount.Email = "talha@gmail.com";

            EmployeeBusinessUnitEmail employeeBusinessUnit = new EmployeeBusinessUnitEmail();
            employeeBusinessUnit.Id = 206;
            employeeBusinessUnit.EmployeeId = 206;
            employeeBusinessUnit.BusinessUnitId = 206;
            employeeBusinessUnit.EmailAccount = eAccount;

            Employee emp = new Employee();
            emp.Id = 206;
            emp.IsDeleted = false;
            emp.IsActive = true;

            UserProfile profile = new UserProfile();
            profile.BusinessUnitId = 206;
            profile.Employees.Add(emp);

            loanApplication1.Opportunity = opportunity;

            lstLoanApplication.Add(loanApplication1);

            mockUserProfileService.Setup(x => x.GetUserProfileEmployeeDetail(It.IsAny<int?>(), It.IsAny<UserProfileService.RelatedEntities>())).ReturnsAsync(profile);
            mockLoanApplicationService.Setup(x => x.GetLoanApplicationWithDetails(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<LoanApplicationService.RelatedEntities>())).Returns(lstLoanApplication);
            ISettingService settingService = new SettingService(mockOpportunityService.Object, mockUserProfileService.Object, mockLoanApplicationService.Object, null, null, new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            EmailTemplate res = await settingService.RenderEmailTokens(1, 4190, 207, "###RequestorUserEmail###", "###RequestorUserEmail###", "You have new tasks to complete for your ###BusinessUnitName### loan application", "<p>Please submit following documents</p>\n<p>###RequestDocumentList###</p>\n<p>Thank you.</p>\n<p><strong>###BusinessUnitName###</strong></p>\n", lstTokenModels);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(1, res.id);
            Assert.Equal("You have new tasks to complete for your  loan application", res.subject);
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

            ISettingService settingService = new SettingService(null,null, null,null, null, new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

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

            ISettingService settingService = new SettingService(null, null,null, null, null, new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
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

            ISettingService settingService = new SettingService(null, null,null, null, null, new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

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

            ISettingService settingService = new SettingService(null, null, null, null, null, new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

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
