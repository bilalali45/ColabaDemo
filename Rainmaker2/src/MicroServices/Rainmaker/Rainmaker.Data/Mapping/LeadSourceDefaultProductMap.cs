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

    // LeadSourceDefaultProduct
    
    public partial class LeadSourceDefaultProductMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<LeadSourceDefaultProduct>
    {
        public void Configure(EntityTypeBuilder<LeadSourceDefaultProduct> builder)
        {
            builder.ToTable("LeadSourceDefaultProduct", "dbo");
            builder.HasKey(x => new { x.LeadSourceId, x.ProductTypeId });

            builder.Property(x => x.LeadSourceId).HasColumnName(@"LeadSourceId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.ProductTypeId).HasColumnName(@"ProductTypeId").HasColumnType("int").IsRequired().ValueGeneratedNever();

            // Foreign keys
            builder.HasOne(a => a.LeadSource).WithMany(b => b.LeadSourceDefaultProducts).HasForeignKey(c => c.LeadSourceId).OnDelete(DeleteBehavior.SetNull); // FK_LeadSourceDefaultProduct_LeadSource
            builder.HasOne(a => a.ProductType).WithMany(b => b.LeadSourceDefaultProducts).HasForeignKey(c => c.ProductTypeId).OnDelete(DeleteBehavior.SetNull); // FK_LeadSourceDefaultProduct_ProductType
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
