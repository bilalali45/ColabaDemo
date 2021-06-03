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

    // CollateralAssetType
    
    public partial class CollateralAssetType : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child BorrowerAssets where [BorrowerAsset].[CollateralAssetTypeId] point to this entity (FK_BorrowerAsset_CollateralAssetType_Id)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerAsset> BorrowerAssets { get; set; } // BorrowerAsset.FK_BorrowerAsset_CollateralAssetType_Id

        public CollateralAssetType()
        {
            DisplayOrder = 0;
            IsActive = true;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            BorrowerAssets = new System.Collections.Generic.HashSet<BorrowerAsset>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
