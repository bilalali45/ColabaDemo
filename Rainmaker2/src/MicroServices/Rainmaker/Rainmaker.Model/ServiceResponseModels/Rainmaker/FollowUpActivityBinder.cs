namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class FollowUpActivityBinder
    {
        public int FollowUpId { get; set; }
        public int FollowUpActivityId { get; set; }

        public FollowUp FollowUp { get; set; }

        public FollowUpActivity FollowUpActivity { get; set; }
    }
}