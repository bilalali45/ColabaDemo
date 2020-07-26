namespace ByteWebConnector.API.Models
{
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
}
