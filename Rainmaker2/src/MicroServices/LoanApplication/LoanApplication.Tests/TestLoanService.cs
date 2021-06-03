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
using OwnType = LoanApplicationDb.Entity.Models.OwnType;

namespace LoanApplication.Tests
{
    public class TestLoanService
    {
        [Fact]
        public async Task TestGetLoanApplicationForFirstReview_CalledNormally_OK()
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
            var service = new LoanService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                       serviceProviderMock.Object, null);
            var results = await service.GetLoanApplicationForFirstReview(fakeTenant, fakeUserId, fakeModelReceived.LoanApplicationId);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);

        }


        [Fact]
        public async Task TestGetLoanApplicationForSecondReview_CalledNormally_OK()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
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
                    LoanGoalId = 1,
                    LoanGoal = new LoanGoal()
                    {
                        Id = 1,
                        Name = "fake loan goal"
                    },
                    PropertyInfo = new PropertyInfo()
                    {
                        PropertyUsageId = 1,
                        PropertyTypeId = 1,
                        PropertyType = new PropertyType()
                        {
                            Id = 1,
                            Name = "fake property type"
                        },
                        PropertyUsage = new PropertyUsage()
                        {
                            Id = 1,
                            Name = "fake usage name"
                        },
                        AddressInfo = new AddressInfo()
                        {
                            StateName = "fake state",
                            StreetAddress = "fake street",
                            CountryId = 1,
                            CountryName = "fake country",
                            StateId = 1,
                            CityId = 1,
                            CityName = "fake city",
                            ZipCode = "123789",
                            UnitNo = "fakeUnit"
                        }
                    }

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

            List<OwnType> fakeOwnTypes = new List<OwnType>()
            {
                new OwnType()
                {
                    Id = 1,
                    Name = "Primary",
                },
                new OwnType()
                {
                    Id = 2,
                    Name = "Secondary"
                }
            };
            dbFunctionServiceMock.Setup(x => x.UdfOwnType(It.IsAny<int>(), It.IsAny<int?>())).Returns(fakeOwnTypes.AsQueryable);

            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new LoanService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                       serviceProviderMock.Object, dbFunctionServiceMock.Object);
            var results = await service.GetLoanApplicationForSecondReview(fakeTenant, fakeUserId, fakeModelReceived.LoanApplicationId);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);




        }

        [Fact]
        public async Task TestUpdateState__CalledNormally_OK()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
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
                    LoanGoalId = 1,
                    LoanGoal = new LoanGoal()
                    {
                        Id = 1,
                        Name = "fake loan goal"
                    },
                    PropertyInfo = new PropertyInfo()
                    {
                        PropertyUsageId = 1,
                        PropertyTypeId = 1,
                        PropertyType = new PropertyType()
                        {
                            Id = 1,
                            Name = "fake property type"
                        },
                        PropertyUsage = new PropertyUsage()
                        {
                            Id = 1,
                            Name = "fake usage name"
                        },
                        AddressInfo = new AddressInfo()
                        {
                            StateName = "fake state",
                            StreetAddress = "fake street",
                            CountryId = 1,
                            CountryName = "fake country",
                            StateId = 1,
                            CityId = 1,
                            CityName = "fake city",
                            ZipCode = "123789",
                            UnitNo = "fakeUnit"
                        }
                    }

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


            UpdateStateModel fakemodel = new UpdateStateModel()
            {
                LoanApplicationId=1,
                State="fake state"
            };

            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeLoanApplication);
            await applicationContext.SaveChangesAsync();
            var service = new LoanService(new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories())),
                                                       serviceProviderMock.Object, dbFunctionServiceMock.Object);
            var results = await service.UpdateState(fakeTenant, fakeUserId, fakemodel);

            await applicationContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);

            Assert.Equal(1,results);




        }
    }
}
