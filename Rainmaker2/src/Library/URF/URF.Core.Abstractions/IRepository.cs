using RainMaker.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;

namespace URF.Core.Abstractions
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken = default);
        Task<TEntity> FindAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(object[] keyValues, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default);
        Task LoadPropertyAsync(TEntity item, Expression<Func<TEntity, object>> property, CancellationToken cancellationToken = default);
        void Attach(TEntity item);
        void Detach(TEntity item);
        void Insert(TEntity item);
        void InsertRange(IEnumerable<TEntity> entities);
        void Update(TEntity item);
        void Delete(TEntity item);
        void SoftDelete(TEntity item);
        void HardDelete(TEntity item);
        Task<bool> DeleteAsync(object[] keyValues, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default);
        Task<bool> HardDeleteAsync(object[] keyValues, CancellationToken cancellationToken = default);
        Task<bool> HardDeleteAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default);
        IQueryable<TEntity> Queryable();
        IQueryable<TEntity> QueryableSql(string sql, params object[] parameters);
        IQueryable<TEntity> Query();
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> query);
        IQueryable<TEntity> Query(DynamicLinQFilter dynamicQuery, Expression<Func<TEntity, bool>> query = null);
        IRepository<T> GetRepository<T>() where T : class, ITrackable;
        dynamic GetCustomRepository<T>() where T : class, ITrackable;
    }
}