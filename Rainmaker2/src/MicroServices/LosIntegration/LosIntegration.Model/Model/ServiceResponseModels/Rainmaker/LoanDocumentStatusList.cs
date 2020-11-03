













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LoanDocumentStatusList

    public partial class LoanDocumentStatusList 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public bool? IsSystemInputOnly { get; set; } // IsSystemInputOnly
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted
        public string EmployeeDisplay { get; set; } // EmployeeDisplay (length: 150)
        public string CustomerDisplay { get; set; } // CustomerDisplay (length: 150)

        // Reverse navigation

        /// <summary>
        /// Child LoanDocuments where [LoanDocument].[LoanDocumentStatusId] point to this entity (FK_LoanDocument_LoanDocumentStatusList)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanDocument> LoanDocuments { get; set; } // LoanDocument.FK_LoanDocument_LoanDocumentStatusList

        public LoanDocumentStatusList()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 209;
            IsDefault = false;
            IsSystem = false;
            IsSystemInputOnly = false;
            IsDeleted = false;
            LoanDocuments = new System.Collections.Generic.HashSet<LoanDocument>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
