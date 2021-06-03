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

    // TenantUrl
    
    public partial class TenantUrlMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<TenantUrl>
    {
        public void Configure(EntityTypeBuilder<TenantUrl> builder)
        {
            builder.ToTable("TenantUrl", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Url).HasColumnName(@"Url").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(100);
            builder.Property(x => x.TenantId).HasColumnName(@"TenantId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.BranchId).HasColumnName(@"BranchId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.TypeId).HasColumnName(@"TypeId").HasColumnType("int").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Branch).WithMany(b => b.TenantUrls).HasForeignKey(c => c.BranchId).OnDelete(DeleteBehavior.SetNull); // FK_WebUrl_Branch_Id
            builder.HasOne(a => a.Tenant).WithMany(b => b.TenantUrls).HasForeignKey(c => c.TenantId).OnDelete(DeleteBehavior.SetNull); // FK_WebUrl_Tenant_Id
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
