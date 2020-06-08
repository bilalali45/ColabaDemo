using System;
using Xunit;
using Rainmaker.Service;
using Moq;
using System.Collections.Generic;
using Rainmaker.Model;
using RainMaker.Entity.Models;
using Rainmaker.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RainMaker.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RainmakerTest
{
    public class LoanApplicationTest
    {
        [Fact]

        public async Task TestGetLoanSummaryController()
        {
            Mock<ILoanApplicationService> mock = new Mock<ILoanApplicationService>();
            LoanSummary obj = new LoanSummary() { CityName = "Karachi" };

            mock.Setup(x => x.GetLoanSummary(It.IsAny<int>())).ReturnsAsync(obj);

            LoanApplicationController controller = new LoanApplicationController(mock.Object);
            ////Act
            IActionResult result = await controller.GetLoanInfo(1);
            ////Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var content = (result as OkObjectResult).Value as LoanSummary;
            Assert.NotNull(content);
            Assert.Equal("Karachi", content.CityName);
        }

 
        //[Fact]
        //public async Task TestGetLoanSummaryService(int loanApplicationId)
        //{   //Arrange

        //    DbContextOptions<RainMakerContext> options;
        //    var builder = new DbContextOptionsBuilder<RainMakerContext>();
        //    builder.UseInMemoryDatabase("RainMaker");
        //    options = builder.Options;
        //    RainMakerContext dataContext = new RainMakerContext(options);
           
        //    dataContext.Database.lo();
        //    dataContext..Set<LoanSummary>() 
        //}

    }
}
