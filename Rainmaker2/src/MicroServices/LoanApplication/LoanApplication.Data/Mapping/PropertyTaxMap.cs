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

    // PropertyTax
    
    public partial class PropertyTaxMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<PropertyTax>
    {
        public void Configure(EntityTypeBuilder<PropertyTax> builder)
        {
            builder.ToTable("PropertyTax", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.FeeNumber).HasColumnName(@"FeeNumber").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.FeeTypeId).HasColumnName(@"FeeTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.PaidById).HasColumnName(@"PaidById").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.FeeBlockId).HasColumnName(@"FeeBlockId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.StateId).HasColumnName(@"StateId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.EscrowEntityTypeId).HasColumnName(@"EscrowEntityTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.RoundTypeId).HasColumnName(@"RoundTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.CalcTypeId).HasColumnName(@"CalcTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.FormulaId).HasColumnName(@"FormulaId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Value).HasColumnName(@"Value").HasColumnType("decimal(18,3)").IsRequired();
            builder.Property(x => x.DisplayOrder).HasColumnName(@"DisplayOrder").HasColumnType("int").IsRequired();
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.IsDefault).HasColumnName(@"IsDefault").HasColumnType("bit").IsRequired();
            builder.Property(x => x.IsSystem).HasColumnName(@"IsSystem").HasColumnType("bit").IsRequired();
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ModifiedOnUtc).HasColumnName(@"ModifiedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
            builder.Property(x => x.CalcBaseOnId).HasColumnName(@"CalcBaseOnId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.RangSetId).HasColumnName(@"RangSetId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.TenantId).HasColumnName(@"TenantId").HasColumnType("int").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.EscrowEntityType).WithMany(b => b.PropertyTaxes).HasForeignKey(c => c.EscrowEntityTypeId).OnDelete(DeleteBehavior.SetNull); // FK_PropertyTax_EscrowEntityType
            builder.HasOne(a => a.PaidBy).WithMany(b => b.PropertyTaxes).HasForeignKey(c => c.PaidById).OnDelete(DeleteBehavior.SetNull); // FK_PropertyTax_PaidBy
            builder.HasOne(a => a.State).WithMany(b => b.PropertyTaxes).HasForeignKey(c => c.StateId).OnDelete(DeleteBehavior.SetNull); // FK_PropertyTax_State
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
