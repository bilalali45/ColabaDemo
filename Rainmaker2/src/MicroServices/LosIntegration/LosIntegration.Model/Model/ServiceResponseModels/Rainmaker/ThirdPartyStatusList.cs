













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // ThirdPartyStatusList

    public partial class ThirdPartyStatusList 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child ThirdPartyLeads where [ThirdPartyLead].[StatusId] point to this entity (FK_ThirdPartyLead_ThirdPartyStatusList)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ThirdPartyLead> ThirdPartyLeads { get; set; } // ThirdPartyLead.FK_ThirdPartyLead_ThirdPartyStatusList

        public ThirdPartyStatusList()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 176;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            ThirdPartyLeads = new System.Collections.Generic.HashSet<ThirdPartyLead>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
