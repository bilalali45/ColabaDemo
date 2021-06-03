using LoanApplication.API.Controllers;
using LoanApplication.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;
using Xunit;
using LoanApplication.Tests.Extensions;
using LoanApplication.Model;

namespace LoanApplication.Tests
{
    public class AssetsTest
    {
        [Fact]
        public async Task TestGetFromLoanNonRealState()
        {
            // Arrange
            Mock<IAssetsService> AssetsServiceMock = new Mock<IAssetsService>();

            int fakeUserId = 1;
            int faketanentId = 1;
            int fakeBorrowerAssetId = 1;
            int fakeAssetTypeId = 1;
            int fakeBorrowerId = 1;
            int fakeLoanApplicationId = 1;

            // Act
            var controller = new AssetsController(AssetsServiceMock.Object);
            controller.MockHttpContext(faketanentId, fakeUserId);

            var results = await controller.GetFromLoanNonRealState(fakeBorrowerAssetId, fakeAssetTypeId, fakeBorrowerId, fakeLoanApplicationId);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
        [Fact]
        public async Task TestAddOrUpdateAssestsNonRealState()
        {
            // Arrange
            Mock<IAssetsService> AssetsServiceMock = new Mock<IAssetsService>();

            int fakeUserId = 1;
            int faketanentId = 1;
            int fakeintReturn = 1;
            AddOrUpdateAssetModelNonRealState model = new AddOrUpdateAssetModelNonRealState();
            TenantModel contextTenant = new TenantModel()
            {
                Branches = new List<BranchModel>()
                {
                    new BranchModel()
                    {
                        Code = "someBranchCodeByZain", Id = 1, IsCorporate = true, LoanOfficers = new List<LoanOfficerModel>()
                    }
                }
            };

            // Act
            var controller = new AssetsController(AssetsServiceMock.Object);
            controller.MockHttpContext(faketanentId, fakeUserId);

            AssetsServiceMock.Setup(x => x.AddOrUpdateAssestsNonRealState(contextTenant, fakeUserId, model)).ReturnsAsync(fakeintReturn);

            var results = await controller.AddOrUpdateAssestsNonRealState(model);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkResult>(results);
        }

        [Fact]
        public async Task TestAddOrUpdateAssestsRealState()
        {
            // Arrange
            Mock<IAssetsService> AssetsServiceMock = new Mock<IAssetsService>();

            int fakeUserId = 1;
            int faketanentId = 1;

            AddOrUpdateAssetModelRealState model = new AddOrUpdateAssetModelRealState();


            // Act
            var controller = new AssetsController(AssetsServiceMock.Object);
            controller.MockHttpContext(faketanentId, fakeUserId);



            var results = await controller.AddOrUpdateAssestsRealState(model);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkResult>(results);
        }

        [Fact]
        public async Task TestGetFromLoanRealState()
        {
            // Arrange
            Mock<IAssetsService> AssetsServiceMock = new Mock<IAssetsService>();

            int fakeUserId = 1;
            int faketanentId = 1;
            int fakeBorrowerAssetId = 1;
            int fakeAssetTypeId = 1;
            int fakeBorrowerId = 1;
            int fakeLoanApplicationId = 1;



            // Act
            var controller = new AssetsController(AssetsServiceMock.Object);
            controller.MockHttpContext(faketanentId, fakeUserId);



            var results = await controller.GetFromLoanRealState(fakeBorrowerAssetId, fakeAssetTypeId, fakeBorrowerId, fakeLoanApplicationId);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
        [Fact]
        public async Task TestAddOrUpdateProceedsfromloan()
        {
            // Arrange
            Mock<IAssetsService> AssetsServiceMock = new Mock<IAssetsService>();

            int fakeUserId = 1;
            int faketanentId = 1;


            ProceedFromLoanModel fakemodel = new ProceedFromLoanModel();

            // Act
            var controller = new AssetsController(AssetsServiceMock.Object);
            controller.MockHttpContext(faketanentId, fakeUserId);



            var results = await controller.AddOrUpdateProceedsfromloan(fakemodel);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
        [Fact]
        public async Task TestAddOrUpdateProceedsfromloanOther()
        {
            // Arrange
            Mock<IAssetsService> AssetsServiceMock = new Mock<IAssetsService>();

            int fakeUserId = 1;
            int faketanentId = 1;


            ProceedFromLoanOtherModel fakemodel = new ProceedFromLoanOtherModel();

            // Act
            var controller = new AssetsController(AssetsServiceMock.Object);
            controller.MockHttpContext(faketanentId, fakeUserId);



            var results = await controller.AddOrUpdateProceedsfromloanOther(fakemodel);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestGetProceedsfromloan()
        {
            // Arrange
            Mock<IAssetsService> AssetsServiceMock = new Mock<IAssetsService>();

            int fakeUserId = 1;
            int faketanentId = 1;
            int fakeBorrowerAssetId = 1;
            int fakeAssetTypeId = 1;
            int fakeBorrowerId = 1;
            int fakeLoanApplicationId = 1;




            // Act
            var controller = new AssetsController(AssetsServiceMock.Object);
            controller.MockHttpContext(faketanentId, fakeUserId);



            var results = await controller.GetProceedsfromloan(fakeBorrowerAssetId, fakeAssetTypeId, fakeBorrowerId, fakeLoanApplicationId);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestGetAssetsHomeScreen()
        {
            // Arrange
            Mock<IAssetsService> AssetsServiceMock = new Mock<IAssetsService>();

            int fakeUserId = 1;
            int faketanentId = 1;

            int fakeLoanApplicationId = 1;

            // Act
            var controller = new AssetsController(AssetsServiceMock.Object);
            controller.MockHttpContext(faketanentId, fakeUserId);



            var results = await controller.GetAssetsHomeScreen(fakeLoanApplicationId);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
        [Fact]
        public async Task TestDeleteAsset()
        {
            // Arrange
            Mock<IAssetsService> AssetsServiceMock = new Mock<IAssetsService>();
            AssetDeleteModel fakePOstModel = new AssetDeleteModel()
            {
                AssetId = 1,
                BorrowerId = 1,
                LoanApplicationId = 1
            };

            int fakeUserId = 1;
            int faketanentId = 1;

            int fakeLoanApplicationId = 1;


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

            AssetsServiceMock.Setup(x => x.DeleteAsset(It.IsAny<TenantModel>(), It.IsAny<int>(), fakePOstModel)).ReturnsAsync(1);

            // Act
            var controller = new AssetsController(AssetsServiceMock.Object);
            controller.MockHttpContext(faketanentId, fakeUserId);


            var results = await controller.DeleteAsset(fakePOstModel);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

    }
}
