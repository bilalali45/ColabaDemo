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

    // ProductTemplatekey
    
    public partial class ProductTemplatekeyMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<ProductTemplatekey>
    {
        public void Configure(EntityTypeBuilder<ProductTemplatekey> builder)
        {
            builder.ToTable("ProductTemplatekey", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.ProductId).HasColumnName(@"ProductId").HasColumnType("int").IsRequired();
            builder.Property(x => x.Symbol).HasColumnName(@"Symbol").HasColumnType("nvarchar").IsRequired().HasMaxLength(100);
            builder.Property(x => x.KeyName).HasColumnName(@"KeyName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(300);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.FieldTypeId).HasColumnName(@"FieldTypeId").HasColumnType("int").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Product).WithMany(b => b.ProductTemplatekeys).HasForeignKey(c => c.ProductId).OnDelete(DeleteBehavior.SetNull); // FK_ProductTemplatekey_Product
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>