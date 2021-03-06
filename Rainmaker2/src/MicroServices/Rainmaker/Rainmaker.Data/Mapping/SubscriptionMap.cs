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

    // Subscription
    
    public partial class SubscriptionMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable("Subscription", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired().HasMaxLength(150);
            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType("nvarchar").IsRequired().HasMaxLength(500);
            builder.Property(x => x.CustomerDescription).HasColumnName(@"CustomerDescription").HasColumnType("nvarchar").IsRequired().HasMaxLength(500);
            builder.Property(x => x.DisplayOrder).HasColumnName(@"DisplayOrder").HasColumnType("int").IsRequired();
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.IsDefault).HasColumnName(@"IsDefault").HasColumnType("bit").IsRequired();
            builder.Property(x => x.IsSystem).HasColumnName(@"IsSystem").HasColumnType("bit").IsRequired();
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ModifiedOnUtc).HasColumnName(@"ModifiedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
            builder.Property(x => x.SectionId).HasColumnName(@"SectionId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.SubscriptionGroupId).HasColumnName(@"SubscriptionGroupId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.SubscriptionTypeId).HasColumnName(@"SubscriptionTypeId").HasColumnType("int").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.SubscriptionGroup).WithMany(b => b.Subscriptions).HasForeignKey(c => c.SubscriptionGroupId).OnDelete(DeleteBehavior.SetNull); // FK_Subscription_SubscriptionGroup
            builder.HasOne(a => a.SubscriptionSection).WithMany(b => b.Subscriptions).HasForeignKey(c => c.SectionId).OnDelete(DeleteBehavior.SetNull); // FK_Subscription_SubscriptionSection
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
