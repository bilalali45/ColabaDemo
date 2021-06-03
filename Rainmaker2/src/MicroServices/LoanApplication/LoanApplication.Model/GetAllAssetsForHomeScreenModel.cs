using System.Collections.Generic;

namespace LoanApplication.Model
{
    public class GetAllAssetsForHomeScreenModel
    {
        public decimal? TotalAssetsValue { get; set; }
        public List<Borrower> Borrowers { get; set; }

        public class Borrower
        {
            public int BorrowerId { get; set; }
            public string BorrowerName { get; set; }
            public int? OwnTypeId { get; set; }
            public string OwnTypeName { get; set; }
            public string OwnTypeDisplayName { get; set; }
            public List<BorrowerAsset> BorrowerAssets { get; set; }
            public decimal? AssetsValue { get; set; }
        }

        public class BorrowerAsset
        {
            public string AssetName { get; set; }

            public decimal? AssetValue { get; set; }
            public int AssetId { get; set; }

            public int AssetTypeID { get; set; }
            public int AssetCategoryId { get; set; }
            public string AssetCategoryName { get; set; }
            public string AssetTypeName { get; set; }

        }
    }
}