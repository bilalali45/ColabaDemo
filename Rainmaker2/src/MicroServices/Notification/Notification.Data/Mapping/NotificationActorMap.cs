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

    // NotificationActor
    
    public partial class NotificationActorMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<NotificationActor>
    {
        public void Configure(EntityTypeBuilder<NotificationActor> builder)
        {
            builder.ToTable("NotificationActor", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("bigint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.NotificationObjectId).HasColumnName(@"NotificationObjectId").HasColumnType("bigint").IsRequired(false);
            builder.Property(x => x.ActorId).HasColumnName(@"ActorId").HasColumnType("int").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.NotificationObject).WithOne(b => b.NotificationActor).HasForeignKey<NotificationActor>(c => c.NotificationObjectId).OnDelete(DeleteBehavior.SetNull); // FK_NotificationActor_NotificationObject_Id
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
