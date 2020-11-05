













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // MortgageOnProperty

    public partial class MortgageOnProperty 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? PropertyOwnId { get; set; } // PropertyOwnId
        public bool? IsFirstMortgage { get; set; } // IsFirstMortgage
        public int? SecondLienTypeId { get; set; } // SecondLienTypeId
        public decimal? MortgageBalance { get; set; } // MortgageBalance
        public decimal? MortgageLimit { get; set; } // MortgageLimit
        public bool PaidAtClosing { get; set; } // PaidAtClosing
        public bool? WasSmTaken { get; set; } // WasSmTaken
        public string LenderName { get; set; } // LenderName (length: 150)
        public string AccountNumber { get; set; } // AccountNumber (length: 50)
        public decimal? MonthlyPayment { get; set; } // MonthlyPayment
        public decimal? PrepaymentPenalty { get; set; } // PrepaymentPenalty
        public int? CurrentTermLeft { get; set; } // CurrentTermLeft
        public bool IsWithEscrow { get; set; } // IsWithEscrow
        public int? ProductFamilyId { get; set; } // ProductFamilyId
        public int? LoanTypeId { get; set; } // LoanTypeId
        public int? AmortizationTypeId { get; set; } // AmortizationTypeId
        public decimal? Rate { get; set; } // Rate
        public int? TermInYears { get; set; } // TermInYears

        // Foreign keys

        /// <summary>
        /// Parent LoanType pointed by [MortgageOnProperty].([LoanTypeId]) (FK_MortgageOnProperty_LoanType)
        /// </summary>
        public virtual LoanType LoanType { get; set; } // FK_MortgageOnProperty_LoanType

        /// <summary>
        /// Parent ProductAmortizationType pointed by [MortgageOnProperty].([AmortizationTypeId]) (FK_MortgageOnProperty_ProductAmortizationType)
        /// </summary>
        public virtual ProductAmortizationType ProductAmortizationType { get; set; } // FK_MortgageOnProperty_ProductAmortizationType

        /// <summary>
        /// Parent ProductFamily pointed by [MortgageOnProperty].([ProductFamilyId]) (FK_MortgageOnProperty_ProductFamily)
        /// </summary>
        public virtual ProductFamily ProductFamily { get; set; } // FK_MortgageOnProperty_ProductFamily

        /// <summary>
        /// Parent PropertyInfo pointed by [MortgageOnProperty].([PropertyOwnId]) (FK_MortgageOnProperty_PropertyInfo)
        /// </summary>
        public virtual PropertyInfo PropertyInfo { get; set; } // FK_MortgageOnProperty_PropertyInfo

        /// <summary>
        /// Parent SecondLienType pointed by [MortgageOnProperty].([SecondLienTypeId]) (FK_MortgageOnProperty_SecondLienType)
        /// </summary>
        public virtual SecondLienType SecondLienType { get; set; } // FK_MortgageOnProperty_SecondLienType

        public MortgageOnProperty()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>