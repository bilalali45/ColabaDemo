













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // InitialContact

    public partial class InitialContact 
    {
        public int Id { get; set; } // Id (Primary key)
        public string FirstName { get; set; } // FirstName (length: 300)
        public string LastName { get; set; } // LastName (length: 300)
        public string Email { get; set; } // Email (length: 150)
        public string Phone { get; set; } // Phone (length: 150)
        public int? OpportunityId { get; set; } // OpportunityId
        public int? VisitorId { get; set; } // VisitorId
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc

        // Foreign keys

        /// <summary>
        /// Parent Opportunity pointed by [InitialContact].([OpportunityId]) (FK_InitialContact_Opportunity)
        /// </summary>
        public virtual Opportunity Opportunity { get; set; } // FK_InitialContact_Opportunity

        /// <summary>
        /// Parent Visitor pointed by [InitialContact].([VisitorId]) (FK_InitialContact_Visitor)
        /// </summary>
        public virtual Visitor Visitor { get; set; } // FK_InitialContact_Visitor

        public InitialContact()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
