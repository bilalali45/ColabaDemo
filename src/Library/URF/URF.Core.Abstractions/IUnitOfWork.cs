using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions.Trackable;

namespace URF.Core.Abstractions
{
    public interface IUnitOfWork<out TDbContext> : IDisposable where TDbContext : DbContext
    {
        bool IsCurrentUserSystemAdmin { get; set; }
        int CurrentUserId { get; set; }
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess = true, CancellationToken cancellationToken = default);
        Task<int> ExecuteSqlCommandAsync(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default );
        TDbContext DataContext { get; }
        ITrackableRepository<TEntity> Repository<TEntity>() where TEntity : class, ITrackable;
        Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Unspecified, CancellationToken cancellationToken = default);
        Task<bool> CommitAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync(CancellationToken cancellationToken = default);
        dynamic GetCustomRepository<T>();
        dynamic GetCustomRepository(Type type);
    }
}