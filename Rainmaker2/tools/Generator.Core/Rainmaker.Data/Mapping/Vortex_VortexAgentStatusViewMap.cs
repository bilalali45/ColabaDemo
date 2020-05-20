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

    // VortexAgentStatusView
    
    public partial class Vortex_VortexAgentStatusViewMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Vortex_VortexAgentStatusView>
    {
        public void Configure(EntityTypeBuilder<Vortex_VortexAgentStatusView> builder)
        {
            builder.ToTable("VortexAgentStatusView", "Vortex");
            builder.HasKey(x => new { x.Id, x.StatusId, x.DateTimeStamp, x.UserName, x.StatusName, x.TypeId, x.IsActive, x.UserAgent });

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("bigint").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.StatusId).HasColumnName(@"StatusId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.DateTimeStamp).HasColumnName(@"DateTimeStamp").HasColumnType("datetime2").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.UserName).HasColumnName(@"UserName").HasColumnType("nvarchar").IsRequired().HasMaxLength(256).ValueGeneratedNever();
            builder.Property(x => x.DeviceType).HasColumnName(@"DeviceType").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.StatusName).HasColumnName(@"StatusName").HasColumnType("nvarchar").IsRequired().HasMaxLength(100).ValueGeneratedNever();
            builder.Property(x => x.TypeId).HasColumnName(@"TypeId").HasColumnType("smallint").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.IpAddress).HasColumnName(@"IPAddress").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.VortexVersion).HasColumnName(@"VortexVersion").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.UserAgent).HasColumnName(@"UserAgent").HasColumnType("nvarchar(max)").IsRequired().ValueGeneratedNever();
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
