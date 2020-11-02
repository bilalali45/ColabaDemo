













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // BorrowerAsset

    public partial class BorrowerAsset 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? AssetTypeId { get; set; } // AssetTypeId
        public string Description { get; set; } // Description (length: 150)
        public int? GiftSourceId { get; set; } // GiftSourceId
        public bool? IsDeposited { get; set; } // IsDeposited
        public decimal? Value { get; set; } // Value
        public decimal? UseForDownpayment { get; set; } // UseForDownpayment
        public System.DateTime? ValueDate { get; set; } // ValueDate
        public bool? IsJoinType { get; set; } // IsJoinType

        // Reverse navigation

        /// <summary>
        /// Child AssetBorrowerBinders where [AssetBorrowerBinder].[BorrowerAssetId] point to this entity (FK_AssetBorrowerBinder_BorrowerAsset)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AssetBorrowerBinder> AssetBorrowerBinders { get; set; } // AssetBorrowerBinder.FK_AssetBorrowerBinder_BorrowerAsset

        // Foreign keys

        /// <summary>
        /// Parent AssetType pointed by [BorrowerAsset].([AssetTypeId]) (FK_BorrowerAsset_AssetType)
        /// </summary>
        public virtual AssetType AssetType { get; set; } // FK_BorrowerAsset_AssetType

        /// <summary>
        /// Parent GiftSource pointed by [BorrowerAsset].([GiftSourceId]) (FK_BorrowerAsset_GiftSource)
        /// </summary>
        public virtual GiftSource GiftSource { get; set; } // FK_BorrowerAsset_GiftSource

        public BorrowerAsset()
        {
            AssetBorrowerBinders = new System.Collections.Generic.HashSet<AssetBorrowerBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
