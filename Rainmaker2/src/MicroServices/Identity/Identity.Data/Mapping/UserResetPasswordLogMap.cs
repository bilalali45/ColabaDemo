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

    // UserResetPasswordLog
    
    public partial class UserResetPasswordLogMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<UserResetPasswordLog>
    {
        public void Configure(EntityTypeBuilder<UserResetPasswordLog> builder)
        {
            builder.ToTable("UserResetPasswordLog", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType("int").IsRequired();
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.TenantId).HasColumnName(@"TenantId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ChangeTypeId).HasColumnName(@"ChangeTypeId").HasColumnType("int").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.User).WithMany(b => b.UserResetPasswordLogs).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.SetNull); // FK_UserResetPasswordLog_User_Id
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>