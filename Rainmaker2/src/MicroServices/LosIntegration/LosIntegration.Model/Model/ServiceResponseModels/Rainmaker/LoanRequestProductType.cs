













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LoanRequestProductType

    public partial class LoanRequestProductType 
    {
        public int LoanRequestId { get; set; } // LoanRequestId (Primary key)
        public int ProductTypeId { get; set; } // ProductTypeId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent LoanRequest pointed by [LoanRequestProductType].([LoanRequestId]) (FK_LoanRequestProductType_LoanRequest)
        /// </summary>
        public virtual LoanRequest LoanRequest { get; set; } // FK_LoanRequestProductType_LoanRequest

        /// <summary>
        /// Parent ProductType pointed by [LoanRequestProductType].([ProductTypeId]) (FK_LoanRequestProductType_ProductType)
        /// </summary>
        public virtual ProductType ProductType { get; set; } // FK_LoanRequestProductType_ProductType

        public LoanRequestProductType()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
