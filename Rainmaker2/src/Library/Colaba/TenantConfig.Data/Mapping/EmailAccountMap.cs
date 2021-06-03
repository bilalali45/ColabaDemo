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

    // EmailAccount
    
    public partial class EmailAccountMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<EmailAccount>
    {
        public void Configure(EntityTypeBuilder<EmailAccount> builder)
        {
            builder.ToTable("EmailAccount", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Email).HasColumnName(@"Email").HasColumnType("nvarchar").IsRequired().HasMaxLength(255);
            builder.Property(x => x.DisplayName).HasColumnName(@"DisplayName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(255);
            builder.Property(x => x.Host).HasColumnName(@"Host").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(255);
            builder.Property(x => x.Port).HasColumnName(@"Port").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Username).HasColumnName(@"Username").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(255);
            builder.Property(x => x.Password).HasColumnName(@"Password").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(255);
            builder.Property(x => x.EnableSsl).HasColumnName(@"EnableSsl").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.UseDefaultCredentials).HasColumnName(@"UseDefaultCredentials").HasColumnType("bit").IsRequired();
            builder.Property(x => x.UseReplyTo).HasColumnName(@"UseReplyTo").HasColumnType("bit").IsRequired();
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ModifiedOnUtc).HasColumnName(@"ModifiedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.TenantId).HasColumnName(@"TenantId").HasColumnType("int").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.Tenant).WithMany(b => b.EmailAccounts).HasForeignKey(c => c.TenantId).OnDelete(DeleteBehavior.SetNull); // FK_EmailAccount
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
