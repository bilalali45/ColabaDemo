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

    // NotificationTo
    
    public partial class NotificationToMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<NotificationTo>
    {
        public void Configure(EntityTypeBuilder<NotificationTo> builder)
        {
            builder.ToTable("NotificationTo", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.NotificationId).HasColumnName(@"NotificationId").HasColumnType("int").IsRequired();
            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType("int").IsRequired();
            builder.Property(x => x.UserName).HasColumnName(@"UserName").HasColumnType("nvarchar").IsRequired().HasMaxLength(150);
            builder.Property(x => x.NotificationMediumId).HasColumnName(@"NotificationMediumId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.SeenOnUtc).HasColumnName(@"SeenOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.VisitOnUtc).HasColumnName(@"VisitOnUtc").HasColumnType("datetime").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.Notification).WithMany(b => b.NotificationToes).HasForeignKey(c => c.NotificationId).OnDelete(DeleteBehavior.SetNull); // FK_NotificationTo_Notification
            builder.HasOne(a => a.UserProfile).WithMany(b => b.NotificationToes).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.SetNull); // FK_NotificationTo_UserProfile
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>