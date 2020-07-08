using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Identity.Models
{

    public partial class Employee
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("contactId")]
        public long ContactId { get; set; }

        [JsonProperty("userId")]
        public long? UserId { get; set; }

        [JsonProperty("displayOrder")]
        public long DisplayOrder { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isSystem")]
        public bool IsSystem { get; set; }

        [JsonProperty("modifiedBy")]
        public long? ModifiedBy { get; set; }

        [JsonProperty("modifiedOnUtc")]
        public DateTimeOffset? ModifiedOnUtc { get; set; }

        [JsonProperty("createdBy")]
        public long? CreatedBy { get; set; }

        [JsonProperty("createdOnUtc")]
        public DateTimeOffset? CreatedOnUtc { get; set; }

        [JsonProperty("tpId")]
        public object TpId { get; set; }

        [JsonProperty("entityTypeId")]
        public long EntityTypeId { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("branchId")]
        public object BranchId { get; set; }

        [JsonProperty("nmlsNo")]
        public object NmlsNo { get; set; }

        [JsonProperty("maxLeadQuota")]
        public object MaxLeadQuota { get; set; }

        [JsonProperty("perDayLeadQuota")]
        public object PerDayLeadQuota { get; set; }

        [JsonProperty("autoLeadReassignMins")]
        public object AutoLeadReassignMins { get; set; }

        [JsonProperty("autoLeadAssignAllowed")]
        public bool? AutoLeadAssignAllowed { get; set; }

        [JsonProperty("autoLeadReassignAllowed")]
        public bool? AutoLeadReassignAllowed { get; set; }

        [JsonProperty("emailAccountId")]
        public object EmailAccountId { get; set; }

        [JsonProperty("phoneInfoId")]
        public object PhoneInfoId { get; set; }

        [JsonProperty("phoneExtensionNo")]
        public object PhoneExtensionNo { get; set; }

        [JsonProperty("checkPerformanceBeforeAssigningLead")]
        public bool? CheckPerformanceBeforeAssigningLead { get; set; }

        [JsonProperty("checkLoginBeforeAssigningLead")]
        public bool? CheckLoginBeforeAssigningLead { get; set; }

        [JsonProperty("checkStateLicenseBeforeAssigningLead")]
        public bool? CheckStateLicenseBeforeAssigningLead { get; set; }

        [JsonProperty("leadAssignmentPriority")]
        public object LeadAssignmentPriority { get; set; }

        [JsonProperty("hireDateUtc")]
        public DateTimeOffset? HireDateUtc { get; set; }

        [JsonProperty("leaveDateUtc")]
        public object LeaveDateUtc { get; set; }

        [JsonProperty("scheduleUrl")]
        public object ScheduleUrl { get; set; }

        [JsonProperty("loanUrl")]
        public object LoanUrl { get; set; }

        [JsonProperty("loanLoginUrl")]
        public object LoanLoginUrl { get; set; }

        [JsonProperty("photo")]
        public object Photo { get; set; }

        [JsonProperty("profile")]
        public object Profile { get; set; }

        [JsonProperty("cmsName")]
        public object CmsName { get; set; }

        [JsonProperty("empAssignmentRuleBinders")]
        public List<object> EmpAssignmentRuleBinders { get; set; }

        [JsonProperty("empDepartmentBinders")]
        public List<object> EmpDepartmentBinders { get; set; }

        [JsonProperty("employeeBusinessUnitEmails")]
        public List<object> EmployeeBusinessUnitEmails { get; set; }

        [JsonProperty("employeeCsrLoBinders_EmployeeCsrId")]
        public List<object> EmployeeCsrLoBindersEmployeeCsrId { get; set; }

        [JsonProperty("employeeCsrLoBinders_EmployeeLoId")]
        public List<object> EmployeeCsrLoBindersEmployeeLoId { get; set; }

        [JsonProperty("employeeLicenses")]
        public List<object> EmployeeLicenses { get; set; }

        [JsonProperty("employeePhoneBinders")]
        public List<EmployeePhoneBinder> EmployeePhoneBinders { get; set; }

        [JsonProperty("followUps")]
        public List<object> FollowUps { get; set; }

        [JsonProperty("loanApplications")]
        public List<object> LoanApplications { get; set; }

        [JsonProperty("loanRequests")]
        public List<object> LoanRequests { get; set; }

        [JsonProperty("notifications")]
        public List<object> Notifications { get; set; }

        [JsonProperty("oppAssignLogs")]
        public List<object> OppAssignLogs { get; set; }

        [JsonProperty("opportunities")]
        public List<object> Opportunities { get; set; }

        [JsonProperty("quoteResults")]
        public List<object> QuoteResults { get; set; }

        [JsonProperty("subordinates")]
        public List<object> Subordinates { get; set; }

        [JsonProperty("teamMembers")]
        public List<object> TeamMembers { get; set; }

        [JsonProperty("vortex_FollowUps")]
        public List<object> VortexFollowUps { get; set; }

        [JsonProperty("vortex_VoiceMailAssignments")]
        public List<object> VortexVoiceMailAssignments { get; set; }

        [JsonProperty("vortex_VoiceMailReadBies")]
        public List<object> VortexVoiceMailReadBies { get; set; }

        [JsonProperty("branch")]
        public object Branch { get; set; }

        [JsonProperty("contact")]
        public Contact Contact { get; set; }

        [JsonProperty("emailAccount")]
        public object EmailAccount { get; set; }

        [JsonProperty("entityType")]
        public object EntityType { get; set; }

        [JsonProperty("trackingState")]
        public long TrackingState { get; set; }

        [JsonProperty("modifiedProperties")]
        public object ModifiedProperties { get; set; }

        [JsonProperty("entityIdentifier")]
        public Guid EntityIdentifier { get; set; }
    }

  
}
