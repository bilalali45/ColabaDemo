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

    // FollowUpActivityBinder
    
    public partial class FollowUpActivityBinderMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<FollowUpActivityBinder>
    {
        public void Configure(EntityTypeBuilder<FollowUpActivityBinder> builder)
        {
            builder.ToTable("FollowUpActivityBinder", "dbo");
            builder.HasKey(x => new { x.FollowUpId, x.FollowUpActivityId });

            builder.Property(x => x.FollowUpId).HasColumnName(@"FollowUpId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.FollowUpActivityId).HasColumnName(@"FollowUpActivityId").HasColumnType("int").IsRequired().ValueGeneratedNever();

            // Foreign keys
            builder.HasOne(a => a.FollowUp).WithMany(b => b.FollowUpActivityBinders).HasForeignKey(c => c.FollowUpId).OnDelete(DeleteBehavior.SetNull); // FK_FollowUpActivityBinder_FollowUp
            builder.HasOne(a => a.FollowUpActivity).WithMany(b => b.FollowUpActivityBinders).HasForeignKey(c => c.FollowUpActivityId).OnDelete(DeleteBehavior.SetNull); // FK_FollowUpActivityBinder_FollowUpActivity
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
