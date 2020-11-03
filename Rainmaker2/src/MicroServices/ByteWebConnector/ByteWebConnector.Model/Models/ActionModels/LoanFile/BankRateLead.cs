













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // BankRateLead

    public partial class BankRateLead 
    {
        public int Id { get; set; } // Id (Primary key)
        public string BuyerName { get; set; } // BuyerName (length: 500)
        public string BuyerCampaignName { get; set; } // BuyerCampaignName (length: 500)
        public string BuyerId { get; set; } // BuyerId (length: 500)
        public string LoanTek { get; set; } // LoanTek (length: 1000)
        public string RequestDt { get; set; } // RequestDt (length: 500)
        public string SchemaVersion { get; set; } // SchemaVersion (length: 500)
        public string ClientDt { get; set; } // ClientDt (length: 500)
        public string CustLangPref { get; set; } // CustLangPref (length: 500)
        public string ClientAppOrg { get; set; } // ClientAppOrg (length: 500)
        public string ClientAppName { get; set; } // ClientAppName (length: 500)
        public string ClientAppVersion { get; set; } // ClientAppVersion (length: 500)
        public string SellerCampaignName { get; set; } // SellerCampaignName (length: 500)
        public string ClientAppSourceIp { get; set; } // ClientAppSourceIp (length: 500)
        public string UniversalLeadId { get; set; } // UniversalLeadId (length: 500)
        public string RqUid { get; set; } // RqUid (length: 500)
        public string TransactionRequestDt { get; set; } // TransactionRequestDt (length: 500)
        public string CurCd { get; set; } // CurCd (length: 500)
        public string BankrateLeadId { get; set; } // BankrateLeadId (length: 500)
        public string BorrowerSurname { get; set; } // BorrowerSurname (length: 500)
        public string BorrowerGivenName { get; set; } // BorrowerGivenName (length: 500)
        public string BorrowerTaxIdTypeCd { get; set; } // BorrowerTaxIdTypeCd (length: 500)
        public string BorrowerTaxId { get; set; } // BorrowerTaxId (length: 500)
        public string BorrowerAddrTypeCd { get; set; } // BorrowerAddrTypeCd (length: 500)
        public string BorrowerAddr1 { get; set; } // BorrowerAddr1 (length: 500)
        public string BorrowerAddr2 { get; set; } // BorrowerAddr2 (length: 500)
        public string BorrowerCity { get; set; } // BorrowerCity (length: 500)
        public string BorrowerStateProvCd { get; set; } // BorrowerStateProvCd (length: 500)
        public string BorrowerPostalCode { get; set; } // BorrowerPostalCode (length: 500)
        public string BorrowerCounty { get; set; } // BorrowerCounty (length: 500)
        public string BorrowerBusiness { get; set; } // BorrowerBusiness (length: 500)
        public string BorrowerHome { get; set; } // BorrowerHome (length: 500)
        public string BorrowerAlternate { get; set; } // BorrowerAlternate (length: 500)
        public string BorrowerDay { get; set; } // BorrowerDay (length: 500)
        public string BorrowerEmailAddr { get; set; } // BorrowerEmailAddr (length: 500)
        public string BorrowerWebsiteUrl { get; set; } // BorrowerWebsiteUrl (length: 500)
        public string BorrowerBirthDt { get; set; } // BorrowerBirthDt (length: 500)
        public string BorrowerOccupationClassCd { get; set; } // BorrowerOccupationClassCd (length: 500)
        public string BorrowerMilitaryRankDesc { get; set; } // BorrowerMilitaryRankDesc (length: 500)
        public string CreditProfileAmount { get; set; } // CreditProfileAmount (length: 500)
        public string CreditProfileDescription { get; set; } // CreditProfileDescription (length: 500)
        public string BorrowerCurrentStep { get; set; } // BorrowerCurrentStep (length: 500)
        public string IsWorkingWithRealtor { get; set; } // IsWorkingWithRealtor (length: 500)
        public string BorrowerNewConsultation { get; set; } // BorrowerNewConsultation (length: 500)
        public string BorrowerUnitMeasurementCd { get; set; } // BorrowerUnitMeasurementCd (length: 500)
        public string AverageMonthlyExpensesAmount { get; set; } // AverageMonthlyExpensesAmount (length: 500)
        public string GrossAnnualIncomeAmount { get; set; } // GrossAnnualIncomeAmount (length: 500)
        public string HasProofIncome { get; set; } // HasProofIncome (length: 500)
        public string BorrowerTimeframeToPurchase { get; set; } // BorrowerTimeframeToPurchase (length: 500)
        public string CoBorrowerSurname { get; set; } // CoBorrowerSurname (length: 500)
        public string CoBorrowerGivenName { get; set; } // CoBorrowerGivenName (length: 500)
        public string PropertyAddrTypeCd { get; set; } // PropertyAddrTypeCd (length: 500)
        public string PropertyAddress { get; set; } // PropertyAddress (length: 500)
        public string PropertyAddr2 { get; set; } // PropertyAddr2 (length: 500)
        public string PropertyCity { get; set; } // PropertyCity (length: 500)
        public string PropertyState { get; set; } // PropertyState (length: 500)
        public string ZipCode { get; set; } // ZipCode (length: 500)
        public string PropertyCounty { get; set; } // PropertyCounty (length: 500)
        public string LoanType { get; set; } // LoanType (length: 500)
        public string IsFirstPropertyPurchase { get; set; } // IsFirstPropertyPurchase (length: 500)
        public string VaLoan { get; set; } // VaLoan (length: 500)
        public string HasFhaLoan { get; set; } // HasFhaLoan (length: 500)
        public string HasFiledBankruptcy { get; set; } // HasFiledBankruptcy (length: 500)
        public string ForeclosureCd { get; set; } // ForeclosureCd (length: 500)
        public string HousePurchaseYear { get; set; } // HousePurchaseYear (length: 500)
        public string PropertyEstimatedValueAmount { get; set; } // PropertyEstimatedValueAmount (length: 500)
        public string FirstMortgageAmt { get; set; } // FirstMortgageAmt (length: 500)
        public string FirstMortgageBalAmt { get; set; } // FirstMortgageBalAmt (length: 500)
        public string SecondMortgageAmt { get; set; } // SecondMortgageAmt (length: 500)
        public string DownPaymentAmt { get; set; } // DownPaymentAmt (length: 500)
        public string AdditionalCashAmt { get; set; } // AdditionalCashAmt (length: 500)
        public string FirstMortgageRate { get; set; } // FirstMortgageRate (length: 500)
        public string SecondCurrentInterestRate { get; set; } // SecondCurrentInterestRate (length: 500)
        public string RequestedInterestRate { get; set; } // RequestedInterestRate (length: 500)
        public string RequestedApr { get; set; } // RequestedApr (length: 500)
        public string RequestedProductDesc { get; set; } // RequestedProductDesc (length: 500)
        public string MonthlyPayAmt { get; set; } // MonthlyPayAmt (length: 500)
        public string CurrentLoanServicer { get; set; } // CurrentLoanServicer (length: 500)
        public string RateTypeDesired { get; set; } // RateTypeDesired (length: 500)
        public string HasSecondMortgage { get; set; } // HasSecondMortgage (length: 500)
        public string HasLatePayments { get; set; } // HasLatePayments (length: 500)
        public string PropertyUse { get; set; } // PropertyUse (length: 500)
        public string PropertyType { get; set; } // PropertyType (length: 500)
        public string HomeImprovementBathRemodel { get; set; } // HomeImprovementBathRemodel (length: 500)
        public string HomeImprovementKitchenRemodel { get; set; } // HomeImprovementKitchenRemodel (length: 500)
        public string HomeImprovementMajorRemodel { get; set; } // HomeImprovementMajorRemodel (length: 500)
        public string HomeImprovementContractor { get; set; } // HomeImprovementContractor (length: 500)
        public string HomeImprovementWindows { get; set; } // HomeImprovementWindows (length: 500)
        public string HomeImprovementNewRoof { get; set; } // HomeImprovementNewRoof (length: 500)
        public string HomeImprovementRoofRepair { get; set; } // HomeImprovementRoofRepair (length: 500)
        public string HomeImprovementNewHvac { get; set; } // HomeImprovementNewHvac (length: 500)
        public string HomeImprovementExistingHvac { get; set; } // HomeImprovementExistingHvac (length: 500)
        public string HomeImprovementSiding { get; set; } // HomeImprovementSiding (length: 500)
        public string HomeImprovementFlooring { get; set; } // HomeImprovementFlooring (length: 500)
        public string HomeImprovementSolar { get; set; } // HomeImprovementSolar (length: 500)
        public string HomeImprovementProjectedStartDt { get; set; } // HomeImprovementProjectedStartDt (length: 500)
        public int? ThirdPartyId { get; set; } // ThirdPartyId
        public string ProductRate { get; set; } // ProductRate (length: 500)
        public string ProductApr { get; set; } // ProductApr (length: 500)
        public string LoanAmount { get; set; } // LoanAmount (length: 500)
        public string EmploymentStatus { get; set; } // EmploymentStatus (length: 500)
        public string PropertyPurchaseSituation { get; set; } // PropertyPurchaseSituation (length: 500)
        public string PurchasePriceRange { get; set; } // PurchasePriceRange (length: 500)
        public string DownPaymentPercent { get; set; } // DownPaymentPercent (length: 500)
        public string GrossAnnualIncomeRange { get; set; } // GrossAnnualIncomeRange (length: 500)
        public string PropertyEstimatedValueRange { get; set; } // PropertyEstimatedValueRange (length: 500)
        public string FirstMortgageBalanceRange { get; set; } // FirstMortgageBalanceRange (length: 500)
        public string AdditionalCashRange { get; set; } // AdditionalCashRange (length: 500)
        public string AverageMonthlyIncomeRange { get; set; } // AverageMonthlyIncomeRange (length: 500)
        public string AverageMonthlyIncomeAmount { get; set; } // AverageMonthlyIncomeAmount (length: 500)
        public string AverageMonthlyExpensesRange { get; set; } // AverageMonthlyExpensesRange (length: 500)
        public string Phone { get; set; } // Phone (length: 500)
        public string PurchasePriceAmount { get; set; } // PurchasePriceAmount (length: 500)
        public string LeadId { get; set; } // LeadId (length: 500)
        public string UserAgent { get; set; } // UserAgent (length: 500)
        public string PhoneScore { get; set; } // PhoneScore (length: 500)
        public string EmailScore { get; set; } // EmailScore (length: 500)
        public string MobilePhone { get; set; } // MobilePhone (length: 500)
        public string RecentPhoneActivity { get; set; } // RecentPhoneActivity (length: 500)
        public string Url { get; set; } // Url (length: 500)

        // Foreign keys

        /// <summary>
        /// Parent ThirdPartyLead pointed by [BankRateLead].([ThirdPartyId]) (FK_BankRateLead_ThirdPartyLead)
        /// </summary>
        public virtual ThirdPartyLead ThirdPartyLead { get; set; } // FK_BankRateLead_ThirdPartyLead

        public BankRateLead()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
