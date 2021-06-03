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
using Microsoft.Extensions.Logging;
using Moq;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace LoanApplication.Tests
{
    public class TestSubjectPropertyService
    {

        [Fact]

        public async Task AddOrUpdateLoanAmountDetail_CalledNormally_Ok()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplicationAddOrUpdateLoanAmountDetail");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContextAddOrUpdateLoanAmountDetail");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;



            LoanApplicationDb.Entity.Models.LoanApplication fakeApplication =
            new LoanApplicationDb.Entity.Models.LoanApplication()
            {
                CreatedOnUtc = DateTime.UtcNow,
                LoanPurposeId = 1,
                TenantId = fakeTenant.Id,
                UserId = fakeUserId,
                Borrowers = new List<Borrower>()
                {
                        new Borrower ()
                        {
                            TenantId = fakeTenant.Id,
                            OwnTypeId = 1, // 1. Primary Borrower / 2. Co Borrower
                            LoanContact_LoanContactId = new LoanContact()
                            {
                                TenantId = fakeTenant.Id,
                                FirstName = "FirstName",
                                MiddleName = "MiddleName",
                                LastName = "LastName",
                                EmailAddress = "fake@email.com"
                            },
                            VaDetails = new List<VaDetail>()
                            {
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Active_Military,
                                    ExpirationDateUtc =  DateTime.UtcNow.AddYears(-5)
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Reserves_National_Guard,
                                    ReserveEverActivated = true
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Surviving_Spouse
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    LoanApplicationId = (int)MilitaryAffiliationEnum.Veteran
                                }
                            }
                        }

                },
                PropertyInfo = new PropertyInfo()
                {    
                    Id=1,
                    PropertyValue= 800
                }
            };
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await applicationContext.SaveChangesAsync();
            bool fakeResult = true;

            var fakeModel = new LoanAmountDetailModel { DateOfTransfer = DateTime.Now, DownPayment = 100, GiftAmount = 100, GiftPartOfDownPayment = true, GiftPartReceived = true, LoanApplicationId = fakeApplication.Id, PropertyValue = 100 };



            // Act
            var service = new SubjectPropertyService(
                new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object
                , loggerServiceMock.Object, dbFunctionServiceMock.Object);

            var results = await service.AddOrUpdateLoanAmountDetail(fakeTenant, fakeModel, fakeUserId);

            await applicationContext.Database.EnsureDeletedAsync();
            await tenantContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.Equal(fakeResult, results);

        }

        [Fact]

        public async Task AddOrUpdateLoanAmountDetail_loanApplicationIsNull_False()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplicationAddOrUpdateLoanAmountDetail");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContextAddOrUpdateLoanAmountDetail");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;



            LoanApplicationDb.Entity.Models.LoanApplication fakeApplication =
            new LoanApplicationDb.Entity.Models.LoanApplication()
            {
               

            };

          
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await applicationContext.SaveChangesAsync();
            bool fakeResult = false;

            var fakeModel = new LoanAmountDetailModel { DateOfTransfer = DateTime.Now, DownPayment = 100, GiftAmount = 100, GiftPartOfDownPayment = true, GiftPartReceived = true, LoanApplicationId = fakeApplication.Id, PropertyValue = 100 };

            

            // Act
            var service = new SubjectPropertyService(
                new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object
                , loggerServiceMock.Object, dbFunctionServiceMock.Object);

            var results = await service.AddOrUpdateLoanAmountDetail(fakeTenant, fakeModel, fakeUserId);

            await applicationContext.Database.EnsureDeletedAsync();
            await tenantContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.Equal(fakeResult, results);

        }

        [Fact]
        public async Task AddOrUpdateLoanAmountDetail_PropertyInfoIsNull_False()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplicationAddOrUpdateLoanAmountDetail");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContextAddOrUpdateLoanAmountDetail");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;


            LoanApplicationDb.Entity.Models.LoanApplication fakeApplication =
            new LoanApplicationDb.Entity.Models.LoanApplication()
            {
                CreatedOnUtc = DateTime.UtcNow,
                LoanPurposeId = 1,
                TenantId = fakeTenant.Id,
                UserId = fakeUserId,
                Borrowers = new List<Borrower>()
                {
                        new Borrower ()
                        {
                            TenantId = fakeTenant.Id,
                              OwnTypeId = 1, // 1. Primary Borrower / 2. Co Borrower
                            LoanContact_LoanContactId = new LoanContact()
                            {
                                TenantId = fakeTenant.Id,
                                FirstName = "FirstName",
                                MiddleName = "MiddleName",
                                LastName = "LastName",
                                EmailAddress = "fake@email.com"
                            },
                            VaDetails = new List<VaDetail>()
                            {
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Active_Military,
                                    ExpirationDateUtc =  DateTime.UtcNow.AddYears(-5)
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Reserves_National_Guard,
                                    ReserveEverActivated = true
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Surviving_Spouse
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    LoanApplicationId = (int)MilitaryAffiliationEnum.Veteran
                                }
                            }
                        }

                }
               
            };


            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await applicationContext.SaveChangesAsync();
            bool fakeResult = false;

            var fakeModel = new LoanAmountDetailModel { DateOfTransfer = DateTime.Now, DownPayment = 100, GiftAmount = 100, GiftPartOfDownPayment = true, GiftPartReceived = true, LoanApplicationId = fakeApplication.Id, PropertyValue = 100 };



            // Act
            var service = new SubjectPropertyService(
                new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object
                , loggerServiceMock.Object, dbFunctionServiceMock.Object);

            var results = await service.AddOrUpdateLoanAmountDetail(fakeTenant, fakeModel, fakeUserId);

            await applicationContext.Database.EnsureDeletedAsync();
            await tenantContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.Equal(fakeResult, results);

        }

        [Fact]
        public async Task GetSubjectPropertyLoanAmountDetail_CalledNormally_Ok()
        {

            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplicationAddOrUpdateLoanAmountDetail");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContextAddOrUpdateLoanAmountDetail");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;



            LoanApplicationDb.Entity.Models.LoanApplication fakeApplication =
            new LoanApplicationDb.Entity.Models.LoanApplication()
            {
                CreatedOnUtc = DateTime.UtcNow,
                LoanPurposeId = 1,
                TenantId = fakeTenant.Id,
                UserId = fakeUserId,
                Borrowers = new List<Borrower>()
                {
                        new Borrower ()
                        {
                            TenantId = fakeTenant.Id,
                            OwnTypeId = 1, // 1. Primary Borrower / 2. Co Borrower
                            LoanContact_LoanContactId = new LoanContact()
                            {
                                TenantId = fakeTenant.Id,
                                FirstName = "FirstName",
                                MiddleName = "MiddleName",
                                LastName = "LastName",
                                EmailAddress = "fake@email.com"
                            },
                            VaDetails = new List<VaDetail>()
                            {
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Active_Military,
                                    ExpirationDateUtc =  DateTime.UtcNow.AddYears(-5)
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Reserves_National_Guard,
                                    ReserveEverActivated = true
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Surviving_Spouse
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    LoanApplicationId = (int)MilitaryAffiliationEnum.Veteran
                                }
                            }
                        }

                },
                PropertyInfo = new PropertyInfo()
                {
                    Id = 1,
                    PropertyValue = 800
                }
            };
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await applicationContext.SaveChangesAsync();
            LoanAmountDetailBase fakeResult = new LoanAmountDetailBase();

            var fakeModel = new LoanAmountDetailModel { DateOfTransfer = DateTime.Now, DownPayment = 100, GiftAmount = 100, GiftPartOfDownPayment = true, GiftPartReceived = true, LoanApplicationId = fakeApplication.Id, PropertyValue = 100 };



            // Act
            var service = new SubjectPropertyService(
                new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object
                , loggerServiceMock.Object, dbFunctionServiceMock.Object);

            var results = await service.GetSubjectPropertyLoanAmountDetail(fakeTenant, fakeModel.LoanApplicationId, fakeUserId);

            await applicationContext.Database.EnsureDeletedAsync();
            await tenantContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);
            Assert.Equal(fakeModel.LoanApplicationId,results.LoanApplicationId);
        }

        [Fact]
        public async Task GetSubjectPropertyLoanAmountDetail_loanApplicationIsNull_Null()
        {

            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplicationAddOrUpdateLoanAmountDetail");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContextAddOrUpdateLoanAmountDetail");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;



            LoanApplicationDb.Entity.Models.LoanApplication fakeApplication =
            new LoanApplicationDb.Entity.Models.LoanApplication()
            {


            };
            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await applicationContext.SaveChangesAsync();
            //string fakeResult =null;

            var fakeModel = new LoanAmountDetailModel { DateOfTransfer = DateTime.Now, DownPayment = 100, GiftAmount = 100, GiftPartOfDownPayment = true, GiftPartReceived = true, LoanApplicationId = fakeApplication.Id, PropertyValue = 100 };

        

            // Act
            var service = new SubjectPropertyService(
                new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object
                , loggerServiceMock.Object, dbFunctionServiceMock.Object);

            var results = await service.GetSubjectPropertyLoanAmountDetail(fakeTenant, fakeModel.LoanApplicationId, fakeUserId);

            await applicationContext.Database.EnsureDeletedAsync();
            await tenantContext.Database.EnsureDeletedAsync();

            // Assert
            //Assert.NotNull(results);
            Assert.Equal(null, results);
        }

        [Fact]
        public async Task GetSubjectPropertyLoanAmountDetail_PropertyInfoIsNull_Null()
        {

            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplicationAddOrUpdateLoanAmountDetail");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContextAddOrUpdateLoanAmountDetail");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;



            LoanApplicationDb.Entity.Models.LoanApplication fakeApplication =
           new LoanApplicationDb.Entity.Models.LoanApplication()
           {
               CreatedOnUtc = DateTime.UtcNow,
               LoanPurposeId = 1,
               TenantId = fakeTenant.Id,
               UserId = fakeUserId,
               Borrowers = new List<Borrower>()
               {
                        new Borrower ()
                        {
                            TenantId = fakeTenant.Id,
                            OwnTypeId = 1, // 1. Primary Borrower / 2. Co Borrower
                            LoanContact_LoanContactId = new LoanContact()
                            {
                                TenantId = fakeTenant.Id,
                                FirstName = "FirstName",
                                MiddleName = "MiddleName",
                                LastName = "LastName",
                                EmailAddress = "fake@email.com"
                            },
                            VaDetails = new List<VaDetail>()
                            {
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Active_Military,
                                    ExpirationDateUtc =  DateTime.UtcNow.AddYears(-5)
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Reserves_National_Guard,
                                    ReserveEverActivated = true
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Surviving_Spouse
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    LoanApplicationId = (int)MilitaryAffiliationEnum.Veteran
                                }
                            }
                        }

               }
               
           };

            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await applicationContext.SaveChangesAsync();
            //string fakeResult =null;

            var fakeModel = new LoanAmountDetailModel { DateOfTransfer = DateTime.Now, DownPayment = 100, GiftAmount = 100, GiftPartOfDownPayment = true, GiftPartReceived = true, LoanApplicationId = fakeApplication.Id, PropertyValue = 100 };



            // Act
            var service = new SubjectPropertyService(
                new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object
                , loggerServiceMock.Object, dbFunctionServiceMock.Object);

            var results = await service.GetSubjectPropertyLoanAmountDetail(fakeTenant, fakeModel.LoanApplicationId, fakeUserId);

            await applicationContext.Database.EnsureDeletedAsync();
            await tenantContext.Database.EnsureDeletedAsync();

            // Assert
            //Assert.NotNull(results);
            Assert.Equal(null, results);
        }

        [Fact]
        public async Task GetSubjectPropertyState_CalledNormally_Ok()
        {

            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplicationAddOrUpdateLoanAmountDetail");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContextAddOrUpdateLoanAmountDetail");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;



            LoanApplicationDb.Entity.Models.LoanApplication fakeApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenant.Id,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                        new Borrower ()
                        {
                            TenantId = fakeTenant.Id,
                            OwnTypeId = 1, // 1. Primary Borrower / 2. Co Borrower
                            LoanContact_LoanContactId = new LoanContact()
                            {
                                TenantId = fakeTenant.Id,
                                FirstName = "FirstName",
                                MiddleName = "MiddleName",
                                LastName = "LastName",
                                EmailAddress = "fake@email.com"
                            },
                            VaDetails = new List<VaDetail>()
                            {
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Active_Military,
                                    ExpirationDateUtc =  DateTime.UtcNow.AddYears(-5)
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Reserves_National_Guard,
                                    ReserveEverActivated = true
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Surviving_Spouse
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    LoanApplicationId = (int)MilitaryAffiliationEnum.Veteran
                                }
                            }
                        }

                  },
                  PropertyInfo = new PropertyInfo()
                  {
                      Id = 1,
                      PropertyValue = 800,
                      AddressInfo = new AddressInfo()
                      {
                          Id=1
                      }
                  },
                  
              };

            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await applicationContext.SaveChangesAsync();
            //string fakeResult =null;

            var fakeModel = new LoanAmountDetailModel { DateOfTransfer = DateTime.Now, DownPayment = 100, GiftAmount = 100, GiftPartOfDownPayment = true, GiftPartReceived = true, LoanApplicationId = fakeApplication.Id, PropertyValue = 100 };



            // Act
            var service = new SubjectPropertyService(
                new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object
                , loggerServiceMock.Object, dbFunctionServiceMock.Object);

            var results = await service.GetSubjectPropertyState(fakeTenant, fakeModel.LoanApplicationId, fakeUserId);

            await applicationContext.Database.EnsureDeletedAsync();
            await tenantContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);
            Assert.Equal(fakeModel.LoanApplicationId, results.LoanApplicationId);
        }

        [Fact]
        public async Task GetSubjectPropertyState_PropertyInfoIsNull_Null()
        {

            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplicationAddOrUpdateLoanAmountDetail");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContextAddOrUpdateLoanAmountDetail");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;



            LoanApplicationDb.Entity.Models.LoanApplication fakeApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenant.Id,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                        new Borrower ()
                        {
                            TenantId = fakeTenant.Id,
                            OwnTypeId = 1, // 1. Primary Borrower / 2. Co Borrower
                            LoanContact_LoanContactId = new LoanContact()
                            {
                                TenantId = fakeTenant.Id,
                                FirstName = "FirstName",
                                MiddleName = "MiddleName",
                                LastName = "LastName",
                                EmailAddress = "fake@email.com"
                            },
                            VaDetails = new List<VaDetail>()
                            {
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Active_Military,
                                    ExpirationDateUtc =  DateTime.UtcNow.AddYears(-5)
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Reserves_National_Guard,
                                    ReserveEverActivated = true
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Surviving_Spouse
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    LoanApplicationId = (int)MilitaryAffiliationEnum.Veteran
                                }
                            }
                        }

                  }

              };

            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await applicationContext.SaveChangesAsync();
            //string fakeResult =null;

            var fakeModel = new LoanAmountDetailModel { DateOfTransfer = DateTime.Now, DownPayment = 100, GiftAmount = 100, GiftPartOfDownPayment = true, GiftPartReceived = true, LoanApplicationId = fakeApplication.Id, PropertyValue = 100 };



            // Act
            var service = new SubjectPropertyService(
                new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object
                , loggerServiceMock.Object, dbFunctionServiceMock.Object);

            var results = await service.GetSubjectPropertyState(fakeTenant, fakeModel.LoanApplicationId, fakeUserId);

            await applicationContext.Database.EnsureDeletedAsync();
            await tenantContext.Database.EnsureDeletedAsync();

            // Assert
            //Assert.NotNull(results);
            Assert.Equal(null, results);
        }

        [Fact]
        public async Task GetSubjectPropertyState_loanApplicationIsNull_Null()
        {

            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplicationAddOrUpdateLoanAmountDetail");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContextAddOrUpdateLoanAmountDetail");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;



            LoanApplicationDb.Entity.Models.LoanApplication fakeApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                 

              };

            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await applicationContext.SaveChangesAsync();
            //string fakeResult =null;

            var fakeModel = new LoanAmountDetailModel { DateOfTransfer = DateTime.Now, DownPayment = 100, GiftAmount = 100, GiftPartOfDownPayment = true, GiftPartReceived = true, LoanApplicationId = fakeApplication.Id, PropertyValue = 100 };



            // Act
            var service = new SubjectPropertyService(
                new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object
                , loggerServiceMock.Object, dbFunctionServiceMock.Object);

            var results = await service.GetSubjectPropertyState(fakeTenant, fakeModel.LoanApplicationId, fakeUserId);

            await applicationContext.Database.EnsureDeletedAsync();
            await tenantContext.Database.EnsureDeletedAsync();

            // Assert
            //Assert.NotNull(results);
            Assert.Equal(null, results);
        }

        [Fact]
        public async Task AddOrUpdateSubjectPropertyState_loanApplicationIsNull_False()
        {

            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplicationAddOrUpdateLoanAmountDetail");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContextAddOrUpdateLoanAmountDetail");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;



            LoanApplicationDb.Entity.Models.LoanApplication fakeApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {


              };
            bool fakeResult = false;

            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await applicationContext.SaveChangesAsync();
            //string fakeResult =null;

            var fakeModel = new UpdateSubjectPropertyStateModel { State="fake state", StateId=1, LoanApplicationId = fakeApplication.Id };



            // Act
            var service = new SubjectPropertyService(
                new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object
                , loggerServiceMock.Object, dbFunctionServiceMock.Object);

            var results = await service.AddOrUpdateSubjectPropertyState(fakeTenant, fakeModel, fakeUserId);

            await applicationContext.Database.EnsureDeletedAsync();
            await tenantContext.Database.EnsureDeletedAsync();

            // Assert
  
            Assert.Equal(fakeResult, results);
        }

        [Fact]
        public async Task AddOrUpdateSubjectPropertyState_PropertyInfo_False()
        {

            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplicationAddOrUpdateLoanAmountDetail");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContextAddOrUpdateLoanAmountDetail");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;



            LoanApplicationDb.Entity.Models.LoanApplication fakeApplication =
               new LoanApplicationDb.Entity.Models.LoanApplication()
               {
                   CreatedOnUtc = DateTime.UtcNow,
                   LoanPurposeId = 1,
                   TenantId = fakeTenant.Id,
                   UserId = fakeUserId,
                   Borrowers = new List<Borrower>()
                   {
                        new Borrower ()
                        {
                            TenantId = fakeTenant.Id,
                            OwnTypeId = 1, // 1. Primary Borrower / 2. Co Borrower
                            LoanContact_LoanContactId = new LoanContact()
                            {
                                TenantId = fakeTenant.Id,
                                FirstName = "FirstName",
                                MiddleName = "MiddleName",
                                LastName = "LastName",
                                EmailAddress = "fake@email.com"
                            },
                            VaDetails = new List<VaDetail>()
                            {
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Active_Military,
                                    ExpirationDateUtc =  DateTime.UtcNow.AddYears(-5)
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Reserves_National_Guard,
                                    ReserveEverActivated = true
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Surviving_Spouse
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    LoanApplicationId = (int)MilitaryAffiliationEnum.Veteran
                                }
                            }
                        }

                   }

               };
            bool fakeResult = false;

            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await applicationContext.SaveChangesAsync();
            //string fakeResult =null;

            var fakeModel = new UpdateSubjectPropertyStateModel { State = "fake state", StateId = 1, LoanApplicationId = fakeApplication.Id };



            // Act
            var service = new SubjectPropertyService(
                new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object
                , loggerServiceMock.Object, dbFunctionServiceMock.Object);

            var results = await service.AddOrUpdateSubjectPropertyState(fakeTenant, fakeModel, fakeUserId);

            await applicationContext.Database.EnsureDeletedAsync();
            await tenantContext.Database.EnsureDeletedAsync();

            // Assert

            Assert.Equal(fakeResult, results);
        }

        [Fact]
        public async Task AddOrUpdateSubjectPropertyState_CalledNormally_Ok()
        {

            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplicationAddOrUpdateLoanAmountDetail");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContextAddOrUpdateLoanAmountDetail");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;




            LoanApplicationDb.Entity.Models.LoanApplication fakeApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenant.Id,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                        new Borrower ()
                        {
                            TenantId = fakeTenant.Id,
                            OwnTypeId = 1, // 1. Primary Borrower / 2. Co Borrower
                            LoanContact_LoanContactId = new LoanContact()
                            {
                                TenantId = fakeTenant.Id,
                                FirstName = "FirstName",
                                MiddleName = "MiddleName",
                                LastName = "LastName",
                                EmailAddress = "fake@email.com"
                            },
                            VaDetails = new List<VaDetail>()
                            {
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Active_Military,
                                    ExpirationDateUtc =  DateTime.UtcNow.AddYears(-5)
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Reserves_National_Guard,
                                    ReserveEverActivated = true
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Surviving_Spouse
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    LoanApplicationId = (int)MilitaryAffiliationEnum.Veteran
                                }
                            }
                        }

                  },
                  PropertyInfo = new PropertyInfo()
                  {
                      Id = 1,
                      PropertyValue = 800,
                      AddressInfo = new AddressInfo()
                      {
                          Id = 1
                      }
                  },

              };
            bool fakeResult = true;

            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await applicationContext.SaveChangesAsync();
            //string fakeResult =null;

            var fakeModel = new UpdateSubjectPropertyStateModel { State = "fake state", StateId = 1, LoanApplicationId = fakeApplication.Id };



            // Act
            var service = new SubjectPropertyService(
                new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object
                , loggerServiceMock.Object, dbFunctionServiceMock.Object);

            var results = await service.AddOrUpdateSubjectPropertyState(fakeTenant, fakeModel, fakeUserId);

            await applicationContext.Database.EnsureDeletedAsync();
            await tenantContext.Database.EnsureDeletedAsync();

            // Assert

            Assert.Equal(fakeResult, results);
        }

        [Fact]
        public async Task UpdatePropertyIdentified_CalledNormally_Ok()
        {

            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplicationAddOrUpdateLoanAmountDetail");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContextAddOrUpdateLoanAmountDetail");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;




            LoanApplicationDb.Entity.Models.LoanApplication fakeApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenant.Id,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                        new Borrower ()
                        {
                            TenantId = fakeTenant.Id,
                            OwnTypeId = 1, // 1. Primary Borrower / 2. Co Borrower
                            LoanContact_LoanContactId = new LoanContact()
                            {
                                TenantId = fakeTenant.Id,
                                FirstName = "FirstName",
                                MiddleName = "MiddleName",
                                LastName = "LastName",
                                EmailAddress = "fake@email.com"
                            },
                            VaDetails = new List<VaDetail>()
                            {
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Active_Military,
                                    ExpirationDateUtc =  DateTime.UtcNow.AddYears(-5)
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Reserves_National_Guard,
                                    ReserveEverActivated = true
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Surviving_Spouse
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    LoanApplicationId = (int)MilitaryAffiliationEnum.Veteran
                                }
                            }
                        }

                  },
                  PropertyInfo = new PropertyInfo()
                  {
                      Id = 1,
                      PropertyValue = 800,
                      AddressInfo = new AddressInfo()
                      {
                          Id = 1,
                          StateId=1,

                      }
                  },

              };
            bool fakeResult = true;

            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await applicationContext.SaveChangesAsync();
            //string fakeResult =null;

            var fakeModel = new UpdateSubjectPropertyStateModel { State = "fake state", StateId = 1, LoanApplicationId = fakeApplication.Id };



            // Act
            var service = new SubjectPropertyService(
                new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object
                , loggerServiceMock.Object, dbFunctionServiceMock.Object);

            PropertyIdentifiedModel fakemodel = new PropertyIdentifiedModel()
            {
                LoanApplicationId = 1,
                StateId=1,
                
            };

            var results = await service.UpdatePropertyIdentifiedFlag(fakeTenant, fakeUserId, fakemodel);

            await applicationContext.Database.EnsureDeletedAsync();
            await tenantContext.Database.EnsureDeletedAsync();

            // Assert

            Assert.Equal(1, results);
        }


        [Fact]
        public async Task GetPropertyIdentifiedFlag_CalledNormally_Ok()
        {

            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbFunctionServiceMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplicationAddOrUpdateLoanAmountDetail");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContextAddOrUpdateLoanAmountDetail");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            int fakeTenantId = 1;
            TenantModel fakeTenant = new TenantModel()
            {
                Branches = new EditableList<BranchModel>(),
                Code = "fakeTenantCode",
                Id = fakeTenantId,
                Urls = new List<UrlModel>()
            };

            int fakeUserId = 1;




            LoanApplicationDb.Entity.Models.LoanApplication fakeApplication =
              new LoanApplicationDb.Entity.Models.LoanApplication()
              {
                  CreatedOnUtc = DateTime.UtcNow,
                  LoanPurposeId = 1,
                  TenantId = fakeTenant.Id,
                  UserId = fakeUserId,
                  Borrowers = new List<Borrower>()
                  {
                        new Borrower ()
                        {
                            TenantId = fakeTenant.Id,
                            OwnTypeId = 1, // 1. Primary Borrower / 2. Co Borrower
                            LoanContact_LoanContactId = new LoanContact()
                            {
                                TenantId = fakeTenant.Id,
                                FirstName = "FirstName",
                                MiddleName = "MiddleName",
                                LastName = "LastName",
                                EmailAddress = "fake@email.com"
                            },
                            VaDetails = new List<VaDetail>()
                            {
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Active_Military,
                                    ExpirationDateUtc =  DateTime.UtcNow.AddYears(-5)
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Reserves_National_Guard,
                                    ReserveEverActivated = true
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    MilitaryAffiliationId = (int)MilitaryAffiliationEnum.Surviving_Spouse
                                },
                                new VaDetail()
                                {
                                    TenantId = fakeTenantId,
                                    LoanApplicationId = (int)MilitaryAffiliationEnum.Veteran
                                }
                            }
                        }

                  },
                  PropertyInfo = new PropertyInfo()
                  {
                      Id = 1,
                      PropertyValue = 800,
                      AddressInfo = new AddressInfo()
                      {
                          Id = 1,
                          StateId = 1,

                      }
                  },

              };
            bool fakeResult = true;

            applicationContext.Add<LoanApplicationDb.Entity.Models.LoanApplication>(fakeApplication);
            await applicationContext.SaveChangesAsync();
            //string fakeResult =null;

            var fakeModel = new UpdateSubjectPropertyStateModel { State = "fake state", StateId = 1, LoanApplicationId = fakeApplication.Id };



            // Act
            var service = new SubjectPropertyService(
                new UnitOfWork<LoanApplicationContext>(applicationContext, new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object
                , loggerServiceMock.Object, dbFunctionServiceMock.Object);

            PropertyIdentifiedModel fakemodel = new PropertyIdentifiedModel()
            {
                LoanApplicationId = 1,
                StateId = 1,

            };
            PropertyIdentifiedBaseModel fakemodelToReturn = new PropertyIdentifiedBaseModel();

            var results = await service.GetPropertyIdentifiedFlag(fakeTenant, fakeUserId,fakemodel.LoanApplicationId);

            await applicationContext.Database.EnsureDeletedAsync();
            await tenantContext.Database.EnsureDeletedAsync();

            // Assert
            Assert.NotNull(results);

        }

        Mock<ILogger<SubjectPropertyService>> loggerServiceMock = new Mock<ILogger<SubjectPropertyService>>();
    }
}
