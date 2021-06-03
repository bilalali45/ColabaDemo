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

namespace LoanApplication.Tests
{
    public class TestBorrowerService
    {
        //[Fact]
        //public async Task TestRemoveRelationshipIfRequiredWhenBorrowerNotFound()
        //{
        //    // Arrange
        //    Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();

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
        //                new Borrower()
        //                {
        //                    TenantId = 1,
        //                    OwnTypeId = 1, // Primary
        //                    LoanContact = new LoanContact()
        //                    {
        //                        TenantId = fakeTenant.Id,
        //                        FirstName = "PrimaryFirstName",
        //                        MiddleName = "PrimaryMiddleName",
        //                        LastName = "PrimaryLastName",
        //                        EmailAddress = "primaryborrower@emailinator.com",
        //                        MaritalStatusId = (int)MaritalStatus.Married
        //                    }
        //                },
        //                new Borrower()
        //                {
        //                    TenantId = 1,
        //                    OwnTypeId = 2, // Secondary
        //                    LoanContact = new LoanContact()
        //                    {
        //                        TenantId = fakeTenant.Id,
        //                        FirstName = "SecondaryFirstName",
        //                        MiddleName = "SecondaryMiddleName",
        //                        LastName = "SecondaryLastName",
        //                        EmailAddress = "secondaryborrower@emailinator.com",
        //                        MaritalStatusId = (int)MaritalStatus.Married
        //                    }
        //                }
        //            }
        //        };
        //    applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
        //    await applicationContext.SaveChangesAsync();
        //    //int primaryBorrowerId = fakeLoanApplication.Borrowers.First().Id;
        //    int wrongBorrowerId = 23;

        //    // Act
        //    var service = new BorrowerService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object);
        //    var results = await service.RemoveRelationshipIfRequired(fakeTenant, MaritalStatus.Single, fakeUserId, wrongBorrowerId,
        //        "fake state");

        //    await applicationContext.Database.EnsureDeletedAsync();

        //    // Assert
        //    Assert.Equal(0, results);
        //}

        //[Fact]
        //public async Task TestRemoveRelationshipIfRequiredWhenLoanContactNotFound()
        //{
        //    // Arrange
        //    Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();

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
        //                new Borrower()
        //                {
        //                    TenantId = 1,
        //                    OwnTypeId = 1, // Primary
        //                }
        //            }
        //        };
        //    applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
        //    await applicationContext.SaveChangesAsync();
        //    int primaryBorrowerId = fakeLoanApplication.Borrowers.First().Id;

        //    // Act
        //    var service = new BorrowerService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object);
        //    var results = await service.RemoveRelationshipIfRequired(fakeTenant, MaritalStatus.Single, fakeUserId, primaryBorrowerId,
        //        "fake state");

        //    await applicationContext.Database.EnsureDeletedAsync();

        //    // Assert
        //    Assert.Equal(0, results);
        //}

        //[Fact]
        //public async Task TestRemoveRelationshipIfRequiredWhenPrimaryBorrowerStatusChanged()
        //{
        //    // Arrange
        //    Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();

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
        //                new Borrower()
        //                {
        //                    TenantId = fakeTenantId,
        //                    OwnTypeId = 1, // Primary
        //                    LoanContact = new LoanContact()
        //                    {
        //                        TenantId = fakeTenant.Id,
        //                        FirstName = "PrimaryFirstName",
        //                        MiddleName = "PrimaryMiddleName",
        //                        LastName = "PrimaryLastName",
        //                        EmailAddress = "primaryborrower@emailinator.com",
        //                        MaritalStatusId = (int)MaritalStatus.Married,
        //                    }
        //                },
        //                new Borrower()
        //                {
        //                    TenantId = fakeTenantId,
        //                    OwnTypeId = 2, // Secondary
        //                    RelationWithPrimaryId = (byte)FamilyRelationTypeEnum.Spouse, // Spouse
        //                    LoanContact = new LoanContact()
        //                    {
        //                        TenantId = fakeTenant.Id,
        //                        FirstName = "SecondaryFirstName",
        //                        MiddleName = "SecondaryMiddleName",
        //                        LastName = "SecondaryLastName",
        //                        EmailAddress = "secondaryborrower@emailinator.com",
        //                        MaritalStatusId = (int)MaritalStatus.Married
        //                    }
        //                }
        //            }
        //        };
        //    applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
        //    await applicationContext.SaveChangesAsync();
        //    int primaryBorrowerId = fakeLoanApplication.Borrowers.First().Id;

        //    // Act
        //    var service = new BorrowerService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object);
        //    var results = await service.RemoveRelationshipIfRequired(fakeTenant, MaritalStatus.Single, fakeUserId, primaryBorrowerId,
        //        "fake state");

        //    await applicationContext.Database.EnsureDeletedAsync();

        //    Assert.Equal(1, results);

        //}

        //[Fact]
        //public async Task TestRemoveRelationshipIfRequiredWhenSecondaryBorrowerStatusChanged()
        //{
        //    // Arrange
        //    Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();

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
        //                new Borrower()
        //                {
        //                    TenantId = fakeTenantId,
        //                    OwnTypeId = 1, // Primary
        //                    LoanContact = new LoanContact()
        //                    {
        //                        TenantId = fakeTenant.Id,
        //                        FirstName = "PrimaryFirstName",
        //                        MiddleName = "PrimaryMiddleName",
        //                        LastName = "PrimaryLastName",
        //                        EmailAddress = "primaryborrower@emailinator.com",
        //                        MaritalStatusId = (int)MaritalStatus.Married,
        //                    }
        //                },
        //                new Borrower()
        //                {
        //                    TenantId = fakeTenantId,
        //                    OwnTypeId = 2, // Secondary
        //                    RelationWithPrimaryId = (byte)FamilyRelationTypeEnum.Spouse, // Spouse
        //                    LoanContact = new LoanContact()
        //                    {
        //                        TenantId = fakeTenant.Id,
        //                        FirstName = "SecondaryFirstName",
        //                        MiddleName = "SecondaryMiddleName",
        //                        LastName = "SecondaryLastName",
        //                        EmailAddress = "secondaryborrower@emailinator.com",
        //                        MaritalStatusId = (int)MaritalStatus.Married
        //                    }
        //                }
        //            }
        //        };
        //    applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
        //    await applicationContext.SaveChangesAsync();
        //    int secondaryBorrowerId = fakeLoanApplication.Borrowers.Last().Id;

        //    // Act
        //    var service = new BorrowerService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object);
        //    var results = await service.RemoveRelationshipIfRequired(fakeTenant, MaritalStatus.Single, fakeUserId, secondaryBorrowerId,
        //        "fake state");

        //    await applicationContext.Database.EnsureDeletedAsync();

        //    // Assert
        //    Assert.Equal(1, results);
        //}


        [Fact]
        public async Task TestGetBorrowersForFirstReview_CalledNormally_OK()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();

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

            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();

            // var service1 = new BorrowerService(new  UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())), serviceProviderMock.Object);
            // Act
            var service = new BorrowerService(new UnitOfWork<TenantConfigContext>(dataContext, new RepositoryProvider(new RepositoryFactories())),
                                                new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                        serviceProviderMock.Object, Mock.Of<IMyPropertyService>());

            var results = await service.GetBorrowersForFirstReview(fakeTenant, fakeUserId, fakeModelReceived.LoanApplicationId);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);



        }
    }
}
