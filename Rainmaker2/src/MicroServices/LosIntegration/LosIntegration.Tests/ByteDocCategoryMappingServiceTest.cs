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
    public class ByteDocCategoryMappingServiceTest
    {
        [Fact]
        public void TestGetByteDocCategoryMappingWithDetailsService()
        {
            //Arrange
            DbContextOptions<LosIntegrationContext> options;
            var builder = new DbContextOptionsBuilder<LosIntegrationContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using LosIntegrationContext dataContext = new LosIntegrationContext(options);

            dataContext.Database.EnsureCreated();

            ByteDocCategoryMapping byteDocCategoryMapping = new ByteDocCategoryMapping
            {
                Id = 123
            };
            dataContext.Set<ByteDocCategoryMapping>().Add(byteDocCategoryMapping);

            dataContext.SaveChanges();

            IByteDocCategoryMappingService btByteDocCategoryMappingService = new ByteDocCategoryMappingService(new UnitOfWork<LosIntegrationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            List<ByteDocCategoryMapping> res = btByteDocCategoryMappingService.GetByteDocCategoryMappingWithDetails(123);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(123, res[0].Id);
        }
    }
}
