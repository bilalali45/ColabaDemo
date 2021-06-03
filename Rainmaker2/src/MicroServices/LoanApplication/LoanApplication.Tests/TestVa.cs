using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using LoanApplication.API.Controllers;
using LoanApplication.Model;
using LoanApplication.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TenantConfig.Common.DistributedCache;
using Xunit;

namespace LoanApplication.Tests
{
    public class TestVa
    {
        [Fact]
        public async Task TestGetBorrowerVaDetail()
        {
            // Arrange
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IVaDetailService> vaDetailServiceMock = new Mock<IVaDetailService>();

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

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() { User =  user} };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            BorrowerIdModel model = new BorrowerIdModel()
            {
                BorrowerId = 1
            };
            BorrowerVaDetailGetModel fakeModelReturned = new BorrowerVaDetailGetModel()
            {
                IsVaEligible = true,
                VaDetails = new ArrayList()
            };
            vaDetailServiceMock.Setup(x => x.GetVaDetails(contextTenant, fakeUserId, model.BorrowerId)).ReturnsAsync(fakeModelReturned);

            var controller = new VaController(infoServiceMock.Object, vaDetailServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            // Act
            var results = await controller.GetBorrowerVaDetail(model);

            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestAddOrUpdateBorrowerVaStatus()
        {
            // Arrange
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IVaDetailService> vaDetailServiceMock = new Mock<IVaDetailService>();

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
            
            VaDetailAddOrUpdate fakeModelReceived = new VaDetailAddOrUpdate()
            {
                IsVaEligible = true,
                ExpirationDateUtc = null,
                BorrowerId = 1,
                ReserveEverActivated = null,
                State = "fakestate",
                MilitaryAffiliationIds = null
            };
            vaDetailServiceMock.Setup(x => x.AddOrUpdate(contextTenant, fakeUserId, fakeModelReceived)).ReturnsAsync(1);

            var controller = new VaController(infoServiceMock.Object, vaDetailServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            // Act
            var results = await controller.AddOrUpdateBorrowerVaStatus(fakeModelReceived);

            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestGetMilitaryAffiliationsList()
        {
            // Arrange
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IVaDetailService> vaDetailServiceMock = new Mock<IVaDetailService>();

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
            

            var contextMock = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            contextMock.HttpContext.Items = new ConcurrentDictionary<object, object>();
            contextMock.HttpContext.Items[Constants.COLABA_TENANT] = contextTenant;

            List<MilitaryAffiliationModel> fakeModelToReturn = new List<MilitaryAffiliationModel>
            {
                new MilitaryAffiliationModel() {Id = 1, Name = "fakeName"}
            };
            infoServiceMock.Setup(x => x.GetAllMilitaryAffiliation(contextTenant)).Returns(fakeModelToReturn);

            var controller = new VaController(infoServiceMock.Object, vaDetailServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            // Act
            var results = await controller.GetMilitaryAffiliationsList();

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestSetBorrowerVaStatus()
        {
            // Arrange
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IVaDetailService> vaDetailServiceMock = new Mock<IVaDetailService>();

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

            BorrowerVaStatusModel fakeModelReceived = new BorrowerVaStatusModel()
            {
                IsVaEligible = true,
            };
            vaDetailServiceMock.Setup(x => x.SetBorrowerVaStatus(contextTenant, fakeUserId, fakeModelReceived)).ReturnsAsync(1);

            var controller = new VaController(infoServiceMock.Object, vaDetailServiceMock.Object);
            controller.ControllerContext.HttpContext = contextMock.HttpContext;

            // Act
            var results = await controller.SetBorrowerVaStatus(fakeModelReceived);

            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
    }
}
