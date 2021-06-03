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

    // ContactPhoneInfo
    
    public partial class ContactPhoneInfoMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<ContactPhoneInfo>
    {
        public void Configure(EntityTypeBuilder<ContactPhoneInfo> builder)
        {
            builder.ToTable("ContactPhoneInfo", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.TenantId).HasColumnName(@"TenantId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Phone).HasColumnName(@"Phone").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.TypeId).HasColumnName(@"TypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ContactId).HasColumnName(@"ContactId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
            builder.Property(x => x.IsValid).HasColumnName(@"IsValid").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.SurrogateId).HasColumnName(@"SurrogateId").HasColumnType("uniqueidentifier").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Contact).WithMany(b => b.ContactPhoneInfoes).HasForeignKey(c => c.ContactId).OnDelete(DeleteBehavior.SetNull); // FK_ContactPhoneInfo_Contact
            builder.HasOne(a => a.Tenant).WithMany(b => b.ContactPhoneInfoes).HasForeignKey(c => c.TenantId).OnDelete(DeleteBehavior.SetNull); // FK_ContactPhoneInfo_Tenant_Id
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
