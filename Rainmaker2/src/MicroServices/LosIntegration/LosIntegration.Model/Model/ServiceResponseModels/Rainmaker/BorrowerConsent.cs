













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // BorrowerConsent

    public partial class BorrowerConsent 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? ConsentTypeId { get; set; } // ConsentTypeId
        public int? BorrowerId { get; set; } // BorrowerId
        public int? LoanApplicationId { get; set; } // LoanApplicationId
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool? IsAccepted { get; set; } // IsAccepted
        public string IpAddress { get; set; } // IpAddress (length: 50)

        // Reverse navigation

        /// <summary>
        /// Child BorrowerConsentLogs where [BorrowerConsentLog].[BorrowerConsentId] point to this entity (FK_BorrowerConsentLog_BorrowerConsent)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerConsentLog> BorrowerConsentLogs { get; set; } // BorrowerConsentLog.FK_BorrowerConsentLog_BorrowerConsent

        // Foreign keys

        /// <summary>
        /// Parent Borrower pointed by [BorrowerConsent].([BorrowerId]) (FK_BorrowerConsent_Borrower)
        /// </summary>
        public virtual Borrower Borrower { get; set; } // FK_BorrowerConsent_Borrower

        /// <summary>
        /// Parent ConsentType pointed by [BorrowerConsent].([ConsentTypeId]) (FK_BorrowerConsent_ConsentType)
        /// </summary>
        public virtual ConsentType ConsentType { get; set; } // FK_BorrowerConsent_ConsentType

        /// <summary>
        /// Parent LoanApplication pointed by [BorrowerConsent].([LoanApplicationId]) (FK_BorrowerConsent_LoanApplication)
        /// </summary>
        public virtual LoanApplication LoanApplication { get; set; } // FK_BorrowerConsent_LoanApplication

        public BorrowerConsent()
        {
            BorrowerConsentLogs = new System.Collections.Generic.HashSet<BorrowerConsentLog>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
