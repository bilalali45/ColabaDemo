













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // FollowUpReminderVia

    public partial class FollowUpReminderVia 
    {
        public int Id { get; set; } // Id (Primary key)
        public int FollowUpId { get; set; } // FollowUpId
        public int FollowUpViaId { get; set; } // FollowUpViaId
        public int? WorkQueueId { get; set; } // WorkQueueId

        // Foreign keys

        /// <summary>
        /// Parent FollowUp pointed by [FollowUpReminderVia].([FollowUpId]) (FK_FollowUpReminderVia_FollowUp)
        /// </summary>
        public virtual FollowUp FollowUp { get; set; } // FK_FollowUpReminderVia_FollowUp

        /// <summary>
        /// Parent WorkQueue pointed by [FollowUpReminderVia].([WorkQueueId]) (FK_FollowUpReminderVia_WorkQueue)
        /// </summary>
        public virtual WorkQueue WorkQueue { get; set; } // FK_FollowUpReminderVia_WorkQueue

        public FollowUpReminderVia()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
