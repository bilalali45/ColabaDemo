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
    public partial class FinishingUpServiceTests
    {

        private readonly TenantModel _tenant;
        public FinishingUpServiceTests()
        {
            _tenant = ObjectHelper.GetTenantModel(1);
        }


        [Fact]
        public async Task GetSecondaryAddress_ShouldReturnSecondaryAddress()
        {
            //Arrange
            const string testMethodName = nameof(GetSecondaryAddress_ShouldReturnSecondaryAddress);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var finishingUpService = new FinishingUpService(unitOfWork, null);

            const int userId = 1;
            const int borrowerResidenceId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int ownershipTypeId = 1;

            var borrowerResidence = new BorrowerResidence()
            {
                Id = borrowerResidenceId,
                BorrowerId = borrowerId,
                TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,
                TenantId = _tenant.Id,
                OwnershipTypeId = ownershipTypeId,
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
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    }
                },
                LoanAddress = new AddressInfo()
                {
                    Id = 1,
                    CountryId = 1,
                    StateId = 1,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                }
            };



            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerResidence>().Insert(borrowerResidence);
            await unitOfWork.SaveChangesAsync();

            //Act
            var result = await finishingUpService.GetSecondaryAddress(_tenant, userId, borrowerResidenceId, loanApplicationId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetBorrowerCitizenship_ShouldReturnBorrowerCitizenship()
        {
            //Arrange
            const string testMethodName = nameof(GetBorrowerCitizenship_ShouldReturnBorrowerCitizenship);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var finishingUpService = new FinishingUpService(unitOfWork, null);

            const int userId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int residencyStateId = 1;
            const int residencyTypeId = 1;


            var borrower = new Borrower()
            {
                Id = borrowerId,
                LoanApplicationId = loanApplicationId,
                TenantId = _tenant.Id,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    Id = loanApplicationId,
                    UserId = userId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                },
                LoanContact_LoanContactId = new LoanContact()
                {
                    ResidencyStateId = residencyStateId,
                    ResidencyTypeId = residencyTypeId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                }
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);
            await unitOfWork.SaveChangesAsync();

            //Act
            var result = await finishingUpService.GetBorrowerCitizenship(_tenant, userId, borrowerId, loanApplicationId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetResidenceDetails_ShouldReturnResidenceDetails()
        {
            //Arrange
            const string testMethodName = nameof(GetResidenceDetails_ShouldReturnResidenceDetails);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var finishingUpService = new FinishingUpService(unitOfWork, null);

            const int userId = 1;
            const int borrowerResidenceId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;

            var borrowerResidence = new BorrowerResidence()
            {
                Id = borrowerResidenceId,
                BorrowerId = borrowerId,
                TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,
                TenantId = _tenant.Id,
                FromDate = new DateTime(),
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
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    }
                },
                LoanAddress = new AddressInfo()
                {
                    Id = 1,
                    CountryId = 1,
                    StateId = 1,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                }
            };



            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerResidence>().Insert(borrowerResidence);
            await unitOfWork.SaveChangesAsync();

            //Act
            var result = await finishingUpService.GetResidenceDetails(_tenant, userId, borrowerId, loanApplicationId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetResidenceHistory_ShouldReturnGetResidenceHistory()
        {
            //Arrange
            const string testMethodName = nameof(GetResidenceHistory_ShouldReturnGetResidenceHistory);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var finishingUpService = new FinishingUpService(unitOfWork, null);

            const int userId = 1;
            const int borrowerResidenceId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int id = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
            {
                Id = loanApplicationId,
                UserId = userId,
                TenantId = _tenant.Id,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrowers = new HashSet<Borrower>()
                {
                    new Borrower
                    {
                        Id = borrowerId,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                        LoanContact_LoanContactId = new LoanContact()
                        {
                            Id = id,
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                        },
                        BorrowerResidences = new HashSet<BorrowerResidence>()
                        {
                            new BorrowerResidence
                            {
                                Id = borrowerResidenceId,
                                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                            }
                        }
                    }
                }
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            await unitOfWork.SaveChangesAsync();

            //Act
            var result = await finishingUpService.GetResidenceHistory(_tenant, userId, loanApplicationId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddOrUpdateBorrowerCitizenship_ShouldUpdateRecord() //Always Update
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateBorrowerCitizenship_ShouldUpdateRecord));
            var finishingUpService = new FinishingUpService(unitOfWork, null);

            const int userId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            const int id = 1;
            const int borrowerId = 1;


            var borrower = new Borrower()
            {
                Id = borrowerId,
                TenantId = _tenant.Id,
                LoanApplicationId = loanApplication.Id,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                LoanContact_LoanContactId = new LoanContact()
                {
                    Id = id,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                }
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);
            await unitOfWork.SaveChangesAsync();


            //Act
            const int residencyStateId = 1;
            const int residencyTypeId = 1;

            BorrowerCitizenshipRequestModel borrowerCitizenshipRequestModel = new BorrowerCitizenshipRequestModel()
            {
                BorrowerId = borrowerId,
                ResidencyStateId = residencyStateId,
                ResidencyTypeId = residencyTypeId,
                LoanApplicationId = loanApplication.Id,
            };

            var result = await finishingUpService.AddOrUpdateBorrowerCitizenship(_tenant, userId, borrowerCitizenshipRequestModel);


            //Assert
            const int expectedId = 1;

            Assert.IsType<int>(result);
            Assert.Equal<int>(expectedId, result);

        }

        [Fact]
        public async Task AddOrUpdateSecondaryAddress_ShouldAddRecord()
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateSecondaryAddress_ShouldAddRecord));
            var finishingUpService = new FinishingUpService(unitOfWork, null);

            const int userId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            await unitOfWork.SaveChangesAsync();


            //Act
            const int countryId = 1;
            const int stateId = 1;

            BorrowerSecondaryAddressRequestModel borrowerSecondaryAddressRequestModel = new BorrowerSecondaryAddressRequestModel()
            {
                BorrowerResidenceId = null,
                BorrowerId = 1,
                LoanApplicationId = loanApplication.Id,
                HousingStatusId = 1,
                AddressModel = new GenericAddressModel
                {
                    CountryId = countryId,
                    StateId = stateId,
                }
            };

            var result = await finishingUpService.AddOrUpdateSecondaryAddress(_tenant, userId, borrowerSecondaryAddressRequestModel);


            //Assert
            const int expectedBorrowerResidenceId = 1;

            Assert.IsType<int>(result);
            Assert.Equal<int>(expectedBorrowerResidenceId, result);

        }

        [Fact]
        public async Task AddOrUpdateSecondaryAddress_ShouldUpdateRecord()
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateSecondaryAddress_ShouldUpdateRecord));
            var finishingUpService = new FinishingUpService(unitOfWork, null);

            const int userId = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication();
            loanApplication.Id = userId;
            loanApplication.TenantId = _tenant.Id;
            loanApplication.UserId = userId;
            loanApplication.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;

            const int borrowerId = 1;
            const int id = 1;
            const int borrowerResidenceId = 1;


            var borrowerResidence = new BorrowerResidence()
            {
                Id = borrowerResidenceId,
                BorrowerId = 1,
                TenantId = _tenant.Id,
                TypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Secondary,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                Borrower = new Borrower()
                {
                    Id = borrowerId,
                    LoanApplicationId = loanApplication.Id,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                },
                LoanAddress = new AddressInfo()
                {
                    Id = id,
                    StateId = 1,
                    CountryId = 1,
                    CityId = 1,
                    CountyId = 1,
                    TenantId = _tenant.Id,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                }
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.BorrowerResidence>().Insert(borrowerResidence);
            await unitOfWork.SaveChangesAsync();


            //Act
            const int countryId = 1;
            const int stateId = 1;

            BorrowerSecondaryAddressRequestModel borrowerSecondaryAddressRequestModel = new BorrowerSecondaryAddressRequestModel()
            {
                BorrowerResidenceId = 1,
                BorrowerId = 1,
                LoanApplicationId = loanApplication.Id,
                HousingStatusId = 1,
                AddressModel = new GenericAddressModel
                {
                    CountryId = countryId,
                    StateId = stateId,
                }
            };

            var result = await finishingUpService.AddOrUpdateSecondaryAddress(_tenant, userId, borrowerSecondaryAddressRequestModel);


            //Assert
            const int expectedBorrowerResidenceId = 1;

            Assert.IsType<int>(result);
            Assert.Equal<int>(expectedBorrowerResidenceId, result);

        }

        [Fact]
        public async Task AddOrUpdateDependentInfo_ShouldUpdateRecord() //Update
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateDependentInfo_ShouldUpdateRecord));
            var finishingUpService = new FinishingUpService(unitOfWork, null);

            const int userId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;


            var borrower = new LoanApplicationDb.Entity.Models.Borrower()
            {
                Id = borrowerId,
                TenantId = _tenant.Id,
                LoanApplicationId = loanApplicationId,
                OwnTypeId = 1,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    Id = loanApplicationId,
                    TenantId = _tenant.Id,
                    UserId = userId,
                    TrackingState = TrackingState.Added
                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);
            await unitOfWork.SaveChangesAsync();


            //Act
            DependentModel dependentModel = new DependentModel()
            {
                BorrowerId = borrowerId,
                LoanApplicationId = loanApplicationId,
            };

            var result = await finishingUpService.AddOrUpdateDependentinfo(_tenant, userId, dependentModel);


            //Assert
            var record = await unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Query(x => x.Id == 1).SingleOrDefaultAsync();

            Assert.NotNull(record);
        }

        [Fact]
        public async Task GetDependentInfo_ShouldReturnPropertyList()
        {
            //Arrange
            const string testMethodName = nameof(GetDependentInfo_ShouldReturnPropertyList);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var finishingUpService = new FinishingUpService(unitOfWork, null);

            const int userId = 18;
            const int borrowerId = 2298;
            const int loanApplicationId = 2175;

            var borrower = new LoanApplicationDb.Entity.Models.Borrower()
            {
                Id = borrowerId,
                TenantId = _tenant.Id,
                LoanApplicationId = loanApplicationId,
                DependentAge = "22,33,44",
                OwnTypeId = 1,
                NoOfDependent = 2,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    Id = loanApplicationId,
                    UserId = userId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);
            await unitOfWork.SaveChangesAsync();

            //Act
            DependentModel result = await finishingUpService.GetDependentinfo(_tenant, userId, loanApplicationId, borrowerId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetCoborrowerResidence_ShouldReturnCoborrowerResidence()
        {
            //Arrange
            const string testMethodName = nameof(GetCoborrowerResidence_ShouldReturnCoborrowerResidence);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var finishingUpService = new FinishingUpService(unitOfWork, null);

            const int userId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int borrowerResidenceId = 1;

            const int secborrowerResidenceId = 2;
            const int ownershipTypeId = 1;


            var borrower = new Borrower()
            {
                Id = borrowerId,
                LoanApplicationId = loanApplicationId,
                TenantId = _tenant.Id,
                OwnTypeId = 1,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    Id = loanApplicationId,
                    UserId = userId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                },
                BorrowerResidences = new HashSet<BorrowerResidence>()
                        {
                            new BorrowerResidence
                            {
                                Id = borrowerResidenceId,
                                TypeId=1,
                                TenantId = _tenant.Id,
                                BorrowerId = borrowerId,
                                OwnershipTypeId = ownershipTypeId,
                                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                            },
                             new BorrowerResidence
                            {
                                Id = secborrowerResidenceId,
                                TypeId=2,
                                TenantId = _tenant.Id,
                                BorrowerId = borrowerId,
                                OwnershipTypeId = ownershipTypeId,
                                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                            }
                        }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);
            await unitOfWork.SaveChangesAsync();

            //Act
            var result = await finishingUpService.GetCoborrowerResidence(_tenant, userId, loanApplicationId);

            //Assert
            Assert.NotNull(result);


        }

        [Fact]
        public async Task GetBorrowerCurrentResidenceMoveInDate_ShouldReturnBorrowerCurrentResidenceMoveInDate()
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(AddOrUpdateBorrowerCurrentResidenceMoveInDate_ShouldUpdateRecord));
            var finishingUpService = new FinishingUpService(unitOfWork, null);

            const int userId = 1;
            const int loanApplicationId = 1;

            //Act
            const int borrowerId = 1;
            const int ownershipTypeId = 1;
            const int borrowerResidenceId = 1;

            var borrower = new Borrower()
            {
                Id = borrowerId,
                LoanApplicationId = loanApplicationId,
                TenantId = _tenant.Id,
                OwnTypeId = 1,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    Id = loanApplicationId,
                    UserId = userId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                },
                BorrowerResidences = new HashSet<BorrowerResidence>()
                        {
                            new BorrowerResidence
                            {
                                Id = borrowerResidenceId,
                                TypeId=1,
                                TenantId = _tenant.Id,
                                BorrowerId = borrowerId,
                                OwnershipTypeId = ownershipTypeId,
                                FromDate = DateTime.Now,
                                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                            }
                        }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);
            await unitOfWork.SaveChangesAsync();


            //Assert


            var record = await finishingUpService.GetBorrowerCurrentResidenceMoveInDate(_tenant, userId, borrowerResidenceId, loanApplicationId);

            Assert.NotNull(record);
        }

        [Fact]
        public async Task GetBorrowerPrimaryAddressDetail_ShouldReturnBorrowerPrimaryAddressDetail()
        {

            //Arrange
            const string testMethodName = nameof(GetBorrowerPrimaryAddressDetail_ShouldReturnBorrowerPrimaryAddressDetail);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var finishingUpService = new FinishingUpService(unitOfWork, null);

            const int userId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;

            var borrower = new Borrower()
            {
                Id = borrowerId,
                LoanApplicationId = loanApplicationId,
                TenantId = _tenant.Id,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    Id = loanApplicationId,
                    UserId = userId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                }
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);
            await unitOfWork.SaveChangesAsync();

            //Act
            var result = await finishingUpService.GetBorrowerPrimaryAddressDetail(_tenant, userId, borrowerId, loanApplicationId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddOrUpdateBorrowerCurrentResidenceMoveInDate_ShouldUpdateRecord() //Update
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>("AddOrUpdateBorrowerCurrentResidenceMoveInDate_ShouldUpdateRecord657");
            var finishingUpService = new FinishingUpService(unitOfWork, null);

            const int userId = 1;
            const int loanApplicationId = 1;



            //Act
            const int borrowerId = 1;
            const int ownershipTypeId = 1;
            const int borrowerResidenceId = 1;

            var borrower = new Borrower()
            {
                Id = borrowerId,
                LoanApplicationId = loanApplicationId,
                TenantId = _tenant.Id,
                OwnTypeId = 1,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    Id = loanApplicationId,
                    UserId = userId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                },
                BorrowerResidences = new HashSet<BorrowerResidence>()
                        {
                            new BorrowerResidence
                            {
                                Id = borrowerResidenceId,
                                TypeId=1,
                                TenantId = _tenant.Id,
                                BorrowerId = borrowerId,
                                OwnershipTypeId = ownershipTypeId,
                                FromDate = DateTime.Now,
                                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                            }
                        }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);
            await unitOfWork.SaveChangesAsync();

            //Assert


            var record = await finishingUpService.GetBorrowerCurrentResidenceMoveInDate(_tenant, userId, loanApplicationId, borrowerId);
            Assert.NotNull(record);

        }

        [Fact]
        public async Task GetDependentInfo_ShouldReturnDependentInfo()
        {
            //Arrange
            const string testMethodName = nameof(GetDependentInfo_ShouldReturnDependentInfo);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var finishingUpService = new FinishingUpService(unitOfWork, null);

            const int userId = 1;
            const int countryId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;



            var borrower = new Borrower()
            {
                Id = borrowerId,
                LoanApplicationId = loanApplicationId,
                TenantId = _tenant.Id,
                DependentAge = "22,33,44",
                NoOfDependent = 2,
                OwnTypeId = (int)TenantConfig.Common.DistributedCache.OwnType.Primary,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    Id = loanApplicationId,
                    UserId = userId,
                    TenantId = _tenant.Id,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                },
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);
            await unitOfWork.SaveChangesAsync();

            //Act
            DependentModel result = await finishingUpService.GetDependentinfo(_tenant, userId, loanApplicationId, borrowerId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteSecondaryAddress_ShouldAddRecord()
        {
            //Arrange
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(nameof(DeleteSecondaryAddress_ShouldAddRecord));
            var finishingUpService = new FinishingUpService(unitOfWork, null);


            //const int userId = 1;
            //const int borrowerId = 1;
            //const int id = 1;
            //const int borrowerResidenceId = 1;
            //const int loanApplicationId = 1;




            //var borrowerResidence = await Uow.Repository<BorrowerResidence>().Query(x => x.Id == borrowerResidenceId && x.Borrower.LoanApplicationId == loanApplicationId
            //  && x.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary
            //  && x.Borrower.LoanApplication.UserId == userId && x.TenantId == tenant.Id)



            //var borrowerResidence = await Uow.Repository<BorrowerResidence>().Query(x => x.Id == borrowerResidenceId && x.Borrower.LoanApplicationId == loanApplicationId
            //                       && x.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary
            //                       && x.Borrower.LoanApplication.UserId == userId && x.TenantId == tenant.Id)

            const int userId = 1;
            const int loanApplicationId = 1;

            //Act
            const int borrowerId = 1;
            const int ownershipTypeId = 1;
            const int borrowerResidenceId = 1;

            var borrower = new Borrower()
            {
                Id = borrowerId,
                LoanApplicationId = loanApplicationId,
                TenantId = _tenant.Id,
                OwnTypeId = 1,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    Id = loanApplicationId,
                    UserId = userId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                },

                BorrowerResidences = new HashSet<BorrowerResidence>()
                        {
                            new BorrowerResidence
                            {
                                Id = borrowerResidenceId,
                                TypeId=2,
                                TenantId = _tenant.Id,
                                BorrowerId = borrowerId,
                                OwnershipTypeId = ownershipTypeId,
                                FromDate = DateTime.Now,
                                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                                LoanAddress = new AddressInfo()
                                {
                                Id=1,
                               TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                                }
                            }
                        }
            };

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);
            await unitOfWork.SaveChangesAsync();

            //Act


            await finishingUpService.DeleteSecondaryAddress(_tenant, userId, borrowerResidenceId, loanApplicationId);

            var record = await unitOfWork.Repository<BorrowerResidence>().Query(x => x.Id == borrowerResidenceId).SingleOrDefaultAsync();

            //Assert
            Assert.Null(record);

        }

        [Fact]
        public async Task GetAllSpouseInfo_ShouldReturnBorrowerPrimaryAddressDetail()
        {

            //Arrange
            const string testMethodName = nameof(GetAllSpouseInfo_ShouldReturnBorrowerPrimaryAddressDetail);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var finishingUpService = new FinishingUpService(unitOfWork, null);

            const int userId = 1;
            const int borrowerId = 1;
            const int borrowerId2 = 1;
            const int loanApplicationId = 1;
            const int spouseLoanContactId = 1;
            var borrower = new Borrower()
            {
                Id = borrowerId,
                TenantId = _tenant.Id,
                OwnTypeId = 1,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,

                LoanContact_LoanContactId = new LoanContact()
                {
                    Id = 2,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                },
                LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    Id = loanApplicationId,
                    UserId = userId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                },
                SpouseLoanContact = new LoanContact()
                {
                    Id = spouseLoanContactId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    TenantId = _tenant.Id
                }
            };
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);
            await unitOfWork.SaveChangesAsync();

            //Act
            var result = await finishingUpService.GetAllSpouseInfo(_tenant, userId, loanApplicationId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddOrUpdateSpouseInfo_ShouldUpdateRecord() //Update
        {
            //Arrange
            const string testMethodName = nameof(AddOrUpdateSpouseInfo_ShouldUpdateRecord);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var finishingUpService = new FinishingUpService(unitOfWork, null);

            const int userId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int spouseLoanContactId = 1;

            var borrower = new Borrower()
            {
                Id = borrowerId,
                LoanApplicationId = loanApplicationId,
                TenantId = _tenant.Id,
                SpouseLoanContactId = spouseLoanContactId,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    Id = loanApplicationId,
                    UserId = userId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                },
                SpouseLoanContact = new LoanContact()
                {

                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    TenantId = _tenant.Id
                }
            };
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);
            await unitOfWork.SaveChangesAsync();
            //Act


            var result = await finishingUpService.GetSpouseInfo(_tenant, userId, borrowerId, spouseLoanContactId, loanApplicationId);


            //Assert
            const int expectedId = 1;

            Assert.IsType<int>(result.SpouseLoanContactId.Value);
            Assert.Equal<int>(expectedId, result.SpouseLoanContactId.Value);

        }

        [Fact]
        public async Task GetSpouseInfo_ShouldReturnSpouseInfo()
        {

            //Arrange
            const string testMethodName = nameof(GetSpouseInfo_ShouldReturnSpouseInfo);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var finishingUpService = new FinishingUpService(unitOfWork, null);

            const int userId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int spouseLoanContactId = 1;

            var borrower = new Borrower()
            {
                Id = borrowerId,
                LoanApplicationId = loanApplicationId,
                TenantId = _tenant.Id,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                LoanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    Id = loanApplicationId,
                    UserId = userId,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                },
                SpouseLoanContact = new LoanContact()
                {
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                    TenantId = _tenant.Id
                }
            };


            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);
            await unitOfWork.SaveChangesAsync();

            //Act
            var result = await finishingUpService.GetSpouseInfo(_tenant, userId, borrowerId, spouseLoanContactId, loanApplicationId);

            //Assert
            Assert.NotNull(result);
        }



        [Fact]
        public async Task ReviewBorrowerAndAllCoBorrowersInfo_ShouldReturnReviewBorrowerAndAllCoBorrowersInfo()
        {
            //Arrange
            const string testMethodName = nameof(ReviewBorrowerAndAllCoBorrowersInfo_ShouldReturnReviewBorrowerAndAllCoBorrowersInfo);
            var unitOfWork = ObjectHelper.GetInMemoryUnitOfWork<LoanApplicationContext>(testMethodName);
            var finishingUpService = new FinishingUpService(unitOfWork, null);

            const int userId = 1;
            const int borrowerResidenceId = 1;
            const int borrowerId = 1;
            const int loanApplicationId = 1;
            const int id = 1;

            var loanApplication = new LoanApplicationDb.Entity.Models.LoanApplication()
            {
                Id = loanApplicationId,
                UserId = userId,
                TenantId = _tenant.Id,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added
            };
           
            var borrower = new Borrower
            {
                Id = borrowerId,
                LoanApplicationId = loanApplicationId,
                TenantId = _tenant.Id,
                OwnTypeId = 1,
                TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                LoanContact_LoanContactId = new LoanContact()
                {
                    Id = id,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                },
                BorrowerResidences = new HashSet<BorrowerResidence>()
                {
                    new BorrowerResidence
                    {
                        Id = borrowerResidenceId,
                        TypeId = 1,
                        TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                        LoanAddress = new AddressInfo()
                        {
                            CountryId = 1,
                            StateId = 1,
                            TrackingState = TrackableEntities.Common.Core.TrackingState.Added,
                        }
                    }
                }
            };
         

            unitOfWork.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Insert(loanApplication);
            unitOfWork.Repository<LoanApplicationDb.Entity.Models.Borrower>().Insert(borrower);
            await unitOfWork.SaveChangesAsync();

            //Act
            var result = await finishingUpService.ReviewBorrowerAndAllCoBorrowersInfo(_tenant, userId, loanApplicationId);

            //Assert
            Assert.NotNull(result);
        }

    }
}
