













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // PromotionalProgram

    public partial class PromotionalProgram 
    {
        public int Id { get; set; } // Id (Primary key)
        public int BusinessUnitId { get; set; } // BusinessUnitId
        public string PageName { get; set; } // PageName (length: 150)
        public int? StateId { get; set; } // StateId
        public int? CountyId { get; set; } // CountyId
        public int? CityId { get; set; } // CityId
        public int? ZipCode { get; set; } // ZipCode
        public int PageId { get; set; } // PageId
        public string Heading { get; set; } // Heading (length: 500)
        public string Description { get; set; } // Description (length: 1000)
        public bool IsSystem { get; set; } // IsSystem
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public int? LoanPurposeId { get; set; } // LoanPurposeId
        public bool IsActive { get; set; } // IsActive

        // Foreign keys

        /// <summary>
        /// Parent BusinessUnit pointed by [PromotionalProgram].([BusinessUnitId]) (FK_PromotionalProgram_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_PromotionalProgram_BusinessUnit

        /// <summary>
        /// Parent City pointed by [PromotionalProgram].([CityId]) (FK_PromotionalProgram_City)
        /// </summary>
        public virtual City City { get; set; } // FK_PromotionalProgram_City

        /// <summary>
        /// Parent County pointed by [PromotionalProgram].([CountyId]) (FK_PromotionalProgram_County)
        /// </summary>
        public virtual County County { get; set; } // FK_PromotionalProgram_County

        /// <summary>
        /// Parent LoanPurpose pointed by [PromotionalProgram].([LoanPurposeId]) (FK_PromotionalProgram_LoanPurpose)
        /// </summary>
        public virtual LoanPurpose LoanPurpose { get; set; } // FK_PromotionalProgram_LoanPurpose

        /// <summary>
        /// Parent State pointed by [PromotionalProgram].([StateId]) (FK_PromotionalProgram_State)
        /// </summary>
        public virtual State State { get; set; } // FK_PromotionalProgram_State

        public PromotionalProgram()
        {
            IsSystem = false;
            IsActive = true;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>