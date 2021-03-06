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

    // ActivityLog
    
    public partial class Vortex_ActivityLogMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Vortex_ActivityLog>
    {
        public void Configure(EntityTypeBuilder<Vortex_ActivityLog> builder)
        {
            builder.ToTable("ActivityLog", "Vortex");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("bigint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.NoteId).HasColumnName(@"NoteId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.OpportunityId).HasColumnName(@"OpportunityId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.DateUtc).HasColumnName(@"DateUtc").HasColumnType("datetime2").IsRequired();
            builder.Property(x => x.ActivityTypeId).HasColumnName(@"ActivityTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.ToNumber).HasColumnName(@"ToNumber").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(20);
            builder.Property(x => x.FromNumber).HasColumnName(@"FromNumber").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(20);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired();
            builder.Property(x => x.CreatedDateUtc).HasColumnName(@"CreatedDateUtc").HasColumnType("datetime2").IsRequired();
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ModifiedDateUtc).HasColumnName(@"ModifiedDateUtc").HasColumnType("datetime2").IsRequired(false);
            builder.Property(x => x.Sid).HasColumnName(@"Sid").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.ActivityDirectionId).HasColumnName(@"ActivityDirectionId").HasColumnType("int").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Note).WithMany(b => b.Vortex_ActivityLogs).HasForeignKey(c => c.NoteId).OnDelete(DeleteBehavior.SetNull); // FK_Activity_Note
            builder.HasOne(a => a.Opportunity).WithMany(b => b.Vortex_ActivityLogs).HasForeignKey(c => c.OpportunityId).OnDelete(DeleteBehavior.SetNull); // FK_Activity_Opportunity
            builder.HasOne(a => a.UserProfile).WithMany(b => b.Vortex_ActivityLogs).HasForeignKey(c => c.CreatedBy).OnDelete(DeleteBehavior.SetNull); // FK_Activity_UserProfile
            builder.HasOne(a => a.Vortex_ActivityDirection).WithMany(b => b.Vortex_ActivityLogs).HasForeignKey(c => c.ActivityDirectionId).OnDelete(DeleteBehavior.SetNull); // FK_Activity_ActivityDirection
            builder.HasOne(a => a.Vortex_ActivityType).WithMany(b => b.Vortex_ActivityLogs).HasForeignKey(c => c.ActivityTypeId).OnDelete(DeleteBehavior.SetNull); // FK_Activity_ActivityType
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
