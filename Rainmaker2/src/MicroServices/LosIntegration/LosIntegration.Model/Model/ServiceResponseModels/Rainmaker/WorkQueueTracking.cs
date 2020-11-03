













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // WorkQueueTracking

    public partial class WorkQueueTracking 
    {
        public int Id { get; set; } // Id (Primary key)
        public int WorkQueueId { get; set; } // WorkQueueId
        public System.DateTime TrackingOnUtc { get; set; } // TrackingOnUtc
        public int QueueStatusId { get; set; } // QueueStatusId
        public int EntityTypeId { get; set; } // EntityTypeId

        // Foreign keys

        /// <summary>
        /// Parent WorkQueue pointed by [WorkQueueTracking].([WorkQueueId]) (FK_WorkQueueTracking_WorkQueue)
        /// </summary>
        public virtual WorkQueue WorkQueue { get; set; } // FK_WorkQueueTracking_WorkQueue

        public WorkQueueTracking()
        {
            EntityTypeId = 82;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
