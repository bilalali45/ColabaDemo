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

    // UserResetPasswordKey
    
    public partial class UserResetPasswordKeyMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<UserResetPasswordKey>
    {
        public void Configure(EntityTypeBuilder<UserResetPasswordKey> builder)
        {
            builder.ToTable("UserResetPasswordKey", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType("int").IsRequired();
            builder.Property(x => x.PasswordKey).HasColumnName(@"PasswordKey").HasColumnType("nvarchar").IsRequired().HasMaxLength(500);
            builder.Property(x => x.ExpireOnUtc).HasColumnName(@"ExpireOnUtc").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.ResetOnUtc).HasColumnName(@"ResetOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            builder.Property(x => x.TenantId).HasColumnName(@"TenantId").HasColumnType("int").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.User).WithMany(b => b.UserResetPasswordKeys).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.SetNull); // FK_UserResetPasswordKey_User_Id
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>