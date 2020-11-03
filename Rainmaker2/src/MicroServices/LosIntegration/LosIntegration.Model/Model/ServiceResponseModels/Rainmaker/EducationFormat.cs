













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // EducationFormat

    public partial class EducationFormat 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int? MortgageEducationTypeId { get; set; } // MortgageEducationTypeId
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
        /// Child MortgageEducations where [MortgageEducation].[EducationFormatId] point to this entity (FK_MortgageEducation_EducationFormat)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<MortgageEducation> MortgageEducations { get; set; } // MortgageEducation.FK_MortgageEducation_EducationFormat

        // Foreign keys

        /// <summary>
        /// Parent MortgageEducationType pointed by [EducationFormat].([MortgageEducationTypeId]) (FK_EducationFormat_MortgageEducationType)
        /// </summary>
        public virtual MortgageEducationType MortgageEducationType { get; set; } // FK_EducationFormat_MortgageEducationType

        public EducationFormat()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 224;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            MortgageEducations = new System.Collections.Generic.HashSet<MortgageEducation>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
