using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class Opportunity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public int? StatusId { get; set; }
        public bool? IsValid { get; set; }
        public int? LeadSourceId { get; set; }
        public int? LeadSourceTypeId { get; set; }
        public int? LeadTypeId { get; set; }
        public int? LeadSourceOriginalId { get; set; }
        public int? LeadCreatedFromId { get; set; }
        public string ReferUrl { get; set; }
        public int? StatusCauseId { get; set; }
        public int? OwnerId { get; set; }
        public int? TeamId { get; set; }
        public bool NoRuleMatched { get; set; }
        public int? AssignmentFlagId { get; set; }
        public bool IsAutoAssigned { get; set; }
        public bool IsPickedByOwner { get; set; }
        public DateTime? AssignedOnUtc { get; set; }
        public int? AssignmentHopCount { get; set; }
        public int? BranchId { get; set; }
        public int? OriginalBusinessUnitId { get; set; }
        public int? BusinessUnitId { get; set; }
        public string CustomerMessage { get; set; }
        public int? AdSourceId { get; set; }
        public int? LoanRequestId { get; set; }
        public DateTime? EstimatedClosingDate { get; set; }
        public DateTime? PickedOnUtc { get; set; }
        public decimal? FinancedFeePaid { get; set; }
        public int? LockStatusId { get; set; }
        public int? LockCauseId { get; set; }
        public bool IsDuplicate { get; set; }
        public int? DuplicateOfId { get; set; }
        public int? CopyFromId { get; set; }
        public int? MoveToId { get; set; }
        public string TpId { get; set; }
        public DateTime? LeadCreatedOnUtc { get; set; }
        public int? MaxHopCount { get; set; }
        public int? LeadGroupId { get; set; }
        public int? LoanCoordinatorId { get; set; }
        public int? PreProcessorId { get; set; }
        public int? LoanProcessorId { get; set; }

        //public DenormOpportunityContact DenormOpportunityContact { get; set; }

        //public Five9LeadPosting Five9LeadPosting { get; set; }

        //public System.Collections.Generic.ICollection<FollowUp> FollowUps { get; set; }

        //public System.Collections.Generic.ICollection<InitialContact> InitialContacts { get; set; }

        //public System.Collections.Generic.ICollection<LoanApplication> LoanApplications { get; set; }

        //public System.Collections.Generic.ICollection<Notification> Notifications { get; set; }

        //public System.Collections.Generic.ICollection<OppAssignLog> OppAssignLogs { get; set; }

        //public System.Collections.Generic.ICollection<OpportunityDesireRate> OpportunityDesireRates { get; set; }

        //public System.Collections.Generic.ICollection<OpportunityFee> OpportunityFees { get; set; }

        //public System.Collections.Generic.ICollection<OpportunityFeeDetail> OpportunityFeeDetails { get; set; }

        public ICollection<OpportunityLeadBinder> OpportunityLeadBinders { get; set; }

        //public System.Collections.Generic.ICollection<OpportunityLockStatusLog> OpportunityLockStatusLogs { get; set; }

        public ICollection<OpportunityPropertyTax> OpportunityPropertyTaxes { get; set; }

        public ICollection<OpportunityStatusLog> OpportunityStatusLogs { get; set; }

        //public System.Collections.Generic.ICollection<OtpTracing> OtpTracings { get; set; }

        //public System.Collections.Generic.ICollection<ThirdPartyLead> ThirdPartyLeads { get; set; }

        //public System.Collections.Generic.ICollection<Vortex_ActivityLog> Vortex_ActivityLogs { get; set; }

        //public AdsSource AdsSource { get; set; }

        //public Branch Branch { get; set; }

        public BusinessUnit BusinessUnit { get; set; }

        public Employee LoanCoordinator { get; set; }

        public Employee LoanProcessor { get; set; }

        public Employee Owner { get; set; }

        public Employee PreProcessor { get; set; }

        //public LeadGroup LeadGroup { get; set; }

        //public LeadSource LeadSource_LeadSourceId { get; set; }

        //public LeadSource LeadSourceOriginal { get; set; }

        //public LeadSourceType LeadSourceType { get; set; }

        //public LeadType LeadType { get; set; }

        public LoanRequest LoanRequest { get; set; }

        //public LockStatusCause LockStatusCause { get; set; }

        //public LockStatusList LockStatusList { get; set; }

        //public StatusCause StatusCause { get; set; }

        public StatusList StatusList { get; set; }

        //public Team Team { get; set; }
    }
}