using System;
using System.Collections.Generic;
using System.Text;
using Castle.Components.DictionaryAdapter;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanApplication.Model;
using LoanApplication.Service;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http;

namespace LoanApplication.Tests
{
    public class IncomeServiceTest
    {
        //[Fact]
        //public async Task TestAddOrUpdateOtherAnnualIncomeWithDescription_CalledNormally_OK()
        //{
        //    // Arrange
        //    Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
        //    Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
        //    Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
        //    #region LoanApplicationContext
        //    DbContextOptions<LoanApplicationContext> loanOptions;
        //    var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
        //    loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        //    loanOptions = loanBuilder.Options;
        //    using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
        //    applicationContext.Database.EnsureCreated();
        //    await applicationContext.SaveChangesAsync();

        //    int fakeTenantId = 1;
        //    TenantModel fakeTenant = new TenantModel()
        //    {
        //        Branches = new EditableList<BranchModel>(),
        //        Code = "fakeTenantCode",
        //        Id = fakeTenantId,
        //        Urls = new List<UrlModel>()
        //    };

        //    int fakeUserId = 1;
        //    LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
        //        new LoanApplicationDb.Entity.Models.LoanApplication()
        //        {
        //            CreatedOnUtc = DateTime.UtcNow,
        //            LoanPurposeId = 1,
        //            TenantId = fakeTenantId,
        //            UserId = fakeUserId,
        //            Borrowers = new List<Borrower>()
        //            {
        //                    new Borrower()
        //                    {
        //                        TenantId = 1,
        //                        OwnTypeId = 1, // Primary
        //                        LoanContact = new LoanContact()
        //                        {
        //                            TenantId = fakeTenant.Id,
        //                            FirstName = "PrimaryFirstName",
        //                            MiddleName = "PrimaryMiddleName",
        //                            LastName = "PrimaryLastName",
        //                            EmailAddress = "primaryborrower@emailinator.com",
        //                            MaritalStatusId = (int)MaritalStatus.Married
        //                        },
        //                       BorrowerResidences=new List<BorrowerResidence>()
        //                       {
        //                            new BorrowerResidence()
        //                            {
        //                                 TenantId=fakeTenant.Id,
        //                                 LoanAddressId=1,
        //                                 TypeId=1,
        //                                  LoanAddress= new AddressInfo()
        //                                 {
        //                                     CountryId=1,
        //                                     CountryName="fake country name",
        //                                     StateId=1,
        //                                     StateName="fake state name",
        //                                     CityId=1,
        //                                     CityName="fake city name",
        //                                     StreetAddress="street address",
        //                                     ZipCode="fake cip code",
        //                                    UnitNo="fake uint no",

        //                                 }

        //                            },


        //                       },
        //                       IncomeInfoes= new List< IncomeInfo> ()
        //                       {
        //                            new IncomeInfo()
        //                            {

        //                                    Id=1,
        //                                    BorrowerId=1
        //                            }

        //                       }

        //                    },
        //                    new Borrower()
        //                    {
        //                        TenantId = 1,
        //                        OwnTypeId = 2, // Secondary
        //                        LoanContact = new LoanContact()
        //                        {
        //                            TenantId = fakeTenant.Id,
        //                            FirstName = "SecondaryFirstName",
        //                            MiddleName = "SecondaryMiddleName",
        //                            LastName = "SecondaryLastName",
        //                            EmailAddress = "secondaryborrower@emailinator.com",
        //                            MaritalStatusId = (int)MaritalStatus.Married
        //                        },

        //                       BorrowerResidences=new List<BorrowerResidence>()
        //                       {
        //                            new BorrowerResidence()
        //                            {
        //                                 TenantId=fakeTenant.Id,
        //                                 LoanAddressId=1,
        //                                 TypeId=1,
        //                                 LoanAddress= new AddressInfo()
        //                                 {

        //                                     CountryId=1,
        //                                     CountryName="fake country name",
        //                                     StateId=1,
        //                                     StateName="fake state name",
        //                                     CityId=1,
        //                                     CityName="fake city name",
        //                                     StreetAddress="street address",
        //                                     ZipCode="fake cip code",
        //                                    UnitNo="fake uint no",


        //                                 }


        //                            }

        //                       },
        //                        IncomeInfoes= new List< IncomeInfo> ()
        //                       {
        //                            new IncomeInfo()
        //                            {

        //                                    Id=2,
        //                                    BorrowerId=1
        //                            }

        //                       }

        //                    },

        //             },


        //        };

        //    LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
        //    {

        //        LoanApplicationId = 1
        //    };
        //    #endregion

        //    #region  TenantConfigContext
        //    DbContextOptions<TenantConfigContext> options;
        //    var builder = new DbContextOptionsBuilder<TenantConfigContext>();
        //    builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
        //    options = builder.Options;
        //    using TenantConfigContext dataContext = new TenantConfigContext(options);
        //    dataContext.Database.EnsureCreated();
        //    #endregion

        //    UpdateOtherAnnualIncomeWithDescriptionModel fakeModel = new UpdateOtherAnnualIncomeWithDescriptionModel()
        //    {
        //        BorrowerId = 1,
        //        IncomeInfoId = 1,
        //        AnnualBaseIncome = 10000,
        //        Description = "fake descritption"
        //    };

        //    applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
        //    await applicationContext.SaveChangesAsync();
        //    var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
        //                                                                   serviceProviderMock.Object, loggerIncomeServiceMock.Object, dbFunctionServiceMock.Object);


        //    var results = await service.AddOrUpdateOtherAnnualIncomeWithDescription(fakeTenant, fakeUserId, fakeModel);

        //    await applicationContext.Database.EnsureDeletedAsync();

        //    // Assert
        //    Assert.NotNull(results);

        //    Assert.Equal(1, results);

        //}

        //[Fact]
        //public async Task TestAddOrUpdateOtherAnnualIncomeWithDescription_loanApplicationNull_MinusOne()
        //{
        //    // Arrange
        //    Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
        //    Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
        //    Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
        //    #region LoanApplicationContext
        //    DbContextOptions<LoanApplicationContext> loanOptions;
        //    var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
        //    loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        //    loanOptions = loanBuilder.Options;
        //    using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
        //    applicationContext.Database.EnsureCreated();
        //    await applicationContext.SaveChangesAsync();

        //    int fakeTenantId = 1;
        //    TenantModel fakeTenant = new TenantModel()
        //    {
        //        Branches = new EditableList<BranchModel>(),
        //        Code = "fakeTenantCode",
        //        Id = fakeTenantId,
        //        Urls = new List<UrlModel>()
        //    };

        //    int fakeUserId = 1;
        //    LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
        //        new LoanApplicationDb.Entity.Models.LoanApplication()
        //        {



        //        };

        //    LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
        //    {

        //        LoanApplicationId = 1
        //    };
        //    #endregion

        //    #region  TenantConfigContext
        //    DbContextOptions<TenantConfigContext> options;
        //    var builder = new DbContextOptionsBuilder<TenantConfigContext>();
        //    builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
        //    options = builder.Options;
        //    using TenantConfigContext dataContext = new TenantConfigContext(options);
        //    dataContext.Database.EnsureCreated();
        //    #endregion

        //    UpdateOtherAnnualIncomeWithDescriptionModel fakeModel = new UpdateOtherAnnualIncomeWithDescriptionModel()
        //    {
        //        BorrowerId = 1,
        //        IncomeInfoId = 1,
        //        AnnualBaseIncome = 10000,
        //        Description = "fake descritption"
        //    };

        //    applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
        //    await applicationContext.SaveChangesAsync();
        //    var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
        //                                                                   serviceProviderMock.Object, loggerIncomeServiceMock.Object, dbFunctionServiceMock.Object);


        //    var results = await service.AddOrUpdateOtherAnnualIncomeWithDescription(fakeTenant, fakeUserId, fakeModel);

        //    await applicationContext.Database.EnsureDeletedAsync();

        //    // Assert
        //    Assert.NotNull(results);

        //    Assert.Equal(-1, results);

        //}

        //[Fact]
        //public async Task TestAddOrUpdateOtherAnnualIncomeWithDescription_IncomeInfoNull_AddedAndOk()
        //{
        //    // Arrange
        //    Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
        //    Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
        //    Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
        //    #region LoanApplicationContext
        //    DbContextOptions<LoanApplicationContext> loanOptions;
        //    var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
        //    loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        //    loanOptions = loanBuilder.Options;
        //    using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
        //    applicationContext.Database.EnsureCreated();
        //    await applicationContext.SaveChangesAsync();

        //    int fakeTenantId = 1;
        //    TenantModel fakeTenant = new TenantModel()
        //    {
        //        Branches = new EditableList<BranchModel>(),
        //        Code = "fakeTenantCode",
        //        Id = fakeTenantId,
        //        Urls = new List<UrlModel>()
        //    };

        //    int fakeUserId = 1;
        //    LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
        //        new LoanApplicationDb.Entity.Models.LoanApplication()
        //        {
        //            CreatedOnUtc = DateTime.UtcNow,
        //            LoanPurposeId = 1,
        //            TenantId = fakeTenantId,
        //            UserId = fakeUserId,
        //            Borrowers = new List<Borrower>()
        //            {
        //                    new Borrower()
        //                    {
        //                        TenantId = 1,
        //                        OwnTypeId = 1, // Primary
        //                        LoanContact = new LoanContact()
        //                        {
        //                            TenantId = fakeTenant.Id,
        //                            FirstName = "PrimaryFirstName",
        //                            MiddleName = "PrimaryMiddleName",
        //                            LastName = "PrimaryLastName",
        //                            EmailAddress = "primaryborrower@emailinator.com",
        //                            MaritalStatusId = (int)MaritalStatus.Married
        //                        },
        //                       BorrowerResidences=new List<BorrowerResidence>()
        //                       {
        //                            new BorrowerResidence()
        //                            {
        //                                 TenantId=fakeTenant.Id,
        //                                 LoanAddressId=1,
        //                                 TypeId=1,
        //                                  LoanAddress= new AddressInfo()
        //                                 {
        //                                     CountryId=1,
        //                                     CountryName="fake country name",
        //                                     StateId=1,
        //                                     StateName="fake state name",
        //                                     CityId=1,
        //                                     CityName="fake city name",
        //                                     StreetAddress="street address",
        //                                     ZipCode="fake cip code",
        //                                    UnitNo="fake uint no",

        //                                 }

        //                            },


        //                       }


        //                    },
        //                    new Borrower()
        //                    {
        //                        TenantId = 1,
        //                        OwnTypeId = 2, // Secondary
        //                        LoanContact = new LoanContact()
        //                        {
        //                            TenantId = fakeTenant.Id,
        //                            FirstName = "SecondaryFirstName",
        //                            MiddleName = "SecondaryMiddleName",
        //                            LastName = "SecondaryLastName",
        //                            EmailAddress = "secondaryborrower@emailinator.com",
        //                            MaritalStatusId = (int)MaritalStatus.Married
        //                        },

        //                       BorrowerResidences=new List<BorrowerResidence>()
        //                       {
        //                            new BorrowerResidence()
        //                            {
        //                                 TenantId=fakeTenant.Id,
        //                                 LoanAddressId=1,
        //                                 TypeId=1,
        //                                 LoanAddress= new AddressInfo()
        //                                 {

        //                                     CountryId=1,
        //                                     CountryName="fake country name",
        //                                     StateId=1,
        //                                     StateName="fake state name",
        //                                     CityId=1,
        //                                     CityName="fake city name",
        //                                     StreetAddress="street address",
        //                                     ZipCode="fake cip code",
        //                                    UnitNo="fake uint no",


        //                                 }


        //                            }

        //                       }

        //                    },

        //             },


        //        };

        //    LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
        //    {

        //        LoanApplicationId = 1
        //    };
        //    #endregion

        //    #region  TenantConfigContext
        //    DbContextOptions<TenantConfigContext> options;
        //    var builder = new DbContextOptionsBuilder<TenantConfigContext>();
        //    builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
        //    options = builder.Options;
        //    using TenantConfigContext dataContext = new TenantConfigContext(options);
        //    dataContext.Database.EnsureCreated();
        //    #endregion

        //    UpdateOtherAnnualIncomeWithDescriptionModel fakeModel = new UpdateOtherAnnualIncomeWithDescriptionModel()
        //    {
        //        BorrowerId = 1,
        //        IncomeInfoId = 1,
        //        AnnualBaseIncome = 10000,
        //        Description = "fake descritption"
        //    };

        //    applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
        //    await applicationContext.SaveChangesAsync();
        //    var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
        //                                                                   serviceProviderMock.Object, loggerIncomeServiceMock.Object, dbFunctionServiceMock.Object);


        //    var results = await service.AddOrUpdateOtherAnnualIncomeWithDescription(fakeTenant, fakeUserId, fakeModel);

        //    await applicationContext.Database.EnsureDeletedAsync();

        //    // Assert
        //    Assert.NotNull(results);

        //    Assert.Equal(1, results);

        //}

        //[Fact]
        //public async Task TestAddOrUpdateEmployerYearlyIncome_CalledNormally_Ok()
        //{
        //    // Arrange
        //    Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
        //    Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
        //    Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
        //    #region LoanApplicationContext
        //    DbContextOptions<LoanApplicationContext> loanOptions;
        //    var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
        //    loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        //    loanOptions = loanBuilder.Options;
        //    using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
        //    applicationContext.Database.EnsureCreated();
        //    await applicationContext.SaveChangesAsync();

        //    int fakeTenantId = 1;
        //    TenantModel fakeTenant = new TenantModel()
        //    {
        //        Branches = new EditableList<BranchModel>(),
        //        Code = "fakeTenantCode",
        //        Id = fakeTenantId,
        //        Urls = new List<UrlModel>()
        //    };

        //    int fakeUserId = 1;
        //    LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
        //        new LoanApplicationDb.Entity.Models.LoanApplication()
        //        {
        //            CreatedOnUtc = DateTime.UtcNow,
        //            LoanPurposeId = 1,
        //            TenantId = fakeTenantId,
        //            UserId = fakeUserId,
        //            Borrowers = new List<Borrower>()
        //            {
        //                    new Borrower()
        //                    {
        //                        TenantId = 1,
        //                        OwnTypeId = 1, // Primary
        //                        LoanContact = new LoanContact()
        //                        {
        //                            TenantId = fakeTenant.Id,
        //                            FirstName = "PrimaryFirstName",
        //                            MiddleName = "PrimaryMiddleName",
        //                            LastName = "PrimaryLastName",
        //                            EmailAddress = "primaryborrower@emailinator.com",
        //                            MaritalStatusId = (int)MaritalStatus.Married
        //                        },
        //                       BorrowerResidences=new List<BorrowerResidence>()
        //                       {
        //                            new BorrowerResidence()
        //                            {
        //                                 TenantId=fakeTenant.Id,
        //                                 LoanAddressId=1,
        //                                 TypeId=1,
        //                                  LoanAddress= new AddressInfo()
        //                                 {
        //                                     CountryId=1,
        //                                     CountryName="fake country name",
        //                                     StateId=1,
        //                                     StateName="fake state name",
        //                                     CityId=1,
        //                                     CityName="fake city name",
        //                                     StreetAddress="street address",
        //                                     ZipCode="fake cip code",
        //                                    UnitNo="fake uint no",

        //                                 }

        //                            },


        //                       },

        //                       IncomeInfoes= new List< IncomeInfo> ()
        //                       {
        //                            new IncomeInfo()
        //                            {

        //                                    Id=1,
        //                                    BorrowerId=1
        //                            }

        //                       }


        //                    },
        //                    new Borrower()
        //                    {
        //                        TenantId = 1,
        //                        OwnTypeId = 2, // Secondary
        //                        LoanContact = new LoanContact()
        //                        {
        //                            TenantId = fakeTenant.Id,
        //                            FirstName = "SecondaryFirstName",
        //                            MiddleName = "SecondaryMiddleName",
        //                            LastName = "SecondaryLastName",
        //                            EmailAddress = "secondaryborrower@emailinator.com",
        //                            MaritalStatusId = (int)MaritalStatus.Married
        //                        },

        //                       BorrowerResidences=new List<BorrowerResidence>()
        //                       {
        //                            new BorrowerResidence()
        //                            {
        //                                 TenantId=fakeTenant.Id,
        //                                 LoanAddressId=1,
        //                                 TypeId=1,
        //                                 LoanAddress= new AddressInfo()
        //                                 {

        //                                     CountryId=1,
        //                                     CountryName="fake country name",
        //                                     StateId=1,
        //                                     StateName="fake state name",
        //                                     CityId=1,
        //                                     CityName="fake city name",
        //                                     StreetAddress="street address",
        //                                     ZipCode="fake cip code",
        //                                    UnitNo="fake uint no",


        //                                 }


        //                            }

        //                       },

        //                       IncomeInfoes= new List< IncomeInfo> ()
        //                       {
        //                            new IncomeInfo()
        //                            {

        //                                    Id=2,
        //                                    BorrowerId=1
        //                            }

        //                       }

        //                    },

        //             },


        //        };

        //    LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
        //    {

        //        LoanApplicationId = 1
        //    };
        //    #endregion

        //    #region  TenantConfigContext
        //    DbContextOptions<TenantConfigContext> options;
        //    var builder = new DbContextOptionsBuilder<TenantConfigContext>();
        //    builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
        //    options = builder.Options;
        //    using TenantConfigContext dataContext = new TenantConfigContext(options);
        //    dataContext.Database.EnsureCreated();
        //    #endregion

        //    UpdateOtherAnnualIncomeModel fakeModel = new UpdateOtherAnnualIncomeModel()
        //    {
        //        BorrowerId = 1,
        //        IncomeInfoId = 1,
        //        AnnualBaseIncome = 10000,

        //    };

        //    applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
        //    await applicationContext.SaveChangesAsync();
        //    var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
        //                                                                   serviceProviderMock.Object, loggerIncomeServiceMock.Object, dbFunctionServiceMock.Object);


        //    var results = await service.AddOrUpdateEmployerYearlyIncome(fakeTenant, fakeUserId, fakeModel);

        //    await applicationContext.Database.EnsureDeletedAsync();

        //    // Assert
        //    Assert.NotNull(results);

        //    Assert.Equal(1, results);

        //}

        //[Fact]
        //public async Task TestAddOrUpdateEmployerYearlyIncome_loanApplicationNull_MinusOne()
        //{
        //    // Arrange
        //    Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
        //    Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
        //    Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
        //    #region LoanApplicationContext
        //    DbContextOptions<LoanApplicationContext> loanOptions;
        //    var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
        //    loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        //    loanOptions = loanBuilder.Options;
        //    using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
        //    applicationContext.Database.EnsureCreated();
        //    await applicationContext.SaveChangesAsync();

        //    int fakeTenantId = 1;
        //    TenantModel fakeTenant = new TenantModel()
        //    {
        //        Branches = new EditableList<BranchModel>(),
        //        Code = "fakeTenantCode",
        //        Id = fakeTenantId,
        //        Urls = new List<UrlModel>()
        //    };

        //    int fakeUserId = 1;
        //    LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
        //        new LoanApplicationDb.Entity.Models.LoanApplication()
        //        {



        //        };

        //    LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
        //    {

        //        LoanApplicationId = 1
        //    };
        //    #endregion

        //    #region  TenantConfigContext
        //    DbContextOptions<TenantConfigContext> options;
        //    var builder = new DbContextOptionsBuilder<TenantConfigContext>();
        //    builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
        //    options = builder.Options;
        //    using TenantConfigContext dataContext = new TenantConfigContext(options);
        //    dataContext.Database.EnsureCreated();
        //    #endregion

        //    UpdateOtherAnnualIncomeModel fakeModel = new UpdateOtherAnnualIncomeModel()
        //    {
        //        BorrowerId = 1,
        //        IncomeInfoId = 1,
        //        AnnualBaseIncome = 10000,

        //    };

        //    applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
        //    await applicationContext.SaveChangesAsync();
        //    var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
        //                                                                   serviceProviderMock.Object, loggerIncomeServiceMock.Object, dbFunctionServiceMock.Object);


        //    var results = await service.AddOrUpdateEmployerYearlyIncome(fakeTenant, fakeUserId, fakeModel);

        //    await applicationContext.Database.EnsureDeletedAsync();

        //    // Assert
        //    Assert.NotNull(results);

        //    Assert.Equal(-1, results);

        //}

        //[Fact]
        //public async Task TestAddOrUpdateEmployerYearlyIncome_IncomeInfoNull_AddedAndOk()
        //{
        //    // Arrange
        //    Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
        //    Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
        //    Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
        //    #region LoanApplicationContext
        //    DbContextOptions<LoanApplicationContext> loanOptions;
        //    var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
        //    loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        //    loanOptions = loanBuilder.Options;
        //    using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
        //    applicationContext.Database.EnsureCreated();
        //    await applicationContext.SaveChangesAsync();

        //    int fakeTenantId = 1;
        //    TenantModel fakeTenant = new TenantModel()
        //    {
        //        Branches = new EditableList<BranchModel>(),
        //        Code = "fakeTenantCode",
        //        Id = fakeTenantId,
        //        Urls = new List<UrlModel>()
        //    };

        //    int fakeUserId = 1;
        //    LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
        //        new LoanApplicationDb.Entity.Models.LoanApplication()
        //        {
        //            CreatedOnUtc = DateTime.UtcNow,
        //            LoanPurposeId = 1,
        //            TenantId = fakeTenantId,
        //            UserId = fakeUserId,
        //            Borrowers = new List<Borrower>()
        //            {
        //                    new Borrower()
        //                    {
        //                        TenantId = 1,
        //                        OwnTypeId = 1, // Primary
        //                        LoanContact = new LoanContact()
        //                        {
        //                            TenantId = fakeTenant.Id,
        //                            FirstName = "PrimaryFirstName",
        //                            MiddleName = "PrimaryMiddleName",
        //                            LastName = "PrimaryLastName",
        //                            EmailAddress = "primaryborrower@emailinator.com",
        //                            MaritalStatusId = (int)MaritalStatus.Married
        //                        },
        //                       BorrowerResidences=new List<BorrowerResidence>()
        //                       {
        //                            new BorrowerResidence()
        //                            {
        //                                 TenantId=fakeTenant.Id,
        //                                 LoanAddressId=1,
        //                                 TypeId=1,
        //                                  LoanAddress= new AddressInfo()
        //                                 {
        //                                     CountryId=1,
        //                                     CountryName="fake country name",
        //                                     StateId=1,
        //                                     StateName="fake state name",
        //                                     CityId=1,
        //                                     CityName="fake city name",
        //                                     StreetAddress="street address",
        //                                     ZipCode="fake cip code",
        //                                    UnitNo="fake uint no",

        //                                 }

        //                            },


        //                       }


        //                    },
        //                    new Borrower()
        //                    {
        //                        TenantId = 1,
        //                        OwnTypeId = 2, // Secondary
        //                        LoanContact = new LoanContact()
        //                        {
        //                            TenantId = fakeTenant.Id,
        //                            FirstName = "SecondaryFirstName",
        //                            MiddleName = "SecondaryMiddleName",
        //                            LastName = "SecondaryLastName",
        //                            EmailAddress = "secondaryborrower@emailinator.com",
        //                            MaritalStatusId = (int)MaritalStatus.Married
        //                        },

        //                       BorrowerResidences=new List<BorrowerResidence>()
        //                       {
        //                            new BorrowerResidence()
        //                            {
        //                                 TenantId=fakeTenant.Id,
        //                                 LoanAddressId=1,
        //                                 TypeId=1,
        //                                 LoanAddress= new AddressInfo()
        //                                 {

        //                                     CountryId=1,
        //                                     CountryName="fake country name",
        //                                     StateId=1,
        //                                     StateName="fake state name",
        //                                     CityId=1,
        //                                     CityName="fake city name",
        //                                     StreetAddress="street address",
        //                                     ZipCode="fake cip code",
        //                                    UnitNo="fake uint no",


        //                                 }


        //                            }

        //                       }

        //                    },

        //             },


        //        };

        //    LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
        //    {

        //        LoanApplicationId = 1
        //    };
        //    #endregion

        //    #region  TenantConfigContext
        //    DbContextOptions<TenantConfigContext> options;
        //    var builder = new DbContextOptionsBuilder<TenantConfigContext>();
        //    builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
        //    options = builder.Options;
        //    using TenantConfigContext dataContext = new TenantConfigContext(options);
        //    dataContext.Database.EnsureCreated();
        //    #endregion

        //    UpdateOtherAnnualIncomeModel fakeModel = new UpdateOtherAnnualIncomeModel()
        //    {
        //        BorrowerId = 1,
        //        IncomeInfoId = 1,
        //        AnnualBaseIncome = 10000,

        //    };

        //    applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
        //    await applicationContext.SaveChangesAsync();
        //    var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
        //                                                                   serviceProviderMock.Object, loggerIncomeServiceMock.Object, dbFunctionServiceMock.Object);


        //    var results = await service.AddOrUpdateEmployerYearlyIncome(fakeTenant, fakeUserId, fakeModel);

        //    await applicationContext.Database.EnsureDeletedAsync();

        //    // Assert
        //    Assert.NotNull(results);

        //    Assert.Equal(1, results);

        //}

        [Fact]
        public async Task TestAddOrUpdateEmployerOtherMonthyIncome_CalledNormally_Ok()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenantId,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>()
                    {
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 1, // Primary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "PrimaryFirstName",
                                    MiddleName = "PrimaryMiddleName",
                                    LastName = "PrimaryLastName",
                                    EmailAddress = "primaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                               BorrowerResidences=new List<BorrowerResidence>()
                               {
                                    new BorrowerResidence()
                                    {
                                         TenantId=fakeTenant.Id,
                                         LoanAddressId=1,
                                         TypeId=1,
                                          LoanAddress= new AddressInfo()
                                         {
                                             CountryId=1,
                                             CountryName="fake country name",
                                             StateId=1,
                                             StateName="fake state name",
                                             CityId=1,
                                             CityName="fake city name",
                                             StreetAddress="street address",
                                             ZipCode="fake cip code",
                                            UnitNo="fake uint no",

                                         }

                                    },


                               }
                               ,
                               IncomeInfoes= new List< IncomeInfo> ()
                               {
                                    new IncomeInfo()
                                    {

                                            Id=1,
                                            BorrowerId=1
                                    }

                               }

                            },
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 2, // Secondary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "SecondaryFirstName",
                                    MiddleName = "SecondaryMiddleName",
                                    LastName = "SecondaryLastName",
                                    EmailAddress = "secondaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },

                               BorrowerResidences=new List<BorrowerResidence>()
                               {
                                    new BorrowerResidence()
                                    {
                                         TenantId=fakeTenant.Id,
                                         LoanAddressId=1,
                                         TypeId=1,
                                         LoanAddress= new AddressInfo()
                                         {

                                             CountryId=1,
                                             CountryName="fake country name",
                                             StateId=1,
                                             StateName="fake state name",
                                             CityId=1,
                                             CityName="fake city name",
                                             StreetAddress="street address",
                                             ZipCode="fake cip code",
                                            UnitNo="fake uint no",


                                         }


                                    }

                               }
                               ,
                               IncomeInfoes= new List< IncomeInfo> ()
                               {
                                    new IncomeInfo()
                                    {

                                            Id=2,
                                            BorrowerId=1
                                    }

                               }
                            },

                     },


                };

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

            AddOrUpdateOtherIncomeModel fakeModel = new AddOrUpdateOtherIncomeModel()
            {
                BorrowerId = 1,
                IncomeInfoId = 1,
                MonthlyBaseIncome = 10000,
                LoanApplicationId = 1

            };

            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, loggerIncomeServiceMock.Object, dbFunctionServiceMock.Object);


            var results = await service.AddOrUpdateEmployerOtherMonthyIncome(fakeTenant, fakeUserId, fakeModel);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);

            Assert.Equal(1, results);

        }

        [Fact]
        public async Task TestAddOrUpdateEmployerOtherMonthyIncome_loanApplicationNull_MinusOne()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    


                };

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

            AddOrUpdateOtherIncomeModel fakeModel = new AddOrUpdateOtherIncomeModel()
            {
                BorrowerId = 1,
                IncomeInfoId = 1,
                MonthlyBaseIncome = 10000,

            };

            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, loggerIncomeServiceMock.Object, dbFunctionServiceMock.Object);


            var results = await service.AddOrUpdateEmployerOtherMonthyIncome(fakeTenant, fakeUserId, fakeModel);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);

            Assert.Equal(-1, results);

        }

        [Fact]
        public async Task TestAddOrUpdateEmployerOtherMonthyIncome_IncomeInfo_AddedAndOk()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenantId,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 1, // Primary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "PrimaryFirstName",
                                    MiddleName = "PrimaryMiddleName",
                                    LastName = "PrimaryLastName",
                                    EmailAddress = "primaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                               BorrowerResidences=new List<BorrowerResidence>()
                               {
                                    new BorrowerResidence()
                                    {
                                         TenantId=fakeTenant.Id,
                                         LoanAddressId=1,
                                         TypeId=1,
                                          LoanAddress= new AddressInfo()
                                         {
                                             CountryId=1,
                                             CountryName="fake country name",
                                             StateId=1,
                                             StateName="fake state name",
                                             CityId=1,
                                             CityName="fake city name",
                                             StreetAddress="street address",
                                             ZipCode="fake cip code",
                                            UnitNo="fake uint no",

                                         }

                                    },


                               }
                              

                            },
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 2, // Secondary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "SecondaryFirstName",
                                    MiddleName = "SecondaryMiddleName",
                                    LastName = "SecondaryLastName",
                                    EmailAddress = "secondaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },

                               BorrowerResidences=new List<BorrowerResidence>()
                               {
                                    new BorrowerResidence()
                                    {
                                         TenantId=fakeTenant.Id,
                                         LoanAddressId=1,
                                         TypeId=1,
                                         LoanAddress= new AddressInfo()
                                         {

                                             CountryId=1,
                                             CountryName="fake country name",
                                             StateId=1,
                                             StateName="fake state name",
                                             CityId=1,
                                             CityName="fake city name",
                                             StreetAddress="street address",
                                             ZipCode="fake cip code",
                                            UnitNo="fake uint no",


                                         }


                                    }

                               }
                              
                            },

                   },


              };

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

            AddOrUpdateOtherIncomeModel fakeModel = new AddOrUpdateOtherIncomeModel()
            {
                BorrowerId = 1,
                IncomeInfoId = 1,
                MonthlyBaseIncome = 10000,
                LoanApplicationId = 1

            };

            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, loggerIncomeServiceMock.Object, dbFunctionServiceMock.Object);


            var results = await service.AddOrUpdateEmployerOtherMonthyIncome(fakeTenant, fakeUserId, fakeModel);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);

            Assert.Equal(1, results);

        }
        //[Fact]
        //public async Task TestAddOrUpdateEmployerMonthlyIncomeWithDescription_CalledNormally_Ok()
        //{
        //    // Arrange
        //    Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
        //    Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
        //    Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
        //    #region LoanApplicationContext
        //    DbContextOptions<LoanApplicationContext> loanOptions;
        //    var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
        //    loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        //    loanOptions = loanBuilder.Options;
        //    using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
        //    applicationContext.Database.EnsureCreated();
        //    await applicationContext.SaveChangesAsync();

        //    int fakeTenantId = 1;
        //    TenantModel fakeTenant = new TenantModel()
        //    {
        //        Branches = new EditableList<BranchModel>(),
        //        Code = "fakeTenantCode",
        //        Id = fakeTenantId,
        //        Urls = new List<UrlModel>()
        //    };

        //    int fakeUserId = 1;
        //    LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
        //      new LoanApplicationDb.Entity.Models.LoanApplication()
        //      {
        //          CreatedOnUtc = DateTime.UtcNow,
        //          LoanPurposeId = 1,
        //          TenantId = fakeTenantId,
        //          UserId = fakeUserId,
        //          Borrowers = new List<Borrower>()
        //          {
        //                    new Borrower()
        //                    {
        //                        TenantId = 1,
        //                        OwnTypeId = 1, // Primary
        //                        LoanContact = new LoanContact()
        //                        {
        //                            TenantId = fakeTenant.Id,
        //                            FirstName = "PrimaryFirstName",
        //                            MiddleName = "PrimaryMiddleName",
        //                            LastName = "PrimaryLastName",
        //                            EmailAddress = "primaryborrower@emailinator.com",
        //                            MaritalStatusId = (int)MaritalStatus.Married
        //                        },
        //                       BorrowerResidences=new List<BorrowerResidence>()
        //                       {
        //                            new BorrowerResidence()
        //                            {
        //                                 TenantId=fakeTenant.Id,
        //                                 LoanAddressId=1,
        //                                 TypeId=1,
        //                                  LoanAddress= new AddressInfo()
        //                                 {
        //                                     CountryId=1,
        //                                     CountryName="fake country name",
        //                                     StateId=1,
        //                                     StateName="fake state name",
        //                                     CityId=1,
        //                                     CityName="fake city name",
        //                                     StreetAddress="street address",
        //                                     ZipCode="fake cip code",
        //                                    UnitNo="fake uint no",

        //                                 }

        //                            },


        //                       }
        //                       ,
        //                       IncomeInfoes= new List< IncomeInfo> ()
        //                       {
        //                            new IncomeInfo()
        //                            {

        //                                    Id=1,
        //                                    BorrowerId=1
        //                            }

        //                       }

        //                    },
        //                    new Borrower()
        //                    {
        //                        TenantId = 1,
        //                        OwnTypeId = 2, // Secondary
        //                        LoanContact = new LoanContact()
        //                        {
        //                            TenantId = fakeTenant.Id,
        //                            FirstName = "SecondaryFirstName",
        //                            MiddleName = "SecondaryMiddleName",
        //                            LastName = "SecondaryLastName",
        //                            EmailAddress = "secondaryborrower@emailinator.com",
        //                            MaritalStatusId = (int)MaritalStatus.Married
        //                        },

        //                       BorrowerResidences=new List<BorrowerResidence>()
        //                       {
        //                            new BorrowerResidence()
        //                            {
        //                                 TenantId=fakeTenant.Id,
        //                                 LoanAddressId=1,
        //                                 TypeId=1,
        //                                 LoanAddress= new AddressInfo()
        //                                 {

        //                                     CountryId=1,
        //                                     CountryName="fake country name",
        //                                     StateId=1,
        //                                     StateName="fake state name",
        //                                     CityId=1,
        //                                     CityName="fake city name",
        //                                     StreetAddress="street address",
        //                                     ZipCode="fake cip code",
        //                                    UnitNo="fake uint no",


        //                                 }


        //                            }

        //                       }
        //                       ,
        //                       IncomeInfoes= new List< IncomeInfo> ()
        //                       {
        //                            new IncomeInfo()
        //                            {

        //                                    Id=2,
        //                                    BorrowerId=1
        //                            }

        //                       }
        //                    },

        //           },


        //      };

        //    LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
        //    {

        //        LoanApplicationId = 1
        //    };
        //    #endregion

        //    #region  TenantConfigContext
        //    DbContextOptions<TenantConfigContext> options;
        //    var builder = new DbContextOptionsBuilder<TenantConfigContext>();
        //    builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
        //    options = builder.Options;
        //    using TenantConfigContext dataContext = new TenantConfigContext(options);
        //    dataContext.Database.EnsureCreated();
        //    #endregion

        //    UpdateOtherMonthlyIncomeWithDescriptionModel fakeModel = new UpdateOtherMonthlyIncomeWithDescriptionModel()
        //    {
        //        BorrowerId = 1,
        //        IncomeInfoId = 1,
        //        MonthlyBaseIncome = 10000,
        //        Description="Fake description"

        //    };

        //    applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
        //    await applicationContext.SaveChangesAsync();
        //    var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
        //                                                                   serviceProviderMock.Object, loggerIncomeServiceMock.Object, dbFunctionServiceMock.Object);


        //    var results = await service.AddOrUpdateEmployerMonthlyIncomeWithDescription(fakeTenant, fakeUserId, fakeModel);

        //    await applicationContext.Database.EnsureDeletedAsync();

        //    // Assert
        //    Assert.NotNull(results);

        //    Assert.Equal(1, results);

        //}

        //[Fact]
        //public async Task TestAddOrUpdateEmployerMonthlyIncomeWithDescription_loanApplicationNull_MinusOne()
        //{
        //    // Arrange
        //    Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
        //    Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
        //    Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
        //    #region LoanApplicationContext
        //    DbContextOptions<LoanApplicationContext> loanOptions;
        //    var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
        //    loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        //    loanOptions = loanBuilder.Options;
        //    using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
        //    applicationContext.Database.EnsureCreated();
        //    await applicationContext.SaveChangesAsync();

        //    int fakeTenantId = 1;
        //    TenantModel fakeTenant = new TenantModel()
        //    {
        //        Branches = new EditableList<BranchModel>(),
        //        Code = "fakeTenantCode",
        //        Id = fakeTenantId,
        //        Urls = new List<UrlModel>()
        //    };

        //    int fakeUserId = 1;
        //    LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
        //      new LoanApplicationDb.Entity.Models.LoanApplication()
        //      {
                 


        //      };

        //    LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
        //    {

        //        LoanApplicationId = 1
        //    };
        //    #endregion

        //    #region  TenantConfigContext
        //    DbContextOptions<TenantConfigContext> options;
        //    var builder = new DbContextOptionsBuilder<TenantConfigContext>();
        //    builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
        //    options = builder.Options;
        //    using TenantConfigContext dataContext = new TenantConfigContext(options);
        //    dataContext.Database.EnsureCreated();
        //    #endregion

        //    UpdateOtherMonthlyIncomeWithDescriptionModel fakeModel = new UpdateOtherMonthlyIncomeWithDescriptionModel()
        //    {
        //        BorrowerId = 1,
        //        IncomeInfoId = 1,
        //        MonthlyBaseIncome = 10000,
        //        Description = "Fake description"

        //    };

        //    applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
        //    await applicationContext.SaveChangesAsync();
        //    var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
        //                                                                   serviceProviderMock.Object, loggerIncomeServiceMock.Object, dbFunctionServiceMock.Object);


        //    var results = await service.AddOrUpdateEmployerMonthlyIncomeWithDescription(fakeTenant, fakeUserId, fakeModel);

        //    await applicationContext.Database.EnsureDeletedAsync();

        //    // Assert
        //    Assert.NotNull(results);

        //    Assert.Equal(-1, results);

        //}
        //[Fact]
        //public async Task TestAddOrUpdateEmployerMonthlyIncomeWithDescription_IncomeInfo_AddedAndOk()
        //{
        //    // Arrange
        //    Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
        //    Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
        //    Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
        //    #region LoanApplicationContext
        //    DbContextOptions<LoanApplicationContext> loanOptions;
        //    var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
        //    loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        //    loanOptions = loanBuilder.Options;
        //    using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
        //    applicationContext.Database.EnsureCreated();
        //    await applicationContext.SaveChangesAsync();

        //    int fakeTenantId = 1;
        //    TenantModel fakeTenant = new TenantModel()
        //    {
        //        Branches = new EditableList<BranchModel>(),
        //        Code = "fakeTenantCode",
        //        Id = fakeTenantId,
        //        Urls = new List<UrlModel>()
        //    };

        //    int fakeUserId = 1;
        //    LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
        //      new LoanApplicationDb.Entity.Models.LoanApplication()
        //      {
        //          CreatedOnUtc = DateTime.UtcNow,
        //          LoanPurposeId = 1,
        //          TenantId = fakeTenantId,
        //          UserId = fakeUserId,
        //          Borrowers = new List<Borrower>()
        //          {
        //                    new Borrower()
        //                    {
        //                        TenantId = 1,
        //                        OwnTypeId = 1, // Primary
        //                        LoanContact = new LoanContact()
        //                        {
        //                            TenantId = fakeTenant.Id,
        //                            FirstName = "PrimaryFirstName",
        //                            MiddleName = "PrimaryMiddleName",
        //                            LastName = "PrimaryLastName",
        //                            EmailAddress = "primaryborrower@emailinator.com",
        //                            MaritalStatusId = (int)MaritalStatus.Married
        //                        },
        //                       BorrowerResidences=new List<BorrowerResidence>()
        //                       {
        //                            new BorrowerResidence()
        //                            {
        //                                 TenantId=fakeTenant.Id,
        //                                 LoanAddressId=1,
        //                                 TypeId=1,
        //                                  LoanAddress= new AddressInfo()
        //                                 {
        //                                     CountryId=1,
        //                                     CountryName="fake country name",
        //                                     StateId=1,
        //                                     StateName="fake state name",
        //                                     CityId=1,
        //                                     CityName="fake city name",
        //                                     StreetAddress="street address",
        //                                     ZipCode="fake cip code",
        //                                    UnitNo="fake uint no",

        //                                 }

        //                            },


        //                       }
                               
                              

        //                    },
        //                    new Borrower()
        //                    {
        //                        TenantId = 1,
        //                        OwnTypeId = 2, // Secondary
        //                        LoanContact = new LoanContact()
        //                        {
        //                            TenantId = fakeTenant.Id,
        //                            FirstName = "SecondaryFirstName",
        //                            MiddleName = "SecondaryMiddleName",
        //                            LastName = "SecondaryLastName",
        //                            EmailAddress = "secondaryborrower@emailinator.com",
        //                            MaritalStatusId = (int)MaritalStatus.Married
        //                        },

        //                       BorrowerResidences=new List<BorrowerResidence>()
        //                       {
        //                            new BorrowerResidence()
        //                            {
        //                                 TenantId=fakeTenant.Id,
        //                                 LoanAddressId=1,
        //                                 TypeId=1,
        //                                 LoanAddress= new AddressInfo()
        //                                 {

        //                                     CountryId=1,
        //                                     CountryName="fake country name",
        //                                     StateId=1,
        //                                     StateName="fake state name",
        //                                     CityId=1,
        //                                     CityName="fake city name",
        //                                     StreetAddress="street address",
        //                                     ZipCode="fake cip code",
        //                                    UnitNo="fake uint no",


        //                                 }


        //                            }

        //                       }
                              
        //                    },

        //           },


        //      };

        //    LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
        //    {

        //        LoanApplicationId = 1
        //    };
        //    #endregion

        //    #region  TenantConfigContext
        //    DbContextOptions<TenantConfigContext> options;
        //    var builder = new DbContextOptionsBuilder<TenantConfigContext>();
        //    builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
        //    options = builder.Options;
        //    using TenantConfigContext dataContext = new TenantConfigContext(options);
        //    dataContext.Database.EnsureCreated();
        //    #endregion

        //    UpdateOtherMonthlyIncomeWithDescriptionModel fakeModel = new UpdateOtherMonthlyIncomeWithDescriptionModel()
        //    {
        //        BorrowerId = 1,
        //        IncomeInfoId = 1,
        //        MonthlyBaseIncome = 10000,
        //        Description = "Fake description"

        //    };

        //    applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
        //    await applicationContext.SaveChangesAsync();
        //    var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
        //                                                                   serviceProviderMock.Object, loggerIncomeServiceMock.Object, dbFunctionServiceMock.Object);


        //    var results = await service.AddOrUpdateEmployerMonthlyIncomeWithDescription(fakeTenant, fakeUserId, fakeModel);

        //    await applicationContext.Database.EnsureDeletedAsync();

        //    // Assert
        //    Assert.NotNull(results);

        //    Assert.Equal(1, results);

        //}


        [Fact]
        public async Task TestGetOtherIncomeInfo_CalledNormally_Ok()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();

            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenantId,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 1, // Primary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "PrimaryFirstName",
                                    MiddleName = "PrimaryMiddleName",
                                    LastName = "PrimaryLastName",
                                    EmailAddress = "primaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                               BorrowerResidences=new List<BorrowerResidence>()
                               {
                                    new BorrowerResidence()
                                    {
                                         TenantId=fakeTenant.Id,
                                         LoanAddressId=1,
                                         TypeId=1,
                                          LoanAddress= new AddressInfo()
                                         {
                                             CountryId=1,
                                             CountryName="fake country name",
                                             StateId=1,
                                             StateName="fake state name",
                                             CityId=1,
                                             CityName="fake city name",
                                             StreetAddress="street address",
                                             ZipCode="fake cip code",
                                            UnitNo="fake uint no",

                                         }

                                    },


                               }

                               ,IncomeInfoes= new List< IncomeInfo> ()
                               {
                                    new IncomeInfo()
                                    {

                                            Id=1,
                                            BorrowerId=1,
                                            TenantId=1,
                                            IncomeTypeId=1,
                                              Description="fake desc",
                                              IncomeType= new IncomeType()
                                              {
                                                  Id=1,
                                                  Name="abc",
                                                  IncomeGroup =new IncomeGroup()
                                                  {
                                                      
                                                      Id =1,
                                                      Name="fake name"
                                                  }
                                              }
                                    },
                                    

                               },
                             

                            },
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 2, // Secondary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "SecondaryFirstName",
                                    MiddleName = "SecondaryMiddleName",
                                    LastName = "SecondaryLastName",
                                    EmailAddress = "secondaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },

                               BorrowerResidences=new List<BorrowerResidence>()
                               {
                                    new BorrowerResidence()
                                    {
                                         TenantId=fakeTenant.Id,
                                         LoanAddressId=1,
                                         TypeId=1,
                                         LoanAddress= new AddressInfo()
                                         {

                                             CountryId=1,
                                             CountryName="fake country name",
                                             StateId=1,
                                             StateName="fake state name",
                                             CityId=1,
                                             CityName="fake city name",
                                             StreetAddress="street address",
                                             ZipCode="fake cip code",
                                            UnitNo="fake uint no",


                                         }


                                    }

                               }
                                  ,IncomeInfoes= new List< IncomeInfo> ()
                               {
                                    new IncomeInfo()
                                    {

                                            Id=2,
                                            BorrowerId=1,
                                            TenantId=1,
                                            IncomeTypeId=1,
                                            Description="fake desc",
                                            IncomeType= new IncomeType()
                                              {
                                                  Id=2,
                                                  Name="abc",
                                                 
                                                  IncomeGroup =new IncomeGroup()
                                                  {
                                                      Id =2,
                                                      Name="fake name",
                                                      
                                                  }
                                              }

                                    }

                                  
                                   
                                    
                               },
                              
                                 
                            },

                   },


              };

            LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
            {

                LoanApplicationId = 1
            };
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion


            int fakeLoanId = 1;

            int fakeIncomeInfoId = 1;

            GetOtherIncomeInfoModel fakeReturnModel = new GetOtherIncomeInfoModel();


            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();

            List<LoanApplicationDb.Entity.Models.IncomeType> fakeIncomeTypes = new List<LoanApplicationDb.Entity.Models.IncomeType>()
            {
                new IncomeType()
                {
                    Id = 1,
                    Name = "xyz",
                    IncomeGroupId = 1
                }
            };
            List<LoanApplicationDb.Entity.Models.IncomeGroup> fakeIncomeGroup = new List<LoanApplicationDb.Entity.Models.IncomeGroup>()
            {
                new IncomeGroup()
                {
                    Id = 1,
                    Name = "xyz"
                }
            };

            //LoanApplicationDb.Entity.Models.IncomeType fakeIncomeTypes = new LoanApplicationDb.Entity.Models.IncomeType()
            // {
            //     Id = 1,
            //     Name = "xyz"
            // };
            //It.IsAny<int>

            dbFunctionServiceMock.Setup(x => x.UdfIncomeType(It.IsAny<int>(),It.IsAny<int?>()))
                .Returns(fakeIncomeTypes.AsQueryable);


            dbFunctionServiceMock.Setup(x => x.UdfIncomeGroup(It.IsAny<int>(), It.IsAny<int?>()))
                .Returns(fakeIncomeGroup.AsQueryable);



            var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, loggerIncomeServiceMock.Object, dbFunctionServiceMock.Object);
            //var customIncomeTypes = _dbFunctionService.UdfIncomeType(tenant.Id);
            //var customIncomeGroups = _dbFunctionService.UdfIncomeGroup(tenant.Id);


            //_dbFunctionService.UdfIncomeType(tenant.Id)



            incomeServiceMock.Setup(x => x.GetOtherIncomeInfo(fakeTenant, fakeUserId, fakeLoanId)).ReturnsAsync(fakeReturnModel);

            var results = await service.GetOtherIncomeInfo(fakeTenant, fakeUserId, fakeIncomeInfoId);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);

            
        }

        //[Fact]
        //public async Task TestGetSummary_CalledNormally_Ok()
        //{
        //    // Arrange
        //    Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
        //    Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
        //    Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
        //    #region LoanApplicationContext
        //    DbContextOptions<LoanApplicationContext> loanOptions;
        //    var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
        //    loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        //    loanOptions = loanBuilder.Options;
        //    using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
        //    applicationContext.Database.EnsureCreated();
        //    await applicationContext.SaveChangesAsync();

        //    int fakeTenantId = 1;
        //    TenantModel fakeTenant = new TenantModel()
        //    {
        //        Branches = new EditableList<BranchModel>(),
        //        Code = "fakeTenantCode",
        //        Id = fakeTenantId,
        //        Urls = new List<UrlModel>()
        //    };

        //    int fakeUserId = 1;
        //    LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
        //        new LoanApplicationDb.Entity.Models.LoanApplication()
        //        {
        //            CreatedOnUtc = DateTime.UtcNow,
        //            LoanPurposeId = 1,
        //            TenantId = fakeTenantId,
        //            UserId = fakeUserId,
        //            Borrowers = new List<Borrower>()
        //            {
        //                    new Borrower()
        //                    {
        //                        TenantId = 1,
        //                        OwnTypeId = 1, // Primary
        //                        LoanContact = new LoanContact()
        //                        {
        //                            TenantId = fakeTenant.Id,
        //                            FirstName = "PrimaryFirstName",
        //                            MiddleName = "PrimaryMiddleName",
        //                            LastName = "PrimaryLastName",
        //                            EmailAddress = "primaryborrower@emailinator.com",
        //                            MaritalStatusId = (int)MaritalStatus.Married
        //                        },
        //                       BorrowerResidences=new List<BorrowerResidence>()
        //                       {
        //                            new BorrowerResidence()
        //                            {
        //                                 TenantId=fakeTenant.Id,
        //                                 LoanAddressId=1,
        //                                 TypeId=1,
        //                                  LoanAddress= new AddressInfo()
        //                                 {
        //                                     CountryId=1,
        //                                     CountryName="fake country name",
        //                                     StateId=1,
        //                                     StateName="fake state name",
        //                                     CityId=1,
        //                                     CityName="fake city name",
        //                                     StreetAddress="street address",
        //                                     ZipCode="fake cip code",
        //                                    UnitNo="fake uint no",

        //                                 }

        //                            },


        //                       },
        //                       IncomeInfoes= new List< IncomeInfo> ()
        //                       {
        //                            new IncomeInfo()
        //                            {

        //                                    Id=1,
        //                                    BorrowerId=1
        //                            }

        //                       }

        //                    },
        //                    new Borrower()
        //                    {
        //                        TenantId = 1,
        //                        OwnTypeId = 2, // Secondary
        //                        LoanContact = new LoanContact()
        //                        {
        //                            TenantId = fakeTenant.Id,
        //                            FirstName = "SecondaryFirstName",
        //                            MiddleName = "SecondaryMiddleName",
        //                            LastName = "SecondaryLastName",
        //                            EmailAddress = "secondaryborrower@emailinator.com",
        //                            MaritalStatusId = (int)MaritalStatus.Married
        //                        },

        //                       BorrowerResidences=new List<BorrowerResidence>()
        //                       {
        //                            new BorrowerResidence()
        //                            {
        //                                 TenantId=fakeTenant.Id,
        //                                 LoanAddressId=1,
        //                                 TypeId=1,
        //                                 LoanAddress= new AddressInfo()
        //                                 {

        //                                     CountryId=1,
        //                                     CountryName="fake country name",
        //                                     StateId=1,
        //                                     StateName="fake state name",
        //                                     CityId=1,
        //                                     CityName="fake city name",
        //                                     StreetAddress="street address",
        //                                     ZipCode="fake cip code",
        //                                    UnitNo="fake uint no",


        //                                 }


        //                            }

        //                       },
        //                        IncomeInfoes= new List< IncomeInfo> ()
        //                       {
        //                            new IncomeInfo()
        //                            {

        //                                    Id=2,
        //                                    BorrowerId=1
        //                            }

        //                       }

        //                    },

        //             },


        //        };

        //    LoanApplicationIdModel fakeModelReceived = new LoanApplicationIdModel()
        //    {

        //        LoanApplicationId = 1
        //    };
        //    #endregion

        //    #region  TenantConfigContext
        //    DbContextOptions<TenantConfigContext> options;
        //    var builder = new DbContextOptionsBuilder<TenantConfigContext>();
        //    builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
        //    options = builder.Options;
        //    using TenantConfigContext dataContext = new TenantConfigContext(options);
        //    dataContext.Database.EnsureCreated();
        //    #endregion

        //    UpdateOtherAnnualIncomeWithDescriptionModel fakeModel = new UpdateOtherAnnualIncomeWithDescriptionModel()
        //    {
        //        BorrowerId = 1,
        //        IncomeInfoId = 1,
        //        AnnualBaseIncome = 10000,
        //        Description = "fake descritption"
        //    };

        //    applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
        //    await applicationContext.SaveChangesAsync();
        //    var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
        //                                                                   serviceProviderMock.Object, loggerIncomeServiceMock.Object, dbFunctionServiceMock.Object);

        //    int fakeIncomeInfoId = 1;
        //    var results = await service.GetSummary(fakeTenant, fakeUserId, fakeIncomeInfoId);

        //    await applicationContext.Database.EnsureDeletedAsync();

        //    // Assert
        //    Assert.NotNull(results);

          
        //}

        [Fact]
        public async Task TestGetAllIncomeGroupsWithOtherIncomeTypes_CalledNormally_Ok()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<ILogger<IncomeService>> loggerIncomeServiceMock = new Mock<ILogger<IncomeService>>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();
            #region LoanApplicationContext
            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;
            LoanApplicationDb.Entity.Models.LoanApplication fakeLoanApplication =
                new LoanApplicationDb.Entity.Models.LoanApplication()
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    LoanPurposeId = 1,
                    TenantId = fakeTenantId,
                    UserId = fakeUserId,
                    Borrowers = new List<Borrower>()
                    {
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 1, // Primary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "PrimaryFirstName",
                                    MiddleName = "PrimaryMiddleName",
                                    LastName = "PrimaryLastName",
                                    EmailAddress = "primaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },
                               BorrowerResidences=new List<BorrowerResidence>()
                               {
                                    new BorrowerResidence()
                                    {
                                         TenantId=fakeTenant.Id,
                                         LoanAddressId=1,
                                         TypeId=1,
                                          LoanAddress= new AddressInfo()
                                         {
                                             CountryId=1,
                                             CountryName="fake country name",
                                             StateId=1,
                                             StateName="fake state name",
                                             CityId=1,
                                             CityName="fake city name",
                                             StreetAddress="street address",
                                             ZipCode="fake cip code",
                                            UnitNo="fake uint no",

                                         }

                                    },


                               },
                               IncomeInfoes= new List< IncomeInfo> ()
                               {
                                    new IncomeInfo()
                                    {

                                            Id=1,
                                            BorrowerId=1
                                    }

                               }

                            },
                            new Borrower()
                            {
                                TenantId = 1,
                                OwnTypeId = 2, // Secondary
                                LoanContact_LoanContactId = new LoanContact()
                                {
                                    TenantId = fakeTenant.Id,
                                    FirstName = "SecondaryFirstName",
                                    MiddleName = "SecondaryMiddleName",
                                    LastName = "SecondaryLastName",
                                    EmailAddress = "secondaryborrower@emailinator.com",
                                    MaritalStatusId = (int)MaritalStatus.Married
                                },

                               BorrowerResidences=new List<BorrowerResidence>()
                               {
                                    new BorrowerResidence()
                                    {
                                         TenantId=fakeTenant.Id,
                                         LoanAddressId=1,
                                         TypeId=1,
                                         LoanAddress= new AddressInfo()
                                         {

                                             CountryId=1,
                                             CountryName="fake country name",
                                             StateId=1,
                                             StateName="fake state name",
                                             CityId=1,
                                             CityName="fake city name",
                                             StreetAddress="street address",
                                             ZipCode="fake cip code",
                                            UnitNo="fake uint no",


                                         }


                                    }

                               },
                                IncomeInfoes= new List< IncomeInfo> ()
                               {
                                    new IncomeInfo()
                                    {

                                            Id=2,
                                            BorrowerId=1
                                    }

                               }

                            },

                     },


                };

           
            #endregion

            #region  TenantConfigContext
            DbContextOptions<TenantConfigContext> options;
            var builder = new DbContextOptionsBuilder<TenantConfigContext>();
            builder.UseInMemoryDatabase("TenantConfigIsRelationAlreadyMappedServiceTest");
            options = builder.Options;
            using TenantConfigContext dataContext = new TenantConfigContext(options);
            dataContext.Database.EnsureCreated();
            #endregion

        
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new IncomeService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                                           serviceProviderMock.Object, loggerIncomeServiceMock.Object, dbFunctionServiceMock.Object);

         
            var results =  service.GetAllIncomeGroupsWithOtherIncomeTypes(fakeTenant);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);


        }
       
    }
}
