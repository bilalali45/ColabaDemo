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

            var loanApplicationController = new LoanApplicationController(mock.Object, null, null);

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
        public async Task TestGetLOInfoController()
        {
            //Arrange
            Mock<ILoanApplicationService> mock = new Mock<ILoanApplicationService>();

            var loanApplicationController = new LoanApplicationController(mock.Object, null, null);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(m => m.User.FindFirst("UserProfileId")).Returns(new Claim("UserProfileId", "1"));

            var context = new ControllerContext(new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor()));

            loanApplicationController.ControllerContext = context;

            LoanOfficer obj = new LoanOfficer() { FirstName = "Smith" };

            mock.Setup(x => x.GetLOInfo(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(obj);

            //Act
            IActionResult result = await loanApplicationController.GetLOInfo(1, 1);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as LoanOfficer;
            Assert.NotNull(content);
            Assert.Equal("Smith", content.FirstName);
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
            Mock<ICommonService> mockcommonservice = new Mock<ICommonService>();
            Mock<IFtpHelper> mockftpservice = new Mock<IFtpHelper>();
            int? businessUnitId = 1;

            mockcommonservice.Setup(x => x.GetSettingFreshValueByKeyAsync<string>(SystemSettingKeys.FtpEmployeePhotoFolder, businessUnitId, default)).ReturnsAsync(string.Empty);
            mockftpservice.Setup(x => x.DownloadStream(It.IsAny<string>())).ReturnsAsync(new MemoryStream());

            LoanApplicationController controller = new LoanApplicationController(null, mockcommonservice.Object, mockftpservice.Object);

            //Act
            string result = await controller.GetPhoto(SystemSettingKeys.FtpEmployeePhotoFolder, (int)businessUnitId);

            //Assert
            Assert.NotNull(result);


        }

        [Fact]
        public async Task TestGetPhotoNullController()
        {
            //Arrange
            Mock<ICommonService> mockcommonservice = new Mock<ICommonService>();
            Mock<IFtpHelper> mockftpservice = new Mock<IFtpHelper>();
            int? businessUnitId = 1;

            mockcommonservice.Setup(x => x.GetSettingFreshValueByKeyAsync<string>(SystemSettingKeys.FtpEmployeePhotoFolder, businessUnitId, default)).ReturnsAsync(string.Empty);
            //  mockftpservice.Setup(x => x.DownloadStream(It.IsAny<string>())).ReturnsAsync(new MemoryStream());
            mockftpservice.Setup(x => x.DownloadStream(It.IsAny<string>())).ReturnsAsync(It.IsAny<Stream>());

            LoanApplicationController controller = new LoanApplicationController(null, mockcommonservice.Object, mockftpservice.Object);

            //Act
            string result = await controller.GetPhoto(It.IsAny<string>()
                //ystemSettingKeys.FtpEmployeePhotoFolder
                , It.IsAny<int>());

            //Assert
            Assert.NotNull(result);

            Assert.IsType<string>(result);

            Assert.Equal("/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAAA8AAD/4QMvaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0MiA3OS4xNjA5MjQsIDIwMTcvMDcvMTMtMDE6MDY6MzkgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE4IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpCOTNCNjI5QkI0NjUxMUVBQjA4MUI3NzVGMjkyRDYwOSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpCOTNCNjI5Q0I0NjUxMUVBQjA4MUI3NzVGMjkyRDYwOSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkI5M0I2Mjk5QjQ2NTExRUFCMDgxQjc3NUYyOTJENjA5IiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkI5M0I2MjlBQjQ2NTExRUFCMDgxQjc3NUYyOTJENjA5Ii8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/+4ADkFkb2JlAGTAAAAAAf/bAIQABgQEBAUEBgUFBgkGBQYJCwgGBggLDAoKCwoKDBAMDAwMDAwQDA4PEA8ODBMTFBQTExwbGxscHx8fHx8fHx8fHwEHBwcNDA0YEBAYGhURFRofHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8f/8AAEQgARgBGAwERAAIRAQMRAf/EAG0AAQEAAwEBAAAAAAAAAAAAAAAGBAUHAwgBAQEAAAAAAAAAAAAAAAAAAAABEAABAwMBBQYFBQAAAAAAAAABAAIDEQQFEiExQYEGYXEyktITUaEiUxeRQmIUFREBAAAAAAAAAAAAAAAAAAAAAP/aAAwDAQACEQMRAD8A+qUBAQEBAQEBAQEBAQEBAQEBAQEBAQT/AFH1dBin/wBWGMXF6RqcwmjIwdxeRxPABBORdf5tsuqSOCRnGMNc3Z2Oq5BZYPOWmXtTNBVkjDpmhd4mO5bweBQbFAQEBAQEHIcpLJLlL2SSpe6eTVXseWgcgKKKxkFH0DLI3OvjbXRJA73Bw+lzdJ5VP6ojoioICAgIBIAqdyDneexMN/fXN9g5G3rNVbu3iNXsfxe0fva7+PH5Bo48fkZZPajtJ3SbtAjfXnUbOairLo+xscZdyw3VxH/szMFbYOBMcY26KjYXne4Ds71UVyAgICAgiOus9KZjiLd2mNoBvHDe4uFRH3U2u+O74oJCKSSKRskL3RSM8L2EtcO4ihUVmyZ7OSR+2+/nLN1A/SfM2h+aDBFWkFpLXNOprgaEOG2oI49qDpPSGdflLBzLg1vLYhsrt2tp8L+dKHtVRvkBAQEE7ddDYq5uprmWe4MkzzI+jmUq48Po3IPL8fYb71z5o/Qgfj7DfeufNH6ED8fYb71z5o/Qgz8N0vYYm5fcW0sznSM9tzZHNLaVBrQNbt2INwgICAgICAgICAgICAgICAgICAgIP//Z", result);
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
            Assert.Null( res.NMLS);
            Assert.Equal("030012345678", res.Phone);
            Assert.Equal("", res.Photo);
            Assert.Equal("https://entityframeworkcore.com//lo/Shehroz", res.WebUrl);
        }


    }
}
