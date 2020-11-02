













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LoanDocumentSubType

    public partial class LoanDocumentSubType 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int? LoanDocumentTypeId { get; set; } // LoanDocumentTypeId
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
        /// Child LoanDocuments where [LoanDocument].[LoanDocumentSubTypeId] point to this entity (FK_LoanDocument_LoanDocumentSubType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanDocument> LoanDocuments { get; set; } // LoanDocument.FK_LoanDocument_LoanDocumentSubType

        // Foreign keys

        /// <summary>
        /// Parent LoanDocumentType pointed by [LoanDocumentSubType].([LoanDocumentTypeId]) (FK_LoanDocumentSubType_LoanDocumentType)
        /// </summary>
        public virtual LoanDocumentType LoanDocumentType { get; set; } // FK_LoanDocumentSubType_LoanDocumentType

        public LoanDocumentSubType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 206;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            LoanDocuments = new System.Collections.Generic.HashSet<LoanDocument>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
