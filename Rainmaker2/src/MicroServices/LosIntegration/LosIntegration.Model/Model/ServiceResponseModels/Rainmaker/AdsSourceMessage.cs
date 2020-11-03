













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // AdsSourceMessage

    public partial class AdsSourceMessage 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? AdSourceId { get; set; } // AdSourceId
        public int? LocalStringResourceId { get; set; } // LocalStringResourceId
        public string ResourceName { get; set; } // ResourceName (length: 256)
        public int? MessageLocationId { get; set; } // MessageLocationId

        // Foreign keys

        /// <summary>
        /// Parent AdsSource pointed by [AdsSourceMessage].([AdSourceId]) (FK_AdsSourceMessage_AdsSource)
        /// </summary>
        public virtual AdsSource AdsSource { get; set; } // FK_AdsSourceMessage_AdsSource

        /// <summary>
        /// Parent LocaleStringResource pointed by [AdsSourceMessage].([LocalStringResourceId]) (FK_AdsSourceMessage_LocaleStringResource)
        /// </summary>
        public virtual LocaleStringResource LocaleStringResource { get; set; } // FK_AdsSourceMessage_LocaleStringResource

        public AdsSourceMessage()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
