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
    public class MappingServiceTest
    {
        [Fact]
        public void TestGetMappingWithDetailsService()
        {
            //Arrange
            DbContextOptions<LosIntegrationContext> options;
            var builder = new DbContextOptionsBuilder<LosIntegrationContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using LosIntegrationContext dataContext = new LosIntegrationContext(options);

            dataContext.Database.EnsureCreated();

            _Mapping _mapping = new _Mapping
            {
                Id = 1,ExtOriginatorId =  1,ExtOriginatorEntityId="1",ExtOriginatorEntityName="a",RMEnittyId="1", RMEntityName="a"
            };
            dataContext.Set<_Mapping>().Add(_mapping);

            dataContext.SaveChanges();

            IMappingService mappingService = new MappingService(new UnitOfWork<LosIntegrationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            List<_Mapping> res = mappingService.GetMappingWithDetails("1","a","1","a",1);

            // Assert
            Assert.NotNull(res);
           
        }
        [Fact]
        public void TestGetMappingService()
        {
            //Arrange
            DbContextOptions<LosIntegrationContext> options;
            var builder = new DbContextOptionsBuilder<LosIntegrationContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using LosIntegrationContext dataContext = new LosIntegrationContext(options);

            dataContext.Database.EnsureCreated();

            _Mapping _mapping = new _Mapping
            {
                Id = 2,
                ExtOriginatorId = 1,
                ExtOriginatorEntityId = "1",
                ExtOriginatorEntityName = "a",
                RMEnittyId = "1",
                RMEntityName = "a"
            };
            dataContext.Set<_Mapping>().Add(_mapping);

            dataContext.SaveChanges();

            IMappingService mappingService = new MappingService(new UnitOfWork<LosIntegrationContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            //Act
            List<_Mapping> res = mappingService.GetMapping(new List<string>() { "1"},"a");

            // Assert
            Assert.NotNull(res);
           
        }
    }
}
