using Microsoft.EntityFrameworkCore;
using Milestone.Data;
using Milestone.Entity.Models;
using Milestone.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace Milestone.Tests
{
    public partial class UnitTest
    {
        [Fact]
        public async Task TestServiceInsertMilestoneLog()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();

            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            await service.InsertMilestoneLog(1, 1);

            Assert.Equal(1,dataContext.Set<MilestoneLog>().Where(x=>x.LoanApplicationId==1).First().MilestoneId);
        }
        [Fact]
        public async Task TestUpdateMilestoneLogNull()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();

            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            await service.UpdateMilestoneLog(2, 1);

            Assert.Equal(1, dataContext.Set<MilestoneLog>().Where(x => x.LoanApplicationId == 2).First().MilestoneId);
        }
        [Fact]
        public async Task TestUpdateMilestoneLogExist()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();
            MilestoneLog log = new MilestoneLog()
            {
                LoanApplicationId=3,
                MilestoneId = 1
            };
            dataContext.Set<MilestoneLog>().Add(log);
            dataContext.SaveChanges();

            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            await service.UpdateMilestoneLog(3, 1);

            Assert.Equal(1, dataContext.Set<MilestoneLog>().Where(x => x.LoanApplicationId == 3).First().MilestoneId);
        }
        [Fact]
        public async Task TestUpdateMilestoneLogExistButNotEqual()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();
            MilestoneLog log = new MilestoneLog()
            {
                LoanApplicationId = 4,
                MilestoneId = 1
            };
            dataContext.Set<MilestoneLog>().Add(log);
            dataContext.SaveChanges();

            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            await service.UpdateMilestoneLog(4, 2);

            Assert.Equal(2, dataContext.Set<MilestoneLog>().Where(x => x.LoanApplicationId == 4 && x.MilestoneId==2).First().MilestoneId);
        }

        [Fact]
        public async Task TestServiceGetMilestoneForMcuDashboard()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();
            Entity.Models.Milestone milestone = new Entity.Models.Milestone()
            {
                Id=1,
                McuName="John"
            };
            dataContext.Set<Entity.Models.Milestone>().Add(milestone);
            TenantMilestone tenantMilestone = new TenantMilestone()
            {
                Id = 1,
                TenantId = 1,
                MilestoneId = 1,
                McuName = "Doe"
            };
            dataContext.Set<TenantMilestone>().Add(tenantMilestone);
            dataContext.SaveChanges();

            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            string result = await service.GetMilestoneForMcuDashboard(1,1);

            Assert.Equal("Doe", result);
        }
        [Fact]
        public async Task TestServiceGetAllMilestones()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();
            Entity.Models.Milestone milestone = new Entity.Models.Milestone()
            {
                Id = 2,
                McuName = "John"
            };
            dataContext.Set<Entity.Models.Milestone>().Add(milestone);
            TenantMilestone tenantMilestone = new TenantMilestone()
            {
                Id = 2,
                TenantId = 2,
                MilestoneId = 2,
                McuName = "Doe"
            };
            dataContext.Set<TenantMilestone>().Add(tenantMilestone);
            dataContext.SaveChanges();

            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            var result = await service.GetAllMilestones(2);

            Assert.Equal("Doe", result[0].Name);
        }
        [Fact]
        public async Task TestServiceGetLosMilestone()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();
            LosTenantMilestone losTenantMilestone = new LosTenantMilestone()
            {
                Id=1,
                ExternalOriginatorId=1,
                Name="Processing",
                TenantId=3
            };
            dataContext.Set<LosTenantMilestone>().Add(losTenantMilestone);
            MilestoneMapping milestoneMapping = new MilestoneMapping()
            {
                LosMilestoneId = 1,
                MilestoneId = 1
            };
            dataContext.Set<MilestoneMapping>().Add(milestoneMapping);
            dataContext.SaveChanges();

            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            var result = await service.GetLosMilestone(3,"Processing",1);

            Assert.Equal(1, result);
        }
        [Fact]
        public async Task TestServiceGetLosMilestoneNull()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();

            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            var result = await service.GetLosMilestone(4, "Processing1", 1);

            Assert.Equal(-1, result);
        }
        [Fact]
        public async Task TestServiceGetLosMilestoneMappingNull()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();
            LosTenantMilestone losTenantMilestone = new LosTenantMilestone()
            {
                Id = 2,
                ExternalOriginatorId = 1,
                Name = "Processing",
                TenantId = 5
            };
            dataContext.Set<LosTenantMilestone>().Add(losTenantMilestone);
            
            dataContext.SaveChanges();

            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            var result = await service.GetLosMilestone(5, "Processing", 1);

            Assert.Equal(-1, result);
        }
    }
}
