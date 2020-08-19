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

    // ClientInfo
    
    public partial class Vortex_ClientInfoMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Vortex_ClientInfo>
    {
        public void Configure(EntityTypeBuilder<Vortex_ClientInfo> builder)
        {
            builder.ToTable("ClientInfo", "Vortex");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("bigint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.UserSessionid).HasColumnName(@"UserSessionid").HasColumnType("bigint").IsRequired();
            builder.Property(x => x.VortexVersion).HasColumnName(@"VortexVersion").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.IpAddress).HasColumnName(@"IPAddress").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.Country).HasColumnName(@"Country").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.State).HasColumnName(@"State").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.City).HasColumnName(@"City").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.NetworkInterfaces).HasColumnName(@"NetworkInterfaces").HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(x => x.OsPlatForm).HasColumnName(@"OsPlatForm").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.OsRelease).HasColumnName(@"OsRelease").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.OsType).HasColumnName(@"OsType").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.Arch).HasColumnName(@"Arch").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.CpUs).HasColumnName(@"CPUs").HasColumnType("varchar(max)").IsRequired(false).IsUnicode(false);
            builder.Property(x => x.HomeDir).HasColumnName(@"HomeDir").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.HostName).HasColumnName(@"HostName").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.UserInfo).HasColumnName(@"UserInfo").HasColumnType("varchar(max)").IsRequired(false).IsUnicode(false);
            builder.Property(x => x.UpTime).HasColumnName(@"UpTime").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(50);

            // Foreign keys
            builder.HasOne(a => a.Vortex_UserSessionLog).WithMany(b => b.Vortex_ClientInfoes).HasForeignKey(c => c.UserSessionid).OnDelete(DeleteBehavior.SetNull); // FK_ClientInfo_UserSessionLog_Id
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
