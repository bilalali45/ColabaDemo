













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // ProjectType

    public partial class ProjectType 
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
        /// Child LoanApplications where [LoanApplication].[ProjectTypeId] point to this entity (FK_LoanApplication_ProjectType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanApplication> LoanApplications { get; set; } // LoanApplication.FK_LoanApplication_ProjectType

        public ProjectType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 215;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            LoanApplications = new System.Collections.Generic.HashSet<LoanApplication>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
