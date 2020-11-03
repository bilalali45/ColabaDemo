













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // WorkQueue

    public partial class WorkQueue 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Code { get; set; } // Code (length: 50)
        public int? RandomNo { get; set; } // RandomNo
        public int? CampaignId { get; set; } // CampaignId
        public int? ActivityTypeId { get; set; } // ActivityTypeId
        public int? ActivityId { get; set; } // ActivityId
        public int? TemplateId { get; set; } // TemplateId
        public System.DateTime ScheduleDateUtc { get; set; } // ScheduleDateUtc
        public System.DateTime? StartDateUtc { get; set; } // StartDateUtc
        public System.DateTime? EndDateUtc { get; set; } // EndDateUtc
        public int? Tries { get; set; } // Tries
        public int? QuoteResultId { get; set; } // QuoteResultId
        public string PriceIds { get; set; } // PriceIds (length: 100)
        public int? ProductId { get; set; } // ProductId
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsActive { get; set; } // IsActive
        public bool IsDeleted { get; set; } // IsDeleted
        public string Remarks { get; set; } // Remarks
        public int? LoanRequestId { get; set; } // LoanRequestId
        public string ToAddress { get; set; } // ToAddress (length: 300)
        public string CcAddress { get; set; } // CcAddress (length: 300)
        public string BccAddress { get; set; } // BccAddress (length: 300)
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? EntityRefTypeId { get; set; } // EntityRefTypeId
        public int? EntityRefId { get; set; } // EntityRefId
        public int? QueueStatusId { get; set; } // QueueStatusId
        public int? Priority { get; set; } // Priority
        public bool IsCustom { get; set; } // IsCustom
        public int? BusinessUnitId { get; set; } // BusinessUnitId

        // Reverse navigation

        /// <summary>
        /// Child EmailLogs where [EmailLog].[WorkQueueId] point to this entity (FK_EmailLog_WorkQueue)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmailLog> EmailLogs { get; set; } // EmailLog.FK_EmailLog_WorkQueue
        /// <summary>
        /// Child FollowUpReminderVias where [FollowUpReminderVia].[WorkQueueId] point to this entity (FK_FollowUpReminderVia_WorkQueue)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<FollowUpReminderVia> FollowUpReminderVias { get; set; } // FollowUpReminderVia.FK_FollowUpReminderVia_WorkQueue
        /// <summary>
        /// Child WorkQueueKeyValues where [WorkQueueKeyValue].[WorkQueueId] point to this entity (FK_WorkQueueKeyValue_WorkQueue)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<WorkQueueKeyValue> WorkQueueKeyValues { get; set; } // WorkQueueKeyValue.FK_WorkQueueKeyValue_WorkQueue
        /// <summary>
        /// Child WorkQueueTrackings where [WorkQueueTracking].[WorkQueueId] point to this entity (FK_WorkQueueTracking_WorkQueue)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<WorkQueueTracking> WorkQueueTrackings { get; set; } // WorkQueueTracking.FK_WorkQueueTracking_WorkQueue

        // Foreign keys

        /// <summary>
        /// Parent Activity pointed by [WorkQueue].([ActivityId]) (FK_WorkQueue_Activity)
        /// </summary>
        public virtual Activity Activity { get; set; } // FK_WorkQueue_Activity

        /// <summary>
        /// Parent ActivityType pointed by [WorkQueue].([ActivityTypeId]) (FK_WorkQueue_ActivityType)
        /// </summary>
        public virtual ActivityType ActivityType { get; set; } // FK_WorkQueue_ActivityType

        /// <summary>
        /// Parent BusinessUnit pointed by [WorkQueue].([BusinessUnitId]) (FK_WorkQueue_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_WorkQueue_BusinessUnit

        /// <summary>
        /// Parent Campaign pointed by [WorkQueue].([CampaignId]) (FK_WorkQueue_Campaign)
        /// </summary>
        public virtual Campaign Campaign { get; set; } // FK_WorkQueue_Campaign

        /// <summary>
        /// Parent LoanRequest pointed by [WorkQueue].([LoanRequestId]) (FK_WorkQueue_LoanRequest)
        /// </summary>
        public virtual LoanRequest LoanRequest { get; set; } // FK_WorkQueue_LoanRequest

        /// <summary>
        /// Parent QuoteResult pointed by [WorkQueue].([QuoteResultId]) (FK_WorkQueue_QuoteResult)
        /// </summary>
        public virtual QuoteResult QuoteResult { get; set; } // FK_WorkQueue_QuoteResult

        /// <summary>
        /// Parent Template pointed by [WorkQueue].([TemplateId]) (FK_WorkQueue_Template)
        /// </summary>
        public virtual Template Template { get; set; } // FK_WorkQueue_Template

        public WorkQueue()
        {
            IsActive = true;
            IsDeleted = false;
            EntityTypeId = 68;
            IsCustom = false;
            EmailLogs = new System.Collections.Generic.HashSet<EmailLog>();
            FollowUpReminderVias = new System.Collections.Generic.HashSet<FollowUpReminderVia>();
            WorkQueueKeyValues = new System.Collections.Generic.HashSet<WorkQueueKeyValue>();
            WorkQueueTrackings = new System.Collections.Generic.HashSet<WorkQueueTracking>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
