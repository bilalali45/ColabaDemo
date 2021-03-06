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


namespace Milestone.Data.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Milestone.Entity.Models;

    // Mapping
    
    public partial class MappingMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Mapping>
    {
        public void Configure(EntityTypeBuilder<Mapping> builder)
        {
            builder.ToTable("Mapping", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("bigint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.RmEntityName).HasColumnName(@"RMEntityName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.RmEnittyId).HasColumnName(@"RMEnittyId").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.ExtOriginatorEntityName).HasColumnName(@"ExtOriginatorEntityName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.ExtOriginatorEntityId).HasColumnName(@"ExtOriginatorEntityId").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.ExtOriginatorId).HasColumnName(@"ExtOriginatorId").HasColumnType("smallint").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.ExternalOriginator).WithMany(b => b.Mappings).HasForeignKey(c => c.ExtOriginatorId).OnDelete(DeleteBehavior.SetNull); // FK_Mapping_ExternalOriginator
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
