













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // LoanRequestProduct

    public partial class LoanRequestProduct 
    {
        public int LoanRequestId { get; set; } // LoanRequestId (Primary key)
        public int ProductId { get; set; } // ProductId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent LoanRequest pointed by [LoanRequestProduct].([LoanRequestId]) (FK_LoanRequestProduct_LoanRequest)
        /// </summary>
        public virtual LoanRequest LoanRequest { get; set; } // FK_LoanRequestProduct_LoanRequest

        /// <summary>
        /// Parent Product pointed by [LoanRequestProduct].([ProductId]) (FK_LoanRequestProduct_Product)
        /// </summary>
        public virtual Product Product { get; set; } // FK_LoanRequestProduct_Product

        public LoanRequestProduct()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
