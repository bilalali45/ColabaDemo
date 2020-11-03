













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // WorkQueueKeyValue

    public partial class WorkQueueKeyValue 
    {
        public int Id { get; set; } // Id (Primary key)
        public string KeyName { get; set; } // KeyName (length: 50)
        public string Value { get; set; } // Value (length: 1073741823)
        public int? WorkQueueId { get; set; } // WorkQueueId
        public bool IsAttachment { get; set; } // IsAttachment

        // Foreign keys

        /// <summary>
        /// Parent WorkQueue pointed by [WorkQueueKeyValue].([WorkQueueId]) (FK_WorkQueueKeyValue_WorkQueue)
        /// </summary>
        public virtual WorkQueue WorkQueue { get; set; } // FK_WorkQueueKeyValue_WorkQueue

        public WorkQueueKeyValue()
        {
            IsAttachment = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
