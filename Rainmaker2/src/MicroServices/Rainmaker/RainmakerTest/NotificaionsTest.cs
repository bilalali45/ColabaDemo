using Microsoft.EntityFrameworkCore;
using Moq;
using Rainmaker.API.Controllers;
using RainMaker.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;
using RainMaker.Entity.Models;
using Rainmaker.Service;
using RainMaker.Service;
using Rainmaker.Model;

namespace Rainmaker.Test
{
    public class NotificaionsTest
    {

        [Fact]
        public async Task TestGetAssignedUsers()
        {
            //Arrange
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            LoanApplication app = new LoanApplication()
            {
                Id = 99,
                LoanAmount = 1000,
                LoanPurposeId = 99,
                EntityTypeId = 99,
                SubjectPropertyDetailId = 1,
                OpportunityId = 99
            };
            dataContext.Set<LoanApplication>().Add(app);

            Opportunity opportunity = new Opportunity
            {
                Id = 99,
                IsActive = true,
                EntityTypeId = 99,
                IsDeleted = false,
                NoRuleMatched = false,
                IsAutoAssigned = true,
                IsPickedByOwner = true,
                IsDuplicate = false,
                BusinessUnitId = 99,
                OwnerId = 99
            };
            dataContext.Set<Opportunity>().Add(opportunity);

            OpportunityLeadBinder opportunityLeadBinder = new OpportunityLeadBinder
            {
                Id = 99,
                OpportunityId = 99,
                CustomerId = 99,
                OwnTypeId = 99
            };
            dataContext.Set<OpportunityLeadBinder>().Add(opportunityLeadBinder);

            Customer customer = new Customer()
            {
                Id = 99,
                UserId = 99,
                EntityTypeId = 99,
                DisplayOrder = 1,
                IsActive = true,
                IsSystem = true,
                IsDeleted = false
            };
            dataContext.Set<Customer>().Add(customer);

            Employee owner = new Employee
            {
                Id = 99,
                ContactId = 99,
                IsActive = true,
                UserId = 1

            };

            dataContext.Set<Employee>().Add(owner);

            dataContext.SaveChanges();
            Mock<INotificationService> mockNotificationService = new Mock<INotificationService>();

            Mock<IUnitOfWork<RainMakerContext>> mockUnitOfWork = new Mock<IUnitOfWork<RainMakerContext>>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();

            mockNotificationService.Setup(x => x.GetAssignedUsers(It.IsAny<int>())).ReturnsAsync(new List<int>());

            var notificationController = new NotificationController(mockNotificationService.Object);
            var service = new NotificationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object);
            //Act
            List<int> result = await service.GetAssignedUsers(99);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, result[0]);
        }
        [Fact]
        public async Task TestGetLoanSummary()
        {
            //Arrange
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();

            LoanApplication app = new LoanApplication()
            {
                Id = 98,
                LoanAmount = 1000,
                LoanPurposeId = 98,
                EntityTypeId = 98,
                SubjectPropertyDetailId = 98,
                OpportunityId = 98,
                StatusId = 98

            };
            dataContext.Set<LoanApplication>().Add(app);

            AddressInfo addressInfo = new AddressInfo
            {
                Id = 98,
                EntityTypeId = 98,
                StateId = 1
            };
            dataContext.Set<AddressInfo>().Add(addressInfo);

            PropertyType propertyType = new PropertyType
            {
                Id = 98,
                IsActive = true,
                EntityTypeId = 98

            };
            dataContext.Set<PropertyType>().Add(propertyType);

            PropertyInfo propertyInfo = new PropertyInfo
            {
                Id = 98,
                AddressInfoId = 98,
                PropertyTypeId = 98
            };
            dataContext.Set<PropertyInfo>().Add(propertyInfo);

            State state = new State
            {
                Id = 98,
                Name = ""
            };
            dataContext.Set<State>().Add(state);
            LoanPurpose loanPurpose = new LoanPurpose
            {
                Id = 98,
                Name = ""

            };
            dataContext.Set<LoanPurpose>().Add(loanPurpose);

            StatusList statusList = new StatusList
            {
                Id = 98,
                IsDeleted = true
            };

            Opportunity opportunity = new Opportunity
            {
                Id = 98,
                IsActive = true,
                EntityTypeId = 98,
                IsDeleted = false,
                NoRuleMatched = false,
                IsAutoAssigned = true,
                IsPickedByOwner = true,
                IsDuplicate = false,
                BusinessUnitId = 98,
                OwnerId = 98
            };
            dataContext.Set<Opportunity>().Add(opportunity);

            OpportunityLeadBinder opportunityLeadBinder = new OpportunityLeadBinder
            {
                Id = 2,
                OpportunityId = 2,
                CustomerId = 2,
                OwnTypeId = 1
            };
            dataContext.Set<OpportunityLeadBinder>().Add(opportunityLeadBinder);

            Customer customer = new Customer()
            {
                Id = 98,
                UserId = 98,
                EntityTypeId = 98,
                DisplayOrder = 98,
                IsActive = true,
                IsSystem = true,
                IsDeleted = false
            };
            dataContext.Set<Customer>().Add(customer);
            Borrower borrower = new Borrower
            {
                Id = 98,
                LoanApplicationId = 98
            };
            dataContext.Set<Borrower>().Add(borrower);
            LoanContact loanContact = new LoanContact
            {
                Id = 98,
                ContactId = 98

            };
            dataContext.Set<LoanContact>().Add(loanContact);

              
             dataContext.SaveChanges();
            Mock<ICommonService> mockCommonService = new Mock<ICommonService>();
            Mock<INotificationService> mockNotificationService = new Mock<INotificationService>();

            Mock<IUnitOfWork<RainMakerContext>> mockUnitOfWork = new Mock<IUnitOfWork<RainMakerContext>>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            LoanSummary loanSummary = new LoanSummary
            {
                Url = ""

            };

            mockServiceProvider
                .Setup(x => x.GetService(typeof(ICommonService)))
                .Returns(mockCommonService.Object);
            mockNotificationService.Setup(x => x.GetLoanSummary(It.IsAny<int>())).ReturnsAsync(loanSummary);


            var service = new NotificationService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object);
            //Act
            LoanSummary result = await service.GetLoanSummary(98);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(1000,result.LoanAmount);

        }
    }
}
