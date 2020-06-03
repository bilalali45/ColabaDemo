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

    // Setting
    
    public partial class SettingMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.ToTable("Setting", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired().HasMaxLength(150);
            builder.Property(x => x.Value).HasColumnName(@"Value").HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(x => x.Remarks).HasColumnName(@"Remarks").HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
            builder.Property(x => x.BranchId).HasColumnName(@"BranchId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.DataTypeId).HasColumnName(@"DataTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.BusinessUnitId).HasColumnName(@"BusinessUnitId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.IsDifferentForBusinessUnit).HasColumnName(@"IsDifferentForBusinessUnit").HasColumnType("bit").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Branch).WithMany(b => b.Settings).HasForeignKey(c => c.BranchId).OnDelete(DeleteBehavior.SetNull); // FK_Setting_Branch
            builder.HasOne(a => a.BusinessUnit).WithMany(b => b.Settings).HasForeignKey(c => c.BusinessUnitId).OnDelete(DeleteBehavior.SetNull); // FK_Setting_BusinessUnit
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>