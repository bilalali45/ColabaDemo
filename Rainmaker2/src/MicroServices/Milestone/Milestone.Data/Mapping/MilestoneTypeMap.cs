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

    // MilestoneType
    
    public partial class MilestoneTypeMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<MilestoneType>
    {
        public void Configure(EntityTypeBuilder<MilestoneType> builder)
        {
            builder.ToTable("MilestoneType", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(50);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
