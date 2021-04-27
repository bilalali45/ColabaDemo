using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ByteWebConnector.Data;
using ByteWebConnector.Entity.Models;
using ByteWebConnector.Model.Models.OwnModels.Settings;
using ByteWebConnector.Service.DbServices;
using Microsoft.EntityFrameworkCore;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace ByteWebConnector.Tests
{
    public class SettingServiceTest
    {
        [Fact]
        public void TestGetSettingWithDetailsService()
        {
            //Arrange
            DbContextOptions<BwcContext> options;
            var builder = new DbContextOptionsBuilder<BwcContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using BwcContext dataContext = new BwcContext(options);

            dataContext.Database.EnsureCreated();

            Setting setting = new Setting
            {
                Id = 1,
                Tag = "BytePro"
            };
            dataContext.Set<Setting>().Add(setting);

            dataContext.SaveChanges();

            ISettingService settingService = new SettingService(new UnitOfWork<BwcContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            List<Setting> res = settingService.GetSettingWithDetails(1, "BytePro");

            // Assert
            Assert.NotNull(res);
            Assert.Equal(1, res[0].Id);
            Assert.Equal("BytePro", res[0].Tag);
        }
    }
}
