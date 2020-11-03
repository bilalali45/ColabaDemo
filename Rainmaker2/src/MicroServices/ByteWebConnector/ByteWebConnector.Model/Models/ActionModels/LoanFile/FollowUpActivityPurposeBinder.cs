













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // FollowUpActivityPurposeBinder

    public partial class FollowUpActivityPurposeBinder 
    {
        public int FollowUpPurposeId { get; set; } // FollowUpPurposeId (Primary key)
        public int FollowUpActivityId { get; set; } // FollowUpActivityId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent FollowUpActivity pointed by [FollowUpActivityPurposeBinder].([FollowUpActivityId]) (FK_FollowUpActivityPurposeBinder_FollowUpActivity)
        /// </summary>
        public virtual FollowUpActivity FollowUpActivity { get; set; } // FK_FollowUpActivityPurposeBinder_FollowUpActivity

        /// <summary>
        /// Parent FollowUpPurpose pointed by [FollowUpActivityPurposeBinder].([FollowUpPurposeId]) (FK_FollowUpActivityPurposeBinder_FollowUpPurpose)
        /// </summary>
        public virtual FollowUpPurpose FollowUpPurpose { get; set; } // FK_FollowUpActivityPurposeBinder_FollowUpPurpose

        public FollowUpActivityPurposeBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
