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

    // Customer
    
    public partial class CustomerMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.ContactId).HasColumnName(@"ContactId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.BusinessUnitId).HasColumnName(@"BusinessUnitId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.LeadSourceId).HasColumnName(@"LeadSourceId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.LeadSourceTypeId).HasColumnName(@"LeadSourceTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.DisplayOrder).HasColumnName(@"DisplayOrder").HasColumnType("int").IsRequired();
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            builder.Property(x => x.IsSystem).HasColumnName(@"IsSystem").HasColumnType("bit").IsRequired();
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ModifiedOnUtc).HasColumnName(@"ModifiedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.TpId).HasColumnName(@"TpId").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.FirstVisitorId).HasColumnName(@"FirstVisitorId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.FirstSessionId).HasColumnName(@"FirstSessionId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
            builder.Property(x => x.HearAboutUsId).HasColumnName(@"HearAboutUsId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.HearAboutUsOther).HasColumnName(@"HearAboutUsOther").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.CreatedFromId).HasColumnName(@"CreatedFromId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.AdSourceId).HasColumnName(@"AdSourceId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Remarks).HasColumnName(@"Remarks").HasColumnType("nvarchar(max)").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.AdsSource).WithMany(b => b.Customers).HasForeignKey(c => c.AdSourceId).OnDelete(DeleteBehavior.SetNull); // FK_Customer_AdsSource
            builder.HasOne(a => a.BusinessUnit).WithMany(b => b.Customers).HasForeignKey(c => c.BusinessUnitId).OnDelete(DeleteBehavior.SetNull); // FK_Customer_BusinessUnit
            builder.HasOne(a => a.Contact).WithMany(b => b.Customers).HasForeignKey(c => c.ContactId).OnDelete(DeleteBehavior.SetNull); // FK_Customer_Contact
            builder.HasOne(a => a.EntityType).WithMany(b => b.Customers).HasForeignKey(c => c.EntityTypeId).OnDelete(DeleteBehavior.SetNull); // FK_Customer_EntityType
            builder.HasOne(a => a.LeadSource).WithMany(b => b.Customers).HasForeignKey(c => c.LeadSourceId).OnDelete(DeleteBehavior.SetNull); // FK_Customer_LeadSource
            builder.HasOne(a => a.LeadSourceType).WithMany(b => b.Customers).HasForeignKey(c => c.LeadSourceTypeId).OnDelete(DeleteBehavior.SetNull); // FK_Customer_LeadSourceType
            builder.HasOne(a => a.UserProfile).WithMany(b => b.Customers).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.SetNull); // FK_Customer_UserProfile
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
