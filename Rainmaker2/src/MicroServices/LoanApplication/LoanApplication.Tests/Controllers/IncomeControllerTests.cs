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
    public class IncomeControllerTests
    {
        [Fact]
        public async Task GetMilitaryIncome_ShouldReturnOkObjectResult()
        {
            //Arrange
            var incomeService = new Mock<IIncomeService>();
            var controller = new IncomeController(incomeService.Object, null,null);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.GetMilitaryIncome(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddOrUpdateMilitaryIncome_ShouldReturnOkObjectResult()
        {
            //Arrange
            var incomeService = new Mock<IIncomeService>();
            var controller = new IncomeController(incomeService.Object, null,null);
            controller.MockHttpContext(tenantId: 1, userId: 1);

            //Act
            var result = await controller.AddOrUpdateMilitaryIncome(new MilitaryIncomeModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}