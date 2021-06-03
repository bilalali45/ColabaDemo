using System.Threading.Tasks;
using LoanApplication.API.Controllers;
using LoanApplication.Model;
using LoanApplication.Service;
using LoanApplication.Tests.Extensions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LoanApplication.Tests.Controllers
{
    public partial class AssetsControllerTests
    {
        [Fact]
        public async Task GetEarnestMoneyDeposit_ShouldReturnOkObjectResult()
        {
            //Arrange
            var assetsService = new Mock<IAssetsService>();
            var controller = new AssetsController(assetsService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetEarnestMoneyDeposit(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddOrUpdateEarnestMoneyDeposit_ShouldReturnOkObjectResult()
        {
            //Arrange
            var assetsService = new Mock<IAssetsService>();
            var controller = new AssetsController(assetsService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.UpdateEarnestMoneyDeposit(new EarnestMoneyDepositModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBankAccountType_ShouldReturnOkObjectResult()
        {
            //Arrange
            var assetsService = new Mock<IAssetsService>();
            var controller = new AssetsController(assetsService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetBankAccountType();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddOrUpdateBankAccount_ShouldReturnOkObjectResult()
        {
            //Arrange
            var assetsService = new Mock<IAssetsService>();
            var controller = new AssetsController(assetsService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            BorrowerAssetModelRequest fakemodel = new BorrowerAssetModelRequest()
            {
                AssetTypeId = 1
            };
            //Act
            var result = await controller.AddOrUpdateBankAccount(fakemodel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBankAccount_ShouldReturnOkObjectResult()
        {
            //Arrange
            var assetsService = new Mock<IAssetsService>();
            var controller = new AssetsController(assetsService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetBankAccount(borrowerAssetId: 1, borrowerId: 1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddOrUpdateGiftAsset_ShouldReturnOkObjectResult()
        {
            //Arrange
            var assetsService = new Mock<IAssetsService>();
            var controller = new AssetsController(assetsService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);
            GiftAssetModel fakemodel = new GiftAssetModel()
            {
                AssetTypeId = 10
            };
            //Act
            var result = await controller.AddOrUpdateGiftAsset(fakemodel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetGiftAsset_ShouldReturnOkObjectResult()
        {
            //Arrange
            var assetsService = new Mock<IAssetsService>();
            var controller = new AssetsController(assetsService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetGiftAsset(borrowerAssetId: 1, borrowerId: 1, loanApplicationId: 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetRetirementAccount_ShouldReturnOkObjectResult()
        {
            //Arrange
            var assetsService = new Mock<IAssetsService>();
            var controller = new AssetsController(assetsService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetRetirementAccount(1002, 1002, 2040);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateRetirementAccount_ShouldReturnOkObjectResult()
        {
            //Arrange
            var assetsService = new Mock<IAssetsService>();
            var controller = new AssetsController(assetsService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.UpdateRetirementAccount(new RetirementAccountModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllFinancialAsset_ShouldReturnOkObjectResult()
        {
            //Arrange
            var assetsService = new Mock<IAssetsService>();
            var controller = new AssetsController(assetsService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetAllFinancialAsset();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddOrUpdateFinancialAsset_ShouldReturnOkObjectResult()
        {
            //Arrange
            var assetsService = new Mock<IAssetsService>();
            var controller = new AssetsController(assetsService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);
            BorrowerAssetFinancialModelRequest fakemodel = new BorrowerAssetFinancialModelRequest()
            {
                AssetTypeId = 3
            };
            //Act
            var result = await controller.AddOrUpdateFinancialAsset(fakemodel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetFinancialAsset_ShouldReturnOkObjectResult()
        {
            //Arrange
            var assetsService = new Mock<IAssetsService>();
            var controller = new AssetsController(assetsService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetFinancialAsset(borrowerAssetId: 1, borrowerId: 1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

    }
}