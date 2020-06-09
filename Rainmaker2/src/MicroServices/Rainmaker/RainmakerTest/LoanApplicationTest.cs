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
        //[Fact]

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


        [Fact]
        public async Task TestGetLoanSummaryService()
        {   //Arrange

            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            LoanApplication app = new LoanApplication()
            {
                Id = 1,
                LoanAmount = 1000,
                LoanPurposeId=1,
                EntityTypeId=1,
                SubjectPropertyDetailId=1
            };
            dataContext.Set<LoanApplication>().Add(app);
            PropertyInfo propertyInfo = new PropertyInfo()
            {
                Id=1,
                PropertyTypeId=1
            };
            dataContext.Set<PropertyInfo>().Add(propertyInfo);
            PropertyType propertyType = new PropertyType()
            {
                Id = 1,
                Description = "Test",
                Name = "",
                DisplayOrder = 1,
                IsDefault = true,
                IsActive = true,
                EntityTypeId = 1,
                IsDeleted = false,
                TpId = "",
                IsSystem = true
            };
            dataContext.Set<PropertyType>().Add(propertyType);
            dataContext.SaveChanges();
            var res = dataContext.Set<LoanApplication>().Where(x => x.Id == 1).Include(x=>x.PropertyInfo).ThenInclude(x=>x.PropertyType).FirstOrDefault();
            Assert.NotNull(res);
        }

    }
}
