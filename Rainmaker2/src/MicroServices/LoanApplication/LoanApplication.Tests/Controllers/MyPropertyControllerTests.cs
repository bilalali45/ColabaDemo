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
    public class MyPropertyControllerTests
    {
        [Fact]
        public async Task GetBorrowerAdditionalPropertyAddress_ShouldReturnOkObjectResult()
        {
            //Arrange
            var myPropertyService = new Mock<IMyPropertyService>();
            var controller = new MyPropertyController(myPropertyService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetBorrowerAdditionalPropertyAddress(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddOrUpdatBorrowerAdditionalPropertyAddress_ShouldReturnOkObjectResult()
        {
            //Arrange
            var myPropertyService = new Mock<IMyPropertyService>();
            var controller = new MyPropertyController(myPropertyService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.AddOrUpdatBorrowerAdditionalPropertyAddress(new BorrowerAdditionalPropertyAddressRequestModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBorrowerAdditionalPropertyInfo_ShouldReturnOkObjectResult()
        {
            //Arrange
            var myPropertyService = new Mock<IMyPropertyService>();
            var controller = new MyPropertyController(myPropertyService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetBorrowerAdditionalPropertyInfo(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddOrUpdateAdditionalPropertyInfo_ShouldReturnOkObjectResult()
        {
            //Arrange
            var myPropertyService = new Mock<IMyPropertyService>();
            var controller = new MyPropertyController(myPropertyService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.AddOrUpdateAdditionalPropertyInfo(new BorrowerAdditionalPropertyInfoRequestModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddOrUpdateAdditionalPropertyType_ShouldReturnOkObjectResult()
        {
            //Arrange
            var myPropertyService = new Mock<IMyPropertyService>();
            var controller = new MyPropertyController(myPropertyService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.AddOrUpdateAdditionalPropertyType(new BorrowerAdditionalPropertyRequestModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBorrowerAdditionalPropertyType_ShouldReturnOkObjectResult()
        {
            //Arrange
            var myPropertyService = new Mock<IMyPropertyService>();
            var controller = new MyPropertyController(myPropertyService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetBorrowerAdditionalPropertyType(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBorrowerPrimaryPropertyType_ShouldReturnOkObjectResult()
        {
            //Arrange
            var myPropertyService = new Mock<IMyPropertyService>();
            var controller = new MyPropertyController(myPropertyService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetBorrowerPrimaryPropertyType(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddOrUpdatePrimaryPropertyType_ShouldReturnOkObjectResult()
        {
            //Arrange
            var myPropertyService = new Mock<IMyPropertyService>();
            var controller = new MyPropertyController(myPropertyService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.AddOrUpdatePrimaryPropertyType(new BorrowerPropertyRequestModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetPropertyList_ShouldReturnOkObjectResult()
        {
            //Arrange
            var myPropertyService = new Mock<IMyPropertyService>();
            var controller = new MyPropertyController(myPropertyService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetPropertyList(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DoYouHaveFirstMortgage_ShouldReturnOkObjectResult()
        {
            //Arrange
            var myPropertyService = new Mock<IMyPropertyService>();
            var controller = new MyPropertyController(myPropertyService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.DoYouHaveFirstMortgage(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddOrUpdateHasFirstMortgage_ShouldReturnOkObjectResult()
        {
            //Arrange
            var myPropertyService = new Mock<IMyPropertyService>();
            var controller = new MyPropertyController(myPropertyService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.AddOrUpdateHasFirstMortgage(new HasFirstMortgageModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DoYouHaveSecondMortgage_ShouldReturnOkObjectResult()
        {
            //Arrange
            var myPropertyService = new Mock<IMyPropertyService>();
            var controller = new MyPropertyController(myPropertyService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.DoYouHaveSecondMortgage(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddOrUpdateHasSecondMortgage_ShouldReturnOkObjectResult()
        {
            //Arrange
            var myPropertyService = new Mock<IMyPropertyService>();
            var controller = new MyPropertyController(myPropertyService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.AddOrUpdateHasSecondMortgage(new HasSecondMortgageModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetFirstMortgageValue_ShouldReturnOkObjectResult()
        {
            //Arrange
            var myPropertyService = new Mock<IMyPropertyService>();
            var controller = new MyPropertyController(myPropertyService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetFirstMortgageValue(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetFinalScreenReview_ShouldReturnOkObjectResult()
        {
            //Arrange
            var myPropertyService = new Mock<IMyPropertyService>();
            var controller = new MyPropertyController(myPropertyService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetFinalScreenReview(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetSecondMortgageValue_ShouldReturnOkObjectResult()
        {
            //Arrange
            var myPropertyService = new Mock<IMyPropertyService>();
            var controller = new MyPropertyController(myPropertyService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetSecondMortgageValue(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddOrUpdateSecondMortgageValue_ShouldReturnOkObjectResult()
        {
            //Arrange
            var myPropertyService = new Mock<IMyPropertyService>();
            var controller = new MyPropertyController(myPropertyService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.AddOrUpdateSecondMortgageValue(new SecondMortgageModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddOrUpdateFirstMortgageValue_ShouldReturnOkObjectResult()
        {
            //Arrange
            var myPropertyService = new Mock<IMyPropertyService>();
            var controller = new MyPropertyController(myPropertyService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.AddOrUpdateFirstMortgageValue(new FirstMortgageModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}