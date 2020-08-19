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

    // PasswordPolicy
    
    public partial class PasswordPolicyMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<PasswordPolicy>
    {
        public void Configure(EntityTypeBuilder<PasswordPolicy> builder)
        {
            builder.ToTable("PasswordPolicy", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.TenantId).HasColumnName(@"TenantId").HasColumnType("int").IsRequired();
            builder.Property(x => x.MinimumLength).HasColumnName(@"MinimumLength").HasColumnType("int").IsRequired();
            builder.Property(x => x.MaximumLength).HasColumnName(@"MaximumLength").HasColumnType("int").IsRequired();
            builder.Property(x => x.PasswordExpiryInDays).HasColumnName(@"PasswordExpiryInDays").HasColumnType("int").IsRequired();
            builder.Property(x => x.PasswordHistoryCount).HasColumnName(@"PasswordHistoryCount").HasColumnType("int").IsRequired();
            builder.Property(x => x.ViewPassword).HasColumnName(@"ViewPassword").HasColumnType("bit").IsRequired();
            builder.Property(x => x.RecaptchaEnabled).HasColumnName(@"RecaptchaEnabled").HasColumnType("bit").IsRequired();
            builder.Property(x => x.RecaptchaClientKey).HasColumnName(@"RecaptchaClientKey").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);
            builder.Property(x => x.RecaptchaServerKey).HasColumnName(@"RecaptchaServerKey").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);
            builder.Property(x => x.IncorrectPasswordCount).HasColumnName(@"IncorrectPasswordCount").HasColumnType("int").IsRequired();
            builder.Property(x => x.Enable2Fa).HasColumnName(@"Enable2FA").HasColumnType("bit").IsRequired();
            builder.Property(x => x.AccountLockDurationInMinutes).HasColumnName(@"AccountLockDurationInMinutes").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ForgotTokenExpiryInMinutes).HasColumnName(@"ForgotTokenExpiryInMinutes").HasColumnType("int").IsRequired(false);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
