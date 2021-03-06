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

    // DeviceType
    
    public partial class Vortex_DeviceTypeMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Vortex_DeviceType>
    {
        public void Configure(EntityTypeBuilder<Vortex_DeviceType> builder)
        {
            builder.ToTable("DeviceType", "Vortex");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("smallint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
