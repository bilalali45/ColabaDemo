













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LoanPurpose

    public partial class LoanPurpose 
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
        /// Child BankRateParameters where [BankRateParameter].[LoanPurposeId] point to this entity (FK_BankRateParameter_LoanPurpose)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BankRateParameter> BankRateParameters { get; set; } // BankRateParameter.FK_BankRateParameter_LoanPurpose
        /// <summary>
        /// Child LoanApplications where [LoanApplication].[LoanPurposeId] point to this entity (FK_LoanApplication_LoanPurpose)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanApplication> LoanApplications { get; set; } // LoanApplication.FK_LoanApplication_LoanPurpose
        /// <summary>
        /// Child LoanGoals where [LoanGoal].[LoanPurposeId] point to this entity (FK_LoanGoal_LoanPurpose)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanGoal> LoanGoals { get; set; } // LoanGoal.FK_LoanGoal_LoanPurpose
        /// <summary>
        /// Child LoanRequests where [LoanRequest].[LoanPurposeId] point to this entity (FK_LoanRequest_LoanPurpose)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequest> LoanRequests { get; set; } // LoanRequest.FK_LoanRequest_LoanPurpose
        /// <summary>
        /// Child PromotionalPrograms where [PromotionalProgram].[LoanPurposeId] point to this entity (FK_PromotionalProgram_LoanPurpose)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PromotionalProgram> PromotionalPrograms { get; set; } // PromotionalProgram.FK_PromotionalProgram_LoanPurpose
        /// <summary>
        /// Child QuestionPurposeBinders where [QuestionPurposeBinder].[LoanPurposeId] point to this entity (FK_QuestionPurposeBinder_LoanPurpose)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<QuestionPurposeBinder> QuestionPurposeBinders { get; set; } // QuestionPurposeBinder.FK_QuestionPurposeBinder_LoanPurpose
        /// <summary>
        /// Child ReviewComments where [ReviewComment].[LoanPurposeId] point to this entity (FK_ReviewComment_LoanPurpose)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ReviewComment> ReviewComments { get; set; } // ReviewComment.FK_ReviewComment_LoanPurpose

        public LoanPurpose()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 4;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            BankRateParameters = new System.Collections.Generic.HashSet<BankRateParameter>();
            LoanApplications = new System.Collections.Generic.HashSet<LoanApplication>();
            LoanGoals = new System.Collections.Generic.HashSet<LoanGoal>();
            LoanRequests = new System.Collections.Generic.HashSet<LoanRequest>();
            PromotionalPrograms = new System.Collections.Generic.HashSet<PromotionalProgram>();
            QuestionPurposeBinders = new System.Collections.Generic.HashSet<QuestionPurposeBinder>();
            ReviewComments = new System.Collections.Generic.HashSet<ReviewComment>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
