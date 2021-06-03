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


namespace LoanApplicationDb.Data.Mapping
{
    using LoanApplicationDb.Entity.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    // BorrowerAsset
    
    public partial class BorrowerAssetMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<BorrowerAsset>
    {
        public void Configure(EntityTypeBuilder<BorrowerAsset> builder)
        {
            builder.ToTable("BorrowerAsset", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.AssetTypeId).HasColumnName(@"AssetTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.GiftSourceId).HasColumnName(@"GiftSourceId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.IsDeposited).HasColumnName(@"IsDeposited").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.Value).HasColumnName(@"Value").HasColumnType("decimal(18,0)").IsRequired(false);
            builder.Property(x => x.UseForDownpayment).HasColumnName(@"UseForDownpayment").HasColumnType("decimal(18,0)").IsRequired(false);
            builder.Property(x => x.ValueDate).HasColumnName(@"ValueDate").HasColumnType("date").IsRequired(false);
            builder.Property(x => x.IsJoinType).HasColumnName(@"IsJoinType").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.TenantId).HasColumnName(@"TenantId").HasColumnType("int").IsRequired();
            builder.Property(x => x.InstitutionName).HasColumnName(@"InstitutionName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.AccountNumber).HasColumnName(@"AccountNumber").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.AccountTitle).HasColumnName(@"AccountTitle").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.CollateralAssetTypeId).HasColumnName(@"CollateralAssetTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CollateralAssetDescription).HasColumnName(@"CollateralAssetDescription").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.SecuredByCollateral).HasColumnName(@"SecuredByCollateral").HasColumnType("bit").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.AssetType).WithMany(b => b.BorrowerAssets).HasForeignKey(c => c.AssetTypeId).OnDelete(DeleteBehavior.SetNull); // FK_BorrowerAsset_AssetType
            builder.HasOne(a => a.CollateralAssetType).WithMany(b => b.BorrowerAssets).HasForeignKey(c => c.CollateralAssetTypeId).OnDelete(DeleteBehavior.SetNull); // FK_BorrowerAsset_CollateralAssetType_Id
            builder.HasOne(a => a.GiftSource).WithMany(b => b.BorrowerAssets).HasForeignKey(c => c.GiftSourceId).OnDelete(DeleteBehavior.SetNull); // FK_BorrowerAsset_GiftSource
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
