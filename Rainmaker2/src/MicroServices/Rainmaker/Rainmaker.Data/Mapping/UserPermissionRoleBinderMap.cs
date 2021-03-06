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

    // UserPermissionRoleBinder
    
    public partial class UserPermissionRoleBinderMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<UserPermissionRoleBinder>
    {
        public void Configure(EntityTypeBuilder<UserPermissionRoleBinder> builder)
        {
            builder.ToTable("UserPermissionRoleBinder", "dbo");
            builder.HasKey(x => new { x.RoleId, x.PermissionId });

            builder.Property(x => x.RoleId).HasColumnName(@"RoleId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.PermissionId).HasColumnName(@"PermissionId").HasColumnType("int").IsRequired().ValueGeneratedNever();

            // Foreign keys
            builder.HasOne(a => a.UserPermission).WithMany(b => b.UserPermissionRoleBinders).HasForeignKey(c => c.PermissionId); // FK_UserPermissionRoleBinder_UserPermission
            builder.HasOne(a => a.UserRole).WithMany(b => b.UserPermissionRoleBinders).HasForeignKey(c => c.RoleId); // FK_UserPermissionRoleBinder_UserRole
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
