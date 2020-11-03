













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LoanDocument

    public partial class LoanDocument 
    {
        public int Id { get; set; } // Id (Primary key)
        public string ClientFileName { get; set; } // ClientFileName (length: 255)
        public string ServerFileName { get; set; } // ServerFileName (length: 500)
        public int? LoanApplicationId { get; set; } // LoanApplicationId
        public int? LoanDocumentTypeId { get; set; } // LoanDocumentTypeId
        public int? LoanDocumentSubTypeId { get; set; } // LoanDocumentSubTypeId
        public int? LoanDocumentStatusId { get; set; } // LoanDocumentStatusId
        public string MessageForCustomer { get; set; } // MessageForCustomer (length: 500)
        public int EntityTypeId { get; set; } // EntityTypeId
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public bool IsDeleted { get; set; } // IsDeleted

        // Foreign keys

        /// <summary>
        /// Parent LoanApplication pointed by [LoanDocument].([LoanApplicationId]) (FK_LoanDocument_LoanApplication)
        /// </summary>
        public virtual LoanApplication LoanApplication { get; set; } // FK_LoanDocument_LoanApplication

        /// <summary>
        /// Parent LoanDocumentStatusList pointed by [LoanDocument].([LoanDocumentStatusId]) (FK_LoanDocument_LoanDocumentStatusList)
        /// </summary>
        public virtual LoanDocumentStatusList LoanDocumentStatusList { get; set; } // FK_LoanDocument_LoanDocumentStatusList

        /// <summary>
        /// Parent LoanDocumentSubType pointed by [LoanDocument].([LoanDocumentSubTypeId]) (FK_LoanDocument_LoanDocumentSubType)
        /// </summary>
        public virtual LoanDocumentSubType LoanDocumentSubType { get; set; } // FK_LoanDocument_LoanDocumentSubType

        /// <summary>
        /// Parent LoanDocumentType pointed by [LoanDocument].([LoanDocumentTypeId]) (FK_LoanDocument_LoanDocumentType)
        /// </summary>
        public virtual LoanDocumentType LoanDocumentType { get; set; } // FK_LoanDocument_LoanDocumentType

        public LoanDocument()
        {
            EntityTypeId = 211;
            IsDeleted = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
