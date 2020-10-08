using Microsoft.EntityFrameworkCore;
using RainMaker.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;

namespace URF.Core.EF
{
    public abstract class Repository<TDbContext,TEntity> : IRepository<TEntity> where TEntity : class where TDbContext : DbContext
    {

        public Repository(IUnitOfWork<TDbContext> uow)
        {
            Uow = uow;
            Context = uow.DataContext;
            Set = Context.Set<TEntity>();
        }

        protected DbContext Context { get; }
        public IUnitOfWork<TDbContext> Uow { get; protected set; }
        protected DbSet<TEntity> Set { get; }

        public virtual async Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken = default)
            => await Set.FindAsync(keyValues, cancellationToken);

        public virtual async Task<TEntity> FindAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default)
            => await FindAsync(new object[] { keyValue }, cancellationToken);

        public virtual async Task<bool> ExistsAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            var item = await FindAsync(keyValues, cancellationToken);
            return item != null;
        }

        public virtual async Task<bool> ExistsAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default)
            => await ExistsAsync(new object[] { keyValue }, cancellationToken);

        public virtual async Task LoadPropertyAsync(TEntity item, Expression<Func<TEntity, object>> property, CancellationToken cancellationToken = default)
            => await Context.Entry(item).Reference(property).LoadAsync(cancellationToken);

        public virtual void Attach(TEntity item)
            => Set.Attach(item);

        public virtual void Detach(TEntity item)
            => Context.Entry(item).State = EntityState.Detached;

        public virtual void Insert(TEntity item)
            => Context.Entry(item).State = EntityState.Added;
        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var e in entities)
                Insert(e);
        }
        public virtual void Update(TEntity item)
            => Context.Entry(item).State = EntityState.Modified;

        public virtual void Delete(TEntity item)
            => Context.Entry(item).State = EntityState.Deleted;

        public virtual async Task<bool> DeleteAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            var item = await FindAsync(keyValues, cancellationToken);
            if (item == null) return false;
            Delete(item);
            return true;
        }

        public virtual async Task<bool> DeleteAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default)
            => await DeleteAsync(new object[] { keyValue }, cancellationToken);

        public virtual IQueryable<TEntity> Queryable() => Set;

        public virtual IQueryable<TEntity> QueryableSql(string sql, params object[] parameters)
            => Set.FromSqlRaw(sql, parameters);

        public virtual IQueryable<TEntity> Query() => Set;
        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> query)
        {
            return Set.Where(query);
        }

        public virtual IQueryable<TEntity> Query(DynamicLinQFilter dynamicQuery, Expression<Func<TEntity, bool>> query = null)
        {
            IQueryable<TEntity> q = Set;
            if (!string.IsNullOrEmpty(dynamicQuery.Filter))
                q = q.Where(dynamicQuery.Filter,dynamicQuery.Predicates.ToArray());
            if (query != null)
                q = q.Where(query);
            return q;
        }

        public virtual void SoftDelete(TEntity item)
        {
            throw new NotImplementedException();
        }
        public virtual IRepository<T> GetRepository<T>() where T : class, ITrackable
        {
            return Uow.Repository<T>();
        }

        public virtual dynamic GetCustomRepository<T>() where T : class, ITrackable
        {
            return Uow.GetCustomRepository<T>();
        }
        public virtual void HardDelete(TEntity item)
        {
            throw new NotImplementedException();
        }

        public async virtual Task<bool> HardDeleteAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            var item = await FindAsync(keyValues, cancellationToken);
            if (item == null) return false;
            HardDelete(item);
            return true;
        }

        public async virtual Task<bool> HardDeleteAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default)
        {
            return await HardDeleteAsync(new object[] { keyValue }, cancellationToken);
        }
    }
}