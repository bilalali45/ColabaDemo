using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanApplication.Model;
using LoanApplication.Service;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;
using MilitaryAffiliation = LoanApplicationDb.Entity.Models.MilitaryAffiliation;

namespace LoanApplication.Tests
{
    public class TestInfoService
    {
        //[Fact]
        //public async Task TestGetAllMilitaryAffiliation()
        //{
        //    // Arrange
        //    Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
        //    Mock<IDbFunctionService> dbServiceFunctionsMock = new Mock<IDbFunctionService>();

        //    DbContextOptions<LoanApplicationContext> loanOptions;
        //    var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
        //    loanBuilder.UseInMemoryDatabase("LoanApplication");
        //    loanOptions = loanBuilder.Options;
        //    using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
        //    applicationContext.Database.EnsureCreated();
        //    await applicationContext.SaveChangesAsync();

        //    DbContextOptions<TenantConfigContext> tenantOptions;
        //    var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
        //    tenantBuilder.UseInMemoryDatabase("TenantContext");
        //    tenantOptions = tenantBuilder.Options;
        //    using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
        //    tenantContext.Database.EnsureCreated();
        //    await tenantContext.SaveChangesAsync();

        //    TenantModel contextTenant = new TenantModel()
        //    {
        //        Branches = new List<BranchModel>()
        //        {
        //            new BranchModel()
        //            {
        //                Code = "someBranchCode", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
        //            }
        //        }
        //    };

        //    List<MilitaryStatusList> statusList = new List<MilitaryStatusList>()
        //    {
        //        new MilitaryStatusList() {Id = (int) MilitaryAffiliationEnum.Active_Military, IsActive = true},
        //        new MilitaryStatusList() {Id = (int) MilitaryAffiliationEnum.Reserves_National_Guard, IsActive = true},
        //        new MilitaryStatusList() {Id = (int) MilitaryAffiliationEnum.Surviving_Spouse, IsActive = true},
        //        new MilitaryStatusList() {Id = (int) MilitaryAffiliationEnum.Veteran, IsActive = true},
        //    };

        //    foreach (var status in statusList)
        //    {
        //        applicationContext.Add<MilitaryStatusList>(status);
        //        await applicationContext.SaveChangesAsync();
        //    }

        //    dbServiceFunctionsMock.Setup(x => x.UdfMilitaryAffiliation(It.IsAny<int>())).Returns(statusList.AsQueryable());

        //    // Act
        //    var service = new InfoService(
        //        new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories()))
        //        , new UnitOfWork<LoanApplicationContext>(applicationContext,
        //            new RepositoryProvider(new RepositoryFactories()))
        //        , serviceProviderMock.Object, dbServiceFunctionsMock.Object
        //    );


        //    var results = await service.GetAllMilitaryAffiliation(contextTenant);
        //}

        [Fact]
        public async Task TestGetAllPropertyUsages()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbServiceFunctionsMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplication");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            List<PropertyUsage> fakePropertyUsages = new List<PropertyUsage>()
            {
                new PropertyUsage() {Description = "Usage 1", IsActive = true, DisplayOrder = 1, IsDeleted = false, Image = "image1.svg" },
                new PropertyUsage() {Description = "Usage 2", IsActive = true, DisplayOrder = 1, IsDeleted = false, Image = "image2.svg" }
            };

            dbServiceFunctionsMock.Setup(x => x.UdfPropertyUsage(It.IsAny<int>(), It.IsAny<int?>()))
                .Returns(fakePropertyUsages.AsQueryable);

            var service = new InfoService(
                new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories()))
                , new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object, dbServiceFunctionsMock.Object
            );

            TenantModel tenant = new TenantModel()
            {
                Id = 1,
                Code = "fakeTenantCode",
                Urls = new List<UrlModel>() { new UrlModel() { Url = "www.tenant.com" } }
            };

            // Act
            var results = service.GetAllPropertyUsages(tenant,1);

            // Assert
            Assert.NotNull(results);
            Assert.NotEmpty(results);
        }

        [Fact]
        public async Task TestGetAllPropertyTypes()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbServiceFunctionsMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplication");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            List<PropertyType> fakePropertyTypes = new List<PropertyType>()
            {
                new PropertyType() {Description = "Usage 1", IsActive = true, DisplayOrder = 1, IsDeleted = false, Image = "image1.svg" },
                new PropertyType() {Description = "Usage 2", IsActive = true, DisplayOrder = 1, IsDeleted = false, Image = "image2.svg" }
            };

            dbServiceFunctionsMock.Setup(x => x.UdfPropertyType(It.IsAny<int>(), It.IsAny<int?>()))
                .Returns(fakePropertyTypes.AsQueryable);

            var service = new InfoService(
                new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories()))
                , new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object, dbServiceFunctionsMock.Object
            );

            TenantModel tenant = new TenantModel()
            {
                Id = 1,
                Code = "fakeTenantCode",
                Urls = new List<UrlModel>() { new UrlModel() { Url = "www.tenant.com" } }
            };

            // Act
            var results = service.GetAllPropertyTypes(tenant);

            // Assert
            Assert.NotNull(results);
            Assert.NotEmpty(results);
        }

        [Fact]
        public async Task TestGetLocationSearchByZipCodeCityState()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbServiceFunctionsMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplication");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            string fakeCity = "fakeCity";
            string fakeState = "fakeState";
            string fakeCountry = "fakeCountry";
            string fakeZipCode = "fakeCode";

            List<LocationSearch> fakeSearchResults = new List<LocationSearch>()
            {
                new LocationSearch() { Abbreviation = "US", CityName = "Fake City", CityId = 1, CountyId = 1, CountyName = "Fake Country", StateId = 1, StateName = "Fake State", ZipPostalCode = "FakeCode" }
            };

            dbServiceFunctionsMock.Setup(x => x.LocationSearchByCityCountyStateZipCode(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(fakeSearchResults.AsQueryable);

            var service = new InfoService(
                new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories()))
                , new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object, dbServiceFunctionsMock.Object
            );

            TenantModel tenant = new TenantModel()
            {
                Id = 1,
                Code = "fakeTenantCode",
                Urls = new List<UrlModel>() { new UrlModel() { Url = "www.tenant.com" } }
            };

            // Act
            var results = service.GetLocationSearchByZipCodeCityState(fakeCity, fakeState, fakeCountry, fakeZipCode);

            // Assert
            Assert.NotNull(results);
        }

        [Fact]
        public async Task TestGetAllCountry()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbServiceFunctionsMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplication");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            Country fakeCountry = new Country() {Id = 1, Name = "United States", TwoLetterIsoCode = "US"};
            List<Country> fakeCountryList = new List<Country>()
            {
                fakeCountry
            };

            applicationContext.Add<Country>(fakeCountry);
            await applicationContext.SaveChangesAsync();

            var service = new InfoService(
                new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories()))
                , new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object, dbServiceFunctionsMock.Object
            );

            TenantModel tenant = new TenantModel()
            {
                Id = 1,
                Code = "fakeTenantCode",
                Urls = new List<UrlModel>() { new UrlModel() { Url = "www.tenant.com" } }
            };

            // Act
            var results = await service.GetAllCountry(tenant);

            // Assert
            Assert.NotNull(results);
            Assert.NotEmpty(results);
        }

        [Fact]
        public async Task TestGetAllOwnershipType()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbServiceFunctionsMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplication");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            List<OwnershipType> fakeOwnershipTypes = new List<OwnershipType>()
            {
                new OwnershipType() {Description = "Usage 1", IsActive = true, DisplayOrder = 1, IsDeleted = false },
            };

            dbServiceFunctionsMock.Setup(x => x.UdfOwnershipType(It.IsAny<int>(), It.IsAny<int?>()))
                .Returns(fakeOwnershipTypes.AsQueryable);

            var service = new InfoService(
                new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories()))
                , new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object, dbServiceFunctionsMock.Object
            );

            TenantModel tenant = new TenantModel()
            {
                Id = 1,
                Code = "fakeTenantCode",
                Urls = new List<UrlModel>() { new UrlModel() { Url = "www.tenant.com" } }
            };

            // Act
            var results = service.GetAllOwnershipType(tenant);

            // Assert
            Assert.NotNull(results);
            Assert.NotEmpty(results);
        }

        [Fact]
        public async Task TestGetAllLoanGoal()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbServiceFunctionsMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplication");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            List<LoanGoal> fakeLoanGoals = new List<LoanGoal>()
            {
                new LoanGoal() { Id = 1, Description = "Fake Loan GOal", IsActive = true, DisplayOrder = 1, IsDeleted = false, LoanPurposeId =  1},
            };

            dbServiceFunctionsMock.Setup(x => x.UdfLoanGoal(It.IsAny<int>(), It.IsAny<int?>()))
                .Returns(fakeLoanGoals.AsQueryable);

            var service = new InfoService(
                new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories()))
                , new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object, dbServiceFunctionsMock.Object
            );

            TenantModel tenant = new TenantModel()
            {
                Id = 1,
                Code = "fakeTenantCode",
                Urls = new List<UrlModel>() { new UrlModel() { Url = "www.tenant.com" } }
            };

            // Act
            var results = service.GetAllLoanGoal(tenant, 1);

            // Assert
            Assert.NotNull(results);
            Assert.NotEmpty(results);
        }

        [Fact]
        public async Task TestGetAllLoanPurpose()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbServiceFunctionsMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplication");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            List<LoanPurpose> fakeLoanPurposes = new List<LoanPurpose>()
            {
                new LoanPurpose() { Id = 1, Description = "Fake Loan Purpose", IsActive = true, DisplayOrder = 1, IsDeleted = false },
            };

            dbServiceFunctionsMock.Setup(x => x.UdfLoanPurpose(It.IsAny<int>(), It.IsAny<int?>()))
                .Returns(fakeLoanPurposes.AsQueryable);

            var service = new InfoService(
                new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories()))
                , new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object, dbServiceFunctionsMock.Object
            );

            TenantModel tenant = new TenantModel()
            {
                Id = 1,
                Code = "fakeTenantCode",
                Urls = new List<UrlModel>() { new UrlModel() { Url = "www.tenant.com" } }
            };

            // Act
            var results = service.GetAllLoanPurpose(tenant);

            // Assert
            Assert.NotNull(results);
            Assert.NotEmpty(results);
        }

        [Fact]
        public async Task TestGetAllMaritalStatus()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbServiceFunctionsMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplication");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            List<MaritalStatusList> fakeList = new List<MaritalStatusList>()
            {
                new MaritalStatusList() { Id = 1, Description = "Married", IsActive = true, DisplayOrder = 1, IsDeleted = false },
            };

            dbServiceFunctionsMock.Setup(x => x.UdfMaritalStatusList(It.IsAny<int>(), It.IsAny<int?>()))
                .Returns(fakeList.AsQueryable);

            var service = new InfoService(
                new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories()))
                , new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object, dbServiceFunctionsMock.Object
            );

            TenantModel tenant = new TenantModel()
            {
                Id = 1,
                Code = "fakeTenantCode",
                Urls = new List<UrlModel>() { new UrlModel() { Url = "www.tenant.com" } }
            };

            // Act
            var results = service.GetAllMaritalStatus(tenant);

            // Assert
            Assert.NotNull(results);
            Assert.NotEmpty(results);
        }

        [Fact]
        public async Task TestGetAllMilitaryAffiliation()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbServiceFunctionsMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplication");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            List<MilitaryAffiliation> fakeList = new List<MilitaryAffiliation>()
            {
                new MilitaryAffiliation() { Id = 1, Description = "Description 1", IsActive = true, DisplayOrder = 1, IsDeleted = false },
            };

            dbServiceFunctionsMock.Setup(x => x.UdfMilitaryAffiliation(It.IsAny<int>(), It.IsAny<int?>()))
                .Returns(fakeList.AsQueryable);

            var service = new InfoService(
                new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories()))
                , new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object, dbServiceFunctionsMock.Object
            );

            TenantModel tenant = new TenantModel()
            {
                Id = 1,
                Code = "fakeTenantCode",
                Urls = new List<UrlModel>() { new UrlModel() { Url = "www.tenant.com" } }
            };

            // Act
            var results = service.GetAllMilitaryAffiliation(tenant);

            // Assert
            Assert.NotNull(results);
            Assert.NotEmpty(results);
        }

        [Fact]
        public async Task TestGetSearchByString()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbServiceFunctionsMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplication");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            List<CustomerSearch> fakeList = new List<CustomerSearch>()
            {
                new CustomerSearch() { Ids = "1", label = "some label", StateName = "fake state"}
            };

            dbServiceFunctionsMock.Setup(x => x.CustomerSearchByString(It.IsAny<string>()))
                .Returns(fakeList.AsQueryable);

            var service = new InfoService(
                new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories()))
                , new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object, dbServiceFunctionsMock.Object
            );

            TenantModel tenant = new TenantModel()
            {
                Id = 1,
                Code = "fakeTenantCode",
                Urls = new List<UrlModel>() { new UrlModel() { Url = "www.tenant.com" } }
            };

            // Act
            var results = service.GetSearchByString("xyz");

            // Assert
            Assert.NotNull(results);
            Assert.NotEmpty(results);
        }

        [Fact]
        public async Task TestGetSearchByZipcode()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbServiceFunctionsMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplication");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            List<CustomerSearch> fakeList = new List<CustomerSearch>()
            {
                new CustomerSearch() { Ids = "1", label = "some label", StateName = "fake state"}
            };

            dbServiceFunctionsMock.Setup(x => x.CustomerSearchByZipcode(It.IsAny<int>()))
                .Returns(fakeList.AsQueryable);

            var service = new InfoService(
                new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories()))
                , new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object, dbServiceFunctionsMock.Object
            );

            TenantModel tenant = new TenantModel()
            {
                Id = 1,
                Code = "fakeTenantCode",
                Urls = new List<UrlModel>() { new UrlModel() { Url = "www.tenant.com" } }
            };

            // Act
            var results = service.GetSearchByZipcode(123);

            // Assert
            Assert.NotNull(results);
            Assert.NotEmpty(results);
        }

        [Fact]
        public async Task TestGetTenantState()
        {
            // Arrange
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            Mock<IDbFunctionService> dbServiceFunctionsMock = new Mock<IDbFunctionService>();

            DbContextOptions<LoanApplicationContext> loanOptions;
            var loanBuilder = new DbContextOptionsBuilder<LoanApplicationContext>();
            loanBuilder.UseInMemoryDatabase("LoanApplication");
            loanOptions = loanBuilder.Options;
            using LoanApplicationContext applicationContext = new LoanApplicationContext(loanOptions);
            applicationContext.Database.EnsureCreated();
            await applicationContext.SaveChangesAsync();

            DbContextOptions<TenantConfigContext> tenantOptions;
            var tenantBuilder = new DbContextOptionsBuilder<TenantConfigContext>();
            tenantBuilder.UseInMemoryDatabase("TenantContext");
            tenantOptions = tenantBuilder.Options;
            using TenantConfigContext tenantContext = new TenantConfigContext(tenantOptions);
            tenantContext.Database.EnsureCreated();
            await tenantContext.SaveChangesAsync();

            List<State> fakeList = new List<State>()
            {
                new State() { Abbreviation = "AS"}
            };

            dbServiceFunctionsMock.Setup(x => x.UdfState(It.IsAny<int>(), It.IsAny<int?>()))
                .Returns(fakeList.AsQueryable);

            var service = new InfoService(
                new UnitOfWork<TenantConfigContext>(tenantContext, new RepositoryProvider(new RepositoryFactories()))
                , new UnitOfWork<LoanApplicationContext>(applicationContext,
                    new RepositoryProvider(new RepositoryFactories()))
                , serviceProviderMock.Object, dbServiceFunctionsMock.Object
            );

            TenantModel tenant = new TenantModel()
            {
                Id = 1,
                Code = "fakeTenantCode",
                Urls = new List<UrlModel>() { new UrlModel() { Url = "www.tenant.com" } }
            };

            // Act
            var results = service.GetTenantState(tenant);

            // Assert
            Assert.NotNull(results);
            Assert.NotEmpty(results);
        }
    }
}
