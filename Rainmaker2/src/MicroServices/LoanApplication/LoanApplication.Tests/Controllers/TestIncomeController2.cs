using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
    public class TestIncomeController2
    {
        [Fact]
        public async Task TestAddOrUpdateCurrentEmploymentDetailWhenModelIsNull()
        {
            // Arrange
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IEmploymentService> employerServiceMock = new Mock<IEmploymentService>();

            var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, employerServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            // Act
            var results = await controller.AddOrUpdateCurrentEmploymentDetail(null);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<BadRequestObjectResult>(results);
        }

        [Fact]
        public async Task TestAddOrUpdateCurrentEmploymentDetailWhenDataNotFound()
        {
            // Arrange
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IEmploymentService> employerServiceMock = new Mock<IEmploymentService>();

            var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, employerServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            AddOrUpdateEmploymentDetailModel fakeModel = new AddOrUpdateEmploymentDetailModel()
            {
                LoanApplicationId = 1
            };

            int fakeErrorIdReturned = -1;

            employerServiceMock.Setup(x =>
                    x.AddOrUpdateCurrentEmploymentDetail(It.IsAny<TenantModel>(), It.IsAny<int>(), fakeModel))
                .ReturnsAsync(fakeErrorIdReturned);

            Dictionary<int, string> fakeMessageList = new Dictionary<int, string>()
            {
                [fakeErrorIdReturned] = "Loan application not found"
            };
            employerServiceMock.Setup(x => x.ErrorMessages).Returns(fakeMessageList);

            // Act
            var results = await controller.AddOrUpdateCurrentEmploymentDetail(fakeModel);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<BadRequestObjectResult>(results);
        }

        [Fact]
        public async Task TestAddOrUpdateCurrentEmploymentDetailWhenAllDataFound()
        {
            // Arrange
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IEmploymentService> employerServiceMock = new Mock<IEmploymentService>();

            var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, employerServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            AddOrUpdateEmploymentDetailModel fakeModel = new AddOrUpdateEmploymentDetailModel()
            {
                LoanApplicationId = 1
            };

            employerServiceMock.Setup(x =>
                    x.AddOrUpdateCurrentEmploymentDetail(It.IsAny<TenantModel>(), It.IsAny<int>(), fakeModel))
                .ReturnsAsync(1);

            // Act
            var results = await controller.AddOrUpdateCurrentEmploymentDetail(fakeModel);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestGetEmploymentDetailWhenErrorOccurs()
        {
            // Arrange
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IEmploymentService> employerServiceMock = new Mock<IEmploymentService>();

            var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, employerServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            EmploymentDetailBaseModel fakeModelToReturn = new EmploymentDetailBaseModel()
            {
                ErrorMessage = "Some fake error"
            };

            GetEmploymentDetailModel fakeGetModel = new GetEmploymentDetailModel()
            {
                LoanApplicationId = 1,
                BorrowerId = 1,
                IncomeInfoId = 1
            };

            employerServiceMock.Setup(x =>
                    x.GetEmploymentDetail(It.IsAny<TenantModel>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync(fakeModelToReturn);

            // Act
            var results = await controller.GetEmploymentDetail(fakeGetModel);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<BadRequestObjectResult>(results);
        }

        [Fact]
        public async Task TestGetEmploymentDetailWhenNoErrorOccurs()
        {
            // Arrange
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IEmploymentService> employerServiceMock = new Mock<IEmploymentService>();

            var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, employerServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            EmploymentDetailBaseModel fakeModelToReturn = new EmploymentDetailBaseModel()
            {
                ErrorMessage = string.Empty
            };

            GetEmploymentDetailModel fakeGetModel = new GetEmploymentDetailModel();

            employerServiceMock.Setup(x =>
                    x.GetEmploymentDetail(It.IsAny<TenantModel>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(fakeModelToReturn);



            // Act
            var results = await controller.GetEmploymentDetail(fakeGetModel);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestAddOrUpdatePreviousEmploymentDetailWhenModelIsNull()
        {
            // Arrange
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IEmploymentService> employerServiceMock = new Mock<IEmploymentService>();

            var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, employerServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            // Act
            var results = await controller.AddOrUpdatePreviousEmploymentDetail(null);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<BadRequestObjectResult>(results);
        }

        [Fact]
        public async Task TestAddOrUpdatePreviousEmploymentDetailWhenDataNotFound()
        {
            // Arrange
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IEmploymentService> employerServiceMock = new Mock<IEmploymentService>();

            var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, employerServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            AddOrUpdateEmploymentDetailModel fakeModel = new AddOrUpdateEmploymentDetailModel()
            {
                LoanApplicationId = 1
            };

            AddOrUpdatePreviousEmploymentDetailModel modelfake = new AddOrUpdatePreviousEmploymentDetailModel()
            {
                LoanApplicationId = 1
            };
            int fakeErrorIdReturned = -1;

            employerServiceMock.Setup(x =>
                    x.AddOrUpdatePreviousEmploymentDetail(It.IsAny<TenantModel>(), It.IsAny<int>(), It.IsAny<AddOrUpdatePreviousEmploymentDetailModel>()))
                .ReturnsAsync(fakeErrorIdReturned);
            
            Dictionary<int, string> fakeMessageList = new Dictionary<int, string>()
            {
                [fakeErrorIdReturned] = "Loan application not found"
            };
            employerServiceMock.Setup(x => x.ErrorMessages).Returns(fakeMessageList);

            // Act
            var results = await controller.AddOrUpdatePreviousEmploymentDetail(modelfake);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<BadRequestObjectResult>(results);
        }

        [Fact]
        public async Task TestAddOrUpdatePreviousEmploymentDetailWhenAllDataFound()
        {
            // Arrange
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IEmploymentService> employerServiceMock = new Mock<IEmploymentService>();

            var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, employerServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);
            
            AddOrUpdatePreviousEmploymentDetailModel modelfake = new AddOrUpdatePreviousEmploymentDetailModel()
            {
                LoanApplicationId = 1

            };
       

            employerServiceMock.Setup(x =>
                                          x.AddOrUpdatePreviousEmploymentDetail(It.IsAny<TenantModel>(), It.IsAny<int>(),It.IsAny<AddOrUpdatePreviousEmploymentDetailModel>() ))
                               .ReturnsAsync(1);
            
            // Act
            var results = await controller.AddOrUpdatePreviousEmploymentDetail(modelfake);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestDeleteEmploymentIncomeWhenErrorOccurs()
        {
            // Arrange
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IEmploymentService> employerServiceMock = new Mock<IEmploymentService>();

            var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, employerServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            CurrentEmploymentDeleteModel fakeDeleteModel = new CurrentEmploymentDeleteModel()
            {

            };

            int fakeErrorIdReturned = -1;
            Dictionary<int, string> fakeMessageList = new Dictionary<int, string>()
            {
                [fakeErrorIdReturned] = "Loan application not found"
            };
            employerServiceMock.Setup(x =>
                    x.DeleteIncomeDetail(It.IsAny<TenantModel>(), It.IsAny<int>(),
                        It.IsAny<CurrentEmploymentDeleteModel>()))
                .ReturnsAsync(fakeErrorIdReturned);
            employerServiceMock.Setup(x => x.ErrorMessages).Returns(fakeMessageList);

            // Act
            var results = await controller.DeleteEmploymentIncome(fakeDeleteModel);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<BadRequestObjectResult>(results);
        }

        [Fact]
        public async Task TestDeleteEmploymentIncomeWhenNoErrorOccurs()
        {
            // Arrange
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IEmploymentService> employerServiceMock = new Mock<IEmploymentService>();

            var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, employerServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            CurrentEmploymentDeleteModel fakeDeleteModel = new CurrentEmploymentDeleteModel()
            {

            };
            employerServiceMock.Setup(x =>
                    x.DeleteIncomeDetail(It.IsAny<TenantModel>(), It.IsAny<int>(),
                        It.IsAny<CurrentEmploymentDeleteModel>()))
                .ReturnsAsync(1);

            // Act
            var results = await controller.DeleteEmploymentIncome(fakeDeleteModel);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestGetAllBorrowersWithIncome()
        {
            // Arrange
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IEmploymentService> employerServiceMock = new Mock<IEmploymentService>();

            var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, employerServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            int fakeLoanApplicationId = 1;


            List<EmploymentDetailWithBorrower> fakeModelToReturn = new List<EmploymentDetailWithBorrower>()
            {

            };
            employerServiceMock.Setup(x =>
                    x.GetAllBorrowerWithIncome(It.IsAny<TenantModel>(), It.IsAny<int>(),
                        It.IsAny<int>()))
                .ReturnsAsync(fakeModelToReturn);

            // Act
            var results = await controller.GetIncomeSectionReview(new LoanApplicationIdModel() { LoanApplicationId = fakeLoanApplicationId });

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestGetBorrowersEmploymentHistoryWhenDataNotFound()
        {
            // Arrange
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IEmploymentService> employerServiceMock = new Mock<IEmploymentService>();

            var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, employerServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            int fakeErrorIdReturned = -1;
            EmploymentHistoryModel fakeModelToReturn = new EmploymentHistoryModel()
            {
                ErrorMessage = "This is a fake error message"
            };
            employerServiceMock.Setup(x =>
                    x.GetEmploymentHistory(It.IsAny<TenantModel>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(fakeModelToReturn);

            // Act
            var results = await controller.GetBorrowersEmploymentHistory(new LoanApplicationIdModel() { LoanApplicationId = 1 });

            // Assert
            Assert.NotNull(results);
            Assert.IsType<BadRequestObjectResult>(results);
        }

        [Fact]
        public async Task TestGetBorrowersEmploymentHistoryWhenAllDataFound()
        {
            // Arrange
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IEmploymentService> employerServiceMock = new Mock<IEmploymentService>();

            var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, employerServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            int fakeErrorIdReturned = -1;
            EmploymentHistoryModel fakeModelToReturn = new EmploymentHistoryModel()
            {
                ErrorMessage = string.Empty
            };
            employerServiceMock.Setup(x =>
                    x.GetEmploymentHistory(It.IsAny<TenantModel>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(fakeModelToReturn);

            // Act
            var results = await controller.GetBorrowersEmploymentHistory(new LoanApplicationIdModel() { LoanApplicationId = 1 });

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }

        [Fact]
        public async Task TestGetMyMoneyHomeScreen()
        {
            // Arrange
            Mock<IIncomeService> incomeServiceMock = new Mock<IIncomeService>();
            Mock<IInfoService> infoServiceMock = new Mock<IInfoService>();
            Mock<IEmploymentService> employerServiceMock = new Mock<IEmploymentService>();

            var controller = new IncomeController(incomeServiceMock.Object, infoServiceMock.Object, employerServiceMock.Object);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            GetAllIncomesForHomeScreenModel fakeModelToReturn = new GetAllIncomesForHomeScreenModel();

            incomeServiceMock.Setup(x =>
                    x.GetAllIncomesForHomeScreen(It.IsAny<TenantModel>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(fakeModelToReturn);

            // Act
            var results = await controller.GetMyMoneyHomeScreen(1);

            // Assert
            Assert.NotNull(results);
            Assert.IsType<OkObjectResult>(results);
        }
    }
}
