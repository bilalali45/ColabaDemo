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
using static LosIntegration.Service.ByteDocTypeMappingService;

namespace LosIntegration.Tests
{
    public class ByteDocTypeMappingServiceTest
    {
        [Fact]
        public void TestGetByteDocTypeMappingWithDetailsService()
        {
            //Arrange
            DbContextOptions<LosIntegrationContext> options;
            var builder = new DbContextOptionsBuilder<LosIntegrationContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using LosIntegrationContext dataContext = new LosIntegrationContext(options);

            dataContext.Database.EnsureCreated();

            ByteDocTypeMapping byteDocTypeMapping = new ByteDocTypeMapping
            {
                Id = 1,
                RmDocTypeName = "Bank Statements - Two Months"
            };
            dataContext.Set<ByteDocTypeMapping>().Add(byteDocTypeMapping);

            ByteDocCategoryMapping byteDocCategoryMapping = new ByteDocCategoryMapping
            {
                Id = 1
            };
            dataContext.Set<ByteDocCategoryMapping>().Add(byteDocCategoryMapping);

            dataContext.SaveChanges();

            IByteDocTypeMappingService byteDocTypeMappingService = new ByteDocTypeMappingService(new UnitOfWork<LosIntegrationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            List<ByteDocTypeMapping> res = byteDocTypeMappingService.GetByteDocTypeMappingWithDetails(1, "Bank Statements - Two Months", RelatedEntities.ByteDocCategoryMapping);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(1, res[0].Id);
            Assert.Equal("Bank Statements - Two Months", res[0].RmDocTypeName);
        }
    }
}
