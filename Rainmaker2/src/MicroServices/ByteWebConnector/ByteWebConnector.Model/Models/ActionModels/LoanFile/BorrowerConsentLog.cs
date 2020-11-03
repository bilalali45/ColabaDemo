













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // BorrowerConsentLog

    public partial class BorrowerConsentLog 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? BorrowerConsentId { get; set; } // BorrowerConsentId
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool? IsAccepted { get; set; } // IsAccepted
        public string IpAddress { get; set; } // IpAddress (length: 50)

        // Foreign keys

        /// <summary>
        /// Parent BorrowerConsent pointed by [BorrowerConsentLog].([BorrowerConsentId]) (FK_BorrowerConsentLog_BorrowerConsent)
        /// </summary>
        public virtual BorrowerConsent BorrowerConsent { get; set; } // FK_BorrowerConsentLog_BorrowerConsent

        public BorrowerConsentLog()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
