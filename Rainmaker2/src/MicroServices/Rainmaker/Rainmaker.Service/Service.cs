using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RainMaker.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;
using URF.Core.Abstractions.Trackable;

namespace RainMaker.Service
{
    public abstract class ServiceBase<TDbContext,TEntity> : IServiceBase<TEntity> where TEntity : class, ITrackable where TDbContext : DbContext
    {
        protected readonly ITrackableRepository<TEntity> Repository;
        protected readonly IServiceProvider services;
        public ServiceBase(IUnitOfWork<TDbContext> previousUow, IServiceProvider services)
        {
            this.services = services;
            this.Uow = previousUow;
            Repository = Uow.Repository<TEntity>();
        }
        public IUnitOfWork<TDbContext> Uow { get; protected set; }

        virtual public async Task<int> SaveChangesAsync()
        {
            return await Uow.SaveChangesAsync();
        }

        virtual public async Task<TEntity> GetByIdWithDetailAsync(int id)
        {

            return await GetByIdAsync(id);
        }

        virtual public async Task<TEntity> GetByIdAsync(int id)
        {

            var obj = await Repository.FindAsync(id);

            if (obj == null) return null;

            System.Reflection.PropertyInfo isDeletedProperty = typeof(TEntity).GetProperty("IsDeleted");

            if (isDeletedProperty != null)
            {
                obj = (bool)isDeletedProperty.GetValue(obj) ? null : obj;
            }

            return obj;
        }

        public virtual void Attach(TEntity item)
            => Repository.Attach(item);

        public virtual void Delete(TEntity item)
            => Repository.Delete(item);

        public virtual async Task<bool> DeleteAsync(object[] keyValues, CancellationToken cancellationToken = default) 
            => await Repository.DeleteAsync(keyValues, cancellationToken);

        public virtual async Task<bool> DeleteAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default)
            => await Repository.DeleteAsync(keyValue, cancellationToken);

        public virtual void Detach(TEntity item)
            => Repository.Detach(item);

        public virtual async Task<bool> ExistsAsync(object[] keyValues, CancellationToken cancellationToken = default)
            => await Repository.ExistsAsync(keyValues, cancellationToken);

        public virtual async Task<bool> ExistsAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default) 
            => await Repository.ExistsAsync(keyValue, cancellationToken);

        public virtual async Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken = default) 
            => await Repository.FindAsync(keyValues, cancellationToken);

        public virtual async Task<TEntity> FindAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default) 
            => await Repository.FindAsync(keyValue, cancellationToken);

        public virtual void Insert(TEntity item)
            => Repository.Insert(item);

        public virtual async Task LoadPropertyAsync(TEntity item, Expression<Func<TEntity, object>> property, CancellationToken cancellationToken = default) 
            => await Repository.LoadPropertyAsync(item, property, cancellationToken);

        public virtual IQueryable<TEntity> Query()
            => Repository.Query();

        public virtual IQueryable<TEntity> Queryable() 
            => Repository.Queryable();

        public virtual IQueryable<TEntity> QueryableSql(string sql, params object[] parameters) 
            => Repository.QueryableSql(sql, parameters);

        public virtual void Update(TEntity item) 
            => Repository.Update(item);

        public virtual void ApplyChanges(params TEntity[] entities)
            => Repository.ApplyChanges(entities);

        public virtual void AcceptChanges(params TEntity[] entities)
            => Repository.AcceptChanges(entities);

        public virtual void DetachEntities(params TEntity[] entities)
            => Repository.DetachEntities(entities);

        public virtual async Task LoadRelatedEntities(params TEntity[] entities)
            =>  await Repository.LoadRelatedEntities(entities);

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            Repository.InsertRange(entities);
        }

        public virtual async Task<bool> HardDeleteAsync(object[] keyValues, CancellationToken cancellationToken = default)
            => await Repository.HardDeleteAsync(keyValues, cancellationToken);

        public virtual async Task<bool> HardDeleteAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default)
            => await Repository.HardDeleteAsync(keyValue, cancellationToken);
        public virtual void HardDelete(TEntity entity)
        {
            Repository.HardDelete(entity);
        }

        public void SoftDelete(TEntity item)
        {
            Repository.SoftDelete(item);
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> query)
        {
            return Repository.Query(query);
        }

        public IQueryable<TEntity> Query(DynamicLinQFilter dynamicQuery, Expression<Func<TEntity, bool>> query = null)
        {
            return Repository.Query(dynamicQuery,query);
        }

        IRepository<T> IRepository<TEntity>.GetRepository<T>()
        {
            return Repository.GetRepository<T>();
        }

        dynamic IRepository<TEntity>.GetCustomRepository<T>()
        {
            return Repository.GetCustomRepository<T>();
        }
        
    }
}