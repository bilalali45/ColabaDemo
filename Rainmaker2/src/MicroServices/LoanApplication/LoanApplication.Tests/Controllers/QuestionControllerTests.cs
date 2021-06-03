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
    public class QuestionControllerTests
    {
        [Fact]
        public async Task GetSection2PrimaryQuestions_ShouldReturnOkObjectResult()
        {
            //Arrange
            var questionService = new Mock<IQuestionService>();
            var controller = new QuestionController(questionService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetSection2PrimaryQuestions(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetSection2SecondaryQuestion_ShouldReturnOkObjectResult()
        {
            //Arrange
            var questionService = new Mock<IQuestionService>();
            var controller = new QuestionController(questionService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetSection2SecondaryQuestion(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetSectionOneForPrimaryBorrower_ShouldReturnOkObjectResult()
        {
            //Arrange
            var questionService = new Mock<IQuestionService>();
            var controller = new QuestionController(questionService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetSectionOneForPrimaryBorrower(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetSectionOneForSecondaryBorrower_ShouldReturnOkObjectResult()
        {
            //Arrange
            var questionService = new Mock<IQuestionService>();
            var controller = new QuestionController(questionService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetSectionOneForSecondaryBorrower(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddOrUpdateSection2_ShouldReturnOkObjectResult()
        {
            //Arrange
            var questionService = new Mock<IQuestionService>();
            var controller = new QuestionController(questionService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.AddOrUpdateSection2(new QuestionRequestModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddOrUpdateSectionOne_ShouldReturnOkObjectResult()
        {
            //Arrange
            var questionService = new Mock<IQuestionService>();
            var controller = new QuestionController(questionService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.AddOrUpdateSectionOne(new QuestionRequestModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetSection3ForPrimaryBorrower_ShouldReturnOkObjectResult()
        {
            //Arrange
            var questionService = new Mock<IQuestionService>();
            var controller = new QuestionController(questionService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetSection3ForPrimaryBorrower(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetPropertyDropDown_ShouldReturnOkObjectResult()
        {
            //Arrange
            var questionService = new Mock<IQuestionService>();
            var controller = new QuestionController(questionService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result =  controller.GetPropertyUsageDropDown();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetSection3ForSecondaryBorrower_ShouldReturnOkObjectResult()
        {
            //Arrange
            var questionService = new Mock<IQuestionService>();
            var controller = new QuestionController(questionService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetSection3ForSecondaryBorrower(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllTitleHeldWithDropDown_ShouldReturnOkObjectResult()
        {
            //Arrange
            var questionService = new Mock<IQuestionService>();
            var controller = new QuestionController(questionService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = controller.GetAllTitleHeldWithDropDown();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllBankruptcy_ShouldReturnOkObjectResult()
        {
            //Arrange
            var questionService = new Mock<IQuestionService>();
            var controller = new QuestionController(questionService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = controller.GetAllBankruptcy();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllLiablilityType_ShouldReturnOkObjectResult()
        {
            //Arrange
            var questionService = new Mock<IQuestionService>();
            var controller = new QuestionController(questionService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = controller.GetAllLiablilityType();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddOrUpdateSection3_ShouldReturnOkObjectResult()
        {
            //Arrange
            var questionService = new Mock<IQuestionService>();
            var controller = new QuestionController(questionService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.AddOrUpdateSection3(new QuestionRequestModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllRaceList_ShouldReturnOkObjectResult()
        {
            //Arrange
            var questionService = new Mock<IQuestionService>();
            var controller = new QuestionController(questionService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = controller.GetAllRaceList();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetGenderList_ShouldReturnOkObjectResult()
        {
            //Arrange
            var questionService = new Mock<IQuestionService>();
            var controller = new QuestionController(questionService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = controller.GetGenderList();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllEthnicityList_ShouldReturnOkObjectResult()
        {
            //Arrange
            var questionService = new Mock<IQuestionService>();
            var controller = new QuestionController(questionService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = controller.GetAllEthnicityList();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetDemographicInformation_ShouldReturnOkObjectResult()
        {
            //Arrange
            var questionService = new Mock<IQuestionService>();
            var controller = new QuestionController(questionService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetDemographicInformation(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddOrUpdateDemogrhphicInfo_ShouldReturnOkObjectResult()
        {
            //Arrange
            var questionService = new Mock<IQuestionService>();
            var controller = new QuestionController(questionService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.AddOrUpdateDemogrhphicInfo(new DemographicInfoResponseModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CheckPrimaryBorrowerSubjectProperty_ShouldReturnOkObjectResult()
        {
            //Arrange
            var questionService = new Mock<IQuestionService>();
            var controller = new QuestionController(questionService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.CheckPrimaryBorrowerSubjectProperty(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CheckSecondaryBorrowerSubjectProperty_ShouldReturnOkObjectResult()
        {
            //Arrange
            var questionService = new Mock<IQuestionService>();
            var controller = new QuestionController(questionService.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.CheckSecondaryBorrowerSubjectProperty(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}