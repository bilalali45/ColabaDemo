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

    // LoanApplicationView
    
    public partial class LoanApplicationViewMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<LoanApplicationView>
    {
        public void Configure(EntityTypeBuilder<LoanApplicationView> builder)
        {
            builder.ToTable("LoanApplicationView", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.OpportunityId).HasColumnName(@"OpportunityId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.BusinessUnitId).HasColumnName(@"BusinessUnitId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.StatusId).HasColumnName(@"StatusId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.EncompassNumber).HasColumnName(@"EncompassNumber").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.StateName).HasColumnName(@"StateName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.CountyName).HasColumnName(@"CountyName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.CityName).HasColumnName(@"CityName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.ZipCode).HasColumnName(@"ZipCode").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(10);
            builder.Property(x => x.LoanPurpose).HasColumnName(@"LoanPurpose").HasColumnType("nvarchar").IsRequired().HasMaxLength(150);
            builder.Property(x => x.ApplicationStatus).HasColumnName(@"ApplicationStatus").HasColumnType("nvarchar").IsRequired().HasMaxLength(150);
            builder.Property(x => x.BusinessUnitName).HasColumnName(@"BusinessUnitName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.CustomerName).HasColumnName(@"CustomerName").HasColumnType("nvarchar").IsRequired().HasMaxLength(601);
            builder.Property(x => x.AllPhone).HasColumnName(@"AllPhone").HasColumnType("nvarchar").IsRequired().HasMaxLength(152);
            builder.Property(x => x.CellPhone).HasColumnName(@"CellPhone").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.HomePhone).HasColumnName(@"HomePhone").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.WorkPhone).HasColumnName(@"WorkPhone").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.EmailAddress).HasColumnName(@"EmailAddress").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.ModifiedOnUtc).HasColumnName(@"ModifiedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.LoanOfficer).HasColumnName(@"LoanOfficer").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(1000);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>