













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // MortgageEducationType

    public partial class MortgageEducationType 
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
        /// Child EducationFormats where [EducationFormat].[MortgageEducationTypeId] point to this entity (FK_EducationFormat_MortgageEducationType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EducationFormat> EducationFormats { get; set; } // EducationFormat.FK_EducationFormat_MortgageEducationType
        /// <summary>
        /// Child MortgageEducations where [MortgageEducation].[MortgageEducationTypeId] point to this entity (FK_MortgageEducation_MortgageEducationType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<MortgageEducation> MortgageEducations { get; set; } // MortgageEducation.FK_MortgageEducation_MortgageEducationType

        public MortgageEducationType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 213;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            EducationFormats = new System.Collections.Generic.HashSet<EducationFormat>();
            MortgageEducations = new System.Collections.Generic.HashSet<MortgageEducation>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
