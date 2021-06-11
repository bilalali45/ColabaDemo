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


namespace TenantConfig.Data.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TenantConfig.Entity.Models;

    // TwoFAModes
    
    public partial class TwoFaModeMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<TwoFaMode>
    {
        public void Configure(EntityTypeBuilder<TwoFaMode> builder)
        {
            builder.ToTable("TwoFAModes", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("smallint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            builder.Property(x => x.DisplayName).HasColumnName(@"DisplayName").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            builder.Property(x => x.DisplayOrder).HasColumnName(@"DisplayOrder").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired(false);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>