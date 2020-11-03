













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // BorrowerAccount

    public partial class BorrowerAccount 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? AccountTypeId { get; set; } // AccountTypeId
        public string Name { get; set; } // Name (length: 150)
        public string AccountTitle { get; set; } // AccountTitle (length: 150)
        public string AccountNumber { get; set; } // AccountNumber (length: 150)
        public decimal? Balance { get; set; } // Balance
        public decimal? UseForDownpayment { get; set; } // UseForDownpayment
        public System.DateTime? BalanceDate { get; set; } // BalanceDate
        public bool? IsJoinType { get; set; } // IsJoinType

        // Reverse navigation

        /// <summary>
        /// Child BorrowerAccountBinders where [BorrowerAccountBinder].[BorrowerAccountId] point to this entity (FK_BorrowerAccountBinder_BorrowerAccount)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerAccountBinder> BorrowerAccountBinders { get; set; } // BorrowerAccountBinder.FK_BorrowerAccountBinder_BorrowerAccount

        // Foreign keys

        /// <summary>
        /// Parent AccountType pointed by [BorrowerAccount].([AccountTypeId]) (FK_BorrowerAccount_AccountType)
        /// </summary>
        public virtual AccountType AccountType { get; set; } // FK_BorrowerAccount_AccountType

        public BorrowerAccount()
        {
            BorrowerAccountBinders = new System.Collections.Generic.HashSet<BorrowerAccountBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
