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

    // BorrowerAsset
    
    public partial class BorrowerAssetMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<BorrowerAsset>
    {
        public void Configure(EntityTypeBuilder<BorrowerAsset> builder)
        {
            builder.ToTable("BorrowerAsset", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.AssetTypeId).HasColumnName(@"AssetTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.GiftSourceId).HasColumnName(@"GiftSourceId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.IsDeposited).HasColumnName(@"IsDeposited").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.Value).HasColumnName(@"Value").HasColumnType("decimal(18,0)").IsRequired(false);
            builder.Property(x => x.UseForDownpayment).HasColumnName(@"UseForDownpayment").HasColumnType("decimal(18,0)").IsRequired(false);
            builder.Property(x => x.ValueDate).HasColumnName(@"ValueDate").HasColumnType("date").IsRequired(false);
            builder.Property(x => x.IsJoinType).HasColumnName(@"IsJoinType").HasColumnType("bit").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.AssetType).WithMany(b => b.BorrowerAssets).HasForeignKey(c => c.AssetTypeId).OnDelete(DeleteBehavior.SetNull); // FK_BorrowerAsset_AssetType
            builder.HasOne(a => a.GiftSource).WithMany(b => b.BorrowerAssets).HasForeignKey(c => c.GiftSourceId).OnDelete(DeleteBehavior.SetNull); // FK_BorrowerAsset_GiftSource
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
