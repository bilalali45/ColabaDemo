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

    // NotificationRecepientStatusLog

    public partial class NotificationRecepientStatusLogMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<NotificationRecepientStatusLog>
    {
        public void Configure(EntityTypeBuilder<NotificationRecepientStatusLog> builder)
        {
            builder.ToTable("NotificationRecepientStatusLog", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("bigint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").HasColumnType("datetime2").IsRequired();
            builder.Property(x => x.NotificationRecepientId).HasColumnName(@"NotificationRecepientId").HasColumnType("bigint").IsRequired();
            builder.Property(x => x.StatusId).HasColumnName(@"StatusId").HasColumnType("tinyint").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.NotificationRecepient).WithMany(b => b.NotificationRecepientStatusLogs).HasForeignKey(c => c.NotificationRecepientId).OnDelete(DeleteBehavior.SetNull); // FK_NotificationRecepientStatusLog_NotificationRecepient_Id
            builder.HasOne(a => a.StatusListEnum).WithMany(b => b.NotificationRecepientStatusLogs).HasForeignKey(c => c.StatusId).OnDelete(DeleteBehavior.SetNull); // FK_NotificationRecepientStatusLog_StatusListEnum_Id
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
