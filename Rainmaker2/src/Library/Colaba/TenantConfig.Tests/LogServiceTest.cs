using ColabaLog.Data;
using ColabaLog.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using TenantConfig.Service;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace TenantConfig.Tests
{
    public class LogServiceTest
    {
        [Fact]
        public async Task InsertLogContactEmailServiceTest()
        {
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            Mock<IUnitOfWork<ColabaLogContext>> mockColabaLogContext = new Mock<IUnitOfWork<ColabaLogContext>>();
            Mock<ILogService> mockLogService = new Mock<ILogService>();

            //Arrange
            DbContextOptions<ColabaLogContext> options;
            var builder = new DbContextOptionsBuilder<ColabaLogContext>();
            builder.UseInMemoryDatabase("ColabaLogContext");
            options = builder.Options;
            using ColabaLogContext ConfigContext = new ColabaLogContext(options);
            ConfigContext.Database.EnsureCreated();
            ContactLog contactLog = new ContactLog
            {
                FirstName = "abc",
                LastName = "bcd",
                Email = "abc@mail.com",
                CreatedOn = DateTime.UtcNow,
                TenantId = 1

            };
            ConfigContext.Set<ContactLog>().Add(contactLog);
            ConfigContext.SaveChanges();

            mockLogService.Setup(x => x.InsertLogContactEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()));
            var service = new LogService(new UnitOfWork<ColabaLogContext>(ConfigContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object);
            //Act
            await service.InsertLogContactEmail("abc", "bcd", "abc@mail.com", 1);


        }
        [Fact]
        public async Task InsertLogContactEmailPhoneServiceTest()
        {
            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            Mock<IUnitOfWork<ColabaLogContext>> mockColabaLogContext = new Mock<IUnitOfWork<ColabaLogContext>>();
            Mock<ILogService> mockLogService = new Mock<ILogService>();

            //Arrange
            DbContextOptions<ColabaLogContext> options;
            var builder = new DbContextOptionsBuilder<ColabaLogContext>();
            builder.UseInMemoryDatabase("ColabaLogContext");
            options = builder.Options;
            using ColabaLogContext ConfigContext = new ColabaLogContext(options);
            ConfigContext.Database.EnsureCreated();
            ContactLog contactLog = new ContactLog
            {
                FirstName = "abc",
                LastName = "bcd",
                Email = "abc@mail.com",
                CreatedOn = DateTime.UtcNow,
                TenantId = 1,
                PhoneNumber="0000000000"

            };
            ConfigContext.Set<ContactLog>().Add(contactLog);
            ConfigContext.SaveChanges();

            mockLogService.Setup(x => x.InsertLogContactEmailPhone(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()));
            var service = new LogService(new UnitOfWork<ColabaLogContext>(ConfigContext, new RepositoryProvider(new RepositoryFactories())), mockServiceProvider.Object);
            //Act
            await service.InsertLogContactEmailPhone("abc", "bcd", "abc@mail.com", "0000000000" ,1);


        }
    }
}
