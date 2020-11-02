













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // OpportunityLeadBinder

    public partial class OpportunityLeadBinder 
    {
        public int Id { get; set; } // Id (Primary key)
        public int OpportunityId { get; set; } // OpportunityId
        public int CustomerId { get; set; } // CustomerId
        public int OwnTypeId { get; set; } // OwnTypeId

        // Foreign keys

        /// <summary>
        /// Parent Customer pointed by [OpportunityLeadBinder].([CustomerId]) (FK_OpportunityLeadBinder_Customer)
        /// </summary>
        public virtual Customer Customer { get; set; } // FK_OpportunityLeadBinder_Customer

        /// <summary>
        /// Parent Opportunity pointed by [OpportunityLeadBinder].([OpportunityId]) (FK_OpportunityLeadBinder_Opportunity)
        /// </summary>
        public virtual Opportunity Opportunity { get; set; } // FK_OpportunityLeadBinder_Opportunity

        /// <summary>
        /// Parent OwnType pointed by [OpportunityLeadBinder].([OwnTypeId]) (FK_OpportunityLeadBinder_OwnType)
        /// </summary>
        public virtual OwnType OwnType { get; set; } // FK_OpportunityLeadBinder_OwnType

        public OpportunityLeadBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
