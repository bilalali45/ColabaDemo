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

    // Template
    
    public partial class TemplateMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.ToTable("Template", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired().HasMaxLength(150);
            builder.Property(x => x.FromAddress).HasColumnName(@"FromAddress").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.FromEmailAccountId).HasColumnName(@"FromEmailAccountId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ToAddresses).HasColumnName(@"ToAddresses").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.OtherToAddresses).HasColumnName(@"OtherToAddresses").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.CcAddresses).HasColumnName(@"CcAddresses").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BccAddresses).HasColumnName(@"BccAddresses").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.OtherBccEmailAddresses).HasColumnName(@"OtherBccEmailAddresses").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.Subject).HasColumnName(@"Subject").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.VisibleForId).HasColumnName(@"VisibleForId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.TemplateTypeId).HasColumnName(@"TemplateTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.TemplateBody).HasColumnName(@"TemplateBody").HasColumnType("ntext").IsRequired(false);
            builder.Property(x => x.DisplayOrder).HasColumnName(@"DisplayOrder").HasColumnType("int").IsRequired();
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.IsDefault).HasColumnName(@"IsDefault").HasColumnType("bit").IsRequired();
            builder.Property(x => x.IsSystem).HasColumnName(@"IsSystem").HasColumnType("bit").IsRequired();
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ModifiedOnUtc).HasColumnName(@"ModifiedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.RefEntityType).HasColumnName(@"RefEntityType").HasColumnType("int").IsRequired();
            builder.Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
            builder.Property(x => x.Utm).HasColumnName(@"Utm").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);

            // Foreign keys
            builder.HasOne(a => a.EmailAccount).WithMany(b => b.Templates).HasForeignKey(c => c.FromEmailAccountId).OnDelete(DeleteBehavior.SetNull); // FK_Template_EmailAccount
            builder.HasOne(a => a.TemplateType).WithMany(b => b.Templates).HasForeignKey(c => c.TemplateTypeId).OnDelete(DeleteBehavior.SetNull); // FK_Template_TemplateType
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>