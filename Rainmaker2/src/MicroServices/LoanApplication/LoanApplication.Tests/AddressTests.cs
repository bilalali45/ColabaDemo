using LoanApplication.API.Controllers;
using LoanApplication.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;
using Xunit;

namespace LoanApplication.Tests
{
    public class AddressTests
    {
        [Fact]
        public async Task TestGetZipCodeByStateCountyCityName()
        {
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            mockInfoService.Setup(x => x.GetLocationSearchByZipCodeCityState(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new LoanApplicationDb.Data.LocationSearch { ZipPostalCode="4235"});

            var controller = new AddressController(mockInfoService.Object);

            var res = await controller.GetZipCodeByStateCountyCityName(null, null, null, null);

            var ok = Assert.IsType<OkObjectResult>(res);
            var location = Assert.IsType<LoanApplicationDb.Data.LocationSearch>(ok.Value);
            Assert.Equal("4235",location.ZipPostalCode);
        }
        [Fact]
        public async Task TestGetZipCodeByStateCountyCity()
        {
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            mockInfoService.Setup(x => x.GetZipCodesByStateCityAndCounty(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new Model.ZipcodeModel { ZipPostalCode = "4235" });

            var controller = new AddressController(mockInfoService.Object);

            var res = await controller.GetZipCodeByStateCountyCity(1,1,1);

            var ok = Assert.IsType<OkObjectResult>(res);
            var location = Assert.IsType<Model.ZipcodeModel>(ok.Value);
            Assert.Equal("4235", location.ZipPostalCode);
        }
        [Fact]
        public async Task TestGetSearchByStateCountyCity()
        {
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            mockInfoService.Setup(x => x.GetSearchByString(It.IsAny<string>())).Returns(new List<LoanApplicationDb.Data.CustomerSearch> { new LoanApplicationDb.Data.CustomerSearch { Ids="1"} });

            var controller = new AddressController(mockInfoService.Object);

            var res = await controller.GetSearchByStateCountyCity("");

            var ok = Assert.IsType<OkObjectResult>(res);
            var locations = Assert.IsType<List<LoanApplicationDb.Data.CustomerSearch>>(ok.Value);
            Assert.Equal("1", locations[0].Ids);
        }
        [Fact]
        public async Task TestGetAllCountry()
        {
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            mockInfoService.Setup(x => x.GetAllCountry(It.IsAny<TenantModel>())).ReturnsAsync(new List<Model.CountryModel> { new Model.CountryModel { Id=1} });
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
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", 1.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;
            var controller = new AddressController(mockInfoService.Object);
            controller.ControllerContext = contextMock;

            var res = await controller.GetAllCountry();

            var ok = Assert.IsType<OkObjectResult>(res);
            var locations = Assert.IsType<List<Model.CountryModel>>(ok.Value);
            Assert.Equal(1, locations[0].Id);
        }
        [Fact]
        public async Task TestGetAllState()
        {
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            mockInfoService.Setup(x => x.GetAllState(It.IsAny<TenantModel>(), It.IsAny<int?>())).ReturnsAsync(new List<Model.StateModel> { new Model.StateModel { Id = 1 } });
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
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", 1.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;
            var controller = new AddressController(mockInfoService.Object);
            controller.ControllerContext = contextMock;

            var res = await controller.GetAllState(1);

            var ok = Assert.IsType<OkObjectResult>(res);
            var locations = Assert.IsType<List<Model.StateModel>>(ok.Value);
            Assert.Equal(1, locations[0].Id);
        }
        [Fact]
        public async Task TestGetAllOwnershipType()
        {
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            mockInfoService.Setup(x => x.GetAllOwnershipType(It.IsAny<TenantModel>())).Returns(new List<Model.OwnershipTypeModel> { new Model.OwnershipTypeModel { Id = 1 } });
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
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", 1.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;
            var controller = new AddressController(mockInfoService.Object);
            controller.ControllerContext = contextMock;

            var res = await controller.GetAllOwnershipType();

            var ok = Assert.IsType<OkObjectResult>(res);
            var locations = Assert.IsType<List<Model.OwnershipTypeModel>>(ok.Value);
            Assert.Equal(1, locations[0].Id);
        }
        [Fact]
        public async Task TestGetSearchByZipcode()
        {
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            mockInfoService.Setup(x => x.GetSearchByZipcode(It.IsAny<int>())).Returns(new List<LoanApplicationDb.Data.CustomerSearch> { new LoanApplicationDb.Data.CustomerSearch { Ids = "1" } });

            var controller = new AddressController(mockInfoService.Object);

            var res = await controller.GetSearchByZipcode("12345");

            var ok = Assert.IsType<OkObjectResult>(res);
            var location = Assert.IsType<List<LoanApplicationDb.Data.CustomerSearch>>(ok.Value);
            Assert.Equal("1", location[0].Ids);
        }
        [Fact]
        public async Task TestGetSearchByZipcodeBadRequest()
        {
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            mockInfoService.Setup(x => x.GetSearchByZipcode(It.IsAny<int>())).Returns(new List<LoanApplicationDb.Data.CustomerSearch> { new LoanApplicationDb.Data.CustomerSearch { Ids = "1" } });

            var controller = new AddressController(mockInfoService.Object);

            var res = await controller.GetSearchByZipcode("");

            Assert.IsType<BadRequestObjectResult>(res);
        }
        [Fact]
        public async Task TestGetTenantState()
        {
            Mock<IInfoService> mockInfoService = new Mock<IInfoService>();
            mockInfoService.Setup(x => x.GetTenantState(It.IsAny<TenantModel>())).Returns(new List<Model.StateModel> { new Model.StateModel { Id = 1 } });
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
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", 1.ToString()),
            }, "mock"));

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;
            var controller = new AddressController(mockInfoService.Object);
            controller.ControllerContext = contextMock;

            var res = await controller.GetTenantState();

            var ok = Assert.IsType<OkObjectResult>(res);
            var locations = Assert.IsType<List<Model.StateModel>>(ok.Value);
            Assert.Equal(1, locations[0].Id);
        }
    }
}
