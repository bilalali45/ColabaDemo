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

    // MilestoneEmailConfiguration
    
    public partial class MilestoneEmailConfigurationMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<MilestoneEmailConfiguration>
    {
        public void Configure(EntityTypeBuilder<MilestoneEmailConfiguration> builder)
        {
            builder.ToTable("MilestoneEmailConfiguration", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.StatusUpdateId).HasColumnName(@"StatusUpdateId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.FromAddress).HasColumnName(@"FromAddress").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.ToAddress).HasColumnName(@"ToAddress").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.CcAddress).HasColumnName(@"CCAddress").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.Subject).HasColumnName(@"Subject").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.Body).HasColumnName(@"Body").HasColumnType("nvarchar(max)").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.MilestoneStatusConfiguration).WithMany(b => b.MilestoneEmailConfigurations).HasForeignKey(c => c.StatusUpdateId).OnDelete(DeleteBehavior.SetNull); // FK_MilestoneEmailConfiguration_MilestoneStatusConfiguration
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
