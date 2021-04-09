using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LosIntegration.Data;
using LosIntegration.Entity.Models;
using LosIntegration.Service;
using LosIntegration.Service.Interface;
using Microsoft.EntityFrameworkCore;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace LosIntegration.Tests
{
    public class ByteDocStatusMappingServiceTest
    {
        [Fact]
        public async Task TestGetByteDocStatusMappingWithDetailsService()
        {
            //Arrange
            DbContextOptions<LosIntegrationContext> options;
            var builder = new DbContextOptionsBuilder<LosIntegrationContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using LosIntegrationContext dataContext = new LosIntegrationContext(options);

            dataContext.Database.EnsureCreated();

            ByteDocStatusMapping byteDocStatusMapping = new ByteDocStatusMapping
            {
                RmDocStatusName = "Draft"
            };
            dataContext.Set<ByteDocStatusMapping>().Add(byteDocStatusMapping);

            dataContext.SaveChanges();

            IByteDocStatusMappingService byteDocStatusMappingService = new ByteDocStatusMappingService(new UnitOfWork<LosIntegrationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            List<ByteDocStatusMapping> res = byteDocStatusMappingService.GetByteDocStatusMappingWithDetails("Draft");

            // Assert
            Assert.NotNull(res);
            Assert.Equal("Draft", res[0].RmDocStatusName);
        }
    }
}
