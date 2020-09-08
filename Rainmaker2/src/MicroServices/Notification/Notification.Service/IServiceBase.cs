using System.Collections.Generic;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions.Trackable;

namespace Notification.Service
{
    public interface IServiceBase<TEntity> : ITrackableRepository<TEntity> where TEntity : class, ITrackable
    {
        int CurrentUserId { get; set; }
        bool IsCurrentUserSystemAdmin { get; set; }
        string Message { get; set; }
        Task<int> SaveChangesAsync();
        Task<IEnumerable<TEntity>> GetAllActiveAsync();
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdWithDetailAsync(int id);
        Task<TEntity> GetByIdAsync(int id);
        Task<bool> IsExistsAsync(Dictionary<string, string> dic);
        Task<bool> IsUniqueAsync(string field, object value, int id = 0);
        Task<bool> IsUniqueAsync(Dictionary<string, object> values, int id = 0);
        Task<TEntity> GetByNameAsync(string name);
    }
}
