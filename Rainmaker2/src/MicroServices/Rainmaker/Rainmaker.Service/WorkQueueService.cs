using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System;

using URF.Core.Abstractions;


namespace Rainmaker.Service
{
    public class WorkQueueService : ServiceBase<RainMakerContext, WorkQueue>, IWorkQueueService
    {
        public WorkQueueService(IUnitOfWork<RainMakerContext> previousUow, IServiceProvider services)
           : base(previousUow, services)
        {

        }
        
    }
}
