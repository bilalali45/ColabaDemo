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

    // ActivityTenantBinder
    
    public partial class ActivityTenantBinderMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<ActivityTenantBinder>
    {
        public void Configure(EntityTypeBuilder<ActivityTenantBinder> builder)
        {
            builder.ToTable("ActivityTenantBinder", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.TenantId).HasColumnName(@"TenantId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ActivityId).HasColumnName(@"ActivityId").HasColumnType("int").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.Activity).WithMany(b => b.ActivityTenantBinders).HasForeignKey(c => c.ActivityId).OnDelete(DeleteBehavior.SetNull); // FK_ActivityTenantBinder_Activity_Id
            builder.HasOne(a => a.Tenant).WithMany(b => b.ActivityTenantBinders).HasForeignKey(c => c.TenantId).OnDelete(DeleteBehavior.SetNull); // FK_ActivityTenantBinder_Tenant_Id
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
