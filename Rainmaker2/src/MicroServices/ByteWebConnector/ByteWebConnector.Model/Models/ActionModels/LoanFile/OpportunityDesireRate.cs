













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // OpportunityDesireRate

    public partial class OpportunityDesireRate 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? OpportunityId { get; set; } // OpportunityId
        public int? ProductTypeId { get; set; } // ProductTypeId
        public decimal? Rate { get; set; } // Rate

        // Foreign keys

        /// <summary>
        /// Parent Opportunity pointed by [OpportunityDesireRate].([OpportunityId]) (FK_OpportunityDesireRate_Opportunity)
        /// </summary>
        public virtual Opportunity Opportunity { get; set; } // FK_OpportunityDesireRate_Opportunity

        /// <summary>
        /// Parent ProductType pointed by [OpportunityDesireRate].([ProductTypeId]) (FK_OpportunityDesireRate_ProductType)
        /// </summary>
        public virtual ProductType ProductType { get; set; } // FK_OpportunityDesireRate_ProductType

        public OpportunityDesireRate()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
