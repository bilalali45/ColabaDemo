













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // CampaignTrigger

    public partial class CampaignTrigger 
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
        /// Child CampaignTriggerBinders where [CampaignTriggerBinder].[CampaignTriggerId] point to this entity (FK_CampaignTriggerBinder_CampaignTrigger)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CampaignTriggerBinder> CampaignTriggerBinders { get; set; } // CampaignTriggerBinder.FK_CampaignTriggerBinder_CampaignTrigger

        public CampaignTrigger()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 155;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            CampaignTriggerBinders = new System.Collections.Generic.HashSet<CampaignTriggerBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
