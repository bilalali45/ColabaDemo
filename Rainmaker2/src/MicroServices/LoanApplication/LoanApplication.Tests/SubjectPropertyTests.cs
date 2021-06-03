using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using LoanApplication.API.Controllers;
using LoanApplication.Model;
using LoanApplication.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TenantConfig.Common.DistributedCache;
using Xunit;

namespace LoanApplication.Tests
{
   public class SubjectPropertyTests
    {
        [Fact]
        public async Task TestAddOrUpdateSubjectPropertyState()
        {
            // Arrange
            Mock<ILogger<SubjectPropertyController>> loggerServiceMock = new Mock<ILogger<SubjectPropertyController>>();
            Mock<ISubjectPropertyService> subjectproperttServiceMock = new Mock<ISubjectPropertyService>();
         

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCodeByZain", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };

            int fakeUserId = 1;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", fakeUserId.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            UpdateSubjectPropertyStateModel fakeModelReceived = new UpdateSubjectPropertyStateModel()
            {

                LoanApplicationId = 1,
                StateId=1,
                State="fake state"
            };

            bool fakeint =true;

            subjectproperttServiceMock.Setup(x => x.AddOrUpdateSubjectPropertyState(contextTenant,fakeModelReceived,fakeUserId)).ReturnsAsync(fakeint);

            var controller = new SubjectPropertyController(loggerServiceMock.Object, subjectproperttServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

          

            // Act
            var results = await controller.AddOrUpdateSubjectPropertyState(fakeModelReceived);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestGetSubjectPropertyState()
        {
            // Arrange
            Mock<ILogger<SubjectPropertyController>> loggerServiceMock = new Mock<ILogger<SubjectPropertyController>>();
            Mock<ISubjectPropertyService> subjectproperttServiceMock = new Mock<ISubjectPropertyService>();


            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCodeByZain", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };

            int fakeUserId = 1;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", fakeUserId.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1,
              
            };

            GetSubjectPropertyStateModel fakemodelData = new GetSubjectPropertyStateModel();

            subjectproperttServiceMock.Setup(x => x.GetSubjectPropertyState(contextTenant, fakeModelReceived.LoanApplicationId, fakeUserId)).ReturnsAsync(fakemodelData);

            var controller = new SubjectPropertyController(loggerServiceMock.Object, subjectproperttServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;



            // Act
            var results = await controller.GetSubjectPropertyState(fakeModelReceived);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestGetSubjectPropertyLoanAmountDetail()
        {
            // Arrange
            Mock<ILogger<SubjectPropertyController>> loggerServiceMock = new Mock<ILogger<SubjectPropertyController>>();
            Mock<ISubjectPropertyService> subjectproperttServiceMock = new Mock<ISubjectPropertyService>();


            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCodeByZain", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };

            int fakeUserId = 1;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", fakeUserId.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1,

            };

            LoanAmountDetailBase fakemodelData = new LoanAmountDetailBase();

            subjectproperttServiceMock.Setup(x => x.GetSubjectPropertyLoanAmountDetail(contextTenant, fakeModelReceived.LoanApplicationId, fakeUserId)).ReturnsAsync(fakemodelData);

            var controller = new SubjectPropertyController(loggerServiceMock.Object, subjectproperttServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;



            // Act
            var results = await controller.GetSubjectPropertyLoanAmountDetail(fakeModelReceived);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestAddOrUpdateLoanAmountDetail_InputModelIsCorrect_OKResult()
        {
            // Arrange
            Mock<ILogger<SubjectPropertyController>> loggerServiceMock = new Mock<ILogger<SubjectPropertyController>>();
            Mock<ISubjectPropertyService> subjectproperttServiceMock = new Mock<ISubjectPropertyService>();


            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCodeByZain", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };

            int fakeUserId = 1;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", fakeUserId.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            LoanAmountDetailModel fakeModelReceived = new LoanAmountDetailModel()
            {

                LoanApplicationId = 1,
                PropertyValue = 1,
                DownPayment = 1,
                GiftPartOfDownPayment = true,
                GiftPartReceived = true,
                DateOfTransfer = DateTime.Now,
                GiftAmount = 1,
                State = "fake state"
            };
        

            GetSubjectPropertyStateModel fakemodelData = new GetSubjectPropertyStateModel();
            bool fakebool = true;

            subjectproperttServiceMock.Setup(x => x.AddOrUpdateLoanAmountDetail(contextTenant, fakeModelReceived, fakeUserId)).ReturnsAsync(fakebool);

            var controller = new SubjectPropertyController(loggerServiceMock.Object, subjectproperttServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;



            // Act
            var results = await controller.AddOrUpdateLoanAmountDetail(fakeModelReceived);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestAddOrUpdateLoanAmountDetail_DownPaymentGiftReceivedAndDateIsNotValid_OKResult()
        {
            // Arrange
            Mock<ILogger<SubjectPropertyController>> loggerServiceMock = new Mock<ILogger<SubjectPropertyController>>();
            Mock<ISubjectPropertyService> subjectproperttServiceMock = new Mock<ISubjectPropertyService>();


            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCodeByZain", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };

            int fakeUserId = 1;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", fakeUserId.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            LoanAmountDetailModel fakeModelReceived = new LoanAmountDetailModel()
            {

                LoanApplicationId = 1,
                PropertyValue = 1,
                DownPayment = 1,
                GiftPartOfDownPayment = true,
                GiftPartReceived = true,
                DateOfTransfer = DateTime.Now.AddDays(10),
                GiftAmount = 1,
                State = "fake state"
            };


            GetSubjectPropertyStateModel fakemodelData = new GetSubjectPropertyStateModel();
            bool fakebool = true;

            subjectproperttServiceMock.Setup(x => x.AddOrUpdateLoanAmountDetail(contextTenant, fakeModelReceived, fakeUserId)).ReturnsAsync(fakebool);

            var controller = new SubjectPropertyController(loggerServiceMock.Object, subjectproperttServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;



            // Act
            var results = await controller.AddOrUpdateLoanAmountDetail(fakeModelReceived);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<BadRequestObjectResult>(results);
        }

        [Fact]
        public async Task TestAddOrUpdateLoanAmountDetail_DownPaymentGiftWillReceiveAndDateIsNotValid_OKResult()
        {
            // Arrange
            Mock<ILogger<SubjectPropertyController>> loggerServiceMock = new Mock<ILogger<SubjectPropertyController>>();
            Mock<ISubjectPropertyService> subjectproperttServiceMock = new Mock<ISubjectPropertyService>();


            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCodeByZain", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };

            int fakeUserId = 1;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", fakeUserId.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            LoanAmountDetailModel fakeModelReceived = new LoanAmountDetailModel()
            {

                LoanApplicationId = 1,
                PropertyValue = 1,
                DownPayment = 1,
                GiftPartOfDownPayment = true,
                GiftPartReceived = false,
                DateOfTransfer = DateTime.Now.AddDays(-10),
                GiftAmount = 1,
                State = "fake state"
            };


            GetSubjectPropertyStateModel fakemodelData = new GetSubjectPropertyStateModel();
            bool fakebool = true;

            subjectproperttServiceMock.Setup(x => x.AddOrUpdateLoanAmountDetail(contextTenant, fakeModelReceived, fakeUserId)).ReturnsAsync(fakebool);

            var controller = new SubjectPropertyController(loggerServiceMock.Object, subjectproperttServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;



            // Act
            var results = await controller.AddOrUpdateLoanAmountDetail(fakeModelReceived);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<BadRequestObjectResult>(results);
        }

        [Fact]
        public async Task TestUpdatePropertyIdentifiedWhenPropertyNotIdentified()
        {
            // Arrange
            var loggerkMock = Mock.Of<ILogger>();
            Mock<ISubjectPropertyService> subjectPropertyServiceMock = new Mock<ISubjectPropertyService>();

            PropertyIdentifiedModel fakePostModel = new PropertyIdentifiedModel()
            {
                IsIdentified = false,
                StateId = 0
            };

            // Start from here Dani bhai
            //var controller = new SubjectPropertyController(loggerkMock, subjectPropertyServiceMock.Object);

            // Act

        }
    }
}
