// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 2.1
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace RainMaker.Data.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RainMaker.Entity.Models;

    // OpportunityLockStatusLog
    
    public partial class OpportunityLockStatusLogMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<OpportunityLockStatusLog>
    {
        public void Configure(EntityTypeBuilder<OpportunityLockStatusLog> builder)
        {
            builder.ToTable("OpportunityLockStatusLog", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.LockStatusId).HasColumnName(@"LockStatusId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.LockCauseId).HasColumnName(@"LockCauseId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.DatetimeUtc).HasColumnName(@"DatetimeUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.OpportunityId).HasColumnName(@"OpportunityId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ModifiedOnUtc).HasColumnName(@"ModifiedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.LockStatusCause).WithMany(b => b.OpportunityLockStatusLogs).HasForeignKey(c => c.LockCauseId).OnDelete(DeleteBehavior.SetNull); // FK_OpportunityLockStatusLog_LockStatusCause
            builder.HasOne(a => a.LockStatusList).WithMany(b => b.OpportunityLockStatusLogs).HasForeignKey(c => c.LockStatusId).OnDelete(DeleteBehavior.SetNull); // FK_OpportunityLockStatusLog_LockStatusList
            builder.HasOne(a => a.Opportunity).WithMany(b => b.OpportunityLockStatusLogs).HasForeignKey(c => c.OpportunityId).OnDelete(DeleteBehavior.SetNull); // FK_OpportunityLockStatusLog_Opportunity
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
