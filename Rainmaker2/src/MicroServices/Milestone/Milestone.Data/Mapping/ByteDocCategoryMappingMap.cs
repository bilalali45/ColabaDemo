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

    // ByteDocCategoryMapping
    
    public partial class ByteDocCategoryMappingMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<ByteDocCategoryMapping>
    {
        public void Configure(EntityTypeBuilder<ByteDocCategoryMapping> builder)
        {
            builder.ToTable("ByteDocCategoryMapping", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.ByteDocCategoryName).HasColumnName(@"ByteDocCategoryName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.RmDocCategoryName).HasColumnName(@"RmDocCategoryName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(50);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
