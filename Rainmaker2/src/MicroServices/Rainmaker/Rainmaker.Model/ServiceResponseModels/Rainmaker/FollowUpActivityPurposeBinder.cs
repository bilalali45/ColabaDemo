namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class FollowUpActivityPurposeBinder
    {
        public int FollowUpPurposeId { get; set; }
        public int FollowUpActivityId { get; set; }

        public FollowUpActivity FollowUpActivity { get; set; }

        public FollowUpPurpose FollowUpPurpose { get; set; }
    }
}