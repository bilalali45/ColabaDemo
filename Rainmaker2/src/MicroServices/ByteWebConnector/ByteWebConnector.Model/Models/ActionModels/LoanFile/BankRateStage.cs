













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // BankRateStage

    public partial class BankRateStage 
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
        /// Child BankRateRequests where [BankRateRequest].[LastStatusId] point to this entity (FK_BankRateRequest_BankRateLastStage)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BankRateRequest> BankRateRequests_LastStatusId { get; set; } // BankRateRequest.FK_BankRateRequest_BankRateLastStage
        /// <summary>
        /// Child BankRateRequests where [BankRateRequest].[StatusId] point to this entity (FK_BankRateRequest_BankRateStage)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BankRateRequest> BankRateRequests_StatusId { get; set; } // BankRateRequest.FK_BankRateRequest_BankRateStage

        public BankRateStage()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 91;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            BankRateRequests_LastStatusId = new System.Collections.Generic.HashSet<BankRateRequest>();
            BankRateRequests_StatusId = new System.Collections.Generic.HashSet<BankRateRequest>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
