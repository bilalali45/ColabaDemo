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

    // ScheduleActivity
    
    public partial class ScheduleActivityMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<ScheduleActivity>
    {
        public void Configure(EntityTypeBuilder<ScheduleActivity> builder)
        {
            builder.ToTable("ScheduleActivity", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired().HasMaxLength(500);
            builder.Property(x => x.SchedulerId).HasColumnName(@"SchedulerId").HasColumnType("int").IsRequired();
            builder.Property(x => x.SystemActivityId).HasColumnName(@"SystemActivityId").HasColumnType("int").IsRequired();
            builder.Property(x => x.QueueStatusId).HasColumnName(@"QueueStatusId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            builder.Property(x => x.StopOnError).HasColumnName(@"StopOnError").HasColumnType("bit").IsRequired();
            builder.Property(x => x.LastStartUtc).HasColumnName(@"LastStartUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.LastEndUtc).HasColumnName(@"LastEndUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.LastSuccessUtc).HasColumnName(@"LastSuccessUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.NextRunUtc).HasColumnName(@"NextRunUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.NextRunOffset).HasColumnName(@"NextRunOffset").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ModifiedOnUtc).HasColumnName(@"ModifiedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.IsSystem).HasColumnName(@"IsSystem").HasColumnType("bit").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Scheduler).WithMany(b => b.ScheduleActivities).HasForeignKey(c => c.SchedulerId).OnDelete(DeleteBehavior.SetNull); // FK_ScheduleActivity_Scheduler
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
