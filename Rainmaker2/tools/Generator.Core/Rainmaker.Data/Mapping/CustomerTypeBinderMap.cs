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

    // CustomerTypeBinder
    
    public partial class CustomerTypeBinderMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<CustomerTypeBinder>
    {
        public void Configure(EntityTypeBuilder<CustomerTypeBinder> builder)
        {
            builder.ToTable("CustomerTypeBinder", "dbo");
            builder.HasKey(x => new { x.CustomerId, x.CustomerTypeId });

            builder.Property(x => x.CustomerId).HasColumnName(@"CustomerId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.CustomerTypeId).HasColumnName(@"CustomerTypeId").HasColumnType("int").IsRequired().ValueGeneratedNever();

            // Foreign keys
            builder.HasOne(a => a.Customer).WithMany(b => b.CustomerTypeBinders).HasForeignKey(c => c.CustomerId).OnDelete(DeleteBehavior.SetNull); // FK_CustomerTypeBinder_Customer
            builder.HasOne(a => a.CustomerType).WithMany(b => b.CustomerTypeBinders).HasForeignKey(c => c.CustomerTypeId).OnDelete(DeleteBehavior.SetNull); // FK_CustomerTypeBinder_CustomerType
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>