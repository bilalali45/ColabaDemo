using System;
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
    public class FinishingUpControllerTests
    {
        [Fact]
        public async Task GetSecondaryAddress_ShouldReturnOkObjectResult()
        {
            //Arrange
            var finishingUpService = new Mock<IFinishingUpService>();
            var controller = new FinishingUpController(finishingUpService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetSecondaryAddress(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBorrowerCitizenship_ShouldReturnOkObjectResult()
        {
            //Arrange
            var finishingUpService = new Mock<IFinishingUpService>();
            var controller = new FinishingUpController(finishingUpService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetBorrowerCitizenship(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetResidenceDetails_ShouldReturnOkObjectResult()
        {
            //Arrange
            var finishingUpService = new Mock<IFinishingUpService>();
            var controller = new FinishingUpController(finishingUpService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetResidenceDetails(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetResidenceHistory_ShouldReturnOkObjectResult()
        {
            //Arrange
            var finishingUpService = new Mock<IFinishingUpService>();
            var controller = new FinishingUpController(finishingUpService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetResidenceHistory(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddOrUpdateSecondaryAddress_ShouldReturnOkObjectResult()
        {
            //Arrange
            var finishingUpService = new Mock<IFinishingUpService>();
            var controller = new FinishingUpController(finishingUpService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.AddOrUpdateSecondaryAddress(new BorrowerSecondaryAddressRequestModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddOrUpdateBorrowerCitizenship_ShouldReturnOkObjectResult()
        {
            //Arrange
            var finishingUpService = new Mock<IFinishingUpService>();
            var controller = new FinishingUpController(finishingUpService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.AddOrUpdateBorrowerCitizenship(new BorrowerCitizenshipRequestModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task AddOrUpdateBorrowerCurrentResidenceMoveInDate_ShouldReturnOkObjectResult()
        {
            //Arrange
            var finishingUpService = new Mock<IFinishingUpService>();
            var controller = new FinishingUpController(finishingUpService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.AddOrUpdateBorrowerCurrentResidenceMoveInDate(new CurrentResidenceRequestModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetBorrowerPrimaryAddressDetail_ShouldReturnOkObjectResult()
        {
            //Arrange
            var finishingUpService = new Mock<IFinishingUpService>();
            var controller = new FinishingUpController(finishingUpService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetBorrowerPrimaryAddressDetail(1,1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetBorrowerCurrentResidenceMoveInDate_ShouldReturnOkObjectResult()
        {
            //Arrange
            var finishingUpService = new Mock<IFinishingUpService>();
            var controller = new FinishingUpController(finishingUpService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetBorrowerCurrentResidenceMoveInDate(1,1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetCoborrowerResidence_ShouldReturnOkObjectResult()
        {
            //Arrange
            var finishingUpService = new Mock<IFinishingUpService>();
            var controller = new FinishingUpController(finishingUpService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetCoborrowerResidence(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteSecondaryAddress_ShouldReturnOkObjectResult()
        {
            //Arrange
            var finishingUpService = new Mock<IFinishingUpService>();
            var controller = new FinishingUpController(finishingUpService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.DeleteSecondaryAddress(1,1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetDependentinfo_ShouldReturnOkObjectResult()
        {
            //Arrange
            var finishingUpService = new Mock<IFinishingUpService>();
            var controller = new FinishingUpController(finishingUpService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetDependentinfo(1,1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task AddOrUpdateDependentinfo_ShouldReturnOkObjectResult()
        {
            //Arrange
            var finishingUpService = new Mock<IFinishingUpService>();
            var controller = new FinishingUpController(finishingUpService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.AddOrUpdateDependentinfo(new DependentModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetAllSpouseInfo_ShouldReturnOkObjectResult()
        {
            //Arrange
            var finishingUpService = new Mock<IFinishingUpService>();
            var controller = new FinishingUpController(finishingUpService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetAllSpouseInfo(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        public async Task AddOrUpdateSpouseInfo_ShouldReturnOkObjectResult()
        {
            //Arrange
            var finishingUpService = new Mock<IFinishingUpService>();
            var controller = new FinishingUpController(finishingUpService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.AddOrUpdateSpouseInfo(new SpouseInfoRequestModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        public async Task GetSpouseInfo_ShouldReturnOkObjectResult()
        {
            //Arrange
            var finishingUpService = new Mock<IFinishingUpService>();
            var controller = new FinishingUpController(finishingUpService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetSpouseInfo(1,1,1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}