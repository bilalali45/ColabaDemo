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

    // NotificationChange
    
    public partial class NotificationChangeMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<NotificationChange>
    {
        public void Configure(EntityTypeBuilder<NotificationChange> builder)
        {
            builder.ToTable("NotificationChange", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.NotificationObjectId).HasColumnName(@"NotificationObjectId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ActorId).HasColumnName(@"ActorId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Active).HasColumnName(@"Active").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.Status).HasColumnName(@"Status").HasColumnType("tinyint").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.NotificationObject).WithMany(b => b.NotificationChanges).HasForeignKey(c => c.NotificationObjectId).OnDelete(DeleteBehavior.SetNull); // FK_NotificationChange_NotificationObject_Id
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
