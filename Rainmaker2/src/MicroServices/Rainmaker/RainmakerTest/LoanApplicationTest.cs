using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Rainmaker.API.Controllers;
using Rainmaker.Model;
using Rainmaker.Service;
using Rainmaker.Service.Helpers;
using RainMaker.Common;
using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System.IO;
using System.Threading.Tasks;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;
using System.Web;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Linq;
using System;
using System.Collections.Generic;
using TrackableEntities.Common.Core;
using Microsoft.Extensions.Logging;
using Rainmaker.Model.Borrower;

namespace RainmakerTest
{
    public class LoanApplicationTest
    {
        [Fact]
        public async Task TestGetLoanSummaryController()
        {
            Mock<ILoanApplicationService> mock = new Mock<ILoanApplicationService>();
            LoanSummary obj = new LoanSummary() { CityName = "Karachi" };

            mock.Setup(x => x.GetLoanSummary(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(obj);

            var loanApplicationController = new LoanApplicationController(mock.Object, null, null, null, null, null, null, null, null);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            loanApplicationController.ControllerContext = context;
            //Act
            IActionResult result = await loanApplicationController.GetLoanInfo(1);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as LoanSummary;
            Assert.NotNull(content);
            Assert.Equal("Karachi", content.CityName);
        }
        [Fact]
        public async Task TestGetLoanSummaryService()
        {
            //Arrange

            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            LoanApplication app = new LoanApplication()
            {
                Id = 2000,
                LoanAmount = 1000,
                LoanPurposeId = 2000,
                EntityTypeId = 2000,
                SubjectPropertyDetailId = 2000,
                OpportunityId = 2000
            };
            dataContext.Set<LoanApplication>().Add(app);

            Opportunity opportunity = new Opportunity
            {
                Id = 2000,
                IsActive = true,
                EntityTypeId = 2000,
                IsDeleted = false,
                NoRuleMatched = false,
                IsAutoAssigned = true,
                IsPickedByOwner = true,
                IsDuplicate = false,
                BusinessUnitId = 2000,
                OwnerId = 2000
            };
            dataContext.Set<Opportunity>().Add(opportunity);

            OpportunityLeadBinder opportunityLeadBinder = new OpportunityLeadBinder
            {
                Id = 2000,
                OpportunityId = 2000,
                CustomerId = 2000,
                OwnTypeId = 1
            };
            dataContext.Set<OpportunityLeadBinder>().Add(opportunityLeadBinder);

            Customer customer = new Customer()
            {
                Id = 2000,
                UserId = 2000,
                EntityTypeId = 2000,
                DisplayOrder = 1,
                IsActive = true,
                IsSystem = true,
                IsDeleted = false
            };
            dataContext.Set<Customer>().Add(customer);

            PropertyInfo propertyInfo = new PropertyInfo()
            {
                Id = 2000,
                PropertyTypeId = 2000,
                AddressInfoId = 2000,
                PropertyValue = 458780
            };
            dataContext.Set<PropertyInfo>().Add(propertyInfo);

            PropertyType propertyType = new PropertyType()
            {
                Id = 2000,
                Description = "Test",
                Name = "",
                DisplayOrder = 1,
                IsDefault = true,
                IsActive = true,
                EntityTypeId = 2000,
                IsDeleted = false,
                TpId = "",
                IsSystem = true
            };
            dataContext.Set<PropertyType>().Add(propertyType);

            AddressInfo addressInfo = new AddressInfo
            {
                Id = 2000,
                IsDeleted = false,
                EntityTypeId = 2000,
                CityName = "Karachi",
                CountyName = "Pakistan",
                StateName = "Sindh",
                ZipCode = "7550",
                StreetAddress = "abc"

            };
            dataContext.Set<AddressInfo>().Add(addressInfo);

            LoanPurpose loanPurpose = new LoanPurpose
            {
                Id = 2000,
                Name = "abc",
                Description = "abc",
                IsPurchase = true,
                DisplayOrder = 1,
                IsActive = true,
                IsDefault = true,
                IsDeleted = false,
                IsSystem = true,
                EntityTypeId = 2000,
                TpId = string.Empty

            };
            dataContext.Set<LoanPurpose>().Add(loanPurpose);
            State state = new State
            {
                Id = 2000,
                Name = "",


            };
            dataContext.Set<State>().Add(state);
            StatusList statusList = new StatusList
            {
                Id = 2000,
                IsDeleted = true

            };
            dataContext.Set<StatusList>().Add(statusList);

            Borrower borrower = new Borrower
            {
                Id = 2000,
                LoanApplicationId = 2000
            };
            dataContext.Set<Borrower>().Add(borrower);

            dataContext.SaveChanges();
            
            Mock<ICommonService> mockcommonservice = new Mock<ICommonService>();
            mockcommonservice.Setup(x => x.GetSettingFreshValueByKeyAsync<string>(SystemSettingKeys.AdminDomainUrl, 1, default)).ReturnsAsync(string.Empty);
            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, mockcommonservice.Object);

            //Act
            LoanSummary res = await loanService.GetLoanSummary(2000, 2000);

            // Assert
            Assert.NotNull(res);
            Assert.Equal("Karachi", res.CityName);
            Assert.Equal("Pakistan", res.CountyName);
            Assert.Equal("Sindh", res.StateName);
            Assert.Equal("abc", res.StreetAddress);
            Assert.Equal("7550", res.ZipCode);
            Assert.Equal(1000, res.LoanAmount);
            Assert.Equal("abc", res.LoanPurpose);
            Assert.Equal("7550", res.ZipCode);
            Assert.Equal("Test", res.PropertyType);
        }
        [Fact]
        public async Task TestGetLOInfoIsNotNullController()
        {
            //Arrange
            Mock<ILoanApplicationService> mock = new Mock<ILoanApplicationService>();

            var loanApplicationController = new LoanApplicationController(mock.Object, null, null, null, null, null, null, null, null);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            loanApplicationController.ControllerContext = context;

            LoanOfficer obj = new LoanOfficer() { FirstName = "Smith" };
            LoanApplicationModel loanApplicationModel = new LoanApplicationModel();
            loanApplicationModel.BusinessUnitId = 1;
            mock.Setup(x => x.GetLOInfo(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(obj);
            mock.Setup(x => x.GetByLoanApplicationId(It.IsAny<int>())).ReturnsAsync(loanApplicationModel);

            //Act
            IActionResult result = await loanApplicationController.GetLOInfo(1);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as LoanOfficer;
            Assert.NotNull(content);
            Assert.Equal("Smith", content.FirstName);
        }
        [Fact]
        public async Task TestGetLOInfoIsnullController()
        {
            //Arrange
            Mock<ILoanApplicationService> mock = new Mock<ILoanApplicationService>();

            var loanApplicationController = new LoanApplicationController(mock.Object, null, null, null, null, null, null, null, null);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            loanApplicationController.ControllerContext = context;

            LoanOfficer obj = new LoanOfficer() { FirstName = null };

            LoanApplicationModel loanApplicationModel = new LoanApplicationModel();
            loanApplicationModel.BusinessUnitId = 1;
            mock.Setup(x => x.GetLOInfo(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(obj);
            mock.Setup(x => x.GetByLoanApplicationId(It.IsAny<int>())).ReturnsAsync(loanApplicationModel);

            //Act
            IActionResult result = await loanApplicationController.GetLOInfo(1);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as LoanOfficer;
            Assert.Null(content);
           
        }
        [Fact]
        public async Task TestGetLOInfoIsnullObjController()
        {
            //Arrange
            Mock<ILoanApplicationService> mock = new Mock<ILoanApplicationService>();

            var loanApplicationController = new LoanApplicationController(mock.Object, null, null, null, null, null, null, null, null);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            loanApplicationController.ControllerContext = context;

            LoanOfficer obj = null;

            LoanApplicationModel loanApplicationModel = new LoanApplicationModel();
            loanApplicationModel.BusinessUnitId = 1;
            mock.Setup(x => x.GetLOInfo(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(obj);
            mock.Setup(x => x.GetByLoanApplicationId(It.IsAny<int>())).ReturnsAsync(loanApplicationModel);

            //Act
            IActionResult result = await loanApplicationController.GetLOInfo(1);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as LoanOfficer;
            Assert.Null(content);
            
        }
       [Fact]
        public async Task GetLOInfo()
        {
            //Arrange

            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();
            LoanApplication app = new LoanApplication()
            {
                Id = 10,
                LoanAmount = 1000,
                LoanPurposeId = 10,
                EntityTypeId = 1,
                OpportunityId = 10,
                BusinessUnitId = 10,
            };
            dataContext.Set<LoanApplication>().Add(app);

            Opportunity opportunity = new Opportunity
            {
                Id = 10,
                IsActive = true,
                EntityTypeId = 1,
                IsDeleted = false,
                NoRuleMatched = false,
                IsAutoAssigned = true,
                IsPickedByOwner = true,
                IsDuplicate = false
                ,
                BusinessUnitId = 10
                ,
                OwnerId = 10
            };
            dataContext.Set<Opportunity>().Add(opportunity);

            OpportunityLeadBinder opportunityLeadBinder = new OpportunityLeadBinder
            {
                Id = 10,
                OpportunityId = 10,
                CustomerId = 10,
                OwnTypeId = 1
            };
            dataContext.Set<OpportunityLeadBinder>().Add(opportunityLeadBinder);

            Customer customer = new Customer()
            {
                Id = 10,
                UserId = 10,
                EntityTypeId = 1,
                DisplayOrder = 1,
                IsActive = true,
                IsSystem = true,
                IsDeleted = false
            };
            dataContext.Set<Customer>().Add(customer);

            Employee employee = new Employee
            {
                Id = 10,
                IsActive = true,
                IsSystem = true,
                EntityTypeId = 1,
                IsDeleted = false,
                NmlsNo = "030012345",
                Photo = "abc.png",
                CmsName = "Shehroz"
                ,
                ContactId = 10

            };
            dataContext.Set<Employee>().Add(employee);


            EmployeePhoneBinder employeePhoneBinder = new EmployeePhoneBinder

            {
                Id = 10,
                EmployeeId = 10,
                CompanyPhoneInfoId = 10,
                TypeId = 3


            };
            dataContext.Set<EmployeePhoneBinder>().Add(employeePhoneBinder);
            CompanyPhoneInfo companyPhoneInfo = new CompanyPhoneInfo
            {
                Id = 10,
                Phone = "030012345678",
                IsDeleted = false,
                EntityTypeId = 1,
                IsDefault = true,
                DisplayOrder = 1,
                IsActive = true,
                IsSystem = true

            };

            dataContext.Set<CompanyPhoneInfo>().Add(companyPhoneInfo);

            EmployeeBusinessUnitEmail employeeBusinessUnitEmail = new EmployeeBusinessUnitEmail
            {
                Id = 10,
                EmployeeId = 10,
                EmailAccountId = 10,
                TypeId = 1,
                BusinessUnitId = 10

            };

            dataContext.Set<EmployeeBusinessUnitEmail>().Add(employeeBusinessUnitEmail);

            BusinessUnit businessUnit = new BusinessUnit
            {
                Id = 10,
                DisplayOrder = 1,
                IsActive = true,
                EntityTypeId = 1,
                IsDefault = true,
                IsSystem = true,
                IsDeleted = false,
                WebUrl = "https://entityframeworkcore.com/"

            };
            dataContext.Set<BusinessUnit>().Add(businessUnit);

            EmailAccount emailAccount = new EmailAccount
            {
                Id = 10,
                UseDefaultCredentials = true,
                UseReplyTo = true,
                DisplayOrder = 1,
                IsActive = true,
                EntityTypeId = 1,
                IsDefault = true,
                IsSystem = true,
                IsDeleted = false,
                Email = "shehroz@gmail.com"

            };
            dataContext.Set<EmailAccount>().Add(emailAccount);

            Contact contact = new Contact
            {
                Id = 10,
                EntityTypeId = 1,
                IsDeleted = false,
                FirstName = "Shehroz",
                LastName = "Riaz"
            };
            dataContext.Set<Contact>().Add(contact);

            dataContext.SaveChanges();

            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);

            //Act
            LoanOfficer res = await loanService.GetLOInfo(10, 10, 10);

            // Assert
            Assert.NotNull(res);
            Assert.Equal("shehroz@gmail.com", res.Email);
            Assert.Equal("Shehroz", res.FirstName);
            Assert.Equal("Riaz", res.LastName);
            Assert.Equal("030012345", res.NMLS);
            Assert.Equal("030012345678", res.Phone);
            Assert.Equal("abc.png", res.Photo);
            Assert.Equal("https://entityframeworkcore.com//lo/Shehroz", res.WebUrl);
        }
        [Fact]
        public async Task TestGetPhotoIsNotNullController()
        {
            //Arrange
            Mock<ILoanApplicationService> mock = new Mock<ILoanApplicationService>();
            Mock<ICommonService> mockcommonservice = new Mock<ICommonService>();
            Mock<IFtpHelper> mockftpservice = new Mock<IFtpHelper>();
            int? businessUnitId = 1;

            mockcommonservice.Setup(x => x.GetSettingFreshValueByKeyAsync<string>(SystemSettingKeys.FtpEmployeePhotoFolder, businessUnitId, default)).ReturnsAsync(string.Empty);
            mockftpservice.Setup(x => x.DownloadStream(It.IsAny<string>())).ReturnsAsync(new MemoryStream());

            LoanApplicationModel loanApplicationModel = new LoanApplicationModel();
            loanApplicationModel.BusinessUnitId = 1;

            mock.Setup(x => x.GetByLoanApplicationId(It.IsAny<int>())).ReturnsAsync(loanApplicationModel);

            LoanApplicationController controller = new LoanApplicationController(mock.Object, mockcommonservice.Object, mockftpservice.Object, null, null, null, null, null, null);


            //Act
            string result = await controller.GetPhoto(SystemSettingKeys.FtpEmployeePhotoFolder, (int)businessUnitId);

            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task TestGetPhotoNullController()
        {
            //Arrange
            Mock<ILoanApplicationService> mock = new Mock<ILoanApplicationService>();
            Mock<ICommonService> mockcommonservice = new Mock<ICommonService>();
            Mock<IFtpHelper> mockftpservice = new Mock<IFtpHelper>();
            int? businessUnitId = 1;

            mockcommonservice.Setup(x => x.GetSettingFreshValueByKeyAsync<string>(SystemSettingKeys.FtpEmployeePhotoFolder, businessUnitId, default)).ReturnsAsync(string.Empty);
           
            mockftpservice.Setup(x => x.DownloadStream(It.IsAny<string>())).Throws(new Exception(""));

            LoanApplicationModel loanApplicationModel = new LoanApplicationModel();
            loanApplicationModel.BusinessUnitId = 1;

            mock.Setup(x => x.GetByLoanApplicationId(It.IsAny<int>())).ReturnsAsync(loanApplicationModel);

            LoanApplicationController controller = new LoanApplicationController(mock.Object, mockcommonservice.Object, mockftpservice.Object, null, null, null, null, null, null);

            //Act
            string result = await controller.GetPhoto(It.IsAny<string>()
                //ystemSettingKeys.FtpEmployeePhotoFolder
                , It.IsAny<int>());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);


        }
        [Fact]
        public async Task GetDbaInfo()
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
                Id = 6,
                Name = "Shehroz",
                DisplayOrder = 1,
                IsActive = true,
                IsDefault = true,
                IsSystem = true,
                IsDeleted = false,
                EmailAccountId = 6,
                Logo = "",
                WebUrl = "https://entityframeworkcore.com//lo/Shehroz"



            };
            dataContext.Set<BusinessUnit>().Add(businessUnit);

            EmailAccount emailAccount = new EmailAccount
            {
                Id = 6,
                UseDefaultCredentials = true,
                UseReplyTo = true,
                DisplayOrder = 1,
                IsActive = true,
                EntityTypeId = 1,
                IsDefault = true,
                IsSystem = true,
                IsDeleted = false,
                Email = "shehroz@gmail.com"

            };
            dataContext.Set<EmailAccount>().Add(emailAccount);

            CompanyPhoneInfo companyPhoneInfo = new CompanyPhoneInfo
            {
                Id = 6,
                Phone = "030012345678",
                IsDeleted = false,
                EntityTypeId = 1,
                IsDefault = true,
                DisplayOrder = 1,
                IsActive = true,
                IsSystem = true

            };

            dataContext.Set<CompanyPhoneInfo>().Add(companyPhoneInfo);
            BusinessUnitPhone businessUnitPhone = new BusinessUnitPhone()
            {
                Id = 1,
                BusinessUnitId = 6,
                CompanyPhoneInfoId = 6,
                TypeId = 3

            };
            dataContext.Set<BusinessUnitPhone>().Add(businessUnitPhone);
            dataContext.SaveChanges();

            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);

            //Act
            LoanOfficer res = await loanService.GetDbaInfo(6);

            // Assert
            Assert.NotNull(res);
            Assert.Equal("shehroz@gmail.com", res.Email);
            Assert.Equal("Shehroz", res.FirstName);
            Assert.Equal("", res.LastName);
            Assert.Null(res.NMLS);
            Assert.Equal("030012345678", res.Phone);
            Assert.Equal("", res.Photo);
            Assert.Equal("https://entityframeworkcore.com//lo/Shehroz", res.WebUrl);
        }
        [Fact]
        public async Task TestPostLoanApplicationController()
        {
            //Arrange
            Mock<ILoanApplicationService> mock = new Mock<ILoanApplicationService>();
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));
            LoanApplicationController loanApplicationController = new LoanApplicationController(mock.Object, null, null, null, null, null, null, null, null);

            loanApplicationController.ControllerContext = context;
            PostLoanApplicationModel model = new PostLoanApplicationModel()
            {
                loanApplicationId = 1,
                isDraft = true
            };
            //Act
            var res = await loanApplicationController.PostLoanApplication(model);

            //Assert
            Assert.NotNull(res);
            Assert.IsType<OkObjectResult>(res);

        }
        [Fact]
        public async Task TestPostLoanApplicationServiceTrue()
        {
            //Arrange

            Mock<IOpportunityService> mock = new Mock<IOpportunityService>();
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            LoanApplication app = new LoanApplication()
            {
                Id = 4,
                LoanAmount = 1000,
                LoanPurposeId = 1,
                EntityTypeId = 1,
                SubjectPropertyDetailId = 1,
                OpportunityId = 4
            };
            dataContext.Set<LoanApplication>().Add(app);

            Opportunity opportunity = new Opportunity
            {
                Id = 4,
                IsActive = true,
                EntityTypeId = 1,
                IsDeleted = false,
                NoRuleMatched = false,
                IsAutoAssigned = true,
                IsPickedByOwner = true,
                IsDuplicate = false,
                BusinessUnitId = 1,
                OwnerId = 1
            };
            dataContext.Set<Opportunity>().Add(opportunity);

            OpportunityLeadBinder opportunityLeadBinder = new OpportunityLeadBinder
            {
                Id = 4,
                OpportunityId = 4,
                CustomerId = 4,
                OwnTypeId = 1
            };
            dataContext.Set<OpportunityLeadBinder>().Add(opportunityLeadBinder);

            Customer customer = new Customer()
            {
                Id = 4,
                UserId = 1,
                EntityTypeId = 1,
                DisplayOrder = 1,
                IsActive = true,
                IsSystem = true,
                IsDeleted = false,
                ContactId = 4
            };
            dataContext.Set<Customer>().Add(customer);

            Contact contact = new Contact()
            {
                Id = 4,
                EntityTypeId = 1,
                IsDeleted = false
            };
            dataContext.Set<Contact>().Add(contact);
            dataContext.SaveChanges();
            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);

            //Act
            PostModel res = await loanService.PostLoanApplication(4, true, 1, mock.Object);
            // Assert
            Assert.NotNull(res);
            Assert.Equal(1, res.userId);
            Assert.Equal(" ", res.userName);
        }
        [Fact]
        public async Task TestPostLoanApplicationServiceFalse()
        {
            //Arrange

            Mock<IOpportunityService> mock = new Mock<IOpportunityService>();
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            LoanApplication app = new LoanApplication()
            {
                Id = 3,
                LoanAmount = 1000,
                LoanPurposeId = 1,
                EntityTypeId = 1,
                SubjectPropertyDetailId = 1,
                OpportunityId = 3
            };
            dataContext.Set<LoanApplication>().Add(app);

            Opportunity opportunity = new Opportunity
            {
                Id = 3,
                IsActive = true,
                EntityTypeId = 1,
                IsDeleted = false,
                NoRuleMatched = false,
                IsAutoAssigned = true,
                IsPickedByOwner = true,
                IsDuplicate = false,
                BusinessUnitId = 1,
                OwnerId = 1
            };
            dataContext.Set<Opportunity>().Add(opportunity);

            OpportunityLeadBinder opportunityLeadBinder = new OpportunityLeadBinder
            {
                Id = 3,
                OpportunityId = 3,
                CustomerId = 3,
                OwnTypeId = 1
            };
            dataContext.Set<OpportunityLeadBinder>().Add(opportunityLeadBinder);

            Customer customer = new Customer()
            {
                Id = 3,
                UserId = 1,
                EntityTypeId = 1,
                DisplayOrder = 1,
                IsActive = true,
                IsSystem = true,
                IsDeleted = false,
                ContactId = 2
            };
            dataContext.Set<Customer>().Add(customer);

            Contact contact = new Contact()
            {
                Id = 3,
                EntityTypeId = 1,
                IsDeleted = false
            };
            dataContext.Set<Contact>().Add(contact);
            dataContext.SaveChanges();
            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);

            Opportunity model = new Opportunity();
            model.Id = 1;
            model.StatusId = 1;
            model.StatusCauseId = null;
            model.LockStatusId = 1;
            model.LockCauseId = null;
            model.ModifiedBy = 1;
            model.ModifiedOnUtc = DateTime.UtcNow;
            model.TpId = null;

            mock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(model);
            //Act
            PostModel res = await loanService.PostLoanApplication(3, false, 1, mock.Object);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(1, res.userId);
            Assert.Equal(" ", res.userName);
        }
        [Fact]
        public async Task TestGetByLoanApplicationIdController()
        {
            //Arrange
            Mock<ILoanApplicationService> mock = new Mock<ILoanApplicationService>();

            var loanApplicationController = new LoanApplicationController(mock.Object, null, null, null, null, null, null, null, null);

            GetLoanApplicationModel getLoanApplicationModel = new GetLoanApplicationModel();
            getLoanApplicationModel.loanApplicationId = 1;

            mock.Setup(x => x.GetByLoanApplicationId(It.IsAny<int>()));

            //Act
            IActionResult result = await loanApplicationController.GetByLoanApplicationId(getLoanApplicationModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestSendBorrowerEmailController()
        {
            //Arrange
            Mock<ILoanApplicationService> mock = new Mock<ILoanApplicationService>();
            Mock<IActivityService> mockActivityService = new Mock<IActivityService>();
            Mock<IWorkQueueService> mockWorkQueueService = new Mock<IWorkQueueService>();
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();
            UserProfile user = new UserProfile()
            {
                Employees = new List<Employee>() { new Employee() { EmailTag = "" } }
            };
            mockUserProfileService.Setup(x => x.GetUserProfileEmployeeDetail(It.IsAny<int?>(), It.IsAny<UserProfileService.RelatedEntities?>())).ReturnsAsync(user);
            var loanApplicationController = new LoanApplicationController(mock.Object, null, null, null, mockActivityService.Object, mockWorkQueueService.Object, mockUserProfileService.Object, null, null);

            LoanApplicationModel loanApplicationModel = new LoanApplicationModel();
            loanApplicationModel.BusinessUnitId = 1;
            loanApplicationModel.OpportunityId = 1;
            loanApplicationModel.LoanRequestId = 1;

            SendBorrowerEmailModel sendBorrowerEmailModel = new SendBorrowerEmailModel();
            sendBorrowerEmailModel.loanApplicationId = 1;
            sendBorrowerEmailModel.emailBody = "Email sent";
            sendBorrowerEmailModel.activityForId = (int)ActivityForType.DocumentSyncFailureActivity;
           

            mock.Setup(x => x.GetByLoanApplicationId(It.IsAny<int>())).ReturnsAsync(loanApplicationModel);

            Activity activity = new Activity();
            activity.Id = 1;
            activity.ActivityTypeId = 1;

            mockActivityService.Setup(x => x.GetCustomerActivity(It.IsAny<int?>(), (ActivityForType)sendBorrowerEmailModel.activityForId)).ReturnsAsync(activity);

            var rnd = new Random();

            var random = rnd.Next(100, 1000);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            loanApplicationController.ControllerContext = context;

            var witem = new WorkQueue
            {
                CampaignId = null,
                ActivityId = activity.Id,
                ActivityTypeId = activity.ActivityTypeId,
                CreatedBy = 1,
                CreatedOnUtc = DateTime.UtcNow,
                EntityRefId = 1,
                EntityRefTypeId = Constants.GetEntityType(typeof(Opportunity)),
                EntityTypeId = Constants.GetEntityType(typeof(WorkQueue)),
                IsActive = true,
                IsDeleted = false,
                RandomNo = random,
                LoanRequestId = 1,
                Code = activity.Id.ToString(System.Globalization.CultureInfo.InvariantCulture),
                ScheduleDateUtc = DateTime.UtcNow,
                IsCustom = false
            };

            witem.WorkQueueKeyValues.Add(new WorkQueueKeyValue { KeyName = "###CustomEmailHeader###", Value = "1", TrackingState = TrackingState.Added });
            mockWorkQueueService.Setup(x => x.Insert(It.IsAny<WorkQueue>()));
            mockWorkQueueService.Setup(x => x.SaveChangesAsync());

            var data = new Dictionary<FillKey, string>();
            data.Add(FillKey.CustomEmailHeader, "");
            data.Add(FillKey.CustomEmailFooter, "");
            data.Add(FillKey.EmailBody, sendBorrowerEmailModel.emailBody.Replace(Environment.NewLine, "<br/>"));

            //Act
            IActionResult result = await loanApplicationController.SendBorrowerEmail(sendBorrowerEmailModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task TestSendBorrowerEmailControllerException()
        {
            //Arrange
            Mock<ILoanApplicationService> mock = new Mock<ILoanApplicationService>();
            Mock<IActivityService> mockActivityService = new Mock<IActivityService>();
            Mock<IWorkQueueService> mockWorkQueueService = new Mock<IWorkQueueService>();
            Mock<IUserProfileService> mockUserProfileService = new Mock<IUserProfileService>();
            UserProfile user = new UserProfile()
            {
                Employees = new List<Employee>() { new Employee() { EmailTag = "" } }
            };
            mockUserProfileService.Setup(x => x.GetUserProfileEmployeeDetail(It.IsAny<int?>(), It.IsAny<UserProfileService.RelatedEntities?>())).ReturnsAsync(user);
            var loanApplicationController = new LoanApplicationController(mock.Object, null, null, null, mockActivityService.Object, mockWorkQueueService.Object, mockUserProfileService.Object, null, null);

            LoanApplicationModel loanApplicationModel = new LoanApplicationModel();
            loanApplicationModel.BusinessUnitId = 1;
            loanApplicationModel.OpportunityId = 1;
            loanApplicationModel.LoanRequestId = 1;

            SendBorrowerEmailModel sendBorrowerEmailModel = new SendBorrowerEmailModel();
            sendBorrowerEmailModel.loanApplicationId = 1;
            sendBorrowerEmailModel.emailBody = "Email sent";
            sendBorrowerEmailModel.activityForId = (int)ActivityForType.DocumentSyncFailureActivity;
           

            mock.Setup(x => x.GetByLoanApplicationId(It.IsAny<int>())).ReturnsAsync(loanApplicationModel);

            Activity activity = new Activity();
            activity.Id = 1;
            activity.ActivityTypeId = 1;

            mockActivityService.Setup(x => x.GetCustomerActivity(It.IsAny<int?>(), (ActivityForType)sendBorrowerEmailModel.activityForId)).ReturnsAsync((Activity)null);

            var rnd = new Random();

            var random = rnd.Next(100, 1000);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            loanApplicationController.ControllerContext = context;

            var witem = new WorkQueue
            {
                CampaignId = null,
                ActivityId = activity.Id,
                ActivityTypeId = activity.ActivityTypeId,
                CreatedBy = 1,
                CreatedOnUtc = DateTime.UtcNow,
                EntityRefId = 1,
                EntityRefTypeId = Constants.GetEntityType(typeof(Opportunity)),
                EntityTypeId = Constants.GetEntityType(typeof(WorkQueue)),
                IsActive = true,
                IsDeleted = false,
                RandomNo = random,
                LoanRequestId = 1,
                Code = activity.Id.ToString(System.Globalization.CultureInfo.InvariantCulture),
                ScheduleDateUtc = DateTime.UtcNow,
                IsCustom = false
            };

            witem.WorkQueueKeyValues.Add(new WorkQueueKeyValue { KeyName = "###CustomEmailHeader###", Value = "1", TrackingState = TrackingState.Added });
            mockWorkQueueService.Setup(x => x.Insert(It.IsAny<WorkQueue>()));
            mockWorkQueueService.Setup(x => x.SaveChangesAsync());

            var data = new Dictionary<FillKey, string>();
            data.Add(FillKey.CustomEmailHeader, "");
            data.Add(FillKey.CustomEmailFooter, "");
            data.Add(FillKey.EmailBody, sendBorrowerEmailModel.emailBody.Replace(Environment.NewLine, "<br/>"));

            //Act
            await Assert.ThrowsAsync<RainMakerException>(async () => await loanApplicationController.SendBorrowerEmail(sendBorrowerEmailModel));

        }
        
        
        
        [Fact]
        public async Task TestGetAdminLoanSummaryService()
        {
            //Arrange
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            LoanApplication app = new LoanApplication()
            {
                Id = 14,
                LoanAmount = 1000,
                LoanPurposeId = 3,
                EntityTypeId = 2,
                SubjectPropertyDetailId = 1,
                OpportunityId = 14,
                LoanNumber = "xyz",
                ExpectedClosingDate = DateTime.Now,
                ByteFileName = "xyz"
            };
            dataContext.Set<LoanApplication>().Add(app);

            PropertyInfo propertyInfo = new PropertyInfo()
            {
                Id = 2,
                PropertyTypeId = 2,
                AddressInfoId = 2
            };
            dataContext.Set<PropertyInfo>().Add(propertyInfo);

            PropertyType propertyType = new PropertyType()
            {
                Id = 2,
                Description = "Single Family Detached",
                Name = "Single Family Detached",
                DisplayOrder = 1,
                IsDefault = true,
                IsActive = true,
                EntityTypeId = 2,
                IsDeleted = false,
                TpId = "",
                IsSystem = true
            };
            dataContext.Set<PropertyType>().Add(propertyType);

            AddressInfo addressInfo = new AddressInfo
            {
                Id = 2,
                IsDeleted = false,
                EntityTypeId = 2,
                CityName = "Karachi",
                CountyName = "Pakistan",
                StateName = "Sindh",
                ZipCode = "7550",
                StreetAddress = "abc",
                StateId = 45,
                UnitNo = "1151"

            };
            dataContext.Set<AddressInfo>().Add(addressInfo);

            State state = new State
            {
                Id = 1,
                CountryId = 1,
                Name = "Alaska",
                Abbreviation = "AK",
                DisplayOrder = 1,
                IsActive = true,
                EntityTypeId = 2,
                IsDefault = true,
                IsSystem = true,
                IsLicenseActive = true,
                IsDeleted = false
            };
            dataContext.Set<State>().Add(state);

            LoanPurpose loanPurpose = new LoanPurpose
            {
                Id = 3,
                Name = "Purchase",
                Description = "Purchase a home",
                IsPurchase = true,
                DisplayOrder = 1,
                IsActive = true,
                IsDefault = true,
                IsDeleted = false,
                IsSystem = true,
                EntityTypeId = 2,
                TpId = string.Empty
            };
            dataContext.Set<LoanPurpose>().Add(loanPurpose);

            StatusList statusList = new StatusList
            {
                Id = 1,
                Name = "Floating",
                DisplayOrder = 1,
                IsActive = true,
                EntityTypeId = 2,
                IsDefault = true,
                IsSystem = true,
                IsDeleted = false,
                TypeId = 1,
                CategoryId = 1,
                CanLockOpportunity = false
            };
            dataContext.Set<StatusList>().Add(statusList);

            Borrower borrower = new Borrower
            {
                Id = 1,
                EntityTypeId = 2,
                OwnTypeId = 1
            };
            dataContext.Set<Borrower>().Add(borrower);

            LoanContact loanContact = new LoanContact
            {
                Id = 1,
                EntityTypeId = 2,
                IsDeleted = false,
                FirstName = "Danish",
                LastName = "Faiz"
            };
            dataContext.Set<LoanContact>().Add(loanContact);

            Product product = new Product
            {
                Id = 1,
                Name = "10 Year Fixed",
                DisplayOrder = 1,
                IsActive = true,
                EntityTypeId = 2,
                IsDefault = true,
                IsSystem = true,
                IsDeleted = false,
                AliasName = "10 Year Fixed"
            };
            dataContext.Set<Product>().Add(product);

            Opportunity opportunity = new Opportunity()
            {
                Id = 14,
                IsActive = true,
                EntityTypeId = 2,
                IsDeleted = false,
                NoRuleMatched = false,
                IsAutoAssigned = false,
                IsPickedByOwner = false,
                IsDuplicate = false,
                BusinessUnitId = 1,
                OwnerId = 1
            };
            dataContext.Set<Opportunity>().Add(opportunity);

            OpportunityLockStatusLog opportunityLockStatusLog = new OpportunityLockStatusLog
            {
                Id = 2,
                IsActive = true,
                EntityTypeId = 2,
                CreatedOnUtc = DateTime.Now
            };
            dataContext.Set<OpportunityLockStatusLog>().Add(opportunityLockStatusLog);

            LockStatusList lockStatusList = new LockStatusList
            {
                Id = 1,
                Name = "Locked",
                DisplayOrder = 1,
                IsActive = true,
                EntityTypeId = 2,
                IsDefault = true,
                IsSystem = false,
                IsDeleted = false,
                LockTypeId = 1
            };
            dataContext.Set<LockStatusList>().Add(lockStatusList);

            dataContext.SaveChanges();
            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);

            //Act
            AdminLoanSummary res = await loanService.GetAdminLoanSummary(14);

            // Assert
            Assert.NotNull(res);
            Assert.Equal("Purchase a home", res.LoanPurpose);
            Assert.Equal("xyz", res.LoanNumber);
        }
        [Fact]
        public async Task TestGetByLoanApplicationIdService()
        {
            //Arrange
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            LoanApplication app = new LoanApplication()
            {
                Id = 5,
                LoanAmount = 1000,
                LoanPurposeId = 4,
                EntityTypeId = 3,
                SubjectPropertyDetailId = 1,
                OpportunityId = 5,
                LoanNumber = "xyz",
                ExpectedClosingDate = DateTime.Now
            };
            dataContext.Set<LoanApplication>().Add(app);

            Opportunity opportunity = new Opportunity()
            {
                Id = 5,
                IsActive = true,
                EntityTypeId = 3,
                IsDeleted = false,
                NoRuleMatched = false,
                IsAutoAssigned = false,
                IsPickedByOwner = false,
                IsDuplicate = false,
                BusinessUnitId = 5,
                OwnerId = 2,
                LoanRequestId = 5
            };
            dataContext.Set<Opportunity>().Add(opportunity);

            dataContext.SaveChanges();
            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);

            //Act
            LoanApplicationModel res = await loanService.GetByLoanApplicationId(5);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(5, res.BusinessUnitId);
            Assert.Equal(5, res.LoanRequestId);
            Assert.Equal(5, res.OpportunityId);
        }

        [Fact]
        public void TestGetLoanApplication()
        {
            var mock = new Mock<ILoanApplicationService>();
            mock.Setup(x => x.GetLoanApplicationWithDetails(It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<LoanApplicationService.RelatedEntities?>())).Returns(new List<LoanApplication>() { new LoanApplication() { Id = 1 } });

            var controller = new LoanApplicationController(mock.Object, null, null, null, null, null, null, null, null);

            var result = controller.GetLoanApplication("", 1);
            var res = Assert.IsType<OkObjectResult>(result);
            var loanApplication = Assert.IsType<LoanApplication>(res.Value);
            Assert.Equal(1, loanApplication.Id);
        }
        [Fact]
        public async Task TestGetLoanApplicationForByte()
        {
            var mock = new Mock<ILoanApplicationService>();
            mock.Setup(x => x.GetLoanApplicationWithDetails(It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<LoanApplicationService.RelatedEntities?>())).Returns(new List<LoanApplication>() { new LoanApplication() { Id = 1 } });

            var controller = new LoanApplicationController(mock.Object, null, null, null, null, null, null, null, null);

            var result = controller.GetLoanApplicationForByte("", 1, 1L);
            var res = Assert.IsType<OkObjectResult>(result);
            var loanApplication = Assert.IsType<Rainmaker.Model.ServiceResponseModels.Rainmaker.LoanApplication>(res.Value);
            Assert.Equal(1, loanApplication.Id);
        }
        [Fact]
        public async Task TestUpdateLoanInfo()
        {
            var mock = new Mock<ILoanApplicationService>();
            mock.Setup(x => x.UpdateLoanInfo(It.IsAny<UpdateLoanInfo>())).Verifiable();

            var controller = new LoanApplicationController(mock.Object, null, null, null, null, null, null, null, null);

            var result = await controller.UpdateLoanInfo(new UpdateLoanInfo());
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestSendEmailSupportTeam()
        {
            //Arrange
            Mock<ILoanApplicationService> mock = new Mock<ILoanApplicationService>();
            Mock<IActivityService> mockActivityService = new Mock<IActivityService>();
            Mock<IWorkQueueService> mockWorkQueueService = new Mock<IWorkQueueService>();

            var employeeList = new List<Employee>()
            {
                new Employee()
                {
                    Id=1,
                    EmployeeBusinessUnitEmails = new List<EmployeeBusinessUnitEmail>()
                    {
                        new EmployeeBusinessUnitEmail()
                        {
                            EmailAccount= new EmailAccount()
                            {
                                Email="a@a.com"
                            }
                        }
                    }
                }
            };

            var mockEmployeeService = new Mock<IEmployeeService>();
            mockEmployeeService.Setup(x => x.GetEmployeeEmailByRoleName(It.IsAny<string>())).ReturnsAsync(employeeList);


            var loanApplicationController = new LoanApplicationController(mock.Object, null, null, null, mockActivityService.Object, mockWorkQueueService.Object, null, mockEmployeeService.Object, Mock.Of<ILogger<LoanApplication>>());

            LoanApplicationModel loanApplicationModel = new LoanApplicationModel();
            loanApplicationModel.BusinessUnitId = 1;
            loanApplicationModel.OpportunityId = 1;
            loanApplicationModel.LoanRequestId = 1;

            SendEmailSupportTeam sendBorrowerEmailModel = new SendEmailSupportTeam()
            {
                loanApplicationId = 1,
                EmailBody = "",
                DocumentCategory = "",
                DocumentExension = "",
                DocumentName = "",
                ErrorCode = "",
                ErrorDate = DateTime.Today.ToString(),
                TenantId = 1,
                Url = ""
            };

            mock.Setup(x => x.GetByLoanApplicationId(It.IsAny<int>())).ReturnsAsync(loanApplicationModel);

            Activity activity = new Activity();
            activity.Id = 1;
            activity.ActivityTypeId = 1;

            mockActivityService.Setup(x => x.GetCustomerActivity(It.IsAny<int?>(), It.IsAny<ActivityForType>())).ReturnsAsync(activity);

           

           

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            loanApplicationController.ControllerContext = context;

            mockWorkQueueService.Setup(x => x.Insert(It.IsAny<WorkQueue>()));
            mockWorkQueueService.Setup(x => x.SaveChangesAsync());

            //Act
            IActionResult result = await loanApplicationController.SendEmailSupportTeam(sendBorrowerEmailModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestSendEmailSupportTeamException()
        {
            //Arrange
            Mock<ILoanApplicationService> mock = new Mock<ILoanApplicationService>();
            Mock<IActivityService> mockActivityService = new Mock<IActivityService>();
            Mock<IWorkQueueService> mockWorkQueueService = new Mock<IWorkQueueService>();

            var employeeList = new List<Employee>()
            {
                new Employee()
                {
                    Id=1,
                    EmployeeBusinessUnitEmails = new List<EmployeeBusinessUnitEmail>()
                    {
                        new EmployeeBusinessUnitEmail()
                        {
                            EmailAccount= new EmailAccount()
                            {
                                Email="a@a.com"
                            }
                        }
                    }
                }
            };

            var mockEmployeeService = new Mock<IEmployeeService>();
            mockEmployeeService.Setup(x => x.GetEmployeeEmailByRoleName(It.IsAny<string>())).ReturnsAsync(employeeList);


            var loanApplicationController = new LoanApplicationController(mock.Object, null, null, null, mockActivityService.Object, mockWorkQueueService.Object, null, mockEmployeeService.Object, Mock.Of<ILogger<LoanApplication>>());

            LoanApplicationModel loanApplicationModel = new LoanApplicationModel();
            loanApplicationModel.BusinessUnitId = 1;
            loanApplicationModel.OpportunityId = 1;
            loanApplicationModel.LoanRequestId = 1;

            SendEmailSupportTeam sendBorrowerEmailModel = new SendEmailSupportTeam()
            {
                loanApplicationId = 1,
                EmailBody = "",
                DocumentCategory = "",
                DocumentExension = "",
                DocumentName = "",
                ErrorCode = "",
                ErrorDate = DateTime.Today.ToString(),
                TenantId = 1,
                Url = ""
            };

            mock.Setup(x => x.GetByLoanApplicationId(It.IsAny<int>())).ReturnsAsync(loanApplicationModel);

            Activity activity = new Activity();
            activity.Id = 1;
            activity.ActivityTypeId = 1;

            mockActivityService.Setup(x => x.GetCustomerActivity(It.IsAny<int?>(), It.IsAny<ActivityForType>())).ReturnsAsync((Activity)null);

           

           

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            loanApplicationController.ControllerContext = context;

            mockWorkQueueService.Setup(x => x.Insert(It.IsAny<WorkQueue>()));
            mockWorkQueueService.Setup(x => x.SaveChangesAsync());

            //Act
            await Assert.ThrowsAsync<RainMakerException>(async () => await loanApplicationController.SendEmailSupportTeam(sendBorrowerEmailModel));
        }

        [Fact]
        public async Task TestSendEmailToSupport()
        {
            //Arrange
            Mock<ILoanApplicationService> mock = new Mock<ILoanApplicationService>();
            Mock<IActivityService> mockActivityService = new Mock<IActivityService>();
            Mock<IWorkQueueService> mockWorkQueueService = new Mock<IWorkQueueService>();

            var employeeList = new List<Employee>()
            {
                new Employee()
                {
                    Id=1,
                    EmployeeBusinessUnitEmails = new List<EmployeeBusinessUnitEmail>()
                    {
                        new EmployeeBusinessUnitEmail()
                        {
                            EmailAccount= new EmailAccount()
                            {
                                Email="a@a.com"
                            }
                        }
                    }
                }
            };

            var mockEmployeeService = new Mock<IEmployeeService>();
            mockEmployeeService.Setup(x => x.GetEmployeeEmailByRoleName(It.IsAny<string>())).ReturnsAsync(employeeList);


            var loanApplicationController = new LoanApplicationController(mock.Object, null, null, null, mockActivityService.Object, mockWorkQueueService.Object, null, mockEmployeeService.Object, Mock.Of<ILogger<LoanApplication>>());

            LoanApplicationModel loanApplicationModel = new LoanApplicationModel();
            loanApplicationModel.BusinessUnitId = 1;
            loanApplicationModel.OpportunityId = 1;
            loanApplicationModel.LoanRequestId = 1;

            SupportEmailModel sendBorrowerEmailModel = new SupportEmailModel()
            {
                loanId = "",
                losId = 1,
                milestone = "",
                tenantId = 1,
                url = "test.com"
            };

            mock.Setup(x => x.GetByLoanApplicationId(It.IsAny<int>())).ReturnsAsync(loanApplicationModel);
            mock.Setup(x => x.GetLoanApplicationId(It.IsAny<string>(), It.IsAny<short>())).ReturnsAsync(1);

            Activity activity = new Activity();
            activity.Id = 1;
            activity.ActivityTypeId = 1;

            mockActivityService.Setup(x => x.GetCustomerActivity(It.IsAny<int?>(), It.IsAny<ActivityForType>())).ReturnsAsync(activity);

           

           

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            loanApplicationController.ControllerContext = context;

            mockWorkQueueService.Setup(x => x.Insert(It.IsAny<WorkQueue>()));
            mockWorkQueueService.Setup(x => x.SaveChangesAsync());

            //Act
            IActionResult result = await loanApplicationController.SendEmailToSupport(sendBorrowerEmailModel);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestSendEmailToSupportException()
        {
            //Arrange
            Mock<ILoanApplicationService> mock = new Mock<ILoanApplicationService>();
            Mock<IActivityService> mockActivityService = new Mock<IActivityService>();
            Mock<IWorkQueueService> mockWorkQueueService = new Mock<IWorkQueueService>();

            var employeeList = new List<Employee>()
            {
                new Employee()
                {
                    Id=1,
                    EmployeeBusinessUnitEmails = new List<EmployeeBusinessUnitEmail>()
                    {
                        new EmployeeBusinessUnitEmail()
                        {
                            EmailAccount= new EmailAccount()
                            {
                                Email="a@a.com"
                            }
                        }
                    }
                }
            };

            var mockEmployeeService = new Mock<IEmployeeService>();
            mockEmployeeService.Setup(x => x.GetEmployeeEmailByRoleName(It.IsAny<string>())).ReturnsAsync(employeeList);


            var loanApplicationController = new LoanApplicationController(mock.Object, null, null, null, mockActivityService.Object, mockWorkQueueService.Object, null, mockEmployeeService.Object, Mock.Of<ILogger<LoanApplication>>());

            LoanApplicationModel loanApplicationModel = new LoanApplicationModel();
            loanApplicationModel.BusinessUnitId = 1;
            loanApplicationModel.OpportunityId = 1;
            loanApplicationModel.LoanRequestId = 1;

            SupportEmailModel sendBorrowerEmailModel = new SupportEmailModel()
            {
                loanId = "",
                losId = 1,
                milestone = "",
                tenantId = 1,
                url = "test.com"
            };

            mock.Setup(x => x.GetByLoanApplicationId(It.IsAny<int>())).ReturnsAsync(loanApplicationModel);
            mock.Setup(x => x.GetLoanApplicationId(It.IsAny<string>(), It.IsAny<short>())).ReturnsAsync(1);

            Activity activity = new Activity();
            activity.Id = 1;
            activity.ActivityTypeId = 1;

            mockActivityService.Setup(x => x.GetCustomerActivity(It.IsAny<int?>(), It.IsAny<ActivityForType>())).ReturnsAsync((Activity)null);

            

           

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            loanApplicationController.ControllerContext = context;

            mockWorkQueueService.Setup(x => x.Insert(It.IsAny<WorkQueue>()));
            mockWorkQueueService.Setup(x => x.SaveChangesAsync());

            //Act
            await Assert.ThrowsAsync<RainMakerException>(async () => await loanApplicationController.SendEmailToSupport(sendBorrowerEmailModel));

        }
        [Fact]
        public async Task TestUpdateLoanApplication()
        {
            var mock = new Mock<ILoanApplicationService>();
            mock.Setup(x => x.UpdateLoanApplication(It.IsAny<LoanApplication>())).Verifiable();

            var controller = new LoanApplicationController(mock.Object, null, null, null, null, null, null, null, null);

            var result = await controller.UpdateLoanApplication(new LoanApplication());
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestGetBanner()
        {
            var mock = new Mock<ILoanApplicationService>();
            mock.Setup(x => x.GetBanner(It.IsAny<int>())).ReturnsAsync("");
            var mockCommonService = new Mock<ICommonService>();
            mockCommonService.Setup(x => x.GetSettingValueByKeyAsync<string>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<string>())).ReturnsAsync("");
            var mockFtp = new Mock<IFtpHelper>();
            mockFtp.Setup(x => x.DownloadStream(It.IsAny<string>())).ReturnsAsync(new MemoryStream());
            var controller = new LoanApplicationController(mock.Object, mockCommonService.Object, mockFtp.Object, null, null, null, null, null, null);

            var result = await controller.GetBanner(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestGetFavIcon()
        {
            var mock = new Mock<ILoanApplicationService>();
            mock.Setup(x => x.GetFavIcon(It.IsAny<int>())).ReturnsAsync("");
            var mockCommonService = new Mock<ICommonService>();
            mockCommonService.Setup(x => x.GetSettingValueByKeyAsync<string>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<string>())).ReturnsAsync("");
            var mockFtp = new Mock<IFtpHelper>();
            mockFtp.Setup(x => x.DownloadStream(It.IsAny<string>())).ReturnsAsync(new MemoryStream());
            var controller = new LoanApplicationController(mock.Object, mockCommonService.Object, mockFtp.Object, null, null, null, null, null, null);

            var result = await controller.GetFavIcon(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestBorrowerAddOrUpdate()
        {
            RainmakerBorrower model = new RainmakerBorrower()
            {
                OldFirstName = "John",
                OldEmailAddress = "test@test.com",
                IsAddOrUpdate = true,
                GenderIds = new List<int>() { },
                EthnicityInfo = new List<EthnicInfoItem>() { },
                RaceInfo = new List<RaceInfoItem>() { }
            };
            var loanApplication = new LoanApplication()
            {
                Id = 1
            };
            
            var mockLaonApplicationService = new Mock<ILoanApplicationService>();
            mockLaonApplicationService.Setup(x => x.GetLoanApplicationWithDetails(It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<LoanApplicationService.RelatedEntities?>())).Returns(new List<LoanApplication>() { loanApplication });
            mockLaonApplicationService.Setup(x => x.Update(It.IsAny<LoanApplication>())).Verifiable();
            mockLaonApplicationService.Setup(x => x.SaveChangesAsync()).Verifiable();

            var mockBorrowerService = new Mock<IBorrowerService>();
            mockBorrowerService.Setup(x => x.GetBorrowerWithDetails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<BorrowerService.RelatedEntities?>())).Returns(new List<Borrower>() { });

            var controller = new BorrowerController(mockBorrowerService.Object, mockLaonApplicationService.Object);
            var result = await controller.AddOrUpdate(model, true);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestBorrowerAddOrUpdateFalse()
        {
            RainmakerBorrower model = new RainmakerBorrower()
            {
                OldFirstName = "John",
                OldEmailAddress = "test@test.com",
                IsAddOrUpdate = true,
                GenderIds = new List<int>() { },
                EthnicityInfo = new List<EthnicInfoItem>() { },
                RaceInfo = new List<RaceInfoItem>() { }
            };
            
            var mockLaonApplicationService = new Mock<ILoanApplicationService>();
            mockLaonApplicationService.Setup(x => x.GetLoanApplicationWithDetails(It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<LoanApplicationService.RelatedEntities?>())).Returns(new List<LoanApplication>() { });

            var mockBorrowerService = new Mock<IBorrowerService>();
            mockBorrowerService.Setup(x => x.GetBorrowerWithDetails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<BorrowerService.RelatedEntities?>())).Returns(new List<Borrower>() { });

            var controller = new BorrowerController(mockBorrowerService.Object, mockLaonApplicationService.Object);
            var result = await controller.AddOrUpdate(model, true);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task TestBorrowerAdd()
        {
            RainmakerBorrower model = new RainmakerBorrower()
            {
                OldFirstName = "John",
                OldEmailAddress = "test@test.com",
                IsAddOrUpdate = true,
                GenderIds = new List<int>() { },
                EthnicityInfo = new List<EthnicInfoItem>() { },
                RaceInfo = new List<RaceInfoItem>() { }
            };
            
            var borrower = new Borrower()
            {
                Id = 1,
                LoanApplicationId = 1,
                LoanContact = new LoanContact()
            };
            var mockLaonApplicationService = new Mock<ILoanApplicationService>();
            mockLaonApplicationService.Setup(x => x.GetLoanApplicationWithDetails(It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<LoanApplicationService.RelatedEntities?>())).Returns(new List<LoanApplication>() { });

            var mockBorrowerService = new Mock<IBorrowerService>();
            mockBorrowerService.Setup(x => x.GetBorrowerWithDetails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<BorrowerService.RelatedEntities?>())).Returns(new List<Borrower>() { borrower });
            mockBorrowerService.Setup(x => x.Update(It.IsAny<Borrower>())).Verifiable();
            mockBorrowerService.Setup(x => x.SaveChangesAsync()).Verifiable();

            var controller = new BorrowerController(mockBorrowerService.Object, mockLaonApplicationService.Object);
            var result = await controller.AddOrUpdate(model, true);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task TestGetLoanApplicationId()
        {
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();
            LosLoanApplicationBinder binder = new LosLoanApplicationBinder()
            {
                Id = 123,
                LosLoanApplicationId = "1",
                LosId = 1,
                LoanApplicationId = 1,
                LosLoanAplicationNumber = "1"
            };
            dataContext.Set<LosLoanApplicationBinder>().Add(binder);
            await dataContext.SaveChangesAsync();

            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);

            //Act
            var res = await loanService.GetLoanApplicationId("1", 1);

            Assert.Equal(1, res);
        }

        [Fact]
        public async Task TestGetLoanApplicationIdNull()
        {
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();
            LosLoanApplicationBinder binder = new LosLoanApplicationBinder()
            {
                Id = 124,
                LosLoanApplicationId = "2",
                LosId = 1,
                LoanApplicationId = 1,
                LosLoanAplicationNumber = "2"
            };
            dataContext.Set<LosLoanApplicationBinder>().Add(binder);
            await dataContext.SaveChangesAsync();

            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);

            //Act
            var res = await loanService.GetLoanApplicationId("1", 1);

            Assert.Equal(-1, res);
        }
        [Fact]
        public async Task TestGetMilestoneId()
        {
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            LoanApplication loanApplication = new LoanApplication()
            {
                Id = 1234,
                MilestoneId = 1
            };
            dataContext.Set<LoanApplication>().Add(loanApplication);
            await dataContext.SaveChangesAsync();

            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);

            //Act
            var res = await loanService.GetMilestoneId(1234);

            Assert.Equal(1, res);
        }

        [Fact]
        public async Task TestSetBothLosAndMilestoneId()
        {
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            LoanApplication loanApplication = new LoanApplication()
            {
                Id = 1235,
                MilestoneId = 1,
                LosMilestoneId = 1
            };
            dataContext.Set<LoanApplication>().Add(loanApplication);
            await dataContext.SaveChangesAsync();

            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);

            //Act
            await loanService.SetBothLosAndMilestoneId(1235, 2, 2);

            var res = (await dataContext.Set<LoanApplication>().FirstAsync(x => x.Id == 1235)).MilestoneId;
            Assert.Equal(2, res);
        }

        [Fact]
        public async Task TestGetBothLosAndMilestoneId()
        {
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            LoanApplication loanApplication = new LoanApplication()
            {
                Id = 1236,
                MilestoneId = 1,
                LosMilestoneId = 1
            };
            dataContext.Set<LoanApplication>().Add(loanApplication);
            await dataContext.SaveChangesAsync();

            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);

            //Act
            var res = await loanService.GetBothLosAndMilestoneId(1236);

            Assert.Equal(1, res.milestoneId);
        }

        [Fact]
        public async Task TestSetMilestoneId()
        {
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            LoanApplication loanApplication = new LoanApplication()
            {
                Id = 1237,
                MilestoneId = 1,
                LosMilestoneId = 1
            };
            dataContext.Set<LoanApplication>().Add(loanApplication);
            await dataContext.SaveChangesAsync();

            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);

            //Act
            await loanService.SetMilestoneId(1237, 2);

            var res = (await dataContext.Set<LoanApplication>().FirstAsync(x => x.Id == 1237)).MilestoneId;
            Assert.Equal(2, res);
        }
        [Fact]
        public async Task TestServiceUpdateLoanInfo()
        {
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);

            //Act
            await loanService.UpdateLoanInfo(new UpdateLoanInfo() { loanApplicationId = 1 });

            var res = (await dataContext.Set<LoanDocumentPipeLine>().FirstAsync(x => x.LoanApplicationId == 1)).LoanApplicationId;
            Assert.Equal(1, res);
        }

        [Fact]
        public async Task TestServiceUpdateLoanInfoUpdate()
        {
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            LoanDocumentPipeLine loanDocumentPipeLine = new LoanDocumentPipeLine()
            {
                DocumentCompleted = 1,
                LoanApplicationId = 2
            };
            dataContext.Set<LoanDocumentPipeLine>().Add(loanDocumentPipeLine);
            await dataContext.SaveChangesAsync();
            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);

            //Act
            await loanService.UpdateLoanInfo(new UpdateLoanInfo() { loanApplicationId = 2, completedDocuments = 2 });

            var res = (await dataContext.Set<LoanDocumentPipeLine>().FirstAsync(x => x.LoanApplicationId == 2)).DocumentCompleted;
            Assert.Equal(2, res);
        }
        [Fact]
        public async Task TestServiceGetBanner()
        {
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            LoanApplication loanApplication = new LoanApplication()
            {
                Id = 1345,
                BusinessUnitId = 1345
            };
            dataContext.Set<LoanApplication>().Add(loanApplication);

            BusinessUnit businessUnit = new BusinessUnit()
            {
                Id = 1345,
                Banner = "Test"
            };
            dataContext.Set<BusinessUnit>().Add(businessUnit);

            await dataContext.SaveChangesAsync();

            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);

            //Act
            var res = await loanService.GetBanner(1345);

            Assert.Equal("Test", res);
        }

        [Fact]
        public async Task TestServiceGetFavIcon()
        {
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            LoanApplication loanApplication = new LoanApplication()
            {
                Id = 1346,
                BusinessUnitId = 1346
            };
            dataContext.Set<LoanApplication>().Add(loanApplication);

            BusinessUnit businessUnit = new BusinessUnit()
            {
                Id = 1346,
                FavIcon = "Test"
            };
            dataContext.Set<BusinessUnit>().Add(businessUnit);

            await dataContext.SaveChangesAsync();

            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);

            //Act
            var res = await loanService.GetFavIcon(1346);

            Assert.Equal("Test", res);
        }

        [Fact]
        public async Task TestUpdateLoanApplicationService()
        {
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            LoanApplication loanApplication = new LoanApplication()
            {
                Id = 1348,
                OpportunityId = 1348
            };
            dataContext.Set<LoanApplication>().Add(loanApplication);

            Opportunity opportunity = new Opportunity()
            {
                Id = 1348
            };
            dataContext.Set<Opportunity>().Add(opportunity);

            await dataContext.SaveChangesAsync();

            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);

            //Act
            await loanService.UpdateLoanApplication(new LoanApplication() { Id = 1348, ByteFileName = "Test" });

            var res = (await dataContext.Set<LoanApplication>().FirstAsync(x => x.Id == 1348)).ByteFileName;
            Assert.Equal("Test", res);
        }
        [Fact]
        public async Task TestServiceGetLoanApplicationWithDetails()
        {
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            LoanApplication loanApplication = new LoanApplication()
            {
                Id = 1448,
                OpportunityId = 1448,
                EncompassNumber = ""
            };
            dataContext.Set<LoanApplication>().Add(loanApplication);

            Opportunity opportunity = new Opportunity()
            {
                Id = 1448
            };
            dataContext.Set<Opportunity>().Add(opportunity);

            await dataContext.SaveChangesAsync();

            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);

            //Act
            var res = loanService.GetLoanApplicationWithDetails(1448, "", LoanApplicationService.RelatedEntities.Opportunity);

            Assert.Equal(1448, res[0].Id);
        }

        [Fact]
        public async Task TestServiceGetLoanApplicationWithDetailsCoverage()
        {
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            LoanApplication loanApplication = new LoanApplication()
            {
                Id = 1447,
                OpportunityId = 1447,
                EncompassNumber = ""
            };
            dataContext.Set<LoanApplication>().Add(loanApplication);

            Opportunity opportunity = new Opportunity()
            {
                Id = 1447
            };
            dataContext.Set<Opportunity>().Add(opportunity);

            await dataContext.SaveChangesAsync();

            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);

            //Act
            var res = loanService.GetLoanApplicationWithDetails(1447, "",
                LoanApplicationService.RelatedEntities.Borrower |
                LoanApplicationService.RelatedEntities.PropertyInfo |
                LoanApplicationService.RelatedEntities.Borrower_LoanContact |
                LoanApplicationService.RelatedEntities.Borrower_EmploymentInfoes |
                LoanApplicationService.RelatedEntities.Borrower_EmploymentInfoes_OtherEmploymentIncomes |
                LoanApplicationService.RelatedEntities.Borrower_OtherIncomes_IncomeType |
                LoanApplicationService.RelatedEntities.Borrower_BorrowerResidences |
                LoanApplicationService.RelatedEntities.LoanGoal |
                LoanApplicationService.RelatedEntities.Borrower_BorrowerResidences_OwnershipType |
                LoanApplicationService.RelatedEntities.Borrower_BorrowerResidences_LoanAddress |
                LoanApplicationService.RelatedEntities.Borrower_FamilyRelationType |
                LoanApplicationService.RelatedEntities.PropertyInfo_PropertyType |
                LoanApplicationService.RelatedEntities.PropertyInfo_PropertyUsage |
                LoanApplicationService.RelatedEntities.Borrower_PropertyInfo |
                LoanApplicationService.RelatedEntities.Borrower_PropertyInfo_AddressInfo |
                LoanApplicationService.RelatedEntities.Borrower_BorrowerAccount |
                LoanApplicationService.RelatedEntities.Borrower_BorrowerAccount_AccountType |
                LoanApplicationService.RelatedEntities.Borrower_EmploymentInfoes_OtherEmploymentIncomes_IncomeType |
                LoanApplicationService.RelatedEntities.Borrower_EmploymentInfoes_AddressInfo |
                LoanApplicationService.RelatedEntities.Borrower_BorrowerQuestionResponses |
                LoanApplicationService.RelatedEntities.Borrower_BorrowerQuestionResponses_Question |
                LoanApplicationService.RelatedEntities.Borrower_BorrowerQuestionResponses_QuestionResponse |
                LoanApplicationService.RelatedEntities.Borrower_LoanContact_Gender |
                LoanApplicationService.RelatedEntities.PropertyInfo_PropertyTaxEscrows |
                LoanApplicationService.RelatedEntities.Borrower_LoanContact_Ethnicity |
                LoanApplicationService.RelatedEntities.Borrower_LoanContact_Race |
                LoanApplicationService.RelatedEntities.Borrower_Bankruptcies |
                LoanApplicationService.RelatedEntities.Borrower_LoanContact_ResidencyType |
                LoanApplicationService.RelatedEntities.Borrower_LoanContact_ResidencyState |
                LoanApplicationService.RelatedEntities.Borrower_BorrowerAssets |
                LoanApplicationService.RelatedEntities.Borrower_EmploymentInfoes_JobType |
                LoanApplicationService.RelatedEntities.PropertyInfo_AddressInfo |
                LoanApplicationService.RelatedEntities.PropertyInfo_MortgageOnProperties |
                LoanApplicationService.RelatedEntities.Borrower_OwnType |
                LoanApplicationService.RelatedEntities.LoanPurpose |
                LoanApplicationService.RelatedEntities.Borrower_Consent_ConsentLog |
                LoanApplicationService.RelatedEntities.Borrower_Liability |
                LoanApplicationService.RelatedEntities.Borrower_SupportPayments |
                LoanApplicationService.RelatedEntities.Borrower_OwnerShipInterests |
                LoanApplicationService.RelatedEntities.Borrower_VaDetails |
                LoanApplicationService.RelatedEntities.BusinessUnit |
                LoanApplicationService.RelatedEntities.Opportunity |
                LoanApplicationService.RelatedEntities.Opportunity_UserProfile |
                LoanApplicationService.RelatedEntities.Opportunity_LoanRequest |
                LoanApplicationService.RelatedEntities.LosSyncLog |
                LoanApplicationService.RelatedEntities.Opportunity_Employee_UserProfile |
                LoanApplicationService.RelatedEntities.Opportunity_Employee_Contact |
                LoanApplicationService.RelatedEntities.Opportunity_Employee_CompanyPhoneInfo |
                LoanApplicationService.RelatedEntities.Opportunity_Employee_EmailAccount |
                LoanApplicationService.RelatedEntities.BusinessUnit_LeadSource |
                LoanApplicationService.RelatedEntities.LoanApplication_Status |
                LoanApplicationService.RelatedEntities.Opportunity_Branch |
                LoanApplicationService.RelatedEntities.Opportunity_Employee_Contact_ContactPhoneInfoes
                );

            Assert.Empty(res);
        }

        [Fact]
        public async Task TestServiceGetLoanRequestWithDetails()
        {
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            LoanApplication loanApplication = new LoanApplication()
            {
                Id = 1548,
                OpportunityId = 1548
            };
            dataContext.Set<LoanApplication>().Add(loanApplication);

            LoanRequest loanRequest = new LoanRequest()
            {
                Id = 1548,
                BusinessUnitId=1548
            };
            dataContext.Set<LoanRequest>().Add(loanRequest);

            Opportunity opportunity = new Opportunity()
            {
                Id = 1548,
                LoanRequestId=1548,
            };
            dataContext.Set<Opportunity>().Add(opportunity);

            BusinessUnit businessUnit = new BusinessUnit
            {
                Id = 1548
            };
            dataContext.Set<BusinessUnit>().Add(businessUnit);

            await dataContext.SaveChangesAsync();

            ILoanRequestService loanService = new LoanRequestService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, null);

            //Act
            var res = loanService.GetLoanRequestWithDetails(1548,1548,1548,LoanRequestService.RelatedEntities.BusinessUnit);

            Assert.Equal(1548, res[0].Id);
        }
    }
}
