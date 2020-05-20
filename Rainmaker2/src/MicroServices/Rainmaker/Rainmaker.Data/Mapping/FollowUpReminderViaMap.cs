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

    // FollowUpReminderVia
    
    public partial class FollowUpReminderViaMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<FollowUpReminderVia>
    {
        public void Configure(EntityTypeBuilder<FollowUpReminderVia> builder)
        {
            builder.ToTable("FollowUpReminderVia", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.FollowUpId).HasColumnName(@"FollowUpId").HasColumnType("int").IsRequired();
            builder.Property(x => x.FollowUpViaId).HasColumnName(@"FollowUpViaId").HasColumnType("int").IsRequired();
            builder.Property(x => x.WorkQueueId).HasColumnName(@"WorkQueueId").HasColumnType("int").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.FollowUp).WithMany(b => b.FollowUpReminderVias).HasForeignKey(c => c.FollowUpId).OnDelete(DeleteBehavior.SetNull); // FK_FollowUpReminderVia_FollowUp
            builder.HasOne(a => a.WorkQueue).WithMany(b => b.FollowUpReminderVias).HasForeignKey(c => c.WorkQueueId).OnDelete(DeleteBehavior.SetNull); // FK_FollowUpReminderVia_WorkQueue
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
