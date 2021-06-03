using RainMaker.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions.Trackable;

namespace LoanApplication.Service
{
    public interface IServiceBase<TEntity>: ITrackableRepository<TEntity> where TEntity : class, ITrackable
    {
        Task<int> SaveChangesAsync();
        Task<TEntity> GetByIdWithDetailAsync(int id);
        Task<TEntity> GetByIdAsync(int id);
    }
}
