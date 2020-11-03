













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LocaleStringResource

    public partial class LocaleStringResource 
    {
        public int Id { get; set; } // Id (Primary key)
        public int LanguageId { get; set; } // LanguageId
        public string ResourceName { get; set; } // ResourceName (length: 256)
        public string ResourceValue { get; set; } // ResourceValue
        public string Location { get; set; } // Location
        public int? ResourceForId { get; set; } // ResourceForId
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public string Description { get; set; } // Description (length: 256)
        public bool IsDifferentForBusinessUnit { get; set; } // IsDifferentForBusinessUnit

        // Reverse navigation

        /// <summary>
        /// Child AdsSourceMessages where [AdsSourceMessage].[LocalStringResourceId] point to this entity (FK_AdsSourceMessage_LocaleStringResource)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AdsSourceMessage> AdsSourceMessages { get; set; } // AdsSourceMessage.FK_AdsSourceMessage_LocaleStringResource
        /// <summary>
        /// Child RateServiceParameters where [RateServiceParameter].[StringResourceId] point to this entity (FK_RateServiceParameter_LocaleStringResource)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<RateServiceParameter> RateServiceParameters { get; set; } // RateServiceParameter.FK_RateServiceParameter_LocaleStringResource

        // Foreign keys

        /// <summary>
        /// Parent BusinessUnit pointed by [LocaleStringResource].([BusinessUnitId]) (FK_LocaleStringResource_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_LocaleStringResource_BusinessUnit

        /// <summary>
        /// Parent Language pointed by [LocaleStringResource].([LanguageId]) (FK_LocaleStringResource_Language)
        /// </summary>
        public virtual Language Language { get; set; } // FK_LocaleStringResource_Language

        public LocaleStringResource()
        {
            EntityTypeId = 94;
            IsDifferentForBusinessUnit = true;
            AdsSourceMessages = new System.Collections.Generic.HashSet<AdsSourceMessage>();
            RateServiceParameters = new System.Collections.Generic.HashSet<RateServiceParameter>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
