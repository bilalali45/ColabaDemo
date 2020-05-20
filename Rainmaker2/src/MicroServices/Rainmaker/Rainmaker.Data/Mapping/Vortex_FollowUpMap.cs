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
    
    public partial class Vortex_FollowUpMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Vortex_FollowUp>
    {
        public void Configure(EntityTypeBuilder<Vortex_FollowUp> builder)
        {
            builder.ToTable("FollowUp", "Vortex");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Message).HasColumnName(@"Message").HasColumnType("nvarchar").IsRequired().HasMaxLength(500);
            builder.Property(x => x.ContactId).HasColumnName(@"ContactId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.EmployeeId).HasColumnName(@"EmployeeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.FollowUpStartDateUtc).HasColumnName(@"FollowUpStartDateUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.FollowUpEndDateUtc).HasColumnName(@"FollowUpEndDateUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.RemindOnUtc).HasColumnName(@"RemindOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.FollowedUpOn).HasColumnName(@"FollowedUpOn").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.StatusId).HasColumnName(@"StatusId").HasColumnType("int").IsRequired();
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ModifiedOnUtc).HasColumnName(@"ModifiedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.ContactPhone).HasColumnName(@"ContactPhone").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(100);

            // Foreign keys
            builder.HasOne(a => a.Contact).WithMany(b => b.Vortex_FollowUps).HasForeignKey(c => c.ContactId).OnDelete(DeleteBehavior.SetNull); // FK_FollowUp_Contact
            builder.HasOne(a => a.Employee).WithMany(b => b.Vortex_FollowUps).HasForeignKey(c => c.EmployeeId).OnDelete(DeleteBehavior.SetNull); // FK_FollowUp_Employee
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
