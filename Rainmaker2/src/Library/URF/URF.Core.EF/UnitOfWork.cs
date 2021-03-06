using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;
using TrackableEntities.EF.Core;
using URF.Core.Abstractions;
using URF.Core.Abstractions.Trackable;
using URF.Core.EF.Factories;

namespace URF.Core.EF
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        private bool _disposed;
        private IDbContextTransaction _transaction;
        private IHttpContextAccessor _httpContextAccessor;

        public UnitOfWork(TDbContext context, IRepositoryProvider repositoryProvider, IHttpContextAccessor httpContextAccessor=null)
        {
            DataContext = context;
            RepositoryProvider = repositoryProvider;
            RepositoryProvider.DataContext = context;
            RepositoryProvider.UnitOfWork = this;
            _httpContextAccessor = httpContextAccessor;
        }
        public bool IsCurrentUserSystemAdmin { get; set; }
        public int CurrentUserId { get; set; }
        public virtual async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess = true, CancellationToken cancellationToken = default)
        {
            if(_httpContextAccessor?.HttpContext!=null)
            {
                await DataContext.Database.OpenConnectionAsync();
                if(_httpContextAccessor.HttpContext.Items["TenantId"]!=null)
                {
                    await DataContext.Database.ExecuteSqlRawAsync($"EXEC sys.sp_set_session_context @key = N'tenantId', @value = N'{_httpContextAccessor.HttpContext.Items["TenantId"]}'");
                }
                if (_httpContextAccessor.HttpContext.Items["CorrelationId"] != null)
                {
                    await DataContext.Database.ExecuteSqlRawAsync($"EXEC sys.sp_set_session_context @key = N'correlationId', @value = N'{_httpContextAccessor.HttpContext.Items["CorrelationId"]}'");
                }
                await DataContext.Database.ExecuteSqlRawAsync($"EXEC sys.sp_set_session_context @key = N'transactionId', @value = N'{Guid.NewGuid()}'");
                if (_httpContextAccessor?.HttpContext.User!=null && _httpContextAccessor?.HttpContext.User.Identity.IsAuthenticated==true)
                {
                    int userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("UserId").Value);
                    await DataContext.Database.ExecuteSqlRawAsync($"EXEC sys.sp_set_session_context @key = N'loggedUser', @value = N'{userId}'");
                }
            }
            IEnumerable<ITrackable> entities = DataContext.ChangeTracker.Entries().Select(x=>x.Entity).OfType<ITrackable>().Where(x=>x.TrackingState!=TrackingState.Unchanged).ToList();
            DataContext.ApplyChanges(entities);
            int ret = await DataContext.SaveChangesAsync(acceptAllChangesOnSuccess,cancellationToken);
            if(acceptAllChangesOnSuccess)
            {
                DataContext.AcceptChanges(entities);
            }
            return ret;
        }

        public virtual async Task<int> ExecuteSqlCommandAsync(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default)
            => await DataContext.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);

        public TDbContext DataContext { get; private set; }
        protected IRepositoryProvider RepositoryProvider { get; set; }

        public async void Dispose()
        {
            await Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected async virtual Task Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                await RollbackAsync();
                if (DataContext != null)
                {
                    DataContext.Dispose();
                    DataContext = null;
                }
            }

            _disposed = true;
        }
        public ITrackableRepository<TEntity> Repository<TEntity>() where TEntity : class, ITrackable
        {
            return RepositoryProvider.GetRepositoryForEntityType<TEntity>();
        }
        public virtual async Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Unspecified, CancellationToken cancellationToken = default)
        {
            if(DataContext.Database.GetDbConnection().State!=ConnectionState.Open)
            {
                DataContext.Database.GetDbConnection().Open();
            }
            _transaction = await DataContext.Database.BeginTransactionAsync(isolationLevel,cancellationToken);
        }
        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null) return false;
            await _transaction.CommitAsync(cancellationToken);
            _transaction = null;
            return true;
        }
        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null) return;
            await _transaction.RollbackAsync(cancellationToken);
            _transaction = null;
        }
        public dynamic GetCustomRepository(Type type)
        {
            return RepositoryProvider.GetCustomRepository(type);
        }

        public dynamic GetCustomRepository<T>()
        {
            return RepositoryProvider.GetCustomRepository<T>();
        }
    }
}