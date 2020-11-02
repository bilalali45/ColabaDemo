













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LeadGroup

    public partial class LeadGroup 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child Opportunities where [Opportunity].[LeadGroupId] point to this entity (FK_Opportunity_LeadGroup)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Opportunity> Opportunities { get; set; } // Opportunity.FK_Opportunity_LeadGroup

        public LeadGroup()
        {
            DisplayOrder = 0;
            EntityTypeId = 232;
            IsDefault = false;
            IsSystem = false;
            Opportunities = new System.Collections.Generic.HashSet<Opportunity>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
