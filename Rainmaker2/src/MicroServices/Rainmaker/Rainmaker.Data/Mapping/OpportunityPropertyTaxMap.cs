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

    // OpportunityPropertyTax
    
    public partial class OpportunityPropertyTaxMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<OpportunityPropertyTax>
    {
        public void Configure(EntityTypeBuilder<OpportunityPropertyTax> builder)
        {
            builder.ToTable("OpportunityPropertyTax", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.PaidById).HasColumnName(@"PaidById").HasColumnType("int").IsRequired();
            builder.Property(x => x.OpportunityId).HasColumnName(@"OpportunityId").HasColumnType("int").IsRequired();
            builder.Property(x => x.LoanRequestId).HasColumnName(@"LoanRequestId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Value).HasColumnName(@"Value").HasColumnType("decimal(18,3)").IsRequired();
            builder.Property(x => x.EscrowMonth).HasColumnName(@"EscrowMonth").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.EscrowEntityTypeId).HasColumnName(@"EscrowEntityTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.PrePaidMonth).HasColumnName(@"PrePaidMonth").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.PrePaid).HasColumnName(@"PrePaid").HasColumnType("decimal(18,3)").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.EscrowEntityType).WithMany(b => b.OpportunityPropertyTaxes).HasForeignKey(c => c.EscrowEntityTypeId).OnDelete(DeleteBehavior.SetNull); // FK_OpportunityPropertyTax_EscrowEntityType
            builder.HasOne(a => a.LoanRequest).WithMany(b => b.OpportunityPropertyTaxes).HasForeignKey(c => c.LoanRequestId).OnDelete(DeleteBehavior.SetNull); // FK_OpportunityPropertyTax_LoanRequest
            builder.HasOne(a => a.Opportunity).WithMany(b => b.OpportunityPropertyTaxes).HasForeignKey(c => c.OpportunityId).OnDelete(DeleteBehavior.SetNull); // FK_OpportunityPropertyTax_Opportunity
            builder.HasOne(a => a.PaidBy).WithMany(b => b.OpportunityPropertyTaxes).HasForeignKey(c => c.PaidById).OnDelete(DeleteBehavior.SetNull); // FK_OpportunityPropertyTax_PaidBy
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
