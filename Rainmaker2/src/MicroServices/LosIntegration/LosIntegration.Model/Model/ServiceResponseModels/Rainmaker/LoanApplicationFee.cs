













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LoanApplicationFee

    public partial class LoanApplicationFee 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public int? LoanApplicationId { get; set; } // LoanApplicationId
        public int? FeeId { get; set; } // FeeId
        public int? FeeDetailId { get; set; } // FeeDetailId
        public int? OpportunityFeeId { get; set; } // OpportunityFeeId
        public bool? IsCredit { get; set; } // IsCredit
        public decimal? Value { get; set; } // Value
        public int? CreditedById { get; set; } // CreditedById
        public int? PaidById { get; set; } // PaidById
        public decimal? PaidBeforeClosing { get; set; } // PaidBeforeClosing
        public decimal? PaidAtClosing { get; set; } // PaidAtClosing

        // Foreign keys

        /// <summary>
        /// Parent Fee pointed by [LoanApplicationFee].([FeeId]) (FK_LoanApplicationFee_Fee)
        /// </summary>
        public virtual Fee Fee { get; set; } // FK_LoanApplicationFee_Fee

        /// <summary>
        /// Parent FeeDetail pointed by [LoanApplicationFee].([FeeDetailId]) (FK_LoanApplicationFee_FeeDetail)
        /// </summary>
        public virtual FeeDetail FeeDetail { get; set; } // FK_LoanApplicationFee_FeeDetail

        /// <summary>
        /// Parent LoanApplication pointed by [LoanApplicationFee].([LoanApplicationId]) (FK_LoanApplicationFee_LoanApplication)
        /// </summary>
        public virtual LoanApplication LoanApplication { get; set; } // FK_LoanApplicationFee_LoanApplication

        /// <summary>
        /// Parent OpportunityFee pointed by [LoanApplicationFee].([OpportunityFeeId]) (FK_LoanApplicationFee_OpportunityFee)
        /// </summary>
        public virtual OpportunityFee OpportunityFee { get; set; } // FK_LoanApplicationFee_OpportunityFee

        /// <summary>
        /// Parent PaidBy pointed by [LoanApplicationFee].([PaidById]) (FK_LoanApplicationFee_PaidBy)
        /// </summary>
        public virtual PaidBy PaidBy { get; set; } // FK_LoanApplicationFee_PaidBy

        public LoanApplicationFee()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
