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

    // NotificationObjectStatusLog
    
    public partial class NotificationObjectStatusLogMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<NotificationObjectStatusLog>
    {
        public void Configure(EntityTypeBuilder<NotificationObjectStatusLog> builder)
        {
            builder.ToTable("NotificationObjectStatusLog", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("bigint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").HasColumnType("datetime2").IsRequired();
            builder.Property(x => x.StatusId).HasColumnName(@"StatusId").HasColumnType("tinyint").IsRequired();
            builder.Property(x => x.NotificationObjectId).HasColumnName(@"NotificationObjectId").HasColumnType("bigint").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.NotificationObject).WithMany(b => b.NotificationObjectStatusLogs).HasForeignKey(c => c.NotificationObjectId).OnDelete(DeleteBehavior.SetNull); // FK_NotificationObjectStatusLog_NotificationObject_Id
            builder.HasOne(a => a.StatusListEnum).WithMany(b => b.NotificationObjectStatusLogs).HasForeignKey(c => c.StatusId).OnDelete(DeleteBehavior.SetNull); // FK_NotificationObjectStatusLog_StatusListEnum_Id
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
