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

    // BankRateLoanToValueBinder
    
    public partial class BankRateLoanToValueBinderMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<BankRateLoanToValueBinder>
    {
        public void Configure(EntityTypeBuilder<BankRateLoanToValueBinder> builder)
        {
            builder.ToTable("BankRateLoanToValueBinder", "dbo");
            builder.HasKey(x => new { x.BankRateProductId, x.LoanToValueId });

            builder.Property(x => x.BankRateProductId).HasColumnName(@"BankRateProductId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.LoanToValueId).HasColumnName(@"LoanToValueId").HasColumnType("int").IsRequired().ValueGeneratedNever();

            // Foreign keys
            builder.HasOne(a => a.BankRateProduct).WithMany(b => b.BankRateLoanToValueBinders).HasForeignKey(c => c.BankRateProductId).OnDelete(DeleteBehavior.SetNull); // FK_BankRateLoanToValueBinder_BankRateProduct
            builder.HasOne(a => a.LoanToValue).WithMany(b => b.BankRateLoanToValueBinders).HasForeignKey(c => c.LoanToValueId).OnDelete(DeleteBehavior.SetNull); // FK_BankRateLoanToValueBinder_LoanToValue
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
