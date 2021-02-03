using Microsoft.EntityFrameworkCore;
using Moq;
using Rainmaker.Service;
using RainMaker.Data;
using RainMaker.Entity.Models;
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
    public class OpportunityTest
    {
        [Fact]
        public async Task TestGetSingleOpportunity()
        {
            //Arrange
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();
            Opportunity opportunity = new Opportunity
            {

                Id = 789,
                IsActive = true,
                BusinessUnitId = 789,
                StatusId = 1,
                OwnerId = 1,
                IsDeleted = false
            };
            dataContext.Set<Opportunity>().Add(opportunity);
           
            Employee employee = new Employee
            {
                Id = 789,
                IsDeleted = true,
                ContactId = 789

            }; 
            dataContext.Set<Employee>().Add(employee);
           
            EmployeeBusinessUnitEmail employeeBusinessUnitEmail = new EmployeeBusinessUnitEmail
            {
                Id = 789,
                EmployeeId = 789,
                EmailAccountId = 789
            }; 
            dataContext.Set<EmployeeBusinessUnitEmail>().Add(employeeBusinessUnitEmail);

            EmailAccount emailAccount = new EmailAccount
            {
                Id = 789,
                IsActive = true,
                IsDeleted = true
            };
            dataContext.Set<EmailAccount>().Add(emailAccount);
            Contact contact = new Contact
            {
                Id = 789,
                IsDeleted = false,
                FirstName = "Abc",
                LastName = "xyz" ,
                EntityTypeId = 1

            };
            dataContext.Set<Contact>().Add(contact);
           
            ContactEmailInfo contactEmailInfo = new ContactEmailInfo
            {
                Id = 789,
                IsDeleted = false,
                ContactId = 789,
                Email = "sc@mail.com"
            }; 
            dataContext.Set<ContactEmailInfo>().Add(contactEmailInfo);
           
            OpportunityLeadBinder opportunityLeadBinder = new OpportunityLeadBinder
            {
                Id = 789,
                CustomerId = 789,
                OpportunityId = 789
            };
            dataContext.Set<OpportunityLeadBinder>().Add(opportunityLeadBinder);
           
            Customer customer = new Customer
            {
                Id = 789,
                ContactId = 789,
                IsActive = true,
                IsDeleted = false,
                BusinessUnitId = 789
            };
            dataContext.Set<Customer>().Add(customer);


            dataContext.SaveChanges();

            Mock<IOpportunityService> mockOpportunityService = new Mock<IOpportunityService>();
            Mock<IUnitOfWork<RainMakerContext>> mockUnitOfWork = new Mock<IUnitOfWork<RainMakerContext>>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();




            mockOpportunityService.Setup(x => x.GetSingleOpportunity(It.IsAny<int>())).ReturnsAsync(opportunity);


            var service = new OpportunityService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object);
            //Act
            Opportunity result = await service.GetSingleOpportunity(789);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(789, result.BusinessUnitId);
            Assert.Equal(1, result.StatusId);
            Assert.Equal(1, result.OwnerId);


        }
        [Fact]
        public  void TestInsertOpportunityStatusLog()
        {
            //Arrange
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();
            Opportunity opportunity = new Opportunity
            {

                Id = 780,
                IsActive = true,
                BusinessUnitId = 780,
                StatusId = 1,
                OwnerId = 1,
                IsDeleted = false
            };
            Mock<IOpportunityService> mockOpportunityService = new Mock<IOpportunityService>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();

            OpportunityStatusLog opportunityStatusLog = new OpportunityStatusLog
            { 
                Id= 780,
                StatusId=1,
                IsActive=true,
                OpportunityId=780
            };


            mockOpportunityService.Setup(x => x.InsertOpportunityStatusLog(It.IsAny<OpportunityStatusLog>()));


            var service = new OpportunityService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object);
            //Act
            service.InsertOpportunityStatusLog(opportunityStatusLog);
        }
    }
}
