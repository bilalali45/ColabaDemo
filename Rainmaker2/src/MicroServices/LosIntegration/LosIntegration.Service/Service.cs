using LosIntegration.Service.Interface;
using Microsoft.EntityFrameworkCore;
using RainMaker.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;
using URF.Core.Abstractions.Trackable;

namespace LosIntegration.Service
{
    public abstract class ServiceBase<TDbContext,TEntity> : IServiceBase<TEntity> where TEntity : class, ITrackable where TDbContext : DbContext
    {
        protected readonly ITrackableRepository<TEntity> Repository;
        protected readonly IServiceProvider services;
        protected ServiceBase(IUnitOfWork<TDbContext> previousUow, IServiceProvider services)
        {
            this.services = services;
            this.Uow = previousUow;
            Repository = Uow.Repository<TEntity>();
        }
        public int CurrentUserId
        {
            get { return Uow.CurrentUserId; }
            set { Uow.CurrentUserId = value; }
        }
        public string Message { get; set; }
        public IUnitOfWork<TDbContext> Uow { get; protected set; }
        public bool IsCurrentUserSystemAdmin
        {
            get { return Uow.IsCurrentUserSystemAdmin; }
            set { Uow.IsCurrentUserSystemAdmin = value; }
        }

        virtual public async Task<int> SaveChangesAsync()
        {
            int res;
            try
            {
                res = await Uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Message = ex.ToString();
                throw;
            }
            return res;
        }

        virtual public async Task<IEnumerable<TEntity>> GetAllActiveAsync()
        {

            System.Reflection.PropertyInfo isActiveProperty = typeof(TEntity).GetProperty("IsActive");
            System.Reflection.PropertyInfo isDeletedProperty = typeof(TEntity).GetProperty("IsDeleted");

            var query = Repository.Query();
            if (isDeletedProperty != null && isActiveProperty != null)
            {
                var dynamicFilter = new DynamicLinQFilter { Filter = "IsActive!=false AND IsDeleted!=true" };
                query = Repository.Query(dynamicFilter);
            }
            else if (isDeletedProperty != null)
            {
                var dynamicFilter = new DynamicLinQFilter { Filter = "IsDeleted!=true" };
                query = Repository.Query(dynamicFilter);
            }
            else if (isActiveProperty != null)
            {
                var dynamicFilter = new DynamicLinQFilter { Filter = "IsActive!=false" };
                query = Repository.Query(dynamicFilter);
            }
            return await query.ToListAsync();
        }

        virtual public async Task<IEnumerable<TEntity>> GetAllAsync()
        {

            System.Reflection.PropertyInfo isDeletedProperty = typeof(TEntity).GetProperty("IsDeleted");

            var query = Repository.Query();
            if (isDeletedProperty != null)
            {
                var dynamicFilter = new DynamicLinQFilter { Filter = "IsDeleted!=true" };
                query = Repository.Query(dynamicFilter);
            }
            return await query.ToListAsync();
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

        virtual public async Task<bool> IsExistsAsync(Dictionary<string, string> dic)
        {
            StringBuilder query = new StringBuilder();
            var dynamicFilter = new DynamicLinQFilter();
            int a = 0;
            foreach (KeyValuePair<string, string> str in dic)
            {
                if (a > 0) query.Append(" And ");
                query.Append(str.Key + "= @" + a.ToString(CultureInfo.InvariantCulture));
                dynamicFilter.Predicates.Add(str.Value);
                a++;
            }
            dynamicFilter.Filter = query.ToString();
            return (await Uow.Repository<TEntity>().Query(dynamicFilter).ToListAsync()).Any();
        }

        virtual public async Task<bool> IsUniqueAsync(string field, object value, int id = 0)
        {
            var dynamicFilter = new DynamicLinQFilter();
            var query = new System.Text.StringBuilder();
            int a = 0;

            query.Append(field);
            query.Append("= @");
            query.Append(a);
            dynamicFilter.Predicates.Add(value);

            if (id > 0)
            {
                a++;
                query.Append(" And ");
                query.Append("Id");
                query.Append("!= @");
                query.Append(a);
                dynamicFilter.Predicates.Add(id);

            }
            System.Reflection.PropertyInfo isDeletedProperty = typeof(TEntity).GetProperty("IsDeleted");

            if (isDeletedProperty != null)
            {
                a++;
                query.Append(" And ");
                query.Append("IsDeleted");
                query.Append("== @");
                query.Append(a);
                dynamicFilter.Predicates.Add(false);
            }


            dynamicFilter.Filter = query.ToString();
            return (await Uow.Repository<TEntity>().Query(dynamicFilter).ToListAsync()).Any();
        }

        virtual public async Task<bool> IsUniqueAsync(Dictionary<string, object> values, int id = 0)
        {
            var dynamicFilter = new DynamicLinQFilter();
            var query = new System.Text.StringBuilder();
            int a = 0;
            int custom;
            string key;
            foreach (KeyValuePair<string, object> strValue in values)
            {
                custom = 0;
                key = strValue.Key;

                if (query.Length > 0) query.Append(" And ");

                if (key.Contains("{#}"))
                {
                    custom = key.Split('(').Length - 1;
                    key = key.Replace("{#}", "");
                }

                query.Append(key);
                query.Append("= @");
                query.Append(a);
                dynamicFilter.Predicates.Add(strValue.Value);
                a++;

                if (custom > 0)
                {
                    query.Append(new String(')', custom));
                }

            }
            if (id > 0)
            {
                query.Append(" And ");
                query.Append("Id");
                query.Append("!= @");
                query.Append(a);
                dynamicFilter.Predicates.Add(id);
                a++;
            }
            System.Reflection.PropertyInfo isDeletedProperty = typeof(TEntity).GetProperty("IsDeleted");

            if (isDeletedProperty != null)
            {
                query.Append(" And ");
                query.Append("IsDeleted");
                query.Append("== @");
                query.Append(a);
                dynamicFilter.Predicates.Add(false);
            }

            dynamicFilter.Filter = query.ToString();
            return (await Uow.Repository<TEntity>().Query(dynamicFilter).ToListAsync()).Any();
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

        public IQueryable<TEntity> Query(DynamicLinQFilter dynamicQuery, Expression<Func<TEntity, bool>> query = null)
        {
            return Repository.Query(dynamicQuery, query);
        }
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> query)
        {
            return Repository.Query(query);
        }

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
        public virtual void HardDelete(TEntity item)
        {
            Repository.HardDelete(item);
        }

        public void SoftDelete(TEntity item)
        {
            Repository.SoftDelete(item);
        }

        IRepository<T> IRepository<TEntity>.GetRepository<T>()
        {
            return Repository.GetRepository<T>();
        }

        dynamic IRepository<TEntity>.GetCustomRepository<T>()
        {
            return Repository.GetCustomRepository<T>();
        }
        
        virtual public async Task<TEntity> GetByNameAsync(string name)
        {
            var dynamicFilter = new DynamicLinQFilter();
            var query = new System.Text.StringBuilder();
            int a = 0;

            System.Reflection.PropertyInfo nameProperty = typeof(TEntity).GetProperty("Name");

            if (nameProperty != null)
            {
                query.Append("Name");
                query.Append("= @");
                query.Append(a);
                dynamicFilter.Predicates.Add(name);
                dynamicFilter.Filter = query.ToString();
                return (await Uow.Repository<TEntity>().Query(dynamicFilter).ToListAsync()).FirstOrDefault();
            }
            else
                return null;
        }

        virtual protected async Task<List<dynamic>> GetDynamicColumnsAsync(DynamicLinQFilter dynamicFilter, string columns = "new(Id,Name,IsActive)")
        {
            var query = Repository.Query(dynamicFilter);
            return await query.Select(columns).ToDynamicListAsync();

        }
    }
}