using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class Employee
    {
        public int Id { get; set; }
        public int? ContactId { get; set; }
        public int? UserId { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public string TpId { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public int? BranchId { get; set; }
        public string NmlsNo { get; set; }
        public int? MaxLeadQuota { get; set; }
        public int? PerDayLeadQuota { get; set; }
        public int? AutoLeadReassignMins { get; set; }
        public bool? AutoLeadAssignAllowed { get; set; }
        public bool? AutoLeadReassignAllowed { get; set; }
        public int? EmailAccountId { get; set; }
        public int? PhoneInfoId { get; set; }
        public int? PhoneExtensionNo { get; set; }
        public bool? CheckPerformanceBeforeAssigningLead { get; set; }
        public bool? CheckLoginBeforeAssigningLead { get; set; }
        public bool? CheckStateLicenseBeforeAssigningLead { get; set; }
        public int? LeadAssignmentPriority { get; set; }
        public DateTime? HireDateUtc { get; set; }
        public DateTime? LeaveDateUtc { get; set; }
        public string ScheduleUrl { get; set; }
        public string LoanUrl { get; set; }
        public string LoanLoginUrl { get; set; }
        public string Photo { get; set; }
        public string Profile { get; set; }
        public string CmsName { get; set; }

        public string EmailTag { get; set; }

        //public System.Collections.Generic.ICollection<EmpAssignmentRuleBinder> EmpAssignmentRuleBinders { get; set; }

        //public System.Collections.Generic.ICollection<EmpDepartmentBinder> EmpDepartmentBinders { get; set; }

        public ICollection<EmployeeBusinessUnitEmail> EmployeeBusinessUnitEmails { get; set; }

        //public System.Collections.Generic.ICollection<EmployeeCsrLoBinder> EmployeeCsrLoBinders_EmployeeCsrId { get; set; }

        //public System.Collections.Generic.ICollection<EmployeeCsrLoBinder> EmployeeCsrLoBinders_EmployeeLoId { get; set; }

        //public System.Collections.Generic.ICollection<EmployeeLicense> EmployeeLicenses { get; set; }

        public ICollection<EmployeePhoneBinder> EmployeePhoneBinders { get; set; }

        //public System.Collections.Generic.ICollection<FollowUp> FollowUps { get; set; }

        //public System.Collections.Generic.ICollection<LoanApplication> LoanApplications { get; set; }

        public ICollection<LoanRequest> LoanRequests { get; set; }

        //public System.Collections.Generic.ICollection<Notification> Notifications { get; set; }

        //public System.Collections.Generic.ICollection<OppAssignLog> OppAssignLogs { get; set; }

        //public System.Collections.Generic.ICollection<Opportunity> Opportunities_LoanCoordinatorId { get; set; }

        //public System.Collections.Generic.ICollection<Opportunity> Opportunities_LoanProcessorId { get; set; }

        //public System.Collections.Generic.ICollection<Opportunity> Opportunities_OwnerId { get; set; }

        //public System.Collections.Generic.ICollection<Opportunity> Opportunities_PreProcessorId { get; set; }

        //        public System.Collections.Generic.ICollection<QuoteResult> QuoteResults { get; set; }

        //public System.Collections.Generic.ICollection<Subordinate> Subordinates { get; set; }

        //public System.Collections.Generic.ICollection<TeamMember> TeamMembers { get; set; }

        //public System.Collections.Generic.ICollection<Vortex_FollowUp> Vortex_FollowUps { get; set; }

        //public System.Collections.Generic.ICollection<Vortex_VoiceMailAssignment> Vortex_VoiceMailAssignments { get; set; }

        //public System.Collections.Generic.ICollection<Vortex_VoiceMailReadBy> Vortex_VoiceMailReadBies { get; set; }

        //public Branch Branch { get; set; }

        public Contact Contact { get; set; }

        //public EmailAccount EmailAccount { get; set; }

        //public EntityType EntityType { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}