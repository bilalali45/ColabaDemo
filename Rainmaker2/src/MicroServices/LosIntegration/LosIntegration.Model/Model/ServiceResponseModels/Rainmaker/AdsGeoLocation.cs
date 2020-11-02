













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // AdsGeoLocation

    public partial class AdsGeoLocation 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? AdsSourceId { get; set; } // AdsSourceId
        public int StateId { get; set; } // StateId
        public int CityId { get; set; } // CityId
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

        // Foreign keys

        /// <summary>
        /// Parent AdsSource pointed by [AdsGeoLocation].([AdsSourceId]) (FK_AdsGeoLocation_AdsSource)
        /// </summary>
        public virtual AdsSource AdsSource { get; set; } // FK_AdsGeoLocation_AdsSource

        /// <summary>
        /// Parent City pointed by [AdsGeoLocation].([CityId]) (FK_AdsGeoLocation_City)
        /// </summary>
        public virtual City City { get; set; } // FK_AdsGeoLocation_City

        /// <summary>
        /// Parent State pointed by [AdsGeoLocation].([StateId]) (FK_AdsGeoLocation_State)
        /// </summary>
        public virtual State State { get; set; } // FK_AdsGeoLocation_State

        public AdsGeoLocation()
        {
            DisplayOrder = 0;
            IsActive = true;
            IsDefault = false;
            IsSystem = false;
            EntityTypeId = 121;
            IsDeleted = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
