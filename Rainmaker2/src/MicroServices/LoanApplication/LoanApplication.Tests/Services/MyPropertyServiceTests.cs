using LoanApplication.API.Controllers;
using LoanApplication.Model;
using LoanApplication.Service;
using LoanApplication.Tests.Helpers;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;
using TrackableEntities.Common.Core;
using Xunit;

namespace LoanApplication.Tests.Services
{
    public partial class MyPropertyServiceTests
    {

        private readonly TenantModel _tenant;
        public MyPropertyServiceTests()
        {
            _tenant = ObjectHelper.GetTenantModel(1);
        }

        [Fact]
        public async Task GetPrimaryBorrowerAddressDetail_Test200ok()
        {

            // Arrange
            Mock<IMyPropertyService> mypropertyServiceMock = new Mock<IMyPropertyService>();

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
            var controller = new MyPropertyController(mypropertyServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.GetPrimaryBorrowerAddressDetail(1);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
           
        }

        [Fact]
        public async Task GetPropertyValue_Test200ok()
        {

            // Arrange
            Mock<IMyPropertyService> mypropertyServiceMock = new Mock<IMyPropertyService>();

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
            var controller = new MyPropertyController(mypropertyServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.GetPropertyValue(1, 1);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task GetPropertyValue_ShouldReturnPropertyValue()
        {

            // Arrange
            Mock<IMyPropertyService> mypropertyServiceMock = new Mock<IMyPropertyService>();

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
            var controller = new MyPropertyController(mypropertyServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.GetPropertyValue(1, 1);

            // Assert
            Assert.NotNull(results);
        }



        [Fact]
        public async Task AddOrUpdatePropertyValue_ShouldUpdatePropertyValue()
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdatBorrowerAdditionalPropertyAddress_ShouldAddRecord));
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            //const string testMethodName = nameof(AddOrUpdatePropertyValue_ShouldUpdatePropertyValue);
            //var Uow = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            //var myPropertyService = new MyPropertyService(Uow, null);


          
            const int Id = 1;
            const int userId = 18;
            const int loanApplicationId = 1;
            const int tenantId = 1;
            const int borrowerPropertyId = 1;


            var borrowerProperty = new LoanApplicationDb.Entity.Models.BorrowerProperty()
            {
                Id = borrowerPropertyId,
                TenantId = _tenant.Id,
                TrackingState = TrackingState.Added,

                Borrower = new Borrower()
                {
                    Id = Id,
                    LoanApplicationId = loanApplicationId,
                    TrackingState = TrackingState.Added,

                    LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                    {
                        Id = Id,
                        UserId = userId,
                        TenantId = _tenant.Id,
                        TrackingState = TrackingState.Added
                    }
                },
                PropertyInfo = new PropertyInfo()
                {
                   Id = Id,
                   TenantId = _tenant.Id,
                   PropertyTypeId = 1,
                   PropertyUsageId = 1,
                    TrackingState = TrackingState.Added
                }
            };


            //unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();



            //Act
            CurrentResidenceModel currentResidenceModel = new CurrentResidenceModel()
            {
                Id = Id,
                BorrowerId = 1,
                LoanApplicationId = Id,
                IsSelling = true,
                PropertyValue = 4000,
                OwnersDue = 100,
                State = "abc"
            };


            await myPropertyService.AddOrUpdatePropertyValue(_tenant, userId, currentResidenceModel);


            //Assert
            var record = await unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Query(x => x.Id == borrowerProperty.Id).SingleOrDefaultAsync();

            Assert.NotNull(record);
            Assert.Equal(borrowerProperty.PropertyInfo.PropertyValue, record.PropertyInfo.PropertyValue);

        }


        [Fact]
        public async Task AddOrUpdatBorrowerAdditionalPropertyAddress_ShouldAddRecord()
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>("AddOrUpdatBorrowerAdditionalPropertyAddress_ShouldAddRecord11");
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = 1;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            const int borrowerPropertyId = 1;
            const int propertyInfoId = 1;

            var borrowerProperty = new BorrowerProperty
            {
                Id = borrowerPropertyId,
                TenantId = loanApplication.TenantId,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    LoanApplicationId = loanApplication.Id,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                },

                PropertyInfo = new PropertyInfo()
                {
                    Id = propertyInfoId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();

            //Act
            BorrowerAdditionalPropertyAddressRequestModel borrowerAdditionalPropertyAddressRequestModel = new BorrowerAdditionalPropertyAddressRequestModel()
            {
                BorrowerPropertyId = borrowerPropertyId,
                LoanApplicationId = loanApplication.Id,
                CountryId = 1,
                StateId = 1
            };

            await myPropertyService.AddOrUpdatBorrowerAdditionalPropertyAddress(_tenant, userId, borrowerAdditionalPropertyAddressRequestModel);

            //Assert
            var record = unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Query(x => x.Id == borrowerPropertyId).FirstOrDefault();

            Assert.NotNull(record.PropertyInfo.AddressInfo);

        }

        [Fact]
        public async Task AddOrUpdatBorrowerAdditionalPropertyAddress_ShouldUpdateRecord()
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdatBorrowerAdditionalPropertyAddress_ShouldUpdateRecord));
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            const int borrowerPropertyId = 1;
            const int propertyInfoId = 1;
            const int cityId = 1;
            const int countryId = 1;

            var borrowerProperty = new BorrowerProperty
            {
                Id = borrowerPropertyId,
                TenantId = loanApplication.TenantId,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    LoanApplicationId = loanApplication.Id,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                },

                PropertyInfo = new PropertyInfo()
                {
                    Id = propertyInfoId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    AddressInfo = new AddressInfo()
                    {
                        TenantId = loanApplication.TenantId,
                        CityId = cityId,
                        CountryId = countryId,
                        CountryName = "ABC",
                        StateName = "ABC",
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();


            //Act
            const int updatedCountryId = 2;
            BorrowerAdditionalPropertyAddressRequestModel borrowerAdditionalPropertyAddressRequestModel = new BorrowerAdditionalPropertyAddressRequestModel()
            {
                BorrowerPropertyId = borrowerPropertyId,
                LoanApplicationId = loanApplication.Id,
                CountryName = "ABC Edited",
                StateName = "ABC Edited",
                CountryId = 2
            };

            await myPropertyService.AddOrUpdatBorrowerAdditionalPropertyAddress(_tenant, userId, borrowerAdditionalPropertyAddressRequestModel);


            //Assert
            var record = unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Query(x => x.Id == borrowerPropertyId).Single();

            Assert.NotNull(record);
            Assert.Equal(updatedCountryId, record.PropertyInfo.AddressInfo.CountryId);

        }

        [Fact]
        public async Task GetBorrowerAdditionalPropertyAddress_ShouldReturnBorrowerAdditionalPropertyAddress()
        {
            //Arrange
            const string testMethodName = nameof(GetBorrowerAdditionalPropertyAddress_ShouldReturnBorrowerAdditionalPropertyAddress);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            const int borrowerPropertyId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int propertyInfoId = 1;
            const int addressInfoId = 1;

            var borrowerProperty = new LoanApplicationDb.Entity.Models.BorrowerProperty()
            {
                Id = borrowerPropertyId,
                TenantId = _tenant.Id,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplicationId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                    {
                        Id = loanApplicationId,
                        UserId = userId,
                        TenantId = _tenant.Id,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                },
                PropertyInfo = new PropertyInfo()
                {
                    Id = propertyInfoId,
                    AddressInfoId = addressInfoId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    AddressInfo = new AddressInfo()
                    {
                        Id = addressInfoId,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                }
            };



            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();

            //Act
            BorrowerAdditionalPropertyAddressRsponseModel borrowerAdditionalPropertyAddressRsponseModel = await myPropertyService.GetBorrowerAdditionalPropertyAddress(_tenant, userId, loanApplicationId, borrowerPropertyId);

            //Assert
            Assert.NotNull(borrowerAdditionalPropertyAddressRsponseModel);
        }

        [Fact]
        public async Task AddOrUpdateAdditionalPropertyInfo_ShouldUpdateRecord() //Always Update
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateAdditionalPropertyInfo_ShouldUpdateRecord));
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            const int borrowerPropertyId = 1;
            const int propertyInfoId = 1;
            const int borrowerId = 1;
            const int propertyUsageId = 4;

            var borrowerProperty = new BorrowerProperty
            {
                Id = borrowerPropertyId,
                TenantId = loanApplication.TenantId,
                TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplication.Id,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                },

                PropertyInfo = new PropertyInfo()
                {
                    Id = propertyInfoId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();


            //Act
            const decimal updatedRentalIncome = 200;
            BorrowerAdditionalPropertyInfoRequestModel borrowerAdditionalPropertyInfoRequestModel = new BorrowerAdditionalPropertyInfoRequestModel()
            {
                Id = borrowerPropertyId,
                BorrowerId = borrowerId,
                LoanApplicationId = loanApplication.Id,
                PropertyUsageId = propertyUsageId,
                RentalIncome = updatedRentalIncome,
            };

            await myPropertyService.AddOrUpdateAdditionalPropertyInfo(_tenant, userId, borrowerAdditionalPropertyInfoRequestModel);


            //Assert
            var record = unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Query(x => x.Id == borrowerPropertyId).Single();

            Assert.NotNull(record);
            Assert.Equal(updatedRentalIncome, record.PropertyInfo.RentalIncome);

        }

        [Fact]
        public async Task GetBorrowerAdditionalPropertyInfo_ShouldReturnBorrowerAdditionalPropertyInfo()
        {
            //Arrange
            const string testMethodName = nameof(GetBorrowerAdditionalPropertyInfo_ShouldReturnBorrowerAdditionalPropertyInfo);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            const int borrowerPropertyId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int propertyInfoId = 1;
            const int propertyUsageId = 4;
            const decimal rentalIncome = 200;

            var borrowerProperty = new LoanApplicationDb.Entity.Models.BorrowerProperty()
            {
                Id = borrowerPropertyId,
                TenantId = _tenant.Id,
                TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplicationId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                    {
                        Id = loanApplicationId,
                        UserId = userId,
                        TenantId = _tenant.Id,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                },
                PropertyInfo = new PropertyInfo()
                {
                    Id = propertyInfoId,
                    PropertyUsageId = propertyUsageId,
                    RentalIncome = rentalIncome,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();

            //Act
            BorrowerAdditionalPropertyInfoResponseModel borrowerAdditionalPropertyInfoResponseModel = await myPropertyService.GetBorrowerAdditionalPropertyInfo(_tenant, userId, loanApplicationId, borrowerPropertyId);

            //Assert
            Assert.NotNull(borrowerAdditionalPropertyInfoResponseModel);
        }

        [Fact]
        public async Task GetBorrowerAdditionalPropertyType_ShouldReturnBorrowerAdditionalPropertyType()
        {
            //Arrange
            const string testMethodName = nameof(GetBorrowerAdditionalPropertyType_ShouldReturnBorrowerAdditionalPropertyType);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            const int borrowerPropertyId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int propertyInfoId = 1;
            const int propertyTypeId = 5;

            var borrowerProperty = new LoanApplicationDb.Entity.Models.BorrowerProperty()
            {
                Id = borrowerPropertyId,
                TenantId = _tenant.Id,
                TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplicationId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                    {
                        Id = loanApplicationId,
                        UserId = userId,
                        TenantId = _tenant.Id,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                },
                PropertyInfo = new PropertyInfo()
                {
                    Id = propertyInfoId,
                    PropertyTypeId = propertyTypeId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();

            //Act
            BorrowerAdditionalPropertyResponseModel borrowerAdditionalPropertyResponseModel = await myPropertyService.GetBorrowerAdditionalPropertyType(_tenant, userId, loanApplicationId, borrowerPropertyId);

            //Assert
            Assert.NotNull(borrowerAdditionalPropertyResponseModel);
        }

        [Fact]
        public async Task GetPrimaryBorrowerAddressDetail_ShouldReturnGetPrimaryBorrowerAddressDetail()
        {
            //Arrange
            const string testMethodName = nameof(GetPrimaryBorrowerAddressDetail_ShouldReturnGetPrimaryBorrowerAddressDetail);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            const int countryId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int cityId = 1;
            const int stateId = 1;
            const int addressInfo = 1;

            var borrower = new Borrower()
            {
                Id = borrowerId,
                LoanApplicationId = loanApplicationId,
                TenantId = _tenant.Id,
                OwnTypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    Id = loanApplicationId,
                    UserId = userId,
                    TenantId = _tenant.Id,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                },
                BorrowerResidences = new HashSet<BorrowerResidence>()
                {
                    new BorrowerResidence
                    {
                        Id= 1 ,
                        BorrowerId = borrowerId,
                        TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,
                        OwnershipTypeId = 1,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                        LoanAddress = new  AddressInfo()
                        {
                            Id = addressInfo,
                            CityId = cityId,
                            CountryId = countryId,
                            StateId = stateId,
                            CityName = "ABC",
                            CountryName = "ABC",
                            StateName = "ABC",
                            StreetAddress = "ABC",
                            UnitNo = "A",
                            ZipCode = "ABC",
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                        }
                    }
                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);
            await unitOfWork.SaveChangesAsync();

            //Act
            PrimaryBorrowerDetailModel primaryBorrowerDetailModel = await myPropertyService.GetPrimaryBorrowerAddressDetail(_tenant, userId, loanApplicationId);

            //Assert
            Assert.NotNull(primaryBorrowerDetailModel);
        }



        [Fact]
        public async Task Encryptiondobssn_OkTest()
        {
            // Arrange
            Mock<ILoanContactService> loanContactServiceMock = new Mock<ILoanContactService>();
            Mock<IBorrowerConsentService> borrowerConsentServiceMock = new Mock<IBorrowerConsentService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(Encryptiondobssn_OkTest));
            var loancontatservice = new LoanContactService(null, unitOfWork, null, null);



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

            int fakeUserId = 18;
            int fakeBorrowerId = 2298;
            int fakeLoanApplicationId = 2175;

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


            BorrowerDobSsnGet ssn = loancontatservice.GetDobSsn(contextTenant, fakeModelReceived.BorrowerId, fakeModelReceived.LoanApplicationId, fakeUserId).Result;

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
          
        }


        [Fact]
        public async Task GetPropertyList_ShouldReturnPropertyList()
        {
            //Arrange
            const string testMethodName = nameof(GetPropertyList_ShouldReturnPropertyList);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            const int borrowerPropertyId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int propertyInfoId = 1;
            const int propertyTypeId = 5;

            var borrowerProperty = new LoanApplicationDb.Entity.Models.BorrowerProperty()
            {
                Id = borrowerPropertyId,
                TenantId = _tenant.Id,
                BorrowerId = borrowerId,
                TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,
                
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplicationId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                    {
                        Id = loanApplicationId,
                        UserId = userId,
                        TenantId = _tenant.Id,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                },
                PropertyInfo = new PropertyInfo()
                {
                    Id = propertyInfoId,
                    PropertyTypeId = propertyTypeId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    PropertyType = new PropertyType()
                    {
                        Id = 1,
                        Name = "ABC",
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    },
                    AddressInfo = new AddressInfo()
                    {
                        CityName = "ABC",
                        CountryId = 1,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    }
                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();

            //Act
            List<MyPropertyModel> result = await myPropertyService.GetPropertyList(_tenant, userId, loanApplicationId, borrowerPropertyId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DoYouHaveFirstMortgage_ShouldReturnFirstMortgage()
        {
            //Arrange
            const string testMethodName = nameof(DoYouHaveFirstMortgage_ShouldReturnFirstMortgage);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            const int borrowerPropertyId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int propertyInfoId = 1;

            var borrowerProperty = new LoanApplicationDb.Entity.Models.BorrowerProperty()
            {
                Id = borrowerPropertyId,
                TenantId = _tenant.Id,
                BorrowerId = borrowerId,
                TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplicationId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                    {
                        Id = loanApplicationId,
                        UserId = userId,
                        TenantId = _tenant.Id,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                },
                PropertyInfo = new PropertyInfo()
                {
                    Id = propertyInfoId,
                    HasFirstMortgage = true,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                    PropertyTaxEscrows = new HashSet<PropertyTaxEscrow>()
                    {
                        new PropertyTaxEscrow()
                        {
                            EscrowEntityTypeId = (int)EscrowEntityTypeEnum.PropertyTax,
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                        },
                        new PropertyTaxEscrow()
                        {
                            EscrowEntityTypeId = (int)EscrowEntityTypeEnum.HomeOwnerInsurance,
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                        }
                    }
                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();

            //Act
            HasFirstMortgageModel result = await myPropertyService.DoYouHaveFirstMortgage(_tenant, userId, loanApplicationId, borrowerPropertyId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DoYouHaveSecondMortgage_ShouldReturnSecondMortgage()
        {
            //Arrange
            const string testMethodName = nameof(DoYouHaveSecondMortgage_ShouldReturnSecondMortgage);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            const int borrowerPropertyId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int propertyInfoId = 1;

            var borrowerProperty = new LoanApplicationDb.Entity.Models.BorrowerProperty()
            {
                Id = borrowerPropertyId,
                TenantId = _tenant.Id,
                BorrowerId = borrowerId,
                TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplicationId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                    {
                        Id = loanApplicationId,
                        UserId = userId,
                        TenantId = _tenant.Id,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                },
                PropertyInfo = new PropertyInfo()
                {
                    Id = propertyInfoId,
                    HasFirstMortgage = true,
                    HasSecondMortgage = true,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();

            //Act
            bool? result = await myPropertyService.DoYouHaveSecondMortgage(_tenant, userId, loanApplicationId, borrowerPropertyId);

            //Assert
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task AddOrUpdateHasSecondMortgage_ShouldUpdateRecord() //Always Update
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateHasSecondMortgage_ShouldUpdateRecord));
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            const int borrowerPropertyId = 1;
            const int propertyInfoId = 1;
            const int borrowerId = 1;

            var borrowerProperty = new BorrowerProperty
            {
                Id = borrowerPropertyId,
                TenantId = loanApplication.TenantId,
                TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplication.Id,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                },

                PropertyInfo = new PropertyInfo()
                {
                    Id = propertyInfoId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();

            //Act
            HasSecondMortgageModel hasSecondMortgageModel = new HasSecondMortgageModel()
            {
                Id = borrowerPropertyId,
                LoanApplicationId = loanApplication.Id,
                HasSecondMortgage = true,
                State = "Abc"
            };

           int result= await myPropertyService.AddOrUpdateHasSecondMortgage(_tenant, userId, hasSecondMortgageModel);

            //Assert
            Assert.IsType<int>(result);

        }


        [Fact]
        public async Task AddOrUpdateHasFirstMortgage_ShouldUpdateRecord() //Always Update
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateHasFirstMortgage_ShouldUpdateRecord));
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            const int borrowerPropertyId = 1;
            const int propertyInfoId = 1;
            const int borrowerId = 1;

            var borrowerProperty = new BorrowerProperty
            {
                Id = borrowerPropertyId,
                TenantId = loanApplication.TenantId,
                TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplication.Id,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                },

                PropertyInfo = new PropertyInfo()
                {
                    Id = propertyInfoId,
                    HasFirstMortgage = true,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    PropertyTaxEscrows = new HashSet<PropertyTaxEscrow>()
                    {
                        new PropertyTaxEscrow()
                        {
                            Id = 1,
                            EscrowEntityTypeId = (int)EscrowEntityTypeEnum.PropertyTax,
                            //EscrowEntityTypeId = (int)EscrowEntityTypeEnum.
                        }
                    }
                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();

            //Act
            HasFirstMortgageModel hasFirstMortgageModel = new HasFirstMortgageModel()
            {
                Id = borrowerPropertyId,
                LoanApplicationId = loanApplication.Id,
                HasFirstMortgage = false,
                PropertyTax = 200,
                HomeOwnerInsurance = 300,
                FloodInsurance = 20,
                State = "Abc"
            };

            int result = await myPropertyService.AddOrUpdateHasFirstMortgage(_tenant, userId, hasFirstMortgageModel);

            //Assert
            Assert.IsType<int>(result);

        }

        //[Fact]
        //public async Task GetPropertyValue_ShouldReturnPropertyValue()
        //{
        //    //Arrange
        //    const string testMethodName = nameof(GetPropertyValue_ShouldReturnPropertyValue);
        //    var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
        //    var myPropertyService = new MyPropertyService(unitOfWork, null);

        //    const int userId = 1;
        //    const int borrowerPropertyId = 1;
        //    const int borrowerId = 1;
        //    const int loanApplicationId = 1;
        //    const int propertyInfoId = 1;
        //    const bool intentToSellPriorToPurchase = false;
        //    const decimal hoaDues = 50;
        //    const decimal PropertyValue = 2007;

        //    var borrowerProperty = new LoanApplicationDb.Entity.Models.BorrowerProperty()
        //    {
        //        Id = borrowerPropertyId,
        //        TenantId = _tenant.Id,
        //        BorrowerId = borrowerId,
        //        TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,

        //        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
        //        Borrower = new Borrower()
        //        {
        //            Id = borrowerId,
        //            LoanApplicationId = loanApplicationId,
        //            TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
        //            LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
        //            {
        //                Id = loanApplicationId,
        //                UserId = userId,
        //                TenantId = _tenant.Id,
        //                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
        //            }
        //        },
        //        PropertyInfo = new PropertyInfo()
        //        {
        //            Id = propertyInfoId,
        //            PropertyValue = PropertyValue,
        //            HoaDues = hoaDues,
        //            IntentToSellPriorToPurchase = intentToSellPriorToPurchase,
        //            TrackingState = TrackableEntities.Common.Core.TrackingState.Added
        //        }
        //    };

        //    unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
        //    await unitOfWork.SaveChangesAsync();

        //    //Act
        //    CurrentResidenceModel result = await myPropertyService.GetPropertyValue(_tenant, userId, borrowerPropertyId, loanApplicationId);

        //    //Assert
        //    Assert.NotNull(result);
        //}

        //[Fact]
        //public async Task GetFirstMortgageValue_ShouldReturnFirstMortgageValue()
        //{
        //    //Arrange
        //    const string testMethodName = nameof(GetFirstMortgageValue_ShouldReturnFirstMortgageValue);
        //    var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
        //    var myPropertyService = new MyPropertyService(unitOfWork, null);

        //    const int userId = 1;
        //    const int mortgageOnPropertyId = 1;
        //    const decimal monthlyPayment = 1000;
        //    const decimal mortgageBalance = 500;

        //    var mortgageOnProperty = new MortgageOnProperty()
        //    {
        //        Id = 1,
        //        TenantId = _tenant.Id,
        //        MonthlyPayment = monthlyPayment,
        //        MortgageBalance = mortgageBalance
        //    };

        //    unitOfWork.Repository<LoanApplicationDb.Entity.Models.MortgageOnProperty>().Insert(mortgageOnProperty);
        //    await unitOfWork.SaveChangesAsync();

        //    //Act
        //    FirstMortgageModel result = await myPropertyService.GetFirstMortgageValue(_tenant, userId, mortgageOnPropertyId);

        //    //Assert
        //    Assert.NotNull(result);
        //}

        [Fact]
        public async Task GetBorrowerPrimaryPropertyType_ShouldReturnPBorrowerPrimaryPropertyType()
        {
            //Arrange
            const string testMethodName = nameof(GetBorrowerPrimaryPropertyType_ShouldReturnPBorrowerPrimaryPropertyType);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            const int borrowerPropertyId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int propertyInfoId = 1;
            const int propertyTypeId = 1;
            const decimal rentalIncome = 50;

            var borrowerProperty = new LoanApplicationDb.Entity.Models.BorrowerProperty()
            {
                Id = borrowerPropertyId,
                TenantId = _tenant.Id,
                TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplicationId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                    {
                        Id = loanApplicationId,
                        UserId = userId,
                        TenantId = _tenant.Id,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                },
                PropertyInfo = new PropertyInfo()
                {
                    Id = propertyInfoId,
                    PropertyTypeId = propertyTypeId,
                    RentalIncome = rentalIncome,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();

            //Act
            BorrowerPropertyResponseModel result = await myPropertyService.GetBorrowerPrimaryPropertyType(_tenant, userId, borrowerPropertyId, loanApplicationId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddOrUpdatePropertyValue_ShouldUpdateRecord() //Always Update
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdatePropertyValue_ShouldUpdateRecord));
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            const int borrowerPropertyId = 1;
            const int propertyInfoId = 1;
            const int borrowerId = 1;

            var borrowerProperty = new BorrowerProperty
            {
                Id = borrowerPropertyId,
                TenantId = loanApplication.TenantId,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplication.Id,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                },
                PropertyInfo = new PropertyInfo()
                {
                    Id = propertyInfoId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();


            //Act
            const decimal updatedPropertyValue = 200;
            const decimal updatedOwnersDue = 200;
            const bool updatedIsSelling = false;

            CurrentResidenceModel currentResidenceModel = new CurrentResidenceModel()
            {
                Id = 1, 
                BorrowerId = 1,
                LoanApplicationId = loanApplication.Id,
                PropertyValue = updatedPropertyValue,
                OwnersDue = updatedOwnersDue,
                IsSelling= updatedIsSelling
            };

            await myPropertyService.AddOrUpdatePropertyValue(_tenant, userId, currentResidenceModel);


            //Assert
            var record = unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Query(x => x.Id == borrowerPropertyId).Single();

            Assert.NotNull(record);

        }

        [Fact]
        public async Task AddOrUpdatePrimaryPropertyType_ShouldAddRecord() //Add
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdatePrimaryPropertyType_ShouldAddRecord));
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            const int borrowerPropertyId = 1;
            const int propertyInfoId = 1;
            const int borrowerId = 1;

            var borrower = new Borrower()
            {
                Id = borrowerId,
                LoanApplicationId = loanApplication.Id,
                TenantId = _tenant.Id,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                BorrowerResidences = new HashSet<BorrowerResidence>()
                {
                    new BorrowerResidence()
                    {
                        TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                        LoanAddress = new AddressInfo()
                        {
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                        }
                    }
                }
            };

            var borrowerProperty = new LoanApplicationDb.Entity.Models.BorrowerProperty()
            {
                Id = borrowerPropertyId,
                TenantId = _tenant.Id,
                TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplication.Id,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                    {
                        Id = loanApplication.Id,
                        UserId = userId,
                        TenantId = _tenant.Id,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                },
                PropertyInfo = new PropertyInfo()
                {
                    Id = propertyInfoId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                }
            };
            

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();

            //Act
            const decimal rentalIncome = 2000;
            BorrowerPropertyRequestModel borrowerPropertyRequestModel = new BorrowerPropertyRequestModel()
            {
                //Id = borrowerPropertyId,
                LoanApplicationId = loanApplication.Id,
                BorrowerId = borrowerId,
                PropertyTypeId = (int)PropertyTypes.Triplex3Unit,
                RentalIncome = rentalIncome,
                State = "Abc"
            };

            int result = await myPropertyService.AddOrUpdatePrimaryPropertyType(_tenant, userId, borrowerPropertyRequestModel);

            //Assert
            const int expectedBorrowerPropertyId = 2;

            Assert.IsType<int>(result);
            Assert.Equal<int>(expectedBorrowerPropertyId, result);

        }

        [Fact]
        public async Task AddOrUpdatePrimaryPropertyType_ShouldUpdateRecord() //Update
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdatePrimaryPropertyType_ShouldUpdateRecord));
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            const int borrowerPropertyId = 1;
            const int propertyInfoId = 1;
            const int borrowerId = 1;

            var borrower = new Borrower()
            {
                Id = borrowerId,
                LoanApplicationId = loanApplication.Id,
                TenantId = _tenant.Id,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                BorrowerResidences = new HashSet<BorrowerResidence>()
                {
                    new BorrowerResidence()
                    {
                        TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                        LoanAddress = new AddressInfo()
                        {
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                        }
                    }
                }
            };

            var borrowerProperty = new LoanApplicationDb.Entity.Models.BorrowerProperty()
            {
                Id = borrowerPropertyId,
                TenantId = _tenant.Id,
                TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplication.Id,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                    {
                        Id = loanApplication.Id,
                        UserId = userId,
                        TenantId = _tenant.Id,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                },
                PropertyInfo = new PropertyInfo()
                {
                    Id = propertyInfoId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                }
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();

            //Act
            const decimal rentalIncome = 2000;
            BorrowerPropertyRequestModel borrowerPropertyRequestModel = new BorrowerPropertyRequestModel()
            {
                Id = borrowerPropertyId,
                LoanApplicationId = loanApplication.Id,
                BorrowerId = borrowerId,
                PropertyTypeId = (int)PropertyTypes.Triplex3Unit,
                RentalIncome = rentalIncome,
                State = "Abc"
            };

            int result = await myPropertyService.AddOrUpdatePrimaryPropertyType(_tenant, userId, borrowerPropertyRequestModel);

            //Assert
            const int expectedBorrowerPropertyId = 1;

            Assert.IsType<int>(result);
            Assert.Equal<int>(expectedBorrowerPropertyId, result);

        }

        //[Fact]
        //public async Task AddOrUpdateFirstMortgageValue_ShouldAddRecord() //Add
        //{
        //    //Arrange
        //    var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateFirstMortgageValue_ShouldAddRecord));
        //    var myPropertyService = new MyPropertyService(unitOfWork, null);

        //    const int userId = 1;
        //    var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
        //    loanApplication.Id = userId;
        //    loanApplication.TenantId = _tenant.Id;
        //    loanApplication.UserId = userId;
        //    loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

        //    unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            
        //    await unitOfWork.SaveChangesAsync();

        //    //Act
        //    const decimal firstMortgagePayment = 5000;
        //    const decimal unpaidFirstMortgagePayment = 2000;
        //    const bool isHeloc = false;
        //    FirstMortgageModel firstMortgageModel = new FirstMortgageModel()
        //    {
        //        Id = null,
        //        LoanApplicationId = loanApplication.Id,
        //        FirstMortgagePayment = firstMortgagePayment,
        //        UnpaidFirstMortgagePayment = unpaidFirstMortgagePayment,
        //        IsHeloc = isHeloc
        //    };

        //    int result = await myPropertyService.AddOrUpdateFirstMortgageValue(_tenant, userId, firstMortgageModel);

        //    //Assert
        //    const int expectedMortgageOnPropertyId = 1;

        //    Assert.IsType<int>(result);
        //    Assert.Equal<int>(expectedMortgageOnPropertyId, result);

        //}

        [Fact]
        public async Task AddOrUpdateFirstMortgageValue_ShouldUpdateRecord() //Update
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateFirstMortgageValue_ShouldUpdateRecord));
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            const int mortgageOnPropertyId = 1;

            var borrowerProperty = new BorrowerProperty()
            {
                Id = 1,
                TenantId = _tenant.Id,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    Id = 1,
                    LoanApplicationId = loanApplication.Id,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                },
                PropertyInfo = new PropertyInfo()
                {
                    Id = 1,
                    TenantId = _tenant.Id,
                    PropertyTypeId = 1,
                    PropertyUsageId = 1,
                    AddressInfoId = 1,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                    PropertyTaxEscrows = new HashSet<PropertyTaxEscrow>() 
                    {
                        new PropertyTaxEscrow()
                        {
                            Id = 1,
                            TenantId = _tenant.Id,
                            PaidById = 1,
                            PropertyDetailId = 1,
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                        }
                    },
                    MortgageOnProperties = new HashSet<MortgageOnProperty>() 
                    {
                        new MortgageOnProperty()
                        {
                            Id = 1,
                            TenantId = _tenant.Id,
                            AmortizationTypeId = 1,
                            LoanTypeId = 1,
                            MortgageTypeId = 1,
                            ProductFamilyId = 1,
                            PropertyOwnId = 1,
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                        }
                    }
                }
            };

            //var mortgageOnProperty = new MortgageOnProperty()
            //{
            //    Id = mortgageOnPropertyId,
            //    TenantId = _tenant.Id,
            //    TrackingState = TrackableEntities.Common.Core.TrackingState.Added                
            //};

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            //unitOfWork.Repository<LoanApplicationDb.Entity.Models.MortgageOnProperty>().Insert(mortgageOnProperty);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);

            await unitOfWork.SaveChangesAsync();

            //Act
            //Act
            const decimal firstMortgagePayment = 5000;
            const decimal unpaidFirstMortgagePayment = 2000;
            const decimal propertyTax = 1000;
            const bool isHeloc = false;
            FirstMortgageModel firstMortgageModel = new FirstMortgageModel()
            {
                Id = 1,
                LoanApplicationId = loanApplication.Id,
                FirstMortgagePayment = firstMortgagePayment,
                UnpaidFirstMortgagePayment = unpaidFirstMortgagePayment,
                IsHeloc = isHeloc,
                PropertyTax = propertyTax,
                PropertyTaxesIncludeinPayment = true,
                HomeOwnerInsurance = 20,
                HomeOwnerInsuranceIncludeinPayment = true,
                FloodInsurance = 100,
                FloodInsuranceIncludeinPayment =true,
                PaidAtClosing = true
            };

            int result = await myPropertyService.AddOrUpdateFirstMortgageValue(_tenant, userId, firstMortgageModel);

            //Assert
            const int expectedBorrowerPropertyId = 1;

            Assert.IsType<int>(result);
            Assert.Equal<int>(expectedBorrowerPropertyId, result);

        }

        [Fact]
        public async Task AddOrUpdateAdditionalPropertyType_ShouldAddRecord() //Add
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateAdditionalPropertyType_ShouldAddRecord));
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            const int borrowerId = 1;
            
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            
            await unitOfWork.SaveChangesAsync();


            //Act
            BorrowerAdditionalPropertyRequestModel borrowerAdditionalPropertyRequestModel = new BorrowerAdditionalPropertyRequestModel()
            {
                Id = null,
                BorrowerId = borrowerId,
                LoanApplicationId = loanApplication.Id,
                PropertyTypeId = 1
            };

            var result = await myPropertyService.AddOrUpdateAdditionalPropertyType(_tenant, userId, borrowerAdditionalPropertyRequestModel);


            //Assert
            const int expectedBorowerPropertyId = 1;
            Assert.IsType<int>(result);
            Assert.Equal<int>(expectedBorowerPropertyId, result);
        }

        [Fact]
        public async Task AddOrUpdateAdditionalPropertyType_ShouldUpdateRecord() //Update
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateAdditionalPropertyType_ShouldUpdateRecord));
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            const int borrowerPropertyId = 1;
            const int propertyInfoId = 1;
            const int borrowerId = 1;

            var borrowerProperty = new BorrowerProperty
            {
                Id = borrowerPropertyId,
                TenantId = loanApplication.TenantId,
                TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplication.Id,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                },

                PropertyInfo = new PropertyInfo()
                {
                    Id = propertyInfoId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();


            //Act
            BorrowerAdditionalPropertyRequestModel borrowerAdditionalPropertyRequestModel = new BorrowerAdditionalPropertyRequestModel()
            {
                Id = 1,
                BorrowerId = borrowerId,
                LoanApplicationId = loanApplication.Id,
                PropertyTypeId = 1
            };

            var result = await myPropertyService.AddOrUpdateAdditionalPropertyType(_tenant, userId, borrowerAdditionalPropertyRequestModel);


            //Assert
            const int expectedBorowerPropertyId = 1;
            Assert.IsType<int>(result);
            Assert.Equal<int>(expectedBorowerPropertyId, result);

        }

        [Fact]
        public async Task GetFinalScreenReview_ShouldReturnPropertyList()
        {
            //Arrange
            const string testMethodName = nameof(GetFinalScreenReview_ShouldReturnPropertyList);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            const int borrowerPropertyId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int propertyInfoId = 1;
            const int propertyTypeId = 5;

            const int ownTypeId = 1;
            const int loanContactId = 1;

            var borrowerProperty = new LoanApplicationDb.Entity.Models.BorrowerProperty()
            {
                Id = borrowerPropertyId,
                TenantId = _tenant.Id,
                BorrowerId = borrowerId,
                TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplicationId,
                    OwnTypeId = ownTypeId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                    {
                        Id = loanApplicationId,
                        UserId = userId,
                        TenantId = _tenant.Id,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    },
                    LoanContact_LoanContactId = new LoanContact()
                    {
                        Id = loanContactId,
                        FirstName = "ABC",
                        LastName = "ABC",
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    }
                },
                PropertyInfo = new PropertyInfo()
                {
                    Id = propertyInfoId,
                    PropertyTypeId = propertyTypeId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    PropertyType = new PropertyType()
                    {
                        Id = 1,
                        Name = "ABC",
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    },
                    AddressInfo = new AddressInfo()
                    {
                        CityName = "ABC",
                        CountryId = 1,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    }
                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();

            //Act
            List<MyPropertyModel> result = await myPropertyService.GetFinalScreenReview(_tenant, userId, loanApplicationId, borrowerId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetFirstMortgageValue_ShouldReturnFirstMortgageValue()
        {
            //Arrange
            const string testMethodName = nameof(GetFirstMortgageValue_ShouldReturnFirstMortgageValue);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            const int borrowerPropertyId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int propertyInfoId = 1;
            const int propertyTypeId = 5;
            const int ownTypeId = 1;

            var borrowerProperty = new LoanApplicationDb.Entity.Models.BorrowerProperty()
            {
                Id = borrowerPropertyId,
                TenantId = _tenant.Id,
                BorrowerId = borrowerId,
                TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplicationId,
                    OwnTypeId = ownTypeId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                    {
                        Id = loanApplicationId,
                        UserId = userId,
                        TenantId = _tenant.Id,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                },
                PropertyInfo = new PropertyInfo()
                {
                    Id = propertyInfoId,
                    PropertyTypeId = propertyTypeId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    PropertyTaxEscrows = new HashSet<PropertyTaxEscrow>()
                    {
                        new PropertyTaxEscrow()
                        {
                            EscrowEntityTypeId = (int)EscrowEntityTypeEnum.HomeOwnerInsurance,
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                        }
                    },
                    MortgageOnProperties = new HashSet<MortgageOnProperty>()
                    {
                        new MortgageOnProperty()
                        {
                            MortgageTypeId = (int)MortgageTypeEnum.FirstMortgage,
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                        }
                    }
                   
                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();

            //Act
            FirstMortgageModel result = await myPropertyService.GetFirstMortgageValue(_tenant, userId, borrowerPropertyId , loanApplicationId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetSecondMortgageValue_ShouldReturnSecondMortgageValue()
        {
            //Arrange
            const string testMethodName = nameof(GetSecondMortgageValue_ShouldReturnSecondMortgageValue);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            const int borrowerPropertyId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int propertyInfoId = 1;
            const int propertyTypeId = 5;
            const int ownTypeId = 1;

            var borrowerProperty = new LoanApplicationDb.Entity.Models.BorrowerProperty()
            {
                Id = borrowerPropertyId,
                TenantId = _tenant.Id,
                BorrowerId = borrowerId,
                TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplicationId,
                    OwnTypeId = ownTypeId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                    {
                        Id = loanApplicationId,
                        UserId = userId,
                        TenantId = _tenant.Id,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                },
                PropertyInfo = new PropertyInfo()
                {
                    Id = propertyInfoId,
                    PropertyTypeId = propertyTypeId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    PropertyTaxEscrows = new HashSet<PropertyTaxEscrow>()
                    {
                        new PropertyTaxEscrow()
                        {
                            EscrowEntityTypeId = (int)EscrowEntityTypeEnum.HomeOwnerInsurance,
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                        }
                    },
                    MortgageOnProperties = new HashSet<MortgageOnProperty>()
                    {
                        new MortgageOnProperty()
                        {
                            MortgageTypeId = (int)MortgageTypeEnum.SecondMortgage,
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                        }
                    }

                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();

            //Act
            SecondMortgageModel result = await myPropertyService.GetSecondMortgageValue(_tenant, userId, borrowerPropertyId, loanApplicationId);

            //Assert
            Assert.NotNull(result);
           
        }

        [Fact]
        public async Task DoYouOwnAdditionalPropertyTest200ok()
        {

            // Arrange
            Mock<IMyPropertyService> mypropertyServiceMock = new Mock<IMyPropertyService>();

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
            var controller = new MyPropertyController(mypropertyServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;
            var results = await controller.DoYouOwnAdditionalProperty(1, 1);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task AddOrUpdateSecondMortgageValue_ShouldAddRecord() //Add
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>("AddOrUpdateSecondMortgageValue_ShouldAddRecord11");
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            const int borrowerPropertyId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int propertyInfoId = 1;
            const int propertyTypeId = 5;
            const int ownTypeId = 1;

            var borrowerProperty = new LoanApplicationDb.Entity.Models.BorrowerProperty()
            {
                Id = borrowerPropertyId,
                TenantId = _tenant.Id,
                BorrowerId = borrowerId,
                TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplicationId,
                    OwnTypeId = ownTypeId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                    {
                        Id = loanApplicationId,
                        UserId = userId,
                        TenantId = _tenant.Id,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                },
                PropertyInfo = new PropertyInfo()
                {
                    Id = propertyInfoId,
                    PropertyTypeId = propertyTypeId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    PropertyTaxEscrows = new HashSet<PropertyTaxEscrow>()
                    {
                        new PropertyTaxEscrow()
                        {
                            EscrowEntityTypeId = (int)EscrowEntityTypeEnum.HomeOwnerInsurance,
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                        }
                    },
                    MortgageOnProperties = new HashSet<MortgageOnProperty>()
                    {
                        new MortgageOnProperty()
                        {
                            //MortgageTypeId = (int)MortgageTypeEnum.SecondMortgage,
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                        }
                    }

                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();


            //Act
            SecondMortgageModel secondMortgageModel = new SecondMortgageModel()
            {
                Id = 1,
                LoanApplicationId = loanApplicationId,
                SecondMortgagePayment = 200,
                UnpaidSecondMortgagePayment = 100,
                PaidAtClosing = true,
                IsHeloc = true
            };

            var result = await myPropertyService.AddOrUpdateSecondMortgageValue(_tenant, userId, secondMortgageModel);


            //Assert
            const decimal expectedSecondMortgagePayment = 200;

            var record = await unitOfWork.Repository<LoanApplicationDb.Entity.Models.MortgageOnProperty>().Query(x => x.Id == 2).SingleOrDefaultAsync();

            Assert.Equal(expectedSecondMortgagePayment, record.MonthlyPayment);
            
        }

        [Fact]
        public async Task AddOrUpdateSecondMortgageValue_ShouldUpdateRecord() //Update
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateSecondMortgageValue_ShouldAddRecord));
            var myPropertyService = new MyPropertyService(unitOfWork, null);

            const int userId = 1;
            const int borrowerPropertyId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int propertyInfoId = 1;
            const int propertyTypeId = 5;
            const int ownTypeId = 1;

            var borrowerProperty = new LoanApplicationDb.Entity.Models.BorrowerProperty()
            {
                Id = borrowerPropertyId,
                TenantId = _tenant.Id,
                BorrowerId = borrowerId,
                TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,

                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplicationId,
                    OwnTypeId = ownTypeId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                    {
                        Id = loanApplicationId,
                        UserId = userId,
                        TenantId = _tenant.Id,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                    }
                },
                PropertyInfo = new PropertyInfo()
                {
                    Id = propertyInfoId,
                    PropertyTypeId = propertyTypeId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    PropertyTaxEscrows = new HashSet<PropertyTaxEscrow>()
                    {
                        new PropertyTaxEscrow()
                        {
                            EscrowEntityTypeId = (int)EscrowEntityTypeEnum.HomeOwnerInsurance,
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                        }
                    },
                    MortgageOnProperties = new HashSet<MortgageOnProperty>()
                    {
                        new MortgageOnProperty()
                        {
                            MortgageTypeId = (int)MortgageTypeEnum.SecondMortgage,
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                        }
                    }

                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Insert(borrowerProperty);
            await unitOfWork.SaveChangesAsync();


            //Act
            SecondMortgageModel secondMortgageModel = new SecondMortgageModel()
            {
                Id = 1,
                LoanApplicationId = loanApplicationId,
                SecondMortgagePayment = 200,
                UnpaidSecondMortgagePayment = 100,
                IsHeloc = true,
                PaidAtClosing = true,
                HelocCreditLimit = 100
            };

            var result = await myPropertyService.AddOrUpdateSecondMortgageValue(_tenant, userId, secondMortgageModel);


            //Assert
            const decimal expectedSecondMortgagePayment = 200;
            
            var record = await unitOfWork.Repository<LoanApplicationDb.Entity.Models.MortgageOnProperty>().Query(x => x.Id == 1).SingleOrDefaultAsync();
            
            Assert.Equal(expectedSecondMortgagePayment, record.MonthlyPayment);
        }
    }
}
 