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

    // LocaleStringResource
    
    public partial class LocaleStringResourceMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<LocaleStringResource>
    {
        public void Configure(EntityTypeBuilder<LocaleStringResource> builder)
        {
            builder.ToTable("LocaleStringResource", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.LanguageId).HasColumnName(@"LanguageId").HasColumnType("int").IsRequired();
            builder.Property(x => x.ResourceName).HasColumnName(@"ResourceName").HasColumnType("nvarchar").IsRequired().HasMaxLength(256);
            builder.Property(x => x.ResourceValue).HasColumnName(@"ResourceValue").HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(x => x.Location).HasColumnName(@"Location").HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(x => x.ResourceForId).HasColumnName(@"ResourceForId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.BusinessUnitId).HasColumnName(@"BusinessUnitId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType("nvarchar").IsRequired().HasMaxLength(256);
            builder.Property(x => x.IsDifferentForBusinessUnit).HasColumnName(@"IsDifferentForBusinessUnit").HasColumnType("bit").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.BusinessUnit).WithMany(b => b.LocaleStringResources).HasForeignKey(c => c.BusinessUnitId).OnDelete(DeleteBehavior.SetNull); // FK_LocaleStringResource_BusinessUnit
            builder.HasOne(a => a.Language).WithMany(b => b.LocaleStringResources).HasForeignKey(c => c.LanguageId).OnDelete(DeleteBehavior.SetNull); // FK_LocaleStringResource_Language
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
