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


namespace Notification.Data.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Notification.Entity.Models;

    // TenantSettings
    
    public partial class TenantSettingMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<TenantSetting>
    {
        public void Configure(EntityTypeBuilder<TenantSetting> builder)
        {
            builder.ToTable("TenantSettings", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.TenantId).HasColumnName(@"TenantId").HasColumnType("int").IsRequired();
            builder.Property(x => x.DeliveryModeId).HasColumnName(@"DeliveryModeId").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.NotificationMediumId).HasColumnName(@"NotificationMediumId").HasColumnType("int").IsRequired();
            builder.Property(x => x.NotificationTypeId).HasColumnName(@"NotificationTypeId").HasColumnType("int").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.DeliveryModeEnum).WithMany(b => b.TenantSettings).HasForeignKey(c => c.DeliveryModeId).OnDelete(DeleteBehavior.SetNull); // FK_TenantDeliveryMode_DeliveryModeEnum
            builder.HasOne(a => a.NotificationMedium).WithMany(b => b.TenantSettings).HasForeignKey(c => c.NotificationMediumId).OnDelete(DeleteBehavior.SetNull); // FK_TenantSettings_NotificationMedium
            builder.HasOne(a => a.NotificationType).WithMany(b => b.TenantSettings).HasForeignKey(c => c.NotificationTypeId).OnDelete(DeleteBehavior.SetNull); // FK_TenantDeliveryMode_NotificationType
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>