













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // BorrowerSupportPayment

    public partial class BorrowerSupportPayment 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? BorrowerId { get; set; } // BorrowerId
        public int? SupportPaymentTypeId { get; set; } // SupportPaymentTypeId
        public decimal? MonthlyPayment { get; set; } // MonthlyPayment
        public int? RemainingMonth { get; set; } // RemainingMonth
        public string PaymentTo { get; set; } // PaymentTo (length: 150)

        // Foreign keys

        /// <summary>
        /// Parent Borrower pointed by [BorrowerSupportPayment].([BorrowerId]) (FK_BorrowerSupportPayment_Borrower)
        /// </summary>
        public virtual Borrower Borrower { get; set; } // FK_BorrowerSupportPayment_Borrower

        /// <summary>
        /// Parent SupportPaymentType pointed by [BorrowerSupportPayment].([SupportPaymentTypeId]) (FK_BorrowerSupportPayment_SupportPaymentType)
        /// </summary>
        public virtual SupportPaymentType SupportPaymentType { get; set; } // FK_BorrowerSupportPayment_SupportPaymentType

        public BorrowerSupportPayment()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
