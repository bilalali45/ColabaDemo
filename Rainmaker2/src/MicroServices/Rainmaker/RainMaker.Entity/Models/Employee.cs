// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 2.1
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace RainMaker.Entity.Models
{
    // Employee

    public partial class Employee : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? ContactId { get; set; } // ContactId
        public int? UserId { get; set; } // UserId
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string TpId { get; set; } // TpId (length: 50)
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDeleted { get; set; } // IsDeleted
        public int? BranchId { get; set; } // BranchId
        public string NmlsNo { get; set; } // NmlsNo (length: 50)
        public int? MaxLeadQuota { get; set; } // MaxLeadQuota
        public int? PerDayLeadQuota { get; set; } // PerDayLeadQuota
        public int? AutoLeadReassignMins { get; set; } // AutoLeadReassignMins
        public bool? AutoLeadAssignAllowed { get; set; } // AutoLeadAssignAllowed
        public bool? AutoLeadReassignAllowed { get; set; } // AutoLeadReassignAllowed
        public int? EmailAccountId { get; set; } // EmailAccountId
        public int? PhoneInfoId { get; set; } // PhoneInfoId
        public int? PhoneExtensionNo { get; set; } // PhoneExtensionNo
        public bool? CheckPerformanceBeforeAssigningLead { get; set; } // CheckPerformanceBeforeAssigningLead
        public bool? CheckLoginBeforeAssigningLead { get; set; } // CheckLoginBeforeAssigningLead
        public bool? CheckStateLicenseBeforeAssigningLead { get; set; } // CheckStateLicenseBeforeAssigningLead
        public int? LeadAssignmentPriority { get; set; } // LeadAssignmentPriority
        public System.DateTime? HireDateUtc { get; set; } // HireDateUtc
        public System.DateTime? LeaveDateUtc { get; set; } // LeaveDateUtc
        public string ScheduleUrl { get; set; } // ScheduleUrl (length: 500)
        public string LoanUrl { get; set; } // LoanUrl (length: 500)
        public string LoanLoginUrl { get; set; } // LoanLoginUrl (length: 500)
        public string Photo { get; set; } // Photo (length: 500)
        public string Profile { get; set; } // Profile
        public string CmsName { get; set; } // CmsName (length: 50)


        public string EmailTag { get; set; } // EmailTag (length: 100)
        // Reverse navigation

        /// <summary>
        /// Child EmpAssignmentRuleBinders where [EmpAssignmentRuleBinder].[EmployeeId] point to this entity (FK_EmpAssignmentRuleBinder_Employee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmpAssignmentRuleBinder> EmpAssignmentRuleBinders { get; set; } // EmpAssignmentRuleBinder.FK_EmpAssignmentRuleBinder_Employee
        /// <summary>
        /// Child EmpDepartmentBinders where [EmpDepartmentBinder].[EmployeeId] point to this entity (FK_EmpDepartmentBinder_Employee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmpDepartmentBinder> EmpDepartmentBinders { get; set; } // EmpDepartmentBinder.FK_EmpDepartmentBinder_Employee
        /// <summary>
        /// Child EmployeeBusinessUnitEmails where [EmployeeBusinessUnitEmail].[EmployeeId] point to this entity (FK_EmployeeBusinessUnitEmail_Employee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmployeeBusinessUnitEmail> EmployeeBusinessUnitEmails { get; set; } // EmployeeBusinessUnitEmail.FK_EmployeeBusinessUnitEmail_Employee
        /// <summary>
        /// Child EmployeeCsrLoBinders where [EmployeeCsrLoBinder].[EmployeeCsrId] point to this entity (FK_EmployeeCsrLoBinder_Employee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmployeeCsrLoBinder> EmployeeCsrLoBinders_EmployeeCsrId { get; set; } // EmployeeCsrLoBinder.FK_EmployeeCsrLoBinder_Employee
        /// <summary>
        /// Child EmployeeCsrLoBinders where [EmployeeCsrLoBinder].[EmployeeLoId] point to this entity (FK_EmployeeCsrLoBinder_EmployeeLo)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmployeeCsrLoBinder> EmployeeCsrLoBinders_EmployeeLoId { get; set; } // EmployeeCsrLoBinder.FK_EmployeeCsrLoBinder_EmployeeLo
        /// <summary>
        /// Child EmployeeLicenses where [EmployeeLicense].[EmployeeId] point to this entity (FK_EmployeeLicense_Employee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmployeeLicense> EmployeeLicenses { get; set; } // EmployeeLicense.FK_EmployeeLicense_Employee
        /// <summary>
        /// Child EmployeePhoneBinders where [EmployeePhoneBinder].[EmployeeId] point to this entity (FK_EmployeePhoneBinder_Employee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmployeePhoneBinder> EmployeePhoneBinders { get; set; } // EmployeePhoneBinder.FK_EmployeePhoneBinder_Employee
        /// <summary>
        /// Child FollowUps where [FollowUp].[EmployeeId] point to this entity (FK_FollowUp_Employee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<FollowUp> FollowUps { get; set; } // FollowUp.FK_FollowUp_Employee
        /// <summary>
        /// Child LoanApplications where [LoanApplication].[LoanOriginatorId] point to this entity (FK_LoanApplication_Employee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanApplication> LoanApplications { get; set; } // LoanApplication.FK_LoanApplication_Employee
        /// <summary>
        /// Child LoanRequests where [LoanRequest].[EmployeId] point to this entity (FK_LoanRequest_Employee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequest> LoanRequests { get; set; } // LoanRequest.FK_LoanRequest_Employee
        /// <summary>
        /// Child Notifications where [Notification].[EmployeeId] point to this entity (FK_Notification_Employee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Notification> Notifications { get; set; } // Notification.FK_Notification_Employee
        /// <summary>
        /// Child OppAssignLogs where [OppAssignLog].[EmployeeId] point to this entity (FK_OppAssignLog_Employee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OppAssignLog> OppAssignLogs { get; set; } // OppAssignLog.FK_OppAssignLog_Employee
        /// <summary>
        /// Child Opportunities where [Opportunity].[LoanCoordinatorId] point to this entity (FK_Opportunity_Employee2)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Opportunity> Opportunities_LoanCoordinatorId { get; set; } // Opportunity.FK_Opportunity_Employee2
        /// <summary>
        /// Child Opportunities where [Opportunity].[LoanProcessorId] point to this entity (FK_Opportunity_Employee4)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Opportunity> Opportunities_LoanProcessorId { get; set; } // Opportunity.FK_Opportunity_Employee4
        /// <summary>
        /// Child Opportunities where [Opportunity].[OwnerId] point to this entity (FK_Opportunity_Employee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Opportunity> Opportunities_OwnerId { get; set; } // Opportunity.FK_Opportunity_Employee
        /// <summary>
        /// Child Opportunities where [Opportunity].[PreProcessorId] point to this entity (FK_Opportunity_Employee3)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Opportunity> Opportunities_PreProcessorId { get; set; } // Opportunity.FK_Opportunity_Employee3
        /// <summary>
        /// Child QuoteResults where [QuoteResult].[EmployeId] point to this entity (FK_QuoteResult_Employee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<QuoteResult> QuoteResults { get; set; } // QuoteResult.FK_QuoteResult_Employee
        /// <summary>
        /// Child Subordinates where [Subordinate].[EmployeeId] point to this entity (FK_Subordinate_Employee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Subordinate> Subordinates { get; set; } // Subordinate.FK_Subordinate_Employee
        /// <summary>
        /// Child TeamMembers where [TeamMember].[EmployeeId] point to this entity (FK_TeamMember_Employee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TeamMember> TeamMembers { get; set; } // TeamMember.FK_TeamMember_Employee
        /// <summary>
        /// Child Vortex_FollowUps where [FollowUp].[EmployeeId] point to this entity (FK_FollowUp_Employee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Vortex_FollowUp> Vortex_FollowUps { get; set; } // FollowUp.FK_FollowUp_Employee
        /// <summary>
        /// Child Vortex_VoiceMailAssignments where [VoiceMailAssignment].[EmployeeId] point to this entity (FK_VoiceMailAssignment_Employee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Vortex_VoiceMailAssignment> Vortex_VoiceMailAssignments { get; set; } // VoiceMailAssignment.FK_VoiceMailAssignment_Employee
        /// <summary>
        /// Child Vortex_VoiceMailReadBies where [VoiceMailReadBy].[EmployeeId] point to this entity (FK_VoiceMailReadBy_Employee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Vortex_VoiceMailReadBy> Vortex_VoiceMailReadBies { get; set; } // VoiceMailReadBy.FK_VoiceMailReadBy_Employee

        // Foreign keys

        /// <summary>
        /// Parent Branch pointed by [Employee].([BranchId]) (FK_Employee_Branch)
        /// </summary>
        public virtual Branch Branch { get; set; } // FK_Employee_Branch

        /// <summary>
        /// Parent Contact pointed by [Employee].([ContactId]) (FK_Employee_Contact)
        /// </summary>
        public virtual Contact Contact { get; set; } // FK_Employee_Contact

        /// <summary>
        /// Parent EmailAccount pointed by [Employee].([EmailAccountId]) (FK_Employee_EmailAccount)
        /// </summary>
        public virtual EmailAccount EmailAccount { get; set; } // FK_Employee_EmailAccount

        /// <summary>
        /// Parent EntityType pointed by [Employee].([EntityTypeId]) (FK_Employee_EntityType)
        /// </summary>
        public virtual EntityType EntityType { get; set; } // FK_Employee_EntityType

        /// <summary>
        /// Parent UserProfile pointed by [Employee].([UserId]) (FK_Employee_UserProfile)
        /// </summary>
        public virtual UserProfile UserProfile { get; set; } // FK_Employee_UserProfile

        public Employee()
        {
            DisplayOrder = 0;
            IsActive = true;
            IsSystem = false;
            EntityTypeId = 45;
            IsDeleted = false;
            EmpAssignmentRuleBinders = new System.Collections.Generic.HashSet<EmpAssignmentRuleBinder>();
            EmpDepartmentBinders = new System.Collections.Generic.HashSet<EmpDepartmentBinder>();
            EmployeeBusinessUnitEmails = new System.Collections.Generic.HashSet<EmployeeBusinessUnitEmail>();
            EmployeeCsrLoBinders_EmployeeCsrId = new System.Collections.Generic.HashSet<EmployeeCsrLoBinder>();
            EmployeeCsrLoBinders_EmployeeLoId = new System.Collections.Generic.HashSet<EmployeeCsrLoBinder>();
            EmployeeLicenses = new System.Collections.Generic.HashSet<EmployeeLicense>();
            EmployeePhoneBinders = new System.Collections.Generic.HashSet<EmployeePhoneBinder>();
            FollowUps = new System.Collections.Generic.HashSet<FollowUp>();
            Vortex_FollowUps = new System.Collections.Generic.HashSet<Vortex_FollowUp>();
            LoanApplications = new System.Collections.Generic.HashSet<LoanApplication>();
            LoanRequests = new System.Collections.Generic.HashSet<LoanRequest>();
            Notifications = new System.Collections.Generic.HashSet<Notification>();
            OppAssignLogs = new System.Collections.Generic.HashSet<OppAssignLog>();
            Opportunities_LoanCoordinatorId = new System.Collections.Generic.HashSet<Opportunity>();
            Opportunities_LoanProcessorId = new System.Collections.Generic.HashSet<Opportunity>();
            Opportunities_OwnerId = new System.Collections.Generic.HashSet<Opportunity>();
            Opportunities_PreProcessorId = new System.Collections.Generic.HashSet<Opportunity>();
            QuoteResults = new System.Collections.Generic.HashSet<QuoteResult>();
            Subordinates = new System.Collections.Generic.HashSet<Subordinate>();
            TeamMembers = new System.Collections.Generic.HashSet<TeamMember>();
            Vortex_VoiceMailAssignments = new System.Collections.Generic.HashSet<Vortex_VoiceMailAssignment>();
            Vortex_VoiceMailReadBies = new System.Collections.Generic.HashSet<Vortex_VoiceMailReadBy>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
