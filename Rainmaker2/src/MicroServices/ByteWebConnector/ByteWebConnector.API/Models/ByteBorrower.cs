using System;
using System.Runtime.Serialization;
using ByteWebConnector.API.Enums;
using ByteWebConnector.API.Models.ClientModels;

namespace ByteWebConnector.API.Models
{
    [DataContract(Name = "Borrower")]
    public class ByteBorrower
    {
        [DataMember]
        public int? BorrowerID { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string MiddleName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Generation { get; set; }
        [DataMember]
        public string NickName { get; set; }
        [DataMember]
        public string SSN { get; set; }
        [DataMember]
        public string HomePhone { get; set; }
        [DataMember]
        public string MobilePhone { get; set; }
        [DataMember]
        public string Fax { get; set; }
        [DataMember]
        public object Age { get; set; }
        [DataMember]
        public DateTime? DOB { get; set; }
        [DataMember]
        public int Ethnicity { get; set; }
        [DataMember]
        public bool GovDoNotWishToFurnish { get; set; }
        [DataMember]
        public bool RaceNotApplicable { get; set; }
        [DataMember]
        public bool RaceNotProvided { get; set; }
        [DataMember]
        public bool RaceAmericanIndian { get; set; }
        [DataMember]
        public bool RaceAsian { get; set; }
        [DataMember]
        public bool RaceBlack { get; set; }
        [DataMember]
        public bool RacePacificIslander { get; set; }
        [DataMember]
        public bool RaceWhite { get; set; }
        [DataMember]
        public int Gender { get; set; }
        [DataMember]
        public object YearsSchool { get; set; }
        [DataMember]
        public int MaritalStatus { get; set; }
        [DataMember]
        public int? NoDeps { get; set; }
        [DataMember]
        public string DepsAges { get; set; }
        [DataMember]
        public string Email { get; set; }

        //public object DateSigned1003 { get; set; }
        //    public bool OmitFromTitle { get; set; }
        //    public string FNMACreditRefNo { get; set; }
        //    public string VAServiceNo { get; set; }
        //    public string VAClaimFolderNo { get; set; }
        //    public string CAIVRS { get; set; }
        //    public string LDP { get; set; }
        [DataMember]
        public int OutstandingJudgements { get; set; }
        [DataMember]
        public int Bankruptcy { get; set; }
        [DataMember]
        public int PropertyForeclosed { get; set; }
        [DataMember]
        public int PartyToLawsuit { get; set; }
        [DataMember]
        public int LoanForeclosed { get; set; }
        [DataMember]
        public int DelinquentFederalDebt { get; set; }
        [DataMember]
        public int AlimonyObligation { get; set; }
        [DataMember]
        public int DownPaymentBorrowed { get; set; }
        [DataMember]
        public int EndorserOnNote { get; set; }
        [DataMember]
        public int OccupyAsPrimaryRes { get; set; }
        [DataMember]
        public int OwnershipInterest { get; set; }
        [DataMember]
        public int PropertyType { get; set; }
        [DataMember]
        public int TitleHeld { get; set; }
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
        [DataMember]
        public int CitizenResidencyType { get; set; }
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
        [DataMember]
        public string EthnicityOtherHispanicOrLatinoDesc { get; set; }
        [DataMember]
        public string RaceAmericanIndianTribe { get; set; }
        [DataMember]
        public string RaceOtherAsianDesc { get; set; }
        [DataMember]
        public string RaceOtherPacificIslanderDesc { get; set; }
        //public int DemographicInfoProvidedMethod { get; set; }
        [DataMember]
        public int Race2 { get; set; }
        [DataMember]
        public int Ethnicity2 { get; set; }
        [DataMember]
        public int Gender2 { get; set; }
        [DataMember]
        public int Race2CompletionMethod { get; set; }
        [DataMember]
        public int Ethnicity2CompletionMethod { get; set; }
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
        //public object UndisclosedBorrowerFundsAmount { get; set; }
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
        [DataMember]
        public long FileDataID { get; set; }


        public BorrowerEntity GetRainmakerBorrower()
        {
            var borrowerEntity = new ClientModels.BorrowerEntity();

            borrowerEntity.CellPhone = this.MobilePhone;
            borrowerEntity.FirstName = this.FirstName;
            borrowerEntity.LastName = this.LastName;
            borrowerEntity.MiddleName = this.MiddleName;
            borrowerEntity.Suffix = this.Generation;
            borrowerEntity.HomePhone = this.HomePhone;
            borrowerEntity.EmailAddress = this.Email;
            borrowerEntity.MaritalStatusId = this.MaritalStatus;
            borrowerEntity.ResidencyStateId = this.CitizenResidencyType;
            borrowerEntity.GenderId = GetGenderId(this.Gender2);
            borrowerEntity.NoOfDependent = this.NoDeps;
            borrowerEntity.DependentAge = this.DepsAges;
            borrowerEntity.OutstandingJudgementsIndicator = this.OutstandingJudgements;
            borrowerEntity.BankruptcyIndicator = this.Bankruptcy;
            borrowerEntity.PartyToLawsuitIndicator = this.PartyToLawsuit;
            borrowerEntity.LoanForeclosureOrJudgementIndicator = this.LoanForeclosed;
            borrowerEntity.AlimonyChildSupportObligationIndicator = this.AlimonyObligation;
            borrowerEntity.BorrowedDownPaymentIndicator = this.DownPaymentBorrowed;
            borrowerEntity.CoMakerEndorserOfNoteIndicator = this.EndorserOnNote;
            borrowerEntity.HomeownerPastThreeYearsIndicator = this.OwnershipInterest;
            borrowerEntity.PresentlyDelinquentIndicator = this.DelinquentFederalDebt;
            borrowerEntity.IntentToOccupyIndicator = this.OccupyAsPrimaryRes;
            borrowerEntity.DeclarationsJIndicator = this.CitizenResidencyType == 1 ? 1 : 0;
            borrowerEntity.DeclarationsKIndicator = this.CitizenResidencyType == 2 ? 1 : 0;

            borrowerEntity.PriorPropertyUsageType = this.PropertyType.ToString();
            borrowerEntity.PriorPropertyTitleType = this.TitleHeld.ToString();

            borrowerEntity.EthnicityId = GetEthnicityId(this.Ethnicity);
            borrowerEntity.EthnicityDetailId = GetEthnicityDetailId(this.Ethnicity2);
            if (this.RaceAmericanIndian)
            {
                borrowerEntity.RaceId = (int)RaceEnum.AmericanIndianOrAlaskaNative;
            }
            else if (this.RaceAsian)
            {
                borrowerEntity.RaceId = (int)RaceEnum.Asian;
            }
            else if (this.RaceBlack)
            {
                borrowerEntity.RaceId = (int)RaceEnum.BlackOrAfricanAmerican;
            }
            else if (this.RacePacificIslander)
            {
                borrowerEntity.RaceId = (int)RaceEnum.NativeHawaiianOrOtherPacificIslander;
            }
            else if (this.RaceWhite)
            {
                borrowerEntity.RaceId = (int)RaceEnum.White;
            }
            else
            {
                borrowerEntity.RaceId = (int)RaceEnum.DoNotWishToProvideThisInformation;
            }
            borrowerEntity.RaceDetailId = GetRaceDetailId(this.Race2);
            return borrowerEntity;
        }


        private int GetGenderId(int gender2)
        {
            switch (gender2)
            {
                case 1:
                {
                    const int female = (int)Enums.Gender.Female;
                    return female;
                }
                case 2:
                {
                    const int male = (int)Enums.Gender.Male;
                    return male;
                }
                case 3:
                {
                    const int doNotWish = (int)Enums.Gender.Do_Not_Wish;
                    return doNotWish;
                }
            }

            return 0;
        }


        private int GetRaceDetailId(int race2)
        {
            switch (race2)
            {
                case 64:
                {
                    const int asianIndian = (int)RaceDetailEnum.AsianIndian;
                    return asianIndian;
                }
                case 128:
                {
                    const int chinese = (int)RaceDetailEnum.Chinese;
                    return chinese;
                }
                case 256:
                {
                    const int filipino = (int)RaceDetailEnum.Filipino;
                    return filipino;
                }
                case 512:
                {
                    const int japanese = (int)RaceDetailEnum.Japanese;
                    return japanese;
                }
                case 1024:
                {
                    const int korean = (int)RaceDetailEnum.Korean;
                    return korean;
                }
                case 2048:
                {
                    const int vietnamese = (int)RaceDetailEnum.Vietnamese;
                    return vietnamese;
                }
                case 4096:
                {
                    const int otherAsian = (int)RaceDetailEnum.OtherAsian;
                    return otherAsian;
                }
                case 8192:
                {
                    const int nativeHawaiian = (int)RaceDetailEnum.NativeHawaiian;
                    return nativeHawaiian;
                }
                case 16384:
                {
                    const int guamanianOrChamorro = (int)RaceDetailEnum.GuamanianOrChamorro;
                    return guamanianOrChamorro;
                }
                case 32768:
                {
                    const int samoan = (int)RaceDetailEnum.Samoan;
                    return samoan;
                }
                case 65536:
                {
                    const int otherPacificIslander = (int)RaceDetailEnum.OtherPacificIslander;
                    return otherPacificIslander;
                }
            }

            return 0;
        }


        private int GetEthnicityId(int ethnicity)
        {
            switch (ethnicity)
            {
                case 1:
                {
                    const int hispanicOrLatino = (int)EthnicityEnum.HispanicOrLatino;
                    return hispanicOrLatino;
                }
                case 2:
                {
                    const int notHispanicOrLatino = (int)EthnicityEnum.NotHispanicOrLatino;
                    return notHispanicOrLatino;
                }
                case 3:
                {
                    const int doNotWishToProvideThisInformation = (int)EthnicityEnum.DoNotWishToProvideThisInformation;
                    return doNotWishToProvideThisInformation;
                }
            }

            return 0;
        }


        private int GetEthnicityDetailId(int ethnicity2)
        {
            switch (ethnicity2)
            {
                case 8:
                {
                    const int mexican = (int)EthnicityDetailEnum.Mexican;
                    return mexican;
                }
                case 16:
                {
                    const int puertoRican = (int)EthnicityDetailEnum.PuertoRican;
                    return puertoRican;
                }
                case 32:
                {
                    const int puertoRican = (int)EthnicityDetailEnum.PuertoRican;
                    return puertoRican;
                }
                case 64:
                {
                    const int puertoRican = (int)EthnicityDetailEnum.PuertoRican;
                    return puertoRican;
                }
            }

            return 0;
        }
    }
}