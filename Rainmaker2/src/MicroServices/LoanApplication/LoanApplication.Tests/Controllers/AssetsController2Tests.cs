using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using LoanApplication.API.Controllers;
using LoanApplication.Model;
using LoanApplication.Service;
using LoanApplication.Tests.Extensions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TenantConfig.Common.DistributedCache;
using Xunit;

namespace LoanApplication.Tests.Controllers
{
    public partial class AssetsControllerTests
    {
        [Fact]
        public async Task TestGetAllAssetCategories()
        {
            // Arrange
            Mock<IAssetsService> assetServiceMock = new Mock<IAssetsService>();
            List<GetAllAssetCategoriesModel> fakeCategories = new List<GetAllAssetCategoriesModel>();
            assetServiceMock.Setup(x => x.GetAllAssetCategories(It.IsAny<TenantModel>())).Returns(fakeCategories);

            var controller = new AssetsController(assetServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            // Act
            var results = await controller.GetAllAssetCategories();

            //Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        #region GetAssetTypesByCategory

        [Fact]
        public void TestGetAssetTypesByCategoryWhenCategoryIdIsNotValid()
        {
            // Arrange
            Mock<IAssetsService> assetServiceMock = new Mock<IAssetsService>();
            var fakeCategories = new List<GetAssetTypesByCategoryModel>();
            assetServiceMock.Setup(x => x.GetAssetTypesByCategory(It.IsAny<TenantModel>(), It.IsAny<int>()))
                .Returns(fakeCategories);

            var controller = new AssetsController(assetServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            int categoryId = 0;

            // Act
            var results = controller.GetAssetTypesByCategory(categoryId);

            //Assert
            Assert.NotNull(results);
            Assert.IsType<BadRequestObjectResult>(results);
        }

        [Fact]
        public void TestGetAssetTypesByCategoryWhenCategoryIdIsValid()
        {
            // Arrange
            Mock<IAssetsService> assetServiceMock = new Mock<IAssetsService>();
            var fakeCategories = new List<GetAssetTypesByCategoryModel>();
            assetServiceMock.Setup(x => x.GetAssetTypesByCategory(It.IsAny<TenantModel>(), It.IsAny<int>()))
                .Returns(fakeCategories);

            var controller = new AssetsController(assetServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            int categoryId = 1;

            // Act
            var results = controller.GetAssetTypesByCategory(categoryId);

            //Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        #endregion

        #region AddOrUpdateBorrowerAsset

        [Fact]
        public async Task TestAddOrUpdateBorrowerAssetWhenErrorOccurs()
        {
            // Arrange
            Mock<IAssetsService> assetServiceMock = new Mock<IAssetsService>();
            AddOrUpdateBorrowerAssetModel fakePostModel = new AddOrUpdateBorrowerAssetModel();
            int fakeErrorIdentifier = -1;
            assetServiceMock.Setup(x => x.AddOrUpdateBorrowerAsset(It.IsAny<TenantModel>(), It.IsAny<int>(),
                It.IsAny<AddOrUpdateBorrowerAssetModel>())).ReturnsAsync(fakeErrorIdentifier);
            
            Dictionary<int, string> fakeErrorList = new Dictionary<int, string>()
            {
                [fakeErrorIdentifier] = "Fake Error Message"
            };
            assetServiceMock.Setup(x => x.ErrorMessages).Returns(fakeErrorList);


            var controller = new AssetsController(assetServiceMock.Object);
            controller.MockHttpContext(tenantId:1, userId:1);

            // Act
            var results = await controller.AddOrUpdateBorrowerAsset(fakePostModel);

            //Assert
            Assert.NotNull(results);
            Assert.IsType<BadRequestObjectResult>(results);
        }
        [Fact]
        public async Task TestAddOrUpdateBorrowerAsset()
        {
            // Arrange
            Mock<IAssetsService> assetServiceMock = new Mock<IAssetsService>();
            AddOrUpdateBorrowerAssetModel fakePostModel = new AddOrUpdateBorrowerAssetModel();
            int fakeErrorIdentifier = -1;
            int fakeAssetId = 1;
            assetServiceMock.Setup(x => x.AddOrUpdateBorrowerAsset(It.IsAny<TenantModel>(), It.IsAny<int>(),
                It.IsAny<AddOrUpdateBorrowerAssetModel>())).ReturnsAsync(fakeAssetId);
            
            Dictionary<int, string> fakeErrorList = new Dictionary<int, string>()
            {
                [fakeErrorIdentifier] = "Fake Error Message"
            };
            assetServiceMock.Setup(x => x.ErrorMessages).Returns(fakeErrorList);


            var controller = new AssetsController(assetServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            // Act
            var results = await controller.AddOrUpdateBorrowerAsset(fakePostModel);

            //Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        #endregion

        #region GetLoanApplicationBorrowersAssets

        [Fact]
        public async Task TestGetLoanApplicationBorrowersAssetsWhenErrorOccurs()
        {
            // Arrange
            Mock<IAssetsService> assetServiceMock = new Mock<IAssetsService>();
            LoanApplicationIdModel fakeGetModel = new LoanApplicationIdModel()
            {
                LoanApplicationId = 1
            };
            LoanApplicationBorrowersAssets fakeModelToReturn = new LoanApplicationBorrowersAssets()
            {
                ErrorMessage = "Fake error message"
            };
            assetServiceMock.Setup(x => x.GetLoanApplicationBorrowersAssets(It.IsAny<TenantModel>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(fakeModelToReturn);
            
            var controller = new AssetsController(assetServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            // Act
            var results = await controller.GetLoanApplicationBorrowersAssets(fakeGetModel);

            //Assert
            Assert.NotNull(results);
            Assert.IsType<BadRequestObjectResult>(results);
        }
        [Fact]
        public async Task TestGetLoanApplicationBorrowersAssets()
        {
            // Arrange
            Mock<IAssetsService> assetServiceMock = new Mock<IAssetsService>();
            LoanApplicationIdModel fakeGetModel = new LoanApplicationIdModel()
            {
                LoanApplicationId = 1
            };
            LoanApplicationBorrowersAssets fakeModelToReturn = new LoanApplicationBorrowersAssets()
            {
                ErrorMessage = string.Empty
            };
            assetServiceMock.Setup(x => x.GetLoanApplicationBorrowersAssets(It.IsAny<TenantModel>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(fakeModelToReturn);

            var controller = new AssetsController(assetServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            // Act
            var results = await controller.GetLoanApplicationBorrowersAssets(fakeGetModel);

            //Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        #endregion

        #region GetBorrowerAssets

        [Fact]
        public async Task TesGetBorrowerAssetssWhenErrorOccurs()
        {
            // Arrange
            Mock<IAssetsService> assetServiceMock = new Mock<IAssetsService>();
            BorrowerAssetDetailGetModel fakeGetModel = new BorrowerAssetDetailGetModel()
            {
                LoanApplicationId = 1,
                BorrowerId = 1
            };
            BorrowerAssetsGetModel fakeModelToReturn = new BorrowerAssetsGetModel()
            {
                ErrorMessage = "Fake error message"
            };
            assetServiceMock.Setup(x => x.GetBorrowerAssets(It.IsAny<TenantModel>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(fakeModelToReturn);

            var controller = new AssetsController(assetServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            // Act
            var results = await controller.GetBorrowerAssets(fakeGetModel);

            //Assert
            Assert.NotNull(results);
            Assert.IsType<BadRequestObjectResult>(results);
        }
        [Fact]
        public async Task TestGetBorrowerAssets()
        {
            // Arrange
            Mock<IAssetsService> assetServiceMock = new Mock<IAssetsService>();
            BorrowerAssetDetailGetModel fakeGetModel = new BorrowerAssetDetailGetModel()
            {
                LoanApplicationId = 1,
                BorrowerId = 1
            };
            BorrowerAssetsGetModel fakeModelToReturn = new BorrowerAssetsGetModel()
            {
                ErrorMessage = string.Empty
            };
            assetServiceMock.Setup(x => x.GetBorrowerAssets(It.IsAny<TenantModel>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(fakeModelToReturn);

            var controller = new AssetsController(assetServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            // Act
            var results = await controller.GetBorrowerAssets(fakeGetModel);

            //Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        #endregion

        #region GetBorrowerAssetDetail

        [Fact]
        public async Task TesGetBorrowerAssetDetailErrorOccurs()
        {
            // Arrange
            Mock<IAssetsService> assetServiceMock = new Mock<IAssetsService>();
            BorrowerAssetDetailGetModel fakeGetModel = new BorrowerAssetDetailGetModel()
            {
                LoanApplicationId = 1,
                BorrowerId = 1
            };
            BorrowerAssetsGetModel fakeModelToReturn = new BorrowerAssetsGetModel()
            {
                ErrorMessage = "Fake error message"
            };
            assetServiceMock.Setup(x => x.GetBorrowerAssetDetail(It.IsAny<TenantModel>(), It.IsAny<int>(), It.IsAny<BorrowerAssetDetailGetModel>())).ReturnsAsync(fakeModelToReturn);

            var controller = new AssetsController(assetServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            // Act
            var results = await controller.GetBorrowerAssetDetail(fakeGetModel);

            //Assert
            Assert.NotNull(results);
            Assert.IsType<BadRequestObjectResult>(results);
        }
        [Fact]
        public async Task TestGetBorrowerAssetDetail()
        {
            // Arrange
            Mock<IAssetsService> assetServiceMock = new Mock<IAssetsService>();
            BorrowerAssetDetailGetModel fakeGetModel = new BorrowerAssetDetailGetModel()
            {
                LoanApplicationId = 1,
                BorrowerId = 1
            };
            BorrowerAssetsGetModel fakeModelToReturn = new BorrowerAssetsGetModel()
            {
                ErrorMessage = string.Empty
            };
            assetServiceMock.Setup(x => x.GetBorrowerAssetDetail(It.IsAny<TenantModel>(), It.IsAny<int>(), It.IsAny<BorrowerAssetDetailGetModel>())).ReturnsAsync(fakeModelToReturn);

            var controller = new AssetsController(assetServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            // Act
            var results = await controller.GetBorrowerAssetDetail(fakeGetModel);

            //Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        #endregion

        #region GetGiftSourceAssets

        [Fact]
        public async Task TestGetGiftSourceAssetsGiftSourceIdIsNotValid()
        {
            // Arrange
            Mock<IAssetsService> assetServiceMock = new Mock<IAssetsService>();
            var fakeGiftSourceList = new List<GetGiftSourceAssetsModel>();
            assetServiceMock.Setup(x => x.GetGiftSourceAssets(It.IsAny<TenantModel>(), It.IsAny<int>()))
                .ReturnsAsync(fakeGiftSourceList);

            var controller = new AssetsController(assetServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            int giftSourceId = 0;

            // Act
            var results = await controller.GetGiftSourceAssets(giftSourceId);

            //Assert
            Assert.NotNull(results);
            Assert.IsType<BadRequestObjectResult>(results);
        }

        [Fact]
        public async Task TestGetGiftSourceAssetsWhenGiftSourceIdIsValid()
        {
            // Arrange
            Mock<IAssetsService> assetServiceMock = new Mock<IAssetsService>();
            var fakeGiftSourceList = new List<GetGiftSourceAssetsModel>();
            assetServiceMock.Setup(x => x.GetGiftSourceAssets(It.IsAny<TenantModel>(), It.IsAny<int>()))
                .ReturnsAsync(fakeGiftSourceList);

            var controller = new AssetsController(assetServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            int giftSourceId = 1;

            // Act
            var results = await controller.GetGiftSourceAssets(giftSourceId);

            //Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        #endregion

        [Fact]
        public void TestGetCollateralAssetTypes()
        {
            // Arrange
            Mock<IAssetsService> assetServiceMock = new Mock<IAssetsService>();
            List<GetCollateralAssetTypesModel> fakeCategories = new List<GetCollateralAssetTypesModel>();
            assetServiceMock.Setup(x => x.GetCollateralAssetTypes(It.IsAny<TenantModel>())).Returns(fakeCategories);

            var controller = new AssetsController(assetServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            // Act
            var results = controller.GetCollateralAssetTypes();

            //Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
    }
}
