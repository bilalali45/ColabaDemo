













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // AdsPromotion

    public partial class AdsPromotion 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child AdsSources where [AdsSource].[AdsPromotionId] point to this entity (FK_AdsSource_AdsPromotion)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AdsSource> AdsSources { get; set; } // AdsSource.FK_AdsSource_AdsPromotion

        public AdsPromotion()
        {
            DisplayOrder = 0;
            IsActive = true;
            IsDefault = false;
            IsSystem = false;
            EntityTypeId = 35;
            IsDeleted = false;
            AdsSources = new System.Collections.Generic.HashSet<AdsSource>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
