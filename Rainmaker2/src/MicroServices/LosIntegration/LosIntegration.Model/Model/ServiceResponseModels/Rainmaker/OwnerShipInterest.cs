













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // OwnerShipInterest

    public partial class OwnerShipInterest 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? BorrowerId { get; set; } // BorrowerId
        public int? PropertyTypeId { get; set; } // PropertyTypeId
        public int? TitleHeldWithId { get; set; } // TitleHeldWithId

        // Foreign keys

        /// <summary>
        /// Parent Borrower pointed by [OwnerShipInterest].([BorrowerId]) (FK_OwnerShipInterest_Borrower)
        /// </summary>
        public virtual Borrower Borrower { get; set; } // FK_OwnerShipInterest_Borrower

        /// <summary>
        /// Parent PropertyType pointed by [OwnerShipInterest].([PropertyTypeId]) (FK_OwnerShipInterest_PropertyType)
        /// </summary>
        public virtual PropertyType PropertyType { get; set; } // FK_OwnerShipInterest_PropertyType

        /// <summary>
        /// Parent TitleHeldWith pointed by [OwnerShipInterest].([TitleHeldWithId]) (FK_OwnerShipInterest_TitleHeldWith)
        /// </summary>
        public virtual TitleHeldWith TitleHeldWith { get; set; } // FK_OwnerShipInterest_TitleHeldWith

        public OwnerShipInterest()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
