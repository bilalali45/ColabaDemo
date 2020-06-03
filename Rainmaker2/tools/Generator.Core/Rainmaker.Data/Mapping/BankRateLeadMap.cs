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

    // BankRateLead
    
    public partial class BankRateLeadMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<BankRateLead>
    {
        public void Configure(EntityTypeBuilder<BankRateLead> builder)
        {
            builder.ToTable("BankRateLead", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.BuyerName).HasColumnName(@"BuyerName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BuyerCampaignName).HasColumnName(@"BuyerCampaignName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BuyerId).HasColumnName(@"BuyerId").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.LoanTek).HasColumnName(@"LoanTek").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(1000);
            builder.Property(x => x.RequestDt).HasColumnName(@"RequestDt").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.SchemaVersion).HasColumnName(@"SchemaVersion").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ClientDt).HasColumnName(@"ClientDt").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.CustLangPref).HasColumnName(@"CustLangPref").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ClientAppOrg).HasColumnName(@"ClientAppOrg").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ClientAppName).HasColumnName(@"ClientAppName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ClientAppVersion).HasColumnName(@"ClientAppVersion").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.SellerCampaignName).HasColumnName(@"SellerCampaignName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ClientAppSourceIp).HasColumnName(@"ClientAppSourceIp").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.UniversalLeadId).HasColumnName(@"UniversalLeadId").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.RqUid).HasColumnName(@"RqUid").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.TransactionRequestDt).HasColumnName(@"TransactionRequestDt").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.CurCd).HasColumnName(@"CurCd").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BankrateLeadId).HasColumnName(@"BankrateLeadId").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerSurname).HasColumnName(@"BorrowerSurname").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerGivenName).HasColumnName(@"BorrowerGivenName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerTaxIdTypeCd).HasColumnName(@"BorrowerTaxIdTypeCd").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerTaxId).HasColumnName(@"BorrowerTaxId").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerAddrTypeCd).HasColumnName(@"BorrowerAddrTypeCd").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerAddr1).HasColumnName(@"BorrowerAddr1").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerAddr2).HasColumnName(@"BorrowerAddr2").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerCity).HasColumnName(@"BorrowerCity").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerStateProvCd).HasColumnName(@"BorrowerStateProvCd").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerPostalCode).HasColumnName(@"BorrowerPostalCode").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerCounty).HasColumnName(@"BorrowerCounty").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerBusiness).HasColumnName(@"BorrowerBusiness").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerHome).HasColumnName(@"BorrowerHome").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerAlternate).HasColumnName(@"BorrowerAlternate").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerDay).HasColumnName(@"BorrowerDay").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerEmailAddr).HasColumnName(@"BorrowerEmailAddr").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerWebsiteUrl).HasColumnName(@"BorrowerWebsiteUrl").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerBirthDt).HasColumnName(@"BorrowerBirthDt").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerOccupationClassCd).HasColumnName(@"BorrowerOccupationClassCd").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerMilitaryRankDesc).HasColumnName(@"BorrowerMilitaryRankDesc").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.CreditProfileAmount).HasColumnName(@"CreditProfileAmount").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.CreditProfileDescription).HasColumnName(@"CreditProfileDescription").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerCurrentStep).HasColumnName(@"BorrowerCurrentStep").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.IsWorkingWithRealtor).HasColumnName(@"IsWorkingWithRealtor").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerNewConsultation).HasColumnName(@"BorrowerNewConsultation").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerUnitMeasurementCd).HasColumnName(@"BorrowerUnitMeasurementCd").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.AverageMonthlyExpensesAmount).HasColumnName(@"AverageMonthlyExpensesAmount").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.GrossAnnualIncomeAmount).HasColumnName(@"GrossAnnualIncomeAmount").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.HasProofIncome).HasColumnName(@"HasProofIncome").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.BorrowerTimeframeToPurchase).HasColumnName(@"BorrowerTimeframeToPurchase").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.CoBorrowerSurname).HasColumnName(@"CoBorrowerSurname").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.CoBorrowerGivenName).HasColumnName(@"CoBorrowerGivenName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PropertyAddrTypeCd).HasColumnName(@"PropertyAddrTypeCd").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PropertyAddress).HasColumnName(@"PropertyAddress").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PropertyAddr2).HasColumnName(@"PropertyAddr2").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PropertyCity).HasColumnName(@"PropertyCity").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PropertyState).HasColumnName(@"PropertyState").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ZipCode).HasColumnName(@"ZipCode").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PropertyCounty).HasColumnName(@"PropertyCounty").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.LoanType).HasColumnName(@"LoanType").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.IsFirstPropertyPurchase).HasColumnName(@"IsFirstPropertyPurchase").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.VaLoan).HasColumnName(@"VaLoan").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.HasFhaLoan).HasColumnName(@"HasFhaLoan").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.HasFiledBankruptcy).HasColumnName(@"HasFiledBankruptcy").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ForeclosureCd).HasColumnName(@"ForeclosureCd").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.HousePurchaseYear).HasColumnName(@"HousePurchaseYear").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PropertyEstimatedValueAmount).HasColumnName(@"PropertyEstimatedValueAmount").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.FirstMortgageAmt).HasColumnName(@"FirstMortgageAmt").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.FirstMortgageBalAmt).HasColumnName(@"FirstMortgageBalAmt").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.SecondMortgageAmt).HasColumnName(@"SecondMortgageAmt").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.DownPaymentAmt).HasColumnName(@"DownPaymentAmt").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.AdditionalCashAmt).HasColumnName(@"AdditionalCashAmt").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.FirstMortgageRate).HasColumnName(@"FirstMortgageRate").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.SecondCurrentInterestRate).HasColumnName(@"SecondCurrentInterestRate").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.RequestedInterestRate).HasColumnName(@"RequestedInterestRate").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.RequestedApr).HasColumnName(@"RequestedApr").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.RequestedProductDesc).HasColumnName(@"RequestedProductDesc").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.MonthlyPayAmt).HasColumnName(@"MonthlyPayAmt").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.CurrentLoanServicer).HasColumnName(@"CurrentLoanServicer").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.RateTypeDesired).HasColumnName(@"RateTypeDesired").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.HasSecondMortgage).HasColumnName(@"HasSecondMortgage").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.HasLatePayments).HasColumnName(@"HasLatePayments").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PropertyUse).HasColumnName(@"PropertyUse").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PropertyType).HasColumnName(@"PropertyType").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.HomeImprovementBathRemodel).HasColumnName(@"HomeImprovementBathRemodel").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.HomeImprovementKitchenRemodel).HasColumnName(@"HomeImprovementKitchenRemodel").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.HomeImprovementMajorRemodel).HasColumnName(@"HomeImprovementMajorRemodel").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.HomeImprovementContractor).HasColumnName(@"HomeImprovementContractor").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.HomeImprovementWindows).HasColumnName(@"HomeImprovementWindows").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.HomeImprovementNewRoof).HasColumnName(@"HomeImprovementNewRoof").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.HomeImprovementRoofRepair).HasColumnName(@"HomeImprovementRoofRepair").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.HomeImprovementNewHvac).HasColumnName(@"HomeImprovementNewHvac").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.HomeImprovementExistingHvac).HasColumnName(@"HomeImprovementExistingHvac").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.HomeImprovementSiding).HasColumnName(@"HomeImprovementSiding").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.HomeImprovementFlooring).HasColumnName(@"HomeImprovementFlooring").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.HomeImprovementSolar).HasColumnName(@"HomeImprovementSolar").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.HomeImprovementProjectedStartDt).HasColumnName(@"HomeImprovementProjectedStartDt").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ThirdPartyId).HasColumnName(@"ThirdPartyId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ProductRate).HasColumnName(@"ProductRate").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.ProductApr).HasColumnName(@"ProductApr").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.LoanAmount).HasColumnName(@"LoanAmount").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.EmploymentStatus).HasColumnName(@"EmploymentStatus").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PropertyPurchaseSituation).HasColumnName(@"PropertyPurchaseSituation").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PurchasePriceRange).HasColumnName(@"PurchasePriceRange").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.DownPaymentPercent).HasColumnName(@"DownPaymentPercent").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.GrossAnnualIncomeRange).HasColumnName(@"GrossAnnualIncomeRange").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PropertyEstimatedValueRange).HasColumnName(@"PropertyEstimatedValueRange").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.FirstMortgageBalanceRange).HasColumnName(@"FirstMortgageBalanceRange").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.AdditionalCashRange).HasColumnName(@"AdditionalCashRange").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.AverageMonthlyIncomeRange).HasColumnName(@"AverageMonthlyIncomeRange").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.AverageMonthlyIncomeAmount).HasColumnName(@"AverageMonthlyIncomeAmount").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.AverageMonthlyExpensesRange).HasColumnName(@"AverageMonthlyExpensesRange").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.Phone).HasColumnName(@"Phone").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PurchasePriceAmount).HasColumnName(@"PurchasePriceAmount").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.LeadId).HasColumnName(@"LeadId").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.UserAgent).HasColumnName(@"UserAgent").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.PhoneScore).HasColumnName(@"PhoneScore").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.EmailScore).HasColumnName(@"EmailScore").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.MobilePhone).HasColumnName(@"MobilePhone").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.RecentPhoneActivity).HasColumnName(@"RecentPhoneActivity").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.Url).HasColumnName(@"Url").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(500);

            // Foreign keys
            builder.HasOne(a => a.ThirdPartyLead).WithMany(b => b.BankRateLeads).HasForeignKey(c => c.ThirdPartyId).OnDelete(DeleteBehavior.SetNull); // FK_BankRateLead_ThirdPartyLead
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>