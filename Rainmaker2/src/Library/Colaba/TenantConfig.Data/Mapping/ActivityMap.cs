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


namespace TenantConfig.Data.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TenantConfig.Entity.Models;

    // Activity
    
    public partial class ActivityMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Activity", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.ActivityTypeId).HasColumnName(@"ActivityTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.TemplateId).HasColumnName(@"TemplateId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(50);

            // Foreign keys
            builder.HasOne(a => a.ActivityType).WithMany(b => b.Activities).HasForeignKey(c => c.ActivityTypeId).OnDelete(DeleteBehavior.SetNull); // FK_Activity_ActivityType_Id
            builder.HasOne(a => a.Template).WithMany(b => b.Activities).HasForeignKey(c => c.TemplateId).OnDelete(DeleteBehavior.SetNull); // FK_Activity_Template_Id
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>