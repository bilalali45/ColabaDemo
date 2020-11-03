













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // LoanRequestProductClass

    public partial class LoanRequestProductClass 
    {
        public int LoanRequestId { get; set; } // LoanRequestId (Primary key)
        public int ProductClassId { get; set; } // ProductClassId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent LoanRequest pointed by [LoanRequestProductClass].([LoanRequestId]) (FK_LoanRequestProductClass_LoanRequest)
        /// </summary>
        public virtual LoanRequest LoanRequest { get; set; } // FK_LoanRequestProductClass_LoanRequest

        /// <summary>
        /// Parent ProductClass pointed by [LoanRequestProductClass].([ProductClassId]) (FK_LoanRequestProductClass_ProductClass)
        /// </summary>
        public virtual ProductClass ProductClass { get; set; } // FK_LoanRequestProductClass_ProductClass

        public LoanRequestProductClass()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
