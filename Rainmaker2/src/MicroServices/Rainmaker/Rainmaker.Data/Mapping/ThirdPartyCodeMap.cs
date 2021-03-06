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

    // ThirdPartyCode
    
    public partial class ThirdPartyCodeMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<ThirdPartyCode>
    {
        public void Configure(EntityTypeBuilder<ThirdPartyCode> builder)
        {
            builder.ToTable("ThirdPartyCode", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.ThirdPartyId).HasColumnName(@"ThirdPartyId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ElementName).HasColumnName(@"ElementName").HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(x => x.Code).HasColumnName(@"Code").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(300);
            builder.Property(x => x.EntityRefTypeId).HasColumnName(@"EntityRefTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.EntityRefId).HasColumnName(@"EntityRefId").HasColumnType("int").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.EntityType).WithMany(b => b.ThirdPartyCodes).HasForeignKey(c => c.EntityRefTypeId).OnDelete(DeleteBehavior.SetNull); // FK_ThirdPartyCode_EntityType
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
