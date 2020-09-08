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
    // FollowUp

    public partial class FollowUp : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Subject { get; set; } // Subject (length: 150)
        public string Message { get; set; } // Message (length: 500)
        public bool IsAnytime { get; set; } // IsAnytime
        public int? FollowUpPurposeId { get; set; } // FollowUpPurposeId
        public int? OpportunityId { get; set; } // OpportunityId
        public int? ContactId { get; set; } // ContactId
        public int? ContactPhoneId { get; set; } // ContactPhoneId
        public int? ContactEmailId { get; set; } // ContactEmailId
        public int EmployeeId { get; set; } // EmployeeId
        public System.DateTime? FollowUpStartDateUtc { get; set; } // FollowUpStartDateUtc
        public System.DateTime? FollowUpEndDateUtc { get; set; } // FollowUpEndDateUtc
        public System.DateTime? RemindOnUtc { get; set; } // RemindOnUtc
        public System.DateTime? FollowedUpOn { get; set; } // FollowedUpOn
        public string ActivityMessage { get; set; } // ActivityMessage (length: 1000)
        public int StatusId { get; set; } // StatusId
        public int? FollowUpPriorityId { get; set; } // FollowUpPriorityId
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child FollowUpActivityBinders where [FollowUpActivityBinder].[FollowUpId] point to this entity (FK_FollowUpActivityBinder_FollowUp)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<FollowUpActivityBinder> FollowUpActivityBinders { get; set; } // FollowUpActivityBinder.FK_FollowUpActivityBinder_FollowUp
        /// <summary>
        /// Child FollowUpReminderVias where [FollowUpReminderVia].[FollowUpId] point to this entity (FK_FollowUpReminderVia_FollowUp)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<FollowUpReminderVia> FollowUpReminderVias { get; set; } // FollowUpReminderVia.FK_FollowUpReminderVia_FollowUp

        // Foreign keys

        /// <summary>
        /// Parent Contact pointed by [FollowUp].([ContactId]) (FK_FollowUp_Contact)
        /// </summary>
        public virtual Contact Contact { get; set; } // FK_FollowUp_Contact

        /// <summary>
        /// Parent ContactEmailInfo pointed by [FollowUp].([ContactEmailId]) (FK_FollowUp_ContactEmailInfo)
        /// </summary>
        public virtual ContactEmailInfo ContactEmailInfo { get; set; } // FK_FollowUp_ContactEmailInfo

        /// <summary>
        /// Parent ContactPhoneInfo pointed by [FollowUp].([ContactPhoneId]) (FK_FollowUp_ContactPhoneInfo)
        /// </summary>
        public virtual ContactPhoneInfo ContactPhoneInfo { get; set; } // FK_FollowUp_ContactPhoneInfo

        /// <summary>
        /// Parent Employee pointed by [FollowUp].([EmployeeId]) (FK_FollowUp_Employee)
        /// </summary>
        public virtual Employee Employee { get; set; } // FK_FollowUp_Employee

        /// <summary>
        /// Parent FollowUpPriority pointed by [FollowUp].([FollowUpPriorityId]) (FK_FollowUp_FollowUpPriority)
        /// </summary>
        public virtual FollowUpPriority FollowUpPriority { get; set; } // FK_FollowUp_FollowUpPriority

        /// <summary>
        /// Parent FollowUpPurpose pointed by [FollowUp].([FollowUpPurposeId]) (FK_FollowUp_FollowUpPurpose)
        /// </summary>
        public virtual FollowUpPurpose FollowUpPurpose { get; set; } // FK_FollowUp_FollowUpPurpose

        /// <summary>
        /// Parent Opportunity pointed by [FollowUp].([OpportunityId]) (FK_FollowUp_Opportunity)
        /// </summary>
        public virtual Opportunity Opportunity { get; set; } // FK_FollowUp_Opportunity

        public FollowUp()
        {
            EmployeeId = 0;
            EntityTypeId = 165;
            IsDeleted = false;
            FollowUpActivityBinders = new System.Collections.Generic.HashSet<FollowUpActivityBinder>();
            FollowUpReminderVias = new System.Collections.Generic.HashSet<FollowUpReminderVia>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
