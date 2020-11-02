













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LoanPurposeProgram

    public partial class LoanPurposeProgram 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public bool IsPurchase { get; set; } // IsPurchase
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
        /// Child LoanApplications where [LoanApplication].[LoanPurposeProgramId] point to this entity (FK_LoanApplication_LoanPurposeProgram)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanApplication> LoanApplications { get; set; } // LoanApplication.FK_LoanApplication_LoanPurposeProgram

        public LoanPurposeProgram()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 217;
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
