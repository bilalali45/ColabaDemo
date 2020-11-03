













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // Fee

    public partial class Fee 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public string Number { get; set; } // Number (length: 50)
        public int? FeeTypeId { get; set; } // FeeTypeId
        public int? FeeBlockId { get; set; } // FeeBlockId
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int DisplayOrder { get; set; } // DisplayOrder
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted
        public bool IsDisplayZero { get; set; } // IsDisplayZero
        public int? FeeCategoryId { get; set; } // FeeCategoryId

        // Reverse navigation

        /// <summary>
        /// Child FeeDetails where [FeeDetail].[FeeId] point to this entity (FK_FeeDetail_Fee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<FeeDetail> FeeDetails { get; set; } // FeeDetail.FK_FeeDetail_Fee
        /// <summary>
        /// Child LoanApplicationFees where [LoanApplicationFee].[FeeId] point to this entity (FK_LoanApplicationFee_Fee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanApplicationFee> LoanApplicationFees { get; set; } // LoanApplicationFee.FK_LoanApplicationFee_Fee
        /// <summary>
        /// Child OpportunityFees where [OpportunityFee].[FeeId] point to this entity (FK_OpportunityFee_Fee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityFee> OpportunityFees { get; set; } // OpportunityFee.FK_OpportunityFee_Fee
        /// <summary>
        /// Child TemplateFormPlotDetails where [TemplateFormPlotDetail].[FeeId] point to this entity (FK_TemplateFormPlotDetail_Fee)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TemplateFormPlotDetail> TemplateFormPlotDetails { get; set; } // TemplateFormPlotDetail.FK_TemplateFormPlotDetail_Fee

        // Foreign keys

        /// <summary>
        /// Parent FeeBlock pointed by [Fee].([FeeBlockId]) (FK_Fee_FeeBlock)
        /// </summary>
        public virtual FeeBlock FeeBlock { get; set; } // FK_Fee_FeeBlock

        /// <summary>
        /// Parent FeeCategory pointed by [Fee].([FeeCategoryId]) (FK_Fee_FeeCategory)
        /// </summary>
        public virtual FeeCategory FeeCategory { get; set; } // FK_Fee_FeeCategory

        /// <summary>
        /// Parent FeeType pointed by [Fee].([FeeTypeId]) (FK_Fee_FeeType)
        /// </summary>
        public virtual FeeType FeeType { get; set; } // FK_Fee_FeeType

        public Fee()
        {
            IsActive = true;
            EntityTypeId = 123;
            IsDefault = false;
            IsSystem = false;
            DisplayOrder = 0;
            IsDeleted = false;
            IsDisplayZero = false;
            FeeDetails = new System.Collections.Generic.HashSet<FeeDetail>();
            LoanApplicationFees = new System.Collections.Generic.HashSet<LoanApplicationFee>();
            OpportunityFees = new System.Collections.Generic.HashSet<OpportunityFee>();
            TemplateFormPlotDetails = new System.Collections.Generic.HashSet<TemplateFormPlotDetail>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
