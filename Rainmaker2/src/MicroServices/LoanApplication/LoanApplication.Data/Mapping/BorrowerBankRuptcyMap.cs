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

    // BorrowerBankRuptcy
    
    public partial class BorrowerBankRuptcyMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<BorrowerBankRuptcy>
    {
        public void Configure(EntityTypeBuilder<BorrowerBankRuptcy> builder)
        {
            builder.ToTable("BorrowerBankRuptcy", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.BorrowerId).HasColumnName(@"BorrowerId").HasColumnType("int").IsRequired();
            builder.Property(x => x.BankRuptcyId).HasColumnName(@"BankRuptcyId").HasColumnType("int").IsRequired();
            builder.Property(x => x.TenantId).HasColumnName(@"TenantId").HasColumnType("int").IsRequired();
            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();

            // Foreign keys
            builder.HasOne(a => a.BankRuptcy).WithMany(b => b.BorrowerBankRuptcies).HasForeignKey(c => c.BankRuptcyId).OnDelete(DeleteBehavior.SetNull); // FK_BorrowerBankRuptcy_BankRuptcy
            builder.HasOne(a => a.Borrower).WithMany(b => b.BorrowerBankRuptcies).HasForeignKey(c => c.BorrowerId).OnDelete(DeleteBehavior.SetNull); // FK_BorrowerBankRuptcy_Borrower
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>