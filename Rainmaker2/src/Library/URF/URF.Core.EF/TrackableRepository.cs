using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackableEntities.Common.Core;
using TrackableEntities.EF.Core;
using URF.Core.Abstractions;
using URF.Core.Abstractions.Trackable;

namespace URF.Core.EF.Trackable
{
    public class TrackableRepository<TDbContext,TEntity> : Repository<TDbContext, TEntity>, ITrackableRepository<TEntity>
        where TEntity : class, ITrackable where TDbContext : DbContext
    {
        public TrackableRepository(IUnitOfWork<TDbContext> uow) : base(uow)
        {
        }

        public override void Insert(TEntity item)
        {
            item.TrackingState = TrackingState.Added;
            base.Insert(item);
        }

        public override void Update(TEntity item)
        {
            item.TrackingState = TrackingState.Modified;
            base.Update(item);
        }

        public override void SoftDelete(TEntity item)
        {
            System.Reflection.PropertyInfo isDeletedProperty = typeof(TEntity).GetProperty("IsDeleted");
            if (isDeletedProperty != null)
            {
                isDeletedProperty.SetValue(item, true);
                Update(item);
            }
        }

        public override void HardDelete(TEntity item)
        {
            item.TrackingState = TrackingState.Deleted;
            base.Delete(item);
        }
        public override void Delete(TEntity item)
        {
            System.Reflection.PropertyInfo isDeletedProperty = typeof(TEntity).GetProperty("IsDeleted");
            if(isDeletedProperty!=null)
            {
                SoftDelete(item);
            }
            else
            {
                HardDelete(item);
            }
        }

        public virtual void ApplyChanges(params TEntity[] entities)
            => Context.ApplyChanges(entities);

        public virtual void AcceptChanges(params TEntity[] entities)
            => Context.AcceptChanges(entities);

        public virtual void DetachEntities(params TEntity[] entities)
            => Context.DetachEntities(entities);

        public virtual async Task LoadRelatedEntities(params TEntity[] entities)
            => await Context.LoadRelatedEntitiesAsync(entities);
    }
}
