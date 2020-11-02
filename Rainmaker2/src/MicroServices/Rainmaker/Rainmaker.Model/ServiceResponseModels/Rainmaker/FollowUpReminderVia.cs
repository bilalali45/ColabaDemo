namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class FollowUpReminderVia
    {
        public int Id { get; set; }
        public int FollowUpId { get; set; }

        public int FollowUpViaId { get; set; }
        //        public int? WorkQueueId { get; set; }

        public FollowUp FollowUp { get; set; }

        //        public WorkQueue WorkQueue { get; set; }
    }
}