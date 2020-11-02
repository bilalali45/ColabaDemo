using System;
using System.Collections.Generic;
using System.Text;

namespace LosIntegration.Model.Model.ServiceResponseModels.ByteWebConnector
{
    public class FileData
    {
        public int FileDataId { get; set; } // FileDataID (Primary key)
        public int? LoanId { get; set; } // LoanID
        public int OrganizationId { get; set; } // OrganizationID
        public string FileName { get; set; } // FileName (length: 50)
        public string FilePath { get; set; } // FilePath (length: 255)
        public short OccupancyType { get; set; } // OccupancyType
        public System.DateTime DateCreated { get; set; } // DateCreated
        public System.DateTime DateModified { get; set; } // DateModified
        public string AgencyCaseNo { get; set; } // AgencyCaseNo (length: 50)
        public string TitleNamesNonBorrowers { get; set; } // TitleNamesNonBorrowers (length: 200)
        public string DesiredCoName { get; set; } // DesiredCoName (length: 150)
        public string DesiredCoStreet1 { get; set; } // DesiredCoStreet1 (length: 50)
        public string DesiredCoStreet2 { get; set; } // DesiredCoStreet2 (length: 50)
        public string DesiredCoCity { get; set; } // DesiredCoCity (length: 50)
        public string DesiredCoState { get; set; } // DesiredCoState (length: 2)
        public string DesiredCoZip { get; set; } // DesiredCoZip (length: 9)
        public string DesiredCoPhone { get; set; } // DesiredCoPhone (length: 20)
        public string DesiredCoFax { get; set; } // DesiredCoFax (length: 20)
        public short DownPaymentType1 { get; set; } // DownPaymentType1
        public decimal? DownPaymentAmount1 { get; set; } // DownPaymentAmount1
        public short DownPaymentType2 { get; set; } // DownPaymentType2
        public decimal? DownPaymentAmount2 { get; set; } // DownPaymentAmount2
        public string DownPaymentDescOv { get; set; } // DownPaymentDescOV (length: 100)
        public string FhaLenderId { get; set; } // FHALenderId (length: 50)
        public string FhaSponsorId { get; set; } // FHASponsorId (length: 50)
        public decimal? SalesConcessions { get; set; } // SalesConcessions
        public decimal? GiftAmount { get; set; } // GiftAmount
        public string GiftSource { get; set; } // GiftSource (length: 50)
        public string GiftDonorName1 { get; set; } // GiftDonorName1 (length: 50)
        public string GiftDonorName2 { get; set; } // GiftDonorName2 (length: 50)
        public string GiftDonorStreet { get; set; } // GiftDonorStreet (length: 50)
        public string GiftDonorCity { get; set; } // GiftDonorCity (length: 50)
        public string GiftDonorState { get; set; } // GiftDonorState (length: 2)
        public string GiftDonorZip { get; set; } // GiftDonorZip (length: 9)
        public string GiftDonorPhone { get; set; } // GiftDonorPhone (length: 20)
        public string MinNumber { get; set; } // MINNumber (length: 20)
        public int? MinOrgId { get; set; } // MINOrgID
        public long? MinSequence { get; set; } // MINSequence
        public short WaiveEscrow { get; set; } // WaiveEscrow
        public short VaFundingFeeCategory { get; set; } // VAFundingFeeCategory
        public short FirstTimeHomeBuyer { get; set; } // FirstTimeHomeBuyer
        public int? TaxBracket { get; set; } // TaxBracket
        public short CommunityLending { get; set; } // CommunityLending
        public short HomeBuyerEducation { get; set; } // HomeBuyerEducation
        public short FirstMortgageHolder { get; set; } // FirstMortgageHolder
        public string LoanOfficerUserName { get; set; } // LoanOfficerUserName (length: 50)
        public short LoanOfficerAccess { get; set; } // LoanOfficerAccess
        public string LoanProcessorUserName { get; set; } // LoanProcessorUserName (length: 50)
        public short LoanProcessorAccess { get; set; } // LoanProcessorAccess
        public string OtherUserName { get; set; } // OtherUserName (length: 50)
        public short OtherUserAccess { get; set; } // OtherUserAccess
        public string OtherUser2Name { get; set; } // OtherUser2Name (length: 50)
        public short OtherUser2Access { get; set; } // OtherUser2Access
        public string OtherUser3Name { get; set; } // OtherUser3Name (length: 50)
        public short OtherUser3Access { get; set; } // OtherUser3Access
        public string OtherUser4Name { get; set; } // OtherUser4Name (length: 50)
        public short OtherUser4Access { get; set; } // OtherUser4Access
        public short DocumentationType { get; set; } // DocumentationType
        public bool UseCustomRepaymentWording { get; set; } // UseCustomRepaymentWording
        public string RepaymentWordingOv { get; set; } // RepaymentWordingOV (length: 500)
        public short EvidenceOfTitleOption { get; set; } // EvidenceOfTitleOption
        public string OtherEvidenceOfTitle { get; set; } // OtherEvidenceOfTitle (length: 50)
        public short CommitmentReturnOption { get; set; } // CommitmentReturnOption
        public int? CommitmentReturnDays { get; set; } // CommitmentReturnDays
        public string CommitmentReturnAddress { get; set; } // CommitmentReturnAddress (length: 250)
        public string XSiteLoanAppId { get; set; } // XSiteLoanAppID (length: 50)
        public bool Deleted { get; set; } // Deleted
        public int SubFolder { get; set; } // SubFolder
        public decimal? TrustAccountBalance { get; set; } // _TrustAccountBalance
        public decimal? TotalAllIncomes { get; set; } // _TotalAllIncomes
        public decimal? TotalAllDebtPayments { get; set; } // _TotalAllDebtPayments
        public decimal? TotalAllDebtBalances { get; set; } // _TotalAllDebtBalances
        public decimal? TotalAllLiquidAssets { get; set; } // _TotalAllLiquidAssets
        public string CosignerNames { get; set; } // _CosignerNames (length: 200)
        public string AllBorrowerNames { get; set; } // _AllBorrowerNames (length: 200)
        public System.DateTime? LoYellowAlertDate { get; set; } // _LOYellowAlertDate
        public System.DateTime? LoRedAlertDate { get; set; } // _LORedAlertDate
        public System.DateTime? LpYellowAlertDate { get; set; } // _LPYellowAlertDate
        public System.DateTime? LpRedAlertDate { get; set; } // _LPRedAlertDate
        public System.DateTime? OuYellowAlertDate { get; set; } // _OUYellowAlertDate
        public System.DateTime? OuRedAlertDate { get; set; } // _OURedAlertDate
        public System.DateTime? Ou2YellowAlertDate { get; set; } // _OU2YellowAlertDate
        public System.DateTime? Ou2RedAlertDate { get; set; } // _OU2RedAlertDate
        public System.DateTime? Ou3YellowAlertDate { get; set; } // _OU3YellowAlertDate
        public System.DateTime? Ou3RedAlertDate { get; set; } // _OU3RedAlertDate
        public System.DateTime? Ou4YellowAlertDate { get; set; } // _OU4YellowAlertDate
        public System.DateTime? Ou4RedAlertDate { get; set; } // _OU4RedAlertDate
        public bool OverrideInterviewer { get; set; } // OverrideInterviewer
        public int? DriveScore { get; set; } // DRIVEScore
        public string DriveStatus { get; set; } // DRIVEStatus (length: 50)
        public int? FileCreditScore { get; set; } // _FileCreditScore
        public string OrgNameOv { get; set; } // OrgNameOV (length: 150)
        public string OrgStreet1Ov { get; set; } // OrgStreet1OV (length: 50)
        public string OrgStreet2Ov { get; set; } // OrgStreet2OV (length: 50)
        public string OrgCityOv { get; set; } // OrgCityOV (length: 50)
        public string OrgStateOv { get; set; } // OrgStateOV (length: 2)
        public string OrgZipOv { get; set; } // OrgZipOV (length: 9)
        public string OrgPhoneOv { get; set; } // OrgPhoneOV (length: 20)
        public string OrgFaxOv { get; set; } // OrgFaxOV (length: 20)
        public string MortgageBotAccountId { get; set; } // MortgageBotAccountId (length: 50)
        public int SuperLoanType { get; set; } // SuperLoanType
        public string VaLenderId { get; set; } // VALenderID (length: 50)
        public string VaAgentId { get; set; } // VAAgentID (length: 50)
        public int GfeVersion { get; set; } // GFEVersion
        public int CompanyTypeOv { get; set; } // CompanyTypeOV
        public int OriginationChannel { get; set; } // OriginationChannel
        public int TilVersionOv { get; set; } // TILVersionOV
        public int LockRequestType { get; set; } // LockRequestType
        public System.DateTime? LockRequestTime { get; set; } // LockRequestTime
        public string LockRequestComments { get; set; } // LockRequestComments (length: 2147483647)
        public string CorvisaFileId1 { get; set; } // CorvisaFileID1 (length: 50)
        public string CorvisaFileId2 { get; set; } // CorvisaFileID2 (length: 50)
        public System.Guid? TradeGuid { get; set; } // TradeGUID
        public int? LockRequestExtensionDays { get; set; } // LockRequestExtensionDays
        public int? RhsCaseNoAssignmentPeriod { get; set; } // RHSCaseNoAssignmentPeriod
        public string ThirdPartyFileName { get; set; } // ThirdPartyFileName (length: 100)
        public string ThirdPartyFileNamePiggyback { get; set; } // ThirdPartyFileNamePiggyback (length: 100)
        public System.DateTime? ThirdPartyTransferDateFirst { get; set; } // ThirdPartyTransferDateFirst
        public System.DateTime? ThirdPartyTransferDateMostRecent { get; set; } // ThirdPartyTransferDateMostRecent
        public string OpenerUserName { get; set; } // OpenerUserName (length: 50)
        public string UnderwriterUserName { get; set; } // UnderwriterUserName (length: 50)
        public string DocDrawerUserName { get; set; } // DocDrawerUserName (length: 50)
        public string CloserUserName { get; set; } // CloserUserName (length: 50)
        public string QcUserName { get; set; } // QCUserName (length: 50)
        public string ComplianceUserName { get; set; } // ComplianceUserName (length: 50)
        public string ShipperUserName { get; set; } // ShipperUserName (length: 50)
        public string LockDeskUserName { get; set; } // LockDeskUserName (length: 50)
        public string AccountingUserName { get; set; } // AccountingUserName (length: 50)
        public string ServicingUserName { get; set; } // ServicingUserName (length: 50)
        public string InsuringUserName { get; set; } // InsuringUserName (length: 50)
        public string SecondaryUserName { get; set; } // SecondaryUserName (length: 50)
        public System.DateTime? OpenerYellowAlertDate { get; set; } // _OpenerYellowAlertDate
        public System.DateTime? OpenerRedAlertDate { get; set; } // _OpenerRedAlertDate
        public System.DateTime? UnderwriterYellowAlertDate { get; set; } // _UnderwriterYellowAlertDate
        public System.DateTime? UnderwriterRedAlertDate { get; set; } // _UnderwriterRedAlertDate
        public System.DateTime? DocDrawerYellowAlertDate { get; set; } // _DocDrawerYellowAlertDate
        public System.DateTime? DocDrawerRedAlertDate { get; set; } // _DocDrawerRedAlertDate
        public System.DateTime? CloserYellowAlertDate { get; set; } // _CloserYellowAlertDate
        public System.DateTime? CloserRedAlertDate { get; set; } // _CloserRedAlertDate
        public System.DateTime? QcYellowAlertDate { get; set; } // _QCYellowAlertDate
        public System.DateTime? QcRedAlertDate { get; set; } // _QCRedAlertDate
        public System.DateTime? ComplianceYellowAlertDate { get; set; } // _ComplianceYellowAlertDate
        public System.DateTime? ComplianceRedAlertDate { get; set; } // _ComplianceRedAlertDate
        public System.DateTime? ShipperYellowAlertDate { get; set; } // _ShipperYellowAlertDate
        public System.DateTime? ShipperRedAlertDate { get; set; } // _ShipperRedAlertDate
        public System.DateTime? LockDeskYellowAlertDate { get; set; } // _LockDeskYellowAlertDate
        public System.DateTime? LockDeskRedAlertDate { get; set; } // _LockDeskRedAlertDate
        public System.DateTime? AccountingYellowAlertDate { get; set; } // _AccountingYellowAlertDate
        public System.DateTime? AccountingRedAlertDate { get; set; } // _AccountingRedAlertDate
        public System.DateTime? ServicingYellowAlertDate { get; set; } // _ServicingYellowAlertDate
        public System.DateTime? ServicingRedAlertDate { get; set; } // _ServicingRedAlertDate
        public System.DateTime? InsuringYellowAlertDate { get; set; } // _InsuringYellowAlertDate
        public System.DateTime? InsuringRedAlertDate { get; set; } // _InsuringRedAlertDate
        public System.DateTime? SecondaryYellowAlertDate { get; set; } // _SecondaryYellowAlertDate
        public System.DateTime? SecondaryRedAlertDate { get; set; } // _SecondaryRedAlertDate
        public string GiftDonorAccountInstitution { get; set; } // GiftDonorAccountInstitution (length: 50)
        public string GiftDonorAccountNo { get; set; } // GiftDonorAccountNo (length: 30)
        public System.DateTime? GiftTransferDate { get; set; } // GiftTransferDate
        public bool GiftFundsProvidedAtClosing { get; set; } // GiftFundsProvidedAtClosing
        public string GiftDonorInstitutionAddress { get; set; } // GiftDonorInstitutionAddress (length: 100)
        public System.DateTime? AgencyCaseNoAssignmentDate { get; set; } // AgencyCaseNoAssignmentDate
        public int DocTypeEmployment { get; set; } // DocTypeEmployment
        public int DocTypeIncome { get; set; } // DocTypeIncome
        public int DocTypeAsset { get; set; } // DocTypeAsset
        public int? EstimatedCreditScore { get; set; } // EstimatedCreditScore
        public string OptimalBlueLoanIdentifier { get; set; } // OptimalBlueLoanIdentifier (length: 20)
        public bool SelfEmployed { get; set; } // _SelfEmployed
        public bool HasNonOccCoBorrower { get; set; } // _HasNonOccCoBorrower
        public string OtherUser5Name { get; set; } // OtherUser5Name (length: 50)
        public System.DateTime? Ou5YellowAlertDate { get; set; } // _OU5YellowAlertDate
        public System.DateTime? Ou5RedAlertDate { get; set; } // _OU5RedAlertDate
        public string OtherUser6Name { get; set; } // OtherUser6Name (length: 50)
        public System.DateTime? Ou6YellowAlertDate { get; set; } // _OU6YellowAlertDate
        public System.DateTime? Ou6RedAlertDate { get; set; } // _OU6RedAlertDate
        public string OtherUser7Name { get; set; } // OtherUser7Name (length: 50)
        public System.DateTime? Ou7YellowAlertDate { get; set; } // _OU7YellowAlertDate
        public System.DateTime? Ou7RedAlertDate { get; set; } // _OU7RedAlertDate
        public string OtherUser8Name { get; set; } // OtherUser8Name (length: 50)
        public System.DateTime? Ou8YellowAlertDate { get; set; } // _OU8YellowAlertDate
        public System.DateTime? Ou8RedAlertDate { get; set; } // _OU8RedAlertDate
        public string OtherUser9Name { get; set; } // OtherUser9Name (length: 50)
        public System.DateTime? Ou9YellowAlertDate { get; set; } // _OU9YellowAlertDate
        public System.DateTime? Ou9RedAlertDate { get; set; } // _OU9RedAlertDate
        public string OtherUser10Name { get; set; } // OtherUser10Name (length: 50)
        public System.DateTime? Ou10YellowAlertDate { get; set; } // _OU10YellowAlertDate
        public System.DateTime? Ou10RedAlertDate { get; set; } // _OU10RedAlertDate
        public string OtherUser11Name { get; set; } // OtherUser11Name (length: 50)
        public System.DateTime? Ou11YellowAlertDate { get; set; } // _OU11YellowAlertDate
        public System.DateTime? Ou11RedAlertDate { get; set; } // _OU11RedAlertDate
        public string OtherUser12Name { get; set; } // OtherUser12Name (length: 50)
        public System.DateTime? Ou12YellowAlertDate { get; set; } // _OU12YellowAlertDate
        public System.DateTime? Ou12RedAlertDate { get; set; } // _OU12RedAlertDate
        public int LoCompType { get; set; } // LOCompType
        public int EligibleForPurchaseByGseOv { get; set; } // EligibleForPurchaseByGSE_OV
        public string IncomeAndDebtQmatrNotes { get; set; } // IncomeAndDebtQMATRNotes (length: 2147483647)
        public string DocPrepAltLenderCode { get; set; } // DocPrepAltLenderCode (length: 10)
        public bool IsBusinessPurpose { get; set; } // IsBusinessPurpose
        public int EventInfo { get; set; } // EventInfo
        public bool IsFlpLoan { get; set; } // _IsFLPLoan
        public int SyncFileDataId { get; set; } // SyncFileDataID
        public int SyncType { get; set; } // SyncType
        public string SyncFileName { get; set; } // SyncFileName (length: 50)
        public decimal? SyncedFileDotCredits { get; set; } // SyncedFileDOTCredits
        public string MarksmanProspectId { get; set; } // MarksmanProspectID (length: 32)
        public int EscrowAbsenceReason { get; set; } // EscrowAbsenceReason
        public int PartialPaymentOption { get; set; } // PartialPaymentOption
        public int LiabilityAfterForeclosure { get; set; } // LiabilityAfterForeclosure
        public string TridLoanIdov { get; set; } // TRIDLoanIDOV (length: 30)
        public bool DoNotApplyTridRules { get; set; } // DoNotApplyTRIDRules
        public int ConstTilaCalcMethod { get; set; } // ConstTILACalcMethod
        public string OtherUser13Name { get; set; } // OtherUser13Name (length: 50)
        public System.DateTime? Ou13YellowAlertDate { get; set; } // _OU13YellowAlertDate
        public System.DateTime? Ou13RedAlertDate { get; set; } // _OU13RedAlertDate
        public string OtherUser14Name { get; set; } // OtherUser14Name (length: 50)
        public System.DateTime? Ou14YellowAlertDate { get; set; } // _OU14YellowAlertDate
        public System.DateTime? Ou14RedAlertDate { get; set; } // _OU14RedAlertDate
        public string OtherUser15Name { get; set; } // OtherUser15Name (length: 50)
        public System.DateTime? Ou15YellowAlertDate { get; set; } // _OU15YellowAlertDate
        public System.DateTime? Ou15RedAlertDate { get; set; } // _OU15RedAlertDate
        public string OtherUser16Name { get; set; } // OtherUser16Name (length: 50)
        public System.DateTime? Ou16YellowAlertDate { get; set; } // _OU16YellowAlertDate
        public System.DateTime? Ou16RedAlertDate { get; set; } // _OU16RedAlertDate
        public string OtherUser17Name { get; set; } // OtherUser17Name (length: 50)
        public System.DateTime? Ou17YellowAlertDate { get; set; } // _OU17YellowAlertDate
        public System.DateTime? Ou17RedAlertDate { get; set; } // _OU17RedAlertDate
        public string OtherUser18Name { get; set; } // OtherUser18Name (length: 50)
        public System.DateTime? Ou18YellowAlertDate { get; set; } // _OU18YellowAlertDate
        public System.DateTime? Ou18RedAlertDate { get; set; } // _OU18RedAlertDate
        public string OtherUser19Name { get; set; } // OtherUser19Name (length: 50)
        public System.DateTime? Ou19YellowAlertDate { get; set; } // _OU19YellowAlertDate
        public System.DateTime? Ou19RedAlertDate { get; set; } // _OU19RedAlertDate
        public string OtherUser20Name { get; set; } // OtherUser20Name (length: 50)
        public System.DateTime? Ou20YellowAlertDate { get; set; } // _OU20YellowAlertDate
        public System.DateTime? Ou20RedAlertDate { get; set; } // _OU20RedAlertDate
        public string OtherUser21Name { get; set; } // OtherUser21Name (length: 50)
        public System.DateTime? Ou21YellowAlertDate { get; set; } // _OU21YellowAlertDate
        public System.DateTime? Ou21RedAlertDate { get; set; } // _OU21RedAlertDate
        public string OtherUser22Name { get; set; } // OtherUser22Name (length: 50)
        public System.DateTime? Ou22YellowAlertDate { get; set; } // _OU22YellowAlertDate
        public System.DateTime? Ou22RedAlertDate { get; set; } // _OU22RedAlertDate
        public string OtherUser23Name { get; set; } // OtherUser23Name (length: 50)
        public System.DateTime? Ou23YellowAlertDate { get; set; } // _OU23YellowAlertDate
        public System.DateTime? Ou23RedAlertDate { get; set; } // _OU23RedAlertDate
        public string OtherUser24Name { get; set; } // OtherUser24Name (length: 50)
        public System.DateTime? Ou24YellowAlertDate { get; set; } // _OU24YellowAlertDate
        public System.DateTime? Ou24RedAlertDate { get; set; } // _OU24RedAlertDate
        public string OtherUser25Name { get; set; } // OtherUser25Name (length: 50)
        public System.DateTime? Ou25YellowAlertDate { get; set; } // _OU25YellowAlertDate
        public System.DateTime? Ou25RedAlertDate { get; set; } // _OU25RedAlertDate
        public int SyncFileDataId2 { get; set; } // SyncFileDataID2
        public string SyncFileName2 { get; set; } // SyncFileName2 (length: 50)
        public bool IsConsumerPortalLoan { get; set; } // _IsConsumerPortalLoan
        public string MarksmanLockId { get; set; } // MarksmanLockID (length: 32)
        public string Uli { get; set; } // ULI (length: 45)
        public bool OverrideUli { get; set; } // OverrideULI
        public int UrlaVersion { get; set; } // URLAVersion
        public int? CoreLogicFraudScore { get; set; } // CoreLogicFraudScore
        public string TitleCurrentlyHeldInNames { get; set; } // TitleCurrentlyHeldInNames (length: 2147483647)
        public string OriginalBorrowers { get; set; } // OriginalBorrowers (length: 2147483647)

        public FileData()
        {
            OrganizationId = 0;
            OccupancyType = 0;
            DownPaymentType1 = 0;
            DownPaymentType2 = 0;
            WaiveEscrow = 0;
            VaFundingFeeCategory = 0;
            FirstTimeHomeBuyer = 0;
            CommunityLending = 0;
            HomeBuyerEducation = 0;
            FirstMortgageHolder = 0;
            LoanOfficerAccess = 0;
            LoanProcessorAccess = 0;
            OtherUserAccess = 0;
            OtherUser2Access = 0;
            OtherUser3Access = 0;
            OtherUser4Access = 0;
            DocumentationType = 0;
            UseCustomRepaymentWording = false;
            EvidenceOfTitleOption = 0;
            CommitmentReturnOption = 0;
            Deleted = false;
            SubFolder = 0;
            OverrideInterviewer = false;
            SuperLoanType = 0;
            GfeVersion = 0;
            CompanyTypeOv = 0;
            OriginationChannel = 0;
            TilVersionOv = 0;
            LockRequestType = 0;
            GiftFundsProvidedAtClosing = false;
            DocTypeEmployment = 0;
            DocTypeIncome = 0;
            DocTypeAsset = 0;
            SelfEmployed = false;
            HasNonOccCoBorrower = false;
            LoCompType = 0;
            EligibleForPurchaseByGseOv = 0;
            IsBusinessPurpose = false;
            EventInfo = 0;
            IsFlpLoan = false;
            SyncFileDataId = 0;
            SyncType = 0;
            EscrowAbsenceReason = 0;
            PartialPaymentOption = 0;
            LiabilityAfterForeclosure = 0;
            DoNotApplyTridRules = false;
            ConstTilaCalcMethod = 0;
            SyncFileDataId2 = 0;
            IsConsumerPortalLoan = false;
            OverrideUli = false;
            UrlaVersion = 0;
        }

    }
}
