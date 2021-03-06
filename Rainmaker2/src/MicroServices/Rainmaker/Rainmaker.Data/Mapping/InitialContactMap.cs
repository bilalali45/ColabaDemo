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

    // InitialContact
    
    public partial class InitialContactMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<InitialContact>
    {
        public void Configure(EntityTypeBuilder<InitialContact> builder)
        {
            builder.ToTable("InitialContact", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.FirstName).HasColumnName(@"FirstName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(300);
            builder.Property(x => x.LastName).HasColumnName(@"LastName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(300);
            builder.Property(x => x.Email).HasColumnName(@"Email").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.Phone).HasColumnName(@"Phone").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.OpportunityId).HasColumnName(@"OpportunityId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.VisitorId).HasColumnName(@"VisitorId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.Opportunity).WithMany(b => b.InitialContacts).HasForeignKey(c => c.OpportunityId).OnDelete(DeleteBehavior.SetNull); // FK_InitialContact_Opportunity
            builder.HasOne(a => a.Visitor).WithMany(b => b.InitialContacts).HasForeignKey(c => c.VisitorId).OnDelete(DeleteBehavior.SetNull); // FK_InitialContact_Visitor
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
