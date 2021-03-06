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


namespace LoanApplicationDb.Data.Mapping
{
    using LoanApplicationDb.Entity.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    // LoanContact
    
    public partial class LoanContactMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<LoanContact>
    {
        public void Configure(EntityTypeBuilder<LoanContact> builder)
        {
            builder.ToTable("LoanContact", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.FirstName).HasColumnName(@"FirstName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(300);
            builder.Property(x => x.MiddleName).HasColumnName(@"MiddleName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.LastName).HasColumnName(@"LastName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(300);
            builder.Property(x => x.NickName).HasColumnName(@"NickName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.Prefix).HasColumnName(@"Prefix").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(10);
            builder.Property(x => x.Suffix).HasColumnName(@"Suffix").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(10);
            builder.Property(x => x.Preferred).HasColumnName(@"Preferred").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(1000);
            builder.Property(x => x.AlternateFirstName).HasColumnName(@"AlternateFirstName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(300);
            builder.Property(x => x.AlternateMiddleName).HasColumnName(@"AlternateMiddleName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.AlternateLastName).HasColumnName(@"AlternateLastName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(300);
            builder.Property(x => x.AlternatePrefix).HasColumnName(@"AlternatePrefix").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(10);
            builder.Property(x => x.AlternateSuffix).HasColumnName(@"AlternateSuffix").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(10);
            builder.Property(x => x.Company).HasColumnName(@"Company").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired();
            builder.Property(x => x.PreferredId).HasColumnName(@"PreferredId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Ssn).HasColumnName(@"Ssn").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(256);
            builder.Property(x => x.DobUtc).HasColumnName(@"DobUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.YrsSchool).HasColumnName(@"YrsSchool").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.MaritalStatusId).HasColumnName(@"MaritalStatusId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.UnmarriedStatusId).HasColumnName(@"UnmarriedStatusId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.IsPlanToMarrySoon).HasColumnName(@"IsPlanToMarrySoon").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.OtherMaritalStatus).HasColumnName(@"OtherMaritalStatus").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.RelationFormedStateId).HasColumnName(@"RelationFormedStateId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.GenderId).HasColumnName(@"GenderId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.HomePhone).HasColumnName(@"HomePhone").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.WorkPhone).HasColumnName(@"WorkPhone").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.WorkPhoneExt).HasColumnName(@"WorkPhoneExt").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.CellPhone).HasColumnName(@"CellPhone").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.EmailAddress).HasColumnName(@"EmailAddress").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.OtherEmailAddress).HasColumnName(@"OtherEmailAddress").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.ResidencyTypeId).HasColumnName(@"ResidencyTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ResidencyStateId).HasColumnName(@"ResidencyStateId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.DemographicMediumId).HasColumnName(@"DemographicMediumId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.SetEthnicityInfoByObservation).HasColumnName(@"SetEthnicityInfoByObservation").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.SetGenderInfoByObservation).HasColumnName(@"SetGenderInfoByObservation").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.SetRaceInfoByObservation).HasColumnName(@"SetRaceInfoByObservation").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.ContactId).HasColumnName(@"ContactId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.TenantId).HasColumnName(@"TenantId").HasColumnType("int").IsRequired();
            builder.Property(x => x.ResidencyStateExplanation).HasColumnName(@"ResidencyStateExplanation").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(250);

            // Foreign keys
            builder.HasOne(a => a.Gender).WithMany(b => b.LoanContacts).HasForeignKey(c => c.GenderId).OnDelete(DeleteBehavior.SetNull); // FK_LoanContact_Gender
            builder.HasOne(a => a.MaritalStatusList).WithMany(b => b.LoanContacts).HasForeignKey(c => c.MaritalStatusId).OnDelete(DeleteBehavior.SetNull); // FK_LoanContact_MaritalStatusList
            builder.HasOne(a => a.MaritalStatusType).WithMany(b => b.LoanContacts).HasForeignKey(c => c.UnmarriedStatusId).OnDelete(DeleteBehavior.SetNull); // FK_LoanContact_MaritalStatusType
            builder.HasOne(a => a.ResidencyState).WithMany(b => b.LoanContacts).HasForeignKey(c => c.ResidencyStateId).OnDelete(DeleteBehavior.SetNull); // FK_LoanContact_ResidencyState
            builder.HasOne(a => a.ResidencyType).WithMany(b => b.LoanContacts).HasForeignKey(c => c.ResidencyTypeId).OnDelete(DeleteBehavior.SetNull); // FK_LoanContact_ResidencyType
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
