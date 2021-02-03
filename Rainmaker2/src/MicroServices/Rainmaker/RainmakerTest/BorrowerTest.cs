using Microsoft.EntityFrameworkCore;
using Moq;
using Rainmaker.Service;
using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace Rainmaker.Test
{
    public class BorrowerTest
    {
        [Fact]
        public void TestGetLoanSummary()
        {
            //Arrange
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();


            Borrower borrower = new Borrower
            {
                Id = 599,
                LoanApplicationId = 599,
                OwnTypeId = 1,
                LoanContactId = 599
            };
            dataContext.Set<Borrower>().Add(borrower);

            LoanApplication loanApplication = new LoanApplication
            {
                Id = 599
                ,
                EncompassNumber = "123"
            };
            dataContext.Set<LoanApplication>().Add(loanApplication);

            LoanContact loanContact = new LoanContact
            {
                Id = 599,
                FirstName = "ABC",
                LastName = "XYZ",
                EmailAddress = "abc@mail.com"


            };
            dataContext.Set<LoanContact>().Add(loanContact);
            dataContext.SaveChanges();
            Mock<ICommonService> mockCommonService = new Mock<ICommonService>();
            Mock<IBorrowerService> mockBorrowerService = new Mock<IBorrowerService>();
            Mock<IUnitOfWork<RainMakerContext>> mockUnitOfWork = new Mock<IUnitOfWork<RainMakerContext>>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();


            List<Borrower> borrowers = new List<Borrower>()
            {
                new Borrower ()
                {
                     Id=599,
                     OwnTypeId=1,
                     LoanApplicationId=599

                }
            };


            mockBorrowerService.Setup(x => x.GetBorrowerWithDetails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), null)).Returns(borrowers);


            var service = new BorrowerService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object);
            //Act
            List<Borrower> result = service.GetBorrowerWithDetails("ABC", "XYZ", "abc@mail.com", 599, "123", null);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(599, result[0].Id);
            Assert.Equal(1, result[0].OwnTypeId);
            Assert.Equal(599, result[0].LoanApplicationId);
        }
        [Fact]
        public void TestGetLoanSummaryIncludes()
        {
            //Arrange
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();


            Borrower borrower = new Borrower
            {
                Id = 5990,
                LoanApplicationId = 5990,
                OwnTypeId = 1,
                LoanContactId = 5990
            };
            dataContext.Set<Borrower>().Add(borrower);

            LoanApplication loanApplication = new LoanApplication
            {
                Id = 5990
                ,
                EncompassNumber = "123"
            };
            dataContext.Set<LoanApplication>().Add(loanApplication);

            LoanContact loanContact = new LoanContact
            {
                Id = 5990,
                FirstName = "ABC",
                LastName = "XYZ",
                EmailAddress = "abc@mail.com"


            };
            dataContext.Set<LoanContact>().Add(loanContact);
            dataContext.SaveChanges();
            Mock<ICommonService> mockCommonService = new Mock<ICommonService>();
            Mock<IBorrowerService> mockBorrowerService = new Mock<IBorrowerService>();
            Mock<IUnitOfWork<RainMakerContext>> mockUnitOfWork = new Mock<IUnitOfWork<RainMakerContext>>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();


            List<Borrower> borrowers = new List<Borrower>()
            {
                new Borrower ()
                {
                     Id=5990,
                     OwnTypeId=1,
                     LoanApplicationId=5990

                }
            };


            mockBorrowerService.Setup(x => x.GetBorrowerWithDetails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), null)).Returns(borrowers);


            var service = new BorrowerService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object);
            //Act
            List<Borrower> result = service.GetBorrowerWithDetails("ABC", "XYZ", "abc@mail.com", 5990, "123", 
                BorrowerService.RelatedEntities.LoanContact |
                BorrowerService.RelatedEntities.LoanContact_Ethnicity |
                BorrowerService.RelatedEntities.LoanApplication |
                BorrowerService.RelatedEntities.LoanContact_Race |
                BorrowerService.RelatedEntities.BorrowerQuestionResponses |
                BorrowerService.RelatedEntities.BorrowerQuestionResponses_QuestionResponse
                );
            //Assert
            Assert.NotNull(result);
        }
    }
}
