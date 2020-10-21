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
    public class MilestoneService
    {
        [Fact]
        public async Task TestInsertMilestoneLog()
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
    }
}
