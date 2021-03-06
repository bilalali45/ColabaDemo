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

    // ContactEmailInfo
    
    public partial class ContactEmailInfoMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<ContactEmailInfo>
    {
        public void Configure(EntityTypeBuilder<ContactEmailInfo> builder)
        {
            builder.ToTable("ContactEmailInfo", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Email).HasColumnName(@"Email").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.Type).HasColumnName(@"Type").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.IsPrimary).HasColumnName(@"IsPrimary").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.ContactId).HasColumnName(@"ContactId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
            builder.Property(x => x.ValidityId).HasColumnName(@"ValidityId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.UseForId).HasColumnName(@"UseForId").HasColumnType("int").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.Contact).WithMany(b => b.ContactEmailInfoes).HasForeignKey(c => c.ContactId).OnDelete(DeleteBehavior.SetNull); // FK_ContactEmailInfo_Contact
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
