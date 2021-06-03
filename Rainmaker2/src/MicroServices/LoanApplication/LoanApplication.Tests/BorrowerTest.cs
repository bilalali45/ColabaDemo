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
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace LoanApplication.Tests
{
    public partial class BorrowerTest
    {
        [Fact]
        public async Task TestGetBorrowerDobSsn()
        {
            // Arrange
            Mock<ILoanContactService> loanContactServiceMock = new Mock<ILoanContactService>();
            Mock<IBorrowerConsentService> borrowerConsentServiceMock = new Mock<IBorrowerConsentService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();

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
            int fakeBorrowerId = 1;
            int fakeLoanApplicationId = 1;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", fakeUserId.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            BorrowerDobSsnGetModel fakeModelReceived = new BorrowerDobSsnGetModel()
            {
                BorrowerId = fakeBorrowerId,
                LoanApplicationId = fakeLoanApplicationId
            };

            BorrowerDobSsnGet fakeResponseFromDb = new BorrowerDobSsnGet()
            {
                DobUtc = DateTime.UtcNow.AddYears(-18),
                Ssn = "123456789"
            };

            loanContactServiceMock.Setup(x => x.GetDobSsn(contextTenant, fakeModelReceived.BorrowerId,
                fakeModelReceived.LoanApplicationId, fakeUserId)).ReturnsAsync(fakeResponseFromDb);

            // Act
            var controller = new BorrowerController(loanContactServiceMock.Object, borrowerConsentServiceMock.Object,
                infoServiceMock.Object, null);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.GetBorrowerDobSsn(fakeModelReceived);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestAddOrUpdateInfoWhenDobFoundButSsnMissing()
        {
            // Arrange
            Mock<ILoanContactService> loanContactServiceMock = new Mock<ILoanContactService>();
            Mock<IBorrowerConsentService> borrowerConsentServiceMock = new Mock<IBorrowerConsentService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();

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
            int fakeBorrowerId = 1;
            int fakeLoanApplicationId = 1;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", fakeUserId.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            BorrowerDobSsnAddOrUpdate fakeModelReceived = new BorrowerDobSsnAddOrUpdate()
            {
                BorrowerId = fakeBorrowerId,
                DobUtc = DateTime.UtcNow.AddYears(-18),
                LoanApplicationId = fakeLoanApplicationId,
                State = "fakeState",
                Ssn = null
            };

            loanContactServiceMock.Setup(x => x.UpdateDobSsn(contextTenant, fakeUserId, fakeModelReceived)).ReturnsAsync(1);

            // Act
            var controller = new BorrowerController(loanContactServiceMock.Object, borrowerConsentServiceMock.Object,
                infoServiceMock.Object, null);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.AddOrUpdateDobSsn(fakeModelReceived);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestAddOrUpdateInfoWhenSsnFoundButDobMissing()
        {
            // Arrange
            Mock<ILoanContactService> loanContactServiceMock = new Mock<ILoanContactService>();
            Mock<IBorrowerConsentService> borrowerConsentServiceMock = new Mock<IBorrowerConsentService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();

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
            int fakeBorrowerId = 1;
            int fakeLoanApplicationId = 1;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", fakeUserId.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            BorrowerDobSsnAddOrUpdate fakeModelReceived = new BorrowerDobSsnAddOrUpdate()
            {
                BorrowerId = fakeBorrowerId,
                DobUtc = null,
                LoanApplicationId = fakeLoanApplicationId,
                State = "fakeState",
                Ssn = "1234567890"
            };

            loanContactServiceMock.Setup(x => x.UpdateDobSsn(contextTenant, fakeUserId, fakeModelReceived)).ReturnsAsync(1);

            // Act
            var controller = new BorrowerController(loanContactServiceMock.Object, borrowerConsentServiceMock.Object,
                infoServiceMock.Object, null);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.AddOrUpdateDobSsn(fakeModelReceived);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<BadRequestObjectResult>(results);
        }

        [Fact]
        public async Task TestAddOrUpdateInfo()
        {
            // Arrange
            Mock<ILoanContactService> loanContactServiceMock = new Mock<ILoanContactService>();
            Mock<IBorrowerConsentService> borrowerConsentServiceMock = new Mock<IBorrowerConsentService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();

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
            int fakeBorrowerId = 1;
            int fakeLoanApplicationId = 1;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", fakeUserId.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            BorrowerDobSsnAddOrUpdate fakeModelReceived = new BorrowerDobSsnAddOrUpdate()
            {
                BorrowerId = fakeBorrowerId,
                DobUtc = DateTime.UtcNow.AddYears(-18),
                LoanApplicationId = fakeLoanApplicationId,
                State = "fakeState",
                Ssn = "123456789"
            };

            loanContactServiceMock.Setup(x => x.UpdateDobSsn(contextTenant, fakeUserId, fakeModelReceived)).ReturnsAsync(1);

            // Act
            var controller = new BorrowerController(loanContactServiceMock.Object, borrowerConsentServiceMock.Object,
                infoServiceMock.Object, null);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.AddOrUpdateDobSsn(fakeModelReceived);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestGetAllConsentTypes()
        {
            // Arrange
            Mock<ILoanContactService> loanContactServiceMock = new Mock<ILoanContactService>();
            Mock<IBorrowerConsentService> borrowerConsentServiceMock = new Mock<IBorrowerConsentService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();

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

            //List<ConsentTypeModel> fakeResponseReceived = new List<ConsentTypeModel>()
            //{
            //    new ConsentTypeModel() {Description = "fakeDescription", Id = 1, Name = "fakeName"}
            //};
            ConsentTypeGetModel fakeResponseReceived = new ConsentTypeGetModel()
            {
                ConsentHash = "fakeHash",
                ConsentList = new EditableList<ConsentTypeModel>()
                {
                    new ConsentTypeModel()
                    {
                        Description = "FakeDescription", Id = 1, Name = "FakeName"
                    }
                }
            };

            borrowerConsentServiceMock.Setup(x => x.GetAllConsentType(contextTenant)).Returns(fakeResponseReceived);

            // Act
            var controller = new BorrowerController(loanContactServiceMock.Object, borrowerConsentServiceMock.Object,
                infoServiceMock.Object, null);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.GetAllConsentTypes();

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestAddOrUpdateBorrowerConsent()
        {
            // Arrange
            Mock<ILoanContactService> loanContactServiceMock = new Mock<ILoanContactService>();
            Mock<IBorrowerConsentService> borrowerConsentServiceMock = new Mock<IBorrowerConsentService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();

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
            int fakeBorrowerId = 1;
            int fakeLoanApplicationId = 1;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", fakeUserId.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            BorrowerConsentModel fakeModelReceived = new BorrowerConsentModel()
            {
                BorrowerId = fakeBorrowerId,
                Description = "fakeDescription",
                ConsentTypeId = 1,
                IsAccepted = true,
                LoanApplicationId = fakeLoanApplicationId,
                State = "fakeState"
            };

            borrowerConsentServiceMock.Setup(x => x.AddOrUpdate(contextTenant, fakeModelReceived, It.IsAny<string>(), fakeUserId)).ReturnsAsync(1);

            // Act
            var controller = new BorrowerController(loanContactServiceMock.Object, borrowerConsentServiceMock.Object,
                infoServiceMock.Object, null);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.AddOrUpdateBorrowerConsent(fakeModelReceived);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestIsRelationAlreadyMapped()
        {
            // Arrange
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

            // Act
            var controller = new BorrowerController(null, null, null, borrowerServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.IsRelationAlreadyMapped(1, 1);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
        [Fact]
        public async Task TestGetAllBorrower()
        {
            // Arrange
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

            // Act
            var controller = new BorrowerController(null, null, null, borrowerServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.GetAllBorrower(1);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestGetMaritalStatus()
        {
            // Arrange
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

            // Act
            var controller = new BorrowerController(null, null, null, borrowerServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.GetMaritalStatus(1, 1);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
        [Fact]
        public async Task TestGetBorrowerInfo()
        {
            // Arrange
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

            // Act
            var controller = new BorrowerController(null, null, null, borrowerServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.GetBorrowerInfo(1, 1);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
        [Fact]
        public async Task TestPopulatePrimaryBorrowerInfo()
        {
            // Arrange
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

            // Act
            var controller = new BorrowerController(null, null, null, borrowerServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.PopulatePrimaryBorrowerInfo();

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
        [Fact]
        public async Task TestGetBorrowerAddress()
        {
            // Arrange
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

            // Act
            var controller = new BorrowerController(null, null, null, borrowerServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.GetBorrowerAddress(1, 1);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
        [Fact]
        public async Task TestAddOrUpdateMaritalStatus()
        {
            // Arrange
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

            // Act
            var controller = new BorrowerController(null, null, null, borrowerServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.AddOrUpdateMaritalStatus(new BorrowerMaritalStatusModel { });

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
        [Fact]
        public async Task TestAddOrUpdateBorrowerInfo()
        {
            // Arrange
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

            // Act
            var controller = new BorrowerController(null, null, null, borrowerServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.AddOrUpdateBorrowerInfo(new BorrowerInfoModel { });

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
        [Fact]
        public async Task TestAddOrUpdateAddress()
        {
            // Arrange
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

            // Act
            var controller = new BorrowerController(null, null, null, borrowerServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.AddOrUpdateAddress(new BorrowerAddressModel { });

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
        [Fact]
        public async Task TestDeleteSecondaryBorrower()
        {
            // Arrange
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

            // Act
            var controller = new BorrowerController(null, null, null, borrowerServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.DeleteSecondaryBorrower(new DeleteBorrowerModel { });

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
        [Fact]
        public async Task IsRelationAlreadyMappedServiceTest()
        {
            TenantModel model = new TenantModel();
            model.Id = 3;
            model.Code = "33";
            model.Urls = new List<UrlModel>
                    {  new UrlModel
                    {
                        Type=TenantUrlType.Customer,
                        Url="apply.lendova9.com:9003"
                    }
                    };
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

                    {
                        new TenantConfig.Common.DistributedCache.BranchModel
                        {
                            Code="texas",
                            Id= 3,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id= 3,
                                     Code="33"
                                 }
                            }

                        }
                    };

            //Arrange
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();

            dataContext.SaveChanges();
            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationIsRelationAlreadyMappedServiceTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            Borrower borrower = new Borrower
            {
                Id = 3,
                LoanApplicationId = 3,

                TenantId = 3,
                OwnTypeId = 3,
                LoanContact_LoanContactId = new LoanContact
                {
                    Id = 3,
                    TenantId = 3
                },
                RelationWithPrimaryId = 3


            };

            dataContext1.Set<Borrower>().Add(borrower);
            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 3,
                UserId = 3,
                TenantId = 3
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            var service = new BorrowerService(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), Mock.Of<IMyPropertyService>());

            var result = await service.IsRelationAlreadyMapped(model, 3, 3, 3);
            //Assert
            Assert.NotNull(result);

        }
        [Fact]
        public async Task GetAllBorrowerServiceTest()
        {
            TenantModel model = new TenantModel();
            model.Id = 6;
            model.Code = "33";
            model.Urls = new List<UrlModel>
                    {  new UrlModel
                    {
                        Type=TenantUrlType.Customer,
                        Url="apply.lendova9.com:9003"
                    }
                    };
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

                    {
                        new TenantConfig.Common.DistributedCache.BranchModel
                        {
                            Code="texas",
                            Id=6,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=6,
                                     Code="33"
                                 }
                            }

                        }
                    };

            //Arrange
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigGetAllBorrowerServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();

            dataContext.SaveChanges();
            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationGetAllBorrowerServiceTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            Borrower borrower = new Borrower
            {
                Id = 6,
                LoanApplicationId = 6,

                TenantId = 6,
                OwnTypeId = 6,
                LoanContact_LoanContactId = new LoanContact
                {
                    Id = 6,
                    TenantId = 6
                },
                RelationWithPrimaryId = 6


            };

            dataContext1.Set<Borrower>().Add(borrower);
            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 6,
                UserId = 6,
                TenantId = 6
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            var service = new BorrowerService(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), Mock.Of<IMyPropertyService>());

            var result = await service.GetAllBorrower(model, 6, 6);
            //Assert
            Assert.NotNull(result);

        }
        [Fact]
        public async Task GetMaritalStatusServiceTest()
        {
            TenantModel model = new TenantModel();
            model.Id = 9;
            model.Code = "33";
            model.Urls = new List<UrlModel>
                    {  new UrlModel
                    {
                        Type=TenantUrlType.Customer,
                        Url="apply.lendova9.com:9003"
                    }
                    };
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

                    {
                        new TenantConfig.Common.DistributedCache.BranchModel
                        {
                            Code="texas",
                            Id=9,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=9,
                                     Code="33"
                                 }
                            }

                        }
                    };
            //Arrange
            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase("LoanApplicationGetPrimaryBorrowerMaritalStatus");
            options = builder.Options;
            using LoanApplicationContext dataContext = new LoanApplicationContext(options);
            dataContext.Database.EnsureCreated();

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 6002,
                MilestoneId = 1,
                IsActive = true,
                UserId = 1,
                TenantId = 9,
                SettingHash = "123"
            };
            dataContext.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            LoanApplicationDb.Entity.Models.Borrower borrower = new LoanApplicationDb.Entity.Models.Borrower
            {
                Id = 6002,
                OwnTypeId = 1,
                LoanApplicationId = 6002,
                LoanContactId = 6002,
                TenantId = 9
            };
            dataContext.Set<LoanApplicationDb.Entity.Models.Borrower>().Add(borrower);
            LoanApplicationDb.Entity.Models.LoanContact contact = new LoanApplicationDb.Entity.Models.LoanContact
            {
                Id = 6002,
                MaritalStatusId = 1,
            };
            dataContext.Set<LoanApplicationDb.Entity.Models.LoanContact>().Add(contact);
            dataContext.SaveChanges();

            var service = new BorrowerService(null, new UnitOfWork<LoanApplicationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, Mock.Of<IMyPropertyService>());
            var res = await service.GetMaritalStatus(model, 1, 6002, 6002);
            Assert.NotNull(res);
        }
        [Fact]
        public async Task GetBorrowerInfo()
        {
            Mock<IBorrowerService> borrowerService = new Mock<IBorrowerService>();
            TenantModel model = new TenantModel();
            model.Id = 9;
            model.Code = "33";
            model.Urls = new List<UrlModel>
                    {  new UrlModel
                    {
                        Type=TenantUrlType.Customer,
                        Url="apply.lendova9.com:9003"
                    }
                    };
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

                    {
                        new TenantConfig.Common.DistributedCache.BranchModel
                        {
                            Code="texas",
                            Id=9,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=9,
                                     Code="33"
                                 }
                            }

                        }
                    };
            //Arrange
            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase("LoanApplicationGetBorrowerInfo");
            options = builder.Options;
            using LoanApplicationContext dataContext = new LoanApplicationContext(options);
            dataContext.Database.EnsureCreated();

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 6004,
                MilestoneId = 1,
                IsActive = true,
                UserId = 1,
                TenantId = 9,
                SettingHash = "123"
            };
            dataContext.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            LoanApplicationDb.Entity.Models.Borrower borrower = new LoanApplicationDb.Entity.Models.Borrower
            {
                Id = 6004,
                OwnTypeId = 1,
                LoanApplicationId = 6004,
                LoanContactId = 6004,
                TenantId = 9
            };
            dataContext.Set<LoanApplicationDb.Entity.Models.Borrower>().Add(borrower);
            LoanApplicationDb.Entity.Models.LoanContact contact = new LoanApplicationDb.Entity.Models.LoanContact
            {
                Id = 6004,
                MaritalStatusId = 1,
                FirstName = "Mubashir"
            };
            dataContext.Set<LoanApplicationDb.Entity.Models.LoanContact>().Add(contact);
            dataContext.SaveChanges();

            var service = new BorrowerService(null, new UnitOfWork<LoanApplicationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null, Mock.Of<IMyPropertyService>());
            var res = await service.GetBorrowerInfo(model, 1, 6004, 6004);
            Assert.Equal("Mubashir", res.FirstName);
        }
        [Fact]
        public async Task PopulatePrimaryBorrowerInfo()
        {
            TenantModel model = new TenantModel();
            model.Id = 9;
            model.Code = "33";
            model.Urls = new List<UrlModel>
                    {  new UrlModel
                    {
                        Type=TenantUrlType.Customer,
                        Url="apply.lendova9.com:9003"
                    }
                    };
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

                    {
                        new TenantConfig.Common.DistributedCache.BranchModel
                        {
                            Code="texas",
                            Id=9,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=9,
                                     Code="33"
                                 }
                            }

                        }
                    };
            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationPopulatePrimaryBorrowerInfo");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();

            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigPopulatePrimaryBorrowerInfo");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();

            Customer customer = new Customer
            {
                Id = 6001,
                TenantId = 9,
                UserId = 1,
                ContactId = 6001
            };
            dataContext.Set<Customer>().Add(customer);

            Contact contact = new Contact
            {
                Id = 6001,
                TenantId = 9,
                FirstName = "Mubashir",
            };
            dataContext.Set<Contact>().Add(contact);

            dataContext.SaveChanges();

            var service = new BorrowerService(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), null, Mock.Of<IMyPropertyService>());
            var res = await service.PopulatePrimaryBorrowerInfo(model, 1);
            Assert.Equal("Mubashir", res.FirstName);
        }
        [Fact]
        public async Task GetBorrowerAddressServiceTest()
        {
            TenantModel model = new TenantModel();
            model.Id = 5;
            model.Code = "33";
            model.Urls = new List<UrlModel>
                    {  new UrlModel
                    {
                        Type=TenantUrlType.Customer,
                        Url="apply.lendova9.com:9003"
                    }
                    };
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

                    {
                        new TenantConfig.Common.DistributedCache.BranchModel
                        {
                            Code="texas",
                            Id=5,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=5,
                                     Code="33"
                                 }
                            }

                        }
                    };

            //Arrange
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigGetBorrowerAddressServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();

            dataContext.SaveChanges();
            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationGetBorrowerAddressServiceTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            Borrower borrower = new Borrower
            {
                Id = 5,
                LoanApplicationId = 5,
                TenantId = 5,
            };

            dataContext1.Set<Borrower>().Add(borrower);
            LoanApplicationDb.Entity.Models.AddressInfo addressInfo = new LoanApplicationDb.Entity.Models.AddressInfo
            {
                Id = 5,
                TenantId = 5,
                CityName = "Test",
                CountryId=1,
                StateId=1,
            };

            dataContext1.Set<LoanApplicationDb.Entity.Models.AddressInfo>().Add(addressInfo);
            BorrowerResidence borrowerResidence = new BorrowerResidence
            {
                Id=5,
                BorrowerId=5,
                LoanAddressId=5,
                TypeId=1,
                OwnershipTypeId=1
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.BorrowerResidence>().Add(borrowerResidence);
            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            var service = new BorrowerService(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), Mock.Of<IMyPropertyService>());

            var result = await service.GetBorrowerAddress(model, 5, 5, 5);
            //Assert
            Assert.NotNull(result);

        }
        [Fact]
        public async Task AddOrUpdateMaritalStatusTest()
        {
            TenantModel model = new TenantModel();
            model.Id = 5;
            model.Code = "33";
            model.Urls = new List<UrlModel>
                    {  new UrlModel
                    {
                        Type=TenantUrlType.Customer,
                        Url="apply.lendova9.com:9003"
                    }
                    };
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

                    {
                        new TenantConfig.Common.DistributedCache.BranchModel
                        {
                            Code="texas",
                            Id=5,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=5,
                                     Code="33"
                                 }
                            }

                        }
                    };

            //Arrange
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigAddOrUpdateMaritalStatusTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();

            dataContext.SaveChanges();
            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationAddOrUpdateMaritalStatusTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            Borrower borrower = new Borrower
            {
                Id = 5,
                LoanApplicationId = 5,
                TenantId = 5,
                LoanContactId=5,
                OwnTypeId=1
            };

            dataContext1.Set<Borrower>().Add(borrower);

            Borrower borrower1 = new Borrower
            {
                Id = 6,
                LoanApplicationId = 5,
                TenantId = 5,
                OwnTypeId = 2,
                RelationWithPrimaryId=(byte)FamilyRelationTypeEnum.Spouse
            };

            dataContext1.Set<Borrower>().Add(borrower1);

            LoanContact loanContact = new LoanContact
            {
                Id = 5,
                TenantId = 5,
                MaritalStatusId = (int)MaritalStatus.Married
            };

            dataContext1.Set<LoanContact>().Add(loanContact);

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            var service = new BorrowerService(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), Mock.Of<IMyPropertyService>());

            //var result = await service.AddOrUpdateMaritalStatus(model, 5, new BorrowerMaritalStatusModel { Id=5,LoanApplicationId=5,MaritalStatus=3,RelationshipWithPrimary=1});
            var result = await service.AddOrUpdateMaritalStatus(model, 5, new BorrowerMaritalStatusModel { Id = 5, LoanApplicationId = 5, MaritalStatus = 3 });
            //Assert
            Assert.True(result);

        }
        [Fact]
        public async Task AddOrUpdateMaritalStatusSecondaryTest()
        {
            TenantModel model = new TenantModel();
            model.Id = 5;
            model.Code = "33";
            model.Urls = new List<UrlModel>
                    {  new UrlModel
                    {
                        Type=TenantUrlType.Customer,
                        Url="apply.lendova9.com:9003"
                    }
                    };
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

                    {
                        new TenantConfig.Common.DistributedCache.BranchModel
                        {
                            Code="texas",
                            Id=5,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=5,
                                     Code="33"
                                 }
                            }

                        }
                    };

            //Arrange
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigAddOrUpdateMaritalStatusSecondaryTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();

            dataContext.SaveChanges();
            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationAddOrUpdateMaritalStatusSecondaryTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            Borrower borrower = new Borrower
            {
                Id = 5,
                LoanApplicationId = 5,
                TenantId = 5,
                OwnTypeId = 1
            };

            dataContext1.Set<Borrower>().Add(borrower);

            Borrower borrower1 = new Borrower
            {
                Id = 6,
                LoanApplicationId = 5,
                TenantId = 5,
                OwnTypeId = 2,
                RelationWithPrimaryId = (byte)FamilyRelationTypeEnum.Spouse,
                LoanContactId=5
            };

            dataContext1.Set<Borrower>().Add(borrower1);

            LoanContact loanContact = new LoanContact
            {
                Id = 5,
                TenantId = 5,
                MaritalStatusId = (int)MaritalStatus.Married
            };

            dataContext1.Set<LoanContact>().Add(loanContact);

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            var service = new BorrowerService(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), Mock.Of<IMyPropertyService>());

            //var result = await service.AddOrUpdateMaritalStatus(model, 5, new BorrowerMaritalStatusModel { Id = 6, LoanApplicationId = 5, MaritalStatus = 3, RelationshipWithPrimary = 1 });
            var result = await service.AddOrUpdateMaritalStatus(model, 5, new BorrowerMaritalStatusModel { Id = 6, LoanApplicationId = 5, MaritalStatus = 3});
            //Assert
            Assert.True(result);

        }
        [Fact]
        public async Task AddOrUpdateBorrowerInfoTest()
        {
            TenantModel model = new TenantModel();
            model.Id = 5;
            model.Code = "33";
            model.Urls = new List<UrlModel>
                    {  new UrlModel
                    {
                        Type=TenantUrlType.Customer,
                        Url="apply.lendova9.com:9003"
                    }
                    };
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

                    {
                        new TenantConfig.Common.DistributedCache.BranchModel
                        {
                            Code="texas",
                            Id=5,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=5,
                                     Code="33"
                                 }
                            }

                        }
                    };

            //Arrange
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigAddOrUpdateBorrowerInfoTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();

            dataContext.SaveChanges();
            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationAddOrUpdateBorrowerInfoTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();

            Borrower borrower1 = new Borrower
            {
                Id = 5,
                LoanApplicationId = 5,
                TenantId = 5,
                OwnTypeId = 2,
                RelationWithPrimaryId = (byte)FamilyRelationTypeEnum.Spouse,
                LoanContactId = 5
            };

            dataContext1.Set<Borrower>().Add(borrower1);

            LoanContact loanContact = new LoanContact
            {
                Id = 5,
                TenantId = 5,
                MaritalStatusId = (int)MaritalStatus.Married
            };

            dataContext1.Set<LoanContact>().Add(loanContact);

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            var service = new BorrowerService(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), Mock.Of<IMyPropertyService>());

            var result = await service.AddOrUpdateBorrowerInfo(model, 5, new BorrowerInfoModel { Id = 5, LoanApplicationId = 5});
            //Assert
            Assert.Equal(5,result.Id);

        }
        [Fact]
        public async Task AddOrUpdateBorrowerInfoSecTest()
        {
            TenantModel model = new TenantModel();
            model.Id = 5;
            model.Code = "33";
            model.Urls = new List<UrlModel>
                    {  new UrlModel
                    {
                        Type=TenantUrlType.Customer,
                        Url="apply.lendova9.com:9003"
                    }
                    };
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

                    {
                        new TenantConfig.Common.DistributedCache.BranchModel
                        {
                            Code="texas",
                            Id=5,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=5,
                                     Code="33"
                                 }
                            }

                        }
                    };

            //Arrange
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigAddOrUpdateBorrowerInfoSecTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();

            dataContext.SaveChanges();
            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationAddOrUpdateBorrowerInfoSecTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();

            Borrower borrower1 = new Borrower
            {
                Id = 5,
                LoanApplicationId = 5,
                TenantId = 5,
                OwnTypeId = 2,
                RelationWithPrimaryId = (byte)FamilyRelationTypeEnum.Spouse,
                LoanContactId = 5
            };

            dataContext1.Set<Borrower>().Add(borrower1);

            LoanContact loanContact = new LoanContact
            {
                Id = 5,
                TenantId = 5,
                MaritalStatusId = (int)MaritalStatus.Married
            };

            dataContext1.Set<LoanContact>().Add(loanContact);

            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            var service = new BorrowerService(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), Mock.Of<IMyPropertyService>());

            var result = await service.AddOrUpdateBorrowerInfo(model, 5, new BorrowerInfoModel { Id = null, LoanApplicationId = 5 });
            //Assert
            Assert.NotEqual(5, result.Id);

        }

        [Fact]
        public async Task AddOrUpdateAddressTest()
        {
            TenantModel model = new TenantModel();
            model.Id = 5;
            model.Code = "33";
            model.Urls = new List<UrlModel>
                    {  new UrlModel
                    {
                        Type=TenantUrlType.Customer,
                        Url="apply.lendova9.com:9003"
                    }
                    };
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

                    {
                        new TenantConfig.Common.DistributedCache.BranchModel
                        {
                            Code="texas",
                            Id=5,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=5,
                                     Code="33"
                                 }
                            }

                        }
                    };

            //Arrange
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigAddOrUpdateAddressTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();

            dataContext.SaveChanges();
            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationAddOrUpdateAddressTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            Borrower borrower = new Borrower
            {
                Id = 5,
                LoanApplicationId = 5,
                TenantId = 5,
            };

            dataContext1.Set<Borrower>().Add(borrower);
            LoanApplicationDb.Entity.Models.AddressInfo addressInfo = new LoanApplicationDb.Entity.Models.AddressInfo
            {
                Id = 5,
                TenantId = 5,
                CityName = "Test",
                CountryId = 1,
                StateId = 1,
            };

            dataContext1.Set<LoanApplicationDb.Entity.Models.AddressInfo>().Add(addressInfo);
            BorrowerResidence borrowerResidence = new BorrowerResidence
            {
                Id = 5,
                BorrowerId = 5,
                LoanAddressId = 5,
                TypeId = 1,
                OwnershipTypeId = 1
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.BorrowerResidence>().Add(borrowerResidence);
            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            var service = new BorrowerService(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), Mock.Of<IMyPropertyService>());

            var result = await service.AddOrUpdateAddress(model, 5, new BorrowerAddressModel { Id=5, LoanApplicationId=5});
            //Assert
            Assert.Equal(5,result);

        }

        [Fact]
        public async Task AddOrUpdateAddressNoResTest()
        {
            TenantModel model = new TenantModel();
            model.Id = 5;
            model.Code = "33";
            model.Urls = new List<UrlModel>
                    {  new UrlModel
                    {
                        Type=TenantUrlType.Customer,
                        Url="apply.lendova9.com:9003"
                    }
                    };
            model.Branches = new List<TenantConfig.Common.DistributedCache.BranchModel>

                    {
                        new TenantConfig.Common.DistributedCache.BranchModel
                        {
                            Code="texas",
                            Id=5,
                            IsCorporate=true,
                            LoanOfficers= new List<LoanOfficerModel>
                            {
                                 new LoanOfficerModel
                                 {
                                     Id=5,
                                     Code="33"
                                 }
                            }

                        }
                    };

            //Arrange
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigAddOrUpdateAddressNoResTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();

            dataContext.SaveChanges();
            //Arrange
            DbContextOptions<LoanApplicationContext> options1;
            var builder1 = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder1.UseInMemoryDatabase("LoanApplicationAddOrUpdateAddressNoResTest");
            options1 = builder1.Options;
            using LoanApplicationContext dataContext1 = new LoanApplicationContext(options1);
            dataContext1.Database.EnsureCreated();
            Borrower borrower = new Borrower
            {
                Id = 5,
                LoanApplicationId = 5,
                TenantId = 5,
            };

            dataContext1.Set<Borrower>().Add(borrower);
            LoanApplicationDb.Entity.Models.AddressInfo addressInfo = new LoanApplicationDb.Entity.Models.AddressInfo
            {
                Id = 5,
                TenantId = 5,
                CityName = "Test",
                CountryId = 1,
                StateId = 1,
            };

            dataContext1.Set<LoanApplicationDb.Entity.Models.AddressInfo>().Add(addressInfo);
            BorrowerResidence borrowerResidence = new BorrowerResidence
            {
                Id = 5,
                BorrowerId = 6,
                LoanAddressId = 5,
                TypeId = 1,
                OwnershipTypeId = 1
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.BorrowerResidence>().Add(borrowerResidence);
            LoanApplicationDb.Entity.Models.LoanApplication loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication
            {
                Id = 5,
                UserId = 5,
                TenantId = 5
            };
            dataContext1.Set<LoanApplicationDb.Entity.Models.LoanApplication>().Add(loanApplication);
            dataContext1.SaveChanges();

            var service = new BorrowerService(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), new UnitOfWork<LoanApplicationContext>(dataContext1, new RepositoryProvider(new RepositoryFactories())), Mock.Of<IServiceProvider>(), Mock.Of<IMyPropertyService>());

            var result = await service.AddOrUpdateAddress(model, 5, new BorrowerAddressModel { Id = 5, LoanApplicationId = 5 });
            //Assert
            Assert.Equal(5, result);

        }
    }
}
