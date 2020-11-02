













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // GiftSource

    public partial class GiftSource 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public bool IsAccountType { get; set; } // IsAccountType
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
        /// Child BorrowerAssets where [BorrowerAsset].[GiftSourceId] point to this entity (FK_BorrowerAsset_GiftSource)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerAsset> BorrowerAssets { get; set; } // BorrowerAsset.FK_BorrowerAsset_GiftSource

        public GiftSource()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 225;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            BorrowerAssets = new System.Collections.Generic.HashSet<BorrowerAsset>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
