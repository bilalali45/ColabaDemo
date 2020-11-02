













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // TransactionInfoBinder

    public partial class TransactionInfoBinder 
    {
        public int LoanApplicationId { get; set; } // LoanApplicationId (Primary key)
        public int TransactionInfoId { get; set; } // TransactionInfoId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent LoanApplication pointed by [TransactionInfoBinder].([LoanApplicationId]) (FK_TransactionInfoBinder_LoanApplication)
        /// </summary>
        public virtual LoanApplication LoanApplication { get; set; } // FK_TransactionInfoBinder_LoanApplication

        /// <summary>
        /// Parent TransactionInfo pointed by [TransactionInfoBinder].([TransactionInfoId]) (FK_TransactionInfoBinder_TransactionInfo)
        /// </summary>
        public virtual TransactionInfo TransactionInfo { get; set; } // FK_TransactionInfoBinder_TransactionInfo

        public TransactionInfoBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
