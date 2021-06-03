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
using Moq;
using TenantConfig.Common.DistributedCache;
using Xunit;

namespace LoanApplication.Tests
{
    public partial class BorrowerTest
    {
        [Fact]
        public async Task TestAddOrUpdateBorrowerConsentsWhenConsentNotFound()
        {
            // Arrange
            Mock<ILoanContactService> loanContactServiceMock = new Mock<ILoanContactService>();
            Mock<IBorrowerConsentService> borrowerConsentServiceMock = new Mock<IBorrowerConsentService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IBorrowerService> borrowerServiceMock = new Mock<IBorrowerService>();

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
            
            borrowerConsentServiceMock.Setup(x => x.GetAllConsentType(It.IsAny<TenantModel>())).Returns(null as ConsentTypeGetModel);

            var controller = new BorrowerController(loanContactServiceMock.Object, borrowerConsentServiceMock.Object,
                infoServiceMock.Object, null);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            BorrowerMultipleConsentsBase fakePostModel = new BorrowerMultipleConsentsBase()
            {

            };

            // Act
            var results = await controller.AddOrUpdateBorrowerConsents(fakePostModel);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<BadRequestObjectResult>(results);
        }

        [Fact]
        public async Task TestAddOrUpdateBorrowerConsentsWhenHashDoesNotMatch()
        {
            // Arrange
            Mock<ILoanContactService> loanContactServiceMock = new Mock<ILoanContactService>();
            Mock<IBorrowerConsentService> borrowerConsentServiceMock = new Mock<IBorrowerConsentService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IBorrowerService> borrowerServiceMock = new Mock<IBorrowerService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
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

            ConsentTypeGetModel fakeConsentModel = new ConsentTypeGetModel()
            {
                ConsentHash = "OriginalConsentHash"
            };
            borrowerConsentServiceMock.Setup(x => x.GetAllConsentType(It.IsAny<TenantModel>())).Returns(fakeConsentModel);

            var controller = new BorrowerController(loanContactServiceMock.Object, borrowerConsentServiceMock.Object,
                infoServiceMock.Object, null);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            BorrowerMultipleConsentsBase fakePostModel = new BorrowerMultipleConsentsBase()
            {
                ConsentHash = "TemperedConsentHash"
            };

            // Act
            var results = await controller.AddOrUpdateBorrowerConsents(fakePostModel);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<BadRequestObjectResult>(results);
        }


        [Fact]
        public async Task TestAddOrUpdateBorrowerConsentsWhenAlreadyMapped()
        {
            // Arrange
            Mock<ILoanContactService> loanContactServiceMock = new Mock<ILoanContactService>();
            Mock<IBorrowerConsentService> borrowerConsentServiceMock = new Mock<IBorrowerConsentService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IBorrowerService> borrowerServiceMock = new Mock<IBorrowerService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
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

            ConsentTypeGetModel fakeConsentModel = new ConsentTypeGetModel()
            {
                ConsentHash = "OriginalConsentHash",
                ConsentList = new EditableList<ConsentTypeModel>()
                {
                    new ConsentTypeModel()
                    {
                        Description = "Consent Description",
                        Id = 1,
                        Name = "Consent Name"
                    }
                }
            };
            borrowerConsentServiceMock.Setup(x => x.GetAllConsentType(It.IsAny<TenantModel>())).Returns(fakeConsentModel);

            var controller = new BorrowerController(loanContactServiceMock.Object, borrowerConsentServiceMock.Object,
                infoServiceMock.Object, null);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            int fakeBorrowerId = 1;
            int fakeLoanApplicationId = 1;
            BorrowerMultipleConsentsBase fakePostModel = new BorrowerMultipleConsentsBase()
            {
                ConsentHash = "OriginalConsentHash",
                BorrowerId = fakeBorrowerId,
                LoanApplicationId = fakeLoanApplicationId,
                State = "fakeState",
                IsAccepted = true
            };

            borrowerConsentServiceMock.Setup(x => x.AddOrUpdateMultipleConsents(contextTenant, It.IsAny<BorrowerMultipleConsentsModel>(), It.IsAny<string>(), fakeUserId)).ReturnsAsync(-1);

            // Act
            var results = await controller.AddOrUpdateBorrowerConsents(fakePostModel);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<BadRequestObjectResult>(results);
        }

        [Fact]
        public async Task TestAddOrUpdateBorrowerConsentsWhenNotAlreadyMapped()
        {
            // Arrange
            Mock<ILoanContactService> loanContactServiceMock = new Mock<ILoanContactService>();
            Mock<IBorrowerConsentService> borrowerConsentServiceMock = new Mock<IBorrowerConsentService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IBorrowerService> borrowerServiceMock = new Mock<IBorrowerService>();

            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
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

            ConsentTypeGetModel fakeConsentModel = new ConsentTypeGetModel()
            {
                ConsentHash = "OriginalConsentHash",
                ConsentList = new EditableList<ConsentTypeModel>()
                {
                    new ConsentTypeModel()
                    {
                        Description = "Consent Description",
                        Id = 1,
                        Name = "Consent Name"
                    }
                }
            };
            borrowerConsentServiceMock.Setup(x => x.GetAllConsentType(It.IsAny<TenantModel>())).Returns(fakeConsentModel);
            borrowerConsentServiceMock
                .Setup(x => x.GetBorrowerConsent(It.IsAny<TenantModel>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(fakeConsentModel);

            var controller = new BorrowerController(loanContactServiceMock.Object, borrowerConsentServiceMock.Object,
                infoServiceMock.Object, null);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            int fakeBorrowerId = 1;
            int fakeLoanApplicationId = 1;
            BorrowerMultipleConsentsBase fakePostModel = new BorrowerMultipleConsentsBase()
            {
                ConsentHash = "OriginalConsentHash",
                BorrowerId = fakeBorrowerId,
                LoanApplicationId = fakeLoanApplicationId,
                State = "fakeState",
                IsAccepted = true
            };

            borrowerConsentServiceMock.Setup(x => x.AddOrUpdateMultipleConsents(contextTenant, It.IsAny<BorrowerMultipleConsentsModel>(), It.IsAny<string>(), fakeUserId)).ReturnsAsync(0);

            // Act
            var results = await controller.AddOrUpdateBorrowerConsents(fakePostModel);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestGetBorrowerAcceptedConsents()
        {
            // Arrange
            Mock<ILoanContactService> loanContactServiceMock = new Mock<ILoanContactService>();
            Mock<IBorrowerConsentService> borrowerConsentServiceMock = new Mock<IBorrowerConsentService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IBorrowerService> borrowerServiceMock = new Mock<IBorrowerService>();

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

            BorrowerAcceptedConsentsGetModel fakeModelReceived = new BorrowerAcceptedConsentsGetModel()
            {
                BorrowerId = 1,
                LoanApplicationId =  1
            };

            BorrowerAcceptedConsentsModel fakeDataRetrieved = new BorrowerAcceptedConsentsModel();

            borrowerConsentServiceMock.Setup(x => x.GetBorrowerAcceptedConsents(contextTenant, fakeUserId, fakeModelReceived.BorrowerId, fakeModelReceived.LoanApplicationId)).ReturnsAsync(fakeDataRetrieved);

            var controller = new BorrowerController(loanContactServiceMock.Object, borrowerConsentServiceMock.Object,
                infoServiceMock.Object, null);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            BorrowerMultipleConsentsBase fakePostModel = new BorrowerMultipleConsentsBase()
            {

            };

            // Act
            var results = await controller.GetBorrowerAcceptedConsents(fakeModelReceived);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestGetBorrowersForFirstReview()
        {
            // Arrange
            Mock<ILoanContactService> loanContactServiceMock = new Mock<ILoanContactService>();
            Mock<IBorrowerConsentService> borrowerConsentServiceMock = new Mock<IBorrowerConsentService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IBorrowerService> borrowerServiceMock = new Mock<IBorrowerService>();

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
               
                LoanApplicationId = 1
            };

            BorrowersFirstReviewModel fakeDataRetrieved = new BorrowersFirstReviewModel();

            borrowerServiceMock.Setup(x => x.GetBorrowersForFirstReview(contextTenant, fakeUserId, fakeModelReceived.LoanApplicationId)).ReturnsAsync(fakeDataRetrieved);

            var controller = new BorrowerController(loanContactServiceMock.Object, borrowerConsentServiceMock.Object,
                infoServiceMock.Object, borrowerServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            BorrowerMultipleConsentsBase fakePostModel = new BorrowerMultipleConsentsBase()
            {

            };

            // Act
            var results = await controller.GetBorrowersForFirstReview(fakeModelReceived);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

       



    }
}
