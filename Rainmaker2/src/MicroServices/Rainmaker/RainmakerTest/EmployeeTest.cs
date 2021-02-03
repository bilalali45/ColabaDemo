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
    public class EmployeeTest
    {
        [Fact]
        public async Task TestGetEmployeeEmailByRoleName()
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
                Id = 251,
                IsActive = true,
                IsDeleted = false,
                BusinessUnitId = 251,
                UserName = "ABC"
            };
            dataContext.Set<UserProfile>().Add(userProfile);

            UserInRole userInRole = new UserInRole
            {
                RoleId = 251,
                UserId = 251

            };
            dataContext.Set<UserInRole>().Add(userInRole);
            UserRole userRole = new UserRole
            {
                Id = 251,
                IsDeleted = true,
                RoleName = "ADMIN",
                IsActive = true

            };
            dataContext.Set<UserRole>().Add(userRole);
            Employee employee = new Employee
            {
                Id = 251,
                IsActive = true,
                IsDeleted = false,
                UserId = 251,

            };
            dataContext.Set<Employee>().Add(employee);
            EmployeeBusinessUnitEmail employeeBusinessUnitEmail = new EmployeeBusinessUnitEmail
            {
                Id = 251,
                BusinessUnitId = 251,
                EmployeeId = 251,
                EmailAccountId = 251
            };
            dataContext.Set<Employee>().Add(employee);

            EmailAccount emailAccount = new EmailAccount
            {
                Id= 251,
                Email="ABC"
               
                
            };
            dataContext.Set<EmailAccount>().Add(emailAccount);
            dataContext.SaveChanges();

            Mock<IEmployeeService> mockEmployeeService = new Mock<IEmployeeService>();
            Mock<IUnitOfWork<RainMakerContext>> mockUnitOfWork = new Mock<IUnitOfWork<RainMakerContext>>();
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            List<Employee> employees = new List<Employee>
            {
                new Employee
                {
                    Id=1,
                    IsDeleted=false,
                    IsActive=true,
                    EmailAccountId=251

                }
            };
         
            mockEmployeeService.Setup(x => x.GetEmployeeEmailByRoleName(It.IsAny<string>())).ReturnsAsync(employees);


            var service = new EmployeeService(new UnitOfWork<RainMakerContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object);
            //Act
            List<Employee> result = await service.GetEmployeeEmailByRoleName("admin");
            //Assert
            Assert.NotNull(result);
            Assert.Equal(251, result[0].UserId);
            Assert.True( result[0].IsActive);
            Assert.False(result[0].IsDeleted);



        }
    }
}
