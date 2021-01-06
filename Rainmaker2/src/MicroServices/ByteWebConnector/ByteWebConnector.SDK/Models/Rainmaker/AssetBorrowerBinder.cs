namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class AssetBorrowerBinder
    {
        public int BorrowerAssetId { get; set; }
        public int BorrowerId { get; set; }

        public Borrower Borrower { get; set; }

        public BorrowerAsset BorrowerAsset { get; set; }
    }
}