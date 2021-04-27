using Microsoft.EntityFrameworkCore;
using Moq;
using Rainmaker.Service;
using RainMaker.Common;
using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using System.Collections.Generic;
using Castle.Core.Logging;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Rainmaker.Test
{
    public class MembershipTest
    {
        [Fact]
        public async Task TestGetLoanSummaryFalse()
        {
            //Arrange
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();
            UserProfile userProfile = new UserProfile
            {
                Id = 6650,
                UserName = "abcd",
                IsActive = true,
                IsDeleted = false,
                Password = "XYZ"
            };
            dataContext.Set<UserProfile>().Add(userProfile);

            Customer customer = new Customer
            {
                Id = 6650,
                IsActive = true,
                UserId = 6650
            };
            dataContext.Set<Customer>().Add(customer);
            PasswordPolicy passwordPolicy = new PasswordPolicy
            {
                Id = 665,
                TenantId = 2,
                IncorrectPasswordCount = 1
            };
            dataContext.Set<PasswordPolicy>().Add(passwordPolicy);
            dataContext.SaveChanges();

            Mock<IMembershipService> mockMembershipService = new Mock<IMembershipService>();
           
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();

            mockMembershipService.Setup(x => x.GetUser(It.IsAny<string>())).Returns(userProfile);


            var service = new MembershipService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object, Mock.Of<ILogger<MembershipService>>());
            //Act
            UserProfile result = await service.ValidateUser(2, "ABCD", "XYZ", false);
            //Assert
            Assert.NotNull(result);
            Assert.Equal("XYZ", result.Password);

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
            UserProfile userProfile = new UserProfile
            {
                Id = 666,
                UserName = "Shery",
                IsActive = true,
                IsDeleted = false,
                Password = "XYZ"
            };
            dataContext.Set<UserProfile>().Add(userProfile);

            Customer customer = new Customer
            {
                Id = 666,
                IsActive = true,
                UserId = 666
            };
            dataContext.Set<Customer>().Add(customer);
            Employee employee = new Employee
            {
                Id = 666,
                ContactId = 666
               ,
                IsActive = true
            };
            dataContext.Set<Employee>().Add(employee);
            Contact contact = new Contact
            {
                Id = 666,
                FirstName = "abc",
                LastName = "xyz"

            };
            dataContext.Set<Contact>().Add(contact);

            
            PasswordPolicy passwordPolicy = new PasswordPolicy
            {
                Id = 666,
                TenantId = 1,
                IncorrectPasswordCount = 1
            };
            dataContext.Set<PasswordPolicy>().Add(passwordPolicy);
            dataContext.SaveChanges();

            Mock<IMembershipService> mockMembershipService = new Mock<IMembershipService>();
           
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();


            mockMembershipService.Setup(x => x.GetEmployeeUser(It.IsAny<string>())).Returns(userProfile);


            var service = new MembershipService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object, Mock.Of<ILogger<MembershipService>>());
            //Act
            UserProfile result = await service.ValidateUser(1, "Shery", "XYZ", true);
            //Assert
            Assert.NotNull(result);
            Assert.Equal("XYZ", result.Password);

        }

        [Fact]
        public async Task TestGetLoanSummaryMultipleUserFoundException()
        {
            //Arrange
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();
            // user 1
            UserProfile userProfile = new UserProfile
            {
                Id = 866,
                UserName = "abcde",
                IsActive = true,
                IsDeleted = false,
                Password = "XYZ1"
            };
            dataContext.Set<UserProfile>().Add(userProfile);

            Customer customer = new Customer
            {
                Id = 866,
                IsActive = true,
                UserId = 866
            };
            dataContext.Set<Customer>().Add(customer);
            Employee employee = new Employee
            {
                Id = 866,
                ContactId = 866
               ,
                IsActive = true
            };
            dataContext.Set<Employee>().Add(employee);
            Contact contact = new Contact
            {
                Id = 866,
                FirstName = "abc",
                LastName = "xyz"

            };
            dataContext.Set<Contact>().Add(contact);

            EmployeePhoneBinder employeePhoneBinders = new EmployeePhoneBinder
            {
                Id = 866,
                EmployeeId = 866
            };

            PasswordPolicy passwordPolicy = new PasswordPolicy
            {
                TenantId = 1,
                IncorrectPasswordCount=1
            };
            dataContext.Set<PasswordPolicy>().Add(passwordPolicy);
            dataContext.Set<EmployeePhoneBinder>().Add(employeePhoneBinders);

            // user2
            userProfile = new UserProfile
            {
                Id = 8661,
                UserName = "abcde",
                IsActive = true,
                IsDeleted = false,
                Password = "XYZ1"
            };
            dataContext.Set<UserProfile>().Add(userProfile);

            customer = new Customer
            {
                Id = 8661,
                IsActive = true,
                UserId = 8661
            };
            dataContext.Set<Customer>().Add(customer);
            employee = new Employee
            {
                Id = 8661,
                ContactId = 8661
               ,
                IsActive = true
            };
            dataContext.Set<Employee>().Add(employee);
            contact = new Contact
            {
                Id = 8661,
                FirstName = "abc",
                LastName = "xyz"

            };
            dataContext.Set<Contact>().Add(contact);

            employeePhoneBinders = new EmployeePhoneBinder
            {
                Id = 8661,
                EmployeeId = 8661
            };
            dataContext.Set<EmployeePhoneBinder>().Add(employeePhoneBinders);
            dataContext.SaveChanges();

            Mock<IMembershipService> mockMembershipService = new Mock<IMembershipService>();
           
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();


            mockMembershipService.Setup(x => x.GetEmployeeUser(It.IsAny<string>())).Returns(userProfile);


            var service = new MembershipService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object, Mock.Of<ILogger<MembershipService>>());
            //Assert
            await Assert.ThrowsAsync<RainMakerException>(() => service.ValidateUser(1,"ABCDE", "XYZ1", true));

        }
       
        
        [Fact]
        public void TestGetLoanSummaryUserNameNullExcepton()
        {
            //Arrange
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);
            dataContext.Database.EnsureCreated();

            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();


            var service = new MembershipService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object, Mock.Of<ILogger<MembershipService>>());
            //Assert

            Assert.ThrowsAsync<ArgumentException>(() => service.ValidateUser(1, null, "sada", false));

        }

        [Fact]
        public void TestGetLoanSummaryPasswordNullException()
        {
            //Arrange

            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            
           
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();

            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();
            
            
            var service = new MembershipService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object, Mock.Of<ILogger<MembershipService>>());


            //Assert

            Assert.ThrowsAsync<ArgumentException>(() => service.ValidateUser(1, "sada", null, false));


        }


        [Fact]
        public async Task TestGetLoanSummaryRequiredUserNull()
        {
            //Arrange
            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();
            UserProfile userProfile = new UserProfile
            {
                Id = 516,
                UserName = "abc",
                IsActive = true,
                IsDeleted = false,
                Password = "XYZ"
            };
            dataContext.Set<UserProfile>().Add(userProfile);

            Customer customer = new Customer
            {
                Id = 516,
                IsActive = true,
                UserId = 516
            };
            dataContext.Set<Customer>().Add(customer);
            Employee employee = new Employee
            {
                Id = 516,
                ContactId = 516
               ,
                IsActive = true
            };
            dataContext.Set<Employee>().Add(employee);
            Contact contact = new Contact
            {
                Id = 516,
                FirstName = "abc",
                LastName = "xyz"

            };
            dataContext.Set<Contact>().Add(contact);

            
            dataContext.SaveChanges();

            Mock<IMembershipService> mockMembershipService = new Mock<IMembershipService>();
          
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();


            mockMembershipService.Setup(x => x.GetEmployeeUser(It.IsAny<string>())).Returns(userProfile);


            var service = new MembershipService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object, Mock.Of<ILogger<MembershipService>>());
            //Act
            UserProfile result = await service.ValidateUser(1, "ABCXYZ", "XYZ1", false);
            //Assert
            Assert.Null(result);


        }
    }
}
