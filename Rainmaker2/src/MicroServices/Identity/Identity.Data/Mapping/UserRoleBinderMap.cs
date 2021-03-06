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


namespace Identity.Data.Mapping
{
    using Identity.Entity.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    // UserRoleBinder
    
    public partial class UserRoleBinderMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<UserRoleBinder>
    {
        public void Configure(EntityTypeBuilder<UserRoleBinder> builder)
        {
            builder.ToTable("UserRoleBinder", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType("int").IsRequired();
            builder.Property(x => x.RoleId).HasColumnName(@"RoleId").HasColumnType("int").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Role).WithMany(b => b.UserRoleBinders).HasForeignKey(c => c.RoleId).OnDelete(DeleteBehavior.SetNull); // FK_UserRoleBinder_Role_Id
            builder.HasOne(a => a.User).WithMany(b => b.UserRoleBinders).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.SetNull); // FK_UserRoleBinder_User_Id
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
