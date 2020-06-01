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

    // VendorCustomerBinder
    
    public partial class VendorCustomerBinderMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<VendorCustomerBinder>
    {
        public void Configure(EntityTypeBuilder<VendorCustomerBinder> builder)
        {
            builder.ToTable("VendorCustomerBinder", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.VendorId).HasColumnName(@"VendorId").HasColumnType("int").IsRequired();
            builder.Property(x => x.CustomerId).HasColumnName(@"CustomerId").HasColumnType("int").IsRequired();
            builder.Property(x => x.OwnTypeId).HasColumnName(@"OwnTypeId").HasColumnType("int").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.Customer).WithMany(b => b.VendorCustomerBinders).HasForeignKey(c => c.CustomerId).OnDelete(DeleteBehavior.SetNull); // FK_VendorCustomerBinder_Customer
            builder.HasOne(a => a.OwnType).WithMany(b => b.VendorCustomerBinders).HasForeignKey(c => c.OwnTypeId).OnDelete(DeleteBehavior.SetNull); // FK_VendorCustomerBinder_OwnType
            builder.HasOne(a => a.Vendor).WithMany(b => b.VendorCustomerBinders).HasForeignKey(c => c.VendorId).OnDelete(DeleteBehavior.SetNull); // FK_VendorCustomerBinder_Vendor
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
