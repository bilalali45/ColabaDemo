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

    // FollowUp
    
    public partial class FollowUpMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<FollowUp>
    {
        public void Configure(EntityTypeBuilder<FollowUp> builder)
        {
            builder.ToTable("FollowUp", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Subject).HasColumnName(@"Subject").HasColumnType("nvarchar").IsRequired().HasMaxLength(150);
            builder.Property(x => x.Message).HasColumnName(@"Message").HasColumnType("nvarchar").IsRequired().HasMaxLength(500);
            builder.Property(x => x.IsAnytime).HasColumnName(@"IsAnytime").HasColumnType("bit").IsRequired();
            builder.Property(x => x.FollowUpPurposeId).HasColumnName(@"FollowUpPurposeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.OpportunityId).HasColumnName(@"OpportunityId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ContactId).HasColumnName(@"ContactId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ContactPhoneId).HasColumnName(@"ContactPhoneId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ContactEmailId).HasColumnName(@"ContactEmailId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.EmployeeId).HasColumnName(@"EmployeeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.FollowUpStartDateUtc).HasColumnName(@"FollowUpStartDateUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.FollowUpEndDateUtc).HasColumnName(@"FollowUpEndDateUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.RemindOnUtc).HasColumnName(@"RemindOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.FollowedUpOn).HasColumnName(@"FollowedUpOn").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.ActivityMessage).HasColumnName(@"ActivityMessage").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(1000);
            builder.Property(x => x.StatusId).HasColumnName(@"StatusId").HasColumnType("int").IsRequired();
            builder.Property(x => x.FollowUpPriorityId).HasColumnName(@"FollowUpPriorityId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ModifiedOnUtc).HasColumnName(@"ModifiedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Contact).WithMany(b => b.FollowUps).HasForeignKey(c => c.ContactId).OnDelete(DeleteBehavior.SetNull); // FK_FollowUp_Contact
            builder.HasOne(a => a.ContactEmailInfo).WithMany(b => b.FollowUps).HasForeignKey(c => c.ContactEmailId).OnDelete(DeleteBehavior.SetNull); // FK_FollowUp_ContactEmailInfo
            builder.HasOne(a => a.ContactPhoneInfo).WithMany(b => b.FollowUps).HasForeignKey(c => c.ContactPhoneId).OnDelete(DeleteBehavior.SetNull); // FK_FollowUp_ContactPhoneInfo
            builder.HasOne(a => a.Employee).WithMany(b => b.FollowUps).HasForeignKey(c => c.EmployeeId).OnDelete(DeleteBehavior.SetNull); // FK_FollowUp_Employee
            builder.HasOne(a => a.FollowUpPriority).WithMany(b => b.FollowUps).HasForeignKey(c => c.FollowUpPriorityId).OnDelete(DeleteBehavior.SetNull); // FK_FollowUp_FollowUpPriority
            builder.HasOne(a => a.FollowUpPurpose).WithMany(b => b.FollowUps).HasForeignKey(c => c.FollowUpPurposeId).OnDelete(DeleteBehavior.SetNull); // FK_FollowUp_FollowUpPurpose
            builder.HasOne(a => a.Opportunity).WithMany(b => b.FollowUps).HasForeignKey(c => c.OpportunityId).OnDelete(DeleteBehavior.SetNull); // FK_FollowUp_Opportunity
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
