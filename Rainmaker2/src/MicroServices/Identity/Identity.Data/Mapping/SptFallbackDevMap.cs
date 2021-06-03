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


namespace Identity.Data.Mapping
{
    using Identity.Entity.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    // spt_fallback_dev
    
    public partial class SptFallbackDevMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<SptFallbackDev>
    {
        public void Configure(EntityTypeBuilder<SptFallbackDev> builder)
        {
            builder.ToTable("spt_fallback_dev", "dbo");
            builder.HasKey(x => new { x.XserverName, x.XdttmIns, x.XdttmLastInsUpd, x.Low, x.High, x.Status, x.Name, x.Phyname });

            builder.Property(x => x.XserverName).HasColumnName(@"xserver_name").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(30).ValueGeneratedNever();
            builder.Property(x => x.XdttmIns).HasColumnName(@"xdttm_ins").HasColumnType("datetime").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.XdttmLastInsUpd).HasColumnName(@"xdttm_last_ins_upd").HasColumnType("datetime").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.XfallbackLow).HasColumnName(@"xfallback_low").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.XfallbackDrive).HasColumnName(@"xfallback_drive").HasColumnType("char").IsRequired(false).IsFixedLength().IsUnicode(false).HasMaxLength(2);
            builder.Property(x => x.Low).HasColumnName(@"low").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.High).HasColumnName(@"high").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Status).HasColumnName(@"status").HasColumnType("smallint").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Name).HasColumnName(@"name").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(30).ValueGeneratedNever();
            builder.Property(x => x.Phyname).HasColumnName(@"phyname").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(127).ValueGeneratedNever();
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
