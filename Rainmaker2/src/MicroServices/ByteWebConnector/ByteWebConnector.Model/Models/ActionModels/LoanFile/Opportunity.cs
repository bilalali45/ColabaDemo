













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // Opportunity

    public partial class Opportunity 
    {
        public int Id { get; set; } // Id (Primary key)
        public bool IsActive { get; set; } // IsActive
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDeleted { get; set; } // IsDeleted
        public int? StatusId { get; set; } // StatusId
        public bool? IsValid { get; set; } // IsValid
        public int? LeadSourceId { get; set; } // LeadSourceId
        public int? LeadSourceTypeId { get; set; } // LeadSourceTypeId
        public int? LeadTypeId { get; set; } // LeadTypeId
        public int? LeadSourceOriginalId { get; set; } // LeadSourceOriginalId
        public int? LeadCreatedFromId { get; set; } // LeadCreatedFromId
        public string ReferUrl { get; set; } // ReferUrl (length: 1000)
        public int? StatusCauseId { get; set; } // StatusCauseId
        public int? OwnerId { get; set; } // OwnerId
        public int? TeamId { get; set; } // TeamId
        public bool NoRuleMatched { get; set; } // NoRuleMatched
        public int? AssignmentFlagId { get; set; } // AssignmentFlagId
        public bool IsAutoAssigned { get; set; } // IsAutoAssigned
        public bool IsPickedByOwner { get; set; } // IsPickedByOwner
        public System.DateTime? AssignedOnUtc { get; set; } // AssignedOnUtc
        public int? AssignmentHopCount { get; set; } // AssignmentHopCount
        public int? BranchId { get; set; } // BranchId
        public int? OriginalBusinessUnitId { get; set; } // OriginalBusinessUnitId
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public string CustomerMessage { get; set; } // CustomerMessage (length: 1000)
        public int? AdSourceId { get; set; } // AdSourceId
        public int? LoanRequestId { get; set; } // LoanRequestId
        public System.DateTime? EstimatedClosingDate { get; set; } // EstimatedClosingDate
        public System.DateTime? PickedOnUtc { get; set; } // PickedOnUtc
        public decimal? FinancedFeePaid { get; set; } // FinancedFeePaid
        public int? LockStatusId { get; set; } // LockStatusId
        public int? LockCauseId { get; set; } // LockCauseId
        public bool IsDuplicate { get; set; } // IsDuplicate
        public int? DuplicateOfId { get; set; } // DuplicateOfId
        public int? CopyFromId { get; set; } // CopyFromId
        public int? MoveToId { get; set; } // MoveToId
        public string TpId { get; set; } // TpId (length: 50)
        public System.DateTime? LeadCreatedOnUtc { get; set; } // LeadCreatedOnUtc
        public int? MaxHopCount { get; set; } // MaxHopCount
        public int? LeadGroupId { get; set; } // LeadGroupId
        public int? LoanOfficerId { get; set; } // LoanOfficerId
        public int? LoanCoordinatorId { get; set; } // LoanCoordinatorId
        public int? PreProcessorId { get; set; } // PreProcessorId
        public int? LoanProcessorId { get; set; } // LoanProcessorId

        // Reverse navigation

        /// <summary>
        /// Parent (One-to-One) Opportunity pointed by [DenormOpportunityContact].[Id] (FK_DenormOpportunityContact_Opportunity)
        /// </summary>
        public virtual DenormOpportunityContact DenormOpportunityContact { get; set; } // DenormOpportunityContact.FK_DenormOpportunityContact_Opportunity
        /// <summary>
        /// Parent (One-to-One) Opportunity pointed by [Five9LeadPosting].[Id] (FK_Five9LeadPosting_Opportunity)
        /// </summary>
        public virtual Five9LeadPosting Five9LeadPosting { get; set; } // Five9LeadPosting.FK_Five9LeadPosting_Opportunity
        /// <summary>
        /// Child FollowUps where [FollowUp].[OpportunityId] point to this entity (FK_FollowUp_Opportunity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<FollowUp> FollowUps { get; set; } // FollowUp.FK_FollowUp_Opportunity
        /// <summary>
        /// Child InitialContacts where [InitialContact].[OpportunityId] point to this entity (FK_InitialContact_Opportunity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<InitialContact> InitialContacts { get; set; } // InitialContact.FK_InitialContact_Opportunity
        /// <summary>
        /// Child LoanApplications where [LoanApplication].[OpportunityId] point to this entity (FK_LoanApplication_Opportunity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanApplication> LoanApplications { get; set; } // LoanApplication.FK_LoanApplication_Opportunity
        /// <summary>
        /// Child Notifications where [Notification].[OpportunityId] point to this entity (FK_Notification_Opportunity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Notification> Notifications { get; set; } // Notification.FK_Notification_Opportunity
        /// <summary>
        /// Child OppAssignLogs where [OppAssignLog].[OpportunityId] point to this entity (FK_OppAssignLog_Opportunity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OppAssignLog> OppAssignLogs { get; set; } // OppAssignLog.FK_OppAssignLog_Opportunity
        /// <summary>
        /// Child OpportunityDesireRates where [OpportunityDesireRate].[OpportunityId] point to this entity (FK_OpportunityDesireRate_Opportunity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityDesireRate> OpportunityDesireRates { get; set; } // OpportunityDesireRate.FK_OpportunityDesireRate_Opportunity
        /// <summary>
        /// Child OpportunityFees where [OpportunityFee].[OpportunityId] point to this entity (FK_OpportunityFee_Opportunity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityFee> OpportunityFees { get; set; } // OpportunityFee.FK_OpportunityFee_Opportunity
        /// <summary>
        /// Child OpportunityFeeDetails where [OpportunityFeeDetail].[OpportunityId] point to this entity (FK_OpportunityFeeDetail_Opportunity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityFeeDetail> OpportunityFeeDetails { get; set; } // OpportunityFeeDetail.FK_OpportunityFeeDetail_Opportunity
        /// <summary>
        /// Child OpportunityLeadBinders where [OpportunityLeadBinder].[OpportunityId] point to this entity (FK_OpportunityLeadBinder_Opportunity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityLeadBinder> OpportunityLeadBinders { get; set; } // OpportunityLeadBinder.FK_OpportunityLeadBinder_Opportunity
        /// <summary>
        /// Child OpportunityLockStatusLogs where [OpportunityLockStatusLog].[OpportunityId] point to this entity (FK_OpportunityLockStatusLog_Opportunity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityLockStatusLog> OpportunityLockStatusLogs { get; set; } // OpportunityLockStatusLog.FK_OpportunityLockStatusLog_Opportunity
        /// <summary>
        /// Child OpportunityPropertyTaxes where [OpportunityPropertyTax].[OpportunityId] point to this entity (FK_OpportunityPropertyTax_Opportunity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityPropertyTax> OpportunityPropertyTaxes { get; set; } // OpportunityPropertyTax.FK_OpportunityPropertyTax_Opportunity
        /// <summary>
        /// Child OpportunityStatusLogs where [OpportunityStatusLog].[OpportunityId] point to this entity (FK_OpportunityStatusLog_Opportunity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityStatusLog> OpportunityStatusLogs { get; set; } // OpportunityStatusLog.FK_OpportunityStatusLog_Opportunity
        /// <summary>
        /// Child OtpTracings where [OtpTracing].[OpportunityId] point to this entity (FK_OtpTracing_Opportunity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OtpTracing> OtpTracings { get; set; } // OtpTracing.FK_OtpTracing_Opportunity
        /// <summary>
        /// Child ThirdPartyLeads where [ThirdPartyLead].[OpportunityId] point to this entity (FK_ThirdPartyLead_Opportunity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ThirdPartyLead> ThirdPartyLeads { get; set; } // ThirdPartyLead.FK_ThirdPartyLead_Opportunity
        

        // Foreign keys

        /// <summary>
        /// Parent AdsSource pointed by [Opportunity].([AdSourceId]) (FK_Opportunity_AdsSource)
        /// </summary>
        public virtual AdsSource AdsSource { get; set; } // FK_Opportunity_AdsSource

        /// <summary>
        /// Parent Branch pointed by [Opportunity].([BranchId]) (FK_Opportunity_Branch)
        /// </summary>
        public virtual Branch Branch { get; set; } // FK_Opportunity_Branch

        /// <summary>
        /// Parent BusinessUnit pointed by [Opportunity].([BusinessUnitId]) (FK_Opportunity_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_Opportunity_BusinessUnit

        /// <summary>
        /// Parent Employee pointed by [Opportunity].([LoanCoordinatorId]) (FK_Opportunity_Employee2)
        /// </summary>
        public virtual Employee LoanCoordinator { get; set; } // FK_Opportunity_Employee2

        /// <summary>
        /// Parent Employee pointed by [Opportunity].([LoanOfficerId]) (FK_Opportunity_Employee1)
        /// </summary>
        public virtual Employee LoanOfficer { get; set; } // FK_Opportunity_Employee1

        /// <summary>
        /// Parent Employee pointed by [Opportunity].([LoanProcessorId]) (FK_Opportunity_Employee4)
        /// </summary>
        public virtual Employee LoanProcessor { get; set; } // FK_Opportunity_Employee4

        /// <summary>
        /// Parent Employee pointed by [Opportunity].([OwnerId]) (FK_Opportunity_Employee)
        /// </summary>
        public virtual Employee Owner { get; set; } // FK_Opportunity_Employee

        /// <summary>
        /// Parent Employee pointed by [Opportunity].([PreProcessorId]) (FK_Opportunity_Employee3)
        /// </summary>
        public virtual Employee PreProcessor { get; set; } // FK_Opportunity_Employee3

        /// <summary>
        /// Parent LeadGroup pointed by [Opportunity].([LeadGroupId]) (FK_Opportunity_LeadGroup)
        /// </summary>
        public virtual LeadGroup LeadGroup { get; set; } // FK_Opportunity_LeadGroup

        /// <summary>
        /// Parent LeadSource pointed by [Opportunity].([LeadSourceId]) (FK_Opportunity_LeadSource)
        /// </summary>
        public virtual LeadSource LeadSource_LeadSourceId { get; set; } // FK_Opportunity_LeadSource

        /// <summary>
        /// Parent LeadSource pointed by [Opportunity].([LeadSourceOriginalId]) (FK_Opportunity_OriginalLeadSource)
        /// </summary>
        public virtual LeadSource LeadSourceOriginal { get; set; } // FK_Opportunity_OriginalLeadSource

        /// <summary>
        /// Parent LeadSourceType pointed by [Opportunity].([LeadSourceTypeId]) (FK_Opportunity_LeadSourceType)
        /// </summary>
        public virtual LeadSourceType LeadSourceType { get; set; } // FK_Opportunity_LeadSourceType

        /// <summary>
        /// Parent LeadType pointed by [Opportunity].([LeadTypeId]) (FK_Opportunity_LeadType)
        /// </summary>
        public virtual LeadType LeadType { get; set; } // FK_Opportunity_LeadType

        /// <summary>
        /// Parent LoanRequest pointed by [Opportunity].([LoanRequestId]) (FK_Opportunity_LoanRequest)
        /// </summary>
        public virtual LoanRequest LoanRequest { get; set; } // FK_Opportunity_LoanRequest

        /// <summary>
        /// Parent LockStatusCause pointed by [Opportunity].([LockCauseId]) (FK_Opportunity_LockStatusCause)
        /// </summary>
        public virtual LockStatusCause LockStatusCause { get; set; } // FK_Opportunity_LockStatusCause

        /// <summary>
        /// Parent LockStatusList pointed by [Opportunity].([LockStatusId]) (FK_Opportunity_LockStatusList)
        /// </summary>
        public virtual LockStatusList LockStatusList { get; set; } // FK_Opportunity_LockStatusList

        /// <summary>
        /// Parent StatusCause pointed by [Opportunity].([StatusCauseId]) (FK_Opportunity_StatusCause)
        /// </summary>
        public virtual StatusCause StatusCause { get; set; } // FK_Opportunity_StatusCause

        /// <summary>
        /// Parent StatusList pointed by [Opportunity].([StatusId]) (FK_Opportunity_StatusList)
        /// </summary>
        public virtual StatusList StatusList { get; set; } // FK_Opportunity_StatusList

        /// <summary>
        /// Parent Team pointed by [Opportunity].([TeamId]) (FK_Opportunity_Team)
        /// </summary>
        public virtual Team Team { get; set; } // FK_Opportunity_Team

        public Opportunity()
        {
            IsActive = true;
            EntityTypeId = 148;
            IsDeleted = false;
            IsAutoAssigned = true;
            IsPickedByOwner = false;
            FinancedFeePaid = 0m;
            IsDuplicate = false;
            
            FollowUps = new System.Collections.Generic.HashSet<FollowUp>();
            InitialContacts = new System.Collections.Generic.HashSet<InitialContact>();
            LoanApplications = new System.Collections.Generic.HashSet<LoanApplication>();
            Notifications = new System.Collections.Generic.HashSet<Notification>();
            OppAssignLogs = new System.Collections.Generic.HashSet<OppAssignLog>();
            OpportunityDesireRates = new System.Collections.Generic.HashSet<OpportunityDesireRate>();
            OpportunityFees = new System.Collections.Generic.HashSet<OpportunityFee>();
            OpportunityFeeDetails = new System.Collections.Generic.HashSet<OpportunityFeeDetail>();
            OpportunityLeadBinders = new System.Collections.Generic.HashSet<OpportunityLeadBinder>();
            OpportunityLockStatusLogs = new System.Collections.Generic.HashSet<OpportunityLockStatusLog>();
            OpportunityPropertyTaxes = new System.Collections.Generic.HashSet<OpportunityPropertyTax>();
            OpportunityStatusLogs = new System.Collections.Generic.HashSet<OpportunityStatusLog>();
            OtpTracings = new System.Collections.Generic.HashSet<OtpTracing>();
            ThirdPartyLeads = new System.Collections.Generic.HashSet<ThirdPartyLead>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
