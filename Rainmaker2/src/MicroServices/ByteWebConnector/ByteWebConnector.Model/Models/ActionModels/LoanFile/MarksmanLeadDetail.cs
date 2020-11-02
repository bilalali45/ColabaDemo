













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // MarksmanLeadDetail

    public partial class MarksmanLeadDetail 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? ProspectId { get; set; } // ProspectId
        public string ProspectTrackingId { get; set; } // ProspectTrackingId (length: 250)
        public string ProspectFirstName { get; set; } // ProspectFirstName (length: 250)
        public string ProspectMiddleName { get; set; } // ProspectMiddleName (length: 250)
        public string ProspectLastName { get; set; } // ProspectLastName (length: 250)
        public string ProspectSsn { get; set; } // ProspectSsn (length: 250)
        public string ProspectAddress1 { get; set; } // ProspectAddress1 (length: 250)
        public string ProspectAddress2 { get; set; } // ProspectAddress2 (length: 250)
        public string ProspectCity { get; set; } // ProspectCity (length: 250)
        public string ProspectState { get; set; } // ProspectState (length: 250)
        public string ProspectZip { get; set; } // ProspectZip (length: 250)
        public string ProspectEmail { get; set; } // ProspectEmail (length: 250)
        public string ProspectHomePhone { get; set; } // ProspectHomePhone (length: 250)
        public string ProspectMobilePhone { get; set; } // ProspectMobilePhone (length: 250)
        public string ProspectWorkPhone { get; set; } // ProspectWorkPhone (length: 250)
        public System.DateTime? ProspectDob { get; set; } // ProspectDob
        public string ProspectCustomerId { get; set; } // ProspectCustomerId (length: 250)
        public int? ProspectSource { get; set; } // ProspectSource
        public int? ProspectStatus { get; set; } // ProspectStatus
        public int? ProspectLoanAmount { get; set; } // ProspectLoanAmount
        public int? ProspectPurchasePrice { get; set; } // ProspectPurchasePrice
        public int? ProspectDownPayment { get; set; } // ProspectDownPayment
        public int? ProspectLockInPeriod { get; set; } // ProspectLockInPeriod
        public int? ProspectLoanPurpose { get; set; } // ProspectLoanPurpose
        public decimal? ProspectLtv { get; set; } // ProspectLtv
        public int? ProspectOccupancy { get; set; } // ProspectOccupancy
        public int? ProspectFico { get; set; } // ProspectFico
        public string ProspectPropertyAddress1 { get; set; } // ProspectPropertyAddress1 (length: 250)
        public string ProspectPropertyAddress2 { get; set; } // ProspectPropertyAddress2 (length: 250)
        public string ProspectPropertyCity { get; set; } // ProspectPropertyCity (length: 250)
        public string ProspectPropertyState { get; set; } // ProspectPropertyState (length: 250)
        public string ProspectPropertyZip { get; set; } // ProspectPropertyZip (length: 250)
        public string ProspectPropertyCounty { get; set; } // ProspectPropertyCounty (length: 250)
        public int? ProspectLoanOfficerId { get; set; } // ProspectLoanOfficerId
        public string ProspectLoanOfficerName { get; set; } // ProspectLoanOfficerName (length: 250)
        public string ProspectLoanOfficerEmail { get; set; } // ProspectLoanOfficerEmail (length: 250)
        public string ProspectLoanOfficerUsername { get; set; } // ProspectLoanOfficerUsername (length: 250)
        public string ProspectLoanOfficerPhone { get; set; } // ProspectLoanOfficerPhone (length: 250)
        public string ProspectRealtorName { get; set; } // ProspectRealtorName (length: 250)
        public string ProspectRealtorPhone { get; set; } // ProspectRealtorPhone (length: 250)
        public string ProspectRealtorEmail { get; set; } // ProspectRealtorEmail (length: 250)
        public string ProspectProjectedCloseDate { get; set; } // ProspectProjectedCloseDate (length: 250)
        public int? ProspectFoundAHome { get; set; } // ProspectFoundAHome
        public string ProspectSellersName { get; set; } // ProspectSellersName (length: 250)
        public string ProspectSellersEmail { get; set; } // ProspectSellersEmail (length: 250)
        public string ProspectOriginalAssignedLo { get; set; } // ProspectOriginalAssignedLo (length: 250)
        public string ProspectLastAssignedDate { get; set; } // ProspectLastAssignedDate (length: 250)
        public string ProspectCbFico { get; set; } // ProspectCbFico (length: 250)
        public string CurrentMortgageBalance { get; set; } // CurrentMortgageBalance (length: 250)
        public string CurrentMortgageRate { get; set; } // CurrentMortgageRate (length: 250)
        public string CurrentMortgagePayment { get; set; } // CurrentMortgagePayment (length: 250)
        public string AgencyCaseNum { get; set; } // AgencyCaseNum (length: 250)
        public string LenderCaseNum { get; set; } // LenderCaseNum (length: 250)
        public string LegalDescription { get; set; } // LegalDescription (length: 250)
        public string YearLotAcquired { get; set; } // YearLotAcquired (length: 250)
        public string OriginalCost { get; set; } // OriginalCost (length: 250)
        public string AmountLien { get; set; } // AmountLien (length: 250)
        public string PresentValue { get; set; } // PresentValue (length: 250)
        public string PurposeRefi { get; set; } // PurposeRefi (length: 250)
        public string MannerTitleHeld { get; set; } // MannerTitleHeld (length: 250)
        public string PropertyImprovements { get; set; } // PropertyImprovements (length: 250)
        public string ImprovementMade { get; set; } // ImprovementMade (length: 250)
        public string CostOfImprovements { get; set; } // CostOfImprovements (length: 250)
        public string ProspectDependents { get; set; } // ProspectDependents (length: 250)
        public string ProspectDependentsAge { get; set; } // ProspectDependentsAge (length: 250)
        public string ProspectYearsInSchool { get; set; } // ProspectYearsInSchool (length: 250)
        public string ProspectMaritalStatus { get; set; } // ProspectMaritalStatus (length: 250)
        public string ProspectSex { get; set; } // ProspectSex (length: 250)
        public string ProspectRace { get; set; } // ProspectRace (length: 250)
        public string ProspectEthnicity { get; set; } // ProspectEthnicity (length: 250)
        public string ProspectCbDependents { get; set; } // ProspectCbDependents (length: 250)
        public string ProspectCbDependentsAge { get; set; } // ProspectCbDependentsAge (length: 250)
        public string ProspectCbYearsInSchool { get; set; } // ProspectCbYearsInSchool (length: 250)
        public string ProspectCbMaritalStatus { get; set; } // ProspectCbMaritalStatus (length: 250)
        public string ProspectCbSex { get; set; } // ProspectCbSex (length: 250)
        public string ProspectCbRace { get; set; } // ProspectCbRace (length: 250)
        public string ProspectCbEthnicity { get; set; } // ProspectCbEthnicity (length: 250)
        public string ProspectEstateHeld { get; set; } // ProspectEstateHeld (length: 250)
        public string ProspectExpirationDate { get; set; } // ProspectExpirationDate (length: 250)
        public string ProspectDownPaymentType { get; set; } // ProspectDownPaymentType (length: 250)
        public int? ProspectProduct1VendorId { get; set; } // ProspectProduct1VendorId
        public int? ProspectProduct1Id { get; set; } // ProspectProduct1Id
        public decimal? ProspectProduct1Rate { get; set; } // ProspectProduct1Rate
        public decimal? ProspectProduct1Price { get; set; } // ProspectProduct1Price
        public decimal? ProspectProduct1Apr { get; set; } // ProspectProduct1Apr
        public string ProspectProduct1LtProductName { get; set; } // ProspectProduct1LtProductName (length: 250)
        public int? ProspectProduct1MonthlyPayment { get; set; } // ProspectProduct1MonthlyPayment
        public int? ProspectProduct2VendorId { get; set; } // ProspectProduct2VendorId
        public int? ProspectProduct2Id { get; set; } // ProspectProduct2Id
        public decimal? ProspectProduct2Rate { get; set; } // ProspectProduct2Rate
        public decimal? ProspectProduct2Price { get; set; } // ProspectProduct2Price
        public decimal? ProspectProduct2Apr { get; set; } // ProspectProduct2Apr
        public string ProspectProduct2LtProductName { get; set; } // ProspectProduct2LtProductName (length: 250)
        public int? ProspectProduct2MonthlyPayment { get; set; } // ProspectProduct2MonthlyPayment
        public int? ProspectProduct3VendorId { get; set; } // ProspectProduct3VendorId
        public int? ProspectProduct3Id { get; set; } // ProspectProduct3Id
        public decimal? ProspectProduct3Rate { get; set; } // ProspectProduct3Rate
        public decimal? ProspectProduct3Price { get; set; } // ProspectProduct3Price
        public decimal? ProspectProduct3Apr { get; set; } // ProspectProduct3Apr
        public string ProspectProduct3LtProductName { get; set; } // ProspectProduct3LtProductName (length: 250)
        public int? ProspectProduct3MonthlyPayment { get; set; } // ProspectProduct3MonthlyPayment
        public int? ProspectCurrResOwnOrRent { get; set; } // ProspectCurrResOwnOrRent
        public int? ProspectCurrResNumberOfYears { get; set; } // ProspectCurrResNumberOfYears
        public int? ProspectCurrResNumberOfMonths { get; set; } // ProspectCurrResNumberOfMonths
        public string ProspectCurrResAddress1 { get; set; } // ProspectCurrResAddress1 (length: 250)
        public string ProspectCurrResAddress2 { get; set; } // ProspectCurrResAddress2 (length: 250)
        public string ProspectCurrResCity { get; set; } // ProspectCurrResCity (length: 250)
        public string ProspectCurrResState { get; set; } // ProspectCurrResState (length: 250)
        public string ProspectCurrResZip { get; set; } // ProspectCurrResZip (length: 250)
        public int? ProspectPrevResOwnOrRent { get; set; } // ProspectPrevResOwnOrRent
        public int? ProspectPrevResNumberOfYears { get; set; } // ProspectPrevResNumberOfYears
        public int? ProspectPrevResNumberOfMonths { get; set; } // ProspectPrevResNumberOfMonths
        public string ProspectPrevResAddress1 { get; set; } // ProspectPrevResAddress1 (length: 250)
        public string ProspectPrevResAddress2 { get; set; } // ProspectPrevResAddress2 (length: 250)
        public string ProspectPrevResCity { get; set; } // ProspectPrevResCity (length: 250)
        public string ProspectPrevResState { get; set; } // ProspectPrevResState (length: 250)
        public string ProspectPrevResZip { get; set; } // ProspectPrevResZip (length: 250)
        public string ProspectCurrEmpEmployerName { get; set; } // ProspectCurrEmpEmployerName (length: 250)
        public int? ProspectCurrEmpMonthsOnJob { get; set; } // ProspectCurrEmpMonthsOnJob
        public int? ProspectCurrEmpYearsOnJob { get; set; } // ProspectCurrEmpYearsOnJob
        public int? ProspectCurrEmpYearsOfExperience { get; set; } // ProspectCurrEmpYearsOfExperience
        public string ProspectCurrEmpTitle { get; set; } // ProspectCurrEmpTitle (length: 250)
        public string ProspectCurrEmpPhone { get; set; } // ProspectCurrEmpPhone (length: 250)
        public string ProspectCurrEmpPhoneExtension { get; set; } // ProspectCurrEmpPhoneExtension (length: 250)
        public int? ProspectCurrEmpIsSelfEmployed { get; set; } // ProspectCurrEmpIsSelfEmployed
        public string ProspectCurrEmpAddress1 { get; set; } // ProspectCurrEmpAddress1 (length: 250)
        public string ProspectCurrEmpAddress2 { get; set; } // ProspectCurrEmpAddress2 (length: 250)
        public string ProspectCurrEmpCity { get; set; } // ProspectCurrEmpCity (length: 250)
        public string ProspectCurrEmpState { get; set; } // ProspectCurrEmpState (length: 250)
        public string ProspectCurrEmpZip { get; set; } // ProspectCurrEmpZip (length: 250)
        public System.DateTime? ProspectCurrEmpStartDate { get; set; } // ProspectCurrEmpStartDate
        public System.DateTime? ProspectCurrEmpEndDate { get; set; } // ProspectCurrEmpEndDate
        public string ProspectPrevEmpEmployerName { get; set; } // ProspectPrevEmpEmployerName (length: 250)
        public int? ProspectPrevEmpMonthsOnJob { get; set; } // ProspectPrevEmpMonthsOnJob
        public int? ProspectPrevEmpYearsOnJob { get; set; } // ProspectPrevEmpYearsOnJob
        public int? ProspectPrevEmpYearsOfExperience { get; set; } // ProspectPrevEmpYearsOfExperience
        public string ProspectPrevEmpTitle { get; set; } // ProspectPrevEmpTitle (length: 250)
        public string ProspectPrevEmpPhone { get; set; } // ProspectPrevEmpPhone (length: 250)
        public string ProspectPrevEmpPhoneExtension { get; set; } // ProspectPrevEmpPhoneExtension (length: 250)
        public int? ProspectPrevEmpIsSelfEmployed { get; set; } // ProspectPrevEmpIsSelfEmployed
        public string ProspectPrevEmpAddress1 { get; set; } // ProspectPrevEmpAddress1 (length: 250)
        public string ProspectPrevEmpAddress2 { get; set; } // ProspectPrevEmpAddress2 (length: 250)
        public string ProspectPrevEmpCity { get; set; } // ProspectPrevEmpCity (length: 250)
        public string ProspectPrevEmpState { get; set; } // ProspectPrevEmpState (length: 250)
        public string ProspectPrevEmpZip { get; set; } // ProspectPrevEmpZip (length: 250)
        public System.DateTime? ProspectPrevEmpStartDate { get; set; } // ProspectPrevEmpStartDate
        public System.DateTime? ProspectPrevEmpEndDate { get; set; } // ProspectPrevEmpEndDate
        public int? ProspectCondo { get; set; } // ProspectCondo
        public int? ProspectUnits { get; set; } // ProspectUnits
        public int? ProspectWaiveEscrow { get; set; } // ProspectWaiveEscrow
        public int? ProspectSecondaryFinancing { get; set; } // ProspectSecondaryFinancing
        public int? ProspectManufacturedHome { get; set; } // ProspectManufacturedHome
        public string ProspectAddDate { get; set; } // ProspectAddDate (length: 250)
        public string ProspectCreditRating { get; set; } // ProspectCreditRating (length: 250)
        public string ProspectBankruptcy { get; set; } // ProspectBankruptcy (length: 250)
        public int? ProspectMonthlyIncome { get; set; } // ProspectMonthlyIncome
        public int? ProspectMonthlyDebt { get; set; } // ProspectMonthlyDebt
        public string ProspectOccupationalStatus { get; set; } // ProspectOccupationalStatus (length: 250)
        public string ProspectFilters { get; set; } // ProspectFilters (length: 250)
        public string ProspectPropertyYearBuilt { get; set; } // ProspectPropertyYearBuilt (length: 250)
        public int? ProspectContactPreference { get; set; } // ProspectContactPreference
        public int? ProspectFirstTimeHomeBuyer { get; set; } // ProspectFirstTimeHomeBuyer
        public int? ProspectIncomeType { get; set; } // ProspectIncomeType
        public int? ProspectIncomeVerify { get; set; } // ProspectIncomeVerify
        public int? ProspectChapter7Years { get; set; } // ProspectChapter7Years
        public int? ProspectChapter13Years { get; set; } // ProspectChapter13Years
        public int? ProspectForeclosureYears { get; set; } // ProspectForeclosureYears
        public int? ProspectLate30 { get; set; } // ProspectLate30
        public int? ProspectLate60 { get; set; } // ProspectLate60
        public int? ProspectLate90 { get; set; } // ProspectLate90
        public int? ProspectLate120 { get; set; } // ProspectLate120
        public int? ProspectGrossDisposableIncome { get; set; } // ProspectGrossDisposableIncome
        public int? ProspectDeclarationBaseIncome { get; set; } // ProspectDeclarationBaseIncome
        public int? ProspectDeclarationBonuses { get; set; } // ProspectDeclarationBonuses
        public int? ProspectDeclarationNetRentalIncome { get; set; } // ProspectDeclarationNetRentalIncome
        public int? ProspectDeclarationOvertime { get; set; } // ProspectDeclarationOvertime
        public int? ProspectDeclarationCommissions { get; set; } // ProspectDeclarationCommissions
        public int? ProspectDeclarationInstallmentDebts { get; set; } // ProspectDeclarationInstallmentDebts
        public int? ProspectDeclarationContinuingDebts { get; set; } // ProspectDeclarationContinuingDebts
        public int? ProspectDeclaration1 { get; set; } // ProspectDeclaration1
        public int? ProspectDeclaration2 { get; set; } // ProspectDeclaration2
        public int? ProspectDeclaration3 { get; set; } // ProspectDeclaration3
        public int? ProspectDeclaration4 { get; set; } // ProspectDeclaration4
        public int? ProspectDeclaration5 { get; set; } // ProspectDeclaration5
        public int? ProspectDeclaration6 { get; set; } // ProspectDeclaration6
        public int? ProspectDeclaration7 { get; set; } // ProspectDeclaration7
        public int? ProspectDeclaration8 { get; set; } // ProspectDeclaration8
        public int? ProspectDeclaration9 { get; set; } // ProspectDeclaration9
        public int? ProspectDeclaration10 { get; set; } // ProspectDeclaration10
        public int? ProspectDeclaration11 { get; set; } // ProspectDeclaration11
        public string ProspectCbFirstName { get; set; } // ProspectCbFirstName (length: 250)
        public string ProspectCbMiddleName { get; set; } // ProspectCbMiddleName (length: 250)
        public string ProspectCbLastName { get; set; } // ProspectCbLastName (length: 250)
        public string ProspectCbEmail { get; set; } // ProspectCbEmail (length: 250)
        public string ProspectCbHomePhone { get; set; } // ProspectCbHomePhone (length: 250)
        public string ProspectCbMobilePhone { get; set; } // ProspectCbMobilePhone (length: 250)
        public string ProspectCbWorkPhone { get; set; } // ProspectCbWorkPhone (length: 250)
        public string ProspectCbSsn { get; set; } // ProspectCbSsn (length: 250)
        public System.DateTime? ProspectCbDob { get; set; } // ProspectCbDob
        public int? ProspectCbCurrResOwnOrRent { get; set; } // ProspectCbCurrResOwnOrRent
        public int? ProspectCbCurrResNumberOfYears { get; set; } // ProspectCbCurrResNumberOfYears
        public int? ProspectCbCurrResNumberOfMonths { get; set; } // ProspectCbCurrResNumberOfMonths
        public string ProspectCbCurrResAddress1 { get; set; } // ProspectCbCurrResAddress1 (length: 250)
        public string ProspectCbCurrResAddress2 { get; set; } // ProspectCbCurrResAddress2 (length: 250)
        public string ProspectCbCurrResCity { get; set; } // ProspectCbCurrResCity (length: 250)
        public string ProspectCbCurrResState { get; set; } // ProspectCbCurrResState (length: 250)
        public string ProspectCbCurrResZip { get; set; } // ProspectCbCurrResZip (length: 250)
        public int? ProspectCbPrevResOwnOrRent { get; set; } // ProspectCbPrevResOwnOrRent
        public int? ProspectCbPrevResNumberOfYears { get; set; } // ProspectCbPrevResNumberOfYears
        public int? ProspectCbPrevResNumberOfMonths { get; set; } // ProspectCbPrevResNumberOfMonths
        public string ProspectCbPrevResAddress1 { get; set; } // ProspectCbPrevResAddress1 (length: 250)
        public string ProspectCbPrevResAddress2 { get; set; } // ProspectCbPrevResAddress2 (length: 250)
        public string ProspectCbPrevResCity { get; set; } // ProspectCbPrevResCity (length: 250)
        public string ProspectCbPrevResState { get; set; } // ProspectCbPrevResState (length: 250)
        public string ProspectCbPrevResZip { get; set; } // ProspectCbPrevResZip (length: 250)
        public string ProspectCbCurrEmpEmployerName { get; set; } // ProspectCbCurrEmpEmployerName (length: 250)
        public int? ProspectCbCurrEmpMonthsOnJob { get; set; } // ProspectCbCurrEmpMonthsOnJob
        public int? ProspectCbCurrEmpYearsOnJob { get; set; } // ProspectCbCurrEmpYearsOnJob
        public int? ProspectCbCurrEmpYearsOfExperience { get; set; } // ProspectCbCurrEmpYearsOfExperience
        public string ProspectCbCurrEmpTitle { get; set; } // ProspectCbCurrEmpTitle (length: 250)
        public string ProspectCbCurrEmpPhone { get; set; } // ProspectCbCurrEmpPhone (length: 250)
        public string ProspectCbCurrEmpPhoneExtension { get; set; } // ProspectCbCurrEmpPhoneExtension (length: 250)
        public int? ProspectCbCurrEmpIsSelfEmployed { get; set; } // ProspectCbCurrEmpIsSelfEmployed
        public string ProspectCbCurrEmpAddress1 { get; set; } // ProspectCbCurrEmpAddress1 (length: 250)
        public string ProspectCbCurrEmpAddress2 { get; set; } // ProspectCbCurrEmpAddress2 (length: 250)
        public string ProspectCbCurrEmpCity { get; set; } // ProspectCbCurrEmpCity (length: 250)
        public string ProspectCbCurrEmpState { get; set; } // ProspectCbCurrEmpState (length: 250)
        public string ProspectCbCurrEmpZip { get; set; } // ProspectCbCurrEmpZip (length: 250)
        public System.DateTime? ProspectCbCurrEmpStartDate { get; set; } // ProspectCbCurrEmpStartDate
        public System.DateTime? ProspectCbCurrEmpEndDate { get; set; } // ProspectCbCurrEmpEndDate
        public string ProspectCbPrevEmpEmployerName { get; set; } // ProspectCbPrevEmpEmployerName (length: 250)
        public int? ProspectCbPrevEmpMonthsOnJob { get; set; } // ProspectCbPrevEmpMonthsOnJob
        public int? ProspectCbPrevEmpYearsOnJob { get; set; } // ProspectCbPrevEmpYearsOnJob
        public int? ProspectCbPrevEmpYearsOfExperience { get; set; } // ProspectCbPrevEmpYearsOfExperience
        public string ProspectCbPrevEmpTitle { get; set; } // ProspectCbPrevEmpTitle (length: 250)
        public string ProspectCbPrevEmpPhone { get; set; } // ProspectCbPrevEmpPhone (length: 250)
        public string ProspectCbPrevEmpPhoneExtension { get; set; } // ProspectCbPrevEmpPhoneExtension (length: 250)
        public int? ProspectCbPrevEmpIsSelfEmployed { get; set; } // ProspectCbPrevEmpIsSelfEmployed
        public string ProspectCbPrevEmpAddress1 { get; set; } // ProspectCbPrevEmpAddress1 (length: 250)
        public string ProspectCbPrevEmpAddress2 { get; set; } // ProspectCbPrevEmpAddress2 (length: 250)
        public string ProspectCbPrevEmpCity { get; set; } // ProspectCbPrevEmpCity (length: 250)
        public string ProspectCbPrevEmpState { get; set; } // ProspectCbPrevEmpState (length: 250)
        public string ProspectCbPrevEmpZip { get; set; } // ProspectCbPrevEmpZip (length: 250)
        public System.DateTime? ProspectCbPrevEmpStartDate { get; set; } // ProspectCbPrevEmpStartDate
        public System.DateTime? ProspectCbPrevEmpEndDate { get; set; } // ProspectCbPrevEmpEndDate
        public int? ProspectCbDeclarationBaseIncome { get; set; } // ProspectCbDeclarationBaseIncome
        public int? ProspectCbDeclarationBonuses { get; set; } // ProspectCbDeclarationBonuses
        public int? ProspectCbDeclarationNetRentalIncome { get; set; } // ProspectCbDeclarationNetRentalIncome
        public int? ProspectCbDeclarationOvertime { get; set; } // ProspectCbDeclarationOvertime
        public int? ProspectCbDeclarationCommissions { get; set; } // ProspectCbDeclarationCommissions
        public int? ProspectCbDeclarationInstallmentDebts { get; set; } // ProspectCbDeclarationInstallmentDebts
        public int? ProspectCbDeclarationContinuingDebts { get; set; } // ProspectCbDeclarationContinuingDebts
        public int? ProspectCbDeclaration1 { get; set; } // ProspectCbDeclaration1
        public int? ProspectCbDeclaration2 { get; set; } // ProspectCbDeclaration2
        public int? ProspectCbDeclaration3 { get; set; } // ProspectCbDeclaration3
        public int? ProspectCbDeclaration4 { get; set; } // ProspectCbDeclaration4
        public int? ProspectCbDeclaration5 { get; set; } // ProspectCbDeclaration5
        public int? ProspectCbDeclaration6 { get; set; } // ProspectCbDeclaration6
        public int? ProspectCbDeclaration7 { get; set; } // ProspectCbDeclaration7
        public int? ProspectCbDeclaration8 { get; set; } // ProspectCbDeclaration8
        public int? ProspectCbDeclaration9 { get; set; } // ProspectCbDeclaration9
        public int? ProspectCbDeclaration10 { get; set; } // ProspectCbDeclaration10
        public int? ProspectCbDeclaration11 { get; set; } // ProspectCbDeclaration11
        public string ProspectSourceName { get; set; } // ProspectSourceName (length: 250)
        public string ProspectCfName1 { get; set; } // ProspectCfName1 (length: 250)
        public string ProspectCfValue1 { get; set; } // ProspectCfValue1 (length: 250)
        public string ProspectCfName2 { get; set; } // ProspectCfName2 (length: 250)
        public string ProspectCfValue2 { get; set; } // ProspectCfValue2 (length: 250)
        public string ProspectCfName3 { get; set; } // ProspectCfName3 (length: 250)
        public string ProspectCfValue3 { get; set; } // ProspectCfValue3 (length: 250)
        public string ProspectCfName4 { get; set; } // ProspectCfName4 (length: 250)
        public string ProspectCfValue4 { get; set; } // ProspectCfValue4 (length: 250)
        public string ProspectCfName5 { get; set; } // ProspectCfName5 (length: 250)
        public string ProspectCfValue5 { get; set; } // ProspectCfValue5 (length: 250)
        public string ProspectCfName6 { get; set; } // ProspectCfName6 (length: 250)
        public string ProspectCfValue6 { get; set; } // ProspectCfValue6 (length: 250)
        public string ProspectCfName7 { get; set; } // ProspectCfName7 (length: 250)
        public string ProspectCfValue7 { get; set; } // ProspectCfValue7 (length: 250)
        public string ProspectCfName8 { get; set; } // ProspectCfName8 (length: 250)
        public string ProspectCfValue8 { get; set; } // ProspectCfValue8 (length: 250)
        public string ProspectCfName9 { get; set; } // ProspectCfName9 (length: 250)
        public string ProspectCfValue9 { get; set; } // ProspectCfValue9 (length: 250)
        public string ProspectCfName10 { get; set; } // ProspectCfName10 (length: 250)
        public string ProspectCfValue10 { get; set; } // ProspectCfValue10 (length: 250)
        public string BranchName { get; set; } // BranchName (length: 250)
        public string ProspectNotes { get; set; } // ProspectNotes (length: 250)
        public string CallDisposition { get; set; } // CallDisposition (length: 250)

        // Foreign keys

        /// <summary>
        /// Parent MarksmanLead pointed by [MarksmanLeadDetail].([Id]) (FK_MarksmanLeadDetail_MarksmanLead)
        /// </summary>
        public virtual MarksmanLead MarksmanLead { get; set; } // FK_MarksmanLeadDetail_MarksmanLead

        public MarksmanLeadDetail()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
