













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // FollowUpActivityBinder

    public partial class FollowUpActivityBinder 
    {
        public int FollowUpId { get; set; } // FollowUpId (Primary key)
        public int FollowUpActivityId { get; set; } // FollowUpActivityId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent FollowUp pointed by [FollowUpActivityBinder].([FollowUpId]) (FK_FollowUpActivityBinder_FollowUp)
        /// </summary>
        public virtual FollowUp FollowUp { get; set; } // FK_FollowUpActivityBinder_FollowUp

        /// <summary>
        /// Parent FollowUpActivity pointed by [FollowUpActivityBinder].([FollowUpActivityId]) (FK_FollowUpActivityBinder_FollowUpActivity)
        /// </summary>
        public virtual FollowUpActivity FollowUpActivity { get; set; } // FK_FollowUpActivityBinder_FollowUpActivity

        public FollowUpActivityBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
