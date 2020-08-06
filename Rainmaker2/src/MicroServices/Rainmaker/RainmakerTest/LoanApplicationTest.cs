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

            var loanApplicationController = new LoanApplicationController(mock.Object, null, null,null, null, null);

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
                Id = 1,
                LoanAmount = 1000,
                LoanPurposeId = 1,
                EntityTypeId = 1,
                SubjectPropertyDetailId = 1,
                OpportunityId = 2
            };
            dataContext.Set<LoanApplication>().Add(app);

            Opportunity opportunity = new Opportunity
            {
                Id = 2,
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
                Id = 2,
                OpportunityId = 2,
                CustomerId = 2,
                OwnTypeId = 1
            };
            dataContext.Set<OpportunityLeadBinder>().Add(opportunityLeadBinder);

            Customer customer = new Customer()
            {
                Id = 2,
                UserId = 1,
                EntityTypeId = 1,
                DisplayOrder = 1,
                IsActive = true,
                IsSystem = true,
                IsDeleted = false
            };
            dataContext.Set<Customer>().Add(customer);

            PropertyInfo propertyInfo = new PropertyInfo()
            {
                Id = 1,
                PropertyTypeId = 1,
                AddressInfoId = 1
            };
            dataContext.Set<PropertyInfo>().Add(propertyInfo);

            PropertyType propertyType = new PropertyType()
            {
                Id = 1,
                Description = "Test",
                Name = "",
                DisplayOrder = 1,
                IsDefault = true,
                IsActive = true,
                EntityTypeId = 1,
                IsDeleted = false,
                TpId = "",
                IsSystem = true
            };
            dataContext.Set<PropertyType>().Add(propertyType);

            AddressInfo addressInfo = new AddressInfo
            {
                Id = 1,
                IsDeleted = false,
                EntityTypeId = 1,
                CityName = "Karachi",
                CountyName = "Pakistan",
                StateName = "Sindh",
                ZipCode = "7550",
                StreetAddress = "abc"

            };
            dataContext.Set<AddressInfo>().Add(addressInfo);

            LoanPurpose loanPurpose = new LoanPurpose
            {
                Id = 1,
                Name = "abc",
                Description = "abc",
                IsPurchase = true,
                DisplayOrder = 1,
                IsActive = true,
                IsDefault = true,
                IsDeleted = false,
                IsSystem = true,
                EntityTypeId = 1,
                TpId = string.Empty

            };
            dataContext.Set<LoanPurpose>().Add(loanPurpose);
            dataContext.SaveChanges();
            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            LoanSummary res = await loanService.GetLoanSummary(1, 1);

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

            var loanApplicationController = new LoanApplicationController(mock.Object, null, null, null, null, null);

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

            var loanApplicationController = new LoanApplicationController(mock.Object, null, null, null, null, null);

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
           // Assert.Null(content.FirstName);
        }
        [Fact]
        public async Task TestGetLOInfoIsnullObjController()
        {
            //Arrange
            Mock<ILoanApplicationService> mock = new Mock<ILoanApplicationService>();

            var loanApplicationController = new LoanApplicationController(mock.Object, null, null, null, null, null);

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
            // Assert.Null(content.FirstName);
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
                Id = 2,
                LoanAmount = 1000,
                LoanPurposeId = 1,
                EntityTypeId = 1,
                OpportunityId = 1,
                BusinessUnitId = 1,
            };
            dataContext.Set<LoanApplication>().Add(app);

            Opportunity opportunity = new Opportunity
            {
                Id = 1,
                IsActive = true,
                EntityTypeId = 1,
                IsDeleted = false,
                NoRuleMatched = false,
                IsAutoAssigned = true,
                IsPickedByOwner = true,
                IsDuplicate = false
                ,
                BusinessUnitId = 1
                ,
                OwnerId = 1
            };
            dataContext.Set<Opportunity>().Add(opportunity);

            OpportunityLeadBinder opportunityLeadBinder = new OpportunityLeadBinder
            {
                Id = 1,
                OpportunityId = 1,
                CustomerId = 1,
                OwnTypeId = 1
            };
            dataContext.Set<OpportunityLeadBinder>().Add(opportunityLeadBinder);

            Customer customer = new Customer()
            {
                Id = 1,
                UserId = 1,
                EntityTypeId = 1,
                DisplayOrder = 1,
                IsActive = true,
                IsSystem = true,
                IsDeleted = false
            };
            dataContext.Set<Customer>().Add(customer);

            Employee employee = new Employee
            {
                Id = 1,
                IsActive = true,
                IsSystem = true,
                EntityTypeId = 1,
                IsDeleted = false,
                NmlsNo = "030012345",
                Photo = "abc.png",
                CmsName = "Shehroz"
                ,
                ContactId = 1

            };
            dataContext.Set<Employee>().Add(employee);


            EmployeePhoneBinder employeePhoneBinder = new EmployeePhoneBinder

            {
                Id = 1,
                EmployeeId = 1,
                CompanyPhoneInfoId = 1,
                TypeId = 3


            };
            dataContext.Set<EmployeePhoneBinder>().Add(employeePhoneBinder);
            CompanyPhoneInfo companyPhoneInfo = new CompanyPhoneInfo
            {
                Id = 1,
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
                Id = 1,
                EmployeeId = 1,
                EmailAccountId = 1,
                TypeId = 1,
                BusinessUnitId = 1

            };

            dataContext.Set<EmployeeBusinessUnitEmail>().Add(employeeBusinessUnitEmail);

            BusinessUnit businessUnit = new BusinessUnit
            {
                Id = 1,
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
                Id = 1,
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
                Id = 1,
                EntityTypeId = 1,
                IsDeleted = false,
                FirstName = "Shehroz",
                LastName = "Riaz"
            };
            dataContext.Set<Contact>().Add(contact);

            dataContext.SaveChanges();

            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            LoanOfficer res = await loanService.GetLOInfo(2, 1, 1);

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

            LoanApplicationController controller = new LoanApplicationController(mock.Object, mockcommonservice.Object, mockftpservice.Object,null, null, null);
       

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
            //  mockftpservice.Setup(x => x.DownloadStream(It.IsAny<string>())).ReturnsAsync(new MemoryStream());
            mockftpservice.Setup(x => x.DownloadStream(It.IsAny<string>())).Throws(new Exception(""));

            LoanApplicationModel loanApplicationModel = new LoanApplicationModel();
            loanApplicationModel.BusinessUnitId = 1;

            mock.Setup(x => x.GetByLoanApplicationId(It.IsAny<int>())).ReturnsAsync(loanApplicationModel);

            LoanApplicationController controller = new LoanApplicationController(mock.Object, mockcommonservice.Object, mockftpservice.Object, null, null, null);

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
                Id = 2,
                Name = "Shehroz",
                DisplayOrder = 1,
                IsActive = true,
                IsDefault = true,
                IsSystem = true,
                IsDeleted = false,
                EmailAccountId = 2,
                Logo = "",
                WebUrl = "https://entityframeworkcore.com//lo/Shehroz"



            };
            dataContext.Set<BusinessUnit>().Add(businessUnit);

            EmailAccount emailAccount = new EmailAccount
            {
                Id = 2,
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
                Id = 2,
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
                BusinessUnitId = 2,
                CompanyPhoneInfoId = 2,
                TypeId = 3

            };
            dataContext.Set<BusinessUnitPhone>().Add(businessUnitPhone);
            dataContext.SaveChanges();

            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            LoanOfficer res = await loanService.GetDbaInfo(2);

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
            LoanApplicationController loanApplicationController = new LoanApplicationController(mock.Object, null, null, null, null, null);

            loanApplicationController.ControllerContext = context;
            PostLoanApplicationModel model = new PostLoanApplicationModel()
            {
                loanApplicationId = 1, isDraft = true
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
            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            PostModel res = await loanService.PostLoanApplication(4, true,1, mock.Object);
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
            ILoanApplicationService loanService = new LoanApplicationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

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

            var loanApplicationController = new LoanApplicationController(mock.Object, null, null, null, null, null);

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
            var loanApplicationController = new LoanApplicationController(mock.Object, null, null, null, mockActivityService.Object, mockWorkQueueService.Object);

            LoanApplicationModel loanApplicationModel = new LoanApplicationModel();
            loanApplicationModel.BusinessUnitId = 1;
            loanApplicationModel.OpportunityId = 1;
            loanApplicationModel.LoanRequestId = 1;

            SendBorrowerEmailModel sendBorrowerEmailModel = new SendBorrowerEmailModel();
            sendBorrowerEmailModel.loanApplicationId = 1;
            sendBorrowerEmailModel.emailBody = "Email sent";
            sendBorrowerEmailModel.activityForId = (int)ActivityForType.LoanApplicationDocumentRejectActivity;
            var activityEnumType = (ActivityForType)sendBorrowerEmailModel.activityForId;

            mock.Setup(x => x.GetByLoanApplicationId(It.IsAny<int>())).ReturnsAsync(loanApplicationModel);

            Activity activity = new Activity();
            activity.Id = 1;
            activity.ActivityTypeId = 1;

            mockActivityService.Setup(x => x.GetCustomerActivity(It.IsAny<int?>(), (ActivityForType)sendBorrowerEmailModel.activityForId)).ReturnsAsync(activity);

            var rnd = new Random();

            var random = rnd.Next(100, 1000);

            var witem = new WorkQueue
            {
                CampaignId = null,
                ActivityId = activity.Id,
                ActivityTypeId = activity.ActivityTypeId,
                CreatedBy = 1,//todo: employee userid
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
        public async Task TestSendBorrowerEmailControllerActivityIdNull()
        {
            //Arrange
            Mock<ILoanApplicationService> mock = new Mock<ILoanApplicationService>();
            Mock<IActivityService> mockActivityService = new Mock<IActivityService>();
            Mock<IWorkQueueService> mockWorkQueueService = new Mock<IWorkQueueService>();
            var loanApplicationController = new LoanApplicationController(mock.Object, null, null, null, mockActivityService.Object, mockWorkQueueService.Object);

            LoanApplicationModel loanApplicationModel = new LoanApplicationModel();
            loanApplicationModel.BusinessUnitId = 1;
            loanApplicationModel.OpportunityId = 1;
            loanApplicationModel.LoanRequestId = 1;

            SendBorrowerEmailModel sendBorrowerEmailModel = new SendBorrowerEmailModel();
            sendBorrowerEmailModel.loanApplicationId = 1;
            sendBorrowerEmailModel.emailBody = "Email sent";
            sendBorrowerEmailModel.activityForId = (int)ActivityForType.LoanApplicationDocumentRejectActivity;
            var activityEnumType = (ActivityForType)sendBorrowerEmailModel.activityForId;

            mock.Setup(x => x.GetByLoanApplicationId(It.IsAny<int>())).ReturnsAsync(loanApplicationModel);

            Activity activity = new Activity();
            activity = null;

            mockActivityService.Setup(x => x.GetCustomerActivity(It.IsAny<int?>(), (ActivityForType)sendBorrowerEmailModel.activityForId)).ReturnsAsync(activity);
         
            mockWorkQueueService.Setup(x => x.Insert(It.IsAny<WorkQueue>()));
            mockWorkQueueService.Setup(x => x.SaveChangesAsync());

            var data = new Dictionary<FillKey, string>();
            data.Add(FillKey.CustomEmailHeader, "");
            data.Add(FillKey.CustomEmailFooter, "");
            data.Add(FillKey.EmailBody, sendBorrowerEmailModel.emailBody.Replace(Environment.NewLine, "<br/>"));

            await Assert.ThrowsAsync<Exception>(async () => { await loanApplicationController.SendBorrowerEmail(sendBorrowerEmailModel); });
        }
    }
}
