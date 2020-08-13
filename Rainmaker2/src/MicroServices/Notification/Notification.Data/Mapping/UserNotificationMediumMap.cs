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

    // UserNotificationMedium
    
    public partial class UserNotificationMediumMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<UserNotificationMedium>
    {
        public void Configure(EntityTypeBuilder<UserNotificationMedium> builder)
        {
            builder.ToTable("UserNotificationMedium", "dbo");
            builder.HasKey(x => new { x.UserId, x.NotificationMediumId });

            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.NotificationMediumId).HasColumnName(@"NotificationMediumId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.NotificationTypeId).HasColumnName(@"NotificationTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.NotificationMedium).WithMany(b => b.UserNotificationMediums).HasForeignKey(c => c.NotificationMediumId).OnDelete(DeleteBehavior.SetNull); // FK_UserNotificationMedium_NotificationMedium_Id
            builder.HasOne(a => a.NotificationType).WithMany(b => b.UserNotificationMediums).HasForeignKey(c => c.NotificationTypeId).OnDelete(DeleteBehavior.SetNull); // FK_UserNotificationMedium_NotificationType_Id
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
