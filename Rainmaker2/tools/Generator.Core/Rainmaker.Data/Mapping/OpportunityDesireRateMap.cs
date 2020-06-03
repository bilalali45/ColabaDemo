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

    // OpportunityDesireRate
    
    public partial class OpportunityDesireRateMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<OpportunityDesireRate>
    {
        public void Configure(EntityTypeBuilder<OpportunityDesireRate> builder)
        {
            builder.ToTable("OpportunityDesireRate", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.OpportunityId).HasColumnName(@"OpportunityId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ProductTypeId).HasColumnName(@"ProductTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Rate).HasColumnName(@"Rate").HasColumnType("decimal(18,3)").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.Opportunity).WithMany(b => b.OpportunityDesireRates).HasForeignKey(c => c.OpportunityId).OnDelete(DeleteBehavior.SetNull); // FK_OpportunityDesireRate_Opportunity
            builder.HasOne(a => a.ProductType).WithMany(b => b.OpportunityDesireRates).HasForeignKey(c => c.ProductTypeId).OnDelete(DeleteBehavior.SetNull); // FK_OpportunityDesireRate_ProductType
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>