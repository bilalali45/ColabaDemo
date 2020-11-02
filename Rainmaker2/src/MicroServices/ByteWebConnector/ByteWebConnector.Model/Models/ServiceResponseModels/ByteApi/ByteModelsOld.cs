using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ByteWebConnector.Model.Models.ServiceResponseModels.ByteApi
{
    public class ByteModelsOld
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class FileData
        {
            public int LoanID { get; set; }
            public long OrganizationID { get; set; }
            public string FileName { get; set; }
            public int OccupancyType { get; set; }
            public string AgencyCaseNo { get; set; }
            public long FileDataID { get; set; }
        }

        public class LoanStatus
        {
            public long FileDataID { get; set; }
            public long StatusID { get; set; }
            public int LoanStatusID { get; set; }
        }

        public class Status
        {
            public long StatusID { get; set; }
            public int LoanStatus { get; set; }
            public object LeadDate { get; set; }
            public object ApplicationDate { get; set; }
            public int FollowUpFlag { get; set; }
            public object PrequalDate { get; set; }
            public object CreditOnlyDate { get; set; }
            public object InProcessingDate { get; set; }
            public object SubmittedDate { get; set; }
            public object ApprovedDate { get; set; }
            public object ResubmittedDate { get; set; }
            public object DeclinedDate { get; set; }
            public object InClosingDate { get; set; }
            public object ClosedDate { get; set; }
            public object CanceledDate { get; set; }
            public object SchedClosingDate { get; set; }
            public object SchedApprovalDate { get; set; }
            public object SigningDate { get; set; }
            public object FundingDate { get; set; }
            public object PurchaseContractDate { get; set; }
            public bool ExcludeFromManagementReports { get; set; }
            public object FollowUpDate { get; set; }
            public object CreditFirstIssuedDate { get; set; }
            public object SuspendedDate { get; set; }
            public object DocsSignedDate { get; set; }
            public object SchedFundingDate { get; set; }
            public object OtherDate1 { get; set; }
            public object OtherDate2 { get; set; }
            public object OtherDate3 { get; set; }
            public string Notes { get; set; }
            public object LoanPurchasedDate { get; set; }
            public int GFERevisionMethod { get; set; }
            public bool DisclosureWaitingPeriodWaived { get; set; }
            public bool RedisclosureWaitingPeriodWaived { get; set; }
            public object TILRevisionDate { get; set; }
            public int TILRevisionMethod { get; set; }
            public object RecissionDate { get; set; }
            public object InvestorDueDate { get; set; }
            public object ShipByDate { get; set; }
            public object ClearToCloseDate { get; set; }
            public object ShippedDate { get; set; }
            public object CollateralSentDate { get; set; }
            public object DocsSentDate { get; set; }
            public object NoteDate { get; set; }
            public object _NMLSActionDate { get; set; }
            public int PredProtectResult { get; set; }
            public object _StatusDate { get; set; }
            public int ComplianceEaseRiskIndicator { get; set; }
            public object ComplianceCheckRunDate { get; set; }
            public bool ComplianceCheckOverride { get; set; }
            public object AppraisalCompleted { get; set; }
            public object AppraisalDelivered { get; set; }
            public int AppraisalDeliveryMethod { get; set; }
            public object Appraisal2Completed { get; set; }
            public object Appraisal2Delivered { get; set; }

            public long FileDataID { get; set; }
        }

        [DataContract(Name = "Borrower")]
        public class Bor
        {
            [DataMember] public int? BorrowerID { get; set; }

            [DataMember] public string FirstName { get; set; }

            [DataMember] public string MiddleName { get; set; }

            [DataMember] public string LastName { get; set; }

            [DataMember] public string Generation { get; set; }

            [DataMember] public string NickName { get; set; }

            [DataMember] public string SSN { get; set; }

            [DataMember] public string HomePhone { get; set; }

            [DataMember] public string MobilePhone { get; set; }

            [DataMember] public string Fax { get; set; }

            [DataMember] public object Age { get; set; }

            [DataMember] public object DOB { get; set; }

            [DataMember] public int Ethnicity { get; set; }

            [DataMember] public bool GovDoNotWishToFurnish { get; set; }

            [DataMember] public bool RaceNotApplicable { get; set; }

            [DataMember] public bool RaceNotProvided { get; set; }

            [DataMember] public bool RaceAmericanIndian { get; set; }

            [DataMember] public bool RaceAsian { get; set; }

            [DataMember] public bool RaceBlack { get; set; }

            [DataMember] public bool RacePacificIslander { get; set; }

            [DataMember] public bool RaceWhite { get; set; }

            [DataMember] public int Gender { get; set; }

            [DataMember] public object YearsSchool { get; set; }

            [DataMember] public int MaritalStatus { get; set; }

            [DataMember] public int? NoDeps { get; set; }

            [DataMember] public string DepsAges { get; set; }

            [DataMember] public string Email { get; set; }

            //public object DateSigned1003 { get; set; }
            //    public bool OmitFromTitle { get; set; }
            //    public string FNMACreditRefNo { get; set; }
            //    public string VAServiceNo { get; set; }
            //    public string VAClaimFolderNo { get; set; }
            //    public string CAIVRS { get; set; }
            //    public string LDP { get; set; }
            [DataMember] public int OutstandingJudgements { get; set; }

            [DataMember] public int Bankruptcy { get; set; }

            [DataMember] public int PropertyForeclosed { get; set; }

            [DataMember] public int PartyToLawsuit { get; set; }

            [DataMember] public int LoanForeclosed { get; set; }

            [DataMember] public int DelinquentFederalDebt { get; set; }

            [DataMember] public int AlimonyObligation { get; set; }

            [DataMember] public int DownPaymentBorrowed { get; set; }

            [DataMember] public int EndorserOnNote { get; set; }

            [DataMember] public int OccupyAsPrimaryRes { get; set; }

            [DataMember] public int OwnershipInterest { get; set; }

            [DataMember] public int PropertyType { get; set; }

            [DataMember] public int TitleHeld { get; set; }

            //public object IncomeNetRentalOV { get; set; }
            //public object IncomeOther1OV { get; set; }
            //public object IncomeOther2OV { get; set; }
            //public object IncomeTotalOV { get; set; }
            //public string MailingFullAddress { get; set; }
            //public string MailingCityStateZip { get; set; }
            //public string MailingStreet { get; set; }
            //public string MailingCity { get; set; }
            //public string MailingState { get; set; }
            //public string MailingZip { get; set; }
            //public string MailingCountry { get; set; }
            //public string IdentityDocType { get; set; }
            //public string IdentityDocNo { get; set; }
            //public string IdentityDocPlaceOfIssuance { get; set; }
            //public object IdentityDocDateOfIssuance { get; set; }
            //public object IdentityDocExpDate { get; set; }
            //public bool OFACScanComplete { get; set; }
            //public string IdentityComments { get; set; }
            //public object VADeductionFedTax { get; set; }
            //public object VADeductionStateTax { get; set; }
            //public object VADeductionRetirement { get; set; }
            //public object VADeductionOther { get; set; }
            //public int IsVeteran { get; set; }
            //public object ExperianScore { get; set; }
            //public string ExperianModel { get; set; }
            //public string ExperianFactors { get; set; }
            //public string ExperianPIN { get; set; }
            //public object TransUnionScore { get; set; }
            //public string TransUnionModel { get; set; }
            //public string TransUnionFactors { get; set; }
            //public string TransUnionPIN { get; set; }
            //public object EquifaxScore { get; set; }
            //public string EquifaxModel { get; set; }
            //public string EquifaxFactors { get; set; }
            //public string EquifaxPIN { get; set; }
            //public object CreditScoreLow { get; set; }
            //public object CreditScoreHigh { get; set; }
            //public object CreditScoreAverage { get; set; }
            //public object _IncomeNetRental { get; set; }
            //public object _IncomeOther1 { get; set; }
            //public object _IncomeOther2 { get; set; }
            //public double _IncomeTotal { get; set; }
            //public object _CreditScoreMedian { get; set; }
            //public bool IsNonPersonEntity { get; set; }
            //public string FMACCreditRefNo { get; set; }
            //public int HasLDP { get; set; }
            //public int HAMPHardshipIncomeReduced { get; set; }
            //public int HAMPHardshipCircumstancesChanged { get; set; }
            //public int HAMPHardshipExpensesIncreased { get; set; }
            //public int HAMPHardshipCashReservesInsufficient { get; set; }
            //public int HAMPHardshipDebtPaymentsExcessive { get; set; }
            //public int HAMPHardshipOther { get; set; }
            [DataMember] public int CitizenResidencyType { get; set; }

            //public int _USCitizen { get; set; }
            //public int _ResidentAlien { get; set; }
            //public object EquifaxModelRangeLow { get; set; }
            //public object EquifaxModelRangeHigh { get; set; }
            //public object ExperianModelRangeLow { get; set; }
            //public object ExperianModelRangeHigh { get; set; }
            //public object TransUnionModelRangeLow { get; set; }
            //public object TransUnionModelRangeHigh { get; set; }
            //public object EquifaxCreditScoreRank { get; set; }
            //public object ExperianCreditScoreRank { get; set; }
            //public object TransUnionCreditScoreRank { get; set; }
            //public int CreditDenialCreditBureauFlags { get; set; }
            //public string CreditDenialOtherReasons { get; set; }
            //public int CreditDenialCreditScoreBureauOV { get; set; }
            //public int CreditDenialCreditScoreUsedOV { get; set; }
            //public int CounselingConfirmationType { get; set; }
            //public int CounselingFormatType { get; set; }
            //public string MobilePhoneSMSGateway { get; set; }
            //public bool FirstTimeHomebuyer { get; set; }
            //public int LegalEntityType { get; set; }
            //public bool NonTraditionalCreditUsed { get; set; }
            //public bool PostClosingMailingOverride { get; set; }
            //public string PostClosingMailingFullAddress { get; set; }
            //public string PostClosingMailingCityStateZip { get; set; }
            //public string PostClosingMailingStreet { get; set; }
            //public string PostClosingMailingCity { get; set; }
            //public string PostClosingMailingState { get; set; }
            //public string PostClosingMailingZip { get; set; }
            //public string PostClosingMailingCountryCode { get; set; }
            //public int TaxpayerIdentifierType { get; set; }
            //public string CAIVRSInfo { get; set; }
            //public string POAFirstName { get; set; }
            //public string POAMiddleName { get; set; }
            //public string POALastName { get; set; }
            //public string POAGeneration { get; set; }
            //public string POASigningCapacity { get; set; }
            //public string NonPersonEntitySigner { get; set; }
            //public int ForeclosureExplanation { get; set; }
            //public int PrevFHAMort { get; set; }
            //public int PrevFHAMortToBeSold { get; set; }
            //public object PrevFHAMortSalesPrice { get; set; }
            //public object PrevFHAMortOrigLoan { get; set; }
            //public string PrevFHAMortFullAddress { get; set; }
            //public string PrevFHAMortCityStateZip { get; set; }
            //public string PrevFHAMortStreet { get; set; }
            //public string PrevFHAMortCity { get; set; }
            //public string PrevFHAMortState { get; set; }
            //public string PrevFHAMortZip { get; set; }
            //public int FinIntInSubdivision { get; set; }
            //public string SubdivisionDetails { get; set; }
            //public int OwnMoreThanFourDwellings { get; set; }
            //public int EverHadVALoan { get; set; }
            //public int VAOccupancyType { get; set; }
            //public int PriceExceedingValueAwareness { get; set; }
            //public bool OKToEDisclose { get; set; }
            //public bool OKToPullCredit { get; set; }
            //public int CDSignatureMethod { get; set; }
            //public string CDSignatureMethodOtherDesc { get; set; }
            //public int IsCoveredByMilitaryLendingAct { get; set; }
            //public int SSAMatchResult { get; set; }
            //public string SSACheckedSSN { get; set; }
            [DataMember] public string EthnicityOtherHispanicOrLatinoDesc { get; set; }

            [DataMember] public string RaceAmericanIndianTribe { get; set; }

            [DataMember] public string RaceOtherAsianDesc { get; set; }

            [DataMember] public string RaceOtherPacificIslanderDesc { get; set; }

            //public int DemographicInfoProvidedMethod { get; set; }
            [DataMember] public int Race2 { get; set; }

            [DataMember] public int Ethnicity2 { get; set; }

            [DataMember] public int Gender2 { get; set; }

            [DataMember] public int Race2CompletionMethod { get; set; }

            [DataMember] public int Ethnicity2CompletionMethod { get; set; }

            //public int Gender2CompletionMethod { get; set; }
            //public bool GMINotApplicable { get; set; }
            //public int HasMilitaryService { get; set; }
            //public int IsMilitarySurvivingSpouse { get; set; }
            //public object MilitaryServiceExpirationDate { get; set; }
            //public int LanguagePreference { get; set; }
            //public string LanguageOtherDesc { get; set; }
            //public int UseUnmarriedAddendumOV { get; set; }
            //public int HasDomesticRelationship { get; set; }
            //public int DomesticRelationshipType { get; set; }
            //public string DomesticRelationshipOtherDesc { get; set; }
            //public string DomesticRelationshipState { get; set; }
            //public int SpecialBorrowerSellerRelationship { get; set; }
            [DataMember] public decimal? UndisclosedBorrowerFundsAmount { get; set; }

            //public int UndisclosedMortgageApplication { get; set; }
            //public int UndisclosedCreditApplication { get; set; }
            //public int PropertyProposedCleanEnergyLien { get; set; }
            //public int PriorPropertyShortSaleCompleted { get; set; }
            //public int BankruptcyChapterType { get; set; }
            //public bool FormerResidencesDNADesired { get; set; }
            //public bool MailingAddressDNADesired { get; set; }
            //public bool PrimaryEmployerDNADesired { get; set; }
            //public bool SecondaryEmployersDNADesired { get; set; }
            //public bool FormerEmployersDNADesired { get; set; }
            //public bool OtherIncomeDNADesired { get; set; }
            //public bool OtherAssetsDNADesired { get; set; }
            //public bool DebtsDNADesired { get; set; }
            //public bool ExpensesDNADesired { get; set; }
            //public bool DoNotOwnAnyRealEstateDesired { get; set; }
            //public bool GiftsDNADesired { get; set; }
            //public bool AdditionalREOsDNADesired { get; set; }
            //public int ConveyedTitleInLieuOfForeclosure { get; set; }
            //public int HasCompletedEducation { get; set; }
            //public int EducationFormat { get; set; }
            //public string EducationAgencyID { get; set; }
            //public string EducationAgencyName { get; set; }
            //public object EducationCompletionDate { get; set; }
            //public int HasCompletedCounseling { get; set; }
            //public int CounselingFormat { get; set; }
            //public string CounselingAgencyID { get; set; }
            //public string CounselingAgencyName { get; set; }
            //public object CounselingCompletionDate { get; set; }
            //public string URLAAdditionalInfo { get; set; }
            //public int MailingStreetContainsUnitNumberOV { get; set; }
            //public int IsMilitaryActiveDuty { get; set; }
            //public int IsMilitaryRetired { get; set; }
            //public int IsMilitaryReservesOrNationalGuard { get; set; }
            //public int IsPurpleHeartRecipient { get; set; }
            //public string WorkPersonalPhone { get; set; }
            [DataMember] public long FileDataID { get; set; }
        }

        public class Residence
        {
            public int AppNo { get; set; }
            public int ResidenceID { get; set; }
            public int? BorrowerID { get; set; }
            public int DisplayOrder { get; set; }
            public bool Current { get; set; }
            public string FullAddress { get; set; }
            public string CityStateZip { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
            public int LivingStatus { get; set; }
            public int NoYears { get; set; }
            public int NoMonths { get; set; }
            public string LLName { get; set; }
            public string LLAttn { get; set; }
            public string LLFullAddress { get; set; }
            public string LLCityStateZip { get; set; }
            public string LLStreet { get; set; }
            public string LLCity { get; set; }
            public string LLState { get; set; }
            public string LLZip { get; set; }
            public string LLPhone { get; set; }
            public string Notes { get; set; }
            public string LLFax { get; set; }
            public string Country { get; set; }
            public decimal? MonthlyRent { get; set; }
            public int StreetContainsUnitNumberOV { get; set; }
            public long FileDataID { get; set; }
        }

        public class Employer
        {
            public int AppNo { get; set; }
            public int EmployerID { get; set; }
            public int? BorrowerID { get; set; }
            public int DisplayOrder { get; set; }
            public int Status { get; set; }
            public string Name { get; set; }
            public string Attn { get; set; }
            public string FullAddress { get; set; }
            public string CityStateZip { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
            public bool? SelfEmp { get; set; }
            public string Position { get; set; }
            public string TypeBus { get; set; }
            public string Phone { get; set; }
            public DateTime? DateFrom { get; set; }
            public DateTime? DateTo { get; set; }
            public double? YearsOnJob { get; set; }
            public object YearsInProf { get; set; }
            public double? MoIncome { get; set; }
            public string Notes { get; set; }
            public string Fax { get; set; }
            public string VOEVendorID { get; set; }
            public string VOESalaryID { get; set; }
            public object TimeInLineOfWorkYears { get; set; }
            public object TimeInLineOfWorkMonths { get; set; }
            public int IsSpecialRelationship { get; set; }
            public int OwnershipInterest { get; set; }
            public bool IsForeignEmployment { get; set; }
            public bool IsSeasonalEmployment { get; set; }
            public int StreetContainsUnitNumberOV { get; set; }
            public string Country { get; set; }
            public string VOEEmployeeID { get; set; }
            public long FileDataID { get; set; }
        }

        public class Income
        {
            public int AppNo { get; set; }
            public int IncomeID { get; set; }
            public int? BorrowerID { get; set; }
            public int DisplayOrder { get; set; }
            public int IncomeType { get; set; }
            public double? Amount { get; set; }
            public string DescriptionOV { get; set; }
            public int IncomeFrequencyType { get; set; }
            public object IncomeRate { get; set; }
            public object HoursPerWeek { get; set; }
            public string Notes { get; set; }
            public string QMATRNotes { get; set; }
            public string VariablePeriod1Desc { get; set; }
            public object VariablePeriod1Income { get; set; }
            public object VariablePeriod1Months { get; set; }
            public string VariablePeriod2Desc { get; set; }
            public object VariablePeriod2Income { get; set; }
            public object VariablePeriod2Months { get; set; }
            public string VariablePeriod3Desc { get; set; }
            public object VariablePeriod3Income { get; set; }
            public object VariablePeriod3Months { get; set; }
            public object _CalcDescription { get; set; }
            public object _RateDescription { get; set; }
            public bool? SelfEmploymentIncome { get; set; }
            public int EmployerID { get; set; }
            public long FileDataID { get; set; }
        }

        public class Asset
        {
            public int AppNo { get; set; }
            public int AssetID { get; set; }
            public int? BorrowerID { get; set; }
            public int DisplayOrder { get; set; }
            public string Name { get; set; }
            public string Attn { get; set; }
            public string FullAddress { get; set; }
            public string CityStateZip { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
            public int AccountType1 { get; set; }
            public string AccountNo1 { get; set; }
            public double? AccountBalance1 { get; set; }
            public int AccountType2 { get; set; }
            public string AccountNo2 { get; set; }
            public object AccountBalance2 { get; set; }
            public int AccountType3 { get; set; }
            public string AccountNo3 { get; set; }
            public object AccountBalance3 { get; set; }
            public int AccountType4 { get; set; }
            public string AccountNo4 { get; set; }
            public object AccountBalance4 { get; set; }
            public string Notes { get; set; }
            public string Fax { get; set; }
            public string AccountOtherDesc { get; set; }
            public int AccountHeldByType { get; set; }
            public long FileDataID { get; set; }
        }

        public class REO
        {
            public int AppNo { get; set; }
            public int REOID { get; set; }
            public int? BorrowerID { get; set; }
            public int DisplayOrder { get; set; }
            public string FullAddress { get; set; }
            public string CityStateZip { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
            public int REOStatus { get; set; }
            public int REOType { get; set; }
            public double? MarketValue { get; set; }
            public object GrossRentalIncome { get; set; }
            public object Taxes { get; set; }
            public object NetRentalIncomeOV { get; set; }
            public object VacancyFactorOV { get; set; }
            public bool IsSubjectProperty { get; set; }
            public bool IsCurrentResidence { get; set; }
            public object PITIOV { get; set; }
            public bool TIIncludedInMortgage { get; set; }
            public bool VAPurchasedOrRefinancedWithVALoan { get; set; }
            public object VALoanDate { get; set; }
            public int VAEntitlementRestoration { get; set; }
            public bool MortgagesDNADesired { get; set; }
            public int StreetContainsUnitNumberOV { get; set; }
            public int AccountHeldByType { get; set; }
            public int? NoUnits { get; set; }
            public int CurrentUsageType { get; set; }
            public int IntendedUsageType { get; set; }
            public string Country { get; set; }
            public long FileDataID { get; set; }
        }

        public class Debt
        {
            public int AppNo { get; set; }
            public int DebtID { get; set; }
            public int? BorrowerID { get; set; }
            public int DisplayOrder { get; set; }
            public object REOID { get; set; }
            public int DebtType { get; set; }
            public string Name { get; set; }
            public string Attn { get; set; }
            public string FullAddress { get; set; }
            public string CityStateZip { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
            public string AccountNo { get; set; }
            public double? MoPayment { get; set; }
            public int? PaymentsLeft { get; set; }
            public string PaymentsLeftTextOV { get; set; }
            public double? UnpaidBal { get; set; }
            public bool NotCounted { get; set; }
            public bool? ToBePaidOff { get; set; }
            public object LienPosition { get; set; }
            public bool Resubordinated { get; set; }
            public bool Omitted { get; set; }
            public string Notes { get; set; }
            public bool IsLienOnSubProp { get; set; }
            public object TotalPaymentsOV { get; set; }
            public string Fax { get; set; }
            public int SSOrDILAccepted { get; set; }
            public bool ListedOnCreditReport { get; set; }
            public string QMATRNotes { get; set; }
            public string OtherDesc { get; set; }
            public int MortgageType { get; set; }
            public object HELOCCreditLimit { get; set; }
            public int AccountHeldByType { get; set; }
            public long FileDataID { get; set; }
        }

        [DataContract(Name = "Borrower")]
        public class ByteBorrower
        {
            [DataMember] public long FileDataID { get; set; }

            [DataMember] public int? BorrowerID { get; set; }

            [DataMember(Name = "Borrower")] public Bor Borrower { get; set; }

            [DataMember] public List<Residence> Residences { get; set; }

            [DataMember] public List<Employer> Employers { get; set; }

            [DataMember] public List<Income> Incomes { get; set; }

            [DataMember] public List<Asset> Assets { get; set; }

            [DataMember] public List<REO> REOs { get; set; }

            [DataMember] public List<Debt> Debts { get; set; }

            //[DataMember]
            //public List<object> CreditAliases { get; set; }
            //[DataMember]
            //public List<object> Expenses { get; set; }
            //[DataMember]
            //public List<object> Gifts { get; set; }
        }
        //[DataContract(Name = "CoBorrower")]
        //public class ByteCoBorrower
        //{
        //    [DataMember]
        //    public long FileDataID { get; set; }
        //    [DataMember]
        //    public int? BorrowerID { get; set; }

        //    [DataMember(Name = "Borrower")]
        //    public Bor Borrower { get; set; }
        //    [DataMember]
        //    public List<Residence> Residences { get; set; }
        //    [DataMember]
        //    public List<Employer> Employers { get; set; }
        //    [DataMember]
        //    public List<Income> Incomes { get; set; }
        //    [DataMember]
        //    public List<Asset> Assets { get; set; }
        //    [DataMember]
        //    public List<REO> REOs { get; set; }
        //    [DataMember]
        //    public List<Debt> Debts { get; set; }
        //    //[DataMember]
        //    //public List<object> CreditAliases { get; set; }
        //    //[DataMember]
        //    //public List<object> Expenses { get; set; }
        //    //[DataMember]
        //    //public List<object> Gifts { get; set; }

        //}

        [DataContract]
        public class Application
        {
            [DataMember(Name = "Borrower")] public ByteBorrower Borrower { get; set; }

            [DataMember(Name = "CoBorrower")] public ByteBorrower CoBorrower { get; set; }

            [DataMember] public long ApplicationID { get; set; }

            [DataMember] public int DisplayOrder { get; set; }

            [DataMember] public int? BorrowerID { get; set; }

            [DataMember] public int? CoBorrowerID { get; set; }

            [DataMember] public int ApplicationMethod { get; set; }

            [DataMember] public decimal? PresentRent { get; set; }

            //public bool OtherIncome { get; set; }
            //public bool IncomeSpouse { get; set; }
            //public string CreditRefNo { get; set; }
            //public string AutoDesc1 { get; set; }
            //public object AutoValue1 { get; set; }
            //public string AutoDesc2 { get; set; }
            //public object AutoValue2 { get; set; }
            //public string AutoDesc3 { get; set; }
            //public object AutoValue3 { get; set; }
            [DataMember] public int OtherExpenseType { get; set; }

            //public string OtherExpenseOwedTo { get; set; }
            //public object OtherExpenseAmount { get; set; }
            //public string JobExpenseDesc1 { get; set; }
            //public object JobExpenseAmount1 { get; set; }
            //public string JobExpenseDesc2 { get; set; }
            //public object JobExpenseAmount2 { get; set; }
            //public string OtherAssetDesc1 { get; set; }
            //public object OtherAssetValue1 { get; set; }
            //public string OtherAssetDesc2 { get; set; }
            //public object OtherAssetValue2 { get; set; }
            //public string OtherAssetDesc3 { get; set; }
            //public object OtherAssetValue3 { get; set; }
            //public string OtherAssetDesc4 { get; set; }
            //public object OtherAssetValue4 { get; set; }
            //public object NetWorthOfBusiness { get; set; }
            [DataMember] public object RetirementFunds { get; set; }

            [DataMember] public object LifeInsFaceValue { get; set; }

            [DataMember] public object LifeInsCashValue { get; set; }

            [DataMember] public long FileDataID { get; set; }

            [DataMember] public decimal? PresentHOD { get; set; }

            [DataMember] public string StockBondDesc1 { get; set; }

            [DataMember] public decimal? StockBondValue1 { get; set; }
        }

        public class SubProp
        {
            public long SubPropID { get; set; }
            public string FullAddress { get; set; }
            public string CityStateZip { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string County { get; set; }
            public object CountyCode { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
            public object AppraisedValue { get; set; }
            public int PropertyType { get; set; }
            public int? NoUnits { get; set; }
            public string LegalDesc { get; set; }
            public bool MetesAndBounds { get; set; }
            public object YearBuilt { get; set; }
            public object FirstMortPI { get; set; }
            public object FirstMortBalance { get; set; }
            public object SecondMortPI { get; set; }
            public object SecondMortBalance { get; set; }
            public object GrossRentalIncome { get; set; }
            public object VacancyFactorOV { get; set; }
            public bool ReservesRequired { get; set; }
            public object CYearLotAcq { get; set; }
            public object COrigCost { get; set; }
            public object CAmtExLiens { get; set; }
            public object CPresValLot { get; set; }
            public object CImprvCost { get; set; }
            public object RYearLotAcq { get; set; }
            public object ROrigCost { get; set; }
            public object RAmtExLiens { get; set; }
            public int ImprvMade { get; set; }
            public string ImprvDesc { get; set; }
            public object ImprvCost { get; set; }
            public string MannerTitleHeld { get; set; }
            public int EstHeld { get; set; }
            public object EstLeaseHoldEx { get; set; }
            public string MSA { get; set; }
            public object NetCashFlowOV { get; set; }
            public object AltImpRep { get; set; }
            public object LandValue { get; set; }
            public bool IsPUD { get; set; }
            public int PropertyClass { get; set; }
            public string ProjectName { get; set; }
            public object SquareFeet { get; set; }
            public object PropertyAge { get; set; }
            public object TotalRooms { get; set; }
            public object Bathrooms { get; set; }
            public object Bedrooms { get; set; }
            public object FHAVAUnpaidBalance { get; set; }
            public object RemainingEconomicLife { get; set; }
            public string PropertyTypeCustom { get; set; }
            public string AssessorsParcelNo { get; set; }
            public object PriorSaleDate { get; set; }
            public object PriorSaleAmount { get; set; }
            public object FirstMortOrigAmount { get; set; }
            public object DateLandAcquired { get; set; }
            public object LandPurchasePrice { get; set; }
            public bool LandAcquiredNotByPurchase { get; set; }
            public object AVMConfidenceScore { get; set; }
            public object AVMDeterminationDate { get; set; }
            public object Stories { get; set; }
            public int WarrantableCondo { get; set; }
            public bool PropertyTBD { get; set; }
            public bool SpecialFloodHazardArea { get; set; }
            public int ProjectStatusType { get; set; }
            public int ProjectDesignType { get; set; }
            public object ProjectDwellingUnitCount { get; set; }
            public object ProjectDwellingUnitsSoldCount { get; set; }
            public object PropertyValuationEffectiveDate { get; set; }
            public int PropertyValuationMethod { get; set; }
            public int AVMModelType { get; set; }
            public string PropertyValuationUCDPDocumentIdentifier { get; set; }
            public object BedroomsUnit1 { get; set; }
            public object BedroomsUnit2 { get; set; }
            public object BedroomsUnit3 { get; set; }
            public object BedroomsUnit4 { get; set; }
            public object GrossRentUnit1 { get; set; }
            public object GrossRentUnit2 { get; set; }
            public object GrossRentUnit3 { get; set; }
            public object GrossRentUnit4 { get; set; }
            public string OriginalLoanGSEIdentifier { get; set; }
            public int OriginalLoanOwner { get; set; }
            public object AssessedValue { get; set; }
            public bool CreditSaleIndicator { get; set; }
            public int UCDPFindingsStatusFannie { get; set; }
            public int UCDPFindingsStatusFreddie { get; set; }
            public string LegalDescShort { get; set; }
            public bool PartialSFHA { get; set; }
            public string FirstMortQMATRNotes { get; set; }
            public string SecondMortQMATRNotes { get; set; }
            public int ULDDManufacturedWidthType { get; set; }
            public int ULDDPropertyValuationForm { get; set; }
            public string ParsedHouseNumber { get; set; }
            public string ParsedDirectionPrefix { get; set; }
            public string ParsedStreetName { get; set; }
            public string ParsedStreetSuffix { get; set; }
            public string ParsedDirectionSuffix { get; set; }
            public string ParsedUnitNumber { get; set; }
            public int DelayedSettlementDueToConstruction { get; set; }
            public int AppraisedValueStatusOV { get; set; }
            public int LandLoanStatus { get; set; }
            public int TRIDAltImpRepOption { get; set; }
            public object PartialSFHAStructureCount { get; set; }
            public string PreviousLoanNumber { get; set; }
            public int FREAppraisalFormType { get; set; }
            public string FREAppraisalFormTypeOther { get; set; }
            public int ManufacturedHomeLandPropertyInterest { get; set; }
            public object MultifamilyAffordableUnitsCount { get; set; }
            public int ManufacturedHomeSecuredPropertyType { get; set; }
            public bool IsChattelLoan { get; set; }
            public bool PropertyHasNoAddress { get; set; }
            public string Lot { get; set; }
            public string Block { get; set; }
            public object ManufacturedHomeWidth { get; set; }
            public object ManufacturedHomeLength { get; set; }
            public int ManufacturedHomeAttachedToFoundation { get; set; }
            public int ManufacturedHomeCondition { get; set; }
            public string ManufacturedHomeHUDCertLabelID1 { get; set; }
            public string ManufacturedHomeHUDCertLabelID2 { get; set; }
            public string ManufacturedHomeHUDCertLabelID3 { get; set; }
            public string ManufacturedHomeMake { get; set; }
            public string ManufacturedHomeModel { get; set; }
            public string ManufacturedHomeSerialNo { get; set; }
            public bool HasHomesteadExemption { get; set; }
            public int IsMixedUseProperty { get; set; }
            public bool NetCashFlowDNADesired { get; set; }
            public bool OtherLoansDNADesired { get; set; }
            public bool IsConversionOfLandContract { get; set; }
            public bool IsRenovation { get; set; }
            public bool HasCleanEnergyLien { get; set; }
            public DateTime? LotAcquiredDate { get; set; }
            public int IndianCountryLandTenure { get; set; }
            public int StreetContainsUnitNumberOV { get; set; }
            public int LandValueType { get; set; }
            public string PropertyDataID { get; set; }
            public long FileDataID { get; set; }
        }

        public class Loan
        {
            public long LoanID { get; set; }

            public int LoanPurpose { get; set; }
            public int AmortizationType { get; set; }
            public string AmortizationTypeDescOV { get; set; }
            public string LoanProgramName { get; set; }
            public object PurPrice { get; set; }
            public object BaseLoan { get; set; }
            public double LoanWith { get; set; }
            public int MortgageType { get; set; }
            public object SubFiBaseLoan { get; set; }
            public long FileDataID { get; set; }
            public double? RefinanceCashOutAmount { get; internal set; }
            public string LoanGUID { get; set; }
        }

        //public class ClosingCost
        //{
        //    public int CCID { get; set; }
        //    public int LoanID { get; set; }
        //    public int HUDLineNo { get; set; }
        //    public bool PPFC { get; set; }
        //    public object Points { get; set; }
        //    public string Name { get; set; }
        //    public object BorrowerAmount { get; set; }
        //    public object SellerAmount { get; set; }
        //    public bool POC { get; set; }
        //    public bool IsPaidToBroker { get; set; }
        //    public object PaidToBrokerSplit { get; set; }
        //    public bool NotCounted { get; set; }
        //    public string PaidToName { get; set; }
        //    public int ClosingCostType { get; set; }
        //    public object _TotalAmount { get; set; }
        //    public object _PaidToBroker { get; set; }
        //    public object _PaidToOthers { get; set; }
        //    public int PaidByOtherType { get; set; }
        //    public object GFEDisclosedAmount { get; set; }
        //    public bool ProviderChosenByBorrower { get; set; }
        //    public int HUDLineNo2010 { get; set; }
        //    public int GFEBlock { get; set; }
        //    public int ResponsiblePartyType { get; set; }
        //    public int PaidToType { get; set; }
        //    public bool NetFromWire { get; set; }
        //    public object _GLCode { get; set; }
        //    public bool Financed { get; set; }
        //    public object PointsAndFeesAmountOV { get; set; }
        //    public int TRIDBlock { get; set; }
        //    public int IsOptionalOV { get; set; }
        //    public object GFEBaselineAmount { get; set; }
        //    public object BorrowerPOCAmountOV { get; set; }
        //    public object SellerPOCAmountOV { get; set; }
        //    public long FileDataID { get; set; }

        //}

        public class PrepaidItem
        {
            public long PrepaidItemID { get; set; }
            public long LoanID { get; set; }
            public int PrepaidItemType { get; set; }
            public string NameOV { get; set; }
            public double? Payment { get; set; }
            public long FileDataID { get; set; }
            public int PremiumPaidToType { get; set; }
        }

        //public class DOT
        //{
        //    public int DOTID { get; set; }
        //    public object RefiDebtsToBePaidOffOV { get; set; }
        //    public object EstPrepaidsOV { get; set; }
        //    public object EstClosingCostsOV { get; set; }
        //    public object MIPFFTotalOV { get; set; }
        //    public object DiscountOV { get; set; }
        //    public object TotalCostsOV { get; set; }
        //    public object ClosingCostsPaidBySellerOV { get; set; }
        //    public object CashFromToBorrowerOV { get; set; }
        //    public object AppDepositAmount { get; set; }
        //    public string AppDepositHeldBy { get; set; }
        //    public object EarnestMoneyAmount { get; set; }
        //    public string EarnestMoneyHeldBy { get; set; }
        //    public bool ExcludeSubFi { get; set; }
        //    public object _OtherCreditAmount5 { get; set; }
        //    public object _OtherCreditDesc5 { get; set; }
        //    public object _OtherCreditDesc6 { get; set; }
        //    public object _OtherCreditAmount6 { get; set; }
        //    public object _LineA { get; set; }
        //    public object _LineB { get; set; }
        //    public object _LineC { get; set; }
        //    public object _LineD { get; set; }
        //    public double _LineE { get; set; }
        //    public double _LineF { get; set; }
        //    public object _LineG { get; set; }
        //    public double _LineH { get; set; }
        //    public double _LineI { get; set; }
        //    public object _LineJ { get; set; }
        //    public object _LineK { get; set; }
        //    public object _LineL1 { get; set; }
        //    public object _LineL2 { get; set; }
        //    public object _LineL3 { get; set; }
        //    public object _LineL4 { get; set; }
        //    public object _LineM { get; set; }
        //    public object _LineN { get; set; }
        //    public double _LineO { get; set; }
        //    public double _LineP { get; set; }
        //    public double _CashFromToBorrower { get; set; }
        //    public object LECashToCloseAdjustmentAmount { get; set; }
        //    public string LECashToCloseAdjustmentDesc { get; set; }
        //    public object RefiSubPropLoansToBePaidOffOV { get; set; }
        //    public object OtherDebtsToBePaidOffOV { get; set; }
        //    public object BorrowerClosingCostsOV { get; set; }
        //    public object DiscountPointsOV { get; set; }
        //    public long FileDataID { get; set; }

        //}

        //public class Fannie
        //{
        //    public int FannieID { get; set; }
        //    public string CaseFileID { get; set; }
        //    public int CommunityLendingProduct { get; set; }
        //    public int CommSeconds { get; set; }
        //    public int NeighborsElig { get; set; }
        //    public string MSA { get; set; }
        //    public string Recommendation { get; set; }
        //    public string InstitutionID { get; set; }
        //    public int CommunitySecondsRepaymentSchedule { get; set; }
        //    public object UnderwritingRunDate { get; set; }
        //    public string SellerNumber { get; set; }
        //    public int CreditAgencyCode { get; set; }
        //    public bool RefiOfConstructionLoan { get; set; }
        //    public int HomebuyerEducationType { get; set; }
        //    public int ProductDescription { get; set; }
        //    public object EnergyImpAmount { get; set; }
        //    public object PACELoanPayoffAmount { get; set; }
        //    public string ProductDescriptionOther { get; set; }
        //    public bool IsSellerProvidingBelowMarketSubFi { get; set; }
        //    public int SubmissionType { get; set; }
        //    public string LenderInstitutionIdentifier { get; set; }
        //    public long FileDataID { get; set; }

        //}

        //public class Freddie
        //{
        //    public int FreddieID { get; set; }
        //    public int AffordableProgramId { get; set; }
        //    public int OfferingId { get; set; }
        //    public string OtherOfferingIdentifier { get; set; }
        //    public int LoanDocumentationType { get; set; }
        //    public int BuildingStatusType { get; set; }
        //    public int OriginationProcessingPoint { get; set; }
        //    public string LoanKeyNumber { get; set; }
        //    public string EvaluationStatusType { get; set; }
        //    public object NumberOfSubmissions { get; set; }
        //    public object EvaluationDate { get; set; }
        //    public string CreditRiskType { get; set; }
        //    public string RepositorySource { get; set; }
        //    public string EvaluationType { get; set; }
        //    public object SubmissionDate { get; set; }
        //    public string PurchaseEligibility { get; set; }
        //    public object InitialLTV { get; set; }
        //    public string TransactionNumber { get; set; }
        //    public object InitialTLTV { get; set; }
        //    public string DocumentClassification { get; set; }
        //    public string RiskGrade { get; set; }
        //    public object LossCoverageEstimate { get; set; }
        //    public string MIDecisioin { get; set; }
        //    public object CreditScore { get; set; }
        //    public string CreditRiskComment { get; set; }
        //    public string RepositoryReason { get; set; }
        //    public int BuydownContributor { get; set; }
        //    public string SellerNumber { get; set; }
        //    public string TPONumber { get; set; }
        //    public string NOTP { get; set; }
        //    public int ConstructionPurpose { get; set; }
        //    public object FREReserves { get; set; }
        //    public string LoanIdentifier { get; set; }
        //    public string TransactionIdentifier { get; set; }
        //    public string LenderRegistrationNumber { get; set; }
        //    public string CreditReferenceNo { get; set; }
        //    public int NewConstruction { get; set; }
        //    public string CPMProjectID { get; set; }
        //    public int PropertyCategoryType { get; set; }
        //    public int OwnershipType { get; set; }
        //    public string OriginationProcessingPointOther { get; set; }
        //    public string PropertyCategoryTypeOther { get; set; }
        //    public string OwnershipTypeOther { get; set; }
        //    public string BuydownContributorOther { get; set; }
        //    public string BuildingStatusTypeOther { get; set; }
        //    public bool OrderMergedCredit { get; set; }
        //    public object FHABorrowerClosingCosts { get; set; }
        //    public object FHAFinancedDiscountPoints { get; set; }
        //    public object VAResidualIncome { get; set; }
        //    public string CreditAgencyCode { get; set; }
        //    public string CreditAffiliateCode { get; set; }
        //    public object FREMonthsInReserveOV { get; set; }
        //    public int AppraisalMethodType { get; set; }
        //    public long FileDataID { get; set; }

        //}

        public class Party
        {
            public long PartyID { get; set; }
            public long CategoryID { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public string Title { get; set; }
            public string Company { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
            public string EMail { get; set; }
            public string WorkPhone { get; set; }
            public string HomePhone { get; set; }
            public string MobilePhone { get; set; }
            public string OtherPhone { get; set; }
            public string Pager { get; set; }
            public string Fax { get; set; }
            public string LicenseNo { get; set; }
            public string CHUMSNo { get; set; }
            public string FHAOrigOrSponsorID { get; set; }
            public string BranchID { get; set; }
            public string Notes { get; set; }
            public string ContactNMLSID { get; set; }

            public string CompanyNMLSID { get; set; }

            //public string LockToUser { get; set; }
            //public string CompanyEIN { get; set; }
            //public string MobilePhoneSMSGateway { get; set; }
            //public string CompanyLicenseNo { get; set; }
            //public string SyncData { get; set; }
            //public DateTime EAndOPolicyExpirationDate { get; set; }
            //public string LicensingAgencyCode { get; set; }
            //public string EMail2 { get; set; }
            //public string EMail3 { get; set; }
            public long FileDataID { get; set; }
        }

        //public class PartyMisc
        //{
        //    public int PartyMiscID { get; set; }
        //    public string HazPolicyNo { get; set; }
        //    public object HazEffectiveDate { get; set; }
        //    public object HazRenewalDate { get; set; }
        //    public bool HazInsTypeEarthquake { get; set; }
        //    public bool HazInsTypeFlood { get; set; }
        //    public bool HazInsTypeHazard { get; set; }
        //    public bool HazInsTypeWindStorm { get; set; }
        //    public int HazInsEscrowed { get; set; }
        //    public string EscrowAccountNo { get; set; }
        //    public bool WaiveEscrowAmended { get; set; }
        //    public int PropTaxesEscrowed { get; set; }
        //    public string FloodAccountNo { get; set; }
        //    public string MICertificateNo { get; set; }
        //    public object MIPerCov { get; set; }
        //    public string TitleAccountNo { get; set; }
        //    public string MortgageeClause { get; set; }
        //    public string LenderVesting { get; set; }
        //    public string LenderGovLaws { get; set; }
        //    public int FloodInsRequired { get; set; }
        //    public int FloodInsNFIPType { get; set; }
        //    public string FloodInsZone { get; set; }
        //    public object FloodInsApplicationDate { get; set; }
        //    public string FloodInsCommunity { get; set; }
        //    public string FloodInsInfoObtainedFrom { get; set; }
        //    public object FloodInsNFIPMapPanelDate { get; set; }
        //    public object FloodInsDeterminationDate { get; set; }
        //    public object ReferralFee { get; set; }
        //    public int TitleOrderSurvey { get; set; }
        //    public int TitleOrderPayoffs { get; set; }
        //    public bool ShowFirstAndSecondOnTitleOrder { get; set; }
        //    public object FloodCoverageAmount { get; set; }
        //    public string TitleUnderwriter { get; set; }
        //    public object _LoanOfficerLicenseExpDate { get; set; }
        //    public string CompanyLicenseNoOV { get; set; }
        //    public string CompanyNMLSIDOV { get; set; }
        //    public string BranchLicenseNoOV { get; set; }
        //    public string BranchNMLSIDOV { get; set; }
        //    public object _CompanyLicenseNo { get; set; }
        //    public object _CompanyNMLSID { get; set; }
        //    public object _CompanyLicenseExpirationDate { get; set; }
        //    public object _BranchLicenseNo { get; set; }
        //    public object _BranchNMLSID { get; set; }
        //    public object _BranchLicenseExpirationDate { get; set; }
        //    public string SupervisoryAppraiserLicenseNumber { get; set; }
        //    public string CompanyEINOV { get; set; }
        //    public object _CompanyEIN { get; set; }
        //    public string InvestorCode { get; set; }
        //    public int MICompanyNameType { get; set; }
        //    public string Haz2PolicyNo { get; set; }
        //    public object Haz2EffectiveDate { get; set; }
        //    public object Haz2RenewalDate { get; set; }
        //    public int Haz2InsType { get; set; }
        //    public string FloodInsCounty { get; set; }
        //    public string FloodInsCommunityNo { get; set; }
        //    public string NFIPMapIdentifier { get; set; }
        //    public object NFIPLetterOfMapDate { get; set; }
        //    public int NFIPMapIndicator { get; set; }
        //    public bool FloodInsIsInProtectedArea { get; set; }
        //    public object FloodInsProtectedAreaDesigDate { get; set; }
        //    public int FloodInsIsLifeOfLoan { get; set; }
        //    public string FloodCertificationIdentifier { get; set; }
        //    public object NFIPCommunityParticipationStartDate { get; set; }
        //    public int NFIPFloodDataRevisionType { get; set; }
        //    public object TitleReportDate { get; set; }
        //    public string TitleReportItems { get; set; }
        //    public string TitleReportEndorsements { get; set; }
        //    public bool BuilderOrSellerIsNonPersonEntity { get; set; }
        //    public string CreditAltLenderCaseNo { get; set; }
        //    public int ClosingAgentType { get; set; }
        //    public string ServicerCode { get; set; }
        //    public string WireSpecialInstructions { get; set; }
        //    public object TrusteeFeePercent { get; set; }
        //    public int Seller1CDSignatureMethod { get; set; }
        //    public string Seller1CDSignatureMethodOtherDesc { get; set; }
        //    public object _LoanOfficerLicenseStartDate { get; set; }
        //    public object _CompanyLicenseStartDate { get; set; }
        //    public object _BranchLicenseStartDate { get; set; }
        //    public int MIUnderwritingType { get; set; }
        //    public string MISpecialProgramCode { get; set; }
        //    public object FloodEffectiveDate { get; set; }
        //    public object FloodRenewalDate { get; set; }
        //    public object EducationBorrowerID { get; set; }
        //    public object CounselingBorrowerID { get; set; }
        //    public long FileDataID { get; set; }

        //}

        //public class CreditDenial
        //{
        //    public int CreditDenialID { get; set; }
        //    public bool CreditNoCreditFile { get; set; }
        //    public bool CreditNumberOfReferences { get; set; }
        //    public bool CreditInsufficientFiles { get; set; }
        //    public bool CreditLimitedExperience { get; set; }
        //    public bool CreditUnableToVerifyReferences { get; set; }
        //    public bool CreditGarnishment { get; set; }
        //    public bool CreditJudgment { get; set; }
        //    public bool CreditExcessiveObligation { get; set; }
        //    public bool CreditPaymentRecordPreviousMtg { get; set; }
        //    public bool CreditLackOfCashReserves { get; set; }
        //    public bool CreditDelinquentObligationOthers { get; set; }
        //    public bool CreditBankruptcy { get; set; }
        //    public bool CreditTypeOfReference { get; set; }
        //    public bool CreditPoorPerformanceUs { get; set; }
        //    public bool EmpUnableToVerify { get; set; }
        //    public bool EmpLength { get; set; }
        //    public bool EmpTempOrIrregular { get; set; }
        //    public bool IncInsufficientForAmount { get; set; }
        //    public bool IncUnableToVerify { get; set; }
        //    public bool IncExcessiveObligations { get; set; }
        //    public bool ResTemporary { get; set; }
        //    public bool ResLength { get; set; }
        //    public bool ResUnableToVerify { get; set; }
        //    public bool DeniedHUD { get; set; }
        //    public bool DeniedVa { get; set; }
        //    public bool DeniedFannie { get; set; }
        //    public bool DeniedFreddie { get; set; }
        //    public bool DeniedOther { get; set; }
        //    public string DeniedOtherDesc { get; set; }
        //    public bool OtherInsufficientFundsToClose { get; set; }
        //    public bool OtherCreditApplicationIncomplete { get; set; }
        //    public bool OtherValueOrTypeOfCollateral { get; set; }
        //    public bool OtherUnacceptableProperty { get; set; }
        //    public bool OtherInsufficientDataProperty { get; set; }
        //    public bool OtherUnacceptableAppraisal { get; set; }
        //    public bool OtherUnacceptalbeLeasehold { get; set; }
        //    public bool OtherTermsAndConditions { get; set; }
        //    public bool OtherSpecify { get; set; }
        //    public string OtherDescription { get; set; }
        //    public string ActionTakenDescription { get; set; }
        //    public string AccountDescription { get; set; }
        //    public string CompanyName { get; set; }
        //    public string CompanyFullAddress { get; set; }
        //    public string CompanyCityStateZip { get; set; }
        //    public string CompanyStreet { get; set; }
        //    public string CompanyCity { get; set; }
        //    public string CompanyState { get; set; }
        //    public string CompanyZip { get; set; }
        //    public string CompanyPhone { get; set; }
        //    public bool CreditDecision1 { get; set; }
        //    public bool CreditDecision2 { get; set; }
        //    public int NoticeDeliveryMethod { get; set; }
        //    public string PreparedBy { get; set; }
        //    public bool Equifax { get; set; }
        //    public bool Experian { get; set; }
        //    public bool TransUnion { get; set; }
        //    public bool CreditNumberOfInquiries { get; set; }
        //    public long FileDataID { get; set; }

        //}

        //public class CA883
        //{
        //    public int CA883ID { get; set; }
        //    public object BrokerFee { get; set; }
        //    public int HasAdditionalComp { get; set; }
        //    public object AdditionalComp { get; set; }
        //    public object CreditDisabilityInsAmount { get; set; }
        //    public int PrepayPenaltyOption { get; set; }
        //    public int PrepayPenaltyPrincipalOption { get; set; }
        //    public object PrepayPenaltyMonths { get; set; }
        //    public int BrokerFundedOption { get; set; }
        //    public bool OmitLandAndAlts { get; set; }
        //    public bool OmitMIPFF { get; set; }
        //    public bool OmitSubFi { get; set; }
        //    public bool OmitPaidBySeller { get; set; }
        //    public bool OmitOtherCredits { get; set; }
        //    public object OtherDeduction { get; set; }
        //    public string OtherDeductionText { get; set; }
        //    public string PrepayPenaltyOtherDescription { get; set; }
        //    public bool HasLimitedDocumentation { get; set; }
        //    public object PrepayPenaltyTerm { get; set; }
        //    public object PrepayPenaltyMaxAmount { get; set; }
        //    public long FileDataID { get; set; }

        //}

        //public class IRSForm4506s
        //{
        //    public int IRSForm4506ID { get; set; }
        //    public int BorrowerID { get; set; }
        //    public int FilingStatus { get; set; }
        //    public string LastReturnFullAddress { get; set; }
        //    public string LastReturnCityStateZip { get; set; }
        //    public string LastReturnStreet { get; set; }
        //    public string LastReturnCity { get; set; }
        //    public string LastReturnState { get; set; }
        //    public string LastReturnZip { get; set; }
        //    public string ThirdPartyName { get; set; }
        //    public string ThirdPartyFullAddress { get; set; }
        //    public string ThirdPartyCityStateZip { get; set; }
        //    public string ThirdPartyStreet { get; set; }
        //    public string ThirdPartyCity { get; set; }
        //    public string ThirdPartyState { get; set; }
        //    public string ThirdPartyZip { get; set; }
        //    public string ThirdPartyPhone { get; set; }
        //    public string TaxFormRequested { get; set; }
        //    public string TaxPeriod1 { get; set; }
        //    public string TaxPeriod2 { get; set; }
        //    public string TaxPeriod3 { get; set; }
        //    public string TaxPeriod4 { get; set; }
        //    public bool ReturnTranscript { get; set; }
        //    public bool AccountTranscript { get; set; }
        //    public bool RecordOfAccount { get; set; }
        //    public bool VerificationOfNonFiling { get; set; }
        //    public bool FormW2EtcTranscript { get; set; }
        //    public string PhoneOV { get; set; }
        //    public bool OverrideReturnName { get; set; }
        //    public bool OverrideSpouse { get; set; }
        //    public bool OverrideCurrentName { get; set; }
        //    public bool OverrideCurrentAddress { get; set; }
        //    public string ReturnFirstNameOV { get; set; }
        //    public string ReturnMiddleNameOV { get; set; }
        //    public string ReturnLastNameOV { get; set; }
        //    public string ReturnSuffixOV { get; set; }
        //    public string ReturnSSNOV { get; set; }
        //    public string SpouseFirstNameOV { get; set; }
        //    public string SpouseMiddleNameOV { get; set; }
        //    public string SpouseLastNameOV { get; set; }
        //    public string SpouseSuffixOV { get; set; }
        //    public string SpouseSSNOV { get; set; }
        //    public string CurrentFirstNameOV { get; set; }
        //    public string CurrentMiddleNameOV { get; set; }
        //    public string CurrentLastNameOV { get; set; }
        //    public string CurrentSuffixOV { get; set; }
        //    public string CurrentStreetOV { get; set; }
        //    public string CurrentCityOV { get; set; }
        //    public string CurrentStateOV { get; set; }
        //    public string CurrentZipOV { get; set; }
        //    public int TRVForm1 { get; set; }
        //    public int TRVForm2 { get; set; }
        //    public int TRVForm3 { get; set; }
        //    public object TRVForm1Year1 { get; set; }
        //    public object TRVForm1Year2 { get; set; }
        //    public object TRVForm1Year3 { get; set; }
        //    public object TRVForm1Year4 { get; set; }
        //    public object TRVForm2Year1 { get; set; }
        //    public object TRVForm2Year2 { get; set; }
        //    public object TRVForm2Year3 { get; set; }
        //    public object TRVForm2Year4 { get; set; }
        //    public object TRVForm3Year1 { get; set; }
        //    public object TRVForm3Year2 { get; set; }
        //    public object TRVForm3Year3 { get; set; }
        //    public object TRVForm3Year4 { get; set; }
        //    public string DVOrderId1 { get; set; }
        //    public string DVOrderId2 { get; set; }
        //    public string DVOrderId3 { get; set; }
        //    public bool IdentityTheft { get; set; }
        //    public string IRSForm4506Guid { get; set; }
        //    public long FileDataID { get; set; }

        //}

        //public class IRSForm1098
        //{
        //    public int IRSForm1098ID { get; set; }
        //    public int AddressOption { get; set; }
        //    public string AccountNumber { get; set; }
        //    public object MortgageInterest { get; set; }
        //    public object Points { get; set; }
        //    public object Refund { get; set; }
        //    public object MIP { get; set; }
        //    public object OutstandingPrincipalOnJanFirst { get; set; }
        //    public long FileDataID { get; set; }

        //}

        //public class Transmittal
        //{
        //    public int TransmittalID { get; set; }
        //    public int IsPropType1UnitOV { get; set; }
        //    public int IsPropType2To4UnitsOV { get; set; }
        //    public int IsPropTypeCondoOV { get; set; }
        //    public int IsPropTypePUDOV { get; set; }
        //    public int IsPropTypeCoopOV { get; set; }
        //    public int IsPropTypeManuOV { get; set; }
        //    public int IsPropTypeSinglewideOV { get; set; }
        //    public int IsPropTypeMultiwideOV { get; set; }
        //    public int AmortTypeOV { get; set; }
        //    public string AmortTypeDescOV { get; set; }
        //    public int LoanPurposeOV { get; set; }
        //    public int OriginatorType { get; set; }
        //    public string BrokerName { get; set; }
        //    public object GrossIncomeBorOV { get; set; }
        //    public object GrossIncomeCoBorsOV { get; set; }
        //    public object GrossIncomeTotalOV { get; set; }
        //    public object OtherIncomeBorOV { get; set; }
        //    public object OtherIncomeCoBorsOV { get; set; }
        //    public object OtherIncomeTotalOV { get; set; }
        //    public object CashFlowIncomeBorOV { get; set; }
        //    public object CashFlowIncomeCoBorsOV { get; set; }
        //    public object CashFlowIncomeTotalOV { get; set; }
        //    public object TotalIncomeBorOV { get; set; }
        //    public object TotalIncomeCoBorsOV { get; set; }
        //    public object TotalIncomeTotalOV { get; set; }
        //    public object FirstRatioOV { get; set; }
        //    public object SecondRatioOV { get; set; }
        //    public object GapRatioOV { get; set; }
        //    public object LTVOV { get; set; }
        //    public object CLTVOV { get; set; }
        //    public object HCLTVOV { get; set; }
        //    public int AppraisalType { get; set; }
        //    public string AppraisalFormNo { get; set; }
        //    public int RiskAssessmentMethod { get; set; }
        //    public string RiskAssessmentMethodOther { get; set; }
        //    public string AUSRecommendation { get; set; }
        //    public string AUSFileID { get; set; }
        //    public string DocumentClassification { get; set; }
        //    public object RepCreditScore { get; set; }
        //    public object PrimaryResFirstMortPIOV { get; set; }
        //    public object PrimaryResSecondMortPIOV { get; set; }
        //    public object PrimaryResHazOV { get; set; }
        //    public object PrimaryResPropTaxesOV { get; set; }
        //    public object PrimaryResMIOV { get; set; }
        //    public object PrimaryResHODOV { get; set; }
        //    public object PrimaryResLeaseOV { get; set; }
        //    public object PrimaryResOtherHousingExpOV { get; set; }
        //    public object PrimaryResPITIOV { get; set; }
        //    public object SubPropNetCashFlowNegOV { get; set; }
        //    public object AllOtherPaymentsOV { get; set; }
        //    public object TotalAllPaymentsOV { get; set; }
        //    public object FundsReqToCloseOV { get; set; }
        //    public object VerifiedAssetsOV { get; set; }
        //    public object MonthsInReserveOV { get; set; }
        //    public object SalesConcessionsPercOV { get; set; }
        //    public string UWCom { get; set; }
        //    public int QualRateOption { get; set; }
        //    public string SellerNo { get; set; }
        //    public string InvestorLoanNo { get; set; }
        //    public string SellerLoanNo { get; set; }
        //    public string MasterCommitmentNo { get; set; }
        //    public string ContractNo { get; set; }
        //    public object ContractSignatureDate { get; set; }
        //    public object RequiredMonthsInResOV { get; set; }
        //    public string SpecialFeatureCode01 { get; set; }
        //    public string SpecialFeatureCode02 { get; set; }
        //    public string SpecialFeatureCode03 { get; set; }
        //    public string SpecialFeatureCode04 { get; set; }
        //    public string SpecialFeatureCode05 { get; set; }
        //    public string SpecialFeatureCode06 { get; set; }
        //    public string SpecialFeatureCode07 { get; set; }
        //    public string SpecialFeatureCode08 { get; set; }
        //    public string SpecialFeatureCode09 { get; set; }
        //    public string SpecialFeatureCode10 { get; set; }
        //    public int _MonthsInReserve { get; set; }
        //    public bool UseCommentsAddendum { get; set; }
        //    public object InterestedPartyContributions { get; set; }
        //    public object IncomeNetRentalOV { get; set; }
        //    public object SubPropFirstMortPIOV { get; set; }
        //    public object SubPropOtherFinancingPIOV { get; set; }
        //    public object SubPropOtherPaymentOV { get; set; }
        //    public long FileDataID { get; set; }

        //}

        //public class HMDA
        //{
        //    public int HMDAID { get; set; }
        //    public int PropertyType { get; set; }
        //    public object ActionDate { get; set; }
        //    public int ActionTaken { get; set; }
        //    public string CountyCode { get; set; }
        //    public string CensusTract { get; set; }
        //    public bool GrossAnnualIncomeNA { get; set; }
        //    public bool StateCodeNA { get; set; }
        //    public bool ApplicationDateNA { get; set; }
        //    public bool CountyCodeNA { get; set; }
        //    public bool CensusTractNA { get; set; }
        //    public bool MSANoNA { get; set; }
        //    public int _ApplicantEthnicity { get; set; }
        //    public int _CoApplicantEthnicity { get; set; }
        //    public int _ApplicantRace1 { get; set; }
        //    public int _ApplicantRace2 { get; set; }
        //    public int _ApplicantRace3 { get; set; }
        //    public int _ApplicantRace4 { get; set; }
        //    public int _ApplicantRace5 { get; set; }
        //    public int _CoApplicantRace1 { get; set; }
        //    public int _CoApplicantRace2 { get; set; }
        //    public int _CoApplicantRace3 { get; set; }
        //    public int _CoApplicantRace4 { get; set; }
        //    public int _CoApplicantRace5 { get; set; }
        //    public int _ApplicantSex { get; set; }
        //    public int _CoApplicantSex { get; set; }
        //    public int _Occupancy { get; set; }
        //    public string _GrossAnnualIncomeInThousands { get; set; }
        //    public object _ApplicationDate { get; set; }
        //    public object _CountyCodeDisplay { get; set; }
        //    public object _CensusTractDisplay { get; set; }
        //    public object _MSANo { get; set; }
        //    public string _StateCode { get; set; }
        //    public string DenialReasonOther { get; set; }
        //    public int AUSResult1 { get; set; }
        //    public int AUSResult2 { get; set; }
        //    public int AUSResult3 { get; set; }
        //    public int AUSResult4 { get; set; }
        //    public int AUSResult5 { get; set; }
        //    public int AUSSystem1 { get; set; }
        //    public int AUSSystem2 { get; set; }
        //    public int AUSSystem3 { get; set; }
        //    public int AUSSystem4 { get; set; }
        //    public int AUSSystem5 { get; set; }
        //    public string AUSResultOtherDesc { get; set; }
        //    public string AUSSystemOther { get; set; }
        //    public bool PropertyValueNA { get; set; }
        //    public object Term { get; set; }
        //    public bool TermNA { get; set; }
        //    public object TotalLoanCosts { get; set; }
        //    public bool TotalLoanCostsNA { get; set; }
        //    public object TotalPointsAndFees { get; set; }
        //    public bool TotalPointsAndFeesNA { get; set; }
        //    public object OriginationCharges { get; set; }
        //    public bool OriginationChargesNA { get; set; }
        //    public object DiscountPoints { get; set; }
        //    public bool DiscountPointsNA { get; set; }
        //    public object LenderCredits { get; set; }
        //    public bool LenderCreditsNA { get; set; }
        //    public object InterestRate { get; set; }
        //    public bool InterestRateNA { get; set; }
        //    public object DTIRatio { get; set; }
        //    public bool DTIRatioNA { get; set; }
        //    public object CLTV { get; set; }
        //    public bool CLTV_NA { get; set; }
        //    public object IntroductoryRatePeriod { get; set; }
        //    public bool IntroductoryRatePeriodNA { get; set; }
        //    public bool HasBalloonPayment { get; set; }
        //    public bool HasInterestOnlyPayments { get; set; }
        //    public bool HasOtherNonAmortizingFeature { get; set; }
        //    public string _ApplicantGMICombined { get; set; }
        //    public string _CoApplicantGMICombined { get; set; }
        //    public object ApplicantAge { get; set; }
        //    public object CoApplicantAge { get; set; }
        //    public object ApplicantCreditScore { get; set; }
        //    public int ApplicantCreditScoreModel { get; set; }
        //    public string ApplicantCreditScoreModelOther { get; set; }
        //    public int CoApplicantCreditScore { get; set; }
        //    public int CoApplicantCreditScoreModel { get; set; }
        //    public string CoApplicantCreditScoreModelOther { get; set; }
        //    public object PropertyValue { get; set; }
        //    public bool MLO_NMLSID_NA { get; set; }
        //    public bool PrepaymentPenaltyNA { get; set; }
        //    public object LoanAmount { get; set; }
        //    public bool IsPartiallyExemptFromHMDA { get; set; }
        //    public int InitiallyPayableToYourInstitutionOV { get; set; }
        //    public long FileDataID { get; set; }

        //}

        //public class FHA
        //{
        //    public int FHAID { get; set; }
        //    public object MIPRefund { get; set; }
        //    public object EnergyImp { get; set; }
        //    public string SectionOfActOV { get; set; }
        //    public object ReasonableValue { get; set; }
        //    public int FHALoanPurpose { get; set; }
        //    public string CreditDataAgent { get; set; }
        //    public int ReasonableValueType { get; set; }
        //    public int ReceivedLeadPaintNotice { get; set; }
        //    public int ApprovedOrModified { get; set; }
        //    public object ModifiedLoanWith { get; set; }
        //    public object ModifiedIntRate { get; set; }
        //    public object ModifiedTerm { get; set; }
        //    public object ModifiedPayment { get; set; }
        //    public object ModifiedUFMIP { get; set; }
        //    public object ModifiedMIPayment { get; set; }
        //    public object ModifiedMITerm { get; set; }
        //    public bool ProposedConstInCompliance { get; set; }
        //    public bool NewConstComplete { get; set; }
        //    public bool BuildersWarrantyReq { get; set; }
        //    public bool PropertyHas10YearWarranty { get; set; }
        //    public bool CodeInspectionReq { get; set; }
        //    public bool OONotRequired { get; set; }
        //    public bool HighLTVForNonOOInMilitary { get; set; }
        //    public bool OtherCondition { get; set; }
        //    public string OtherConditionDesc { get; set; }
        //    public int ScorecardRating { get; set; }
        //    public string MortgageeRep { get; set; }
        //    public string DEUnderwriter { get; set; }
        //    public string DUCHUMSID { get; set; }
        //    public int MortgageeHasFinancialRel { get; set; }
        //    public string PropertyJuris { get; set; }
        //    public int HasWaterSupplyOrSewageSys { get; set; }
        //    public string Section221D2CodeLetter { get; set; }
        //    public object MCC { get; set; }
        //    public object MaxLoanLTVFactorOV { get; set; }
        //    public object MIDurationMonthsOV { get; set; }
        //    public bool IsSponsoredOrigination { get; set; }
        //    public bool MaxLoanCanExceedCountyLimitML2011_29 { get; set; }
        //    public object OrigFHAEndorsementDate { get; set; }
        //    public bool AllConditionsSatisfied { get; set; }
        //    public int URLAAddendumRoleOfOfficerOV { get; set; }
        //    public long FileDataID { get; set; }

        //}

        //public class FHA203K
        //{
        //    public int FHA203KID { get; set; }
        //    public int HUDOwned { get; set; }
        //    public int CommitmentStage { get; set; }
        //    public int OccupancyType { get; set; }
        //    public object PurchaseDate { get; set; }
        //    public bool EscrowCommitment { get; set; }
        //    public object ExistingDebt { get; set; }
        //    public object AsIsValue { get; set; }
        //    public object AfterImprovedValue { get; set; }
        //    public object BorClosingCostsOV { get; set; }
        //    public object ContingencyPerc { get; set; }
        //    public object ContingencyPaidInCash { get; set; }
        //    public object CostOfRepairs { get; set; }
        //    public object InspectionCount { get; set; }
        //    public object InspectionCost { get; set; }
        //    public object TitleUpdateCount { get; set; }
        //    public object TitleUpdateCost { get; set; }
        //    public object MonthsEscrowed { get; set; }
        //    public object MortgagePayment { get; set; }
        //    public object ArchAndEngFees { get; set; }
        //    public object ConsultantFees { get; set; }
        //    public object PermitsAndOtherFees { get; set; }
        //    public object ReviewerMiles { get; set; }
        //    public object ReviewerCostPerMile { get; set; }
        //    public object ReviewerOtherFee { get; set; }
        //    public bool WaiveSuppOrigFee { get; set; }
        //    public object SuppOrigFeeOV { get; set; }
        //    public object DiscoutOnRepairs { get; set; }
        //    public object AllowableDownPayment { get; set; }
        //    public object PurRequiredInvOV { get; set; }
        //    public object PurMaxMortOV { get; set; }
        //    public object AllowableFinPrepaids { get; set; }
        //    public object DiscountOnLoan { get; set; }
        //    public object DiscountLoanBasis { get; set; }
        //    public object LineD4OV { get; set; }
        //    public object LineE1OV { get; set; }
        //    public object LineE2OV { get; set; }
        //    public object TotalEscrowedFunds { get; set; }
        //    public string Remarks { get; set; }
        //    public bool ShowDiscountInDollars { get; set; }
        //    public string DETitle { get; set; }
        //    public bool StreamlinedK { get; set; }
        //    public object FeasibilityStudyFee { get; set; }
        //    public object PurInducements { get; set; }
        //    public object LeadBasedPaintCredit { get; set; }
        //    public object MortgageLimit { get; set; }
        //    public object InitialBaseMortgageOV { get; set; }
        //    public object SolarWindCost { get; set; }
        //    public object MaterialCostForItemsOrderedPrepaid { get; set; }
        //    public object MaterialCostForItemsOrderedNotPrepaid { get; set; }
        //    public int FHA203KType { get; set; }
        //    public object DrawArchAndEngFees { get; set; }
        //    public object DrawPermitsAndOtherFees { get; set; }
        //    public object DrawSuppOrigFee { get; set; }
        //    public object DrawDiscountPointsAndFees { get; set; }
        //    public int PropertyAcquired { get; set; }
        //    public double _InitialBaseMortgage { get; set; }
        //    public object DiscountPointsAndFeesOV { get; set; }
        //    public object ContingencyTotalOV { get; set; }
        //    public long FileDataID { get; set; }

        //}

        //public class FHAForms
        //{
        //    public int FHAFormsID { get; set; }
        //    public int FairAnalysis { get; set; }
        //    public string FairAnalysisComments { get; set; }
        //    public string QualityComments { get; set; }
        //    public int ComparablesAcceptable { get; set; }
        //    public string ComparablesComments { get; set; }
        //    public int AdjustmentsAcceptable { get; set; }
        //    public string AdjustmentsComments { get; set; }
        //    public int ValueAcceptable { get; set; }
        //    public int ValueShouldBeCorrected { get; set; }
        //    public object ValueForFHAPurposes { get; set; }
        //    public string ValueCorrectionComments { get; set; }
        //    public string RepairConditions { get; set; }
        //    public string OtherAppraisalComments { get; set; }
        //    public object ActionDate { get; set; }
        //    public string CommitmentIssuedBy { get; set; }
        //    public object CommitmentIssued { get; set; }
        //    public object CommitmentExpires { get; set; }
        //    public int IsEligibleForMaxFi { get; set; }
        //    public bool HasAssuranceOfComp { get; set; }
        //    public object AssuranceCompAmount { get; set; }
        //    public bool HasAdditionaltemsAttached { get; set; }
        //    public string AdditionalConditions { get; set; }
        //    public bool ConditionsOnBackApply { get; set; }
        //    public long FileDataID { get; set; }

        //}

        //public class FHAMCAW
        //{
        //    public int FHAMCAWID { get; set; }
        //    public int ConstructionType { get; set; }
        //    public object StatutoryInvReqOV { get; set; }
        //    public object RequiredAdjustmentsOV { get; set; }
        //    public object SalesConcessionsOV { get; set; }
        //    public object MortgageBasisOV { get; set; }
        //    public object Refi10F1OV { get; set; }
        //    public object Refi10F2OV { get; set; }
        //    public object RefiRequiredInvestmentOV { get; set; }
        //    public object MinDownPaymentOV { get; set; }
        //    public object DiscountNotFinancedOV { get; set; }
        //    public object PrepaidsNotFinancedOV { get; set; }
        //    public object NonFinanceableRepairsOV { get; set; }
        //    public object NonRealtyOV { get; set; }
        //    public object TotalCashToCloseOV { get; set; }
        //    public object OtherCreditsOV { get; set; }
        //    public bool OtherCreditsCashOpt { get; set; }
        //    public bool OtherCreditsOtherOpt { get; set; }
        //    public object CashToCloseOV { get; set; }
        //    public bool AmountToBePaidCashOpt { get; set; }
        //    public bool AmountToBePaidOtherOpt { get; set; }
        //    public object AvailableAssetsOV { get; set; }
        //    public string SecondMortgageSource { get; set; }
        //    public object SecondMortgageOV { get; set; }
        //    public object BasePayBorOV { get; set; }
        //    public object OtherEarningsBorOV { get; set; }
        //    public object BasePayCoBorOV { get; set; }
        //    public object OtherEarningsCoBorOV { get; set; }
        //    public object NetRealEstateIncomeOV { get; set; }
        //    public object GrossMonthlyIncomeOV { get; set; }
        //    public object LTVOV { get; set; }
        //    public object FirstRatioOV { get; set; }
        //    public object SecondRatioOV { get; set; }
        //    public int CreditCharacteristics { get; set; }
        //    public int AdequacyOfIncome { get; set; }
        //    public int StabilityOfIncome { get; set; }
        //    public int AdequacyOfAssets { get; set; }
        //    public int TotalCCOption { get; set; }
        //    public object TotalCCOtherAmount { get; set; }
        //    public bool PurchasedWithinOneYear { get; set; }
        //    public object WeatherizationCosts { get; set; }
        //    public bool IncCCInExistingDebt { get; set; }
        //    public bool IncDPInExistingDebt { get; set; }
        //    public bool IncPPInExistingDebt { get; set; }
        //    public object OtherExistingDebtAmount { get; set; }
        //    public string Remarks { get; set; }
        //    public bool OrigCaseNoAssignedOnOrAfterJuly142008 { get; set; }
        //    public object NetRealEstateIncomeCoBorsOV { get; set; }
        //    public int AmortTypeOV { get; set; }
        //    public string SecondMortgageOtherDesc { get; set; }
        //    public int SecondMortgageSourceType { get; set; }
        //    public int Gift1SourceType { get; set; }
        //    public string Gift1OtherDesc { get; set; }
        //    public string Gift2Source { get; set; }
        //    public int Gift2SourceType { get; set; }
        //    public string Gift2OtherDesc { get; set; }
        //    public object Gift2Amount { get; set; }
        //    public int SellerFundedDAP { get; set; }
        //    public object CLTVOV { get; set; }
        //    public object SellerContributionOV { get; set; }
        //    public object MonthsInReserveFunds { get; set; }
        //    public int ScoredByTOTAL { get; set; }
        //    public int RiskClass { get; set; }
        //    public string AppraisalReviewerCHUMSNo { get; set; }
        //    public int PropertyTypeOV { get; set; }
        //    public string SourceOfFunds { get; set; }
        //    public bool EnergyEfficientMortgage { get; set; }
        //    public bool BuildingOnOwnLand { get; set; }
        //    public bool LoanPurposeOther { get; set; }
        //    public string LoanPurposeOtherDesc { get; set; }
        //    public int IntRateBuydownOV { get; set; }
        //    public int CaseNoAssignmentPeriod { get; set; }
        //    public object ClosingCostsAndPrepaidsPBAOV { get; set; }
        //    public long FileDataID { get; set; }

        //}

        //public class VA
        //{
        //    public int VAID { get; set; }
        //    public object VeteranBorrowerID { get; set; }
        //    public int VATitleVested { get; set; }
        //    public string VATitleVestedOtherDesc { get; set; }
        //    public object DateOfApproval { get; set; }
        //    public object DateApprovalExpires { get; set; }
        //    public int HasVADebt { get; set; }
        //    public object CashFromVeteran { get; set; }
        //    public object IRRRCCPlusPPOV { get; set; }
        //    public object Maintenance { get; set; }
        //    public object Utilities { get; set; }
        //    public object DownPaymentOV { get; set; }
        //    public object LiquidAssetsOV { get; set; }
        //    public int UtilitiesIncludedInPresPITI { get; set; }
        //    public object SpecialAssessments { get; set; }
        //    public int PastCreditRecord { get; set; }
        //    public int MeetCreditStandards { get; set; }
        //    public string LoanAnalysisRemarks { get; set; }
        //    public string Line36OtherDeductionsDesc { get; set; }
        //    public string Line39OtherIncomesDesc { get; set; }
        //    public object Line44SupportBalanceOV { get; set; }
        //    public int HaveFiledClaim { get; set; }
        //    public string CertMailingFullAddress { get; set; }
        //    public string CertMailingCityStateZip { get; set; }
        //    public string CertMailingStreet { get; set; }
        //    public string CertMailingCity { get; set; }
        //    public string CertMailingState { get; set; }
        //    public string CertMailingZip { get; set; }
        //    public int VeteranHasDisability { get; set; }
        //    public object LoanDisburementReportDate { get; set; }
        //    public string RelativeName { get; set; }
        //    public string RelativeFullAddress { get; set; }
        //    public string RelativeCityStateZip { get; set; }
        //    public string RelativeStreet { get; set; }
        //    public string RelativeCity { get; set; }
        //    public string RelativeState { get; set; }
        //    public string RelativeZip { get; set; }
        //    public string RelativePhone { get; set; }
        //    public int IssuanceOfEvidence { get; set; }
        //    public object FullyPaidOutDate { get; set; }
        //    public string LienTypeOther { get; set; }
        //    public object UnpaidSpecialAssessments { get; set; }
        //    public object AnnualMaintenanceAssessment { get; set; }
        //    public string NonRealtyAcquired { get; set; }
        //    public string AdditionalSecurity { get; set; }
        //    public int WithheldAmountDepositType { get; set; }
        //    public bool ConstructionCompletedProperly { get; set; }
        //    public string ApprovedUnderwriter { get; set; }
        //    public bool VetHasNotBeenDischarged { get; set; }
        //    public bool OmitGovInfoFromLoanDisb { get; set; }
        //    public object ApproxAnnualAssessmentOV { get; set; }
        //    public int LienTypeOV { get; set; }
        //    public object AmountWithheldFromProceeds { get; set; }
        //    public object ApproxAnnualRealEstateTaxesOV { get; set; }
        //    public object PreviousLoanAmount { get; set; }
        //    public object PreviousTerm { get; set; }
        //    public object PreviousMonthlyPI { get; set; }
        //    public object PreviousIntRate { get; set; }
        //    public int ShowLendersCert { get; set; }
        //    public string PreviousVALoanNo { get; set; }
        //    public object PrevLoanClosed { get; set; }
        //    public object PreviousMonthlyPITI { get; set; }
        //    public int ActiveDutyDayFollowingSeperation { get; set; }
        //    public string VeteranStatusFileRef { get; set; }
        //    public bool ServedUnderAnotherName { get; set; }
        //    public string OtherNamesUsedDuringMilitaryService { get; set; }
        //    public bool BorrowerHadPreviousVALoan { get; set; }
        //    public int PrevLoanMoreThan30DaysLateInPast6Mo { get; set; }
        //    public int RecommendedAction { get; set; }
        //    public int FinalAction { get; set; }
        //    public object CRVValue { get; set; }
        //    public object CRVExpirationDate { get; set; }
        //    public object CRVEconomicLife { get; set; }
        //    public int PreviousLoanType { get; set; }
        //    public string PreviousLoanTypeOtherDesc { get; set; }
        //    public object PreviousTotalOfPIAndMIPayments { get; set; }
        //    public bool NTBEliminatesMI { get; set; }
        //    public bool NTBIncreasesResidualIncome { get; set; }
        //    public bool NTBRefinancesConstLoan { get; set; }
        //    public int VACashOutRefiType { get; set; }
        //    public long FileDataID { get; set; }

        //}

        //public class VALoanSummary
        //{
        //    public int VALoanSummaryID { get; set; }
        //    public string EntitlementCode { get; set; }
        //    public object EntitlementAmount { get; set; }
        //    public int VAServiceBranch { get; set; }
        //    public int VAMilitaryStatus { get; set; }
        //    public int VALoanProcedure { get; set; }
        //    public int VALoanPurpose { get; set; }
        //    public int VALoanCode { get; set; }
        //    public int VAMortgageType { get; set; }
        //    public int VAHybridARMType { get; set; }
        //    public int VAOwnershipType { get; set; }
        //    public object EnergyImpAmount { get; set; }
        //    public bool EnergyImpNone { get; set; }
        //    public bool EnergyImpSolar { get; set; }
        //    public bool EnergyImpReplacement { get; set; }
        //    public bool EnergyImpAddition { get; set; }
        //    public bool EnergyImpInsulation { get; set; }
        //    public bool EnergyImpOther { get; set; }
        //    public int VAPropertyType { get; set; }
        //    public int VAAppraisalType { get; set; }
        //    public int VAStructureType { get; set; }
        //    public int VAPropertyDesignation { get; set; }
        //    public string MCRVNo { get; set; }
        //    public int VAManufacturedHomeType { get; set; }
        //    public string LenderSARID { get; set; }
        //    public object SARIssueDate { get; set; }
        //    public int AdjustedBySAR { get; set; }
        //    public int ProcessedWithAUS { get; set; }
        //    public int VAAUSSystem { get; set; }
        //    public int VARiskClassification { get; set; }
        //    public object VAMedianCreditScore { get; set; }
        //    public object ResidualIncomeGuideline { get; set; }
        //    public int ConsiderSpouseIncome { get; set; }
        //    public object SpouseIncome { get; set; }
        //    public int TotalDiscountOption { get; set; }
        //    public int VeteranDiscountOption { get; set; }
        //    public int VAFundingFeeExempt { get; set; }
        //    public string OriginalVALoanNo { get; set; }
        //    public object OriginalLoanAmount { get; set; }
        //    public object OriginalIntRate { get; set; }
        //    public string VALoanSummaryRemarks { get; set; }
        //    public object VADiscountPoints { get; set; }
        //    public object VADiscountAmount { get; set; }
        //    public object VADiscountPointVeteran { get; set; }
        //    public object VADiscountAmountVeteran { get; set; }
        //    public int VAPriorLoanType { get; set; }
        //    public long FileDataID { get; set; }

        //}

        //public class VAValue
        //{
        //    public int VAValueID { get; set; }
        //    public string TitleLimits { get; set; }
        //    public string FirmName { get; set; }
        //    public string FirmStreet { get; set; }
        //    public string FirmCityStateZip { get; set; }
        //    public string FirmComments { get; set; }
        //    public string LotDimensions { get; set; }
        //    public object LotSqFootage { get; set; }
        //    public object LotAcreage { get; set; }
        //    public bool IsIrregular { get; set; }
        //    public bool IsAcres { get; set; }
        //    public int ElectricUtil { get; set; }
        //    public int GasUtil { get; set; }
        //    public int WaterUtil { get; set; }
        //    public int SewerUtil { get; set; }
        //    public bool HasRange { get; set; }
        //    public bool HasRefrigerator { get; set; }
        //    public bool HasDishwasher { get; set; }
        //    public bool HasWasher { get; set; }
        //    public bool HasDryer { get; set; }
        //    public bool HasDisposal { get; set; }
        //    public bool HasVentFan { get; set; }
        //    public bool HasCarpet { get; set; }
        //    public bool HasOtherEquip { get; set; }
        //    public string OtherEquipDesc { get; set; }
        //    public int BuildingStatus { get; set; }
        //    public int BuildingType { get; set; }
        //    public int FactoryFab { get; set; }
        //    public object NoBuildings { get; set; }
        //    public object NoLivingUnits { get; set; }
        //    public int StreetAccess { get; set; }
        //    public int StreetMaint { get; set; }
        //    public int Warranty { get; set; }
        //    public string WarrantyProgram { get; set; }
        //    public string WarrantyExpires { get; set; }
        //    public string ConstCompleted { get; set; }
        //    public string Owner { get; set; }
        //    public int PropertyOccupancy { get; set; }
        //    public object Rent { get; set; }
        //    public string OccupantName { get; set; }
        //    public string OccupantPhone { get; set; }
        //    public string BrokerName { get; set; }
        //    public string BrokerPhone { get; set; }
        //    public string InspectionDate { get; set; }
        //    public bool CanInspectAM { get; set; }
        //    public bool CanInspectPM { get; set; }
        //    public string KeysAt { get; set; }
        //    public string InstNo { get; set; }
        //    public int InspectionBy { get; set; }
        //    public int Plans { get; set; }
        //    public string PlansCaseNo { get; set; }
        //    public string Comments { get; set; }
        //    public object Taxes { get; set; }
        //    public int MineralRightsReserved { get; set; }
        //    public string MineralRightsExpl { get; set; }
        //    public int LeaseType { get; set; }
        //    public string LeaseExpires { get; set; }
        //    public object GroundRent { get; set; }
        //    public int PurLotSep { get; set; }
        //    public int ContractAttached { get; set; }
        //    public string ContractNo { get; set; }
        //    public int UseLoanValue { get; set; }
        //    public int Signature { get; set; }
        //    public string RequestorTitle { get; set; }
        //    public string RequestorPhone { get; set; }
        //    public string AssignmentDate { get; set; }
        //    public bool OmitBuilder { get; set; }
        //    public string FirmEmail { get; set; }
        //    public string PointOfContactInfo { get; set; }
        //    public string BuilderVAID { get; set; }
        //    public long FileDataID { get; set; }

        //}

        //public class SalesTools
        //{
        //    public int SalesToolsID { get; set; }
        //    public int ComparisonLoanPurp { get; set; }
        //    public object ComparisonID1 { get; set; }
        //    public object ComparisonID2 { get; set; }
        //    public object ComparisonID3 { get; set; }
        //    public object PrequalID1 { get; set; }
        //    public object PrequalID2 { get; set; }
        //    public object PrequalID3 { get; set; }
        //    public object IncomeOV { get; set; }
        //    public object DebtsOV { get; set; }
        //    public object DesiredPITI { get; set; }
        //    public string ComparisonComments { get; set; }
        //    public string OpenHousePropertyDesc { get; set; }
        //    public string OpenHouseComments { get; set; }
        //    public string OpenHousePicture { get; set; }
        //    public int OpenHousePartyCat { get; set; }
        //    public object PlannerPICurrent { get; set; }
        //    public object PlannerPIProposed { get; set; }
        //    public object PlannerClosingCosts { get; set; }
        //    public bool CalcFlood { get; set; }
        //    public bool ShowTotalSavings { get; set; }
        //    public object CashToBorrower { get; set; }
        //    public string DebtConsolidationComments { get; set; }
        //    public bool ShowPIOnly { get; set; }
        //    public bool ComparisonShowAPR { get; set; }
        //    public bool OpenHouseShowAPR { get; set; }
        //    public bool DebtConsolidationShowAPR { get; set; }
        //    public string OpenHousePictureAgent { get; set; }
        //    public string OpenHousePictureLO { get; set; }
        //    public object AntiSteeringID1 { get; set; }
        //    public object AntiSteeringID2 { get; set; }
        //    public object AntiSteeringID3 { get; set; }
        //    public object AntiSteeringID4 { get; set; }
        //    public string AntiSteeringChoiceExplanation { get; set; }
        //    public string AntiSteeringOption4Title { get; set; }
        //    public int AntiSteeringSelection { get; set; }
        //    public long FileDataID { get; set; }

        //}

        //public class MiscForms
        //{
        //    public int MiscFormsID { get; set; }
        //    public int MBCBrokerRepresents { get; set; }
        //    public int MBCShopOption { get; set; }
        //    public object MBCNumberOfLenders { get; set; }
        //    public string MBCCompAmount { get; set; }
        //    public string MBCCompNoGreaterThan { get; set; }
        //    public string MBCPoints { get; set; }
        //    public string MBCBorFee { get; set; }
        //    public string MBCLenderFee { get; set; }
        //    public object FLBANetProceedsOV { get; set; }
        //    public object FLBABrokerageFeeOV { get; set; }
        //    public object FLBAAppDepositOV { get; set; }
        //    public object FLBAThirdPartyFee { get; set; }
        //    public bool FLBAIsAppDepositRefundable { get; set; }
        //    public string MANoteExpDate { get; set; }
        //    public object MAAppFee { get; set; }
        //    public string FLECBrokerName { get; set; }
        //    public string NYAdvisedDesc1OV { get; set; }
        //    public string NYAdvisedDesc2OV { get; set; }
        //    public string NYProcessingAssistantTextOV { get; set; }
        //    public bool NYLenderFees { get; set; }
        //    public string NYLenderFeesPercent { get; set; }
        //    public string NYLenderFeesDollar { get; set; }
        //    public string NYLenderFeesPoints { get; set; }
        //    public bool NYFeeUnkown { get; set; }
        //    public string NYFeeUnknownPoints { get; set; }
        //    public bool NYFeesFromLoan { get; set; }
        //    public string NYFeesFromLoanPercent { get; set; }
        //    public string NYFeesFromLoanDollar { get; set; }
        //    public bool NYFeesDirect { get; set; }
        //    public string NYFeesDirectCommitment { get; set; }
        //    public string NYFeesDirectClosing { get; set; }
        //    public string NYFeesDirectPercent { get; set; }
        //    public string NYFeesDirectDollar { get; set; }
        //    public object NYApplicationFeeOV { get; set; }
        //    public object NYAppraisalFeeOV { get; set; }
        //    public object NYCreditFeeOV { get; set; }
        //    public string NYApplicationFeeRefundableOV { get; set; }
        //    public string NYRefundDesc1OV { get; set; }
        //    public string NYRefundDesc2OV { get; set; }
        //    public object NYProcFeeOV { get; set; }
        //    public string NYFeeDivisionName1 { get; set; }
        //    public string NYFeeDivisionName2 { get; set; }
        //    public string NYFeeDivisionFee1 { get; set; }
        //    public string NYFeeDivisionFee2 { get; set; }
        //    public string NYFeeDivisionFee3 { get; set; }
        //    public string NYFeeDivisionFee4 { get; set; }
        //    public string NYContactName { get; set; }
        //    public string NYContactPhone { get; set; }
        //    public string NYContactPhoneCollect { get; set; }
        //    public string NYContactPhoneTollFree { get; set; }
        //    public string NYDispositionStatusOV { get; set; }
        //    public object NYDispositionDateOV { get; set; }
        //    public object NYBrokerFeeAmountLender { get; set; }
        //    public object NYBrokerFeeAmountBorrower { get; set; }
        //    public object NYBrokerFeeAmountOther1 { get; set; }
        //    public object NYBrokerFeeAmountOther2 { get; set; }
        //    public string NYBrokerFeeDescOther1 { get; set; }
        //    public string NYBrokerFeeDescOther2 { get; set; }
        //    public string SSA89AgentOV { get; set; }
        //    public string FLInfoDisclosureBranchPhoneOV { get; set; }
        //    public int SSA89CompanyOption { get; set; }
        //    public long FileDataID { get; set; }

        //}

        //public class TXForms
        //{
        //    public int TXFormsID { get; set; }
        //    public string BrokerOrLO { get; set; }
        //    public bool SubmitToLender { get; set; }
        //    public bool IndependentContractor { get; set; }
        //    public bool ActingAsFollows { get; set; }
        //    public string ActingAsFollowsText { get; set; }
        //    public bool RetailPrice { get; set; }
        //    public bool WholesaleOptions { get; set; }
        //    public object FeeAmount { get; set; }
        //    public object ApplicationFee { get; set; }
        //    public object ProcessingFee { get; set; }
        //    public object AppraisalFee { get; set; }
        //    public object CreditReportFee { get; set; }
        //    public object AUFee { get; set; }
        //    public string OtherFee1Desc { get; set; }
        //    public object OtherFee1Amount { get; set; }
        //    public string OtherFee2Desc { get; set; }
        //    public object OtherFee2Amount { get; set; }
        //    public object NonRefundableAmount { get; set; }
        //    public string LicensedEntity { get; set; }
        //    public string LicenseNo { get; set; }
        //    public bool PricingBasedOnCustomFlag { get; set; }
        //    public string PricingBasedOnCustomDesc { get; set; }
        //    public bool EstFeesShownOnGFEFlag { get; set; }
        //    public long FileDataID { get; set; }

        //}

        public class CustomFields
        {
            public int CustomFieldsID { get; set; }
            public string Field01 { get; set; }
            public string Field02 { get; set; }
            public string Field03 { get; set; }
            public string Field04 { get; set; }
            public string Field05 { get; set; }
            public string Field06 { get; set; }
            public string Field07 { get; set; }
            public string Field08 { get; set; }
            public string Field09 { get; set; }
            public string Field10 { get; set; }
            public string Field11 { get; set; }
            public string Field12 { get; set; }
            public string Field13 { get; set; }
            public string Field14 { get; set; }
            public string Field15 { get; set; }
            public string Field16 { get; set; }
            public string Field17 { get; set; }
            public string Field18 { get; set; }
            public string Field19 { get; set; }
            public string Field20 { get; set; }
            public string Field21 { get; set; }
            public string Field22 { get; set; }
            public string Field23 { get; set; }
            public string Field24 { get; set; }
            public string Field25 { get; set; }
            public string Field26 { get; set; }
            public string Field27 { get; set; }
            public string Field28 { get; set; }
            public string Field29 { get; set; }
            public string Field30 { get; set; }
            public string Field31 { get; set; }
            public string Field32 { get; set; }
            public string Field33 { get; set; }
            public string Field34 { get; set; }
            public string Field35 { get; set; }
            public string Field36 { get; set; }
            public string Field37 { get; set; }
            public string Field38 { get; set; }
            public string Field39 { get; set; }
            public string Field40 { get; set; }
            public string Field41 { get; set; }
            public string Field42 { get; set; }
            public string Field43 { get; set; }
            public string Field44 { get; set; }
            public string Field45 { get; set; }
            public string Field46 { get; set; }
            public string Field47 { get; set; }
            public string Field48 { get; set; }
            public string Field49 { get; set; }
            public string Field50 { get; set; }
            public string Field51 { get; set; }
            public string Field52 { get; set; }
            public string Field53 { get; set; }
            public string Field54 { get; set; }
            public string Field55 { get; set; }
            public string Field56 { get; set; }
            public string Field57 { get; set; }
            public string Field58 { get; set; }
            public string Field59 { get; set; }
            public string Field60 { get; set; }
            public string Field61 { get; set; }
            public string Field62 { get; set; }
            public string Field63 { get; set; }
            public string Field64 { get; set; }
            public string Field65 { get; set; }
            public string Field66 { get; set; }
            public string Field67 { get; set; }
            public string Field68 { get; set; }
            public string Field69 { get; set; }
            public string Field70 { get; set; }
            public string Field71 { get; set; }
            public string Field72 { get; set; }
            public string Field73 { get; set; }
            public string Field74 { get; set; }
            public string Field75 { get; set; }
            public string Field76 { get; set; }
            public string Field77 { get; set; }
            public string Field78 { get; set; }
            public string Field79 { get; set; }
            public string Field80 { get; set; }
            public string Field81 { get; set; }
            public string Field82 { get; set; }
            public string Field83 { get; set; }
            public string Field84 { get; set; }
            public string Field85 { get; set; }
            public string Field86 { get; set; }
            public string Field87 { get; set; }
            public string Field88 { get; set; }
            public string Field89 { get; set; }
            public string Field90 { get; set; }
            public string Field91 { get; set; }
            public string Field92 { get; set; }
            public string Field93 { get; set; }
            public string Field94 { get; set; }
            public string Field95 { get; set; }
            public string Field96 { get; set; }
            public string Field97 { get; set; }
            public string Field98 { get; set; }
            public string Field99 { get; set; }
            public string Field100 { get; set; }
            public string Field101 { get; set; }
            public string Field102 { get; set; }
            public string Field103 { get; set; }
            public string Field104 { get; set; }
            public string Field105 { get; set; }
            public string Field106 { get; set; }
            public string Field107 { get; set; }
            public string Field108 { get; set; }
            public string Field109 { get; set; }
            public string Field110 { get; set; }
            public string Field111 { get; set; }
            public string Field112 { get; set; }
            public string Field113 { get; set; }
            public string Field114 { get; set; }
            public string Field115 { get; set; }
            public string Field116 { get; set; }
            public string Field117 { get; set; }
            public string Field118 { get; set; }
            public string Field119 { get; set; }
            public string Field120 { get; set; }
            public string Field121 { get; set; }
            public string Field122 { get; set; }
            public string Field123 { get; set; }
            public string Field124 { get; set; }
            public string Field125 { get; set; }
            public string Field126 { get; set; }
            public string Field127 { get; set; }
            public string Field128 { get; set; }
            public string Field129 { get; set; }
            public string Field130 { get; set; }
            public string Field131 { get; set; }
            public string Field132 { get; set; }
            public string Field133 { get; set; }
            public string Field134 { get; set; }
            public string Field135 { get; set; }
            public string Field136 { get; set; }
            public string Field137 { get; set; }
            public string Field138 { get; set; }
            public string Field139 { get; set; }
            public string Field140 { get; set; }
            public string Field141 { get; set; }
            public string Field142 { get; set; }
            public string Field143 { get; set; }
            public string Field144 { get; set; }
            public string Field145 { get; set; }
            public string Field146 { get; set; }
            public string Field147 { get; set; }
            public string Field148 { get; set; }
            public string Field149 { get; set; }
            public string Field150 { get; set; }
            public string Field151 { get; set; }
            public string Field152 { get; set; }
            public string Field153 { get; set; }
            public string Field154 { get; set; }
            public string Field155 { get; set; }
            public string Field156 { get; set; }
            public string Field157 { get; set; }
            public string Field158 { get; set; }
            public string Field159 { get; set; }
            public string Field160 { get; set; }
            public string Field161 { get; set; }
            public string Field162 { get; set; }
            public string Field163 { get; set; }
            public string Field164 { get; set; }
            public string Field165 { get; set; }
            public string Field166 { get; set; }
            public string Field167 { get; set; }
            public string Field168 { get; set; }
            public string Field169 { get; set; }
            public string Field170 { get; set; }
            public string Field171 { get; set; }
            public string Field172 { get; set; }
            public string Field173 { get; set; }
            public string Field174 { get; set; }
            public string Field175 { get; set; }
            public string Field176 { get; set; }
            public string Field177 { get; set; }
            public string Field178 { get; set; }
            public string Field179 { get; set; }
            public string Field180 { get; set; }
            public string Field181 { get; set; }
            public string Field182 { get; set; }
            public string Field183 { get; set; }
            public string Field184 { get; set; }
            public string Field185 { get; set; }
            public string Field186 { get; set; }
            public string Field187 { get; set; }
            public string Field188 { get; set; }
            public string Field189 { get; set; }
            public string Field190 { get; set; }
            public string Field191 { get; set; }
            public string Field192 { get; set; }
            public string Field193 { get; set; }
            public string Field194 { get; set; }
            public string Field195 { get; set; }
            public string Field196 { get; set; }
            public string Field197 { get; set; }
            public string Field198 { get; set; }
            public string Field199 { get; set; }
            public string Field200 { get; set; }
            public string Field201 { get; set; }
            public string Field202 { get; set; }
            public string Field203 { get; set; }
            public string Field204 { get; set; }
            public string Field205 { get; set; }
            public string Field206 { get; set; }
            public string Field207 { get; set; }
            public string Field208 { get; set; }
            public string Field209 { get; set; }
            public string Field210 { get; set; }
            public string Field211 { get; set; }
            public string Field212 { get; set; }
            public string Field213 { get; set; }
            public string Field214 { get; set; }
            public string Field215 { get; set; }
            public string Field216 { get; set; }
            public string Field217 { get; set; }
            public string Field218 { get; set; }
            public string Field219 { get; set; }
            public string Field220 { get; set; }
            public string Field221 { get; set; }
            public string Field222 { get; set; }
            public string Field223 { get; set; }
            public string Field224 { get; set; }
            public string Field225 { get; set; }
            public string Field226 { get; set; }
            public string Field227 { get; set; }
            public string Field228 { get; set; }
            public string Field229 { get; set; }
            public string Field230 { get; set; }
            public string Field231 { get; set; }
            public string Field232 { get; set; }
            public string Field233 { get; set; }
            public string Field234 { get; set; }
            public string Field235 { get; set; }
            public string Field236 { get; set; }
            public string Field237 { get; set; }
            public string Field238 { get; set; }
            public string Field239 { get; set; }
            public string Field240 { get; set; }
            public string Field241 { get; set; }
            public string Field242 { get; set; }
            public string Field243 { get; set; }
            public string Field244 { get; set; }
            public string Field245 { get; set; }
            public string Field246 { get; set; }
            public string Field247 { get; set; }
            public string Field248 { get; set; }
            public string Field249 { get; set; }
            public string Field250 { get; set; }
            public string Field251 { get; set; }
            public string Field252 { get; set; }
            public string Field253 { get; set; }
            public string Field254 { get; set; }
            public string Field255 { get; set; }
            public string Field256 { get; set; }
            public string Field257 { get; set; }
            public string Field258 { get; set; }
            public string Field259 { get; set; }
            public string Field260 { get; set; }
            public string Field261 { get; set; }
            public string Field262 { get; set; }
            public string Field263 { get; set; }
            public string Field264 { get; set; }
            public string Field265 { get; set; }
            public string Field266 { get; set; }
            public string Field267 { get; set; }
            public string Field268 { get; set; }
            public string Field269 { get; set; }
            public string Field270 { get; set; }
            public string Field271 { get; set; }
            public string Field272 { get; set; }
            public string Field273 { get; set; }
            public string Field274 { get; set; }
            public string Field275 { get; set; }
            public string Field276 { get; set; }
            public string Field277 { get; set; }
            public string Field278 { get; set; }
            public string Field279 { get; set; }
            public string Field280 { get; set; }
            public string Field281 { get; set; }
            public string Field282 { get; set; }
            public string Field283 { get; set; }
            public string Field284 { get; set; }
            public string Field285 { get; set; }
            public string Field286 { get; set; }
            public string Field287 { get; set; }
            public string Field288 { get; set; }
            public string Field289 { get; set; }
            public string Field290 { get; set; }
            public string Field291 { get; set; }
            public string Field292 { get; set; }
            public string Field293 { get; set; }
            public string Field294 { get; set; }
            public string Field295 { get; set; }
            public string Field296 { get; set; }
            public string Field297 { get; set; }
            public string Field298 { get; set; }
            public string Field299 { get; set; }
            public string Field300 { get; set; }
            public string Field301 { get; set; }
            public string Field302 { get; set; }
            public string Field303 { get; set; }
            public string Field304 { get; set; }
            public string Field305 { get; set; }
            public string Field306 { get; set; }
            public string Field307 { get; set; }
            public string Field308 { get; set; }
            public string Field309 { get; set; }
            public string Field310 { get; set; }
            public string Field311 { get; set; }
            public string Field312 { get; set; }
            public string Field313 { get; set; }
            public string Field314 { get; set; }
            public string Field315 { get; set; }
            public string Field316 { get; set; }
            public string Field317 { get; set; }
            public string Field318 { get; set; }
            public string Field319 { get; set; }
            public string Field320 { get; set; }
            public string Field321 { get; set; }
            public string Field322 { get; set; }
            public string Field323 { get; set; }
            public string Field324 { get; set; }
            public string Field325 { get; set; }
            public string Field326 { get; set; }
            public string Field327 { get; set; }
            public string Field328 { get; set; }
            public string Field329 { get; set; }
            public string Field330 { get; set; }
            public string Field331 { get; set; }
            public string Field332 { get; set; }
            public string Field333 { get; set; }
            public string Field334 { get; set; }
            public string Field335 { get; set; }
            public string Field336 { get; set; }
            public string Field337 { get; set; }
            public string Field338 { get; set; }
            public string Field339 { get; set; }
            public string Field340 { get; set; }
            public string Field341 { get; set; }
            public string Field342 { get; set; }
            public string Field343 { get; set; }
            public string Field344 { get; set; }
            public string Field345 { get; set; }
            public string Field346 { get; set; }
            public string Field347 { get; set; }
            public string Field348 { get; set; }
            public string Field349 { get; set; }
            public string Field350 { get; set; }
            public string Field351 { get; set; }
            public string Field352 { get; set; }
            public string Field353 { get; set; }
            public string Field354 { get; set; }
            public string Field355 { get; set; }
            public string Field356 { get; set; }
            public string Field357 { get; set; }
            public string Field358 { get; set; }
            public string Field359 { get; set; }
            public string Field360 { get; set; }
            public string Field361 { get; set; }
            public string Field362 { get; set; }
            public string Field363 { get; set; }
            public string Field364 { get; set; }
            public string Field365 { get; set; }
            public string Field366 { get; set; }
            public string Field367 { get; set; }
            public string Field368 { get; set; }
            public string Field369 { get; set; }
            public string Field370 { get; set; }
            public string Field371 { get; set; }
            public string Field372 { get; set; }
            public string Field373 { get; set; }
            public string Field374 { get; set; }
            public string Field375 { get; set; }
            public string Field376 { get; set; }
            public string Field377 { get; set; }
            public string Field378 { get; set; }
            public string Field379 { get; set; }
            public string Field380 { get; set; }
            public string Field381 { get; set; }
            public string Field382 { get; set; }
            public string Field383 { get; set; }
            public string Field384 { get; set; }
            public string Field385 { get; set; }
            public string Field386 { get; set; }
            public string Field387 { get; set; }
            public string Field388 { get; set; }
            public string Field389 { get; set; }
            public string Field390 { get; set; }
            public string Field391 { get; set; }
            public string Field392 { get; set; }
            public string Field393 { get; set; }
            public string Field394 { get; set; }
            public string Field395 { get; set; }
            public string Field396 { get; set; }
            public string Field397 { get; set; }
            public string Field398 { get; set; }
            public string Field399 { get; set; }
            public string Field400 { get; set; }
            public string Field401 { get; set; }
            public string Field402 { get; set; }
            public string Field403 { get; set; }
            public string Field404 { get; set; }
            public string Field405 { get; set; }
            public string Field406 { get; set; }
            public string Field407 { get; set; }
            public string Field408 { get; set; }
            public string Field409 { get; set; }
            public string Field410 { get; set; }
            public string Field411 { get; set; }
            public string Field412 { get; set; }
            public string Field413 { get; set; }
            public string Field414 { get; set; }
            public string Field415 { get; set; }
            public string Field416 { get; set; }
            public string Field417 { get; set; }
            public string Field418 { get; set; }
            public string Field419 { get; set; }
            public string Field420 { get; set; }
            public string Field421 { get; set; }
            public string Field422 { get; set; }
            public string Field423 { get; set; }
            public string Field424 { get; set; }
            public string Field425 { get; set; }
            public string Field426 { get; set; }
            public string Field427 { get; set; }
            public string Field428 { get; set; }
            public string Field429 { get; set; }
            public string Field430 { get; set; }
            public string Field431 { get; set; }
            public string Field432 { get; set; }
            public string Field433 { get; set; }
            public string Field434 { get; set; }
            public string Field435 { get; set; }
            public string Field436 { get; set; }
            public string Field437 { get; set; }
            public string Field438 { get; set; }
            public string Field439 { get; set; }
            public string Field440 { get; set; }
            public string Field441 { get; set; }
            public string Field442 { get; set; }
            public string Field443 { get; set; }
            public string Field444 { get; set; }
            public string Field445 { get; set; }
            public string Field446 { get; set; }
            public string Field447 { get; set; }
            public string Field448 { get; set; }
            public string Field449 { get; set; }
            public string Field450 { get; set; }
            public string Field451 { get; set; }
            public string Field452 { get; set; }
            public string Field453 { get; set; }
            public string Field454 { get; set; }
            public string Field455 { get; set; }
            public string Field456 { get; set; }
            public string Field457 { get; set; }
            public string Field458 { get; set; }
            public string Field459 { get; set; }
            public string Field460 { get; set; }
            public string Field461 { get; set; }
            public string Field462 { get; set; }
            public string Field463 { get; set; }
            public string Field464 { get; set; }
            public string Field465 { get; set; }
            public string Field466 { get; set; }
            public string Field467 { get; set; }
            public string Field468 { get; set; }
            public string Field469 { get; set; }
            public string Field470 { get; set; }
            public string Field471 { get; set; }
            public string Field472 { get; set; }
            public string Field473 { get; set; }
            public string Field474 { get; set; }
            public string Field475 { get; set; }
            public string Field476 { get; set; }
            public string Field477 { get; set; }
            public string Field478 { get; set; }
            public string Field479 { get; set; }
            public string Field480 { get; set; }
            public string Field481 { get; set; }
            public string Field482 { get; set; }
            public string Field483 { get; set; }
            public string Field484 { get; set; }
            public string Field485 { get; set; }
            public string Field486 { get; set; }
            public string Field487 { get; set; }
            public string Field488 { get; set; }
            public string Field489 { get; set; }
            public string Field490 { get; set; }
            public string Field491 { get; set; }
            public string Field492 { get; set; }
            public string Field493 { get; set; }
            public string Field494 { get; set; }
            public string Field495 { get; set; }
            public string Field496 { get; set; }
            public string Field497 { get; set; }
            public string Field498 { get; set; }
            public string Field499 { get; set; }
            public string Field500 { get; set; }
            public long FileDataID { get; set; }
        }

        //public class HUD1
        //{
        //    public int HUD1ID { get; set; }
        //    public int LoanID { get; set; }
        //    public string FileNo { get; set; }
        //    public int MortgageType { get; set; }
        //    public string BuilderOrSellerTIN { get; set; }
        //    public string SettlementAgentTIN { get; set; }
        //    public string SettlementText { get; set; }
        //    public object InterimInterestDateFrom { get; set; }
        //    public object InterimInterestDateTo { get; set; }
        //    public object MIMonthsOV { get; set; }
        //    public object LenderCoverage { get; set; }
        //    public string Line905Desc { get; set; }
        //    public object OwnersCoverage { get; set; }
        //    public string Line1501Name { get; set; }
        //    public object Line1501Amount { get; set; }
        //    public string Line1502Name { get; set; }
        //    public object Line1502Amount { get; set; }
        //    public string Line1503Name { get; set; }
        //    public object Line1503Amount { get; set; }
        //    public string Line1504Name { get; set; }
        //    public object Line1504Amount { get; set; }
        //    public string Line1505Name { get; set; }
        //    public object Line1505Amount { get; set; }
        //    public string Line1506Name { get; set; }
        //    public object Line1506Amount { get; set; }
        //    public string Line1507Name { get; set; }
        //    public object Line1507Amount { get; set; }
        //    public string Line1508Name { get; set; }
        //    public object Line1508Amount { get; set; }
        //    public string Line1509Name { get; set; }
        //    public object Line1509Amount { get; set; }
        //    public string Line1510Name { get; set; }
        //    public object Line1510Amount { get; set; }
        //    public string Line1511Name { get; set; }
        //    public object Line1511Amount { get; set; }
        //    public string Line1512Name { get; set; }
        //    public object Line1512Amount { get; set; }
        //    public string Line1513Name { get; set; }
        //    public object Line1513Amount { get; set; }
        //    public string Line1514Name { get; set; }
        //    public object Line1514Amount { get; set; }
        //    public string Line1515Name { get; set; }
        //    public object Line1515Amount { get; set; }
        //    public int LoanAmountOption { get; set; }
        //    public object CashFromBorrower { get; set; }
        //    public object Line1008MonthsInReserve { get; set; }
        //    public object Line1008Payment { get; set; }
        //    public string Line1107ItemNumbers { get; set; }
        //    public string Line1108ItemNumbers { get; set; }
        //    public object Line102Amount { get; set; }
        //    public string Line104Desc { get; set; }
        //    public object Line104Amount { get; set; }
        //    public string Line105Desc { get; set; }
        //    public object Line105Amount { get; set; }
        //    public object Line106DateFrom { get; set; }
        //    public object Line106DateTo { get; set; }
        //    public object Line106Amount { get; set; }
        //    public object Line107DateFrom { get; set; }
        //    public object Line107DateTo { get; set; }
        //    public object Line107Amount { get; set; }
        //    public object Line108DateFrom { get; set; }
        //    public object Line108DateTo { get; set; }
        //    public object Line108Amount { get; set; }
        //    public string Line109Desc { get; set; }
        //    public object Line109Amount { get; set; }
        //    public string Line110Desc { get; set; }
        //    public object Line110Amount { get; set; }
        //    public string Line111Desc { get; set; }
        //    public object Line111Amount { get; set; }
        //    public string Line112Desc { get; set; }
        //    public object Line112Amount { get; set; }
        //    public object TotalFromBorrowerOV { get; set; }
        //    public object Line201Amount { get; set; }
        //    public object Line202Amount { get; set; }
        //    public object Line203Amount { get; set; }
        //    public string Line204Desc { get; set; }
        //    public object Line204Amount { get; set; }
        //    public bool Line204PaidByOthers { get; set; }
        //    public string Line205Desc { get; set; }
        //    public object Line205Amount { get; set; }
        //    public bool Line205PaidByOthers { get; set; }
        //    public string Line206Desc { get; set; }
        //    public object Line206Amount { get; set; }
        //    public bool Line206PaidByOthers { get; set; }
        //    public string Line207Desc { get; set; }
        //    public object Line207Amount { get; set; }
        //    public bool Line207PaidByOthers { get; set; }
        //    public string Line208Desc { get; set; }
        //    public object Line208Amount { get; set; }
        //    public bool Line208PaidByOthers { get; set; }
        //    public string Line209Desc { get; set; }
        //    public object Line209Amount { get; set; }
        //    public bool Line209PaidByOthers { get; set; }
        //    public object Line210DateFrom { get; set; }
        //    public object Line210DateTo { get; set; }
        //    public object Line210Amount { get; set; }
        //    public object Line211DateFrom { get; set; }
        //    public object Line211DateTo { get; set; }
        //    public object Line211Amount { get; set; }
        //    public object Line212DateFrom { get; set; }
        //    public object Line212DateTo { get; set; }
        //    public object Line212Amount { get; set; }
        //    public string Line213Desc { get; set; }
        //    public object Line213Amount { get; set; }
        //    public string Line214Desc { get; set; }
        //    public object Line214Amount { get; set; }
        //    public string Line215Desc { get; set; }
        //    public object Line215Amount { get; set; }
        //    public string Line216Desc { get; set; }
        //    public object Line216Amount { get; set; }
        //    public string Line217Desc { get; set; }
        //    public object Line217Amount { get; set; }
        //    public string Line218Desc { get; set; }
        //    public object Line218Amount { get; set; }
        //    public string Line219Desc { get; set; }
        //    public object Line219Amount { get; set; }
        //    public object TotalByBorrowerOV { get; set; }
        //    public object TotalSettlementBorrowerOV { get; set; }
        //    public object Line402Amount { get; set; }
        //    public string Line403Desc { get; set; }
        //    public object Line403Amount { get; set; }
        //    public string Line404Desc { get; set; }
        //    public object Line404Amount { get; set; }
        //    public string Line405Desc { get; set; }
        //    public object Line405Amount { get; set; }
        //    public object Line406DateFrom { get; set; }
        //    public object Line406DateTo { get; set; }
        //    public object Line406Amount { get; set; }
        //    public object Line407DateFrom { get; set; }
        //    public object Line407DateTo { get; set; }
        //    public object Line407Amount { get; set; }
        //    public object Line408DateFrom { get; set; }
        //    public object Line408DateTo { get; set; }
        //    public object Line408Amount { get; set; }
        //    public string Line409Desc { get; set; }
        //    public object Line409Amount { get; set; }
        //    public string Line410Desc { get; set; }
        //    public object Line410Amount { get; set; }
        //    public string Line411Desc { get; set; }
        //    public object Line411Amount { get; set; }
        //    public string Line412Desc { get; set; }
        //    public object Line412Amount { get; set; }
        //    public object TotalFromSellerOV { get; set; }
        //    public object Line501Amount { get; set; }
        //    public object Line503Amount { get; set; }
        //    public object Line504Amount { get; set; }
        //    public object Line505Amount { get; set; }
        //    public string Line506Desc { get; set; }
        //    public object Line506Amount { get; set; }
        //    public string Line507Desc { get; set; }
        //    public object Line507Amount { get; set; }
        //    public string Line508Desc { get; set; }
        //    public object Line508Amount { get; set; }
        //    public string Line509Desc { get; set; }
        //    public object Line509Amount { get; set; }
        //    public object Line510DateFrom { get; set; }
        //    public object Line510DateTo { get; set; }
        //    public object Line510Amount { get; set; }
        //    public object Line511DateFrom { get; set; }
        //    public object Line511DateTo { get; set; }
        //    public object Line511Amount { get; set; }
        //    public object Line512DateFrom { get; set; }
        //    public object Line512DateTo { get; set; }
        //    public object Line512Amount { get; set; }
        //    public string Line513Desc { get; set; }
        //    public object Line513Amount { get; set; }
        //    public string Line514Desc { get; set; }
        //    public object Line514Amount { get; set; }
        //    public string Line515Desc { get; set; }
        //    public object Line515Amount { get; set; }
        //    public string Line516Desc { get; set; }
        //    public object Line516Amount { get; set; }
        //    public string Line517Desc { get; set; }
        //    public object Line517Amount { get; set; }
        //    public string Line518Desc { get; set; }
        //    public object Line518Amount { get; set; }
        //    public string Line519Desc { get; set; }
        //    public object Line519Amount { get; set; }
        //    public object TotalReductionsSellerOV { get; set; }
        //    public object TotalSettlementSellerOV { get; set; }
        //    public object Line700Perc { get; set; }
        //    public object Line700Perc2 { get; set; }
        //    public object Line700Perc3 { get; set; }
        //    public string Line700PercText { get; set; }
        //    public object Line700BaseLoanTier1 { get; set; }
        //    public object Line700BaseLoanTier2 { get; set; }
        //    public object Line700TotalOV { get; set; }
        //    public object Line701Amount { get; set; }
        //    public string Line701PaidTo { get; set; }
        //    public object Line702Amount { get; set; }
        //    public string Line702PaidTo { get; set; }
        //    public object Line703PBA { get; set; }
        //    public object Line703PBS { get; set; }
        //    public string Line704Desc { get; set; }
        //    public object Line704PBA { get; set; }
        //    public object Line704PBS { get; set; }
        //    public object TotalBorrowerChargesOV { get; set; }
        //    public object TotalSellerChargesOV { get; set; }
        //    public object AgentPortionOfTitlePrem { get; set; }
        //    public object UnderwriterPortionOfTitlePrem { get; set; }
        //    public string TitleCompanyNameOV { get; set; }
        //    public bool OmitLenderPaidFee { get; set; }
        //    public string Line102NameOV { get; set; }
        //    public object SellerCredit { get; set; }
        //    public object PersonalPropertyAmount { get; set; }
        //    public object AssumedLoanAmount { get; set; }
        //    public object SellerPayoffOfFirstMort { get; set; }
        //    public object SellerPayoffOfSecondMort { get; set; }
        //    public string DebtsToBePaidOffDescOV { get; set; }
        //    public object ExcessDeposit { get; set; }
        //    public object CureOV { get; set; }
        //    public string CustomaryRecitals { get; set; }
        //    public bool SeeAdditionalPagesBorTrans { get; set; }
        //    public bool SeeAdditionalPagesSellerTrans { get; set; }
        //    public double Cure { get; set; }
        //    public int CureTestResult { get; set; }
        //    public int TRIDPayoffSourceForCD { get; set; }
        //    public bool CDOmitSellerInfo { get; set; }
        //    public object SellerCreditDueFromSellerOV { get; set; }
        //    public long FileDataID { get; set; }

        //}

        //public class HAMP
        //{
        //    public int HAMPID { get; set; }
        //    public string HardshipExplanation { get; set; }
        //    public int PreviouslyModifiedUnderHAMP { get; set; }
        //    public object DateOfImminentDefault { get; set; }
        //    public int Chapter7DischargeAndDidNotReaffirmMortgage { get; set; }
        //    public int CurrentLoanHasEscrowAccount { get; set; }
        //    public int ImminentDefaultDetermined { get; set; }
        //    public string ImminentDefaultExplanation { get; set; }
        //    public object OriginalNoteDate { get; set; }
        //    public string ServicerLoanNumber { get; set; }
        //    public object EscrowShortageAmount { get; set; }
        //    public object SolicitationDeadlineOV { get; set; }
        //    public int IsMERSMortgageeOfRecord { get; set; }
        //    public int IsMBSLoan { get; set; }
        //    public object MBSRequestedRemovalDate { get; set; }
        //    public object MBSActualRemovalDate { get; set; }
        //    public object OriginalIntRate { get; set; }
        //    public object OriginalRemainingTerm { get; set; }
        //    public int EligibleForHARP { get; set; }
        //    public int OwnerOfMortgage { get; set; }
        //    public int OtherHardshipApproved { get; set; }
        //    public bool IncomeVerified { get; set; }
        //    public object DocRequestSentDate { get; set; }
        //    public object DocRequestDueByDate { get; set; }
        //    public object PaymentPostedDate1 { get; set; }
        //    public object PaymentPostedDate2 { get; set; }
        //    public object PaymentPostedDate3 { get; set; }
        //    public object PaymentPostedDate4 { get; set; }
        //    public object PaymentReceivedAmount1 { get; set; }
        //    public object PaymentReceivedAmount2 { get; set; }
        //    public object PaymentReceivedAmount3 { get; set; }
        //    public object PaymentReceivedAmount4 { get; set; }
        //    public bool EscrowAccountEstablished { get; set; }
        //    public object NPVDate { get; set; }
        //    public object NPVResultPreMod { get; set; }
        //    public object NPVResultPostMod { get; set; }
        //    public object IncomeUsedInTrialPlan { get; set; }
        //    public int TrialPlanIncomeType { get; set; }
        //    public object TrialPlanSentDate { get; set; }
        //    public object ModEffectiveDate { get; set; }
        //    public object MarketSurveyRateOV { get; set; }
        //    public object ModAgreementPrepDate { get; set; }
        //    public object ModAgreementSentDate { get; set; }
        //    public object ModAgreementDueDate { get; set; }
        //    public object ModAgreementDueExtensionDate { get; set; }
        //    public object ModAgreementReceivedDate { get; set; }
        //    public object SchedModEffectiveDate { get; set; }
        //    public int ModRequiresRecordingOV { get; set; }
        //    public object TrialPlanReceivedDate { get; set; }
        //    public object DocRequestReceivedDate { get; set; }
        //    public object ValueAnalysisDate { get; set; }
        //    public object CurrentLPIDate { get; set; }
        //    public object UPBBeforeMod { get; set; }
        //    public object OtherContributions { get; set; }
        //    public object AttorneyFeesNotInEscrow { get; set; }
        //    public object EscrowShortageForAdvances { get; set; }
        //    public object OtherAdvances { get; set; }
        //    public object BorrowerContributions { get; set; }
        //    public object InterestOwedOrPaymentNotReported { get; set; }
        //    public object PrincipalPaymentOrOwedNotReported { get; set; }
        //    public object DelinquentInterest { get; set; }
        //    public object ModServicingFeePercent { get; set; }
        //    public object ModOfficerSignatureDate { get; set; }
        //    public bool AllowCustomWaterfall { get; set; }
        //    public string CustomWaterfallName { get; set; }
        //    public string UnderlyingTrustID { get; set; }
        //    public int ProgramType { get; set; }
        //    public object TrialPlanExecutionDateBor { get; set; }
        //    public object TrialPlanExecutionDateServicer { get; set; }
        //    public object ModAgreementExecutionDateBor { get; set; }
        //    public object ModAgreementExecutionDateServicer { get; set; }
        //    public int SubmissionStatus { get; set; }
        //    public int HAMPMortgageType { get; set; }
        //    public int HAMPLienType { get; set; }
        //    public object OriginalTerm { get; set; }
        //    public object OriginalBalloonTerm { get; set; }
        //    public object EscrowPaymentBeforeModOV { get; set; }
        //    public object EscrowPaymentAfterModOV { get; set; }
        //    public object DisbursementForgiven { get; set; }
        //    public object HousingExpenseBeforeModOV { get; set; }
        //    public object HousingExpenseAfterModOV { get; set; }
        //    public int ValuationAnalysisMethod { get; set; }
        //    public int PropertyConditionCode { get; set; }
        //    public object LPIDateAfterMod { get; set; }
        //    public object PrincipalForgiveness { get; set; }
        //    public int SubordinateLienPaydownCode { get; set; }
        //    public object SubordinateLienPaydownAmount { get; set; }
        //    public object TrialPeriodPayment { get; set; }
        //    public object PaymentPostedDate5 { get; set; }
        //    public object PaymentReceivedAmount5 { get; set; }
        //    public object EscrowCushionAmount { get; set; }
        //    public int DelinquencyHardshipReason { get; set; }
        //    public object IntRateCapOV { get; set; }
        //    public int OriginalProductType { get; set; }
        //    public int HAMPOccupancyCode { get; set; }
        //    public int RecordationRequired { get; set; }
        //    public object OriginalPI { get; set; }
        //    public bool ExtendTrialPeriodByOneMonth { get; set; }
        //    public int OtherHardshipRequired { get; set; }
        //    public int EscrowAccountCannotBeEst { get; set; }
        //    public int SubAgreementOrTitlePolRequired { get; set; }
        //    public int NoteMayBeAssumed { get; set; }
        //    public object PaymentDueDate1 { get; set; }
        //    public object PaymentDueDate2 { get; set; }
        //    public object TrialPlanPrepDate { get; set; }
        //    public int OriginalLoanHasPrepayPenalty { get; set; }
        //    public object NPVDataCollectionDate { get; set; }
        //    public object FirstPaymentDateAtOrigination { get; set; }
        //    public object LoanAmountAtOrigination { get; set; }
        //    public object IntRateAtOrigination { get; set; }
        //    public object OriginalPurPrice { get; set; }
        //    public object AppraisedValueAtOrigination { get; set; }
        //    public int OriginalProductTypeNPV { get; set; }
        //    public object ARMResetDate { get; set; }
        //    public object FeesReimbursedByInvestor { get; set; }
        //    public object MIPartialClaimAmount { get; set; }
        //    public object _PaymentDueDate3 { get; set; }
        //    public object _PaymentDueDate4 { get; set; }
        //    public object _PaymentDueDate5 { get; set; }
        //    public int _TrialPeriodPaymentCountAsExtended { get; set; }
        //    public object _ModIntRateCap { get; set; }
        //    public object _ModMaturityDate { get; set; }
        //    public bool ReportedTrial { get; set; }
        //    public bool ReportedModification { get; set; }
        //    public bool ReportedPayment2 { get; set; }
        //    public bool ReportedPayment3 { get; set; }
        //    public bool ReportedPayment4 { get; set; }
        //    public bool ReportedPayment5 { get; set; }
        //    public bool ReportedBorrowerDisqualified { get; set; }
        //    public bool ReportedForeclosureMit { get; set; }
        //    public bool ReportedCancel { get; set; }
        //    public object OriginalMaturityDate { get; set; }
        //    public bool FirstPaymentDateNotFirstOfMonth { get; set; }
        //    public object ARMResetIntRate { get; set; }
        //    public object ForeclosureReferralDate { get; set; }
        //    public object ProjectedForeclosureSaleDate { get; set; }
        //    public int _PeriodCount { get; set; }
        //    public double _IncentiveMonthly { get; set; }
        //    public bool ExtendModEffectiveDateByOneMonth { get; set; }
        //    public int NPVModelVersion { get; set; }
        //    public int HAMPPropertyType { get; set; }
        //    public bool HMDADataNotAvailable { get; set; }
        //    public int NPVModelType { get; set; }
        //    public int HMDAConsentProvided { get; set; }
        //    public int TrialNotApprovedReasonCode { get; set; }
        //    public int TrialFalloutReasonCode { get; set; }
        //    public int LoanDocsOmittedEscrowProvisions { get; set; }
        //    public bool ReportedOfficialCorrection { get; set; }
        //    public bool ReportedOfficialCancel { get; set; }
        //    public bool ReportedHMDAData { get; set; }
        //    public object FHAPriorPartialClaimAmount { get; set; }
        //    public object UPBorrowerRequestDate { get; set; }
        //    public object UPForbearancePlanEffectiveDate { get; set; }
        //    public object UPForbearancePlanNoticeSentDate { get; set; }
        //    public object UPForbearancePlanExpirationDate { get; set; }
        //    public object UPForbearancePlanTerm { get; set; }
        //    public object UPPaymentAmount { get; set; }
        //    public string UPNotes { get; set; }
        //    public int UPCompletedDisposition { get; set; }
        //    public int UPInvestor { get; set; }
        //    public int UPSource { get; set; }
        //    public int UPStatus { get; set; }
        //    public object UPBenefitsStartDate { get; set; }
        //    public object UPBenefitsEndDate { get; set; }
        //    public object UPUnemploymentStartDate { get; set; }
        //    public double _UPPaymentReductionPerc { get; set; }
        //    public object UPRequestConfirmationDate { get; set; }
        //    public object UPEligibilityDeterminationDate { get; set; }
        //    public object UPNonApprovalNoticeSentDate { get; set; }
        //    public object UPTotalMonthlyPayment { get; set; }
        //    public object MinimumNetReturnAmount { get; set; }
        //    public int MIWaiverCode { get; set; }
        //    public object PropertyListPrice { get; set; }
        //    public object PropertyVacancyDate { get; set; }
        //    public int SSCancellationReasonCode { get; set; }
        //    public int DILCancellationReasonCode { get; set; }
        //    public int SSOrDILReasonCode { get; set; }
        //    public object _SubLienReimbursementAmount { get; set; }
        //    public object TotalAllowableCostAmount { get; set; }
        //    public object TransactionClosingDate { get; set; }
        //    public object SSAgreementExpirationDate { get; set; }
        //    public object DILAgreementExpirationDate { get; set; }
        //    public object SSAgreementIssueDate { get; set; }
        //    public object DILAgreementIssueDate { get; set; }
        //    public int LoanDelinquencyType { get; set; }
        //    public object FinalUPBAmount { get; set; }
        //    public object SSBorExecutionDate { get; set; }
        //    public object DILBorExecutionDate { get; set; }
        //    public object SSOrDILReasonDate { get; set; }
        //    public object SSAgreementDueDate { get; set; }
        //    public bool HardshipAffidavitRequired { get; set; }
        //    public object PartialPaymentAmount { get; set; }
        //    public object FirstPartialPaymentDueDate { get; set; }
        //    public object AlternateRSSASentDate { get; set; }
        //    public object AlternateRSSADueDate { get; set; }
        //    public object HAFASolicitationSentDate { get; set; }
        //    public object HAFAEvaluationDate { get; set; }
        //    public string HAFAEvaluationExplanation { get; set; }
        //    public int ShortSaleStatus { get; set; }
        //    public int InvestorApprovalWithForeclosure { get; set; }
        //    public string HAFATitleNotes { get; set; }
        //    public object EstOtherAllowableCosts { get; set; }
        //    public object MANPPerc { get; set; }
        //    public int MANPBasis { get; set; }
        //    public object MarginForListPrice { get; set; }
        //    public string ListPriceHistory { get; set; }
        //    public object ExtensionPeriod { get; set; }
        //    public string ExtensionHistory { get; set; }
        //    public object RSSAReceivedDate { get; set; }
        //    public object PurchaseOfferAmount { get; set; }
        //    public string PurchaseOfferHistory { get; set; }
        //    public int PurchaseOfferApproved { get; set; }
        //    public bool D4LEvaluated { get; set; }
        //    public bool D4LAccepted { get; set; }
        //    public bool D4LReleaseOfClaimsReceived { get; set; }
        //    public int AcceptedSolutionType { get; set; }
        //    public object AltUPBAfterMod { get; set; }
        //    public object AltIntRateAfterMod { get; set; }
        //    public object AltTermAfterMod { get; set; }
        //    public object AltForbearance { get; set; }
        //    public object AltPrincipalReduction { get; set; }
        //    public object MaxMonthsPastDueInPast12Months { get; set; }
        //    public object NPVResultPreModPRA { get; set; }
        //    public object NPVResultPostModPRA { get; set; }
        //    public object SubLienReimbursementAmountOV { get; set; }
        //    public object SSCancellationDate { get; set; }
        //    public bool RSSADeclinedDidNotComply { get; set; }
        //    public string RSSADeclinedDidNotComplyText { get; set; }
        //    public bool RSSADeclinedNotComplete { get; set; }
        //    public bool RSSADeclinedProceedsInsufficient { get; set; }
        //    public string RSSADeclinedOtherText { get; set; }
        //    public bool RSSADeclinedOther { get; set; }
        //    public bool RSSANotCompletedContract { get; set; }
        //    public bool RSSANotCompletedBuyersDoc { get; set; }
        //    public bool RSSAInsufficientSalesPrice { get; set; }
        //    public bool RSSAInsufficientNetProceeds { get; set; }
        //    public bool RSSAInsufficientExConcessions { get; set; }
        //    public bool RSSAInsufficientExCommisions { get; set; }
        //    public bool RSSAInsufficientExClosingCosts { get; set; }
        //    public bool RSSAInsufficientExSubLienObligations { get; set; }
        //    public object PartialPaymentStartDate { get; set; }
        //    public object DILCancelledDate { get; set; }
        //    public object DILAgreementDueDate { get; set; }
        //    public object MarketingPeriodExpirationDate { get; set; }
        //    public object SalesCommissionPerc { get; set; }
        //    public object SalesCommission { get; set; }
        //    public int HAMPDelinquencyTypeCode { get; set; }
        //    public int ForbearancePlanType { get; set; }
        //    public int PrincipalReductionAlternativeCode { get; set; }
        //    public int RestrictionForAltWaterfallTypeCode { get; set; }
        //    public int SupplementaryAssistCode { get; set; }
        //    public object _UPPaymentReductionAmount { get; set; }
        //    public string SSCancellationNotes { get; set; }
        //    public object AlternateRSSAExpirationDate { get; set; }
        //    public int FirstLienHELOCWithRightToNewFunds { get; set; }
        //    public bool WeServiceSecondLien { get; set; }
        //    public long FileDataID { get; set; }

        //}

        //public class Secondary
        //{
        //    public int SecondaryID { get; set; }
        //    public object PAUnpaidPrincipalBalance { get; set; }
        //    public object PANetPrice { get; set; }
        //    public object PAPremiumOrDiscountOV { get; set; }
        //    public object _PAPremiumOrDiscount { get; set; }
        //    public object PA_SRPPerc { get; set; }
        //    public object PA_SRPAmountOV { get; set; }
        //    public object _PA_SRPAmount { get; set; }
        //    public object PAEscrowBalance { get; set; }
        //    public object PAInterestDays { get; set; }
        //    public object PAInterestAmount { get; set; }
        //    public object PABuydownFunds { get; set; }
        //    public object PAOtherFees { get; set; }
        //    public object PARollFee { get; set; }
        //    public string PANotes { get; set; }
        //    public object _PAWireAmount { get; set; }
        //    public string FundingWireAdjustmentDesc1 { get; set; }
        //    public object FundingWireAdjustmentAmount1 { get; set; }
        //    public string FundingWireAdjustmentDesc2 { get; set; }
        //    public object FundingWireAdjustmentAmount2 { get; set; }
        //    public object FundingWireAmount { get; set; }
        //    public object WarehouseAdvanceAmount { get; set; }
        //    public object _WarehouseHaircut { get; set; }
        //    public string WarehouseLoanNo { get; set; }
        //    public bool Hedged { get; set; }
        //    public object InvestorFirstPaymentDate { get; set; }
        //    public string InvestorCommitmentNo { get; set; }
        //    public int InvestorCommitmentType { get; set; }
        //    public object InvestorCommitmentDate { get; set; }
        //    public object InvestorCommitmentExpirationDate { get; set; }
        //    public object InvestorLockDays { get; set; }
        //    public object InvestorLockExtension1Days { get; set; }
        //    public object InvestorLockExtension2Days { get; set; }
        //    public string InvestorRateSheetID { get; set; }
        //    public object InvestorPriceBase { get; set; }
        //    public object InvestorSRP { get; set; }
        //    public object _InvestorPriceAdjustments { get; set; }
        //    public object _InvestorPriceNet { get; set; }
        //    public object _PricingGainPercent { get; set; }
        //    public object InvestorRegisteredDate { get; set; }
        //    public object InvestorIntRate { get; set; }
        //    public string InvestorLoanProgramCode { get; set; }
        //    public string UWNotes { get; set; }
        //    public string MandatoryInvestor { get; set; }
        //    public string ExcludedInvestor { get; set; }
        //    public string PricedInvestor { get; set; }
        //    public object _InvestorProfitNet { get; set; }
        //    public object _PricingGainAmount { get; set; }
        //    public object InvestorLockExtension3Days { get; set; }
        //    public object _CreditedPrincipal { get; set; }
        //    public object _CreditedInterest { get; set; }
        //    public object _CreditedEscrowFunds { get; set; }
        //    public object _CreditedBuydownFunds { get; set; }
        //    public object _CreditedLateFees { get; set; }
        //    public object _CreditedTotal { get; set; }
        //    public object _PaidAmountTotal { get; set; }
        //    public string WarehouseLenderCode { get; set; }
        //    public string OtherAUSCaseNo { get; set; }
        //    public object OtherAUSSubmissionDate { get; set; }
        //    public string OtherAUSRecommendation { get; set; }
        //    public int FundingType { get; set; }
        //    public bool AppDepositIsNettedFromWire { get; set; }
        //    public object NextRateAdjustmentEffectiveDate { get; set; }
        //    public int InvestorCollateralProgram { get; set; }
        //    public int InvestorRemittanceType { get; set; }
        //    public string LoanComments { get; set; }
        //    public object ARMCurrentIntRate { get; set; }
        //    public int GSELoanProgramIdentifier { get; set; }
        //    public object ARMCurrentPIPayment { get; set; }
        //    public object AggregateLoanCurtailmentAmountOV { get; set; }
        //    public int GSERefinanceProgramType { get; set; }
        //    public object DeliquentPaymentsOverPast12MonthsCount { get; set; }
        //    public int CreditScoreImpairmentType { get; set; }
        //    public int ULDDDestination { get; set; }
        //    public int ARMIndexSource { get; set; }
        //    public int FannieARMIndexSource { get; set; }
        //    public int AUSRecommendationOV { get; set; }
        //    public int CCFundsType1 { get; set; }
        //    public int CCSourceType1 { get; set; }
        //    public object CCFundAmount1 { get; set; }
        //    public int CCFundsType2 { get; set; }
        //    public int CCSourceType2 { get; set; }
        //    public object CCFundAmount2 { get; set; }
        //    public int CCFundsType3 { get; set; }
        //    public int CCSourceType3 { get; set; }
        //    public object CCFundAmount3 { get; set; }
        //    public int CCFundsType4 { get; set; }
        //    public int CCSourceType4 { get; set; }
        //    public object CCFundAmount4 { get; set; }
        //    public string SellerLoanIdentifier { get; set; }
        //    public string DocumentCustodianIdentifier { get; set; }
        //    public string ServicerIdentifier { get; set; }
        //    public int ConstructionMethod { get; set; }
        //    public object ULDD_UPBAmountOV { get; set; }
        //    public object LoanAcquisitionScheduledUPBAmount { get; set; }
        //    public object DownPaymentAmount1 { get; set; }
        //    public int DownPaymentSource1 { get; set; }
        //    public int DownPaymentType1 { get; set; }
        //    public object DownPaymentAmount2 { get; set; }
        //    public int DownPaymentSource2 { get; set; }
        //    public int DownPaymentType2 { get; set; }
        //    public object DownPaymentAmount3 { get; set; }
        //    public int DownPaymentSource3 { get; set; }
        //    public int DownPaymentType3 { get; set; }
        //    public object DownPaymentAmount4 { get; set; }
        //    public int DownPaymentSource4 { get; set; }
        //    public int DownPaymentType4 { get; set; }
        //    public int BuydownContributor { get; set; }
        //    public int SecondMortgageType { get; set; }
        //    public bool GuaranteeFeeAddOnIndicator { get; set; }
        //    public object LoanBuyupBuydownBasisPointNumber { get; set; }
        //    public int LoanBuyupBuydownType { get; set; }
        //    public bool ConstructionLoanIndicator { get; set; }
        //    public int ConstToPermClosingType { get; set; }
        //    public int ConstToPermClosingFeatureType { get; set; }
        //    public object ConstToPermFirstPaymentDueDate { get; set; }
        //    public int ProjectClassificationOV { get; set; }
        //    public int ProjectAttachmentTypeOV { get; set; }
        //    public int AttachmentTypeOV { get; set; }
        //    public string FanniePayeeIdentifierOV { get; set; }
        //    public int ULDDSectionOfActTypeOV { get; set; }
        //    public object LastPaidInstallmentDueDateOV { get; set; }
        //    public object LTVBaseOV { get; set; }
        //    public object LTVOV { get; set; }
        //    public object CLTVOV { get; set; }
        //    public object HCLTVOV { get; set; }
        //    public int MIAbsenceReason { get; set; }
        //    public object OFCEscrowFunds { get; set; }
        //    public object OFCBuydown { get; set; }
        //    public object OFCAdvancedPITIPayment { get; set; }
        //    public object OFCPrincipalCurtailment { get; set; }
        //    public object LastUsedAdHocConditionNo { get; set; }
        //    public int ServicingOption { get; set; }
        //    public object InvestorARMMargin { get; set; }
        //    public object _InvestorSRPAdjustments { get; set; }
        //    public object _InvestorSRPNet { get; set; }
        //    public double _UnpaidPrincipalBalance { get; set; }
        //    public bool InterimInterestNotNetFunded { get; set; }
        //    public string InvestorPPEPricedProductName { get; set; }
        //    public object InvestorPPETimePriced { get; set; }
        //    public double _EscrowBalanceAtClosing { get; set; }
        //    public double _EscrowBalanceCurrent { get; set; }
        //    public double _BuydownFundsAtClosing { get; set; }
        //    public double _BuydownFundsCurrent { get; set; }
        //    public object HedgeCancellationDate { get; set; }
        //    public object _PricingGainAmountPercent { get; set; }
        //    public string NotaryCounty { get; set; }
        //    public object MarginTotalPerc { get; set; }
        //    public object MarginTotal { get; set; }
        //    public object WarehouseAdvancePerc { get; set; }
        //    public object _CreditedMI { get; set; }
        //    public double _MICurrent { get; set; }
        //    public object ServicingPrincipalAmountTransferred { get; set; }
        //    public object ServicingEscrowAmountTransferred { get; set; }
        //    public object ServicingBuydownAmountTransferred { get; set; }
        //    public string ServicerLoanNumber { get; set; }
        //    public object WarehouseInterestRate { get; set; }
        //    public object _WarehouseInterestAmount { get; set; }
        //    public string WarehouseOtherFeeName { get; set; }
        //    public object WarehouseOtherFeeAmount { get; set; }
        //    public object WarehouseAdvanceDate { get; set; }
        //    public object WarehousePayoffDate { get; set; }
        //    public string WarehouseBatchNo { get; set; }
        //    public bool EscrowDepositNotNetFunded { get; set; }
        //    public bool MIPFFNotNetFunded { get; set; }
        //    public object InsuranceFeeAmount { get; set; }
        //    public string PMICheckNo { get; set; }
        //    public object TotalMortgagedProperties { get; set; }
        //    public object DeclaredSecondRatio { get; set; }
        //    public object DeclaredMonthsInReserve { get; set; }
        //    public object DeclaredCashOutAmount { get; set; }
        //    public bool DeclaredNonOccCobor { get; set; }
        //    public int WarehouseAdvanceCalcMethod { get; set; }
        //    public object FinalAPR { get; set; }
        //    public object FinalFinanceCharge { get; set; }
        //    public bool LumpSumLenderCreditIsNettedFromWire { get; set; }
        //    public bool CDCureIsNettedFromWire { get; set; }
        //    public object RepriceNeededDateAndTime { get; set; }
        //    public object TotalLiabilitiesMonthlyPaymentAmountOV { get; set; }
        //    public object TotalMonthlyProposedHousingExpenseAmountOV { get; set; }
        //    public int LoanAffordableIndicatorOV { get; set; }
        //    public bool _LoanAffordableIndicator { get; set; }
        //    public bool PortfolioRefiPrepaymentPenaltyPaid { get; set; }
        //    public int IncomeVerified { get; set; }
        //    public object UPBPurchased { get; set; }
        //    public object MonthsSincePreviousRefi { get; set; }
        //    public int IsExistingHCL { get; set; }
        //    public int ENoteIndicator { get; set; }
        //    public string FannieWarehouseIdentifierOV { get; set; }
        //    public string FreddieWarehouseIdentifierOV { get; set; }
        //    public object PricingLastUpated { get; set; }
        //    public object InvestorPricingLastUpated { get; set; }
        //    public object AmountDueToBrokerAtFundingOV { get; set; }
        //    public object FinalTotalOfPayments { get; set; }
        //    public object EscrowBalanceAtDelivery { get; set; }
        //    public long FileDataID { get; set; }

        //}

        //public class Closing
        //{
        //    public int ClosingID { get; set; }
        //    public string NotaryName { get; set; }
        //    public string PropertyTaxDesc { get; set; }
        //    public string PropertyTaxYearDesc { get; set; }
        //    public string TrustName { get; set; }
        //    public int TrustType { get; set; }
        //    public string TrustTypeOtherDescription { get; set; }
        //    public object TrustEstablishedDate { get; set; }
        //    public string TrustState { get; set; }
        //    public string TrustName2 { get; set; }
        //    public int TrustType2 { get; set; }
        //    public string TrustTypeOtherDescription2 { get; set; }
        //    public object TrustEstablishedDate2 { get; set; }
        //    public string TrustState2 { get; set; }
        //    public string VestingDescription { get; set; }
        //    public int LoanProceedsTo { get; set; }
        //    public int RefiOriginalCreditorIncrease { get; set; }
        //    public int CDSplitDisclosureIndicator { get; set; }
        //    public object CDMainEmbeddedDocID { get; set; }
        //    public object CDSellerEmbeddedDocID { get; set; }
        //    public int TexasA6Status { get; set; }
        //    public long FileDataID { get; set; }

        //}
        public class CustomValue
        {
            public int CustomValueID { get; set; }
            public int CustomValueType { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
            public long FileDataID { get; set; }
        }

        public class File
        {
            public long FileDataID { get; set; }
            public FileData FileData { get; set; }
            public Status Status { get; set; }
            public List<Application> Applications { get; set; }
            public SubProp SubProp { get; set; }

            public Loan Loan { get; set; }

            //public List<ClosingCost> ClosingCosts { get; set; }
            public List<PrepaidItem> PrepaidItems { get; set; }

            //public List<object> HELOCPeriods { get; set; }
            //public List<object> DocPackages { get; set; }
            // public DOT DOT { get; set; }
            // public Fannie Fannie { get; set; }
            // public Freddie Freddie { get; set; }
            public List<Party> Parties { get; set; }

            //public PartyMisc PartyMisc { get; set; }
            //public List<object> Tasks { get; set; }
            //public List<object> NeededItems { get; set; }
            //public List<object> Conditions { get; set; }
            public List<CustomValue> CustomValues { get; set; }

            //public List<object> ExtendedTextValues { get; set; }
            // public List<object> ExtendedDateValues { get; set; }
            //public List<object> ExtendedBooleanValues { get; set; }
            //public List<object> ExtendedDecimalValues { get; set; }
            // public CreditDenial CreditDenial { get; set; }
            // public CA883 CA883 { get; set; }
            // public List<IRSForm4506s> IRSForm4506s { get; set; }
            // public IRSForm1098 IRSForm1098 { get; set; }
            // public Transmittal Transmittal { get; set; }
            //  public HMDA HMDA { get; set; }
            // public FHA FHA { get; set; }
            //   public FHA203K FHA203K { get; set; }
            //  public FHAForms FHAForms { get; set; }
            //  public FHAMCAW FHAMCAW { get; set; }
            // public VA VA { get; set; }
            // public VALoanSummary VALoanSummary { get; set; }
            // public VAValue VAValue { get; set; }
            //public List<object> SelfEmpIncs { get; set; }
            //public List<object> SelfEmpIncYears { get; set; }
            // public SalesTools SalesTools { get; set; }
            // public MiscForms MiscForms { get; set; }
            // public List<object> TrustAccountItems { get; set; }
            //  public TXForms TXForms { get; set; }
            public CustomFields CustomFields { get; set; }
            // public HUD1 HUD1 { get; set; }
            //public List<object> NYAppLogFees { get; set; }
            //public List<object> FieldNotes { get; set; }
            //public List<object> OSOResults { get; set; }
            //public List<object> PriceAdjustments { get; set; }
            //public List<object> VAServiceDatas { get; set; }
            //public List<object> VAPreviousLoans { get; set; }
            // public HAMP HAMP { get; set; }
            //public List<object> VAAuthorizedAgents { get; set; }
            //public List<object> ShoppableProviders { get; set; }
            //public List<object> LockHistories { get; set; }
            // public Secondary Secondary { get; set; }
            // public Closing Closing { get; set; }
            //public List<object> LoanPayments { get; set; }
            //public List<object> RelatedLoans { get; set; }
            //public List<object> Markups { get; set; }
            //public List<object> Snapshots { get; set; }
            //public List<object> AdditionalParties { get; set; }
            //public List<object> ClosingProrations { get; set; }
            //public List<object> ClosingAdjustments { get; set; }
            //public List<object> ClosingPayoffs { get; set; }
            //public List<object> DisclosureLogEntries { get; set; }
            //public List<object> DocSigners { get; set; }
            //public List<object> ServiceOrders { get; set; }
        }

        public class NewFile
        {
            public string OrganizationCode { get; set; }
            public string SubPropState { get; set; }
            public string LoanOfficerUserName { get; set; }
            public string BorrowerFirstName { get; set; }
            public string BorrowerLastName { get; set; }
            public string BorrowerMiddleName { get; set; }
            public string BorrowerSuffix { get; set; }
            public string CoBorrowerFirstName { get; set; }
            public string CoBorrowerLastName { get; set; }
            public string CoBorrowerMiddleName { get; set; }
            public string CoBorrowerSuffix { get; set; }
        }
    }
}