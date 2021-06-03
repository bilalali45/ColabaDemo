using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using LoanApplication.Model;
using LoanApplication.Service;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TenantConfig.Common.DistributedCache;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;
using IncomeCategory = LoanApplication.Model.IncomeCategory;

namespace LoanApplication.Tests
{
    public class TestEmploymentInfoService
    {
        private readonly Mock<ILogger<EmploymentService>> _loggerServiceMock = new Mock<ILogger<EmploymentService>>();


        [Fact]
        public async Task AddOrUpdateEmploymentDetailWhenLoanApplicationNotFound()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeUserId = 1;
            var fakeTenant = new TenantModel
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };

            var fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId + 1, // Temper use id to mismatch loan application record
                    Borrowers = new List<Borrower>
                                {
                                    new Borrower
                                    {
                                        TenantId = fakeTenant.Id,
                                        LoanContact_LoanContactId = new LoanContact
                                                      {
                                                          TenantId = fakeTenant.Id,
                                                          FirstName = "FirstName",
                                                          MiddleName = "MiddleName",
                                                          LastName = "LastName",
                                                          EmailAddress = "fake@email.com"
                                                      }
                                    }
                                }
                };
            dataContext.Add(entity: fakeApplication);
            await dataContext.SaveChangesAsync();

            var fakeEmploymentDetailModel = new AddOrUpdateEmploymentDetailModel
            {
                BorrowerId = fakeApplication.Borrowers.First().Id,
                LoanApplicationId = fakeApplication.Id,
                EmployerAddress = new EmployerAddressModel
                {
                    LoanApplicationId = fakeApplication.Id,
                    StreetAddress = "StreetAddress",
                    StateName = "StateName",
                    State = "State",
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    CityId = 1,
                    CountryId = 1,
                    IncomeInfoId = null,
                    StateId = 1,
                    UnitNo = "1",
                    ZipCode = "1"
                },
                EmploymentInfo = new EmploymentInfo
                {
                    IncomeInfoId = 1,
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    EmployedByFamilyOrParty = true,
                    EmployerName = "fake current employer",
                    EmployerPhoneNumber = "123",
                    EndDate = null,
                    HasOwnershipInterest = true,
                    JobTitle = "job title",
                    OwnershipInterest = 20,
                    StartDate = DateTime.Now.AddDays(value: -100),
                    YearsInProfession = 10
                },
                EmploymentOtherIncomes = new List<EmploymentOtherAnnualIncome>
                                                                         {
                                                                             new EmploymentOtherAnnualIncome
                                                                             {
                                                                                 AnnualIncome = 12000,
                                                                                 IncomeTypeId = EmploymentOtherIncomeType.Bonus
                                                                             }
                                                                         },
                ErrorMessage = null,
                State = "state",
                WayOfIncome = new WayOfIncome
                {
                    EmployerAnnualSalary = 120000,
                    IsPaidByMonthlySalary = true,
                    HourlyRate = null,
                    HoursPerWeek = null
                },
            };

            // Act
            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);
            var results = await service.AddOrUpdateCurrentEmploymentDetail(tenant: fakeTenant,
                                                                    userId: fakeUserId,
                                                                    model: fakeEmploymentDetailModel);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.Equal(expected: EmploymentService.LoanApplicationNotFound, actual: results);
        }

        [Fact]
        public async Task AddOrUpdateEmploymentDetailWhenLoanBorrowerNotFound()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeUserId = 1;
            var fakeTenant = new TenantModel
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };

            var fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>
                                {
                                    new Borrower
                                    {
                                        TenantId = fakeTenant.Id,
                                        LoanContact_LoanContactId = new LoanContact
                                                      {
                                                          TenantId = fakeTenant.Id,
                                                          FirstName = "FirstName",
                                                          MiddleName = "MiddleName",
                                                          LastName = "LastName",
                                                          EmailAddress = "fake@email.com"
                                                      }
                                    }
                                }
                };
            dataContext.Add(entity: fakeApplication);
            await dataContext.SaveChangesAsync();

            var fakeEmploymentDetailModel = new AddOrUpdateEmploymentDetailModel
            {
                BorrowerId = fakeApplication.Borrowers.First().Id + 10, // Temper borrower Id to mismatch
                LoanApplicationId = fakeApplication.Id,
                EmployerAddress = new EmployerAddressModel
                {
                    LoanApplicationId = fakeApplication.Id,
                    StreetAddress = "StreetAddress",
                    StateName = "StateName",
                    State = "State",
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    CityId = 1,
                    CountryId = 1,
                    IncomeInfoId = null,
                    StateId = 1,
                    UnitNo = "1",
                    ZipCode = "1"
                },
                EmploymentInfo = new EmploymentInfo
                {
                    IncomeInfoId = 1,
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    EmployedByFamilyOrParty = true,
                    EmployerName = "fake current employer",
                    EmployerPhoneNumber = "123",
                    EndDate = null,
                    HasOwnershipInterest = true,
                    JobTitle = "job title",
                    OwnershipInterest = 20,
                    StartDate = DateTime.Now.AddDays(value: -100),
                    YearsInProfession = 10
                },
                EmploymentOtherIncomes = new List<EmploymentOtherAnnualIncome>
                                                                         {
                                                                             new EmploymentOtherAnnualIncome
                                                                             {
                                                                                 AnnualIncome = 12000,
                                                                                 IncomeTypeId = EmploymentOtherIncomeType.Bonus
                                                                             }
                                                                         },
                ErrorMessage = null,
                State = "state",
                WayOfIncome = new WayOfIncome
                {
                    EmployerAnnualSalary = 120000,
                    IsPaidByMonthlySalary = true,
                    HourlyRate = null,
                    HoursPerWeek = null
                },
            };

            // Act
            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);
            var results = await service.AddOrUpdateCurrentEmploymentDetail(tenant: fakeTenant,
                                                                    userId: fakeUserId,
                                                                    model: fakeEmploymentDetailModel);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.Equal(expected: EmploymentService.BorrowerDetailNotFound, actual: results);
        }

        [Fact]
        public async Task AddOrUpdateEmploymentDetailWhenEmploymentDetailIsNull()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeUserId = 1;
            var fakeTenant = new TenantModel
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };

            var fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>
                                {
                                    new Borrower
                                    {
                                        TenantId = fakeTenant.Id,
                                        LoanContact_LoanContactId = new LoanContact
                                                      {
                                                          TenantId = fakeTenant.Id,
                                                          FirstName = "FirstName",
                                                          MiddleName = "MiddleName",
                                                          LastName = "LastName",
                                                          EmailAddress = "fake@email.com"
                                                      }
                                    }
                                }
                };
            dataContext.Add(entity: fakeApplication);
            await dataContext.SaveChangesAsync();

            var fakeEmploymentDetailModel = new AddOrUpdateEmploymentDetailModel
            {
                BorrowerId = fakeApplication.Borrowers.First().Id,
                LoanApplicationId = fakeApplication.Id,
                EmployerAddress = new EmployerAddressModel
                {
                    LoanApplicationId = fakeApplication.Id,
                    StreetAddress = "StreetAddress",
                    StateName = "StateName",
                    State = "State",
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    CityId = 1,
                    CountryId = 1,
                    IncomeInfoId = null,
                    StateId = 1,
                    UnitNo = "1",
                    ZipCode = "1"
                },
                EmploymentInfo = new EmploymentInfo
                {
                    IncomeInfoId = 1,
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    EmployedByFamilyOrParty = true,
                    EmployerName = "fake current employer",
                    EmployerPhoneNumber = "123",
                    EndDate = null,
                    HasOwnershipInterest = true,
                    JobTitle = "job title",
                    OwnershipInterest = 20,
                    StartDate = DateTime.Now.AddDays(value: -100),
                    YearsInProfession = 10
                },
                EmploymentOtherIncomes = new List<EmploymentOtherAnnualIncome>
                                                                         {
                                                                             new EmploymentOtherAnnualIncome
                                                                             {
                                                                                 AnnualIncome = 12000,
                                                                                 IncomeTypeId = EmploymentOtherIncomeType.Bonus
                                                                             }
                                                                         },
                ErrorMessage = null,
                State = "state",
                WayOfIncome = new WayOfIncome
                {
                    EmployerAnnualSalary = 120000,
                    IsPaidByMonthlySalary = true,
                    HourlyRate = null,
                    HoursPerWeek = null
                },
            };

            fakeEmploymentDetailModel.EmploymentInfo = null; // Temper to null

            // Act
            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);
            var results = await service.AddOrUpdateCurrentEmploymentDetail(tenant: fakeTenant,
                                                                    userId: fakeUserId,
                                                                    model: fakeEmploymentDetailModel);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.Equal(expected: EmploymentService.EmploymentInfoIsRequired, actual: results);
        }

        [Fact]
        public async Task AddOrUpdateEmploymentDetailWhenPreviousEmploymentEnDateMissing()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeUserId = 1;
            var fakeTenant = new TenantModel
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };

            var fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>
                                {
                                    new Borrower
                                    {
                                        TenantId = fakeTenant.Id,
                                        LoanContact_LoanContactId = new LoanContact
                                                      {
                                                          TenantId = fakeTenant.Id,
                                                          FirstName = "FirstName",
                                                          MiddleName = "MiddleName",
                                                          LastName = "LastName",
                                                          EmailAddress = "fake@email.com"
                                                      }
                                    }
                                }
                };
            dataContext.Add(entity: fakeApplication);
            await dataContext.SaveChangesAsync();

            var fakeEmploymentDetailModel = new AddOrUpdatePreviousEmploymentDetailModel
            {
                BorrowerId = fakeApplication.Borrowers.First().Id,
                LoanApplicationId = fakeApplication.Id,
                EmployerAddress = new EmployerAddressModel
                {
                    LoanApplicationId = fakeApplication.Id,
                    StreetAddress = "StreetAddress",
                    StateName = "StateName",
                    State = "State",
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    CityId = 1,
                    CountryId = 1,
                    IncomeInfoId = null,
                    StateId = 1,
                    UnitNo = "1",
                    ZipCode = "1"
                },
                EmploymentInfo = new EmploymentInfo
                {
                    IncomeInfoId = 1,
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    EmployedByFamilyOrParty = true,
                    EmployerName = "fake current employer",
                    EmployerPhoneNumber = "123",
                    EndDate = null,
                    HasOwnershipInterest = true,
                    JobTitle = "job title",
                    OwnershipInterest = 20,
                    StartDate = DateTime.Now.AddDays(value: -100),
                    YearsInProfession = 10
                },
                ErrorMessage = null,
                State = "state",
                WayOfIncome = new WayOfIncomeBase
                {
                    EmployerAnnualSalary = 120000
                },
            };

            // Act
            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);
            var results = await service.AddOrUpdatePreviousEmploymentDetail(tenant: fakeTenant,
                                                                    userId: fakeUserId,
                                                                    model: fakeEmploymentDetailModel);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.Equal(expected: EmploymentService.EmploymentEndDateMissing, actual: results);
        }

        [Fact]
        public async Task AddOrUpdateEmploymentDetailWhenPreviousEmploymentAddressIsNull()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeUserId = 1;
            var fakeTenant = new TenantModel
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };

            var fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>
                                {
                                    new Borrower
                                    {
                                        TenantId = fakeTenant.Id,
                                        LoanContact_LoanContactId = new LoanContact
                                                      {
                                                          TenantId = fakeTenant.Id,
                                                          FirstName = "FirstName",
                                                          MiddleName = "MiddleName",
                                                          LastName = "LastName",
                                                          EmailAddress = "fake@email.com"
                                                      }
                                    }
                                }
                };
            dataContext.Add(entity: fakeApplication);
            await dataContext.SaveChangesAsync();

            var fakeEmploymentDetailModel = new AddOrUpdatePreviousEmploymentDetailModel
            {
                BorrowerId = fakeApplication.Borrowers.First().Id,
                LoanApplicationId = fakeApplication.Id,
                EmployerAddress = new EmployerAddressModel
                {
                    LoanApplicationId = fakeApplication.Id,
                    StreetAddress = "StreetAddress",
                    StateName = "StateName",
                    State = "State",
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    CityId = 1,
                    CountryId = 1,
                    IncomeInfoId = null,
                    StateId = 1,
                    UnitNo = "1",
                    ZipCode = "1"
                },
                EmploymentInfo = new EmploymentInfo
                {
                    IncomeInfoId = 1,
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    EmployedByFamilyOrParty = true,
                    EmployerName = "fake current employer",
                    EmployerPhoneNumber = "123",
                    EndDate = DateTime.Now.AddYears(-2),
                    HasOwnershipInterest = true,
                    JobTitle = "job title",
                    OwnershipInterest = 20,
                    StartDate = DateTime.Now.AddDays(value: -10),
                    YearsInProfession = 10,
                },
                ErrorMessage = null,
                State = "state",
                WayOfIncome = new WayOfIncomeBase
                {
                    EmployerAnnualSalary = 120000
                },
            };

            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);

            fakeEmploymentDetailModel.EmployerAddress = null;

            // Act
            var results = await service.AddOrUpdatePreviousEmploymentDetail(tenant: fakeTenant,
                                                                    userId: fakeUserId,
                                                                    model: fakeEmploymentDetailModel);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.Equal(expected: 1, actual: results);
        }

        [Fact]
        public async Task TestAddOrUpdateEmploymentDetailWhenPreviousEmploymentInfoExists()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeUserId = 1;
            var fakeTenant = new TenantModel
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };

            var fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>
                                {
                                    new Borrower
                                    {
                                        TenantId = fakeTenant.Id,
                                        LoanContact_LoanContactId = new LoanContact
                                                      {
                                                          TenantId = fakeTenant.Id,
                                                          FirstName = "FirstName",
                                                          MiddleName = "MiddleName",
                                                          LastName = "LastName",
                                                          EmailAddress = "fake@email.com"
                                                      },
                                        IncomeInfoes = new List<IncomeInfo>()
                                        {
                                            new IncomeInfo()
                                            {
                                                AddressInfo = new AddressInfo()
                                                {
                                                    CityId = 1,
                                                    CityName = "fake city",
                                                    CountryId = 1,
                                                    CountryName = "fake country",
                                                    IsDeleted = false,
                                                    StateId = 1,
                                                    StateName = "fake state",
                                                    StreetAddress = " xyz address",
                                                    TenantId = fakeTenant.Id,
                                                    UnitNo = "xyz",
                                                    ZipCode = "123"
                                                },
                                                TenantId = fakeTenant.Id,
                                                OtherIncomeInfoes = new List<OtherIncomeInfo>()
                                                {
                                                    new OtherIncomeInfo()
                                                    {
                                                        TenantId = fakeTenant.Id
                                                    }
                                                }
                                            },
                                        }
                                    }
                                }
                };
            dataContext.Add(entity: fakeApplication);
            await dataContext.SaveChangesAsync();

            var fakeEmploymentDetailModel = new AddOrUpdatePreviousEmploymentDetailModel
            {
                BorrowerId = fakeApplication.Borrowers.First().Id,
                LoanApplicationId = fakeApplication.Id,
                EmployerAddress = new EmployerAddressModel
                {
                    LoanApplicationId = fakeApplication.Id,
                    StreetAddress = "StreetAddress",
                    StateName = "StateName",
                    State = "State",
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    CityId = 1,
                    CountryId = 1,
                    IncomeInfoId = null,
                    StateId = 1,
                    UnitNo = "1",
                    ZipCode = "1"
                },
                EmploymentInfo = new EmploymentInfo
                {
                    IncomeInfoId = 1,
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    EmployedByFamilyOrParty = true,
                    EmployerName = "fake current employer",
                    EmployerPhoneNumber = "123",
                    EndDate = DateTime.Now.AddYears(-2),
                    HasOwnershipInterest = true,
                    JobTitle = "job title",
                    OwnershipInterest = 20,
                    StartDate = DateTime.Now.AddDays(value: -10),
                    YearsInProfession = 10
                },
                ErrorMessage = null,
                State = "state",
                WayOfIncome = new WayOfIncomeBase()
                {
                    EmployerAnnualSalary = 120000
                },
            };



            List<OtherIncomeType> otherIncomeTypes = new List<OtherIncomeType>()
            {
                new OtherIncomeType()
                {
                    Id = (int) EmploymentOtherIncomeType.Bonus,
                    Name = "Bonus",
                    IsActive = true,
                    IsDeleted = false
                }
            };

            dbFunctionServiceMock.Setup(x => x.UdfOtherIncomeType(It.IsAny<int>(), It.IsAny<int?>())).Returns(otherIncomeTypes.AsQueryable());

            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);

            // Act
            var results = await service.AddOrUpdatePreviousEmploymentDetail(tenant: fakeTenant,
                                                                    userId: fakeUserId,
                                                                    model: fakeEmploymentDetailModel);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.Equal(expected: 1, actual: results);
        }

        [Fact]
        public async Task TestGetAllBorrowerWithIncomeWhenLoanApplicationNotFound()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeUserId = 1;
            var fakeTenant = new TenantModel
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };

            var fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>
                                {
                                    new Borrower
                                    {
                                        TenantId = fakeTenant.Id,
                                        LoanContact_LoanContactId = new LoanContact
                                                      {
                                                          TenantId = fakeTenant.Id,
                                                          FirstName = "FirstName",
                                                          MiddleName = "MiddleName",
                                                          LastName = "LastName",
                                                          EmailAddress = "fake@email.com"
                                                      },
                                        IncomeInfoes = new List<IncomeInfo>()
                                        {
                                            new IncomeInfo()
                                            {
                                                AddressInfo = new AddressInfo()
                                                {
                                                    CityId = 1,
                                                    CityName = "fake city",
                                                    CountryId = 1,
                                                    CountryName = "fake country",
                                                    IsDeleted = false,
                                                    StateId = 1,
                                                    StateName = "fake state",
                                                    StreetAddress = " xyz address",
                                                    TenantId = fakeTenant.Id,
                                                    UnitNo = "xyz",
                                                    ZipCode = "123"
                                                },
                                                TenantId = fakeTenant.Id,
                                                OtherIncomeInfoes = new List<OtherIncomeInfo>()
                                                {
                                                    new OtherIncomeInfo()
                                                    {
                                                        TenantId = fakeTenant.Id
                                                    }
                                                }
                                            },
                                        }
                                    }
                                }
                };
            dataContext.Add(entity: fakeApplication);
            await dataContext.SaveChangesAsync();

            var fakeEmploymentDetailModel = new AddOrUpdateEmploymentDetailModel
            {
                BorrowerId = fakeApplication.Borrowers.First().Id,
                LoanApplicationId = fakeApplication.Id,
                EmployerAddress = new EmployerAddressModel
                {
                    LoanApplicationId = fakeApplication.Id,
                    StreetAddress = "StreetAddress",
                    StateName = "StateName",
                    State = "State",
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    CityId = 1,
                    CountryId = 1,
                    IncomeInfoId = null,
                    StateId = 1,
                    UnitNo = "1",
                    ZipCode = "1"
                },
                EmploymentInfo = new EmploymentInfo
                {
                    IncomeInfoId = 1,
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    EmployedByFamilyOrParty = true,
                    EmployerName = "fake current employer",
                    EmployerPhoneNumber = "123",
                    EndDate = null,
                    HasOwnershipInterest = true,
                    JobTitle = "job title",
                    OwnershipInterest = 20,
                    StartDate = DateTime.Now.AddDays(value: -100),
                    YearsInProfession = 10
                },
                EmploymentOtherIncomes = new List<EmploymentOtherAnnualIncome>
                                                                         {
                                                                             new EmploymentOtherAnnualIncome
                                                                             {
                                                                                 AnnualIncome = 12000,
                                                                                 IncomeTypeId = EmploymentOtherIncomeType.Bonus
                                                                             }
                                                                         },
                ErrorMessage = null,
                State = "state",
                WayOfIncome = new WayOfIncome
                {
                    EmployerAnnualSalary = 120000,
                    IsPaidByMonthlySalary = true,
                    HourlyRate = null,
                    HoursPerWeek = null
                },
            };



            List<OtherIncomeType> otherIncomeTypes = new List<OtherIncomeType>()
            {
                new OtherIncomeType()
                {
                    Id = (int) EmploymentOtherIncomeType.Bonus,
                    Name = "Bonus",
                    IsActive = true,
                    IsDeleted = false
                }
            };

            dbFunctionServiceMock.Setup(x => x.UdfOtherIncomeType(It.IsAny<int>(), It.IsAny<int?>())).Returns(otherIncomeTypes.AsQueryable());

            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);

            // Act
            var results = await service.GetAllBorrowerWithIncome(tenant: fakeTenant,
                                                                    userId: fakeUserId,
                                                                    2);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.Null(results);
        }

        [Fact]
        public async Task TestGetAllBorrowerWithIncomeWhenLoanApplicationFound()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeUserId = 1;
            var fakeTenant = new TenantModel
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };

            var fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>
                                {
                                    new Borrower
                                    {
                                        TenantId = fakeTenant.Id,
                                        LoanContact_LoanContactId = new LoanContact
                                                      {
                                                          TenantId = fakeTenant.Id,
                                                          FirstName = "FirstName",
                                                          MiddleName = "MiddleName",
                                                          LastName = "LastName",
                                                          EmailAddress = "fake@email.com"
                                                      },
                                        IncomeInfoes = new List<IncomeInfo>()
                                        {
                                            new IncomeInfo()
                                            {
                                                AddressInfo = new AddressInfo()
                                                {
                                                    CityId = 1,
                                                    CityName = "fake city",
                                                    CountryId = 1,
                                                    CountryName = "fake country",
                                                    IsDeleted = false,
                                                    StateId = 1,
                                                    StateName = "fake state",
                                                    StreetAddress = " xyz address",
                                                    TenantId = fakeTenant.Id,
                                                    UnitNo = "xyz",
                                                    ZipCode = "123"
                                                },
                                                TenantId = fakeTenant.Id,
                                                OtherIncomeInfoes = new List<OtherIncomeInfo>()
                                                {
                                                    new OtherIncomeInfo()
                                                    {
                                                        TenantId = fakeTenant.Id
                                                    }
                                                }
                                            },
                                        }
                                    }
                                }
                };
            dataContext.Add(entity: fakeApplication);
            await dataContext.SaveChangesAsync();

            var fakeEmploymentDetailModel = new AddOrUpdateEmploymentDetailModel
            {
                BorrowerId = fakeApplication.Borrowers.First().Id,
                LoanApplicationId = fakeApplication.Id,
                EmployerAddress = new EmployerAddressModel
                {
                    LoanApplicationId = fakeApplication.Id,
                    StreetAddress = "StreetAddress",
                    StateName = "StateName",
                    State = "State",
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    CityId = 1,
                    CountryId = 1,
                    IncomeInfoId = null,
                    StateId = 1,
                    UnitNo = "1",
                    ZipCode = "1"
                },
                EmploymentInfo = new EmploymentInfo
                {
                    IncomeInfoId = 1,
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    EmployedByFamilyOrParty = true,
                    EmployerName = "fake current employer",
                    EmployerPhoneNumber = "123",
                    EndDate = null,
                    HasOwnershipInterest = true,
                    JobTitle = "job title",
                    OwnershipInterest = 20,
                    StartDate = DateTime.Now.AddDays(value: -100),
                    YearsInProfession = 10
                },
                EmploymentOtherIncomes = new List<EmploymentOtherAnnualIncome>
                                                                         {
                                                                             new EmploymentOtherAnnualIncome
                                                                             {
                                                                                 AnnualIncome = 12000,
                                                                                 IncomeTypeId = EmploymentOtherIncomeType.Bonus
                                                                             }
                                                                         },
                ErrorMessage = null,
                State = "state",
                WayOfIncome = new WayOfIncome
                {
                    EmployerAnnualSalary = 120000,
                    IsPaidByMonthlySalary = true,
                    HourlyRate = null,
                    HoursPerWeek = null
                },
            };



            List<OtherIncomeType> otherIncomeTypes = new List<OtherIncomeType>()
            {
                new OtherIncomeType()
                {
                    Id = (int) EmploymentOtherIncomeType.Bonus,
                    Name = "Bonus",
                    IsActive = true,
                    IsDeleted = false
                }
            };

            dbFunctionServiceMock.Setup(x => x.UdfOtherIncomeType(It.IsAny<int>(), It.IsAny<int?>())).Returns(otherIncomeTypes.AsQueryable());

            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);

            // Act
            var results = await service.GetAllBorrowerWithIncome(tenant: fakeTenant,
                                                                    userId: fakeUserId,
                                                                    fakeApplication.Id);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.NotNull(results);
            Assert.NotEmpty(results);
            Assert.Equal(fakeApplication.Borrowers.Count, results.Count);
        }

        [Fact]
        public async Task TestGetAllBorrowerWithIncomeWhenLoanApplicationFoundAndAddressInfoIsNull()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeUserId = 1;
            var fakeTenant = new TenantModel
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };

            var fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>
                                {
                                    new Borrower
                                    {
                                        TenantId = fakeTenant.Id,
                                        LoanContact_LoanContactId = new LoanContact
                                                      {
                                                          TenantId = fakeTenant.Id,
                                                          FirstName = "FirstName",
                                                          MiddleName = "MiddleName",
                                                          LastName = "LastName",
                                                          EmailAddress = "fake@email.com"
                                                      },
                                        IncomeInfoes = new List<IncomeInfo>()
                                        {
                                            new IncomeInfo()
                                            {
                                                
                                                TenantId = fakeTenant.Id,
                                                OtherIncomeInfoes = new List<OtherIncomeInfo>()
                                                {
                                                    new OtherIncomeInfo()
                                                    {
                                                        TenantId = fakeTenant.Id
                                                    }
                                                }
                                            },
                                        }
                                    }
                                }
                };
            dataContext.Add(entity: fakeApplication);
            await dataContext.SaveChangesAsync();

            var fakeEmploymentDetailModel = new AddOrUpdateEmploymentDetailModel
            {
                BorrowerId = fakeApplication.Borrowers.First().Id,
                LoanApplicationId = fakeApplication.Id,
                EmployerAddress = new EmployerAddressModel
                {
                    LoanApplicationId = fakeApplication.Id,
                    StreetAddress = "StreetAddress",
                    StateName = "StateName",
                    State = "State",
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    CityId = 1,
                    CountryId = 1,
                    IncomeInfoId = null,
                    StateId = 1,
                    UnitNo = "1",
                    ZipCode = "1"
                },
                EmploymentInfo = new EmploymentInfo
                {
                    IncomeInfoId = 1,
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    EmployedByFamilyOrParty = true,
                    EmployerName = "fake current employer",
                    EmployerPhoneNumber = "123",
                    EndDate = null,
                    HasOwnershipInterest = true,
                    JobTitle = "job title",
                    OwnershipInterest = 20,
                    StartDate = DateTime.Now.AddDays(value: -100),
                    YearsInProfession = 10
                },
                EmploymentOtherIncomes = new List<EmploymentOtherAnnualIncome>
                                                                         {
                                                                             new EmploymentOtherAnnualIncome
                                                                             {
                                                                                 AnnualIncome = 12000,
                                                                                 IncomeTypeId = EmploymentOtherIncomeType.Bonus
                                                                             }
                                                                         },
                ErrorMessage = null,
                State = "state",
                WayOfIncome = new WayOfIncome
                {
                    EmployerAnnualSalary = 120000,
                    IsPaidByMonthlySalary = true,
                    HourlyRate = null,
                    HoursPerWeek = null
                },
            };



            List<OtherIncomeType> otherIncomeTypes = new List<OtherIncomeType>()
            {
                new OtherIncomeType()
                {
                    Id = (int) EmploymentOtherIncomeType.Bonus,
                    Name = "Bonus",
                    IsActive = true,
                    IsDeleted = false
                }
            };

            dbFunctionServiceMock.Setup(x => x.UdfOtherIncomeType(It.IsAny<int>(), It.IsAny<int?>())).Returns(otherIncomeTypes.AsQueryable());

            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);

            // Act
            var results = await service.GetAllBorrowerWithIncome(tenant: fakeTenant,
                                                                    userId: fakeUserId,
                                                                    fakeApplication.Id);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.NotNull(results);
            Assert.NotEmpty(results);
            Assert.Equal(fakeApplication.Borrowers.Count, results.Count);
        }

        [Fact]
        public async Task TesGetEmploymentHistoryLoanApplicationNotFound()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeUserId = 1;
            var fakeTenant = new TenantModel
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };

            var fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>
                                {
                                    new Borrower
                                    {
                                        TenantId = fakeTenant.Id,
                                        LoanContact_LoanContactId = new LoanContact
                                                      {
                                                          TenantId = fakeTenant.Id,
                                                          FirstName = "FirstName",
                                                          MiddleName = "MiddleName",
                                                          LastName = "LastName",
                                                          EmailAddress = "fake@email.com"
                                                      },
                                        IncomeInfoes = new List<IncomeInfo>()
                                        {
                                            new IncomeInfo()
                                            {
                                                AddressInfo = new AddressInfo()
                                                {
                                                    CityId = 1,
                                                    CityName = "fake city",
                                                    CountryId = 1,
                                                    CountryName = "fake country",
                                                    IsDeleted = false,
                                                    StateId = 1,
                                                    StateName = "fake state",
                                                    StreetAddress = " xyz address",
                                                    TenantId = fakeTenant.Id,
                                                    UnitNo = "xyz",
                                                    ZipCode = "123"
                                                },
                                                TenantId = fakeTenant.Id,
                                                OtherIncomeInfoes = new List<OtherIncomeInfo>()
                                                {
                                                    new OtherIncomeInfo()
                                                    {
                                                        TenantId = fakeTenant.Id
                                                    }
                                                }
                                            },
                                        }
                                    }
                                }
                };
            dataContext.Add(entity: fakeApplication);
            await dataContext.SaveChangesAsync();

            var fakeEmploymentDetailModel = new AddOrUpdateEmploymentDetailModel
            {
                BorrowerId = fakeApplication.Borrowers.First().Id,
                LoanApplicationId = fakeApplication.Id,
                EmployerAddress = new EmployerAddressModel
                {
                    LoanApplicationId = fakeApplication.Id,
                    StreetAddress = "StreetAddress",
                    StateName = "StateName",
                    State = "State",
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    CityId = 1,
                    CountryId = 1,
                    IncomeInfoId = null,
                    StateId = 1,
                    UnitNo = "1",
                    ZipCode = "1"
                },
                EmploymentInfo = new EmploymentInfo
                {
                    IncomeInfoId = 1,
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    EmployedByFamilyOrParty = true,
                    EmployerName = "fake current employer",
                    EmployerPhoneNumber = "123",
                    EndDate = null,
                    HasOwnershipInterest = true,
                    JobTitle = "job title",
                    OwnershipInterest = 20,
                    StartDate = DateTime.Now.AddDays(value: -100),
                    YearsInProfession = 10
                },
                EmploymentOtherIncomes = new List<EmploymentOtherAnnualIncome>
                                                                         {
                                                                             new EmploymentOtherAnnualIncome
                                                                             {
                                                                                 AnnualIncome = 12000,
                                                                                 IncomeTypeId = EmploymentOtherIncomeType.Bonus
                                                                             }
                                                                         },
                ErrorMessage = null,
                State = "state",
                WayOfIncome = new WayOfIncome
                {
                    EmployerAnnualSalary = 120000,
                    IsPaidByMonthlySalary = true,
                    HourlyRate = null,
                    HoursPerWeek = null
                },
            };

            List<OtherIncomeType> otherIncomeTypes = new List<OtherIncomeType>()
            {
                new OtherIncomeType()
                {
                    Id = (int) EmploymentOtherIncomeType.Bonus,
                    Name = "Bonus",
                    IsActive = true,
                    IsDeleted = false
                }
            };

            dbFunctionServiceMock.Setup(x => x.UdfOtherIncomeType(It.IsAny<int>(), It.IsAny<int?>())).Returns(otherIncomeTypes.AsQueryable());

            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);

            // Act
            var results = await service.GetEmploymentHistory(tenant: fakeTenant,
                                                                    userId: fakeUserId,
                                                                    2);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.NotNull(results);
            Assert.NotNull(results.ErrorMessage);
        }

        [Fact]
        public async Task TesGetEmploymentHistoryLoanApplication()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeUserId = 1;
            var fakeTenant = new TenantModel
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };

            var fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>
                                {
                                    new Borrower
                                    {
                                        TenantId = fakeTenant.Id,
                                        LoanContact_LoanContactId = new LoanContact
                                                      {
                                                          TenantId = fakeTenant.Id,
                                                          FirstName = "FirstName",
                                                          MiddleName = "MiddleName",
                                                          LastName = "LastName",
                                                          EmailAddress = "fake@email.com"
                                                      },
                                        IncomeInfoes = new List<IncomeInfo>()
                                        {
                                            new IncomeInfo()
                                            {
                                                AddressInfo = new AddressInfo()
                                                {
                                                    CityId = 1,
                                                    CityName = "fake city",
                                                    CountryId = 1,
                                                    CountryName = "fake country",
                                                    IsDeleted = false,
                                                    StateId = 1,
                                                    StateName = "fake state",
                                                    StreetAddress = " xyz address",
                                                    TenantId = fakeTenant.Id,
                                                    UnitNo = "xyz",
                                                    ZipCode = "123"
                                                },
                                                TenantId = fakeTenant.Id,
                                                OtherIncomeInfoes = new List<OtherIncomeInfo>()
                                                {
                                                    new OtherIncomeInfo()
                                                    {
                                                        TenantId = fakeTenant.Id,
                                                        OtherIncomeTypeId = (int) EmploymentOtherIncomeType.Bonus
                                                    }
                                                },
                                                IncomeTypeId = (int) BorrowerIncomeTypes.EmploymentInfo,
                                                //IncomeType = new IncomeType()
                                                //{
                                                //    Id = (int) BorrowerIncomeTypes.EmploymentInfo,
                                                //    Name = "EmploymentInfo",
                                                //    IncomeCategoryId = (int) BorrowerIncomeCategory.Employment,
                                                //}
                                            },
                                        }
                                    }
                                }
                };
            dataContext.Add(entity: fakeApplication);
            await dataContext.SaveChangesAsync();

            var fakeEmploymentDetailModel = new AddOrUpdateEmploymentDetailModel
            {
                BorrowerId = fakeApplication.Borrowers.First().Id,
                LoanApplicationId = fakeApplication.Id,
                EmployerAddress = new EmployerAddressModel
                {
                    LoanApplicationId = fakeApplication.Id,
                    StreetAddress = "StreetAddress",
                    StateName = "StateName",
                    State = "State",
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    CityId = 1,
                    CountryId = 1,
                    IncomeInfoId = null,
                    StateId = 1,
                    UnitNo = "1",
                    ZipCode = "1"
                },
                EmploymentInfo = new EmploymentInfo
                {
                    IncomeInfoId = 1,
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    EmployedByFamilyOrParty = true,
                    EmployerName = "fake current employer",
                    EmployerPhoneNumber = "123",
                    EndDate = null,
                    HasOwnershipInterest = true,
                    JobTitle = "job title",
                    OwnershipInterest = 20,
                    StartDate = DateTime.Now.AddDays(value: -100),
                    YearsInProfession = 10
                },
                EmploymentOtherIncomes = new List<EmploymentOtherAnnualIncome>
                                                                         {
                                                                             new EmploymentOtherAnnualIncome
                                                                             {
                                                                                 AnnualIncome = 12000,
                                                                                 IncomeTypeId = EmploymentOtherIncomeType.Bonus
                                                                             }
                                                                         },
                ErrorMessage = null,
                State = "state",
                WayOfIncome = new WayOfIncome
                {
                    EmployerAnnualSalary = 120000,
                    IsPaidByMonthlySalary = true,
                    HourlyRate = null,
                    HoursPerWeek = null
                },
            };

            List<OtherIncomeType> otherIncomeTypes = new List<OtherIncomeType>()
            {
                new OtherIncomeType()
                {
                    Id = (int) EmploymentOtherIncomeType.Bonus,
                    Name = "Bonus",
                    IsActive = true,
                    IsDeleted = false
                }
            };

            List<LoanApplicationDb.Entity.Models.IncomeType> incomeTypes = new EditableList<IncomeType>()
            {
                new IncomeType()
                {
                    Id = (int) BorrowerIncomeTypes.EmploymentInfo,
                    Name = "EmploymentInfo",
                    IncomeCategoryId = (int) BorrowerIncomeCategory.Employment
                }
            };

            List<LoanApplicationDb.Entity.Models.IncomeCategory> incomeCategories =
                new EditableList<LoanApplicationDb.Entity.Models.IncomeCategory>()
                {
                    new LoanApplicationDb.Entity.Models.IncomeCategory()
                    {
                        Id = (int) BorrowerIncomeCategory.Employment,
                        Name = "Employment Info"
                    }
                };

            dbFunctionServiceMock.Setup(x => x.UdfOtherIncomeType(It.IsAny<int>(), It.IsAny<int?>())).Returns(otherIncomeTypes.AsQueryable());
            dbFunctionServiceMock.Setup(x => x.UdfIncomeType(It.IsAny<int>(), It.IsAny<int?>())).Returns(incomeTypes.AsQueryable());
            dbFunctionServiceMock.Setup(x => x.UdfIncomeCategory(It.IsAny<int>(), It.IsAny<int?>())).Returns(incomeCategories.AsQueryable());

            List<IncomeType> fakeIncomeTypes = new EditableList<IncomeType>()
            {
                new IncomeType()
                {
                    Id = 1,
                    Name = "fake income type",
                    IncomeCategoryId = 1
                }
            };

            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);

            // Act
            var results = await service.GetEmploymentHistory(tenant: fakeTenant,
                                                                    userId: fakeUserId,
                                                                    fakeApplication.Id);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.NotNull(results);
        }

        [Fact]
        public async Task AddOrUpdateEmploymentDetail_Called_1()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeUserId = 1;
            var fakeTenant = new TenantModel
                             {
                                 Branches = new EditableList<BranchModel>(),
                                 Code = "fakeCode",
                                 Id = 1,
                                 Urls = new List<UrlModel>()
                             };

            var fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>
                                {
                                    new Borrower
                                    {
                                        TenantId = fakeTenant.Id,
                                        LoanContact_LoanContactId = new LoanContact
                                                      {
                                                          TenantId = fakeTenant.Id,
                                                          FirstName = "FirstName",
                                                          MiddleName = "MiddleName",
                                                          LastName = "LastName",
                                                          EmailAddress = "fake@email.com"
                                                      }
                                    }
                                }
                };
            dataContext.Add(entity: fakeApplication);
            await dataContext.SaveChangesAsync();

            var fakeEmploymentDetailModel = new AddOrUpdateEmploymentDetailModel
                                            {
                                                BorrowerId = fakeApplication.Borrowers.First().Id,
                                                LoanApplicationId = fakeApplication.Id,
                                                EmployerAddress = new EmployerAddressModel
                                                                  {
                                                                      LoanApplicationId = fakeApplication.Id,
                                                                      StreetAddress = "StreetAddress",
                                                                      StateName = "StateName",
                                                                      State = "State",
                                                                      BorrowerId = fakeApplication.Borrowers.First().Id,
                                                                      CityId = 1,
                                                                      CountryId = 1,
                                                                      IncomeInfoId = null,
                                                                      StateId = 1,
                                                                      UnitNo = "1",
                                                                      ZipCode = "1"
                                                                  },
                                                EmploymentInfo = new EmploymentInfo
                                                                 {
                                                                     IncomeInfoId = 1,
                                                                     BorrowerId = fakeApplication.Borrowers.First().Id,
                                                                     EmployedByFamilyOrParty = true,
                                                                     EmployerName = "fake current employer",
                                                                     EmployerPhoneNumber = "123",
                                                                     EndDate = null,
                                                                     HasOwnershipInterest = true,
                                                                     JobTitle = "job title",
                                                                     OwnershipInterest = 20,
                                                                     StartDate = DateTime.Now.AddDays(value: -100),
                                                                     YearsInProfession = 10
                                                                 },
                                                EmploymentOtherIncomes = new List<EmploymentOtherAnnualIncome>
                                                                         {
                                                                             new EmploymentOtherAnnualIncome
                                                                             {
                                                                                 AnnualIncome = 12000,
                                                                                 IncomeTypeId = EmploymentOtherIncomeType.Bonus
                                                                             }
                                                                         },
                                                ErrorMessage = null,
                                                State = "state",
                                                WayOfIncome = new WayOfIncome
                                                              {
                                                                  EmployerAnnualSalary = 120000,
                                                                  IsPaidByMonthlySalary = true,
                                                                  HourlyRate = null,
                                                                  HoursPerWeek = null
                                                              },
                                            };

            // Act
            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);
            var results = await service.AddOrUpdateCurrentEmploymentDetail(tenant: fakeTenant,
                                                                    userId: fakeUserId,
                                                                    model: fakeEmploymentDetailModel);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.Equal(expected: 1,
                         actual: results);
        }


        [Fact]
        public async Task GetEmploymentOtherDefaultIncomeTypes_Called_1()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeTenant = new TenantModel
                             {
                                 Branches = new EditableList<BranchModel>(),
                                 Code = "fakeCode",
                                 Id = 1,
                                 Urls = new List<UrlModel>()
                             };

            // Act
            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);
            var results = service.GetEmploymentOtherDefaultIncomeTypes(tenant: fakeTenant
                                                                      );

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.NotNull(@object: results);
        }

        [Fact]
        public async Task TestGetEmploymentDetailWhenLoanApplicationNotFound()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeUserId = 1;
            var fakeTenant = new TenantModel
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };

            var fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId + 1, // Temper use id to mismatch loan application record
                    Borrowers = new List<Borrower>
                                {
                                    new Borrower
                                    {
                                        TenantId = fakeTenant.Id,
                                        LoanContact_LoanContactId = new LoanContact
                                                      {
                                                          TenantId = fakeTenant.Id,
                                                          FirstName = "FirstName",
                                                          MiddleName = "MiddleName",
                                                          LastName = "LastName",
                                                          EmailAddress = "fake@email.com"
                                                      }
                                    }
                                }
                };
            dataContext.Add(entity: fakeApplication);
            await dataContext.SaveChangesAsync();

            var fakeEmploymentDetailModel = new AddOrUpdateEmploymentDetailModel
            {
                BorrowerId = fakeApplication.Borrowers.First().Id,
                LoanApplicationId = fakeApplication.Id,
                EmployerAddress = new EmployerAddressModel
                {
                    LoanApplicationId = fakeApplication.Id,
                    StreetAddress = "StreetAddress",
                    StateName = "StateName",
                    State = "State",
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    CityId = 1,
                    CountryId = 1,
                    IncomeInfoId = null,
                    StateId = 1,
                    UnitNo = "1",
                    ZipCode = "1"
                },
                EmploymentInfo = new EmploymentInfo
                {
                    IncomeInfoId = 1,
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    EmployedByFamilyOrParty = true,
                    EmployerName = "fake current employer",
                    EmployerPhoneNumber = "123",
                    EndDate = null,
                    HasOwnershipInterest = true,
                    JobTitle = "job title",
                    OwnershipInterest = 20,
                    StartDate = DateTime.Now.AddDays(value: -100),
                    YearsInProfession = 10
                },
                EmploymentOtherIncomes = new List<EmploymentOtherAnnualIncome>
                                                                         {
                                                                             new EmploymentOtherAnnualIncome
                                                                             {
                                                                                 AnnualIncome = 12000,
                                                                                 IncomeTypeId = EmploymentOtherIncomeType.Bonus
                                                                             }
                                                                         },
                ErrorMessage = null,
                State = "state",
                WayOfIncome = new WayOfIncome
                {
                    EmployerAnnualSalary = 120000,
                    IsPaidByMonthlySalary = true,
                    HourlyRate = null,
                    HoursPerWeek = null
                },
            };

            // Act
            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);
            var results = await service.GetEmploymentDetail(tenant: fakeTenant,
                                                                    userId: fakeUserId,
                                                                    loanApplicationId:1,
                                                                    borrowerId: 1,
                                                                    incomeInfoId: 1);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.NotNull(results);
            Assert.True(!string.IsNullOrEmpty(results.ErrorMessage));
            Assert.Equal(service.ErrorMessages[EmploymentService.LoanApplicationNotFound], results.ErrorMessage);
        }

        [Fact]
        public async Task TestGetEmploymentDetailWhenBorrowerNotFound()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeUserId = 1;
            var fakeTenant = new TenantModel
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };

            var fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>
                                {
                                    new Borrower
                                    {
                                        TenantId = fakeTenant.Id,
                                        LoanContact_LoanContactId = new LoanContact
                                                      {
                                                          TenantId = fakeTenant.Id,
                                                          FirstName = "FirstName",
                                                          MiddleName = "MiddleName",
                                                          LastName = "LastName",
                                                          EmailAddress = "fake@email.com"
                                                      }
                                    }
                                }
                };
            dataContext.Add(entity: fakeApplication);
            await dataContext.SaveChangesAsync();

            var fakeEmploymentDetailModel = new AddOrUpdateEmploymentDetailModel
            {
                BorrowerId = fakeApplication.Borrowers.First().Id,
                LoanApplicationId = fakeApplication.Id,
                EmployerAddress = new EmployerAddressModel
                {
                    LoanApplicationId = fakeApplication.Id,
                    StreetAddress = "StreetAddress",
                    StateName = "StateName",
                    State = "State",
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    CityId = 1,
                    CountryId = 1,
                    IncomeInfoId = null,
                    StateId = 1,
                    UnitNo = "1",
                    ZipCode = "1"
                },
                EmploymentInfo = new EmploymentInfo
                {
                    IncomeInfoId = 1,
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    EmployedByFamilyOrParty = true,
                    EmployerName = "fake current employer",
                    EmployerPhoneNumber = "123",
                    EndDate = null,
                    HasOwnershipInterest = true,
                    JobTitle = "job title",
                    OwnershipInterest = 20,
                    StartDate = DateTime.Now.AddDays(value: -100),
                    YearsInProfession = 10
                },
                EmploymentOtherIncomes = new List<EmploymentOtherAnnualIncome>
                                                                         {
                                                                             new EmploymentOtherAnnualIncome
                                                                             {
                                                                                 AnnualIncome = 12000,
                                                                                 IncomeTypeId = EmploymentOtherIncomeType.Bonus
                                                                             }
                                                                         },
                ErrorMessage = null,
                State = "state",
                WayOfIncome = new WayOfIncome
                {
                    EmployerAnnualSalary = 120000,
                    IsPaidByMonthlySalary = true,
                    HourlyRate = null,
                    HoursPerWeek = null
                },
            };

            // Act
            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);
            var results = await service.GetEmploymentDetail(tenant: fakeTenant,
                                                                    userId: fakeUserId,
                                                                    loanApplicationId: 1,
                                                                    borrowerId: 10,
                                                                    incomeInfoId: 1);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.NotNull(results);
            Assert.True(!string.IsNullOrEmpty(results.ErrorMessage));
            Assert.Equal(service.ErrorMessages[EmploymentService.BorrowerDetailNotFound], results.ErrorMessage);
        }

        [Fact]
        public async Task TestGetEmploymentDetailWhenAllDataFound()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeUserId = 1;
            var fakeTenant = new TenantModel
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };

            var fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>
                                {
                                    new Borrower
                                    {
                                        TenantId = fakeTenant.Id,
                                        LoanContact_LoanContactId = new LoanContact
                                                      {
                                                          TenantId = fakeTenant.Id,
                                                          FirstName = "FirstName",
                                                          MiddleName = "MiddleName",
                                                          LastName = "LastName",
                                                          EmailAddress = "fake@email.com"
                                                      },
                                        IncomeInfoes = new List<IncomeInfo>()
                                        {
                                            new IncomeInfo()
                                            {
                                                AddressInfo = new AddressInfo()
                                                {
                                                    CityId = 1,
                                                    CityName = "fake city",
                                                    CountryId = 1,
                                                    CountryName = "fake country",
                                                    IsDeleted = false,
                                                    StateId = 1,
                                                    StateName = "fake state",
                                                    StreetAddress = " xyz address",
                                                    TenantId = fakeTenant.Id,
                                                    UnitNo = "xyz",
                                                    ZipCode = "123"
                                                },
                                                TenantId = fakeTenant.Id,
                                                OtherIncomeInfoes = new List<OtherIncomeInfo>()
                                                {
                                                    new OtherIncomeInfo()
                                                    {
                                                        TenantId = fakeTenant.Id
                                                    }
                                                }
                                            },
                                        }
                                    }
                                }
                };
            dataContext.Add(entity: fakeApplication);
            await dataContext.SaveChangesAsync();

            var fakeEmploymentDetailModel = new AddOrUpdateEmploymentDetailModel
            {
                BorrowerId = fakeApplication.Borrowers.First().Id,
                LoanApplicationId = fakeApplication.Id,
                EmployerAddress = new EmployerAddressModel
                {
                    LoanApplicationId = fakeApplication.Id,
                    StreetAddress = "StreetAddress",
                    StateName = "StateName",
                    State = "State",
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    CityId = 1,
                    CountryId = 1,
                    IncomeInfoId = null,
                    StateId = 1,
                    UnitNo = "1",
                    ZipCode = "1"
                },
                EmploymentInfo = new EmploymentInfo
                {
                    IncomeInfoId = 1,
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    EmployedByFamilyOrParty = true,
                    EmployerName = "fake current employer",
                    EmployerPhoneNumber = "123",
                    EndDate = null,
                    HasOwnershipInterest = true,
                    JobTitle = "job title",
                    OwnershipInterest = 20,
                    StartDate = DateTime.Now.AddDays(value: -100),
                    YearsInProfession = 10
                },
                EmploymentOtherIncomes = new List<EmploymentOtherAnnualIncome>
                                                                         {
                                                                             new EmploymentOtherAnnualIncome
                                                                             {
                                                                                 AnnualIncome = 12000,
                                                                                 IncomeTypeId = EmploymentOtherIncomeType.Bonus
                                                                             }
                                                                         },
                ErrorMessage = null,
                State = "state",
                WayOfIncome = new WayOfIncome
                {
                    EmployerAnnualSalary = 120000,
                    IsPaidByMonthlySalary = true,
                    HourlyRate = null,
                    HoursPerWeek = null
                },
            };



            List<OtherIncomeType> otherIncomeTypes = new List<OtherIncomeType>()
            {
                new OtherIncomeType()
                {
                    Id = (int) EmploymentOtherIncomeType.Bonus,
                    Name = "Bonus",
                    IsActive = true,
                    IsDeleted = false
                }
            };

            dbFunctionServiceMock.Setup(x => x.UdfOtherIncomeType(It.IsAny<int>(), It.IsAny<int?>())).Returns(otherIncomeTypes.AsQueryable());

            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);

            // Act
            var results = await service.GetEmploymentDetail(tenant: fakeTenant,
                                                                    userId: fakeUserId,
                                                                    loanApplicationId: fakeApplication.Id,
                                                                    borrowerId: fakeApplication.Borrowers.First().Id,
                                                                    incomeInfoId: fakeApplication.Borrowers.First().IncomeInfoes.First().Id);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.NotNull(results);
        }

        [Fact]
        public async Task TestDeleteIncomeDetailWhenLoanApplicationNotFound()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeUserId = 1;
            var fakeTenant = new TenantModel
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };

            var fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>
                                {
                                    new Borrower
                                    {
                                        TenantId = fakeTenant.Id,
                                        LoanContact_LoanContactId = new LoanContact
                                                      {
                                                          TenantId = fakeTenant.Id,
                                                          FirstName = "FirstName",
                                                          MiddleName = "MiddleName",
                                                          LastName = "LastName",
                                                          EmailAddress = "fake@email.com"
                                                      },
                                        IncomeInfoes = new List<IncomeInfo>()
                                        {
                                            new IncomeInfo()
                                            {
                                                AddressInfo = new AddressInfo()
                                                {
                                                    CityId = 1,
                                                    CityName = "fake city",
                                                    CountryId = 1,
                                                    CountryName = "fake country",
                                                    IsDeleted = false,
                                                    StateId = 1,
                                                    StateName = "fake state",
                                                    StreetAddress = " xyz address",
                                                    TenantId = fakeTenant.Id,
                                                    UnitNo = "xyz",
                                                    ZipCode = "123"
                                                },
                                                TenantId = fakeTenant.Id,
                                                OtherIncomeInfoes = new List<OtherIncomeInfo>()
                                                {
                                                    new OtherIncomeInfo()
                                                    {
                                                        TenantId = fakeTenant.Id
                                                    }
                                                }
                                            },
                                        }
                                    }
                                }
                };
            dataContext.Add(entity: fakeApplication);
            await dataContext.SaveChangesAsync();

            var fakeEmploymentDetailModel = new AddOrUpdateEmploymentDetailModel
            {
                BorrowerId = fakeApplication.Borrowers.First().Id,
                LoanApplicationId = fakeApplication.Id,
                EmployerAddress = new EmployerAddressModel
                {
                    LoanApplicationId = fakeApplication.Id,
                    StreetAddress = "StreetAddress",
                    StateName = "StateName",
                    State = "State",
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    CityId = 1,
                    CountryId = 1,
                    IncomeInfoId = null,
                    StateId = 1,
                    UnitNo = "1",
                    ZipCode = "1"
                },
                EmploymentInfo = new EmploymentInfo
                {
                    IncomeInfoId = 1,
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    EmployedByFamilyOrParty = true,
                    EmployerName = "fake current employer",
                    EmployerPhoneNumber = "123",
                    EndDate = null,
                    HasOwnershipInterest = true,
                    JobTitle = "job title",
                    OwnershipInterest = 20,
                    StartDate = DateTime.Now.AddDays(value: -100),
                    YearsInProfession = 10
                },
                EmploymentOtherIncomes = new List<EmploymentOtherAnnualIncome>
                                                                         {
                                                                             new EmploymentOtherAnnualIncome
                                                                             {
                                                                                 AnnualIncome = 12000,
                                                                                 IncomeTypeId = EmploymentOtherIncomeType.Bonus
                                                                             }
                                                                         },
                ErrorMessage = null,
                State = "state",
                WayOfIncome = new WayOfIncome
                {
                    EmployerAnnualSalary = 120000,
                    IsPaidByMonthlySalary = true,
                    HourlyRate = null,
                    HoursPerWeek = null
                },
            };



            List<OtherIncomeType> otherIncomeTypes = new List<OtherIncomeType>()
            {
                new OtherIncomeType()
                {
                    Id = (int) EmploymentOtherIncomeType.Bonus,
                    Name = "Bonus",
                    IsActive = true,
                    IsDeleted = false
                }
            };

            dbFunctionServiceMock.Setup(x => x.UdfOtherIncomeType(It.IsAny<int>(), It.IsAny<int?>())).Returns(otherIncomeTypes.AsQueryable());

            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);

            CurrentEmploymentDeleteModel fakeDeleteModel = new CurrentEmploymentDeleteModel()
            {
                BorrowerId = 1,
                IncomeInfoId = 1,
                LoanApplicationId = 2 // Wrong loan application id
            };

            // Act
            var results = await service.DeleteIncomeDetail(tenant: fakeTenant,
                                                                    userId: fakeUserId,
                                                                    fakeDeleteModel);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.Equal(EmploymentService.LoanApplicationNotFound, results);
        }

        [Fact]
        public async Task TestDeleteIncomeDetailWhenBorrowerNotFound()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeUserId = 1;
            var fakeTenant = new TenantModel
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };

            var fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>
                                {
                                    new Borrower
                                    {
                                        TenantId = fakeTenant.Id,
                                        LoanContact_LoanContactId = new LoanContact
                                                      {
                                                          TenantId = fakeTenant.Id,
                                                          FirstName = "FirstName",
                                                          MiddleName = "MiddleName",
                                                          LastName = "LastName",
                                                          EmailAddress = "fake@email.com"
                                                      },
                                        IncomeInfoes = new List<IncomeInfo>()
                                        {
                                            new IncomeInfo()
                                            {
                                                AddressInfo = new AddressInfo()
                                                {
                                                    CityId = 1,
                                                    CityName = "fake city",
                                                    CountryId = 1,
                                                    CountryName = "fake country",
                                                    IsDeleted = false,
                                                    StateId = 1,
                                                    StateName = "fake state",
                                                    StreetAddress = " xyz address",
                                                    TenantId = fakeTenant.Id,
                                                    UnitNo = "xyz",
                                                    ZipCode = "123"
                                                },
                                                TenantId = fakeTenant.Id,
                                                OtherIncomeInfoes = new List<OtherIncomeInfo>()
                                                {
                                                    new OtherIncomeInfo()
                                                    {
                                                        TenantId = fakeTenant.Id
                                                    }
                                                }
                                            },
                                        }
                                    }
                                }
                };
            dataContext.Add(entity: fakeApplication);
            await dataContext.SaveChangesAsync();

            var fakeEmploymentDetailModel = new AddOrUpdateEmploymentDetailModel
            {
                BorrowerId = fakeApplication.Borrowers.First().Id,
                LoanApplicationId = fakeApplication.Id,
                EmployerAddress = new EmployerAddressModel
                {
                    LoanApplicationId = fakeApplication.Id,
                    StreetAddress = "StreetAddress",
                    StateName = "StateName",
                    State = "State",
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    CityId = 1,
                    CountryId = 1,
                    IncomeInfoId = null,
                    StateId = 1,
                    UnitNo = "1",
                    ZipCode = "1"
                },
                EmploymentInfo = new EmploymentInfo
                {
                    IncomeInfoId = 1,
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    EmployedByFamilyOrParty = true,
                    EmployerName = "fake current employer",
                    EmployerPhoneNumber = "123",
                    EndDate = null,
                    HasOwnershipInterest = true,
                    JobTitle = "job title",
                    OwnershipInterest = 20,
                    StartDate = DateTime.Now.AddDays(value: -100),
                    YearsInProfession = 10
                },
                EmploymentOtherIncomes = new List<EmploymentOtherAnnualIncome>
                                                                         {
                                                                             new EmploymentOtherAnnualIncome
                                                                             {
                                                                                 AnnualIncome = 12000,
                                                                                 IncomeTypeId = EmploymentOtherIncomeType.Bonus
                                                                             }
                                                                         },
                ErrorMessage = null,
                State = "state",
                WayOfIncome = new WayOfIncome
                {
                    EmployerAnnualSalary = 120000,
                    IsPaidByMonthlySalary = true,
                    HourlyRate = null,
                    HoursPerWeek = null
                },
            };



            List<OtherIncomeType> otherIncomeTypes = new List<OtherIncomeType>()
            {
                new OtherIncomeType()
                {
                    Id = (int) EmploymentOtherIncomeType.Bonus,
                    Name = "Bonus",
                    IsActive = true,
                    IsDeleted = false
                }
            };

            dbFunctionServiceMock.Setup(x => x.UdfOtherIncomeType(It.IsAny<int>(), It.IsAny<int?>())).Returns(otherIncomeTypes.AsQueryable());

            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);

            CurrentEmploymentDeleteModel fakeDeleteModel = new CurrentEmploymentDeleteModel()
            {
                BorrowerId = 10, // Wrong borrower id
                IncomeInfoId = 1,
                LoanApplicationId = fakeApplication.Id
            };

            // Act
            var results = await service.DeleteIncomeDetail(tenant: fakeTenant,
                                                                    userId: fakeUserId,
                                                                    fakeDeleteModel);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.Equal(EmploymentService.BorrowerDetailNotFound, results);
        }

        [Fact]
        public async Task TestDeleteIncomeDetailWhenBorrowerIncomeIsNull()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeUserId = 1;
            var fakeTenant = new TenantModel
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };

            var fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>
                                {
                                    new Borrower
                                    {
                                        TenantId = fakeTenant.Id,
                                        LoanContact_LoanContactId = new LoanContact
                                                      {
                                                          TenantId = fakeTenant.Id,
                                                          FirstName = "FirstName",
                                                          MiddleName = "MiddleName",
                                                          LastName = "LastName",
                                                          EmailAddress = "fake@email.com"
                                                      },
                                        IncomeInfoes = new List<IncomeInfo>()
                                        {
                                            new IncomeInfo()
                                            {
                                                AddressInfo = new AddressInfo()
                                                {
                                                    CityId = 1,
                                                    CityName = "fake city",
                                                    CountryId = 1,
                                                    CountryName = "fake country",
                                                    IsDeleted = false,
                                                    StateId = 1,
                                                    StateName = "fake state",
                                                    StreetAddress = " xyz address",
                                                    TenantId = fakeTenant.Id,
                                                    UnitNo = "xyz",
                                                    ZipCode = "123"
                                                },
                                                TenantId = fakeTenant.Id,
                                                OtherIncomeInfoes = new List<OtherIncomeInfo>()
                                                {
                                                    new OtherIncomeInfo()
                                                    {
                                                        TenantId = fakeTenant.Id
                                                    }
                                                }
                                            },
                                        }
                                    }
                                }
                };
            dataContext.Add(entity: fakeApplication);
            await dataContext.SaveChangesAsync();

            var fakeEmploymentDetailModel = new AddOrUpdateEmploymentDetailModel
            {
                BorrowerId = fakeApplication.Borrowers.First().Id,
                LoanApplicationId = fakeApplication.Id,
                EmployerAddress = new EmployerAddressModel
                {
                    LoanApplicationId = fakeApplication.Id,
                    StreetAddress = "StreetAddress",
                    StateName = "StateName",
                    State = "State",
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    CityId = 1,
                    CountryId = 1,
                    IncomeInfoId = null,
                    StateId = 1,
                    UnitNo = "1",
                    ZipCode = "1"
                },
                EmploymentInfo = new EmploymentInfo
                {
                    IncomeInfoId = 1,
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    EmployedByFamilyOrParty = true,
                    EmployerName = "fake current employer",
                    EmployerPhoneNumber = "123",
                    EndDate = null,
                    HasOwnershipInterest = true,
                    JobTitle = "job title",
                    OwnershipInterest = 20,
                    StartDate = DateTime.Now.AddDays(value: -100),
                    YearsInProfession = 10
                },
                EmploymentOtherIncomes = new List<EmploymentOtherAnnualIncome>
                                                                         {
                                                                             new EmploymentOtherAnnualIncome
                                                                             {
                                                                                 AnnualIncome = 12000,
                                                                                 IncomeTypeId = EmploymentOtherIncomeType.Bonus
                                                                             }
                                                                         },
                ErrorMessage = null,
                State = "state",
                WayOfIncome = new WayOfIncome
                {
                    EmployerAnnualSalary = 120000,
                    IsPaidByMonthlySalary = true,
                    HourlyRate = null,
                    HoursPerWeek = null
                },
            };



            List<OtherIncomeType> otherIncomeTypes = new List<OtherIncomeType>()
            {
                new OtherIncomeType()
                {
                    Id = (int) EmploymentOtherIncomeType.Bonus,
                    Name = "Bonus",
                    IsActive = true,
                    IsDeleted = false
                }
            };

            dbFunctionServiceMock.Setup(x => x.UdfOtherIncomeType(It.IsAny<int>(), It.IsAny<int?>())).Returns(otherIncomeTypes.AsQueryable());

            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);

            fakeApplication.Borrowers.First().IncomeInfoes = null;

            CurrentEmploymentDeleteModel fakeDeleteModel = new CurrentEmploymentDeleteModel()
            {
                BorrowerId = fakeApplication.Borrowers.First().Id, // Wrong borrower id
                IncomeInfoId = 1,
                LoanApplicationId = fakeApplication.Id
            };

            // Act
            var results = await service.DeleteIncomeDetail(tenant: fakeTenant,
                                                                    userId: fakeUserId,
                                                                    fakeDeleteModel);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.Equal(0, results);
        }

        [Fact]
        public async Task TestDeleteIncomeDetailWhenBorrowerIncomeNotFound()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeUserId = 1;
            var fakeTenant = new TenantModel
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };

            var fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>
                                {
                                    new Borrower
                                    {
                                        TenantId = fakeTenant.Id,
                                        LoanContact_LoanContactId = new LoanContact
                                                      {
                                                          TenantId = fakeTenant.Id,
                                                          FirstName = "FirstName",
                                                          MiddleName = "MiddleName",
                                                          LastName = "LastName",
                                                          EmailAddress = "fake@email.com"
                                                      },
                                        IncomeInfoes = new List<IncomeInfo>()
                                        {
                                            new IncomeInfo()
                                            {
                                                AddressInfo = new AddressInfo()
                                                {
                                                    CityId = 1,
                                                    CityName = "fake city",
                                                    CountryId = 1,
                                                    CountryName = "fake country",
                                                    IsDeleted = false,
                                                    StateId = 1,
                                                    StateName = "fake state",
                                                    StreetAddress = " xyz address",
                                                    TenantId = fakeTenant.Id,
                                                    UnitNo = "xyz",
                                                    ZipCode = "123"
                                                },
                                                TenantId = fakeTenant.Id,
                                                OtherIncomeInfoes = new List<OtherIncomeInfo>()
                                                {
                                                    new OtherIncomeInfo()
                                                    {
                                                        TenantId = fakeTenant.Id
                                                    }
                                                }
                                            },
                                        }
                                    }
                                }
                };
            dataContext.Add(entity: fakeApplication);
            await dataContext.SaveChangesAsync();

            var fakeEmploymentDetailModel = new AddOrUpdateEmploymentDetailModel
            {
                BorrowerId = fakeApplication.Borrowers.First().Id,
                LoanApplicationId = fakeApplication.Id,
                EmployerAddress = new EmployerAddressModel
                {
                    LoanApplicationId = fakeApplication.Id,
                    StreetAddress = "StreetAddress",
                    StateName = "StateName",
                    State = "State",
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    CityId = 1,
                    CountryId = 1,
                    IncomeInfoId = null,
                    StateId = 1,
                    UnitNo = "1",
                    ZipCode = "1"
                },
                EmploymentInfo = new EmploymentInfo
                {
                    IncomeInfoId = 1,
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    EmployedByFamilyOrParty = true,
                    EmployerName = "fake current employer",
                    EmployerPhoneNumber = "123",
                    EndDate = null,
                    HasOwnershipInterest = true,
                    JobTitle = "job title",
                    OwnershipInterest = 20,
                    StartDate = DateTime.Now.AddDays(value: -100),
                    YearsInProfession = 10
                },
                EmploymentOtherIncomes = new List<EmploymentOtherAnnualIncome>
                                                                         {
                                                                             new EmploymentOtherAnnualIncome
                                                                             {
                                                                                 AnnualIncome = 12000,
                                                                                 IncomeTypeId = EmploymentOtherIncomeType.Bonus
                                                                             }
                                                                         },
                ErrorMessage = null,
                State = "state",
                WayOfIncome = new WayOfIncome
                {
                    EmployerAnnualSalary = 120000,
                    IsPaidByMonthlySalary = true,
                    HourlyRate = null,
                    HoursPerWeek = null
                },
            };



            List<OtherIncomeType> otherIncomeTypes = new List<OtherIncomeType>()
            {
                new OtherIncomeType()
                {
                    Id = (int) EmploymentOtherIncomeType.Bonus,
                    Name = "Bonus",
                    IsActive = true,
                    IsDeleted = false
                }
            };

            dbFunctionServiceMock.Setup(x => x.UdfOtherIncomeType(It.IsAny<int>(), It.IsAny<int?>())).Returns(otherIncomeTypes.AsQueryable());

            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);

            CurrentEmploymentDeleteModel fakeDeleteModel = new CurrentEmploymentDeleteModel()
            {
                BorrowerId = fakeApplication.Borrowers.First().Id, // Wrong borrower id
                IncomeInfoId = fakeApplication.Borrowers.First().IncomeInfoes.First().Id + 10, // Temper income info id to mismatch
                LoanApplicationId = fakeApplication.Id
            };

            // Act
            var results = await service.DeleteIncomeDetail(tenant: fakeTenant,
                                                                    userId: fakeUserId,
                                                                    fakeDeleteModel);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.Equal(EmploymentService.BorrowerIncomeDetailNotFound, results);
        }

        [Fact]
        public async Task TestDeleteIncomeDetailWhenValidationPassed()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeUserId = 1;
            var fakeTenant = new TenantModel
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeCode",
                Id = 1,
                Urls = new List<UrlModel>()
            };

            var fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>
                                {
                                    new Borrower
                                    {
                                        TenantId = fakeTenant.Id,
                                        LoanContact_LoanContactId = new LoanContact
                                                      {
                                                          TenantId = fakeTenant.Id,
                                                          FirstName = "FirstName",
                                                          MiddleName = "MiddleName",
                                                          LastName = "LastName",
                                                          EmailAddress = "fake@email.com"
                                                      },
                                        IncomeInfoes = new List<IncomeInfo>()
                                        {
                                            new IncomeInfo()
                                            {
                                                AddressInfo = new AddressInfo()
                                                {
                                                    CityId = 1,
                                                    CityName = "fake city",
                                                    CountryId = 1,
                                                    CountryName = "fake country",
                                                    IsDeleted = false,
                                                    StateId = 1,
                                                    StateName = "fake state",
                                                    StreetAddress = " xyz address",
                                                    TenantId = fakeTenant.Id,
                                                    UnitNo = "xyz",
                                                    ZipCode = "123"
                                                },
                                                TenantId = fakeTenant.Id,
                                                OtherIncomeInfoes = new List<OtherIncomeInfo>()
                                                {
                                                    new OtherIncomeInfo()
                                                    {
                                                        TenantId = fakeTenant.Id
                                                    }
                                                }
                                            },
                                        }
                                    }
                                }
                };
            dataContext.Add(entity: fakeApplication);
            await dataContext.SaveChangesAsync();

            var fakeEmploymentDetailModel = new AddOrUpdateEmploymentDetailModel
            {
                BorrowerId = fakeApplication.Borrowers.First().Id,
                LoanApplicationId = fakeApplication.Id,
                EmployerAddress = new EmployerAddressModel
                {
                    LoanApplicationId = fakeApplication.Id,
                    StreetAddress = "StreetAddress",
                    StateName = "StateName",
                    State = "State",
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    CityId = 1,
                    CountryId = 1,
                    IncomeInfoId = null,
                    StateId = 1,
                    UnitNo = "1",
                    ZipCode = "1"
                },
                EmploymentInfo = new EmploymentInfo
                {
                    IncomeInfoId = 1,
                    BorrowerId = fakeApplication.Borrowers.First().Id,
                    EmployedByFamilyOrParty = true,
                    EmployerName = "fake current employer",
                    EmployerPhoneNumber = "123",
                    EndDate = null,
                    HasOwnershipInterest = true,
                    JobTitle = "job title",
                    OwnershipInterest = 20,
                    StartDate = DateTime.Now.AddDays(value: -100),
                    YearsInProfession = 10
                },
                EmploymentOtherIncomes = new List<EmploymentOtherAnnualIncome>
                                                                         {
                                                                             new EmploymentOtherAnnualIncome
                                                                             {
                                                                                 AnnualIncome = 12000,
                                                                                 IncomeTypeId = EmploymentOtherIncomeType.Bonus
                                                                             }
                                                                         },
                ErrorMessage = null,
                State = "state",
                WayOfIncome = new WayOfIncome
                {
                    EmployerAnnualSalary = 120000,
                    IsPaidByMonthlySalary = true,
                    HourlyRate = null,
                    HoursPerWeek = null
                },
            };



            List<OtherIncomeType> otherIncomeTypes = new List<OtherIncomeType>()
            {
                new OtherIncomeType()
                {
                    Id = (int) EmploymentOtherIncomeType.Bonus,
                    Name = "Bonus",
                    IsActive = true,
                    IsDeleted = false
                }
            };

            dbFunctionServiceMock.Setup(x => x.UdfOtherIncomeType(It.IsAny<int>(), It.IsAny<int?>())).Returns(otherIncomeTypes.AsQueryable());

            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);

            CurrentEmploymentDeleteModel fakeDeleteModel = new CurrentEmploymentDeleteModel()
            {
                BorrowerId = fakeApplication.Borrowers.First().Id, // Wrong borrower id
                IncomeInfoId = fakeApplication.Borrowers.First().IncomeInfoes.First().Id,
                LoanApplicationId = fakeApplication.Id
            };

            // Act
            var results = await service.DeleteIncomeDetail(tenant: fakeTenant,
                                                                    userId: fakeUserId,
                                                                    fakeDeleteModel);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.Equal(1, results);
        }


        [Fact]
        public async Task GetEmploymentDetail_Called_1()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> options;
            var builder = new DbContextOptionsBuilder<LoanApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            using var dataContext = new LoanApplicationContext(options: options);
            dataContext.Database.EnsureCreated();
            await dataContext.SaveChangesAsync();

            var fakeUserId = 1;
            var fakeTenant = new TenantModel
                             {
                                 Branches = new EditableList<BranchModel>(),
                                 Code = "fakeCode",
                                 Id = 1,
                                 Urls = new List<UrlModel>()
                             };

            var fakeApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenant.Id,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>
                                {
                                    new Borrower
                                    {
                                        TenantId = fakeTenant.Id,
                                        LoanContact_LoanContactId = new LoanContact
                                                      {
                                                          TenantId = fakeTenant.Id,
                                                          FirstName = "FirstName",
                                                          MiddleName = "MiddleName",
                                                          LastName = "LastName",
                                                          EmailAddress = "fake@email.com"
                                                      }
                                    }
                                }
                };
            dataContext.Add(entity: fakeApplication);
            await dataContext.SaveChangesAsync();

            var fakeEmploymentDetailModel = new AddOrUpdateEmploymentDetailModel
                                            {
                                                BorrowerId = fakeApplication.Borrowers.First().Id,
                                                LoanApplicationId = fakeApplication.Id,
                                                EmployerAddress = new EmployerAddressModel
                                                                  {
                                                                      LoanApplicationId = fakeApplication.Id,
                                                                      StreetAddress = "StreetAddress",
                                                                      StateName = "StateName",
                                                                      State = "State",
                                                                      BorrowerId = fakeApplication.Borrowers.First().Id,
                                                                      CityId = 1,
                                                                      CountryId = 1,
                                                                      IncomeInfoId = null,
                                                                      StateId = 1,
                                                                      UnitNo = "1",
                                                                      ZipCode = "1"
                                                                  },
                                                EmploymentInfo = new EmploymentInfo
                                                                 {
                                                                     IncomeInfoId = 1,
                                                                     BorrowerId = fakeApplication.Borrowers.First().Id,
                                                                     EmployedByFamilyOrParty = true,
                                                                     EmployerName = "employerName",
                                                                     EmployerPhoneNumber = "123",
                                                                     EndDate = null,
                                                                     HasOwnershipInterest = true,
                                                                     JobTitle = "job title",
                                                                     OwnershipInterest = 20,
                                                                     StartDate = DateTime.Now.AddDays(value: -100),
                                                                     YearsInProfession = 10
                                                                 },
                                                EmploymentOtherIncomes = new List<EmploymentOtherAnnualIncome>
                                                                         {
                                                                             new EmploymentOtherAnnualIncome
                                                                             {
                                                                                 AnnualIncome = 12000,
                                                                                 IncomeTypeId = EmploymentOtherIncomeType.Bonus
                                                                             }
                                                                         },
                                                ErrorMessage = null,
                                                State = "state",
                                                WayOfIncome = new WayOfIncome
                                                              {
                                                                  EmployerAnnualSalary = 120000,
                                                                  IsPaidByMonthlySalary = true,
                                                                  HourlyRate = null,
                                                                  HoursPerWeek = null
                                                              },
                                            };

            // Act
            var service = new EmploymentService(uow: new UnitOfWork<LoanApplicationContext>(context: dataContext,
                                                                                            repositoryProvider: new RepositoryProvider(repositoryFactories: new RepositoryFactories())),
                                                services: serviceProviderMock.Object,
                                                logger: _loggerServiceMock.Object,
                                                dbFunctionService: dbFunctionServiceMock.Object);
            var results = service.GetEmploymentDetail(tenant: fakeTenant,
                                                      userId: fakeUserId,
                                                      loanApplicationId: fakeApplication.Id,
                                                      borrowerId: fakeApplication.Borrowers.First().Id,
                                                      incomeInfoId: 1);

            await dataContext.Database.EnsureDeletedAsync();

            //Assert
            Assert.NotNull(@object: results);
        }


    }
}