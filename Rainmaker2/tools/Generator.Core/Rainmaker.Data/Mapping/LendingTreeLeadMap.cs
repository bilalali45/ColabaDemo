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

    // LendingTreeLead
    
    public partial class LendingTreeLeadMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<LendingTreeLead>
    {
        public void Configure(EntityTypeBuilder<LendingTreeLead> builder)
        {
            builder.ToTable("LendingTreeLead", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.RequestType).HasColumnName(@"RequestType").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.LoanRequestType).HasColumnName(@"LoanRequestType").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.IsTest).HasColumnName(@"IsTest").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.TrackingNumber).HasColumnName(@"TrackingNumber").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.RequestAssignmentDate).HasColumnName(@"RequestAssignmentDate").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ContactAddress).HasColumnName(@"ContactAddress").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ContactCity).HasColumnName(@"ContactCity").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ContactState).HasColumnName(@"ContactState").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ContactZip).HasColumnName(@"ContactZip").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ContactPhoneExtension).HasColumnName(@"ContactPhoneExtension").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ConsumerGeoPhoneAreaCode).HasColumnName(@"ConsumerGeoPhoneAreaCode").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ConsumerGeoPhoneCountryCode).HasColumnName(@"ConsumerGeoPhoneCountryCode").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.FirstName).HasColumnName(@"FirstName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.LastName).HasColumnName(@"LastName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.TimeToContact).HasColumnName(@"TimeToContact").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.IsMaskedEmail).HasColumnName(@"IsMaskedEmail").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.Email).HasColumnName(@"Email").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.IsMaskedPhone).HasColumnName(@"IsMaskedPhone").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.Phone).HasColumnName(@"Phone").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.DateOfBirth).HasColumnName(@"DateOfBirth").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.Ssn).HasColumnName(@"Ssn").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.IsMilitary).HasColumnName(@"IsMilitary").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.AssignedCreditValue).HasColumnName(@"AssignedCreditValue").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.SelfCreditRatingId).HasColumnName(@"SelfCreditRatingId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.SelfCreditRating).HasColumnName(@"SelfCreditRating").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BankruptcyId).HasColumnName(@"BankruptcyId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Bankruptcy).HasColumnName(@"Bankruptcy").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ForeclosureId).HasColumnName(@"ForeclosureId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Foreclosure).HasColumnName(@"Foreclosure").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.FirstTimeHomeBuyer).HasColumnName(@"FirstTimeHomeBuyer").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.WorkingWithAgent).HasColumnName(@"WorkingWithAgent").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.FoundHome).HasColumnName(@"FoundHome").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PropertyPurchaseYear).HasColumnName(@"PropertyPurchaseYear").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PropertyPurchasePrice).HasColumnName(@"PropertyPurchasePrice").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.AnnualIncome).HasColumnName(@"AnnualIncome").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ExistingCustomerRelationship).HasColumnName(@"ExistingCustomerRelationship").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ResidenceTypeId).HasColumnName(@"ResidenceTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ResidenceType).HasColumnName(@"ResidenceType").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PurchaseTimeFrame).HasColumnName(@"PurchaseTimeFrame").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.IsSelfEmployedField).HasColumnName(@"IsSelfEmployedField").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.EmploymentStatusId).HasColumnName(@"EmploymentStatusId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.EmploymentStatus).HasColumnName(@"EmploymentStatus").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.EmployerName).HasColumnName(@"EmployerName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.UniversityAttended).HasColumnName(@"UniversityAttended").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.HighestDegreeObtained).HasColumnName(@"HighestDegreeObtained").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.GraduateDegree).HasColumnName(@"GraduateDegree").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.StudentLoanCreditorName).HasColumnName(@"StudentLoanCreditorName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.StudentLoanBalance).HasColumnName(@"StudentLoanBalance").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.StudentLoanStartDate).HasColumnName(@"StudentLoanStartDate").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.StudentLoanMonthlyPayment).HasColumnName(@"StudentLoanMonthlyPayment").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.StudentLoanTerm).HasColumnName(@"StudentLoanTerm").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.TotalPaymentCount).HasColumnName(@"TotalPaymentCount").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PropertyPrice).HasColumnName(@"PropertyPrice").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.PropertyValue).HasColumnName(@"PropertyValue").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.PropertyCity).HasColumnName(@"PropertyCity").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PropertyState).HasColumnName(@"PropertyState").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PropertyZip).HasColumnName(@"PropertyZip").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PropertyCounty).HasColumnName(@"PropertyCounty").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PropertyMsa).HasColumnName(@"PropertyMsa").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.IsTarget).HasColumnName(@"IsTarget").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PropertyTypeId).HasColumnName(@"PropertyTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.PropertyType).HasColumnName(@"PropertyType").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PropertyUseId).HasColumnName(@"PropertyUseId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.PropertyUse).HasColumnName(@"PropertyUse").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.DownPayment).HasColumnName(@"DownPayment").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.LoanAmount).HasColumnName(@"LoanAmount").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.CashOut).HasColumnName(@"CashOut").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.FirstMortgageBalance).HasColumnName(@"FirstMortgageBalance").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.SecondMortgageBalance).HasColumnName(@"SecondMortgageBalance").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.Ltv).HasColumnName(@"Ltv").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PresentLtv).HasColumnName(@"PresentLtv").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PresentCltv).HasColumnName(@"PresentCltv").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ProposedLtv).HasColumnName(@"ProposedLtv").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ProposedCltv).HasColumnName(@"ProposedCltv").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.Term).HasColumnName(@"Term").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.LoanInfoLoanRequestTypeId).HasColumnName(@"LoanInfoLoanRequestTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.LoanInfoLoanRequestType).HasColumnName(@"LoanInfoLoanRequestType").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.TrusteePartnerId).HasColumnName(@"TrusteePartnerId").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.NameOfPartner).HasColumnName(@"NameOfPartner").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.FilterName).HasColumnName(@"FilterName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.FilterCategoryId).HasColumnName(@"FilterCategoryId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.FilterCategory).HasColumnName(@"FilterCategory").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.FilterRoutingId).HasColumnName(@"FilterRoutingId").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.RoutingParams).HasColumnName(@"RoutingParams").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.LoExternalId).HasColumnName(@"LoExternalId").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ThirdPartyId).HasColumnName(@"ThirdPartyId").HasColumnType("int").IsRequired(false);

            // Foreign keys
            builder.HasOne(a => a.ThirdPartyLead).WithMany(b => b.LendingTreeLeads).HasForeignKey(c => c.ThirdPartyId).OnDelete(DeleteBehavior.SetNull); // FK_LendingTreeLead_ThirdPartyLead
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>