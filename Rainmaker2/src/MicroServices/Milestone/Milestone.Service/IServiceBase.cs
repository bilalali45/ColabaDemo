using System.Collections.Generic;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions.Trackable;

namespace Milestone.Service
{
    public interface IServiceBase<TEntity> : ITrackableRepository<TEntity> where TEntity : class, ITrackable
    {
        
    }
}
