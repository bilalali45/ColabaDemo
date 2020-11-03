using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Milestone.Data;
using Milestone.Entity.Models;
using Milestone.Model;
using Milestone.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;
using URF.Core.EF;
using URF.Core.EF.Factories;
using Xunit;

namespace Milestone.Tests
{
    public class UnitOfWorkTest<TDbContext> : UnitOfWork<TDbContext> where TDbContext : DbContext
    {

        public UnitOfWorkTest(TDbContext context, IRepositoryProvider repositoryProvider) : base(context, repositoryProvider)
        {

        }

        public override async Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Unspecified, CancellationToken cancellationToken = default)
        {
        }

    }
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

            Assert.Equal(4, dataContext.Set<MilestoneLog>().Where(x => x.LoanApplicationId == 1).First().MilestoneId);
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
                LoanApplicationId = 3,
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

            Assert.Equal(2, dataContext.Set<MilestoneLog>().Where(x => x.LoanApplicationId == 4 && x.MilestoneId == 2).First().MilestoneId);
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
                Id = 20,
                McuName = "Doe"
            };
            dataContext.Set<Entity.Models.Milestone>().Add(milestone);
            TenantMilestone tenantMilestone = new TenantMilestone()
            {
                Id = 20,
                TenantId = 20,
                MilestoneId = 20,
                McuName = "Doe"
            };
            dataContext.Set<TenantMilestone>().Add(tenantMilestone);
            dataContext.SaveChanges();

            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            string result = await service.GetMilestoneForMcuDashboard(20, 20);

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
                McuName = "John"
            };
            dataContext.Set<TenantMilestone>().Add(tenantMilestone);
            dataContext.SaveChanges();

            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            var result = await service.GetAllMilestones(2);
            Debug.WriteLine(result[0].Name);
            Assert.Equal(5, result[0].Id);
        }
        [Fact]
        public async Task TestServiceGetLosMilestoneIsNull()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();
            LosTenantMilestone losTenantMilestone = new LosTenantMilestone()
            {
                Id = 1,
                ExternalOriginatorId = 1,
                Name = "Processing",
                TenantId = 3
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
            var result = await service.GetLosMilestone(3, 1, 1);

            Assert.Equal(-1, result);
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
            var result = await service.GetLosMilestone(4, 1, 1);

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
            var result = await service.GetLosMilestone(5, 1, 1);

            Assert.Equal(-1, result);
        }

        //Tenant id start from 6 

        [Fact]
        public async Task TestServiceGetGlobalMilestoneSettingIsNotNull()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();
            MilestoneSetting milestoneSetting = new MilestoneSetting()
            {
                TenantId = 6,
                ShowMilestone = false
            };
            dataContext.Set<MilestoneSetting>().Add(milestoneSetting);
            dataContext.SaveChanges();

            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            //var result = await service.GetLosMilestone(5, "Processing", 1);
            var result = await service.GetGlobalMilestoneSetting(6);
            Assert.Equal(6, result.TenantId);

        }


        [Fact]
        public async Task TestServiceSetGlobalMilestoneSettingIsNotNull()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();
            GlobalMilestoneSettingModel globalMilestoneSettingModel = new GlobalMilestoneSettingModel();
            globalMilestoneSettingModel.TenantId = 7;
            globalMilestoneSettingModel.ShowMilestone = true;

            var milestoneSetting = new MilestoneSetting()
            {
                ShowMilestone = true,
                TenantId = 7,
                TrackingState = TrackingState.Added
            };
            dataContext.Set<MilestoneSetting>().Update(milestoneSetting);
            dataContext.SaveChanges();
            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            await service.SetGlobalMilestoneSetting(globalMilestoneSettingModel);
        }
        [Fact]
        public async Task TestServiceSetGlobalMilestoneSettingIsNull()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();
            GlobalMilestoneSettingModel globalMilestoneSettingModel = new GlobalMilestoneSettingModel();
            globalMilestoneSettingModel.TenantId = 8;
            globalMilestoneSettingModel.ShowMilestone = false;

            var milestoneSetting = new MilestoneSetting()
            {
                ShowMilestone = true,
                TenantId = 8,
                TrackingState = TrackingState.Added
            };
            dataContext.Set<MilestoneSetting>().Add(milestoneSetting);
            dataContext.SaveChanges();
            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            //var result = await service.GetLosMilestone(5, "Processing", 1);
            await service.SetGlobalMilestoneSetting(globalMilestoneSettingModel);
        }
        [Fact]
        public async Task TestServiceGetMilestoneSettingList()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();
            Entity.Models.Milestone milestone = new Entity.Models.Milestone()
            {
                Id = 3,
                McuName = "John"
            };
            dataContext.Set<Entity.Models.Milestone>().Add(milestone);
            TenantMilestone tenantMilestone = new TenantMilestone()
            {
                Id = 1,
                TenantId = 9,
                MilestoneId = 3,
                McuName = "John",
                Description = "",
                BorrowerName = "",
                Visibility = true,
            };

            dataContext.Set<TenantMilestone>().Add(tenantMilestone);
            dataContext.SaveChanges();

            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            var result = await service.GetMilestoneSettingList(9);
            Assert.Equal(9, result[0].TenantId);
        }



        [Fact]
        public async Task TestServiceGetMilestoneSetting()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();
            Entity.Models.Milestone milestone = new Entity.Models.Milestone()
            {
                Id = 19,
                McuName = "John"
            };
            dataContext.Set<Entity.Models.Milestone>().Add(milestone);
            TenantMilestone tenantMilestone = new TenantMilestone()
            {
                Id = 19,
                TenantId = 19,
                MilestoneId = 19,
                McuName = "John",
                Description = "",
                BorrowerName = "",
                Visibility = true,
            };

            dataContext.Set<TenantMilestone>().Add(tenantMilestone);
            dataContext.SaveChanges();

            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            var result = await service.GetMilestoneSetting(19, 19);
            Assert.Equal(19, result.TenantId);
        }


        [Fact]
        public async Task TestServiceSetMilestoneSetting()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);
            dataContext.Database.EnsureCreated();
            var milestone = new Entity.Models.Milestone()
            {
                BorrowerName = "Joahn",
                Description = "",
                McuName = "",
                Id = 5
            };
            dataContext.Set<Entity.Models.Milestone>().Add(milestone);
            dataContext.SaveChanges();
            TenantMilestone tenantMilestone = new TenantMilestone()
            {
                Id = 3,
                BorrowerName = "Joahn",
                Description = "",
                McuName = "",
                MilestoneId = 5,
                TenantId = 11,
                Visibility = true,
                TrackingState = TrackingState.Added
            };
            dataContext.Set<TenantMilestone>().Add(tenantMilestone);
            dataContext.SaveChanges();
            var mileStoneModel = new MilestoneSettingModel
            {
                BorrowerName = "Joahn",
                Description = "",
                McuName = "",
                TenantId = 11,
                Visible = true,
                Id = 5
            };
            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWorkTest<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            await service.SetMilestoneSetting(mileStoneModel);

        }

        [Fact]
        public async Task TestServiceGetLosAll()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);
            dataContext.Database.EnsureCreated();
            var externalOriginator = new ExternalOriginator
            {
                Id = 1,
                Name = "Abc"
            };
            dataContext.Set<ExternalOriginator>().Add(externalOriginator);
            dataContext.SaveChanges();
            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            var result = await service.GetLosAll();
            Assert.Equal(1, result[0].Id);

        }


        [Fact]
        public async Task TestServiceGetMappingAll()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);
            dataContext.Database.EnsureCreated();
            var losTenantMilestone = new LosTenantMilestone
            {
                Id = 3,
                Name = "Abc",
                TenantId = 21,
                ExternalOriginatorId = 3
            };
            dataContext.Set<LosTenantMilestone>().Add(losTenantMilestone);
            dataContext.SaveChanges();
            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            var result = await service.GetMappingAll(21, 3);

            Assert.Equal(3, result[0].Id);
            Assert.Equal("Abc", result[0].Name);
        }



        [Fact]
        public async Task TestServiceSetMapping()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);
            dataContext.Database.EnsureCreated();

            var milestoneMappingModel = new MilestoneMappingModel
            {
                Id = 1
            };
            var milestoneMapping = new MilestoneMapping
            {
                MilestoneId = 1,
                LosMilestoneId = 2

            };
            dataContext.Set<MilestoneMapping>().Add(milestoneMapping);
            dataContext.SaveChanges();
            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            await service.SetMapping(milestoneMappingModel);

        }

        [Fact]
        public async Task TestServiceAddMapping()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);
            dataContext.Database.EnsureCreated();

            var milestoneMappingModel = new MilestoneAddMappingModel
            {
                TenantId = 22,
                LosId = 3
            };
            var losTenantMilestone = new LosTenantMilestone
            {
                TenantId = 22,
                ExternalOriginatorId = 3,
                Name = ""
            };

            dataContext.Set<LosTenantMilestone>().Add(losTenantMilestone);
            dataContext.SaveChanges();
            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            await service.AddMapping(milestoneMappingModel);

        }

       

        [Fact]
        public async Task TestServiceEditMapping()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);
            dataContext.Database.EnsureCreated();

            var milestoneMappingModel = new MilestoneAddMappingModel
            {
                Id = 1,
                TenantId = 23,
                LosId = 4,
                Name = "abc"
            };
            var losTenantMilestone = new LosTenantMilestone
            {
                TenantId = 23,
                ExternalOriginatorId = 4,
                Name = "Abc"
            };

            dataContext.Set<LosTenantMilestone>().Add(losTenantMilestone);
            dataContext.SaveChanges();
            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            await service.EditMapping(milestoneMappingModel);

        }

        [Fact]
        public async Task TestServiceGetMapping()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);
            dataContext.Database.EnsureCreated();
            var milestoneMapping = new MilestoneMapping
            {
                MilestoneId = 2,
                LosTenantMilestone = new LosTenantMilestone
                {
                    TenantId = 24
                }
            };
            dataContext.Set<MilestoneMapping>().Add(milestoneMapping);
            dataContext.SaveChanges();
            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);

            var result = await service.GetMapping(24, 2);

            Assert.Equal(2, result.Id);
        }


        [Fact]
        public async Task TestServiceGetGlobalMilestoneSettingIsNull()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();
            MilestoneSetting milestoneSetting = new MilestoneSetting()
            {
                TenantId = 6,
                ShowMilestone = false
            };
            dataContext.Set<MilestoneSetting>().Add(milestoneSetting);
            dataContext.SaveChanges();

            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            //var result = await service.GetLosMilestone(5, "Processing", 1);
            var result = await service.GetGlobalMilestoneSetting(0);
            Assert.Equal(0, result.TenantId);

        }



        [Fact]
        public async Task TestServiceUpdateMilestoneLogIsNull()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();
            var milestoneLog = new MilestoneLog()
            {
                LoanApplicationId = 1,
                MilestoneId = 4
            };
            dataContext.Set<MilestoneLog>().Add(milestoneLog);
            dataContext.SaveChanges();

            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            await service.UpdateMilestoneLog(1, 5);


        }

        [Fact]
        public async Task TestServiceGetMilestoneForLoanCenterIsSpecial()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();
            Entity.Models.Milestone milestone = new Entity.Models.Milestone()
            {
                Id = 6,
                MilestoneTypeId = 2,
                MilestoneLogs = new List<MilestoneLog>
                {
                    new MilestoneLog{
                    LoanApplicationId = 2,
                    MilestoneId = 6
                    }
                },

                TenantMilestones = new List<TenantMilestone>
                {
                     new TenantMilestone {
                         MilestoneId = 5 ,
                         Visibility=true,
                         TenantId=25,
                         BorrowerName="John",
                         Description="ABC"
                     }
                }
            };
            dataContext.Set<Entity.Models.Milestone>().Add(milestone);
            dataContext.SaveChanges();


            //var milestoneLog = new MilestoneLog
            //{
            //    LoanApplicationId = 2,
            //    MilestoneId = 6,
            //    Milestone = new Entity.Models.Milestone
            //    {
            //        Id = 5,
            //        TenantMilestones = new List<TenantMilestone>
            //        {
            //             new TenantMilestone{
            //                 TenantId =25,
            //                 MilestoneId=5

            //             }
            //        }

            //    }
            //};
            //dataContext.Set<MilestoneLog>().Add(milestoneLog);
            dataContext.SaveChanges();
            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            var result = await service.GetMilestoneForLoanCenter(2, 6, 25);
            Assert.Equal(2, result[0].MilestoneType);

        }


        [Fact]
        public async Task TestServiceGetMilestoneForLoanCenterIsNotSpecial()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();
            Entity.Models.Milestone milestone = new Entity.Models.Milestone()
            {
                Id = 7,
                MilestoneTypeId = 1,
                MilestoneLogs = new List<MilestoneLog>
                {
                    new MilestoneLog{
                    LoanApplicationId = 2,
                    MilestoneId = 7
                    }
                },

                TenantMilestones = new List<TenantMilestone>
                {
                     new TenantMilestone {
                         MilestoneId = 5 ,
                         Visibility=true,
                         TenantId=25,
                         BorrowerName="John",
                         Description="ABC"
                     }
                }
            };
            dataContext.Set<Entity.Models.Milestone>().Add(milestone);
            dataContext.SaveChanges();
            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            var result = await service.GetMilestoneForLoanCenter(2, 7, 25);
            Assert.Equal(1, result[0].MilestoneType);

        }

        [Fact]
        public async Task TestServiceGetLosMilestoneIsNotNull()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();
            LosTenantMilestone losTenantMilestone = new LosTenantMilestone()
            {
                Id = 7,
                ExternalOriginatorId = 2,
                Name = "Processing",
                TenantId = 6,
                StatusId = 2
            };
            dataContext.Set<LosTenantMilestone>().Add(losTenantMilestone);
            MilestoneMapping milestoneMapping = new MilestoneMapping()
            {
                LosMilestoneId = 2,
                MilestoneId = 2
            };
            dataContext.Set<MilestoneMapping>().Add(milestoneMapping);
            dataContext.SaveChanges();

            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            var result = await service.GetLosMilestone(6, 2, 2);

            Assert.Equal(-1, result);
        }
        [Fact]
        public async Task TestServiceGetMilestoneForBorrowerDashboardIsNotNull()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();
            Entity.Models.Milestone milestone = new Entity.Models.Milestone()
            {
                Id = 21,
                MilestoneTypeId = 2,
                MilestoneLogs = new List<MilestoneLog>
                {
                    new MilestoneLog{
                    LoanApplicationId = 2,
                    MilestoneId = 6
                    }
                },

                TenantMilestones = new List<TenantMilestone>
                {
                     new TenantMilestone {
                         MilestoneId = 5 ,
                         Visibility=true,
                         TenantId=26,
                         BorrowerName="John",
                         Description="ABC"
                     }
                }
            };
            dataContext.Set<Entity.Models.Milestone>().Add(milestone);
            dataContext.SaveChanges();


            
            dataContext.SaveChanges();
            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            var result = await service.GetMilestoneForBorrowerDashboard(2, 21, 26);
            Assert.Equal("John", result.Name);

        }
        [Fact]
        public async Task TestServiceGetMilestoneForBorrowerDashboardIsNull()
        {
            DbContextOptions<MilestoneContext> options;
            var builder = new DbContextOptionsBuilder<MilestoneContext>();
            builder.UseInMemoryDatabase("LosIntegration");
            options = builder.Options;
            using MilestoneContext dataContext = new MilestoneContext(options);

            dataContext.Database.EnsureCreated();
            Entity.Models.Milestone milestone = new Entity.Models.Milestone()
            {
                Id = 22,
                MilestoneTypeId = 2,
                MilestoneLogs = new List<MilestoneLog>
                {
                    new MilestoneLog{
                    LoanApplicationId = 2,
                    MilestoneId = 6
                    }
                },

                TenantMilestones = new List<TenantMilestone>
                {
                     new TenantMilestone {
                         MilestoneId = 5 ,
                         Visibility=false,
                         TenantId=26,
                         BorrowerName="John",
                         Description="ABC"
                     }
                }
            };
            dataContext.Set<Entity.Models.Milestone>().Add(milestone);
            dataContext.SaveChanges();



            dataContext.SaveChanges();
            IMilestoneService service = new Milestone.Service.MilestoneService(new UnitOfWork<MilestoneContext>(dataContext, new RepositoryProvider(new RepositoryFactories())), null);
            var result = await service.GetMilestoneForBorrowerDashboard(2, 22, 26);
            Assert.Null(result);
 

        }

    }
}
