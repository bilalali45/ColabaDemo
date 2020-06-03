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

    // UserSessionLog
    
    public partial class Vortex_UserSessionLogMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Vortex_UserSessionLog>
    {
        public void Configure(EntityTypeBuilder<Vortex_UserSessionLog> builder)
        {
            builder.ToTable("UserSessionLog", "Vortex");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("bigint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.UserAgent).HasColumnName(@"UserAgent").HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(x => x.UserProfileId).HasColumnName(@"UserProfileId").HasColumnType("int").IsRequired();
            builder.Property(x => x.TypeId).HasColumnName(@"TypeId").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime2").IsRequired();
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            builder.Property(x => x.IsOnCall).HasColumnName(@"IsOnCall").HasColumnType("bit").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.UserProfile).WithMany(b => b.Vortex_UserSessionLogs).HasForeignKey(c => c.UserProfileId).OnDelete(DeleteBehavior.SetNull); // FK_UserSessionLog_UserProfile_Id
            builder.HasOne(a => a.Vortex_DeviceType).WithMany(b => b.Vortex_UserSessionLogs).HasForeignKey(c => c.TypeId).OnDelete(DeleteBehavior.SetNull); // FK_UserSessionLog_DeviceType_Id
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>