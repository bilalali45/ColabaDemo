
















namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // BankRatePricingRequest

    public partial class BankRatePricingRequest 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? BankRequestId { get; set; } // BankRequestId
        public System.DateTime? StartDateTimeUtc { get; set; } // StartDateTimeUtc
        public System.DateTime? EndDateTimeUtc { get; set; } // EndDateTimeUtc
        public decimal? PropertyValue { get; set; } // PropertyValue
        public decimal? LoanAmount { get; set; } // LoanAmount
        public int? StateId { get; set; } // StateId
        public int? CountyId { get; set; } // CountyId
        public int? LoanPurposeId { get; set; } // LoanPurposeId
        public int? PropertyUsageId { get; set; } // PropertyUsageId
        public int? PropertyTypeId { get; set; } // PropertyTypeId
        public int? CreditScoreId { get; set; } // CreditScoreId
        public int? LoanLockPeriodId { get; set; } // LoanLockPeriodId
        public string ProductIds { get; set; } // ProductIds
        public string RequestXml { get; set; } // RequestXml (length: 1073741823)
        public string ResponseXml { get; set; } // ResponseXml (length: 1073741823)
        public bool? IsDone { get; set; } // IsDone
        public string Error { get; set; } // Error
        public int? NoOfTry { get; set; } // NoOfTry
        public int EntityTypeId { get; set; } // EntityTypeId

        // Reverse navigation

        /// <summary>
        /// Child BankRateArchives where [BankRateArchive].[BankRatePriceRequestId] point to this entity (FK_BankRateArchive_BankRatePricingRequest)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BankRateArchive> BankRateArchives { get; set; } // BankRateArchive.FK_BankRateArchive_BankRatePricingRequest

        public BankRatePricingRequest()
        {
            EntityTypeId = 19;
            BankRateArchives = new System.Collections.Generic.HashSet<BankRateArchive>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
