













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // BorrowerProperty

    public partial class BorrowerProperty 
    {
        public int BorrowerId { get; set; } // BorrowerId (Primary key)
        public int PropertyInfoId { get; set; } // PropertyInfoId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent Borrower pointed by [BorrowerProperty].([BorrowerId]) (FK_BorrowerProperty_Borrower)
        /// </summary>
        public virtual Borrower Borrower { get; set; } // FK_BorrowerProperty_Borrower

        /// <summary>
        /// Parent PropertyInfo pointed by [BorrowerProperty].([PropertyInfoId]) (FK_BorrowerProperty_PropertyInfo)
        /// </summary>
        public virtual PropertyInfo PropertyInfo { get; set; } // FK_BorrowerProperty_PropertyInfo

        public BorrowerProperty()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
