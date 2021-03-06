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

    // CampaignActivityBinder
    
    public partial class CampaignActivityBinderMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<CampaignActivityBinder>
    {
        public void Configure(EntityTypeBuilder<CampaignActivityBinder> builder)
        {
            builder.ToTable("CampaignActivityBinder", "dbo");
            builder.HasKey(x => new { x.CampaignId, x.ActivityId });

            builder.Property(x => x.CampaignId).HasColumnName(@"CampaignId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.ActivityId).HasColumnName(@"ActivityId").HasColumnType("int").IsRequired().ValueGeneratedNever();

            // Foreign keys
            builder.HasOne(a => a.Activity).WithMany(b => b.CampaignActivityBinders).HasForeignKey(c => c.ActivityId).OnDelete(DeleteBehavior.SetNull); // FK_CampaignActivityBinder_Activity
            builder.HasOne(a => a.Campaign).WithMany(b => b.CampaignActivityBinders).HasForeignKey(c => c.CampaignId).OnDelete(DeleteBehavior.SetNull); // FK_CampaignActivityBinder_Campaign
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
