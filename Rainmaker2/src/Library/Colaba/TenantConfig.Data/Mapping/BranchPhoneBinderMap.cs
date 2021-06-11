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


namespace TenantConfig.Data.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TenantConfig.Entity.Models;

    // BranchPhoneBinder
    
    public partial class BranchPhoneBinderMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<BranchPhoneBinder>
    {
        public void Configure(EntityTypeBuilder<BranchPhoneBinder> builder)
        {
            builder.ToTable("BranchPhoneBinder", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.BranchId).HasColumnName(@"BranchId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CompanyPhoneInfoId).HasColumnName(@"CompanyPhoneInfoId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.TypeId).HasColumnName(@"TypeId").HasColumnType("int").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.Branch).WithMany(b => b.BranchPhoneBinders).HasForeignKey(c => c.BranchId).OnDelete(DeleteBehavior.SetNull); // FK_BranchPhoneBinder_Branch_Id
            builder.HasOne(a => a.CompanyPhoneInfo).WithMany(b => b.BranchPhoneBinders).HasForeignKey(c => c.CompanyPhoneInfoId).OnDelete(DeleteBehavior.SetNull); // FK_BranchPhoneBinder_CompanyPhoneInfo_Id
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>