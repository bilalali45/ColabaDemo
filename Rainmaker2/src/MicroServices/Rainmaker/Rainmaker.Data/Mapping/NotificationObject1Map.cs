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

    // NotificationObject
    
    public partial class NotificationObject1Map : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<NotificationObject1>
    {
        public void Configure(EntityTypeBuilder<NotificationObject1> builder)
        {
            builder.ToTable("NotificationObject", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.NotificationTypeId).HasColumnName(@"NotificationTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.EntityId).HasColumnName(@"EntityId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").HasColumnType("datetime2").IsRequired(false);
            builder.Property(x => x.Active).HasColumnName(@"Active").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.Status).HasColumnName(@"Status").HasColumnType("tinyint").IsRequired(false);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
