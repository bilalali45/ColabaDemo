using Microsoft.EntityFrameworkCore;
using Moq;
using Rainmaker.Service;
using RainMaker.Common;
using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using System.Collections.Generic;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace Rainmaker.Test
{
    public class MembershipTest
    {
        [Fact]
        public void TestGetLoanSummaryFalse()
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

            dataContext.SaveChanges();

            Mock<IMembershipService> mockMembershipService = new Mock<IMembershipService>();
            Mock<IUnitOfWork<RainMakerContext>> mockUnitOfWork = new Mock<IUnitOfWork<RainMakerContext>>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();


            List<Borrower> borrowers = new List<Borrower>()
            {
                new Borrower ()
                {
                     Id=6650,
                     OwnTypeId=1,
                     LoanApplicationId=6650

                }
            };

            mockMembershipService.Setup(x => x.GetUser(It.IsAny<string>())).Returns(userProfile);


            var service = new MembershipService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object);
            //Act
            UserProfile result = service.ValidateUser("ABCD", "XYZ", false);
            //Assert
            Assert.NotNull(result);
            Assert.Equal("XYZ", result.Password);
         
        }

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

            EmployeePhoneBinder employeePhoneBinders = new EmployeePhoneBinder
            {
                Id = 666,
                EmployeeId = 666
            };
            dataContext.SaveChanges();

            Mock<IMembershipService> mockMembershipService = new Mock<IMembershipService>();
            Mock<IUnitOfWork<RainMakerContext>> mockUnitOfWork = new Mock<IUnitOfWork<RainMakerContext>>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();


            List<Borrower> borrowers = new List<Borrower>()
            {
                new Borrower ()
                {
                     Id=666,
                     OwnTypeId=1,
                     LoanApplicationId=666

                }
            };

            mockMembershipService.Setup(x => x.GetEmployeeUser(It.IsAny<string>())).Returns(userProfile);


            var service = new MembershipService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object);
            //Act
            UserProfile result = service.ValidateUser("Shery", "XYZ", true);
            //Assert
            Assert.NotNull(result);
            Assert.Equal("XYZ", result.Password);
  
        }

        [Fact]
        public void TestGetLoanSummaryMultipleUserFoundException()
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
                Id = 866,
                UserName = "abc",
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
            dataContext.SaveChanges();

            Mock<IMembershipService> mockMembershipService = new Mock<IMembershipService>();
            Mock<IUnitOfWork<RainMakerContext>> mockUnitOfWork = new Mock<IUnitOfWork<RainMakerContext>>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();


            List<Borrower> borrowers = new List<Borrower>()
            {
                new Borrower ()
                {
                     Id=866,
                     OwnTypeId=1,
                     LoanApplicationId=866

                }
            };

            mockMembershipService.Setup(x => x.GetEmployeeUser(It.IsAny<string>())).Returns(userProfile);


            var service = new MembershipService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object);
            //Assert
            Assert.Throws<RainMakerException>(() => service.ValidateUser("ABC", "XYZ", true));

        }
        //[Fact]
        //public void TestGetLoanSummaryException()
        //{
        //    //Arrange
        //    DbContextOptions<RainMakerContext> options;
        //    var builder = new DbContextOptionsBuilder<RainMakerContext>();
        //    builder.UseInMemoryDatabase("RainMaker");
        //    options = builder.Options;
        //    using RainMakerContext dataContext = new RainMakerContext(options);

        //    dataContext.Database.EnsureCreated();
        //    UserProfile userProfile = new UserProfile
        //    {
        //        Id = 666,
        //        UserName = "abc",
        //        IsActive = true,
        //        IsDeleted = false,
        //        Password = "XYZ"
        //    };
        //    dataContext.Set<UserProfile>().Add(userProfile);

        //    Customer customer = new Customer
        //    {
        //        Id = 666,
        //        IsActive = true,
        //        UserId = 666
        //    };
        //    dataContext.Set<Customer>().Add(customer);
        //    Customer customer1 = new Customer
        //    {
        //        Id = 667,
        //        IsActive = true,
        //        UserId = 666
        //    };
        //    dataContext.Set<Customer>().Add(customer1);
        //    Employee employee = new Employee
        //    {
        //        Id = 666,
        //        ContactId = 666
        //       ,
        //        IsActive = true
        //    };
        //    dataContext.Set<Employee>().Add(employee);
        //    Contact contact = new Contact
        //    {
        //        Id = 666,
        //        FirstName = "abc",
        //        LastName = "xyz"

        //    };
        //    dataContext.Set<Contact>().Add(contact);

        //    EmployeePhoneBinder employeePhoneBinders = new EmployeePhoneBinder
        //    {
        //        Id = 666,
        //        EmployeeId = 666
        //    };
        //    dataContext.SaveChanges();

        //    Mock<IMembershipService> mockMembershipService = new Mock<IMembershipService>();
        //    Mock<IUnitOfWork<RainMakerContext>> mockUnitOfWork = new Mock<IUnitOfWork<RainMakerContext>>();
        //    Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();


        //    List<Borrower> borrowers = new List<Borrower>()
        //    {
        //        new Borrower ()
        //        {
        //             Id=666,
        //             OwnTypeId=1,
        //             LoanApplicationId=666

        //        },
        //         new Borrower ()
        //        {
        //             Id=667,
        //             OwnTypeId=1,
        //             LoanApplicationId=666

        //        }
        //    };

        //    mockMembershipService.Setup(x => x.GetEmployeeUser(It.IsAny<string>())).Returns(userProfile);


        //    var service = new MembershipService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object);
        //    //Act
        //    UserProfile result = service.ValidateUser("ABC1", "XYZ1", true);
        //    //Assert
        //    Assert.NotNull(result);
        //    Assert.Equal("XYZ", result.Password);
        //    //Assert.Equal(1, result[0].OwnTypeId);
        //    //Assert.Equal(899, result[0].LoanApplicationId);
        //}
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

            Mock<IMembershipService> mockMembershipService = new Mock<IMembershipService>();
            Mock<IUnitOfWork<RainMakerContext>> mockUnitOfWork = new Mock<IUnitOfWork<RainMakerContext>>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();


            List<Borrower> borrowers = new List<Borrower>()
            {
                new Borrower ()
                {
                     Id=665,
                     OwnTypeId=1,
                     LoanApplicationId=665

                }
            };
            var service = new MembershipService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object);
            //Assert

            Assert.Throws<ArgumentException>(() => service.ValidateUser(null, "sada", false));
 
        }

        [Fact]
        public void TestGetLoanSummaryPasswordNullException()
        {
            //Arrange

            DbContextOptions<RainMakerContext> options;
            var builder = new DbContextOptionsBuilder<RainMakerContext>();
            builder.UseInMemoryDatabase("RainMaker");
            options = builder.Options;
            Mock<IMembershipService> mockMembershipService = new Mock<IMembershipService>();
            Mock<IUnitOfWork<RainMakerContext>> mockUnitOfWork = new Mock<IUnitOfWork<RainMakerContext>>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();

            using RainMakerContext dataContext = new RainMakerContext(options);

            dataContext.Database.EnsureCreated();
            List<Borrower> borrowers = new List<Borrower>()
            {
                new Borrower ()
                {
                     Id=665,
                     OwnTypeId=1,
                     LoanApplicationId=665

                }
            };
            var service = new MembershipService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object);


            //Assert

            Assert.Throws<ArgumentException>(() => service.ValidateUser("sada", null, false));

   
        }



        [Fact]
        public void TestGetLoanSummaryRequiredUserNull()
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

            EmployeePhoneBinder employeePhoneBinders = new EmployeePhoneBinder
            {
                Id = 516,
                EmployeeId = 516
            };
            dataContext.SaveChanges();

            Mock<IMembershipService> mockMembershipService = new Mock<IMembershipService>();
            Mock<IUnitOfWork<RainMakerContext>> mockUnitOfWork = new Mock<IUnitOfWork<RainMakerContext>>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();


            List<Borrower> borrowers = new List<Borrower>()
            {
                new Borrower ()
                {
                     Id=516,
                     OwnTypeId=1,
                     LoanApplicationId=516

                }
            };

            mockMembershipService.Setup(x => x.GetEmployeeUser(It.IsAny<string>())).Returns(userProfile);


            var service = new MembershipService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object);
            //Act
            UserProfile result = service.ValidateUser("ABC", "XYZ1", false);
            //Assert
            Assert.Null(result);
          

        }
    }
}
