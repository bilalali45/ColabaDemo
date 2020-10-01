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

    // Milestone
    
    public partial class MilestoneMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Milestone>
    {
        public void Configure(EntityTypeBuilder<Milestone> builder)
        {
            builder.ToTable("Milestone", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Order).HasColumnName(@"Order").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Icon).HasColumnName(@"Icon").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(2000);
            builder.Property(x => x.BorrowerName).HasColumnName(@"BorrowerName").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.McuName).HasColumnName(@"McuName").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(500);
            builder.Property(x => x.MilestoneTypeId).HasColumnName(@"MilestoneTypeId").HasColumnType("int").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.MilestoneType).WithMany(b => b.Milestones).HasForeignKey(c => c.MilestoneTypeId).OnDelete(DeleteBehavior.SetNull); // FK_Milestone_MilestoneType_Id
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
