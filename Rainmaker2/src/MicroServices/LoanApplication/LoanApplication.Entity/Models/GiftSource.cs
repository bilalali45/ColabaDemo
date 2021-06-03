// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 2.1
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace LoanApplicationDb.Entity.Models
{
    using System;
    using System.Collections.Generic;

    // GiftSource
    
    public partial class GiftSource : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public bool IsAccountType { get; set; } // IsAccountType
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted
        public string Image { get; set; } // Image (length: 50)

        // Reverse navigation

        /// <summary>
        /// Child AssetTypeGiftSourceBinders where [AssetTypeGiftSourceBinder].[GiftSourceId] point to this entity (FK_AssetTypeGiftSourceBinder_GiftSource_Id)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AssetTypeGiftSourceBinder> AssetTypeGiftSourceBinders { get; set; } // AssetTypeGiftSourceBinder.FK_AssetTypeGiftSourceBinder_GiftSource_Id
        /// <summary>
        /// Child BorrowerAssets where [BorrowerAsset].[GiftSourceId] point to this entity (FK_BorrowerAsset_GiftSource)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerAsset> BorrowerAssets { get; set; } // BorrowerAsset.FK_BorrowerAsset_GiftSource

        public GiftSource()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 225;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            AssetTypeGiftSourceBinders = new System.Collections.Generic.HashSet<AssetTypeGiftSourceBinder>();
            BorrowerAssets = new System.Collections.Generic.HashSet<BorrowerAsset>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
