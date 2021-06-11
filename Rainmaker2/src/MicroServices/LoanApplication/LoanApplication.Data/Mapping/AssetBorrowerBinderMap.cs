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

    // AssetBorrowerBinder
    
    public partial class AssetBorrowerBinderMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<AssetBorrowerBinder>
    {
        public void Configure(EntityTypeBuilder<AssetBorrowerBinder> builder)
        {
            builder.ToTable("AssetBorrowerBinder", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.BorrowerAssetId).HasColumnName(@"BorrowerAssetId").HasColumnType("int").IsRequired();
            builder.Property(x => x.BorrowerId).HasColumnName(@"BorrowerId").HasColumnType("int").IsRequired();
            builder.Property(x => x.TenantId).HasColumnName(@"TenantId").HasColumnType("int").IsRequired();
            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();

            // Foreign keys
            builder.HasOne(a => a.Borrower).WithMany(b => b.AssetBorrowerBinders).HasForeignKey(c => c.BorrowerId).OnDelete(DeleteBehavior.SetNull); // FK_AssetBorrowerBinder_Borrower
            builder.HasOne(a => a.BorrowerAsset).WithMany(b => b.AssetBorrowerBinders).HasForeignKey(c => c.BorrowerAssetId).OnDelete(DeleteBehavior.SetNull); // FK_AssetBorrowerBinder_BorrowerAsset
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>