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

    // PaymentOn
    
    public partial class PaymentOnMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<PaymentOn>
    {
        public void Configure(EntityTypeBuilder<PaymentOn> builder)
        {
            builder.ToTable("PaymentOn", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Month).HasColumnName(@"Month").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.NoOfMonths).HasColumnName(@"NoOfMonths").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.PropertyTaxId).HasColumnName(@"PropertyTaxId").HasColumnType("int").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.PropertyTax).WithMany(b => b.PaymentOns).HasForeignKey(c => c.PropertyTaxId).OnDelete(DeleteBehavior.SetNull); // FK_PaymentOn_PropertyTax
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
