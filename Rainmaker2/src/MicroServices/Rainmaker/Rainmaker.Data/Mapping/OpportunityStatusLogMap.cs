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

    // OpportunityStatusLog
    
    public partial class OpportunityStatusLogMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<OpportunityStatusLog>
    {
        public void Configure(EntityTypeBuilder<OpportunityStatusLog> builder)
        {
            builder.ToTable("OpportunityStatusLog", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.StatusId).HasColumnName(@"StatusId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.StatusCauseId).HasColumnName(@"StatusCauseId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.DatetimeUtc).HasColumnName(@"DatetimeUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.OpportunityId).HasColumnName(@"OpportunityId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ModifiedOnUtc).HasColumnName(@"ModifiedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Opportunity).WithMany(b => b.OpportunityStatusLogs).HasForeignKey(c => c.OpportunityId).OnDelete(DeleteBehavior.SetNull); // FK_OpportunityStatusLog_Opportunity
            builder.HasOne(a => a.StatusCause).WithMany(b => b.OpportunityStatusLogs).HasForeignKey(c => c.StatusCauseId).OnDelete(DeleteBehavior.SetNull); // FK_OpportunityStatusLog_StatusCause
            builder.HasOne(a => a.StatusList).WithMany(b => b.OpportunityStatusLogs).HasForeignKey(c => c.StatusId).OnDelete(DeleteBehavior.SetNull); // FK_OpportunityStatusLog_StatusList
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>