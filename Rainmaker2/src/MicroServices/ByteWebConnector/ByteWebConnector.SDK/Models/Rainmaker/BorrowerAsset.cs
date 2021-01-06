using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class BorrowerAsset
    {
        public int Id { get; set; }
        public int? AssetTypeId { get; set; }
        public string Description { get; set; }
        public int? GiftSourceId { get; set; }
        public bool? IsDeposited { get; set; }
        public decimal? Value { get; set; }
        public decimal? UseForDownpayment { get; set; }
        public DateTime? ValueDate { get; set; }
        public bool? IsJoinType { get; set; }

        //public System.Collections.Generic.ICollection<AssetBorrowerBinder> AssetBorrowerBinders { get; set; }

        public AssetType AssetType { get; set; }

        public GiftSource GiftSource { get; set; }

    }
}