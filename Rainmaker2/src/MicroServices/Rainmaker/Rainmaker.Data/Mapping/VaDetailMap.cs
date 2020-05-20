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

    // VaDetails
    
    public partial class VaDetailMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<VaDetail>
    {
        public void Configure(EntityTypeBuilder<VaDetail> builder)
        {
            builder.ToTable("VaDetails", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.LoanApplicationId).HasColumnName(@"LoanApplicationId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.BorrowerId).HasColumnName(@"BorrowerId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.MilitaryBranchId).HasColumnName(@"MilitaryBranchId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.MilitaryAffiliationId).HasColumnName(@"MilitaryAffiliationId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.MilitaryStatusId).HasColumnName(@"MilitaryStatusId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ExpirationDateUtc).HasColumnName(@"ExpirationDateUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.VaOccupancyId).HasColumnName(@"VaOccupancyId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.VaFirstName).HasColumnName(@"VaFirstName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.VaLastName).HasColumnName(@"VaLastName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.RelationContactId).HasColumnName(@"RelationContactId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.IsReceivingDisabilityIncome).HasColumnName(@"IsReceivingDisabilityIncome").HasColumnType("bit").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.Borrower).WithMany(b => b.VaDetails).HasForeignKey(c => c.BorrowerId).OnDelete(DeleteBehavior.SetNull); // FK_VaDetails_Borrower
            builder.HasOne(a => a.LoanApplication).WithMany(b => b.VaDetails).HasForeignKey(c => c.LoanApplicationId).OnDelete(DeleteBehavior.SetNull); // FK_VaDetails_LoanApplication
            builder.HasOne(a => a.MilitaryAffiliation).WithMany(b => b.VaDetails).HasForeignKey(c => c.MilitaryAffiliationId).OnDelete(DeleteBehavior.SetNull); // FK_VaDetails_MilitaryAffiliation
            builder.HasOne(a => a.MilitaryBranch).WithMany(b => b.VaDetails).HasForeignKey(c => c.MilitaryBranchId).OnDelete(DeleteBehavior.SetNull); // FK_VaDetails_MilitaryBranch
            builder.HasOne(a => a.MilitaryStatusList).WithMany(b => b.VaDetails).HasForeignKey(c => c.MilitaryStatusId).OnDelete(DeleteBehavior.SetNull); // FK_VaDetails_MilitaryStatusList
            builder.HasOne(a => a.VaOccupancy).WithMany(b => b.VaDetails).HasForeignKey(c => c.VaOccupancyId).OnDelete(DeleteBehavior.SetNull); // FK_VaDetails_VaOccupancy
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
