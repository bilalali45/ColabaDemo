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

    // DenormOpportunityContact
    
    public partial class DenormOpportunityContactMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<DenormOpportunityContact>
    {
        public void Configure(EntityTypeBuilder<DenormOpportunityContact> builder)
        {
            builder.ToTable("DenormOpportunityContact", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Duplicate).HasColumnName(@"Duplicate").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Prefix).HasColumnName(@"Prefix").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(10);
            builder.Property(x => x.FirstName).HasColumnName(@"FirstName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(300);
            builder.Property(x => x.MiddleName).HasColumnName(@"MiddleName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.LastName).HasColumnName(@"LastName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(300);
            builder.Property(x => x.Suffix).HasColumnName(@"Suffix").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(10);
            builder.Property(x => x.NickName).HasColumnName(@"NickName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.Preferred).HasColumnName(@"Preferred").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(1000);
            builder.Property(x => x.Email).HasColumnName(@"Email").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.Phone).HasColumnName(@"Phone").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.AllName).HasColumnName(@"AllName").HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(x => x.AllEmail).HasColumnName(@"AllEmail").HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(x => x.AllPhone).HasColumnName(@"AllPhone").HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(x => x.XmlNames).HasColumnName(@"XmlNames").HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(x => x.EmpFirstName).HasColumnName(@"EmpFirstName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(300);
            builder.Property(x => x.EmpMiddleName).HasColumnName(@"EmpMiddleName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.EmpLastName).HasColumnName(@"EmpLastName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(300);
            builder.Property(x => x.EmpNickName).HasColumnName(@"EmpNickName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.EmpPreferred).HasColumnName(@"EmpPreferred").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(1000);

            // Foreign keys
            builder.HasOne(a => a.Opportunity).WithMany().OnDelete(DeleteBehavior.SetNull); // FK_DenormOpportunityContact_Opportunity
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>