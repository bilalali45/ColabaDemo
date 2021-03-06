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

    // HierarchyLevel
    
    public partial class HierarchyLevelMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<HierarchyLevel>
    {
        public void Configure(EntityTypeBuilder<HierarchyLevel> builder)
        {
            builder.ToTable("HierarchyLevel", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.LevelCode).HasColumnName(@"LevelCode").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.TenantId).HasColumnName(@"TenantId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(50);

            // Foreign keys
            builder.HasOne(a => a.Tenant).WithMany(b => b.HierarchyLevels).HasForeignKey(c => c.TenantId).OnDelete(DeleteBehavior.SetNull); // FK_HierarchyLevel_Tenant_Id
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
