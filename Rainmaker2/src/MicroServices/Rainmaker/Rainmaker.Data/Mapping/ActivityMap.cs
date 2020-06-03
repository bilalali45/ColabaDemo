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

    // Activity
    
    public partial class ActivityMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Activity", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired().HasMaxLength(150);
            builder.Property(x => x.ActivityTypeId).HasColumnName(@"ActivityTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.TemplateId).HasColumnName(@"TemplateId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.DisplayOrder).HasColumnName(@"DisplayOrder").HasColumnType("int").IsRequired();
            builder.Property(x => x.IsRecurring).HasColumnName(@"IsRecurring").HasColumnType("bit").IsRequired();
            builder.Property(x => x.RecurDuration).HasColumnName(@"RecurDuration").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.RecurCount).HasColumnName(@"RecurCount").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ActivityForId).HasColumnName(@"ActivityForId").HasColumnType("int").IsRequired();
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.IsDefault).HasColumnName(@"IsDefault").HasColumnType("bit").IsRequired();
            builder.Property(x => x.IsSystem).HasColumnName(@"IsSystem").HasColumnType("bit").IsRequired();
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ModifiedOnUtc).HasColumnName(@"ModifiedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
            builder.Property(x => x.EntityRefTypeId).HasColumnName(@"EntityRefTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.RequiredSubscription).HasColumnName(@"RequiredSubscription").HasColumnType("bit").IsRequired();
            builder.Property(x => x.BusinessUnitId).HasColumnName(@"BusinessUnitId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.IsCustomerDefault).HasColumnName(@"IsCustomerDefault").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.Utm).HasColumnName(@"Utm").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.MinimumInterval).HasColumnName(@"MinimumInterval").HasColumnType("int").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.ActivityType).WithMany(b => b.Activities).HasForeignKey(c => c.ActivityTypeId).OnDelete(DeleteBehavior.SetNull); // FK_Activity_ActivityType
            builder.HasOne(a => a.BusinessUnit).WithMany(b => b.Activities).HasForeignKey(c => c.BusinessUnitId).OnDelete(DeleteBehavior.SetNull); // FK_Activity_BusinessUnit
            builder.HasOne(a => a.Template).WithMany(b => b.Activities).HasForeignKey(c => c.TemplateId).OnDelete(DeleteBehavior.SetNull); // FK_Activity_Template
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>