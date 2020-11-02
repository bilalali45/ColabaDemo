













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // AssetBorrowerBinder

    public partial class AssetBorrowerBinder 
    {
        public int BorrowerAssetId { get; set; } // BorrowerAssetId (Primary key)
        public int BorrowerId { get; set; } // BorrowerId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent Borrower pointed by [AssetBorrowerBinder].([BorrowerId]) (FK_AssetBorrowerBinder_Borrower)
        /// </summary>
        public virtual Borrower Borrower { get; set; } // FK_AssetBorrowerBinder_Borrower

        /// <summary>
        /// Parent BorrowerAsset pointed by [AssetBorrowerBinder].([BorrowerAssetId]) (FK_AssetBorrowerBinder_BorrowerAsset)
        /// </summary>
        public virtual BorrowerAsset BorrowerAsset { get; set; } // FK_AssetBorrowerBinder_BorrowerAsset

        public AssetBorrowerBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
