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

    // ProductAd
    
    public partial class ProductAdMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<ProductAd>
    {
        public void Configure(EntityTypeBuilder<ProductAd> builder)
        {
            builder.ToTable("ProductAd", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.ProductId).HasColumnName(@"ProductId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.RateTypeId).HasColumnName(@"RateTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.DisplayOrder).HasColumnName(@"DisplayOrder").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ModifiedOnUtc).HasColumnName(@"ModifiedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.TpId).HasColumnName(@"TpId").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);

            // Foreign keys
            builder.HasOne(a => a.Product).WithMany(b => b.ProductAds).HasForeignKey(c => c.ProductId).OnDelete(DeleteBehavior.SetNull); // FK_ProductAd_Product
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
