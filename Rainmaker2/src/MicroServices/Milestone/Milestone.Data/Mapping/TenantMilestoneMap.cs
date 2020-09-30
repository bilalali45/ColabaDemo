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


namespace Milestone.Data.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Milestone.Entity.Models;

    // TenantMilestone
    
    public partial class TenantMilestoneMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<TenantMilestone>
    {
        public void Configure(EntityTypeBuilder<TenantMilestone> builder)
        {
            builder.ToTable("TenantMilestone", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.BorrowerName).HasColumnName(@"BorrowerName").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.McuName).HasColumnName(@"McuName").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(500);
            builder.Property(x => x.Visibility).HasColumnName(@"Visibility").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.TenantId).HasColumnName(@"TenantId").HasColumnType("int").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.Milestone).WithOne(b => b.TenantMilestone).HasForeignKey<TenantMilestone>(c => c.Id).OnDelete(DeleteBehavior.SetNull); // FK_TenantMilestone_Milestone_Id
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>