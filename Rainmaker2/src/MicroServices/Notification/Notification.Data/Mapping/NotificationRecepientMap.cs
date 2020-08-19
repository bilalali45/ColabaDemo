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

    // NotificationRecepient
    
    public partial class NotificationRecepientMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<NotificationRecepient>
    {
        public void Configure(EntityTypeBuilder<NotificationRecepient> builder)
        {
            builder.ToTable("NotificationRecepient", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("bigint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.NotificationObjectId).HasColumnName(@"NotificationObjectId").HasColumnType("bigint").IsRequired(false);
            builder.Property(x => x.RecipientId).HasColumnName(@"RecipientId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.StatusId).HasColumnName(@"StatusId").HasColumnType("tinyint").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.NotificationObject).WithMany(b => b.NotificationRecepients).HasForeignKey(c => c.NotificationObjectId).OnDelete(DeleteBehavior.SetNull); // FK_NotificationRecepient_NotificationObject_Id
            builder.HasOne(a => a.StatusListEnum).WithMany(b => b.NotificationRecepients).HasForeignKey(c => c.StatusId).OnDelete(DeleteBehavior.SetNull); // FK_NotificationRecepient_StatusListEnum_Id
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
